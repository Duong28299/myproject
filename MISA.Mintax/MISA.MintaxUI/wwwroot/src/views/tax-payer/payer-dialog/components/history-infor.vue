<template>
  <div id="tab-history-info">
    <el-form style="height: 100%">
      <el-row :gutter="40" class="container-content">
        <el-col :span="24" class="" style="">
          <div class="title-bottom-red">
            <div class="main-title">LỊCH SỬ THU NHẬP, THUẾ</div>
            <div class="red-line" />
          </div>
        </el-col>
      </el-row>

      <el-row class="row-table" :gutter="40">
        <el-col :span="24" class="" style="height: 100%">
          <div class="table-history">
            <table-data
              id="table-id"
              :dataTable="tableData"
              :border="true"
              :listColumn="tableConfig"
              :loading="false"
              :havePagination="false"
              :haveAction="false"
              @click-chip="clickToDetail"
            >
            </table-data>
          </div>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>

<script>
import tableData from "@/components/tables";
import employeeService from "@/api/etax/employee.js";
import { MESSAGE } from "@/enums/responseMessage.js";
export default {
  components: {
    "table-data": tableData,
  },
  props: {
    formMode: {
      type: Number,
      default: 0,
    },
    employeeID: {
      type: String,
      default: null,
    },
    resetData:{
      type:Boolean,
      default: false
    }
  },
  data() {
    return {
      tableData: [
      ],
      isEmpty: false,
      totalRecord: 0,
      tableConfig: [
        {
          prop: "PeriodYear",
          label: "Năm",
          propertyName: "PeriodYear",
          align: "left",
          sortable: "true",
          minWidth: "140",
          width: "140",
          type: "",
        },
        {
          prop: "PeriodMonthOrQuarter",
          label: "Tháng/Quý",
          propertyName: "PeriodMonthOrQuarter",
          align: "left",
          sortable: "true",
          minWidth: "160",
          width: "160",
          type: "",
        },
        {
          prop: "TotalIncome",
          label: "Tổng thu nhập",
          propertyName: "TotalIncome",
          align: "right",
          sortable: "true",
          minWidth: "205",
          width: "205",

          type: "decimal",
        },
        {
          prop: "TotalReduction",
          label: "Tổng giảm trừ",
          propertyName: "TotalReduction",
          align: "right",
          sortable: "true",
          minWidth: "205",
          width: "205",

          type: "decimal",
        },
        {
          prop: "TotalIncomeTaxable",
          label: "Thu nhập tính thuế",
          propertyName: "TotalIncomeTaxable",
          align: "right",
          sortable: "true",
          minWidth: "205",
          width: "205",
          type: "decimal",
        },
        {
          prop: "TotalTaxWithHeld",
          label: "Thuế thu nhập cá nhân",
          propertyName: "TotalTaxWithHeld",
          align: "right",
          sortable: "true",
          minWidth: "205",
          width: "205",

          type: "decimal",
        },
        {
          prop: "Chi tiết",
          align: "center",
          type: "btn-chip",
          minWidth: "110",
        },
      ],
      showDetail: false,
      idSelect: "",
      filter:{
        employeeID: null,
        organizationUnitID: "",
        keyword: '',
        periodYear: 0,
        periodMonth: 0,
        pageIndex :1, 
        pageSize: 25,
      },
      loading: false,
      responseData: [],
    };
  },
  watch: {
    employeeID(val) {
      this.filter.employeeID = val;
    },

    filter: {
      async handler(val){
        if(val.employeeID){
        await this.callServiceGetData();
        }
      },  
      deep: true
    },
    /**
     * Reset data khi tắt form
     */
    resetData(val){
      if(val){
        this.resetDataFunc();
      }
    }
  },
  methods: {
    /**
     * hàm clcik để vào xem chi tiết của 1 row
     * created by nvbinh2 24.11.2020
     */
    clickToDetail(row) {
      var obj = {
        info: row,
        open:true,
      }
      //call api
      this.$emit("click-row-history", obj);
    },

    async callServiceGetData(){
        var res = await employeeService.getEmployeeHistoryPaging(this.filter);
        this.callBackGetData(res);
      
    },
    /**
     * Hàm thực thi xử lí khi có dữ liệu truyền về
     */
    callBackGetData(res) {
      if (res.data && res.data.Success) {
        this.responseData = res.data.Data
        this.getDataToTable(this.responseData);

      } else {
        this.$_Popup.warn("Thông báo", MESSAGE["500"], [
          {
            text: "Đóng",
            class: "btn-primary",
            callback: () => {},
          },
        ]);
      }
    },
    /**
     * Chuyển data service trả về vào bảng
     * created by nvbinh2 
     */
    getDataToTable(data){

      // Nhân bản thành dữ liệu đưa vào bảng
      data.ListPaging.forEach(element => {
        var temp = Object.assign({}, element);
        temp.PeriodMonthOrQuarter = `${this.getStringPeriodMonthOrQuarter(element.TemporaryTaxType)} ${element.PeriodMonthOrQuarter}`;
        this.tableData.push(temp)
      });
      // Cập nhật các thông tin phân trang
      this.isEmpty = data.IsEmpty;
      this.totalRecord =data.TotalRecord

    },

    /**
     * Chuyển dạng quý hoặc dạng số sang dạng tên
     */
    getStringPeriodMonthOrQuarter(val){
      if(val ==1) return "Tháng"
      else {
        return "Quý"
      }
    },

    /**
     * Reset dữ liệu
     */
    resetDataFunc(){
      this.resetFilter();
      this.resetCurrentData();
    },

    /**
     * Reset lại bộ lọc
     */
    resetFilter(){
        this.filter.employeeID= null;
        this.filter.organizationUnitID="";
        this.filter.keyword='';
        this.filter.periodYear= 0;
        this.filter.periodMonth= 0;
        this.filter.pageIndex =1;
        this.filter.pageSize= 15;
    },

    /**
     * Reset lại các giá trị dữ liệu
     */
    resetCurrentData(){
      this.tableData = [];
      this.responseData = [];
      this.isEmpty = true;
      this.totalRecord = 0;
    },

  },
  created(){
    if(this.formMode == 1){
      this.filter.employeeID = this.employeeID;
    }
  }
};
</script>

