<template>
    <div class="taxdeduction">
      <el-row class="page-header">
        <el-col :span="24">
          <div class="page-title">
            <p class="heading">Đăng kí người phụ thuộc</p>
          </div>
          <m-btn type="btn-white2-onlyicon-big" tooltip="Làm mới" positionTooltip="left" :handleClick="refreshTable">
            <template v-slot:icon>
              <img class="icon" src="@/icons/AddNew.svg" style="width: 20px;height: 20px;" />
            </template>
          </m-btn>
        </el-col>
      </el-row>
      <el-row class="filter-bar" justify="space-between">
      <el-col :span="20">
        <el-row :gutter="16" v-if="!isCheckRow">
          <el-col :span="6">
            <field-input type="input-icon" classicon="icon-search" :placeholder="'Tìm kiếm'"></field-input>
          </el-col>
          <el-col :span="4">
            <input-dropdown
              selectID="regis-type"
              :list="listRegisterType"
              type="select-border-color"
              label="NAME"
              field="VALUE"
              :placeholder="'Loại đăng kí'"
            ></input-dropdown>
          </el-col>
          <el-col :span="4">
            <input-dropdown
              selectID="status"
              :list="listStatus"
              type="select-border-color"
              label="NAME"
              field="VALUE"
              :placeholder="'Trạng thái'"
            ></input-dropdown>
          </el-col>
          <el-col :span="4">
            <input-dropdown 
            :list="listMonth"
            type="label-none"
            label="MONTH_NAME"
            field="MONTH"
            :size="'medium'" 
            :placeholder="'Năm đăng kí'" 
            ></input-dropdown>
          </el-col>
          <el-col :span="4">
            <date-picker type="month" :size="'medium'" :placeholder="'Tháng đăng kí'" width="100%"></date-picker>
          </el-col>
        </el-row>
        <el-row :gutter="16" v-if="isCheckRow" >
          <div style="float:left;margin-left:8px">
            <m-btn type="btn-white3-onlyicon" :iconel="'el-icon-delete'" tooltip="Xóa" :handleClick="callDeleteMulti" >
              <template v-slot:icon></template>
            </m-btn>
            <m-btn type="btn-white3-onlyicon"  tooltip="Kí nộp" :handleClick="multiSignDeclaration" style="margin-right:16px">
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group 18667@2x.png" />
              </template>
            </m-btn>           
            </div>
        </el-row >
      </el-col>
      <el-col :span="4" v-if="!isCheckRow">
        <m-btn type="btn-primary-icon" name="Thêm mới hồ sơ">
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 17812.png" />
          </template>
        </m-btn>
      </el-col>
    </el-row>
    <el-row class="tax-deduction-table">
      <tbl-tax-deduction
        v-if="!isEmpty && !isLoading"
        ref="tax-deduction"
        class="tax-deduction"
        :listColumn="listColumn"
        :dataTable="dataTable"
        :haveCheck="haveCheck"
        :pageSizes="pageSizes"
        :havePagination="true"
        :total="numberOfRecord"
        :layoutPaging="layoutPaging"
        :chipArray="chipArray"
        :tooltip="true"
        :listaction="listaction"
        typepage="tờ khai"
        @pagination="getFilter"
        @selection="getSelection"
        @object-emit="getObjectEmit"
        @handleSelectionChange="getRowsSelection"
        @handleSelectAll="handleSelectAll"
        @handleRowDBLClick="handleRowDBLClick"
        :loading="false"
      ></tbl-tax-deduction>
    </el-row>
    </div>
</template>

