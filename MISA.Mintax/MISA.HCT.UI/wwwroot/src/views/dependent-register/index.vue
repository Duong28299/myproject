<template>
  <div class="dependent-person">
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
    MỌI THẮC MẮC LIÊN HỆ VDTHANG -->
    <!-- Page title -->
    <el-row class="page-header">
      <el-col :span="24">
        <div class="page-title d-flex">
          <h1 style="height: 40px; line-height: 40px">{{ getTitle }}</h1>
          <m-btn
            type="btn-white2-onlyicon-big"
            tooltip="Làm mới"
            positionTooltip="left"
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
    <!-- <div v-if="dataTable.length>0" class="main">
        
    </div>-->
    <el-row
      class="filter-bar"
      justify="space-between"
      v-if="!isEmpty && !loading"
    >
      <el-col :span="20">
        <el-row :gutter="16" v-if="!isCheckRow">
          <el-col :span="6">
            <field-input
              type="input-icon"
              classicon="icon-search"
              :placeholder="'Tìm kiếm'"
              @value="getValueProfileName"
              :valueInput="filterBar.keyword"
              :clearable="true"
            ></field-input>
          </el-col>
          <el-col :span="4">
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
          <el-col :span="4">
            <date-picker selectID="year-register" :value="filterBar.year" formatInput="yyyy" Valueformat="yyyy" type="year" :size="'medium'" :placeholder="'Năm đăng kí'" width="100%" @getValue="getValueYearRegis" :borderType="'none-border-white'"></date-picker>
          </el-col>
          <el-col :span="4">
            <input-dropdown
              selectID="month"
              :list="listMonth"
              type="select-border-color"
              label="MONTH_NAME"
              field="MONTH"
              :placeholder="'Tháng lập hồ sơ'"
              @value="getValueMonthRegis"
              :defaultNumber="filterBar.month"
              :clearable="true"
            ></input-dropdown>
          </el-col>
        </el-row>
        <el-row :gutter="16" v-if="isCheckRow">
          <div class="group-btn-delete">
            <m-btn
              type="btn-white3-onlyicon"
              tooltip="Kí nộp"
              style="margin-right: 16px"
              :disabled="disabledDeleteAndSign"
              :handleClick="sendDeclaration"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group 18667@2x.png" />
              </template>
            </m-btn>
            <m-btn
              style="margin-right: 16px"
              :type="'multi-button4'"
              :tooltip="'Tải về'"
              :dataDropdown="arrayDownload"
              :handleClick="download"
              positionTooltip="top"
              :disabled="disabledDeleteAndSign"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Component 95 – 1.png" />
              </template>
            </m-btn>
            <m-btn type="btn-white3-onlyicon" :iconel="'el-icon-delete'" tooltip="Xóa" :handleClick="openPopup"  :disabled="disabledDeleteAndSign">
              type="btn-white3-onlyicon"
              :iconel="'el-icon-delete'"
              tooltip="Xóa"
              :handleClick="openPopup"
              :disabled="disabledDeleteAndSign"
            >
              <template v-slot:icon></template>
            </m-btn>
            <div
              style="margin-left: 16px"
              class="deselect-all"
              @click="clearSelection"
            >
              Bỏ chọn {{ rowselect.length }} hồ sơ
            </div>
          </div>
        </el-row>
      </el-col>
      <el-col :span="4" v-if="!isCheckRow">
        <m-btn
          type="btn-primary-icon"
          name="Tạo mới hồ sơ"
          :handleClick="openAddDialog"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 17812.png" />
          </template>
        </m-btn>
      </el-col>
    </el-row>
    <el-row class="data-table">
      <div v-if="loading" v-loading="loading"></div>
      <tbl-dependent-person
        v-if="!loading && !isEmpty"
        ref="dependent-person"
        :listColumn="listColumn"
        :dataTable="getDataTable"
        :haveCheck="haveCheck"
        :pageSizes="pageSizes"
        :havePagination="true"
        :total="numberOfRecord"
        :layoutPaging="layoutPaging"
        :chipArray="chipArray"
        :tooltip="true"
        :listaction="listaction"
        :destiArrStatus="destiArrStatus"
        destiStatus="DeclarationStatus"
        @chip-emit="getObjectEmit"
        typepage="tờ khai"
        @pagination="pagination"
        @selection="getSelection"
        @handleSelectionChange="getRowsSelection"
        @handleSelectAll="handleSelectAll"
        @handleRowDBLClick="handleRowDBLClick"
        :loading="false"
        @row-action="getRowAction"
      ></tbl-dependent-person>
    </el-row>
    <!-- Content -->
    <!--IF DATA Empty -->
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
        src="@/assets/images/Group 19459.png"
        alt="not have data"
        style="position: relative"
      />
      <span>Không tìm thấy kết quả phù hợp</span>
    </div>

    <div class="main-empty" v-if="isEmpty">
      <img src="@/assets/images/Group 19459.png" />
      <m-btn
        type="btn-primary-icon"
        name="Tạo mới hồ sơ"
        :handleClick="openAddDialog"
      >
        <template v-slot:icon>
          <img class="icon" src="@/icons/Group 17812.png" />
        </template>
      </m-btn>
    </div>
    <add-dialog
      :visiableDL="isDialogOpen"
      :isAdd="isAddDialog"
      :DeclarationID="DeclarationID"
      @reload="reloadTable"
      @closeDialog="isDialogOpen = $event"
    />
    <!--/IF DATA Empty -->
  </div>
