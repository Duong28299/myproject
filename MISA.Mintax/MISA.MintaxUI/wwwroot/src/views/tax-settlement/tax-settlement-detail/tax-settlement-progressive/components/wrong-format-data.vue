<template>
  <div class="wrong-format">
    <div class="check-data-wrong">
      <div class="header-title">
        <div
          id="img-back"
          class="pointer d-flex align-items-center"
          @click="goBack"
        >
          <img src="@/icons/ico-expand.png" />
        </div>
        <div>
          <span style="font-size: 21px">{{ title }}</span>
        </div>
      </div>
      <div class="tab-header">
        <div class="filter">
          <fieldinput
            id="wrong-format"
            v-if="totalWrong > 0"
            type="input-icon"
            :placeholder="'Tìm kiếm theo tên, mã nhân viên'"
            classicon="icon-search"
            width="402px"
            :valueInput="filterBar.inputSearch"
            @value="filterBar.inputSearch = $event"
          />
          <div class="button-filter">
            <div class="content-btn">
              Tổng số dữ liệu:
              <div class="total-row-red">{{ totalWrong }}</div>
            </div>
          </div>
        </div>
        <div class="label action">
          Tải về danh sách dữ liệu lỗi
          <m-btn
            :type="'btn-primary-icon'"
            :name="'Tải về'"
            :handleClick="downloadErrorExcel"
            style="margin-left: 10px"
          >
            <template v-slot:icon>
              <img class="icon" src="@/icons/Group 16994.png" />
            </template>
          </m-btn>
        </div>
      </div>
    </div>
    <div class="table-data-wrong-format">
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
import Button from "@/components/buttons";
import table from "@/components/tables";
import excelStatus from "@/enums/excelStatus.js";
import SettlementDetailService from "@/api/etax/declaration_settlement_detail.js";
import { filterData } from "@/utils/filter-paginate.js";
export default {
  props: {
    listColumn: {
      type: Array,
      default: null,
    },
    errorKey: {
      type: String,
      default: null,
    },
  },
  components: {
    "m-btn": Button,
    fieldinput,
    tbl: table,
  },
  data() {
    return {
      title: "Danh sách dữ liệu sai định dạng",
      loading: false,
      isEmpty: false,
      tableData: [],
      pageSizes: [25, 50, 75, 100],
      totalWrong: 0,
      allFilterData:[],
      filterBar: {
        pageIndex: 1,
        pageSize: 25,
        inputSearch: "",
      },
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
    async downloadErrorExcel() {
      const url = this.getDownloadUrl("settlementdetail05_2bk_error");
      try {
        var res = await SettlementDetailService.downloadExcelFile(url);
        if (res.data != null) {
          const a = document.createElement("a");
          var blob = new Blob([res.data], {
            type:
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          });
          var urlDownload = window.URL.createObjectURL(blob);
          a.href = urlDownload;
          a.download = excelStatus.FILE_NAME[1].NAME;
          a.click();
        } else {
          this.$_Notification.error(
            excelStatus.RESPONSE_STATUS.FAILED,
            excelStatus.DOWN_FILE_STATUS[0].NAME
          );
        }
      } catch (error) {
        this.$_Notification.error(
          excelStatus.RESPONSE_STATUS.FAILED,
          excelStatus.DOWN_FILE_STATUS[0].NAME
        );
      }
    },
    getDownloadUrl(mode) {
      var options = {
        errorKey: this.errorKey,
      };
      return SettlementDetailService.getURLDownloadExcel(mode, options);
    },

    /**
     * Hàm lấy dữ liệu danh sách dữ liệu sai trên file excel (phân trang) từ store
     * Created By : NQKHAI(3/11/2020)
     * Modified by bvbao(4/11/2020)
     */
    getData() {
      var dataWrong = this.$store.getters.listWrongFormatExcel;
      filterData();
      if (dataWrong != null) {
        let pageSize = this.filterBar.pageSize;
        let pageIndex = this.filterBar.pageIndex;
        let search = this.filterBar.inputSearch;
        //filter theo input search
        this.allFilterData = dataWrong.filter((data) =>
          data.EmployeeCode.toLowerCase().includes(search.toLowerCase()) ||
          data.FullName.toLowerCase().includes(search.toLowerCase()) ||
          data.CitizenIdentityNo.toLowerCase().includes(search.toLowerCase()) ||
          data.TaxNo.toLowerCase().includes(search.toLowerCase())
        );
        this.tableData = this.allFilterData
          .skip((pageIndex - 1) * pageSize)
          .limit(pageSize);
        this.isEmpty = false;
        this.totalWrong = dataWrong.length;
        this.loading = false;
      } else {
        this.isEmpty = true;
        this.loading = false;
        this.tableData = [];
        this.totalWrong = 0;
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
.wrong-format {
  background-color: $background-grey;
  padding-left: 26px;
  padding-right: 26px;
  .check-data-wrong {
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
    .tab-header {
      display: flex;
      height: 43px;
      .filter {
        display: flex;
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
            .total-row-red {
              margin: 0 12px;
              font-size: 16px;
              color: $red-wrong;
              font-weight: bold;
            }
          }
        }
      }
      .action {
        margin-left: auto;
        display: flex;
        align-items: center;
      }
    }
  }
  .table-data-wrong-format .tableData .el-table__body-wrapper{
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
