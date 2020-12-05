<template>
  <div class="taxRegister">
    <el-dialog
      :before-close="handleClose"
      title="Kết quả gửi hồ sơ"
      :visible.sync="dialogVisible"
      width="950px"
      class="dialog-iframe"
    >
      <iframe v-if="ifSrc" :src="ifSrc" width="100%" height="668"></iframe>
    </el-dialog>

    <!-- LƯU Ý KHÔNG ĐƯỢC ĐỔI DÒNG CODE TITLE DƯỚI ĐÂY
    MỌI THẮC MẮC LIÊN HỆ VDTHANG-->
    <!-- Phần page header -->
    <el-row class="page-header">
      <el-col :span="24">
        <div class="page-title d-flex">
          <h1>{{ getTitle }}</h1>
          <m-btn
            type="btn-white2-onlyicon-big"
            tooltip="Làm mới"
            positionTooltip="left"
            :handleClick="handlerResetTableButton"
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
    <!-- Phần tìm kiếm và lọc -->
    <el-row class="filter-bar" justify="space-between" v-if="!isDataEmpty">
      <el-col :span="19">
        <el-row :gutter="10" v-if="!isCheckRow">
          <el-col :span="8">
            <field-input
              type="input-icon"
              classicon="icon-search"
              :placeholder="'Tìm kiếm'"
              :valueInput="filter.keyword"
              @value="getValueSearch"
            ></field-input>
          </el-col>
          <el-col :span="16">
            <el-row :gutter="10">
              <el-col :span="6">
                <input-dropdown
                  selectID="regis-type"
                  :list="listRegisterType"
                  type="select-border-color"
                  label="NAME"
                  field="VALUE"
                  :placeholder="'Loại đăng kí'"
                  @value="getValueTaxRegisterType"
                  :clearable="true"
                ></input-dropdown>
              </el-col>
              <el-col :span="6">
                <input-dropdown
                  selectID="status"
                  :list="listStatus"
                  type="select-border-color"
                  label="name"
                  field="value"
                  :placeholder="'Trạng thái'"
                  @value="getValueTaxRegisterStatus"
                  :clearable="true"
                ></input-dropdown>
              </el-col>
              <el-col :span="6">
                <date-picker
                  type="year"
                  :size="'medium'"
                  :placeholder="'Năm đăng ký'"
                  width="100%"
                  formatInput="yyyy"
                  Valueformat="yyyy"
                  :value="filter.year"
                  @getValue="getValueTaxRegisterYear"
                  :borderType="'none-border-white'"
                  :clearable="true"
                ></date-picker>
              </el-col>
              <el-col :span="6">
                <input-dropdown
                  selectID="month"
                  :list="listMonth"
                  type="select-border-color"
                  label="MONTH_NAME"
                  field="MONTH"
                  :value="filter.month"
                  :placeholder="'Tháng lập hồ sơ'"
                  @value="getValueTaxRegisterMonth"
                  :clearable="true"
                ></input-dropdown>
              </el-col>
            </el-row>
          </el-col>
        </el-row>
        <!-- Ký nộp và xóa khi có selected -->
        <el-row :gutter="16" v-if="isCheckRow">
          <el-col class="btn-for-row-active">
            <m-btn
              class="btn-sign"
              type="btn-white3-onlyicon"
              tooltip="Ký nộp"
              :handleClick="sendDeclaration"
              positionTooltip="top"
              :disabled="disableButtonSignAndSubmit"
            >
              <template v-slot:icon>
                <img
                  v-show="!disableButtonSignAndSubmit"
                  class="icon"
                  src="@/icons/Group 18666@2x.png"
                />
                <img
                  v-show="disableButtonSignAndSubmit"
                  class="icon"
                  src="@/icons/Group 18667 disable (2).png"
                />
              </template>
            </m-btn>
            <m-btn
              class="btn-download"
              :type="'multi-button4'"
              :tooltip="'Tải về'"
              :dataDropdown="arrayDownload"
              :handleClick="download"
              positionTooltip="top"
              :disabled="disableButtonDownload"
            >
              <template v-slot:icon>
                <img
                  v-show="!disableButtonDownload"
                  class="icon"
                  src="@/icons/Component 95 – 1.png"
                />
              </template>
            </m-btn>
            <m-btn
              class="btn-del"
              type="btn-white3-onlyicon"
              tooltip="Xóa"
              :handleClick="callDeleteMulti"
              positionTooltip="top"
              :disabled="disableButtonDelete"
            >
              <template v-slot:icon>
                <img
                  v-if="!disableButtonDelete"
                  class="icon"
                  src="@/icons/Group 18660@2x.png"
                />
                <img
                  v-if="disableButtonDelete"
                  class="icon"
                  src="@/icons/Group 18660_disabled@2x.png"
                />
              </template>
            </m-btn>
            <div class="un-select-all" @click="unSelectAllRow">
              <p>Bỏ chọn {{ arrRowSelect.length }} hồ sơ</p>
            </div>
          </el-col>
        </el-row>
      </el-col>
      <!-- Nút thêm hồ sơ ở  phần filter -->
      <el-col :span="5" v-if="!isCheckRow">
        <m-btn
          type="btn-primary-icon"
          name="Thêm hồ sơ"
          :handleClick="openDialogCreateMode"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 17812.png" />
          </template>
        </m-btn>
      </el-col>
    </el-row>
    <!-- Bảng hiện thị dữ liệu -->
    <el-row class="tax-register-table"> 
      <div
        v-if="loadingFirst && isFirst"
        v-loading="loadingFirst"
        class="loading-first"
      ></div>
      <div
        v-if="loadingAfter && !isFirst"
        v-loading="loadingAfter"
        class="loading-after"
      ></div>
      <tbl-tax-register
        v-if="!isDataEmpty"
        ref="taxRegisterTable"
        class="tax-register"
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
        destiStatus="DeclarationStatus"
        :destiArrStatus="destiArrStatus"
        typepage="tờ khai"
        @pagination="getFilter"
        @selection="getSelection"
        @chip-emit="getObjectEmit"
        @handleSelectionChange="getRowsSelection"
        @row-action="getRowAction"
        @handleRowDBLClick="handleRowDBLClick"
        :loading="false"
      ></tbl-tax-register>
      <div
        class="empty-search"
        v-if="
          numberOfRecord == 0 && !isDataEmpty && !loadingFirst && !loadingAfter
        "
      >
        <img src="@/assets/images/Group 18279.png" 
        alt="not have data"
        />
        <span>Không tìm thấy kết quả phù hợp</span>
      </div>
    </el-row>
    <div
      class="empty-case"
      v-if="
        numberOfRecord == 0 && isDataEmpty && !loadingFirst && !loadingAfter
      "
    >
      <img
        src="@/assets/images/Group 19228.png"
        alt="not have data"
        class="img-empty"
      />
      <m-btn
        type="btn-primary-icon"
        name="Thêm hồ sơ"
        :handleClick="openDialogCreateMode"
      >
        <template v-slot:icon>
          <img class="icon" src="@/icons/Group 17812.png" />
        </template>
      </m-btn>
    </div>
    <new-tax-profile
      :isTaxRegisterDialogOpen="isTaxRegisterDialogOpen"
      @cancel-insert="cancelInsert"
      @refresh-table="refreshTable"
      @show-popup="showPopup"
      :taxProfile="taxProfileProp"
      :formMode="formMode"
    ></new-tax-profile>
  </div>