<script>
import Button from "@/components/buttons";
import fieldinput from "@/components/inputs";
import Dropdown from "@/components/select";
import DatePicker from "@/components/date-time-pickers";
import Table from "@/components/tables";
import listMonth from "@/enums/filter";
//import ServiceSign from "@/api/etax/signusbtoken.js";
//import Message from "@/enums/signusbtoken.js";
export default {
  name: "TaxDeduction",
  components: {
    "m-btn": Button,
    "field-input": fieldinput,
    "input-dropdown": Dropdown,
    "date-picker": DatePicker,
   "tbl-tax-deduction": Table,
  },
  data(){
    return {
      listRegisterType:[
        {
          NAME: "Đăng kí thuế",
          VALUE: 1,
        },
        {
          NAME: "Thay đổi thông tin MST",
          VALUE: 2,
        },
      ],
      listStatus:[
         {
          NAME: "Chưa nộp",
          VALUE: 1,
        },
        {
          NAME: "Gửi MISA",
          VALUE: 2,
        },
         {
          NAME: "Tổng cục thuế phê duyệt",
          VALUE: 3,
        },
        {
          NAME: "Tổng cục thuế từ chối",
          VALUE: 4,
        }
      ],
      listColumn:[
         {
          label: "Tên hồ sơ",
          prop: "TaxRegistrationProfileName",
          propertyName: "TaxRegistrationProfileName",
          width: "",
          minWidth: "260",
          align: "left",
          type: "",
          sortable: true,
        },
        {
          label: "Lần đăng ký",
          propertyName: "TaxRegistrationNo",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "TaxRegistrationNo",
          sortable: true,
        },
         {
          label: "Năm đăng ký",
          propertyName: "TaxRegistrationYear",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "TaxRegistrationYear",
          sortable: true,
        },
        {
          label: "Ngày lập",
          propertyName: "CreatedDate",
          width: "160",
          minWidth: "160",
          align: "left",
          type: "date",
          prop: "CreatedDate",
          sortable: true,
        },
        {
          label: "Ngày nộp",
          propertyName: "SubmitDate",
          width: "160",
          minWidth: "160",
          align: "left",
          type: "date",
          prop: "SubmitDate",
          sortable: true,
        },
        {
          label: "Trạng thái",
          propertyName: "TaxRegistrationStatus",
          width: "180",
          minWidth: "180",
          align: "left",
          type: "chip",
          prop: "TaxRegistrationStatus",
          sortable: true,
        },
      ],
      dataTable:[
        {
          TaxRegistrationProfileID: "5535bab0-fae1-4e37-bf57-627a3ede5db1",
          TaxRegistrationProfileName:"Tờ khai 05-DK_TH-TCT",
          TaxRegistrationNo:1,
          TaxRegistrationYear:"2020",
          CreatedDate : "2020-09-03T00:00:00",
          SubmitDate:"2020-09-03T00:00:00",
          TaxRegistrationStatus:1,
        },
          {
          TaxRegistrationProfileID: "5535bab0-fae1-4e37-bf57-627a3ede5db1",
          TaxRegistrationProfileName:"Tờ khai 05-DK_TH-TCT",
          TaxRegistrationNo:1,
          TaxRegistrationYear:"2020",
          CreatedDate : "2020-09-03T00:00:00",
          SubmitDate:"2020-09-03T00:00:00",
          TaxRegistrationStatus:2,
        },
          {
          TaxRegistrationProfileID: "5535bab0-fae1-4e37-bf57-627a3ede5db1",
          TaxRegistrationProfileName:"Tờ khai 05-DK_TH-TCT",
          TaxRegistrationNo:1,
          TaxRegistrationYear:"2020",
          CreatedDate : "2020-09-03T00:00:00",
          SubmitDate:"2020-09-03T00:00:00",
          TaxRegistrationStatus:3,
        },
          {
          TaxRegistrationProfileID: "5535bab0-fae1-4e37-bf57-627a3ede5db1",
          TaxRegistrationProfileName:"Tờ khai 05-DK_TH-TCT",
          TaxRegistrationNo:1,
          TaxRegistrationYear:"2020",
          CreatedDate : "2020-09-03T00:00:00",
          SubmitDate:"2020-09-03T00:00:00",
          TaxRegistrationStatus:4,
        },
      ],
      listaction: [
        {
          value: 0,
          name: "Sửa",
        },
        {
          value: 1,
          name: "Xóa",
        },
      ],
      listMonth:[],
      haveCheck: true,
      pageSizes: [25, 50, 75, 100],
      chipArray: "TAX_REGISTER",
      layoutPaging: "prev, pager, next, sizes",
      isEmpty:false,
      isLoading:false,
      numberOfRecord:5,
      isCheckRow:false,
      listSelected:[],

    }
  },
  watch:{},
   created(){
    this.listMonth = listMonth.LIST_MONTH;
    // this.listTaxType = listTaxType.TAX_TYPE;
  },
  methods:{
    refreshTable() {},
    getFilter() {},
    getSelection() {},
    getObjectEmit() {},
    handleSelectAll(){},
    handleRowDBLClick(){},
    getRowsSelection(row){
      this.listSelected= row
      if(this.listSelected.length >= 1){
        this.isCheckRow = true
      }else{
        this.isCheckRow = false
      }
    }
  },
  

};
</script>

<style lang="scss" scoped>
  .taxdeduction{
    height: 100%;
    .page-title .heading{
      font-family: "GoogleSansBold" ;
    }
    .tax-deduction-table{
      height: calc(100% - 120px);
    }
  }
</style>