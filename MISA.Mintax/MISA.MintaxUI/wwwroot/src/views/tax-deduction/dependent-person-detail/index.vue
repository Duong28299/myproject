<template>
  <div class="depentdent-person-detail">
    <!-- PHẦN titile -->
    <PageTitle :haveData="true">
      <el-row class="button-sign">
        <m-btn
          class="mr-8 fl"
          :type="'btn-white-icon'"
          :name="'Xem tờ khai'"
          style="margin-right: 16px;"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18561.png" />
          </template>
        </m-btn>
        <m-btn
          class="mr-8 fl"
          :type="'btn-primary-icon'"
          :name="'Ký nộp qua mTax'"
          style="margin-right: 16px;"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18581.png" />
          </template>
        </m-btn>
      </el-row>
    </PageTitle>

    <!-- Lọc /tìm kiểm trên bảng -->
    <el-row class="filter-bar" justify="space-between">
      <el-col :span="24">
        <el-row :gutter="16">
          <el-col :span="8">
            <field-input type="input-icon" classicon="icon-search" :placeholder="'Tìm kiếm'"></field-input>
          </el-col>
          <el-col :span="4">
            <input-dropdown
              selectID="position"
              :list="listDepartment"
              class="department"
              type="select-border-color"
              label="DEPARTMENT_NAME"
              field="DEPARTMENT"
              :size="'medium'"
              :placeholder="'Bộ phận/Phòng ban'"
            ></input-dropdown>
          </el-col>
          <el-col :span="4" :offset="8">
            <m-btn
              :type="'multi-button'"
              :name="'Thêm người nộp thuế'"
              :dataDropdown="dataDropdown"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/ico-AddUser2.png" />
              </template>
            </m-btn>
          </el-col>
        </el-row>
      </el-col>
    </el-row>

    <!-- Bảng danh sách hồ sơ người phụ thuộc -->
    <el-row class="dependent-person-table">
      <tbl-dependent-person
        ref="dependent-person"
        class="dependent-person"
        :listColumn="listColumn"
        :dataTable="dataTable"
        :haveCheck="haveCheck"
        :pageSizes="pageSizes"
        :havePagination="true"
        :total="numberOfRecord"
        :layoutPaging="layoutPaging"
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
      ></tbl-dependent-person>
    </el-row>
  </div>
</template>

<script>
import Button from "@/components/buttons";
import PageTitle from "@/layout/main/components/page-title";
import fieldinput from "@/components/inputs";
import Dropdown from "@/components/select";
import Table from "@/components/tables";
export default {
  name: "Dependent-person",
  components: {
    "m-btn": Button,
    "field-input": fieldinput,
    "input-dropdown": Dropdown,
    "tbl-dependent-person": Table,
    PageTitle,
  },
  data() {
    return {
      listDepartment: [
        {
          DEPARTMENT_NAME: "Tổng công ty",
          DEPARTMENT: 1,
        },
      ],
      dataDropdown: [
        {
          Icon: "",
          Name: "Thêm mới người nộp thuế",
          nameVal: "",
        },
        {
          Icon: "",
          Name: "Chỉnh sửa thông tin",
          nameVal: "",
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
      listColumn: [
        {
          label: "Mã nhân viên",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "EmployeeCode",
          sortable: true,
        },
        {
          label: "Họ và tên",
          propertyName: "EmployeeName",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "EmployeeName",
          sortable: true,
        },
        {
          label: "Bộ phận/phòng ban",
          propertyName: "Department",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "Department",
          sortable: true,
        },
        {
          label: "Mã số thuế",
          propertyName: "TaxCode",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "TaxCode",
          sortable: true,
        },
        {
          label: "Số người phụ thuộc báo tăng",
          propertyName: "DependentPersonInceased",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "DependentPersonInceased",
          sortable: true,
        },
        {
          label: "Số người phụ thuộc báo tăng",
          propertyName: "DependentPersonDeceased",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "DependentPersonDeceased",
          sortable: true,
        },
      ],
      dataTable: [
        {
          EmployeeCode: 17021085,
          EmployeeName: "Mai Anh Tuấn",
          Department: "Tổng Công Ty",
          TaxCode: "001099013265",
          DependentPersonInceased: 1,
          DependentPersonDeceased: 2,
        },
        {
          EmployeeCode: 17021085,
          EmployeeName: "Mai Anh Tuấn",
          Department: "Tổng Công Ty",
          TaxCode: "001099013265",
          DependentPersonInceased: 1,
          DependentPersonDeceased: 2,
        },
        {
          EmployeeCode: 17021085,
          EmployeeName: "Mai Anh Tuấn",
          Department: "Tổng Công Ty",
          TaxCode: "001099013265",
          DependentPersonInceased: 1,
          DependentPersonDeceased: 2,
        },
        {
          EmployeeCode: 17021085,
          EmployeeName: "Mai Anh Tuấn",
          Department: "Tổng Công Ty",
          TaxCode: "001099013265",
          DependentPersonInceased: 1,
          DependentPersonDeceased: 2,
        },
        {
          EmployeeCode: 17021085,
          EmployeeName: "Mai Anh Tuấn",
          Department: "Tổng Công Ty",
          TaxCode: "001099013265",
          DependentPersonInceased: 1,
          DependentPersonDeceased: 2,
        },
        {
          EmployeeCode: 17021085,
          EmployeeName: "Mai Anh Tuấn",
          Department: "Tổng Công Ty",
          TaxCode: "001099013265",
          DependentPersonInceased: 1,
          DependentPersonDeceased: 2,
        },
      ],
      pageSizes: [25, 50, 75, 100],
      layoutPaging: "prev, pager, next, sizes",
      numberOfRecord: 5,
      haveCheck: true,
      title: "Hồ sơ đăng ký người phụ thuộc - Kỳ kê khai 2020",
    };
  },
  computed: {},
  watch: {},

  async created() {
    sessionStorage.setItem("objectName", this.title);
  },
  methods: {
    refreshTable() {},
    getFilter() {},
    getSelection() {},
    getObjectEmit() {},
    handleSelectAll() {},
    handleRowDBLClick() {},
    getRowsSelection(row) {
      this.listSelected = row;
      if (this.listSelected.length >= 1) {
        this.isCheckRow = true;
      } else {
        this.isCheckRow = false;
      }
    },
  },
};
</script>
    
<style lang="scss" scoped>
.depentdent-person-detail {
  height: 100%;
  .filter-bar {
    margin-top: 24px;
    margin-left: 10px;
    margin-right: 10px;
  }

  .dependent-person-table {
    margin-top: 16px;
    height: calc(100% - 148px);
    margin-left: 8px;
    margin-right: 8px;
  }
  
  .button-sign {
    display: flex;
    position: absolute;
    right: 10px;
  }
    
}

div.page-title.d-flex.el-row {
  align-items: center;
}
</style>