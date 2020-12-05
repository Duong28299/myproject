<template>
  <!-- view xem thông tin chi tiết của hồ sơ -->
  <div class="taxRegisterDetail">
    <!-- Phần page header -->
    <el-row class="page-header">
      <el-col :span="16" class="left">
        <span id="img-back" @click="goBack">
          <img src="@/icons/ico-expand.png" />
        </span>
        <span class="page-title">
          <p class="heading">{{ title }}</p>
        </span>
      </el-col>
      <el-col :span="8" v-if="!isDataEmpty" class="right">
        <m-btn
          type="btn-white-icon"
          name="Xem tờ khai"
          :handleClick="openDeclarationDialog"
          style="padding-left: 10px"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18561@2x.png" />
          </template>
        </m-btn>
        <m-btn
          type="btn-primary-icon"
          name="Ký nộp qua mTax"
          :handleClick="openDialog"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18580@2x.png" />
          </template>
        </m-btn>
      </el-col>
    </el-row>
    <!--Hết Phần page header -->

    <!-- Phần tìm kiếm và lọc -->
    <el-row class="filter-bar" justify="space-between" v-if="!isDataEmpty">
      <!-- cụm tìm kiếm và bộ phận phòng ban -->
      <el-col :span="19">
        <el-row :gutter="16" v-if="!isCheckRow">
          <!-- phần tìm kiếm -->
          <el-col :span="10">
            <field-input
              type="input-icon"
              classicon="icon-search"
              :placeholder="'Tìm kiếm theo họ và tên, mã nhân viên'"
              :valueInput="filter.keyword"
              @value="filter.keyword = $event"
            ></field-input>
          </el-col>

          <!-- bộ phận/phòng ban -->
          <el-col :span="14">
            <el-row :gutter="10">
              <el-col :span="8">
                <tree-select
              :expand="false"
              responeType="string"
              @value-checked="filter.organizationUnit =$event"
              :options="listOrganizationUnit"
            ></tree-select>
                
              </el-col>
            </el-row>
          </el-col>
        </el-row>

        <!-- Xóa khi có selected -->
        <el-row :gutter="16" v-if="isCheckRow">
          <el-col class="btn-for-row-active">
            <m-btn
              class="btn-del"
              type="btn-white3-onlyicon"
              tooltip="Xóa"
              :handleClick="callDeleteMulti"
              positionTooltip="right"
              :disabled="isDisableButtonDelete"
            >
              <template v-slot:icon>
                <img
                  v-if="!isDisableButtonDelete"
                  class="icon"
                  style="position: relative; top: -3px; right: 3px"
                  src="@/icons/Group 18660@2x.png"
                />
                <img
                  v-if="isDisableButtonDelete"
                  class="icon"
                  style="position: relative; top: -3px; right: 3px"
                  src="@/icons/Group 18660_disabled@2x.png"
                />
              </template>
            </m-btn>
            <div class="un-select-all">
              <p @click="unSelectAllRow">Bỏ chọn tất cả</p>
            </div>
          </el-col>
        </el-row>
      </el-col>

      <!-- Nút Thêm người đăng ký ở  phần filter -->
      <el-col :span="5" v-if="!isCheckRow">
        <m-btn
          type="multi-button3"
          :handleClick="openTaxCodeSubcribers"
          :name="'Thêm người đăng ký'"
          :dataDropdown="dataDropdown"
          @option1="openTaxCodeSubcribers"
          style="position: relative; top: -13px"
          v-if="!isDisableButtonDelete"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18242@2x.png" />
          </template>
        </m-btn>
      </el-col>
    </el-row>
    <!--Hết Phần tìm kiếm và lọc -->

    <!-- Bảng hiện thị dữ liệu -->
    <el-row class="tax-register-table" v-if="!isDataEmpty">
      <div v-if="isLoading" v-loading="isLoading"></div>
      <tbl-tax-register
        ref="taxRegisterDetailTable"
        class="tax-register"
        :listColumn="
          GroupType == 20 ? listColumnForModify : listColumnForRegist
        "
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
        @row-action="getRowAction"
        :loading="false"
        :ruleSort="ruleSort"
      ></tbl-tax-register>
    </el-row>

    <!-- btn Thêm người đăng ký ở giữa màn hình, khi không có hồ sơ -->
    <el-row
      class="empty-case"
      v-if="
        ((isDataEmpty && !isLoading) || numberOfRecord == 0) &&
        Declaration.DeclarationStatus == 1
      "
    >
      <div>
        <img
          src="@/assets/images/Group 18278@2x.png"
          alt="not have data"
          style="position: relative"
        />
      </div>
      <div style="padding: 13px 0px 15px 0px" v-if="!isSearching">
        Hiện chưa có người đăng ký mã số thuế. Vui lòng thêm mới!
      </div>
      <div style="padding: 13px 0px 15px 0px" v-if="isSearching">
        Không tìm thấy kết quả phù hợp
      </div>
      <m-btn
        type="multi-button"
        :handleClick="openTaxCodeSubcribers"
        :name="'Thêm người đăng ký'"
        :dataDropdown="dataDropdown"
        @option1="openTaxCodeSubcribers"
        v-if="!isSearching"
      >
        <template v-slot:icon>
          <img class="icon" src="@/icons/Group 18241@2x.png" />
        </template>
      </m-btn>
    </el-row>

    <!-- dialog thêm mới hoặc update
    Với formmode = 0 là update, formmode = 1 là thêm mới-->
    <new-subcribers
      :DeclarationObjectDetail="DeclarationObjectDetail"
      :declarationID="declarationID"
      :isNewSubcribersDialog="isNewSubcribersDialog"
      :FormMode="FormMode"
      @close-dialog="closeNewSubcribersDialog"
      @reset-table="refreshTable"
      :GroupType="GroupType"
      :listOrganizationUnit="listOrganizationUnit"
    ></new-subcribers>

    <!-- dialog xem thông tin chi tiết tờ khai -->
    <view-detail
      :isTaxRegisterDialogOpen="isTaxRegisterDialogOpen"
      :DeclarationObjectDetail="DeclarationObjectDetail"
      :Declaration="Declaration"
      @close-dialog="closeNewSubcribersDialog"
      @open-edit-dialog="openEditDialog"
      :GroupType="GroupType"
    ></view-detail>

    <!-- dialog xem tờ khai -->
    <view-declaration
      :isDeclarationDialog="isDeclarationDialog"
      :declarationID="declarationID"
      @close-dialog="closeDeclarationDialog"
    ></view-declaration>
  </div>
