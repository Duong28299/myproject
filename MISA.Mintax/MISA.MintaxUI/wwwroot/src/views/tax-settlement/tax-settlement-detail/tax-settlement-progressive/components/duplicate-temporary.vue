<template>
  <div class="duplicate-temporary">
    <div class="check-data-duplicate">
      <div class="header-title">
        <!-- Nút back quay trở lại trang trước -->
        <div
          id="img-back"
          class="pointer d-flex align-items-center"
          @click="goBack"
        >
          <img src="@/icons/ico-expand.png" />
        </div>
        <!-- Title của trang -->
        <div>
          <span style="font-size: 21px">{{ title }}</span>
        </div>
      </div>
      <div class="filter">
        <!-- ô input tìm kiếm -->
        <fieldinput
          id="duplicate-temporary"
          v-if="totalDuplicate > 0"
          type="input-icon"
          :placeholder="'Tìm kiếm theo tên, mã nhân viên'"
          classicon="icon-search"
          width="402px"
          :valueInput="filterBar.inputSearch"
            @value="filterBar.inputSearch = $event"
        />
        <!-- thẻ hiển thị tổng số lượng bản ghi trùng -->
        <div class="button-filter">
          <div class="content-btn">
            Tổng số dữ liệu:
            <div class="total-row-violet">{{ totalDuplicate }}</div>
          </div>
        </div>
        <div class="label info-warning">
          <img class="icon" src="@/icons/Group 20045.png" />
          Ứng dụng sẽ không cho phép cập nhật dữ liệu trùng với dữ liệu được
          sinh tự động từ bảng tạm tính thuế
        </div>
      </div>
    </div>
    <div class="table-data-duplicate-temporary">
      <tbl
      propertyError="ImportValidError"
        :styleFullError="{
          'border-top': '1px solid red',
          'border-left': '1px solid red',
          'border-color': 'red',
        }"
        :styleHalfError="{
          'border-left': '1px solid red',
          'border-color': 'red',
        }"
        v-if="!isEmpty"
        typeTable="tableExcel"
        :tooltipStatus="tooltipStatus"
        :loading="loading"
        :style="setStyle()"
        :dataTable="tableData"
        :listColumn="listColumn"
        :pageSizes="pageSizes"
        :total="allFilterData.length"
        :havePagination="true"
        width="100%"
        :tooltip="true"
        :statusFlag="true"
        :statusExcelStatus="excelStatus()"
        @pagination="pagination"
      >
      </tbl>
      <div
        class="mode-btn"
        v-if="tableData.length == 0 && isEmpty == true && !loading"
      >
        <img
          src="@/assets/images/Group 18279.png"
          alt="not have data"
          style="position: relative"
        />
        <span>Không tìm thấy kết quả phù hợp</span>
      </div>
    </div>
  </div>
</template>

<script>
import fieldinput from "@/components/inputs";
import table from "@/components/tables";
import excelStatus from "@/enums/excelStatus.js";
import { filterData } from "@/utils/filter-paginate.js";
export default {
  props: {
    listColumn: {
      type: Array,
      default: null,
    },
  },
  components: {
    fieldinput,
    tbl: table,
  },
  data() {
    return {
      title: "Danh sách dữ liệu trùng với dữ liệu đã có trên bảng quyết toán",
      loading: false,
      tableData: [],
      isEmpty: false,
      totalDuplicate: 0,
      allFilterData:[],
      filterBar: {
        pageIndex: 1,
        pageSize: 25,
        inputSearch: "",
      },
      pageSizes: [25, 50, 75, 100],
      tooltipStatus:[
        {
          value: 4,
          status: "Dữ liệu này sẽ ghi đè vào dữ liệu đã có trên bảng"
        },
        {
          value: 5,
          status: "Dữ liệu này sẽ không được thêm vào bảng quyết toán"
        }
      ]
    };
  },
  methods: {
    setStyle() {
      return { height: "calc(100vh - 196px - 25px)" };
    },
    excelStatus() {
      return excelStatus.EXCEL_STATUS;
    },
    formatPrice(value) {
      return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    goBack() {
      this.$emit("backToMainPage", false);
    },

    /**
     * Hàm lấy dữ liệu danh sách dữ liệu trùng trên DB (phân trang) từ store
     * Created By : NQKHAI(3/11/2020)
     * Modified by bvbao(4/11/2020)
     */
    getData() {
      var dataDuplicate = this.$store.getters.listDuplicateDB;
      filterData();
      if (dataDuplicate != null) {
        let pageSize = this.filterBar.pageSize;
        let pageIndex = this.filterBar.pageIndex;
        let search = this.filterBar.inputSearch;
        //filter theo input search
        this.allFilterData = dataDuplicate.filter((data) =>
          data.EmployeeCode.toLowerCase().includes(search.toLowerCase()) ||
          data.FullName.toLowerCase().includes(search.toLowerCase()) ||
          data.CitizenIdentityNo.toLowerCase().includes(search.toLowerCase()) ||
          data.TaxNo.toLowerCase().includes(search.toLowerCase())
        );
        this.tableData = this.allFilterData
          .skip((pageIndex - 1) * pageSize)
          .limit(pageSize);
        this.isEmpty = false;
        this.totalDuplicate = dataDuplicate.length;
        this.loading = false;
      } else {
        this.isEmpty = true;
        this.loading = false;
        this.tableData = [];
        this.totalDuplicate = 0;
      }
    },

    /**
     * Hàm xử lý sự kiện phân trang
     * Khi thay đổi trang hiện tại hoặc pageSize
     * Created By : NQKHAI(3/11/2020)
     */
    pagination(val) {
      this.filterBar.pageSize = val.limit;
      this.filterBar.pageIndex = val.page;
      this.getData();
    },
  },
  watch: {
    tableData: {
      handler(val) {
        if (val.length == 0) this.isEmpty = true;
        else this.isEmpty = false;
      },
    },
    filterBar: {
      handler() {
        this.getData();
      },
      deep: true,
    },
  },
  created() {
    this.getData();
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
.el-table__body-wrapper {
  overflow-x: auto;
  overflow-y: auto;
  height: 100%;
}
.duplicate-temporary {
  background-color: $background-grey;
  padding-left: 26px;
  padding-right: 26px;
  .check-data-duplicate {
    height: 125px;
    .header-title {
      display: flex;
      height: 65px;
      align-items: center;
      #img-back {
        img {
          padding: 0 10px 0 10px;
        }
      }
    }
    .filter {
      display: flex;
      height: 43px;
      .button-filter {
        padding-left: 12px;
        background-color: $white;
        margin-left: 10px;
        height: 100%;
        border-radius: 5px;
        display: flex;
        align-items: center;
        text-align-last: center;
        .content-btn {
          display: flex;
          .total-row-violet {
            margin: 0 12px;
            font-size: 16px;
            color: $violet-duplicate;
            font-weight: bold;
          }
        }
      }
      .info-warning {
        margin-left: 10px;
        font-family: $_fontregular;
        color: $light-seconds;
        display: flex;
        align-items: center;
        .icon {
          margin-right: 10px;
        }
      }
    }
  }
  .table-data-duplicate-temporary .tableData .el-table__body-wrapper{
    height: calc(100% - 53px);
    overflow-x: inherit;
    overflow-y: initial;
  }
}
.el-input__inner {
  height: 43px;
}
.mode-btn {
  position: absolute;
  top: 57%;
  left: 50%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  transform: translate(-50%, -50%);
}
</style>
