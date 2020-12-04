<template>
  <div class="duplicate-on-excel">
    <!-- Phần header của trang  -->
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
      <div class="tab-header">
        <div class="filter">
          <!-- Ô tìm kiếm -->
          <fieldinput
            v-if="totalDuplicate > 0"
            id="duplicate-excel"
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
              <div class="total-row-blue">{{ totalDuplicate }}</div>
            </div>
          </div>
        </div>
        <div class="label action">
          <m-btn
            :type="'btn-primary-icon'"
            :name="'Hợp nhất'"
            style="margin-left: 10px"
            :handleClick="consolidationData"
          >
          </m-btn>
        </div>
      </div>
    </div>
    <div class="table-data-duplicate-excel">
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
import {filterData} from "@/utils/filter-paginate.js";
export default {
  components: {
    "m-btn": Button,
    fieldinput,
    tbl: table,
  },
  data() {
    return {
      title: "Danh sách dữ liệu trùng lặp nhau trên Excel",
      loading: false,
      isEmpty: false,
      allFilterData:[],
      filterBar: {
        pageIndex: 1,
        pageSize: 25,
        inputSearch: "",
      },
      pageSizes: [25, 50, 75, 100],
      tableData:[],
      totalDuplicate:0,
      listColumn: [
        {
          label: "",
          propertyName: "ImportValidState",
          width: "50",
          type: "status-excel",
          align: "center",
        },
        {
          label: "STT trên file Excels",
          propertyName: "SortExcel",
          width: "",
          minWidth: "60",
          type: "",
          alignHeader: "center",
          align: "center",
        },
        {
          label: "Mã nhân viên",
          validate: true,
          prop: "EmployeeCode",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "130",
          type: "",
        },
        {
          label: "Họ và tên",
          propertyName: "FullName",
          width: "",
          validate: true,
          minWidth: "180",
          type: "",
        },
        {
          label: "Bộ phận/Phòng ban",
          propertyName: "OrganizationUnitName",
          width: "",
          validate: true,
          minWidth: "128",
          type: "",
        },
        {
          label: "Mã số thuế",
          propertyName: "TaxNo",
          width: "",
          validate: true,
          minWidth: "128",
          type: "",
        },
        {
          label: "Loại giấy tờ",
          propertyName: "IndentityTypeName",
          width: "",
          validate: true,
          minWidth: "170",
          type: "",
        },
        {
          label: "Số CMND /CCCD /Hộ chiếu",
          propertyName: "CitizenIdentityNo",
          width: "",
          validate: true,
          minWidth: "113",
          type: "",
        },
        {
          label: "Cá nhân cư trú",
          propertyName: "IndividualResidentData",
          width: "",
          validate: true,
          minWidth: "120",
          type: "",
        },
        {
          label: "Tổng thu nhập chịu thuế TNCN",
          propertyName: "TotalIncomeTaxable",
          width: "",
          validate: true,
          minWidth: "190",
          type: "decimal",
          alignHeader: "left",
          align: "right",
        },
      ],
      isMerge: false,
      listAfterMerge:[]
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
     * Hàm lấy dữ liệu danh sách dữ liệu trùng trên file excel (phân trang) từ store
     * Created By : NQKHAI(3/11/2020)
     */
    getData() {
      var dataDuplicate = [];
      if(!this.isMerge){
        dataDuplicate = this.$store.getters.listDuplicateExcel;
        this.totalDuplicate = dataDuplicate.length;
      }
      else{
        dataDuplicate = this.listAfterMerge;
      }
      dataDuplicate = dataDuplicate.sort(function (list, sortList) {
        return list.FullName.localeCompare(sortList.FullName);
      });
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

    /**
     * Hàm xử lý hợp nhất dữ liệu trùng trong file excel
     * Created by bvbao (3/11/2020)
     */
    consolidationData(){
      var dataDuplicate = this.$store.getters.listDuplicateExcel
      this.tableData = [...dataDuplicate];
      dataDuplicate.forEach(element => {
        var remove = true;
        if(element.ImportValidState != 3){
          remove = false;
        }
        element.ImportValidError.forEach(itemDup => {
            if(!itemDup || itemDup.Code != excelStatus.findInArray("DuplicateInfile") || (itemDup.Code == excelStatus.findInArray("DuplicateInfile") && itemDup.PropertyName != "CitizenIdentityNo")){
              remove = false;
            }
        });
        if(remove){
          var index = this.tableData.findIndex(x => x.DeclarationSettlementDetailId == element.DeclarationSettlementDetailId);
          this.tableData.splice(index,1);
        }
      });
      this.allFilterData = this.tableData;
      this.listAfterMerge = this.tableData;
      this.isMerge = true;
    }
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
.duplicate-on-excel {
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
            .total-row-blue {
              margin: 0 12px;
              font-size: 16px;
              color: $blue-duplicate;
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
  .table-data-duplicate-excel .tableData .el-table__body-wrapper{
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
