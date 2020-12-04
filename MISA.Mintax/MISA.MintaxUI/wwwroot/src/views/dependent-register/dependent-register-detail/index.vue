<template>
  <div class="depentdent-person-detail">
    <!-- Thay thế title để tránh việc sử dụng seesion storage
      created by lttuan 091020
     -->
    <!-- Phần page header -->
    <el-row class="page-header">
      <el-col :span="16" class="left">
        <span id="img-back" @click="goBack">
          <img src="@/icons/ico-expand.png" />
        </span>
        <span class="page-title">
          <p class="heading">{{ this.Declaration.title }}</p>
        </span>
      </el-col>
      <el-col :span="8" v-if="!isEmpty && !loading" class="right">
        <m-btn
          type="btn-white-icon"
          name="Xem tờ khai"
          :handleClick="openViewDeclaration"
          style="padding-left: 8px"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18561@2x.png" />
          </template>
        </m-btn>
        <m-btn
          class="mr-8"
          :type="'btn-primary-icon'"
          :name="'Ký nộp qua mTax'"
          v-if="isSign"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18580@2x.png" />
          </template>
        </m-btn>
      </el-col>
    </el-row>
    <!--Hết Phần page header -->
    <div class="content" v-if="!isEmpty">
      <!-- Lọc /tìm kiểm trên bảng -->
      <el-row class="filter-bar" justify="space-between" v-if="!isCheckRow">
        <el-col :span="24">
          <el-row :gutter="16">
            <el-col :span="8">
              <field-input
                type="input-icon"
                classicon="icon-search"
                @value="filterBar.keyword = $event"
                :placeholder="'Tìm kiếm theo họ và tên, mã nhân viên'"
                :valueInput="filterBar.keyword"
              ></field-input>
            </el-col>
            <el-col :span="4">
              <tree-select
              :expand="false"
              responeType="string"
              @value-checked="filterBar.organizationUnit =$event"
              :options="organizationUnit"
            ></tree-select>
            </el-col>
            <el-col
              :span="5"
              :offset="7"
              class="btn-add-taxpayer"
              v-if="this.Declaration.declarationStatus == 1"
            >
              <m-btn
                :type="'multi-button'"
                :name="'Thêm người đăng ký'"
                :dataDropdown="dataDropdown"
                :handleClick="openAddDialog"
                class="bg-white"
              >
                <template v-slot:icon>
                  <img class="icon" src="@/icons/ico-AddUser2.png" />
                </template>
              </m-btn>
            </el-col>
          </el-row>
        </el-col>
      </el-row>
      <el-row class="del-bar" v-else>
        <el-col :span="24">
          <m-btn
            type="btn-white3-onlyicon"
            :iconel="'el-icon-delete'"
            tooltip="Xóa người nộp thuế"
            :handleClick="openPopup"
          >
            <template v-slot:icon></template>
          </m-btn>
          <span class="del-title" @click="clearSelection"
            >Bỏ chọn {{ select.length }} nhân viên</span
          >
        </el-col>
      </el-row>
      <!-- Bảng danh sách hồ sơ người phụ thuộc -->

      <el-row class="dependent-person-table" v-if="!isEmpty">
        <div v-if="loading" v-loading="loading"></div>
        <tbl-dependent-person
          ref="dependent-person-detail"
          :listColumn="listColumn"
          :dataTable="getDataTable"
          :haveCheck="haveCheck"
          :pageSizes="pageSizes"
          :havePagination="true"
          :total="numberOfRecord"
          :layoutPaging="layoutPaging"
          :tooltip="true"
          :listaction="listaction"
          :isStatus="false"
          typepage="tờ khai"
          @pagination="pagination"
          @selection="getSelection"
          @object-emit="getObjectEmit"
          @handleSelectionChange="getRowsSelection"
          @handleSelectAll="handleSelectAll"
          @handleRowDBLClick="handleRowDBLClick"
          @row-action="getAction"
          :loading="false"
        ></tbl-dependent-person>
      </el-row>
    </div>

    <div
      class="temporary-mode-btn"
      v-if="
        numberOfRecord == 0 &&
        dataTable.length == 0 &&
        isEmpty == false &&
        !loading
      "
    >
      <img
        src="@/assets/images/Group 18279.png"
        alt="not have data"
        style="position: relative"
      />
      <span>Không tìm thấy kết quả phù hợp</span>
    </div>

    <!-- IF Data Table rỗng -->
    <div
      class="empty-content"
      v-if="isEmpty && Declaration.declarationStatus == 1"
    >
      <img src="@/assets/images/Group 18279.png" />
      <p>Hiện chưa có người đăng ký người phụ thuộc. Vui lòng thêm mới!</p>
      <m-btn
        type="multi-button"
        :dataDropdown="[
          {
            name: 'Thêm người đăng ký',
            nameVal: 'add1',
          },
        ]"
        name="Thêm người đăng ký"
        :handleClick="openAddDialog"
      >
        <template v-slot:icon>
          <img class="icon" src="@/icons/ico-AddUser.png" />
        </template>
      </m-btn>
    </div>
    <!-- /IF Data Table rỗng -->

    <!-- Dialog Add -->
    <add-dialog
      :visiableDL="isDialogOpen"
      @closeDialog="isDialogOpen = $event"
      :declarationID="filterBar.declarationID"
      @addSuccess="addSuccess = $event"
    />
    <!-- /Dialog Add -->
    <!-- Dialog chi tiet người phụ thuộc -->

    <dependent-detail-dialog
      :visiableDL="isDialogDetailOpen"
      :dependentDetail="dependentDetail"
      @closeDialog="isDialogDetailOpen = $event"
      @success="addDependentSuccess = $event"
    >
    </dependent-detail-dialog>

    <!-- Dialog chi tiet người phụ thuộc -->
    <view-declaration
      :viewDeclarationFlag="viewDeclarationFlag"
      :declarationID="filterBar.declarationID"
      @view-get-closing="closeViewDeclaration"
      :isSign="isSign"
      :isExpired="isExpired"
    ></view-declaration>
  </div>