<style lang="scss" scoped>
@import "@/styles/variables";
#tab-history-info {
  .title-bottom-red {
    padding-left: 16px;
    padding-top: 0px;
    height: 38px;
  }
  .table-history {
    width: 100%;
    height: 100%;
    background-color: $background-grey;
    padding: 16px 16px 0px 16px;
  }
  .row-table {
    height: calc(100% - 38px);
  }
  .tab-label-info {
    font-family: $_fontbold;
  }
}
</style>

<style lang="scss">
@import "@/styles/variables";

#table-id {
  th {
    font-family: $_fontmedium !important;
  }
  .el-table__header-wrapper {
    .el-table__header {
      table-layout: fixed !important;
    }
    .cell {
      padding: 16px 10px 16px 20px;
      font-family: $_fontregular !important;
    }
    .caret-wrapper {
      float: right;
      height: 12px;
      margin-left: 3.5px;
      width: 12px;
      .sort-caret.descending {
        bottom: -10px;
      }
      .sort-caret.ascending {
        top: 0px;
      }
      .sort-caret {
        left: 0px;
      }
    }
    // tr th{
    //   border-right: 1px solid $background-grey;
    // border-bottom: 1px solid $background-grey;
    // box-sizing: border-box;
    // }
  }
  .el-table__body-wrapper {
    .cell {
      padding-left: 20px;
      padding-right: 20px;
    }
  }
  .el-table .sort-caret.descending {
    bottom: -10px;
  }
  // .el-table tr td{
  //   box-sizing:border-box;
  //   border-right: 1px solid $background-grey;
  //   border-bottom: 1px solid $background-grey;
  // }
}
</style>