</template>

<script>
// import dialogel from "@/components/dialog";
import Button from "@/components/buttons";
import fieldinput from "@/components/inputs";
import Dropdown from "@/components/select";
import DatePicker from "@/components/date-time-pickers";
import Table from "@/components/tables";
import listStatus from "@/enums/chipEnum";
import listMonth from "@/enums/filter";
import taxRegisterType from "@/enums/taxRegister";
import NewTaxProfile from "./new-tax-profile";
import ApiDeclaration from "@/api/etax/declaration.js";

import signDeclaration from "@/mixins/signDeclaration.js";

// import actionTable from "@/enums/actionTable";

export default {
  name: "TaxRegister",
  components: {
    "m-btn": Button,
    "field-input": fieldinput,
    "input-dropdown": Dropdown,
    "date-picker": DatePicker,
    "tbl-tax-register": Table,
    "new-tax-profile": NewTaxProfile,
  },
  mixins: [signDeclaration],
  data() {
    return {
      isTaxRegisterDialogOpen: false,
      ifSrc: "",
      dialogVisible: false,
      disableButtonDelete: false,
      disableButtonDownload: false,
      disableButtonSignAndSubmit: false,
      arrayDownload: [
        {
          icon: "",
          name: "Tải Excel",
          callback: () => {},
        },
        {
          icon: "",
          name: "Tải XML",
          callback: () => {},
        },
        {
          icon: "",
          name: "Tải PDF",
          callback: () => {},
        },
      ],
      listColumn: [
        {
          label: "Tên hồ sơ",
          prop: "DeclarationName",
          propertyName: "DeclarationName",
          width: "",
          minWidth: "300",
          align: "left",
          type: "",
          sortable: true,
        },
        {
          label: "Loại đăng ký",
          propertyName: "GroupTypeName",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "GroupTypeName",
          sortable: true,
        },
        {
          label: "Năm đăng ký",
          propertyName: "PeriodYear",
          width: "",
          minWidth: "130",
          align: "left",
          type: "",
          prop: "PeriodYear",
          sortable: true,
        },
        {
          label: "Ngày lập",
          propertyName: "CreatedDate",
          width: "",
          minWidth: "120",
          align: "center",
          type: "date",
          prop: "CreatedDate",
          sortable: true,
          sortMethod: this.customSort,
        },
        {
          label: "Ngày nộp",
          propertyName: "SendDate",
          width: "",
          minWidth: "120",
          align: "center",
          type: "date",
          prop: "SubmitDate",
          sortable: true,
        },
        {
          label: "Trạng thái",
          propertyName: "DeclarationStatus",
          width: "180",
          minWidth: "180",
          align: "left",
          type: "chip",
          prop: "DeclarationStatus",
          sortable: true,
        },
      ],
      dataTable: [],
      haveCheck: true,
      pageSizes: [25, 50, 75, 100],
      numberOfRecord: 0,
      layoutPaging: "prev, pager, next, sizes",
      chipArray: "TAX_REGISTER",
      isDataEmpty: true,
      loadingFirst: true,
      loadingAfter: true,
      isFirst: true,
      arrRowSelect: [],
      isCheckRow: false,
      taxProfileProp: {},
      formMode: 0, //Chế độ của dialog :0-thêm mới,1-sửa
      listaction: [
        {
          value: 0,
          name: "Sửa",
        },
        {
          value: 1,
          name: "Xóa",
          class: "text-red",
        },
      ],
      // mảng lưu trữ trạng thái để ẩn hiện button xem sửa, mã 2 3 4 5 tương ứng với các declartionStatus
      destiArrStatus: [2, 3, 10, 11, 12],
      listRegisterType: [],
      listStatus: [],
      listMonth: [],
      filter: {
        groupMode: "TaxRegisterMode",
        groupType: 0,
        month: "",
        year: new Date().getFullYear().toString(),
        pageIndex: 1,
        pageSize: 25,
        declarationStatus: "",
        keyword: "",
      },
    };
  },

  watch: {
    arrRowSelect: {
      handler: function (val) {
        var obj = this.filterDeleteTaxRegister(val);

        if (obj.canDelete.length == 0) {
          this.disableButtonDelete = true;
          this.disableButtonSignAndSubmit = true;
        } else {
          this.disableButtonDelete = false;
          this.disableButtonDownload = false;
          this.disableButtonSignAndSubmit = false;
        }
      },
      deep: true,
    },
  },

  methods: {
    /**
     * Hàm rule sắp xếp mặc định của table
     * createdby ndhuy 10.07.2020
     */
    customSort(obj1, obj2) {
      var modifiedDate1 = new Date(obj1.ModifiedDate);
      var modifiedDate2 = new Date(obj2.ModifiedDate);
      var createdDate1 = new Date(obj1.CreatedDate);
      var createdDate2 = new Date(obj2.CreatedDate);
      if (modifiedDate1 > modifiedDate2) return 1;
      else if (modifiedDate1 < modifiedDate2) return -1;
      else {
        if (createdDate1 > createdDate2) return 1;
        else if (createdDate1 < createdDate2) return -1;
        else return 0;
      }
    },

    // Hàm đóng reset src iframe
    handleClose(done) {
      setTimeout(() => {
        this.ifSrc = "";
      }, 500);
      done();
    },

    handlerResetTableButton() {
      this.refreshTable();
    },

    async refreshTable(year = new Date().getFullYear().toString()) {
      this.filter = {
        groupMode: "TaxRegisterMode",
        groupType: "",
        month: "",
        year: year,
        pageIndex: 1,
        pageSize: 1,
        declarationStatus: "",
        keyword: "",
      };
      this.filter.pageSize = this.pageSizes[0];
      await this.getListTaxRegister();
    },

    /**
     * Hàm thực hiện set lại page index và pagesize khi chọn control pagination
     * createdby ndhuy 16.09.2020
     */
    getFilter(val) {
      this.filter.pageSize = val.limit;
      this.filter.pageIndex = val.page;
      this.getListTaxRegister();
    },
    getSelection() {},
    // Hàm gọi service lấy src cho iframe

    getObjectEmit(val) {
      const taxCode = val.OrganizationTaxNo;
      const submissionCode = val.SubmissionCode
        ? val.SubmissionCode
        : "11420201001895800";
      const res = ApiDeclaration.getPreviewNotification(
        taxCode,
        submissionCode
      );
      this.dialogVisible = true;
      this.ifSrc = res;
    },

    /**
     * Hàm bắt sự kiện click vào cột checkbox
     * @param arrRow mảng các hàng được chọn
     * createdby ndhuy 11.09.2020
     */
    getRowsSelection(arrRow) {
      this.arrRowSelect = arrRow;
      if (arrRow.length >= 1) {
        this.isCheckRow = true;
      } else {
        this.isCheckRow = false;
      }
    },

    /**
     * Hàm bắt sự kiện khi click đúp vào 1 row
     * @param val row được chọn
     * createdby ndhuy 17.09.2020
     */
    handleRowDBLClick(val) {
      this.$router.push({
        name: "TaxRegisterDetails",
        params: {
          id: val.DeclarationID,
          DeclarationName: val.DeclarationName,
          DeclarationStatus: val.DeclarationStatus,
          GroupType: val.GroupType,
        },
      });
    },

    /**
     * Hàm xử lý chọn các option trong dropdown tại table
     * @param val row được chọn
     * createdby ndhuy 18.09.2020
     */
    getRowAction(val) {
      switch (val.value) {
        case 0:
          this.handleRowDBLClick(val.row);
          // this.openDialogEditMode(val.row);
          break;
        case 1:
          this.callDeleteOne(val.row);
          break;
      }
    },

    /**
     * Hàm xử lý khi chọn xóa 1 row
     * @param row row được chọn
     * createdby ndhuy 18.09.2020
     */
    callDeleteOne(row) {
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
            this.deleteTaxRegisterHandle(this.filterDeleteTaxRegister([row]));
          },
        },
      ];
      var message =
        'Bạn có chắc chắn muốn xóa <b>"' + row.DeclarationName + '"</b> không?';
      this.$_Popup.delete("Xác nhận", message, buttons);
    },

    /**
     * Hàm thực hiện khi click button xóa nhiều
     * createdby ndhuy 18.09.2020
     */
    callDeleteMulti() {
      if (this.arrRowSelect.length == 1) {
        this.callDeleteOne(this.arrRowSelect[0]);
      } else if (this.arrRowSelect.length > 1) {
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
              this.deleteTaxRegisterHandle(
                this.filterDeleteTaxRegister(this.arrRowSelect)
              );
            },
          },
        ];
        var message =
          "Các hồ sơ được chọn sẽ bị xóa khỏi hệ thống. Bạn có chắc chắn muốn xóa không?";
        this.$_Popup.delete("Xác nhận", message, buttons);
      }
    },

    /**
     * Lọc ra các hồ sơ có thể xóa và không thể xóa từ mảng hồ sơ truyền vào
     * @param listTaxRegister mảng các hồ sơ
     * createdby ndhuy 18.09.2020
     */
    filterDeleteTaxRegister(listTaxRegister) {
      var result = {};
      result.total = listTaxRegister.length;
      result.canDelete = [];
      result.notDelete = [];
      listTaxRegister.forEach((item) => {
        if (item.DeclarationStatus == 1) result.canDelete.push(item);
        else result.notDelete.push(item);
      });
      return result;
    },

    /**
     * Xử lý xóa hồ sơ
     * @param listTaxRegister mảng nhận được từ hàm lọc các hồ sơ xóa được và không xóa được
     * createdby ndhuy 18.09.2020
     */
    deleteTaxRegisterHandle(listTaxRegister) {
      var listIdCanDelete = listTaxRegister.canDelete.map((row) => {
        return row.DeclarationID;
      });
      if (this.deleteTaxRegister(listIdCanDelete)) {
        var message = "";
        if (listTaxRegister.total > 1)
          message +=
            "Đã xóa thành công " +
            listTaxRegister.canDelete.length +
            "/" +
            listTaxRegister.total +
            "hồ sơ." +
            "Xóa thất bại " +
            listTaxRegister.notDelete.length +
            "/" +
            listTaxRegister.total +
            " hồ sơ";
        else message += "Xóa hồ sơ thành công";
        this.popUpSuccess(message);
      } else {
        this.popUpFalse("Có lỗi xảy ra, vui lòng thử lại sau");
      }
    },

    /**
     * Hàm thực hiện gọi service xóa hồ sơ
     * @param arrId mảng id truyền vào
     * createdby ndhuy 17.09.2020
     */
    async deleteTaxRegister(arrId) {
      // var arrId = this.arrRowSelect.map((row)=>{
      //   return row.DeclarationID;
      // });
      //this.unSelectAllRow();
      try {
        var res = await ApiDeclaration.deleteDeclaration(arrId);

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

    /**
     * Hàm xử lý sự kiện bỏ chọn tất cả các row
     * createdby ndhuy 11.09.2020
     */
    unSelectAllRow() {
      this.$refs.taxRegisterTable.clearSelection();
    },
    cancelInsert(value) {
      this.isTaxRegisterDialogOpen = value;
      //this.formMode = 0;
    },

    /**
     * Mở dialog với chế độ thêm mới
     * createdby ndhuy 18.09.2020
     */
    openDialogCreateMode() {
      this.formMode = 0;
      this.taxProfileProp = {};
      this.isTaxRegisterDialogOpen = true;
    },

    /**
     * Mở dialog với chế độ chỉnh sửa
     * @param taxProfile row chỉnh sửa
     * createdby ndhuy 18.09.2020
     */
    openDialogEditMode(taxProfile) {
      this.formMode = 1;
      this.taxProfileProp = taxProfile;
      this.isTaxRegisterDialogOpen = true;
    },

    /**
     * Hàm gọi service lấy danh sách hồ sơ đăng ký thuế
     * createdby ndhuy 15.09.2020
     */
    async getListTaxRegister() {
      this.loadingFirst = true;
      this.loadingAfter = true;
      var res = await ApiDeclaration.getDeclaration(this.filter);
      if (typeof res.data != "undefined" && res.data.Success) {
        this.numberOfRecord = res.data.Data.TotalDeclaration;
        this.mapDataServiceToTable(res.data.Data.DeclarationLists);
        this.isDataEmpty = res.data.Data.IsEmpty;
      } else {
        this.isDataEmpty = true;
        this.dataTable = [];
      }
      this.loadingFirst = false;
      this.loadingAfter = false;
      this.isFirst = false;
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
        obj.GroupTypeName = taxRegisterType.findInArray(
          this.listRegisterType,
          element.GroupType
        );
        result.push(obj);
      });
      this.dataTable = result;
    },

    insertTaxRegister() {},

    /**
     * Cập nhật filter khi người dùng chọn dropdown loại đăng ký
     * @param val giá trị được chọn
     * createdby ndhuy 16.09.2020
     */
    getValueTaxRegisterType(val) {
      this.filter.groupType = val;
      this.getListTaxRegister();
    },

    /**
     * Lấy về giá trị filter năm người dùng chọn
     * @param val năm được chọn
     * createdby ndhuy 16.09.2020
     */
    getValueTaxRegisterYear(val) {
      this.filter.year = val;
      this.getListTaxRegister();
    },

    /**
     * Lấy về giá trị filter tháng người dùng chọn
     * @param val tháng được chọn
     * createdby ndhuy 16.09.2020
     */
    getValueTaxRegisterMonth(val) {
      this.filter.month = val;
      this.getListTaxRegister();
    },

    /**
     * Lấy về giá trị filter trạng thái hồ sơ người dùng chọn
     * @param val trạng thái được chọn
     * createdby ndhuy 16.09.2020
     */
    getValueTaxRegisterStatus(val) {
      this.filter.declarationStatus = val;
      this.getListTaxRegister();
    },

    /**
     * Lấy về giá trị ô tìm kiếm
     * @param val giá trị người dùng nhập
     * createdby ndhuy 16.09.2020
     */
    getValueSearch(val) {
      this.filter.keyword = val;
      this.getListTaxRegister();
    },

    /**
     * Hàm hiển thị popup theo dạng
     * @param val obj bao gồm kiểu và thông điệp popup
     * createdby ndhuy 17.09.2020
     */
    showPopup(val) {
      switch (val.type) {
        case "success":
          this.popUpSuccess(val.message);
          break;
        case "false":
          this.popUpFalse(val.message);
          break;
      }
    },

    /**
     * Hàm hiển thị popup thành công
     * @param message nội dung popup
     * createdby ndhuy 15.09.2020
     */
    popUpSuccess(message) {
      // const loading = this.$loading({
      //   lock: true,
      //   text: "Đang tải",
      //   spinner: "el-icon-loading",
      //   background: "rgba(0,0,0,0.7)",
      // });
      // setTimeout(() => {
      //   loading.close();
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
    /**
     * Gửi tờ khai thuế
     * created by nvbinh2 13.11.2020
     */
    sendDeclaration() {
      this.dataSign = this.arrRowSelect;
      this.registerByMTAX();
    },
    /**
     * Khởi tạo giá trị đối với mixin kí tờ khai
     * created by nvbinh2 11.11.2020
     */
    initDataMixins() {
      this.keyProperty = "DeclarationID";
      this.mode = "Declaration";
      this.nameOfDeclaration = "DeclarationName";
      this.callReloadPage = async function () {
        await this.getListTaxRegister();
      };
    },
  },

  async created() {
    this.initDataMixins();
    this.listStatus = listStatus.TAX_REGISTER;
    this.listMonth = listMonth.LIST_MONTH_ONLY;
    this.listRegisterType = taxRegisterType.TAX_REGISTER_TYPE;
    this.filter.pageSize = this.pageSizes[0];
    await this.getListTaxRegister();
  },

  mounted() {},
  computed: {
    getTitle() {
      return this.$route.meta.title;
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/typo.scss";
@import "@/styles/variables.scss";

.taxRegister {
  height: 100%;
  padding-top: 24px;

  .el-table__body-wrapper {
    height: calc(100% - 52px);
  }

  .page-header {
    margin: 0px 0px 24px 0px;
    height: 40px;

    .page-title h1 {
      line-height: 40px;
      margin: 0px;
    }

    .button-container {
      position: absolute;
      right: 0;
    }
  }

  .filter-bar {
    display: flex;
    margin-bottom: 16px;
    height: 40px;

    .el-input__inner {
      border: none;
    }

    .el-date-editor ::placeholder {
      color: $dark !important;
    }

    .el-select ::placeholder {
      color: $dark !important;
    }

    .btn-for-row-active {
      display: flex;

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
    .btn-download,
    .btn-del {
      margin-right: 16px;

      img {
        width: 20px;
        height: 20px;
      }

      .el-button {
        padding: 7px 9px 11px 9px;
      }
    }

    .el-icon-circle-close {
      font-size: 18px !important;
    }

    .el-date-editor {
      .el-input__prefix {
        width: 25px;
      }

      .el-input__suffix {
        margin-right: 20px;
      }
    }
  }

  .tax-register-table {
    height: calc(100% - 120px);

    .el-tag {
      font-size: 12px;
    }

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

      .el-loading-mask {
        background-color: $white !important;
      }
    }

    .empty-search {
      position: absolute;
      top: 50%;
      left: 50%;
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      transform: translate(-50%, -50%);

      img {
        margin-bottom: 8px;
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

    .img-empty {
      margin-bottom: 16px;
    }
  }

  .tbl-summary .el-table__body-wrapper {
    height: calc(100% - 53px);
  }
}

.dialog-iframe {
  .el-dialog__body {
    padding: 15px 0 0 0;
  }

  .el-dialog__headerbtn:hover {
    background-color: $white;
  }
}

@media only screen and (max-width: 1366px) {
  .taxRegister {
    .empty-search {
      img {
        width: 65%;
      }
    }

    .empty-case {
      .img-empty {
        width: 75%;
      }
    }
  }
}
</style>