</template>

<script>
import Button from "@/components/buttons";
import fieldinput from "@/components/inputs";
import Dropdown from "@/components/select";
import DatePicker from "@/components/date-time-pickers";
import Table from "@/components/tables";
import registerType from "@/enums/registerType.js";
import DeclarationStatus from "@/enums/chipEnum.js";
import actionTable from "@/enums/actionTable.js";
import listMonth from "@/enums/monthOfYear.js";
import AddDialog from "./components/add-new-profile.vue";
import declaration from "@/api/etax/declaration.js";
import message from "@/enums/message.js";
import signDeclaration from "@/mixins/signDeclaration"
export default {
  name: "DependentRegister",
  components: {
    "m-btn": Button,
    "field-input": fieldinput,
    "input-dropdown": Dropdown,
    "date-picker": DatePicker,
    "tbl-dependent-person": Table,
    "add-dialog": AddDialog,
  },
  mixins: [signDeclaration],
  data() {
    return {
      // mảng lưu trữ trạng thái để ẩn hiện button xem sửa, mã 2 3 4 5 tương ứng với các declartionStatus
      destiArrStatus: [2, 3, 10, 11, 12],
      disabledDeleteAndSign: false,
      isDialogOpen: false,
      dialogVisible: false,
      isAddDialog: null, //bien dong/mo form,
      rowselect: 0,
      DeclarationID: "",
      ifSrc: "",
      filterBar: {
        keyword: "", //tên hồ sơ
        subscriptionType: "", //loại đăng ký
        declarationStatus: null, //trạng thái
        year: new Date().getFullYear().toString(), //năm dăng ký
        month: null, //tháng đăng ký
        pageIndex: 1,
        pageSize: 15,
        groupMode: "DependentRegisterMode",
        groupType: 0,
      },
      numberRecordDeleted: 0, //số bản ghi đã xóa
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
          label: "Lần đăng ký",
          propertyName: "NumberOfDeclaration",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "NumberOfDeclaration",
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
        },
        {
          label: "Ngày nộp",
          propertyName: "SendDate",
          width: "",
          minWidth: "120",
          align: "center",
          type: "date",
          prop: "SendDate",
          sortable: true,
        },
        {
          label: "Trạng thái",
          propertyName: "DeclarationStatus",
          width: "120",
          minWidth: "120",
          align: "left",
          type: "chip",
          prop: "DeclarationStatus",
          sortable: true,
        },
      ],
      dataTable: [],
      haveCheck: true,
      pageSizes: [15, 25, 50, 100],
      numberOfRecord: 0,
      layoutPaging: "prev, pager, next, sizes",
      chipArray: "TAX_REGISTER",
      isEmpty: false,
      select: [],
      isCheckRow: false,
      listaction: [],
      listRegisterType: [],
      listStatus: [],
      listMonth: [],
      listIDSelect: [],
      loading: true,
      TaxCode: "0101243150999",
    };
  },
  methods: {
    // Hàm đóng reset src iframe
    handleClose(done) {
      setTimeout(() => {
        this.ifSrc = "";
      }, 500);
      done();
    },
    //Lấy tên hồ sơ
    getValueProfileName(val) {
      this.filterBar.keyword = val;
    },

    //Lấy tên hồ sơ
    getValueSubscriptionType(val) {
      this.filterBar.subscriptionType = val;
    },

    getValueStatus(val) {
      this.filterBar.declarationStatus = val;
    },

    getValueYearRegis(val) {
      this.filterBar.year = val;
    },

    getValueMonthRegis(val) {
      this.filterBar.month = val;
    },

    refreshTable() {
      this.loading = true;
      this.filterBar.keyword = "";
      this.filterBar.declarationStatus = null;
      this.filterBar.year = new Date().getFullYear().toString();
      this.filterBar.month = null;
      (this.filterBar.pageIndex = 1),
        (this.filterBar.pageSize = 15),
        this.getData();
    },

    reloadTable(val) {
      if (val) this.refreshTable();
    },
    /**
     * Bỏ chọn tất cả các dòng trong bảng
     * createdby tcduong 16.9.2020
     */
    clearSelection() {
      this.$refs["dependent-person"].clearSelection();
    },

    pagination(val) {
      this.filterBar.pageSize = val.limit;
      this.filterBar.pageIndex = val.page;
    },

    getSelection() {},
    // Hàm gọi service lấy src cho iframe
    getObjectEmit(val) {
      const taxCode = val.OrganizationTaxNo;
      const submissionCode = val.SubmissionCode
        ? val.SubmissionCode
        : "11420201001895800";
      const res = declaration.getPreviewNotification(taxCode, submissionCode);
      this.dialogVisible = true;
      this.ifSrc = res;
    },

    MutiSign() {
      this.registerByMTAX();
    },

    /**
     * Xóa tất cả
     * createdby tcduong 14.9.2020
     */
    async deleteSelectedRow() {
      this.numberRecordDeleted = 0; //gán lại số bản ghi đã xóa thành công = 0
      var checkProfileStatus = this.select.some(
        (item) => item.DeclarationStatus !== 1
      );
      if (checkProfileStatus) {
        var res = await declaration.deleteDeclaration(this.listIDSelect);
        if (res.data.Success && res.data.MISACode == 200) {
          this.getData(); //load lại data
          this.numberRecordDeleted = this.listIDSelect.length;
          var numberRecordDeletedFail =
            this.select.length - this.numberRecordDeleted; //số hồ sơ xóa không thành công
          var contentNotifi = `Đã xóa thành công ${this.numberRecordDeleted}/${this.select.length} bản ghi.  Xóa thất bại ${numberRecordDeletedFail}/${this.select.length} hồ sơ`;
          this.$_Notification.success("Thông báo", contentNotifi);
        }
      } else {
        var respon = await declaration.deleteDeclaration(this.listIDSelect);
        if (respon.data.Success && respon.data.MISACode == 200) {
          this.getData(); //load lại data
          this.$_Notification.success("Thông báo", "Xóa hồ sơ thành công");
        }
      }
    },
    /**
     * Hiển thị popup cảnh báo khi xóa dữ liệu
     * craetedby tcduong 14.9.2020
     */
    openPopup() {
      var title = "Xóa hồ sơ đăng ký người phụ thuộc";
      var contentPopup = "";
      if (this.select.length == 1) {
        contentPopup = message.Delete.messageWarningDel(
          this.select[0].DeclarationName
        );
      } else {
        contentPopup =
          "Các hồ sơ được chọn sẽ bị xóa khỏi hệ thống. Bạn có chắc chắn muốn xóa không?";
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
    },
    /**
     * Hàm bắt sự kiện click vào cột checkbox
     * @param rows mảng các hàng được chọn
     * createdby tcduong 14.9.2020
     */
    getRowsSelection(rows) {
      this.rowselect = rows;
      this.disabledDeleteAndSign = false;
      if (rows.length > 0) {
        var checkProfileStatus = rows.every(
          (item) => item.DeclarationStatus !== 1
        ); //kiểm tra xem tất cả các dòng được chọn có declarationStatus != 1 không?
        if (checkProfileStatus) {
          this.isCheckRow = true;
          this.disabledDeleteAndSign = true;
        } else if (!checkProfileStatus) {
          var listId = rows.map(function (item) {
            if (item.DeclarationStatus == 1) {
              return item.DeclarationID;
            }
          });
          var filterId = listId.filter((item) => item != undefined);
          this.listIDSelect = [...filterId];
          this.select = rows;
          this.isCheckRow = true;
        }
      } else this.isCheckRow = false;
    },
    async getRowAction(val) {
      if (val.value == 1 && val.row.DeclarationStatus == 1) {
        this.listIDSelect = [];
        this.listIDSelect.push(val.row.DeclarationID);
        this.select = [];
        this.select.push(val.row);
        this.openPopup();
      }
      if (val.value == 0 && val.row.DeclarationStatus == 1) {
        this.isAddDialog = false;
        this.DeclarationID = val.row.DeclarationID;
        this.handleRowDBLClick(val.row);
      }
    },
    handleSelectAll() {},

    /**
     * Chuyển sang trang deatil khi doubleclick vào 1 dòng
     * @param val object chứa dữ liệu của 1 bản ghi
     * craetedby tcduong 14.9.2020
     * modify ltuan 091020
     * loại bỏ việc sử dụng seesion stoaage
     */
    handleRowDBLClick(val) {
      this.$router.push({
        name: "DependentRegisterDetail",
        params: { id: val.DeclarationID },
      });
    },

    /**
     * Mở dialog thêm mới
     * craetedby tcduong 14.9.2020
     */
    openAddDialog() {
      this.isAddDialog = true;
      this.DeclarationID = "";
      this.isDialogOpen = true;
    },

    /**
     * thực hiện gọi service đẻ lấy dánh sách hồ sơ đăng ký
     * createdby tcduong 15.9.2020
     */
    async getData() {
      this.loading = true;
      var res = await declaration.getDeclaration(this.filterBar);
      if (res.data.Success && !res.data.Data.IsEmpty) {
        this.mapdata(res.data.Data.DeclarationLists);
        this.isEmpty = res.data.Data.IsEmpty;
        this.numberOfRecord = res.data.Data.TotalDeclaration;
        this.loading = false;
      } else {
        this.isEmpty = true;
        this.loading = false;
        this.dataTable = [];
        this.numberOfRecord = 0;
      }
    },

    /**
     * map dữ liệu vào table
     * @param val mảng dữ liệu lấy từ service
     * createdby tcduong 15.9.2020
     */
    mapdata(val) {
      var datas = [];
      val.forEach((item) => {
        var data = {};
        data.OrganizationID = item["OrganizationID"];
        data.DeclarationName = item["DeclarationName"];
        data.PeriodYear = item["PeriodYear"];
        data.NumberOfDeclaration = item["NumberOfDeclaration"];
        data.SendDate = item["SendDate"] ? item["SendDate"] : "";
        data.DeclarationStatus = item["DeclarationStatus"];
        data.DeclarationID = item["DeclarationID"];
        data.CreatedDate = item["CreatedDate"] ? item["CreatedDate"] : "";
        data.SubmissionCode = item["SubmissionCode"];
        data.OrganizationTaxNo = item["OrganizationTaxNo"];
        datas.push(data);
      });
      this.dataTable = [...datas];
      this.loading = false;
    },

    /**
     * Gửi tờ khai thuế
     * created by nvbinh2 11.11.2020
     */
    async sendDeclaration() {
      this.dataSign = this.select;
      await this.registerByMTAX();
    },
    /**
     * Khởi tạo giá trị đối với mixin kí tờ khai
     * created by nvbinh2 11.11.2020
     */
    initDataMixins() {
      this.keyProperty = "DeclarationID";
      this.mode = "Declaration";
      this.nameOfDeclaration = "DeclarationName";
      this.callReloadPage = async function(){ await this.getData()};
    },
  },
  computed: {
    checkData() {
      return this.dataTable.length;
    },

    getDataTable() {
      return this.dataTable;
    },
    getTitle() {
      return this.$route.meta.title;
    },
  },
  created() {
    this.initDataMixins();
    this.listRegisterType = registerType.REGISTER_TYPE;
    this.listStatus = DeclarationStatus.TAX_REGISTER;
    this.listaction = actionTable.TAX_REGISTER;
    this.listMonth = listMonth.LIST_MONTH;
    this.getData();
  },
  watch: {
    filterBar: {
      handler() {
        this.getData();
      },
      deep: true,
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/typo.scss";
@import "@/styles/variables.scss";
@import "@/styles/main-page.scss";
.dependent-person {
  @include main-page;
}
</style>