</template>
<script>
import addDialog from "./components/add-employee-dialog";
import Button from "@/components/buttons";
import fieldinput from "@/components/inputs";
import organization from "@/api/etax/organization-unit.js";
import Table from "@/components/tables";
import actionTable from "@/enums/actionTable.js";
import declaration from "@/api/etax/declaration.js";
import DependentDetailDL from "@/views/dependent-register/dependent-register-detail/components/dependent-detail-dialog";
import viewDeclaration from "./components/view-declaration";
import trees from "@/components/tree-dropdown";
import ServiceSign from "@/api/etax/signusbtoken.js";
import declarationService from "@/api/etax/declaration.js";

export default {
  name: "Dependent-person",
  components: {
    "m-btn": Button,
    "field-input": fieldinput,
    "tbl-dependent-person": Table,
    "dependent-detail-dialog": DependentDetailDL,
    addDialog,
    "tree-select": trees,
    "view-declaration": viewDeclaration,
  },
  data() {
    return {
      TaxCode: "0101243150",
      isSign: false,
      isExpired: false,
      loading: true,
      select: [], //mảng
      listIdDelete: [],
      addSuccess: false,
      isDialogDetailOpen: false,
      addDependentSuccess: false,
      isCheckRow: false,
      isEmpty: false,
      filterBar: {
        keyword: "", // tìm kiếm theo từ khóa
        organizationUnit: "", //lọc theo bộ phận phòng ban
        declarationID: "",
        pageIndex: 1,
        pageSize: 25,
      },
      organizationUnit: [],
      arraySetting: {
        value: "OrganizationUnitID",
        label: "OrganizationUnitName",
        children: "OrganizationUnitsChildren",
      },
      dataDropdown: [
        { icon: "", name: "Nhập khẩu" },
        { icon: "", name: "Đồng bộ từ amis" },
      ],
      listaction: [],
      listColumn: [
        {
          label: "Mã nhân viên",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "EmployeeCode",
          sortable: true,
        },
        {
          label: "Họ và tên",
          propertyName: "FullName",
          width: "",
          minWidth: "160",
          align: "left",
          type: "",
          prop: "FullName",
          sortable: true,
        },
        {
          label: "Bộ phận/phòng ban",
          propertyName: "OrganizationUnitName",
          width: "",
          minWidth: "170",
          align: "left",
          type: "",
          prop: "OrganizationUnitName",
          sortable: true,
        },
        {
          label: "Mã số thuế",
          propertyName: "TaxNo",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "TaxNo",
          sortable: true,
        },
        {
          label: "Số người phụ thuộc báo tăng",
          propertyName: "NumberDependentIncrease",
          width: "",
          minWidth: "140",
          align: "right",
          type: "",
          prop: "NumberDependentIncrease",
          sortable: true,
        },
        {
          label: "Số người phụ thuộc báo giảm",
          propertyName: "NumberDependentReduced",
          width: "",
          minWidth: "140",
          align: "right",
          type: "",
          prop: "NumberDependentReduced",
          sortable: true,
        },
      ],
      dataTable: [],
      pageSizes: [25, 50, 75, 100],
      layoutPaging: "prev, pager, next, sizes",
      numberOfRecord: 0,
      haveCheck: true,
      isDBClickOnRow: false,
      isDialogOpen: false,
      dependentDetail: {},
      // mảng lưu trữ thông tin tờ khai
      Declaration: {
        title: "",
        declarationStatus: 1, // chưa nộp
      },
      viewDeclarationFlag: false,
    };
  },
  computed: {
    getDataTable() {
      return this.dataTable;
    },
  },
  watch: {
    filterBar: {
      handler() {
        this.getData();
      },
      deep: true,
    },
    addSuccess: function (val) {
      if (val) this.getData();
    },
    addDependentSuccess: function (val) {
      if (val) this.getData();
    },
  },
  async created() {
    this.CheckSignMtax();
    /**
     * Sửa lỗi 5336 enail nút xóa sửa sau khi hồ sơ đã nộp
     * created by lttuan 09 10 2020
     */
    var res = await declarationService.getDeclarationID(this.$route.params.id);

    this.Declaration.title = this.convertTitle(res.data.Data.DeclarationName);
    this.Declaration.declarationStatus = res.data.Data.DeclarationStatus;

    this.listaction = actionTable.EMPLOYEE_REGISTER;
    this.filterBar.declarationID = this.$route.params.id;
    this.getOrganizationUnitValue();
    this.getData();
    /**
     * Sửa lỗi 5338 thêm người đăng ký trên tờ khác trang thái khác chưa gửi
     * created by lttuan 09 10 2020
     */
    if (this.Declaration.declarationStatus != 1)
      this.listaction.forEach((element) => {
        element.class = "disabled";
      });
  },
  methods: {
    /**
     * Gọi serve check license Mtax
     * Created by NVHUNG 8/10/2020
     */
    async CheckSignMtax() {
      var res = await ServiceSign.licenseChecker(this.TaxCode);
      if (res.data.Data.IsConectedMtax == true) {
        this.isSign = true;
        if (res.data.Data.IsNotExpired == false) {
          this.isExpired = true;
        }
      } else {
        this.isSign = false;
      }
    },
    /**
     * Gọi service lấy thông tin bộ phận phòng ban
     * Created by NQTrung 5/10/2020
     */
    async getOrganizationUnitValue() {
      var res = await organization.getOrganizationUnit("tree", 0);
      if (!res.data.Success || !res.data.MISACode == 200 || !res.data.Data) {
        this.organizationUnit = [];
      } else {
        this.organizationUnit.push(res.data.Data);
      }
    },
    openAddDialog() {
      this.isDialogOpen = true;
    },
    getAction(val) {
      if (val.value == 1) {
        this.listIdDelete = [];
        this.listIdDelete.push(val.row.DeclarationDetailID);
        this.select = [];
        this.select.push(val.row);
        this.openPopup();
      } else {
        this.dependentDetail = val.row;
        this.isDialogDetailOpen = true;
      }
    },
    /**
     * Bỏ chọn tất cả các dòng trong bảng
     * createdby tcduong 16.9.2020
     */
    clearSelection() {
      this.$refs["dependent-person-detail"].clearSelection();
    },
    getOrganizationUnitName(val) {
      this.filterBar.position = val;
    },
    async openPopup() {
      if (this.Declaration.declarationStatus != 1) {
        this.$_Popup.warn(
          "Cảnh báo",
          `Không được xóa người nộp thuế trên ${this.Declaration.title}. Vui lòng kiểm tra lại`,
          [
            {
              text: "Đóng",
              class: "btn-primary",
              callback: () => {},
            },
          ]
        );
      } else {
        var title = "Xóa người nộp thuế";
        var contentPopup = "";
        if (this.select.length == 1) {
          contentPopup = `Bạn có chắc chắn muốn xóa <b>${this.select[0].FullName}</b> không?`;
        } else {
          contentPopup =
            "Danh sách người nộp thuế được chọn sẽ bị xóa khỏi hệ thống. Bạn có chắc chắn muốn xóa không?";
        }
        var buttons = [
          {
            text: "Có",
            class: "btn-delete",
            callback: this.deleteSelectedRow,
          },
          {
            text: "Không",
            class: "btn-grey",
            callback: () => {},
          },
        ];
        this.$_Popup.delete(title, contentPopup, buttons);
      }
    },
    async deleteSelectedRow() {
      var res = await declaration.deleteDeclarationDetail(this.listIdDelete);
      if (res.data.Success && res.data.MISACode == 200) {
        this.$_Notification.success(
          "Thành công",
          `Xóa thành công ${this.listIdDelete.length} người nộp thuế`
        );
        this.getData();
        this.isCheckRow = false;
      } else
        this.$_Notification.error(
          "Thất bại",
          "Bạn xóa người nộp thuế thất bại.Vui lòng kiểm tra lại."
        );
    },
    async getData() {
      var res = await declaration.getDeclarationDetail(this.filterBar);

      if (res.data.Success && !res.data.Data.DeclarationDetailList.IsEmpty) {
        this.mapData(res.data.Data.DeclarationDetailList.DeclarationDetails);
        this.isEmpty = res.data.Data.DeclarationDetailList.IsEmpty;
        this.loading = false;
        this.numberOfRecord =
          res.data.Data.DeclarationDetailList.TotalDeclarationDetail;
        this.$store.commit(
          "taxPeriod/setData",
          res.data.Data.DeclarationDetailList.DeclarationDetails
        );
      } else {
        this.isEmpty = true;
        this.dataTable = [];
        this.$store.commit("taxPeriod/setData", this.dataTable);
        this.loading = false;
      }
    },

    /**
     * map dữ liệu vào table
     * @param val mảng dữ liệu lấy từ service
     * createdby tcduong 15.9.2020
     */
    mapData(val) {
      var datas = [];
      val.forEach((item) => {
        var data = {};
        data.DeclarationDetailID = item["DeclarationDetailID"];
        data.EmployeeCode = item["EmployeeCode"];
        data.FullName = item["FullName"];
        data.OrganizationUnitName = item["OrganizationUnitName"];
        data.TaxNo = item["TaxNo"];
        data.PositionID = item["PositionID"];
        data.NumberDependentIncrease = item["NumberDependentIncrease"];
        data.NumberDependentReduced = item["NumberDependentReduced"];
        datas.push(data);
      });
      this.dataTable = [...datas];
      this.addSuccess = false;
      this.addDependentSuccess = false;
    },

    refreshTable() {},
    getSelection() {},
    getObjectEmit() {},
    handleSelectAll() {},
    handleRowDBLClick(val) {
      this.dependentDetail = val;
      this.isDialogDetailOpen = true;
    },

    getRowsSelection(rows) {
      if (rows.length > 0) {
        var listId = rows.map(function (item) {
          return item.DeclarationDetailID;
        });
        this.listIdDelete = [...listId];
        this.select = rows;
        this.isCheckRow = true;
      } else {
        this.isCheckRow = false;
      }
    },
    /**
     * Phân trang
     * creatd by tcduong 30.9.2020
     * @param{val là object gồm pageIndex và pageSize}
     */
    pagination(val) {
      this.filterBar.pageSize = val.limit;
      this.filterBar.pageIndex = val.page;
      this.getData();
    },
    /**
     * Xem tờ khai
     * Create by: ntdong 28/09/2020
     */
    openViewDeclaration() {
      this.viewDeclarationFlag = true;
    },
    /**
     * Đóng tờ khai
     * Create by: nqhiep 01/10/2020
     */
    closeViewDeclaration() {
      this.viewDeclarationFlag = false;
    },

    /**
     * Hàm sự kiện khi click mũi tên quay lại
     * createdby lttuan 190920
     */
    goBack() {
      this.$router.push("/dependent-register");
    },

    /**
     * Hàm trả về title của frame
     * Created By : NQKHAI(18/11/2020)
     */
    convertTitle(title) {
      var firstLetter = title.substring(0, 1).toLowerCase();
      return "Hồ sơ " + firstLetter + title.substring(1, title.length);
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/typo.scss";
@import "@/styles/variables.scss";
.depentdent-person-detail {
  // Thêm css cho title lttuan 091020
  .page-header {
    margin: 0 -24px 0px -24px;
    padding: 0 24px 0px 24px;
    display: flex;
    align-items: center;
    background-color: #fff;

    .left {
      display: flex;
      align-items: center;

      #img-back {
        display: flex;
        align-items: center;
        justify-content: center;

        height: 36px;
        width: 36px;
        cursor: pointer;

        img {
          padding: 0 10px 0 10px;
        }
      }
      .page-title {
        line-height: 40px;

        .heading {
          font-weight: bold;
          margin: 16px 0px;
        }
      }
    }

    .right {
      margin: 16px 0px;
    }
  }
  .tbl-summary .el-table__body-wrapper {
    height: calc(100% - 53px) !important;
  }
  height: 100%;
  .temporary-mode-btn {
    position: absolute;
    top: 57%;
    left: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    transform: translate(-50%, -50%);
  }

  .filter-bar {
    margin-top: 24px;
    margin-bottom: 16px;

    .button-container {
      float: right !important;
      width: 220px !important;
    }
    .btn-white-icon {
      color: $hover-primary !important;
    }
    .el-select-tree .el-input__inner {
      border: none !important;
    }
    .el-select-tree .el-input__inner::placeholder {
      color: $dark !important;
    }
  }
  .del-bar {
    margin-top: 24px;
    margin-bottom: 15px;
    .button-container {
      float: left !important;
    }
  }

  .dependent-person-table {
    height: calc(100% - 56px);

    .el-loading-parent--relative {
      position: absolute !important;
      top: -56px;
      left: 0;
      right: 0;
      bottom: 0;
      z-index: 99999999;

      .el-loading-mask {
        opacity: 1;
      }
    }
  }

  .button-sign {
    display: flex;
    position: absolute;
    right: 0;
    margin-right: 8px;
  }
  #iconel {
    float: left;
  }
  .btn-add-taxpayer {
    right: 0;
    padding-right: 0px !important;
  }

  .content {
    height: calc(100% - 96px);
  }
}
.el-dropdown .el-button-group {
  display: inline-flex;
}

div.page-title.d-flex.el-row {
  align-items: center;
}

.del-title {
  position: relative;
  top: 10px;
  left: 10px;
  cursor: pointer;
  color: $hover-primary;
}
.empty-content {
  width: 464px;
  height: 262px;
  margin: auto;
  display: flex;
  flex-flow: wrap;
  justify-content: center;
  position: absolute;
  top: calc(50% - 100px);
  left: calc(50% - 200px);
}
</style>