</template>

<script>
import Button from "@/components/buttons";
import Fieldinput from "@/components/inputs";
import treeSelect from "@/components/trees";
import Table from "@/components/tables";
import NewSubcribers from "./new-register-detail/index";
import TaxRegisterInforDetail from "../tax-register-detail/View-tax-register-detail/index";
import declarationService from "@/api/etax/declaration.js";
import organizationUnitService from "@/api/etax/organization-unit.js";
import ViewDeclaration from "./view-declaration/index";

export default {
  name: "TaxRegister",
  components: {
    "m-btn": Button,
    "field-input": Fieldinput,
    "tree-select": treeSelect,
    "tbl-tax-register": Table,
    "new-subcribers": NewSubcribers,
    "view-detail": TaxRegisterInforDetail,
    "view-declaration": ViewDeclaration,
  },
  data() {
    return {
      declaration: {
        declarationStatus: 1,
        declarationName: "",
      },
      ruleSort: {
        prop: "FullName",
        order: "ascending",
      },
      TaxCode: "0101243150",
      isNotExpired: false,
      DeclarationStatus: 0,
      FormMode: 1,
      Declaration: {},
      DeclarationDetailID: null,
      isTaxRegisterDialogOpen: false,
      isNewSubcribersDialog: false,
      isDeclarationDialog: false,
      GroupType: 0,
      title: "",
      declarationID: "",
      arraySetting: {
        value: "OrganizationUnitID",
        label: "OrganizationUnitName",
        children: "OrganizationUnitsChildren",
      },
      listColumnForRegist: [
        {
          prop: "EmployeeCode",
          label: "Mã nhân viên",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
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
          label: "Bộ phận/Phòng ban",
          propertyName: "OrganizationUnitName",
          width: "",
          minWidth: "170",
          align: "left",
          type: "",
          prop: "OrganizationUnitName",
          sortable: true,
        },
        {
          label: "Ngày sinh",
          propertyName: "DateOfBirth",
          width: "120",
          minWidth: "120",
          align: "left",
          type: "date",
          prop: "DateOfBirth",
          sortable: true,
        },
        {
          label: "Số CMND/ CCCD/ Hộ chiếu",
          tooltip: "Số Chứng minh thư nhân dân/ Căn cước công dân/ Hộ chiếu",
          propertyName: "CitizenIdentityNo",
          width: "",
          minWidth: "130",
          align: "left",
          type: "",
          prop: "CitizenIdentityNo",
          sortable: true,
        },
        {
          label: "Số điện thoại",
          propertyName: "Phone",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "Phone",
          sortable: true,
        },
        {
          label: "Email",
          propertyName: "Email",
          width: "",
          minWidth: "160",
          align: "left",
          type: "",
          prop: "Email",
          sortable: true,
        },
      ],
      listColumnForModify: [
        {
          prop: "EmployeeCode",
          label: "Mã nhân viên",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
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
          prop: "TaxNo",
          label: "Mã số thuế",
          propertyName: "TaxNo",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          sortable: true,
        },
        {
          label: "Bộ phận/Phòng ban",
          propertyName: "OrganizationUnitName",
          width: "",
          minWidth: "170",
          align: "left",
          type: "",
          prop: "OrganizationUnitName",
          sortable: true,
        },
        {
          label: "Ngày sinh",
          propertyName: "DateOfBirth",
          width: "120",
          minWidth: "120",
          align: "left",
          type: "date",
          prop: "DateOfBirth",
          sortable: true,
        },
        {
          label: "Số CMND/ CCCD/ Hộ chiếu",
          tooltip: "Số Chứng minh thư nhân dân/ Căn cước công dân/ Hộ chiếu",
          propertyName: "CitizenIdentityNo",
          width: "",
          minWidth: "130",
          align: "left",
          type: "",
          prop: "CitizenIdentityNo",
          sortable: true,
        },
        {
          label: "Số điện thoại",
          propertyName: "Phone",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "Phone",
          sortable: true,
        },
        {
          label: "Email",
          propertyName: "Email",
          width: "",
          minWidth: "160",
          align: "left",
          type: "",
          prop: "Email",
          sortable: true,
        },
      ],
      dataTable: [],
      DeclarationObjectDetail: {},
      haveCheck: true,
      pageSizes: [25, 50, 75, 100],
      numberOfRecord: 1,
      layoutPaging: "prev, pager, next, sizes",
      isDataEmpty: false,
      isSearching: false,
      isLoading: true,
      listRowSelected: [],
      isCheckRow: false,
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
      listOrganizationUnit: [],
      dataDropdown: [
        {
          icon: "@/icons/Group 17812.png",
          name: "Thêm ngươi đăng ký",
          nameVal: "option1",
        },
        {
          icon: "@/icons/Group 17812.png",
          name: "Chọn từ hồ sơ người nộp thuế",
          nameVal: "option2",
        },
        {
          icon: "@/icons/Group 17812.png",
          name: "Nhập khẩu từ hồ sơ exel",
          nameVal: "option3",
        },
      ],
      filter: {
        keyword: "",
        organizationUnit: "",
        declarationID: "",
        pageIndex: 1,
        pageSize: 25,
      },
      formData: {},
      organizationTaxNo: "",
      organizationName: "",
      taxAgentName: "",
      TaxAgentEmployee:"",
      TaxAgentPracticingCertifacateNo:"",
      SignBy:"",
      SignDate:"",
      isTaxRegister: false,
      isChangeTaxRegister: false,
    };
  },
  watch: {
    /**
     * load dữ liệu & cập nhật lại dữ liệu bộ lọc
     * created by: LTTUAN 160920
     */
    filter: {
      handler() {
        this.getListTaxRegisterDetail();
        this.checkIsSearching();
      },
      deep: true,
    },
  },
  computed: {
    isDisableButtonDelete() {
      return this.DeclarationStatus !== 1 ? true : false;
    },
  },
  methods: {
    /**
     * Hàm xử lý việc mở form edit từ form view
     * @param val người khai được chọn
     * createdby ltuan 23.09.2020
     */
    openEditDialog(val) {
      this.FormMode = 0;
      this.openDialogEditMode(val);
    },

    /**
     * Hàm bắt sự kiện click vào button xem tờ khai
     * createdby nqhiep 22.09.2020
     */
    openDeclarationDialog() {
      this.isDeclarationDialog = true;
    },

    /**
     * Hàm bắt sự kiện click vào button đóng tờ khai
     * createdby nqhiep 22.09.2020
     */
    closeDeclarationDialog() {
      this.isDeclarationDialog = false;
    },    

    /**
     * Định dạng lại ngày tháng
     * @param val ngày tháng yyyy-mm-dd
     * createdby nqhiep 23.09.2020
     */
    formatDate(val) {
      let result =
        val !== null
          ? val.slice(8, 10) + "-" + val.slice(5, 7) + "-" + val.slice(0, 4)
          : null;
      return result;
    },
    /**
     * Hàm xử lý chọn các option trong dropdown tại table
     * @param val row được chọn
     * createdby ltuan 19.09.2020
     */
    getRowAction(val) {
      switch (val.value) {
        case 0:
          this.FormMode = 0;
          this.openDialogEditMode(val.row);
          //this.refreshTable();
          break;
        case 1:
          this.callDeleteOne(val.row);
          break;
      }
    },

    /**
     * Hàm thực hiện việc sửa một dòng
     * @param row Object row được chọn
     * createdby lttuan 22.09.2020
     */
    async openDialogEditMode(row) {
      await this.getDeclarationDetailByID(row.DeclarationDetailID);
      this.isNewSubcribersDialog = true;
    },

    /**
     * Hàm xử lý khi chọn xóa 1 row
     * @param row row được chọn
     * createdby ndhuy 18.09.2020
     */
    callDeleteOne(row) {
      if (this.DeclarationStatus != 1) return;
      var buttons = [
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
        {
          text: "Có",
          class: "btn-delete",
          callback: () => {
            this.deleteTaxRegisterHandle([row]);
          },
        },
      ];
      var message =
        'Bạn có chắc chắn muốn xóa nhân viên <b>"' +
        row.FullName +
        '"</b> không?';
      this.$_Popup.delete("Xác nhận", message, buttons);
    },

    /**
     * Xử lý xóa hồ sơ
     * @param listTaxRegister mảng hồ sơ
     * createdby ndhuy 18.09.2020
     */
    deleteTaxRegisterHandle(listTaxRegister) {
      var listId = listTaxRegister.map((row) => {
        return row.DeclarationDetailID;
      });
      if (this.deleteTaxRegisterDetail(listId)) {
        var message = "Xóa hồ sơ thành công";
        this.popUpSuccess(message);
      } else {
        this.popUpFalse("Có lỗi xảy ra, vui lòng thử lại sau");
      }
    },

    /**
     * Hàm thực hiện gọi service xóa hồ sơ
     * @param arrId mảng id truyền vào
     * createdby ndhuy 21.09.2020
     */
    async deleteTaxRegisterDetail(arrId) {
      try {
        var res = await declarationService.deleteDeclarationDetail(arrId);
        if (res.data.Success == true) {
          this.unSelectAllRow();
          this.refreshTable();
          return true;
        } else {
          this.unSelectAllRow();
          this.refreshTable();
          return false;
        }
      } catch (e) {
        this.unSelectAllRow();
        this.refreshTable();
        return false;
      }
    },

    async refreshTable() {
      // var organizationUnit = [this.listOrganizationUnit[0].OrganizationUnitID];
      var organizationUnit = null;

      this.filter = {
        keyword: "",
        organizationUnit: organizationUnit,
        pageIndex: 1,
        pageSize: 25,
        declarationID: this.declarationID,
      };
      this.filter.pageSize = this.pageSizes[0];

      this.isLoading = true;
      await this.getListTaxRegisterDetail();
      if (this.dataTable.length > 0) this.isDataEmpty = false;
    },

    /**
     * Hàm mở bảng
     * lttuan 190920
     */
    openTaxCodeSubcribers() {
      this.isNewSubcribersDialog = true;
      this.FormMode = 1;
    },
    /**
     * Hàm sự kiện khi thao thao tác với pagin
     * lttuan 190920
     */
    getFilter(val) {
      this.filter.pageIndex = val.page;
      this.filter.pageSize = val.limit;
    },
    getSelection() {},
    getObjectEmit() {},

    /**
     * Hàm bắt sự kiện click vào cột checkbox
     * @param row mảng các hàng được chọn
     * createdby ndhuy 11.09.2020
     */
    getRowsSelection(row) {
      this.listRowSelected = row;
      if (row.length >= 1) {
        this.isCheckRow = true;
      } else {
        this.isCheckRow = false;
      }
    },
    handleSelectAll() {},
    /**
     * DB click vào row hiển thị dialog xem chi tiết thông tin người đăng ký
     */
    callDeleteMulti() {
      if (this.DeclarationStatus != 1) return;
      if (this.listRowSelected.length == 1) {
        this.callDeleteOne(this.listRowSelected[0]);
      } else if (this.listRowSelected.length > 1) {
        var buttons = [
          {
            text: "Không",
            class: "btn-grey",
            callback: () => {},
          },
          {
            text: "Có",
            class: "btn-delete",
            callback: () => {
              this.deleteTaxRegisterHandle(this.listRowSelected);
            },
          },
        ];
        var message =
          "Các nhân viên được chọn sẽ bị xóa khỏi hệ thống. Bạn có chắc chắn muốn xóa không?";
        this.$_Popup.delete("Xác nhận", message, buttons);
      }
    },
    async handleRowDBLClick(val) {
      await this.getDeclarationDetailByID(val.DeclarationDetailID);
      this.isTaxRegisterDialogOpen = true;
    },

    /**
     * Hàm gọi service lấy thông tin nhân viên đăng ký
     * Create by : ntdong 22/09/2020     *
     */
    async getDeclarationDetailByID(id) {
      var res = await declarationService.getDeclarationDetailByDeclarationDetailID(
        id
      );
      this.DeclarationObjectDetail = res.data.Data;
    },

    /**
     * Hàm xử lý sự kiện bỏ chọn tất cả các row
     * createdby ndhuy 11.09.2020
     */
    unSelectAllRow() {
      this.$refs.taxRegisterDetailTable.clearSelection();
    },

    /**
     * Hàm mở các sự kiện như ký nộp, xem tờ khai
     * createdby lttuan 190920
     */
    openDialog() {},

    /**
     * Hàm sự kiện khi click mũi tên quay lại
     * createdby lttuan 190920
     */
    goBack() {
      this.$router.push("/tax-register");
    },

    /**
     * hàm thực hiện việc lấy dữ liệu từ service rồi gán vào mảng
     * createdby lttuan 061020
     */
    async getListTaxRegisterDetail() {
      this.isLoading = true;

      // tránh trường hợp mảng organizationUnit = array[0]
      if (this.filter.organizationUnit != null)
        if (this.filter.organizationUnit.length == 0)
          this.filter.organizationUnit = null;

      var res = await declarationService.getDeclarationDetail(this.filter);
      // sửa lại đầu vào theo api mới
      if (res.data && res.data.Success) {
        this.mapDataServiceToTable(
          res.data.Data.DeclarationDetailList.DeclarationDetails
        );
        this.declaration.DeclarationName = res.data.Data.DeclarationName;
        this.declaration.DeclarationStatus = res.data.Data.DeclarationStatus;
        this.numberOfRecord =
          res.data.Data.DeclarationDetailList.TotalDeclarationDetail;
      }
    },

    /**
     * Thực hiện chuyển đổi data lấy từ service về data trong bảng
     * @param data mảng dữ liệu nhận về
     * createdby ndhuy 16.09.2020
     */
    mapDataServiceToTable(data) {
      var result = [];
      data.forEach((element) => {
        var obj = element;
        result.push(obj);
      });
      this.dataTable = result;
      if (this.dataTable.length == 0 && this.isSearching == false)
        this.isDataEmpty = true;
      if (this.dataTable.length !== 0 && this.isSearching == false)
        this.isDataEmpty = false;
      this.isLoading = false;
    },

    /**
     * Hàm kiểm tra có đang tìm kiếm hay không
     * createdby lttuan 190920
     */
    checkIsSearching() {
      if (this.filter.keyword == "") {
        this.isSearching = false;
      }
      if (this.filter.keyword !== "") {
        this.isSearching = true;
      }
    },
    /**
     * Hàm đóng dialog
     * createdby lttuan 190920
     */
    closeNewSubcribersDialog(val) {
      this.isNewSubcribersDialog = val;
      this.isTaxRegisterDialogOpen = val;
    },

    /**
     * Hàm hiển thị popup thành công
     * @param message nội dung popup
     * createdby ndhuy 15.09.2020
     */
    popUpSuccess(message) {
      // const loadin = this.$loading({
      //   lock: true,
      //   text: "Đang tải",
      //   spinner: "el-icon-loading",
      //   background: "rgba(0,0,0,0.7)",
      // });
      // setTimeout(() => {
      //   loadin.close();
      // }, 200);
      this.$_Notification.success("Thông báo", message);
    },

    /**
     * Hàm hiển thị popup thất bại
     * createdby ndhuy 15.09.2020
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

    async getDeclarationByID(id) {
      var res = await declarationService.getDeclarationID(id);
      if (typeof res.data != "undefined" && res.data.Success) {
        this.Declaration = res.data.Data;
      } else this.Declaration = {};
    },

    /**
     * Convert mảng chuổi orgainzation id về dạng id/id/...
     * @param val chuỗi organization id
     * createdby nhuy 05.10.2020
     */
    convertArrOrganizationID(val) {
      this.filter.organizationUnit = val;
    },

    async getOrganizationUnit(style, type) {
      this.listOrganizationUnit = [];
      var res = await organizationUnitService.getOrganizationUnit(style, type);
      if (typeof res.data != "undefined" && res.data.Success) {
        this.listOrganizationUnit.push(res.data.Data);
      } else this.listOrganizationUnit = [];
    },

    /**
     * convert chuỗi string có ký tự đầu là chữ viết hoa thành chữ thường
     * Create by : ntdong 08/10/2020
     */
    convertLowerCase(string) {
      return string.charAt(0).toLowerCase() + string.slice(1);
    },
  },

  async created() {
    this.declarationID = this.$route.params.id;
    await this.getDeclarationByID(this.declarationID);
    await this.getOrganizationUnit("tree", 0);
    this.filter.organizationUnit = null;
    this.filter.declarationID = this.$route.params.id;
    this.GroupType = this.Declaration.GroupType;
    this.title = this.Declaration.DeclarationName;
    this.DeclarationStatus = this.Declaration.DeclarationStatus;
    if (this.DeclarationStatus != 1)
      this.listaction.forEach((element) => {
        element.class = "disabled";
      });
  },

  mounted() {},
};
</script>

