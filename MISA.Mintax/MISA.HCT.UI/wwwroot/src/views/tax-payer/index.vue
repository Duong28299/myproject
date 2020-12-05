<template>
  <div class="tax-payer">
    <!-- Page title -->
    <el-row class="page-header">
      <el-col :span="24">
        <div class="page-title d-flex">
          <h1 style="height: 40px; line-height: 40px">Hồ sơ người nộp thuế</h1>
          <m-btn
            v-if="!isFirst"
            type="btn-white2-onlyicon-big"
            tooltip="Làm mới"
            :handleClick="refreshTable"
          >
            <template v-slot:icon>
              <img
                class="icon"
                src="@/icons/AddNew.svg"
                style="width: 20px; height: 20px"
              />
            </template>
          </m-btn>
        </div>
      </el-col>
    </el-row>
    <!-- /Page title -->

    <!--khi có dữ liệu-->
    <!--phần filter-->
    <el-row
      class="filter-bar"
      justify="space-between"
      v-if="!isEmpty && !isCheckRow"
    >
      <el-col :span="16">
        <el-row :gutter="16">
          <el-col :span="12">
            <field-input
              type="input-icon"
              classicon="icon-search"
              :placeholder="'Tìm kiếm theo tên nhân viên, mã nhân viên và mã số thuế'"
              @value="getValueProfileName"
              :valueInput="filterBar.keyword"
              :clearable="true"
            ></field-input>
          </el-col>
          <!-- bộ phận/phòng ban -->
          <el-col :span="6">
            <tree-select
              :expand="false"
              responeType="string"
              @value-checked="getOrganizationUnitChecked"
              :value="listOrganizationUnit"
              :options="listOrganizationUnit"
            ></tree-select>
          </el-col>
          <!--tình trạng làm việc-->
          <el-col :span="6">
            <input-dropdown
              selectID="status"
              :list="listStatus"
              type="select-border-color"
              label="name"
              field="value"
              :placeholder="'Trạng thái'"
              @value="getValueStatus"
              :defaultNumber="filterBar.declarationStatus"
              :clearable="true"
            ></input-dropdown>
          </el-col>
        </el-row>
      </el-col>

      <!--btn thêm người nộp thuế-->
      <el-col :span="8">
        <m-btn
          type="multi-button"
          :name="'Thêm người nộp thuế'"
          :handleClick="openAddDialog"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18241@2x.png" />
          </template>
        </m-btn>
      </el-col>
    </el-row>

    <el-row v-if="isCheckRow">
      <!-- xóa khi có selected -->
      <el-col :span="8" class="option-check-row">
        <m-btn
          class="btn-del"
          type="btn-white3-onlyicon"
          tooltip="Xóa"
          :handleClick="callDeleteMulti"
          positionTooltip="top"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18660@2x.png" />
          </template>
        </m-btn>
        <div class="un-select-all" @click="unSelectAllRow">
          <p>Bỏ chọn tất cả</p>
        </div>
      </el-col>
    </el-row>
    <!--phần filter-->

    <!--bảng dữ liệu-->
    <el-row class="data-table">
      <!--phần loading-->
      <div
        v-if="
          (loadingFirst && isFirst) || (loadingFirst && !isFirst && isEmpty)
        "
        v-loading="loadingFirst"
        class="loading-first"
      ></div>
      <div
        v-if="loadingAfter && !isFirst && !isEmpty"
        v-loading="loadingAfter"
        class="loading-after"
      ></div>
      <tbl-tax-payer
        v-if="!isEmpty"
        ref="payerPerson"
        :listColumn="listColumn"
        :dataTable="getDataTable"
        chipArray="EMPLOYEE_STATUS"
        :haveCheck="haveCheck"
        :pageSizes="pageSizes"
        :havePagination="true"
        :total="numberOfRecord"
        :layoutPaging="layoutPaging"
        :listaction="listaction"
        :tooltip="true"
        typepage="tờ khai"
        @pagination="pagination"
        @handleSelectionChange="getRowsSelection"
        @handleSelectAll="handleSelectAll"
        @handleRowDBLClick="handleRowDBLClick"
        @row-action="getAction"
        id="tbl-tax-payer"
        :loading="false"
      ></tbl-tax-payer>
      <!--hiển thị khi không có dữ liệu-->
      <div
        class="main-payer-empty"
        v-if="isEmpty && !loadingFirst && !loadingAfter"
      >
        <img src="@/assets/images/Group 20216.png" />
        <div class="btn-payer-empty">
          <!--btn thêm người nộp thuế-->
          <m-btn
            :class="'btn-payer'"
            type="btn-primary-icon"
            name="Tạo mới người nộp thuế"
            :handleClick="openAddDialog"
          >
            <template v-slot:icon>
              <img class="icon" src="@/icons/Group 20219.png" />
            </template>
          </m-btn>

          <!--btn nhập khẩu từ Excels-->
          <m-btn
            :class="'btn-payer'"
            type="btn-white2-icon"
            name="Nhập khẩu từ Excels"
            :handleClick="openAddDialog"
          >
            <template v-slot:icon>
              <img class="icon" src="@/icons/Group 20229.png" />
            </template>
          </m-btn>

          <!--btn đồng bộ từ Amis HRM-->
          <m-btn
            :class="'btn-payer'"
            type="btn-white2-icon"
            name="Đồng bộ từ AMIS HRM"
            :handleClick="openAddDialog"
          >
            <template v-slot:icon>
              <img class="icon" src="@/icons/Group 20233.png" />
            </template>
          </m-btn>
        </div>
      </div>
      <!--hiển thị khi không có dữ liệu-->
    </el-row>
    <!--bảng dữ liệu-->
    <!--khi có dữ liệu-->

    <!--dialog-->
    <!-- :payerDetail="payerDetail" -->
    <add-tax-payer
      :visiableDialog="isShowAddDialog"
      @close-dialog="isShowAddDialog = $event"
      :formMode="formMode"
      :employeeID="selectEmployeeID"
    >
    </add-tax-payer>
    <!--dialog-->
  </div>
</template>

<script>
import Button from "@/components/buttons";
import fieldinput from "@/components/inputs";
import Dropdown from "@/components/select";
import treeSelect from "@/components/tree-dropdown";
import Table from "@/components/tables";
import OrganizationUnitService from "@/api/etax/organization-unit.js";
import Dialog from "./payer-dialog";
import EmployeeService from "@/api/etax/employee.js";
import Cookies from "js-cookie";
import TaxPayer from "@/api/etax/tax-payer.js";
import ActionTable from "@/enums/actionTable.js";
export default {
  name: "TaxPayer",
  components: {
    "m-btn": Button,
    "field-input": fieldinput,
    "input-dropdown": Dropdown,
    "tree-select": treeSelect,
    "tbl-tax-payer": Table,
    "add-tax-payer": Dialog,
  },
  data() {
    return {
      numberOfRecord: 0, //số bản ghi
      dataTable: [], //dữ liệu của bảng
      isEmpty: true,
      isDialogOpen: false,
      isShowAddDialog: false, //bien dong/mo form,
      haveCheck: true,
      pageSizes: [25, 50, 75, 100],
      layoutPaging: "prev, pager, next, sizes",
      chipArray: "TAX_PAYER",
      listaction: [],
      payerDetail: {},
      isDialogDetailOpen: false,
      loadingFirst: true,
      loadingAfter: true,
      isFirst: true,
      selectEmployeeID: null,
      selectedItem: [],
      isCheckRow: false,
      arrRowSelect: [],
      formMode: 0, // 0: thêm mới - 1: sửa/xem
      listIdDelete: [],
      filterBar: {
        keyword: "", //tên hồ sơ
        EmployeeStatus: null, //trạng thái
        pageIndex: 1,
        pageSize: 25,
        OrganizationUnitID: null, //bộ phận phòng ban
      },
      listOrganizationUnit: [
      ],
      arraySetting: {
        value: "OrganizationUnitID",
        label: "OrganizationUnitName",
        children: "OrganizationUnitsChildren",
      },
      listStatus: [
        {
          name: "Đã nghỉ",
          value: "0",
        },
        {
          name: "Đang làm việc",
          value: "1",
        },
      ],
      listColumn: [
        {
          label: "Mã nhân viên",
          prop: "EmployeeCode",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "100",
          align: "left",
          type: "",
          sortable: true,
        },
        {
          label: "Họ và tên",
          propertyName: "FullName",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "FullName",
          sortable: true,
        },
        {
          label: "Vị trí/Chức vụ",
          propertyName: "PositionName",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "PositionName",
          sortable: true,
        },
        {
          label: "Bộ phận/Phòng ban",
          propertyName: "OrganizationUnitName",
          width: "",
          minWidth: "160",
          align: "left",
          type: "",
          prop: "OrganizationUnitName",
          sortable: true,
        },
        {
          label: "Số CMND/CCCD/Hộ chiếu",
          propertyName: "CitizenIdentityNo",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "CitizenIdentityNo",
          sortable: true,
        },
        {
          label: "MST cá nhân",
          propertyName: "TaxNo",
          width: "150",
          minWidth: "150",
          align: "left",
          type: "",
          prop: "TaxNo",
          sortable: true,
        },
        {
          label: "Trạng thái làm việc",
          propertyName: "EmployeeStatus",
          width: "160",
          minWidth: "160",
          align: "left",
          type: "chip",
          prop: "EmployeeStatus",
          sortable: true,
        },
      ],
    };
  },
  methods: {
    /**
     * Hàm refresh lại table 
     * created by nvnguyen 17/11/2020
     */
    async refreshTable() {
      await this.getData();
    },

    /**
     * Hàm mở dialog 
     * created by nvnguyen 17/11/2020
     */
    openAddDialog() {
      this.formMode = 0;
      this.payerDetail = {};
      this.isShowAddDialog = true;
    },

    /**
     * Lấy Bộ phận phòng ban 
     * created by nvnguyen 17/11/2020
     */
    async getOrganizationUnit(style, type) {
      this.listOrganizationUnit = [];
      var res = await OrganizationUnitService.getOrganizationUnit(style, type);
      if (typeof res.data != "undefined" && res.data.Success) {
        this.listOrganizationUnit.push(res.data.Data);
      } else this.listOrganizationUnit = [];
    },

    /**
     * Lấy danh sách trạng thái 
     * created by nvnguyen 17/11/2020
     */
    getValueStatus(val) {
      this.filterBar.EmployeeStatus = val;
    },

    /**
     * phân trang 
     * created by nvnguyen 17/11/2020
     */
    pagination(val) {
      this.filterBar.pageSize = val.limit;
      this.filterBar.pageIndex = val.page;
    },

    /**
     * hàm lấy load dữ liệu theo giá trị filter
     * created by nvnguyen 27/11/2020
     */
    async getEployeeFilter() {
      this.loadingFirst = true;
      this.loadingAfter = true;
      var res = await EmployeeService.getEmployeePaging(this.filterBar);
      this.dataTable = [];
      if (res.data && res.data.Success) {
        this.isEmpty = res.data.Data.IsEmpty;
        this.mapdata(res.data.Data.ListPaging);
        this.numberOfRecord = res.data.Data.TotalRecord;
      } else {
        this.isEmpty = true;
      }
      this.loadingFirst = false;
      this.loadingAfter = false;
      this.isFirst = false;
    },

    /**
     * Lấy input tim kiếm được nhập vào để filter
     * created by nvnguyen 27/11/2020
     */
    getValueProfileName(val) {
      this.filterBar.keyword = val;
    },

    /**
     * filter theo bộ phận phòng ban
     * created by nvnguyen 27/11/2020
     */
    getPostiionSelected(event) {
      alert(event);
    },

    /**
     * Lấy row được chọn 
     * modified by nvnguyen 19/11/2020
     */
    getRowsSelection(rows) {
      if (rows.length > 0) {
        var listId = rows.map(function (item) {
          return item.EmployeeID;
        });
        this.listIdDelete = [...listId];
        this.arrRowSelect = rows;
        this.isCheckRow = true;
      } else {
        this.isCheckRow = false;
      }
    },

    /**
     * hàm lấy thông tin ngươi nộp thuế
     * craeted by nvnguyen 26/11/2020
     */
    async getTaxPayerInfor(obj) {
      return await TaxPayer.getTaxPayerInfor(obj);
    },

    /**
     * bắt sự kiện chọn tất cả 
     * created by nvnguyen 19/11/2020
     */
    handleSelectAll() {},

    /**
     * bắt sự kiện double click vào dòng
     * created by nvnguyen 19/11/2020
     */
    async handleRowDBLClick(val) {
      this.selectEmployeeID = val.EmployeeID;
      this.isShowAddDialog = true;
      this.formMode = 1; // set trạng thái là sửa cho form
    },

    /**
     * thực hiện theo thao tác được chọn(xóa, sửa) 
     * created by nvnguyen 19/11/2020
     */
    getAction(val) {
      if (val.value == 1) {
        this.listIdDelete = [];
        this.listIdDelete.push(val.row.DeclarationDetailID);
        this.select = [];
        this.select.push(val.row);
        this.openAddDialog();
      } else {
        this.dependentDetail = val.row;
        this.isDialogDetailOpen = true;
      }
    },

    /**
     * gọi service lấy dữ liệu người nộp thuế đổ vào bảng
     * created by nvnguyen 19/11/2020
     */
    async getData() {
      this.filterBar.organizationID = Cookies.get("OrganizationID");
      this.getEployeeFilter();
    },

    /**
     * map dữ liệu vào table
     * @param val mảng dữ liệu lấy từ service
     * createdby nvnguyen 20.11.2020
     */
    mapdata(val) {
      var datas = [];
      val.forEach((item) => {
        var data = {};
        data.EmployeeID = item["EmployeeID"];
        data.EmployeeCode = item["EmployeeCode"];
        data.FullName = item["FullName"];
        data.PositionName = item["PositionName"];
        data.OrganizationUnitID = item["OrganizationUnitID"];
        data.OrganizationUnitName = item["OrganizationUnitName"];
        data.CitizenIdentityNo = item["CitizenIdentityNo"];
        data.TaxNo = item["TaxNo"];
        data.PositionID = item["PositionID"];

        if (item["EmployeeStatus"] == 1) {
          data.EmployeeStatus = item["EmployeeStatus"];
        } else {
          data.EmployeeStatus = item["EmployeeStatus"];
        }
        datas.push(data);
      });
      this.dataTable = datas;
    },

    /**
     * hàm xóa các hồ sơ được chọn
     * created by nvnguyen 27/11/2020
     */
    async callDeleteMulti() {
      var buttons = [
        {
          text: "Không",
          class: "btn-grey none-border",
          callback: () => {},
        },
        {
          text: "Có",
          class: "btn-delete",
          callback: () => {
            this.deleteTaxPayerByID(this.listIdDelete);
          },
        },
      ];
      var message =
        "Các hồ sơ được chọn sẽ bị xóa khỏi hệ thống. Bạn có chắc chắn muốn xóa không?";
      this.$_Popup.delete("Xác nhận", message, buttons);
    },

    /**
     * hàm gọi service xóa hồ sơ theo ID
     * created by nvnguyen 27/11/2020
     */
    async deleteTaxPayerByID(lstID) {
      try {
        var res = await EmployeeService.deleteTaxPayerByID(lstID);
        console.log("thuc hien ham xoa");
        console.log(res);
        let message = "";
        if (res.data.Success) {
          this.unSelectAllRow();
          this.refreshTable();
          let result = {}; //object luu thông tin để hiển thị lên popup
          result.total = lstID.length;
          result.canDelete = [];
          (result.canNotDelete = []),
            (result.notFound = []),
            //lọc các hồ sơ xóa được và không xóa được theo response trả về
            res.data.Data.forEach((item) => {
              if (item == 1) {
                result.canDelete.push(item);
              } else if (item == 0) {
                result.notFound.push(item);
              } else {
                result.canNotDelete.push(item);
              }
            });
          if (result.total > 1) {
            message +=
              "Đã xóa thành công " +
              result.canDelete.length +
              "/" +
              result.total +
              " hồ sơ." +
              "Xóa thất bại " +
              result.canNotDelete.length +
              "/" +
              result.total +
              " hồ sơ.";
          } else {
            message += "Xóa hồ sơ thành công";
          }
          this.popUpSuccess(message);
        } else {
          this.unSelectAllRow();
          this.refreshTable();
          this.popUpFalse("Có lỗi xảy ra, vui lòng thử lại sau");
        }
      } catch (ex) {
        this.unSelectAllRow();
        this.refreshTable();
        this.popUpFalse("Có lỗi xảy ra, vui lòng thử lại sau");
      }
    },

    /**
     * Hàm hiển thị popup thành công
     * @param message nội dung popup
     * created by ndhuy 15.09.2020
     */
    popUpSuccess(message) {
      this.$_Notification.success("Thông báo", message);
    },

    /**
     * Hàm hiển thị popup thất bại
     * created by ndhuy 15.09.2020
     * @param message nội dung popup
     */
    popUpFalse(message) {
      this.buttonDialog = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: this.getClose,
        },
      ];
      this.$_Popup.warn("Thông báo", message, this.buttonDialog);
    },

    /** bỏ chọn tất cả các dòng đang được tích chọn trên table
     * created by nvnguyen 27/11/2020
     */
    unSelectAllRow() {
      this.$refs.payerPerson.clearSelection();
    },
    /**
     * Lấy danh sach don vi dax chon
     * created by nvbinh2 28.11.2020
     */
    getOrganizationUnitChecked(val){
      this.filterBar.OrganizationUnitID = val;
    }
  },

  async created() {
    await this.getOrganizationUnit("tree", 0);
    await this.getData();
    this.listaction = ActionTable.TAX_PAYER;
  },

  computed: {
    /**
     * Lấy data đổ vào bảng
     * created by nvnguyen 20/11/2020
     */
    getDataTable() {
      return this.dataTable;
    },
  },
  
  watch: {
    filterBar: {
      handler() {
        this.getEployeeFilter();
      },
      deep: true,
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/styles/variables.scss";
.loading-tax-payer {
  position: relative;
  top: 40%;
}

.option-check-row {
  display: flex;
  padding-bottom: 13px;
  .un-select-all {
    padding-left: 16px;
    color: $hover-primary;
    cursor: pointer;
  }
}
</style>>

<style lang="scss">
@import "@/styles/main-page.scss";
.tax-payer {
  @include main-page;

  .data-table {
    position: relative;

    .loading-first.el-loading-parent--relative {
      height: 100%;

      .el-loading-mask {
        opacity: 1;
      }
    }

    .loading-after.el-loading-parent--relative {
      position: absolute !important;
      top: 52px;
      left: 0;
      right: 0;
      bottom: 60px;
      height: unset;

      .el-loading-mask {
        background-color: $white !important;
        opacity: 0.8;
      }
    }

    .main-payer-empty {
      position: absolute;
      top: 45%;
      left: 50%;
      display: flex;
      align-items: center;
      justify-content: center;
      transform: translate(-50%, -50%);

      .btn-payer-empty {
        display: flex;
        flex-direction: column;
        margin-left: 24px;

        .btn-payer {
          margin: 8px 0 8px 0;
          width: 210px;
          height: 40px;
          font-family: $_fontregular;

          > .btn-white2-icon {
            width: 100%;
            text-align: left;
            color: $hover-primary !important;
          }

          > .btn-primary-icon {
            width: 100%;
            text-align: left;
          }
        }
      }
    }
  }
}

#tax-payer-dialog {
  margin: 25px 24px 0px 24px !important;

  .dialogel {
    .el-dialog__header {
      padding: 0px 24px;
      height: 68px;
      justify-items: center;
    }

    .el-dialog__body {
      height: calc(100% - 128px);
      padding: 0px 0px;
    }

    .el-dialog__footer {
      position: absolute;
      width: 100%;
      bottom: 0 !important;
      left: 0 !important;
      height: 60px;
    }

    .main-content {
      padding: 24px 24px 0px 24px;
      height: calc(100% - 48px);
    }

    .label-infor {
      color: $primary;
    }
  }
}

.col-class {
  .el-form-item > .el-form-item__label {
    line-height: 45px !important;
    height: 36px !important;
    font-size: 13px;
  }
}

@media only screen and (min-width: 1366px) {
  #tax-payer-dialog .dialogel {
    width: 1320px;
    height: 740px;
  }
}

#tbl-tax-payer {
  .el-tag {
    width: 120px !important;
  }
}
</style>