<style lang="scss">
@import "@/styles/typo.scss";
@import "@/styles/variables.scss";

.taxRegisterDetail {
  height: 100%;

  @media all and (max-width: 1366px) {
    // .page-header {
    //   margin: 0 -16px 16px -16px !important;
    // }
  }

  .page-header {
    margin: 0 -24px 16px -24px;
    padding: 0 24px 0px 24px;
    display: flex;
    align-items: center;
    background-color: $white;

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

  .filter-bar {
    display: flex;
    height: 56px;

    .el-input__inner {
      border: none;
    }

    .el-select ::placeholder {
      color: $dark !important;
    }

    .el-date-editor ::placeholder {
      color: $dark !important;
    }
    .btn-for-row-active {
      display: flex;
      padding-bottom: 16px;

      .button-container {
        float: none !important;
      }

      .un-select-all {
        height: 40px;

        p {
          color: $hover-primary;
          &:hover {
            cursor: pointer;
            color: $light-primary;
          }
        }
      }
    }

    .btn-sign,
    .btn-del {
      margin-right: 16px;

      img {
        width: 20px;
        height: 20px;
      }

      .el-button {
        padding: 9px 10px 12px 12px;
      }
    }
  }

  .tax-register-table {
    height: calc(100% - 144px);

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

  .empty-case {
    position: absolute;
    top: 50%;
    left: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    transform: translate(-50%, -50%);
  }
  #multi-button2 {
    button {
      background-color: #fff;
      color: #2944bb;
    }
  }

  .el-icon-circle-close {
    font-size: 18px !important;
  }

  .tbl-summary .el-table__body-wrapper {
    height: calc(100% - 53px);
  }
}

.el-dropdown-menu__item.disabled {
  pointer-events: none;
  opacity: 0.6;
}
</style>
