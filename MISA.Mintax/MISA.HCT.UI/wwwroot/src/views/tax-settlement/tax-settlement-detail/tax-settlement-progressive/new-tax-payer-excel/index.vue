<template>
  <div id="import-ex">
    <!-- Dialog nhập dữ liệu người nộp từ excel -->
    <add-dialog
      :id="dialogid"
      :dialogFormVisible="visibleDL"
      @close-dialog="getClose"
      :beforeClose="resetFormDialog"
      :fullscreen="true"
      :showfooter="showfooter"
    >
      <template v-slot:title> </template>
      <template v-slot:body>
        <div class="two-step-container">
          <div class="import-payer-tab">
            <p class="title">{{ title }}</p>
            <TwoStepProgress
              class="tab-bar"
              id="two-step-progress"
              step1Title="Chọn tệp nguồn"
              step2Title="Kiểm tra dữ liệu"
              step3Title="Thành công"
              :isNext="isNextProgress"
            ></TwoStepProgress>
          </div>
          <div
            class="import-payer-main"
            v-if="
              !isNextProgress &&
              !screenChange.isDuplicateTemporary &&
              !screenChange.isDuplicateExcel &&
              !screenChange.isWrongFormat
            "
          >
            <!-- section : Chọn file để upload -->
            <div v-if="!isUploaded" class="import-section">
              <!-- download button -->
              <btn
                class="mr-8 fl btn-download-file"
                :type="'btn-white-icon'"
                :handleClick="downloadSampleFile"
                :name="'Tải file mẫu'"
                style=""
              >
                <template v-slot:icon>
                  <img
                    class="icon"
                    src="@/icons/Component 95 – 1.png"
                  /> </template
              ></btn>

              <!-- section drag to upload -->
              <el-upload
                ref="upload"
                action=""
                class="upload-excel"
                :auto-upload="false"
                :on-change="handleFileChange"
                :limit="1"
                drag
              >
                <!-- accept="application/excel,application/vnd.ms-excel,application/x-excel,application/x-msexcel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" -->
                <!-- <i class="el-icon-upload upload-excel-icon"></i> -->
                <div class="upload-body">
                  <img
                    class="icon icon-upload"
                    src="@/icons/import-excel-icon.png"
                  />
                  <div class="text-drag-excel">
                    <span>Kéo thả file Excels vào đây hoặc</span>
                  </div>
                  <div class="upload-button">
                    <btn
                      class="btn-upload-file fl"
                      :type="'btn-primary-icon'"
                      :name="'Chọn file Excel'"
                    >
                      <template v-slot:icon>
                        <img class="icon" src="@/icons/upload-icon.png" />
                      </template>
                    </btn>
                  </div>
                  <div class="text-noti-duplicate">
                    <span
                      >Ứng dụng sẽ không cho phép cập nhật dữ liệu trùng với dữ
                      liệu được sinh tự động từ bảng tạm tính thuế
                    </span>
                  </div>
                </div>
              </el-upload>
            </div>

            <!-- Section : Xử lý sau khi đã upload -->
            <div v-if="isUploaded" class="import-section">
              <!-- section drag to upload -->
              <div
                class="upload-excel-after"
                :class="{
                  'border-excel-error-class': isUploadFailed,
                  'border-excel-class': !isUploadFailed,
                }"
              >
                <div class="upload-body-after">
                  <div class="upload-icon-wrap">
                    <img
                      @click="cancleUpload"
                      class="icon icon-cancle-upload"
                      src="@/icons/Group_20123.png"
                    />
                    <img
                      class="icon icon-upload"
                      src="@/icons/Group_18744.png"
                    />
                  </div>
                  <div
                    class="name-file-excel"
                    :class="{
                      'text-error-upload': isUploadFailed,
                      'text-upload': !isUploadFailed,
                    }"
                  >
                    <span>{{ fileName }}</span>
                  </div>
                  <div
                    v-if="isUploadFailed"
                    class="name-file-excel"
                    :class="{
                      'text-error-upload': isUploadFailed,
                      'text-upload': !isUploadFailed,
                    }"
                  >
                    <span>{{ uploadError }}</span>
                  </div>
                  <div v-if="!isUploadFailed" class="text-duplicate-data">
                    <span>Dữ liệu đã có trên phụ lục 05-2BK-QTT-TNCN</span>
                  </div>
                  <div v-if="!isUploadFailed" class="row radio-upload">
                    <el-radio v-model="isOverride" :label="true"
                      >Cập nhật</el-radio
                    >
                    <el-radio v-model="isOverride" :label="false"
                      >Bỏ qua</el-radio
                    >
                  </div>
                  <div v-if="!isUploadFailed" class="text-noti-duplicate">
                    <span style="font-size: 11px"
                      >Ứng dụng sẽ không cho phép cập nhật dữ liệu trùng với dữ
                      liệu được sinh tự động từ bảng tạm tính thuế
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div
            class="check-data"
            v-if="
              isNextProgress &&
              !screenChange.isDuplicateTemporary &&
              !screenChange.isDuplicateExcel &&
              !screenChange.isWrongFormat
            "
          >
            <div class="filter-info-data">
              <div class="filter">
                <fieldinput
                  id="check-data-excel"
                  type="input-icon"
                  v-if="fulldata.length > 0"
                  :placeholder="'Tìm kiếm theo tên, mã nhân viên'"
                  classicon="icon-search"
                  width="402px"
                  :valueInput="filterBar.inputSearch"
                  @value="filterBar.inputSearch = $event"
                />
                <div class="button-filter" style="width: 316px">
                  <div class="content-btn">
                    Số dữ liệu trùng lặp bảng quyết toán:
                  </div>
                  <div class="total-row violet">
                    {{ listDuplicateDB.length }}
                    <el-tooltip
                      effect="light"
                      content="Xem chi tiết"
                      placement="top"
                      popper-class="tooltip"
                    >
                      <i
                        class="el-icon-arrow-right"
                        @click="openDuplicatieTemporary"
                      ></i>
                    </el-tooltip>
                  </div>
                </div>
                <div class="button-filter" style="width: 182px">
                  <div class="content-btn">Dữ liệu sai:</div>
                  <div class="total-row red">
                    {{ listWrongFormat.length }}
                    <el-tooltip
                      effect="light"
                      content="Xem chi tiết"
                      placement="top"
                      popper-class="tooltip"
                    >
                      <i
                        class="el-icon-arrow-right"
                        @click="openWrongFormat"
                      ></i>
                    </el-tooltip>
                  </div>
                </div>
                <div class="button-filter" style="width: 293px">
                  <div class="content-btn">
                    Dữ liệu trùng lặp nhau trên Excels:
                  </div>
                  <div class="total-row blue">
                    {{ listDuplicateExcel.length }}
                    <el-tooltip
                      effect="light"
                      content="Xem chi tiết"
                      placement="top"
                      popper-class="tooltip"
                    >
                      <i
                        class="el-icon-arrow-right"
                        @click="openDuplicatieExcel"
                      ></i>
                    </el-tooltip>
                  </div>
                </div>
              </div>
              <div class="info-total">
                <div class="label info-warning">
                  <img class="icon" src="@/icons/Group 20045.png" />
                  Ứng dụng sẽ không cho phép cập nhật dữ liệu trùng với dữ liệu
                  được sinh tự động từ bảng tạm tính thuế
                </div>
                <div class="label total-num">
                  Tổng số dữ liệu mới:
                  <div class="content-total">{{ fulldata.length }}</div>
                </div>
              </div>
            </div>
            <div class="table-data">
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
                :tooltipStatus="tooltipStatus"
                v-if="!isEmpty"
                typeTable="tableExcel"
                :loading="loading"
                :style="setStyle()"
                :pageSizes="pageSizes"
                :total="allFilterData.length"
                :dataTable="importData"
                :listColumn="listColumn"
                :havePagination="true"
                width="100%"
                :tooltip="true"
                :statusFlag="true"
                :statusExcelStatus="excelStatus()"
                @pagination="getFilter"
                typepage="người nộp thuế"
              >
              </tbl>
              <div
                class="mode-btn"
                v-if="importData.length == 0 && isEmpty == true && !loading"
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

          <frame-duplicate-temporary
            v-if="screenChange.isDuplicateTemporary"
            :listColumn="listColumn"
            @backToMainPage="backToMainPage"
          ></frame-duplicate-temporary>

          <frame-duplicate-excel
            v-if="screenChange.isDuplicateExcel"
            :dataDuplicate="listDuplicateExcel"
            @backToMainPage="backToMainPage"
          ></frame-duplicate-excel>

          <frame-wrong-format
            v-if="screenChange.isWrongFormat"
            :listColumn="listColumn"
            :dataWrong="listWrongFormat"
            :errorKey="errorKey"
            @backToMainPage="backToMainPage"
          ></frame-wrong-format>
        </div>
      </template>
      <template v-slot:footer>
        <btn
          name="Hoàn thành"
          v-if="isNextProgress"
          :handleClick="handleImportExcel"
          :disabled="isCompleteBtn"
          type="btn-primary"
          class="ml-10 btn-footer"
        ></btn>
        <!-- Khi đã upload file -->
        <btn
          name="Tiếp tục"
          v-if="!isNextProgress"
          :disabled="!isUploaded || isUploadFailed"
          :handleClick="handleSubmitUpload"
          type="btn-primary"
          class="ml-10 btn-footer"
        ></btn>
        <btn
          name="Hủy"
          v-if="!isUploaded"
          :handleClick="getClose"
          type="btn-primary"
          class="ml-10 btn-footer"
        ></btn>
        <btn
          name="Hủy"
          v-if="isNextProgress"
          :handleClick="backToUploadFile"
          type="btn-grey"
          class="ml-10 btn-footer"
        ></btn>
        <btn
          name="Hủy"
          v-if="isUploaded && !isNextProgress && !isUploadFailed"
          :handleClick="cancleUpload"
          type="btn-grey"
          class="ml-10 btn-footer"
        ></btn>
        <btn
          name="Hủy"
          v-if="isUploaded && !isNextProgress && isUploadFailed"
          :handleClick="cancleUpload"
          type="btn-primary"
          class="ml-10 btn-footer"
        ></btn>
      </template>
    </add-dialog>
  </div>
</template>

<script>
import dialog from "@/components/dialog";
import btn from "@/components/buttons";
import fieldinput from "@/components/inputs";
import TwoStepProgress from "@/components/progress";
import table from "@/components/tables";
import excelStatus from "@/enums/excelStatus.js";
import declarationDetailAPI from "@/api/etax/declaration_settlementDetail_dependent";
import FrameDuplicateTemporary from "@/views/tax-settlement/tax-settlement-detail/tax-settlement-progressive/components/duplicate-temporary.vue";
import FrameDuplicateExcel from "@/views/tax-settlement/tax-settlement-detail/tax-settlement-progressive/components/duplicate-on-excel.vue";
import FrameWrongFormat from "@/views/tax-settlement/tax-settlement-detail/tax-settlement-progressive/components/wrong-format-data.vue";
import SettlementDetailService from "@/api/etax/declaration_settlement_detail.js";
import { filterData } from "@/utils/filter-paginate.js";
export default {
  components: {
    "add-dialog": dialog,
    fieldinput,
    TwoStepProgress,
    btn,
    tbl: table,
    "frame-duplicate-temporary": FrameDuplicateTemporary,
    "frame-duplicate-excel": FrameDuplicateExcel,
    "frame-wrong-format": FrameWrongFormat,
  },
  props: {
    visibleDL: {
      type: Boolean,
      default: true,
    },
  },
  data() {
    return {
      showfooter: true,
      listDuplicateExcel: [],
      listDuplicateDB: [],
      listWrongFormat: [],
      declarationSettelementID: this.$route.params.id,
      pageSizes: [25, 50, 75, 100],
      isUploaded: false,
      urlpost: "",
      dialogid: "new-payer-excel-dialog",
      title: "NHẬP DỮ LIỆU NGƯỜI NỘP THUẾ TỪ FILE EXCELS",
      tableTab: true,
      isNextProgress: false,
      screenChange: {
        isDuplicateTemporary: false,
        isDuplicateExcel: false,
        isWrongFormat: false,
      },
      loading: false,
      isEmpty: false,
      isCompleteBtn: false,
      listColumn: [
        {
          label: "",
          propertyName: "ImportValidState",
          width: "50",
          type: "status-excel",
          align: "center",
        },
        {
          label: "STT",
          propertyName: "Sort",
          width: "",
          minWidth: "50",
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
          minWidth: "150",
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
          minWidth: "140",
          type: "",
        },
        {
          label: "Tổng thu nhập chịu thuế TNCN",
          propertyName: "TotalIncomeTaxable",
          width: "",
          validate: true,
          minWidth: "180",
          type: "decimal",
          alignHeader: "left",
          align: "right",
        },
      ],
      tooltipStatus: [
        {
          value: 4,
          status: "Dữ liệu này sẽ ghi đè vào dữ liệu đã có trên bảng",
        },
        {
          value: 5,
          status: "Dữ liệu này sẽ không được thêm vào bảng quyết toán",
        },
      ],
      fulldata: [],
      isOverride: true,
      fileName: "",
      // fileType: [
      //   "application/excel",
      //   "application/vnd.ms-excel",
      //   "application/x-excel",
      //   "application/x-msexcel",
      //   "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      // ],
      fileType: [".xlsx", ".xls", ".xlt", ".xlsm", ".xltx"],
      file: "",
      importKey: "",
      errorKey: "",
      importData: [],
      allFilterData: [],
      filterBar: {
        pageIndex: 1,
        pageSize: 25,
        inputSearch: "",
      },
      isUploadFailed: false,
      uploadError: "",
    };
  },
  methods: {
    resetFormDialog() {
      this.getClose();
      this.backToUploadFile();
      this.cancleUpload();
    },
    getClose() {
      this.$emit("closeDialog", false);
    },
    setStyle() {
      return { height: "calc(100vh - 225px - 25px)" };
    },
    excelStatus() {
      return excelStatus.EXCEL_STATUS;
    },
    openDuplicatieTemporary() {
      this.screenChange.isDuplicateTemporary = true;
    },
    openWrongFormat() {
      this.screenChange.isWrongFormat = true;
    },
    openDuplicatieExcel() {
      this.screenChange.isDuplicateExcel = true;
    },
    backToMainPage(val) {
      if (!val) {
        this.screenChange.isDuplicateTemporary = false;
        this.screenChange.isDuplicateExcel = false;
        this.screenChange.isWrongFormat = false;
        this.filterBar.pageSize = 15;
        this.filterBar.pageIndex = 1;
      }
    },
    backToUploadFile() {
      this.screenChange.isDuplicateTemporary = false;
      this.screenChange.isDuplicateExcel = false;
      this.screenChange.isWrongFormat = false;
      this.isNextProgress = false;
      this.fulldata = [];
      this.listDuplicateExcel = [];
      this.listDuplicateDB = [];
      this.listWrongFormat = [];
      this.filterBar.inputSearch = "";
    },
    finishImport() {
      this.isUploaded = false;

      this.screenChange.isDuplicateTemporary = false;
      this.screenChange.isDuplicateExcel = false;
      this.screenChange.isWrongFormat = false;
      this.isNextProgress = false;
      this.fulldata = [];
      this.listDuplicateExcel = [];
      this.listDuplicateDB = [];
      this.listWrongFormat = [];
      this.$emit("closeDialog", false);
      this.$emit("fetchData", true);
    },

    /**
     * Hàm hủy upload file
     * Created by : NQKHAI (29/10/2020)
     */
    cancleUpload() {
      this.isUploadFailed = false;
      this.isUploaded = false;
      this.$refs.upload.clearFiles();
    },

    /**
     * Hàm xử lý sự kiện khi upload file lên hoặc file có sự thay đổi
     * Kiểm tra kích thước, định dạng file hợp lệ
     * Created By : NQKHAI(29/10/2020)
     */
    handleFileChange(file) {
      const isLt5M =
        file.size / 1024 / 1024 < excelStatus.IMPORT_FILE_SIZE.VALUE;
      const fileName = file.raw.name;
      const isExcelType = this.validateExcelType(fileName);
      this.fileName = file.name;
      this.isUploaded = true;

      if (!isLt5M) {
        this.isUploadFailed = true;
        this.uploadError = excelStatus.EXCEL_IMPORT_STATUS[3].NAME;
      } else if (!isExcelType) {
        this.isUploadFailed = true;
        this.uploadError = excelStatus.EXCEL_IMPORT_STATUS[5].NAME;
      } else {
        this.file = file;
        //this.isUploaded = true;
      }
    },

    /**
     * Hàm xác thực file nhập vào có đúng định dạng excel không
     * Created By : NQKHAI(07/11/2020)
     */
    validateExcelType(fileName) {
      if (fileName.length > 0) {
        var isValid = false;
        for (var i = 0; i < this.fileType.length; i++) {
          var type = this.fileType[i];
          if (
            fileName
              .substr(fileName.length - type.length, type.length)
              .toLowerCase() == type.toLowerCase()
          ) {
            isValid = true;
            break;
          }
        }
        if (!isValid) {
          return false;
        }
        return true;
      }
    },

    /**
     * Hàm xử lý sự kiện import file
     * Created By : NQKHAI (31/10/2020)
     * Modified by bvbao (4/11/2020)
     */
    async handleSubmitUpload() {
      this.isNextProgress = true;
      this.loading = true;
      this.isCompleteBtn = true;
      let formData = new FormData();
      formData.append("formFile", this.file.raw);
      const url = this.getExcelUrl("reader");
      try {
        var res = await SettlementDetailService.postUpdoadExcelFile(
          url,
          formData
        );

        if (
          res.data.Success == true &&
          res.data.Data &&
          res.data.MISACode == 200
        ) {
          const importKey = res.data.Data.ImportKey;
          this.importKey = importKey;
          this.errorKey = res.data.Data.ErrorKey;
          this.importData = res.data.Data.ImportData.map((item) => {
            var IndividualResidentData =
              item.IndividualResident == excelStatus.IndividualResident.No
                ? "Không"
                : "Có";
            return {
              ...item,
              IndividualResidentData,
            };
          });

          // data response
          this.fulldata = this.importData;
          // Lưu full data vào store
          this.$store.dispatch("excelData/addListExcels", this.fulldata);
          this.getData();

          // Lọc ra những data bị trùng trong DB
          this.listDuplicateDB = this.fulldata.filter((item) => {
            let test = item.ImportValidError.filter(
              (temp) =>
                temp.Code ==
                excelStatus
                  .findInArray("DuplicateInDatabase")
                  .find((x) => x == temp.Code)
            );
            return test.length > 0 ? true : false;
          });

          this.$store.dispatch(
            "excelData/addListDuplicateDB",
            this.listDuplicateDB
          );

          // Lọc ra những data bị có dữ liệu bị sai định dạng
          this.listWrongFormat = this.fulldata.filter((item) => {
            let test = item.ImportValidError.filter(
              (temp) =>
                temp.Code ==
                excelStatus
                  .findInArray("WrongFormat")
                  .find((x) => x == temp.Code)
            );
            return test.length > 0 ? true : false;
          });
          this.$store.dispatch(
            "excelData/addListErrorExcels",
            this.listWrongFormat
          );

          var duplicateinExcel = this.fulldata.filter((item) => {
            let test = item.ImportValidError.filter(
              (temp) =>
                temp.Code ==
                excelStatus
                  .findInArray("DuplicateInfile")
                  .find((x) => x == temp.Code)
            );
            return test.length > 0 ? true : false;
          });
          duplicateinExcel.forEach((element) => {
            element.ImportValidError.forEach((itemDup) => {
              if (itemDup.Code == excelStatus.findInArray("DuplicateInfile")) {
                var index = this.fulldata.find(
                  (x) => x.DeclarationSettlementDetailId == itemDup.ExtendInfo
                );
                if (index) {
                  if (
                    !this.listDuplicateExcel.find(
                      (x) =>
                        x.DeclarationSettlementDetailId ==
                        index.DeclarationSettlementDetailId
                    )
                  )
                    this.listDuplicateExcel.push(index);
                }
              }
            });
          });
          duplicateinExcel.forEach((element) => {
            this.listDuplicateExcel.push(element);
          });
          this.$store.dispatch(
            "excelData/addListDuplicateExcels",
            this.listDuplicateExcel
          );

          this.isNextProgress = true;
          this.loading = false;
          this.isCompleteBtn = false;

          this.$_Notification.success(
            excelStatus.RESPONSE_STATUS.SUCCESS,
            excelStatus.EXCEL_IMPORT_STATUS[4].NAME
          );
        } else {
          this.isNextProgress = false;
          this.$_Notification.error(
            excelStatus.RESPONSE_STATUS.FAILED,
            excelStatus.EXCEL_IMPORT_STATUS[0].NAME
          );
        }
      } catch (e) {
        this.isNextProgress = false;
        this.$_Notification.error(
          excelStatus.RESPONSE_STATUS.FAILED,
          excelStatus.EXCEL_IMPORT_STATUS[0].NAME
        );
      }
    },

    /**
     * Hàm xử lý import file excel
     * Lưu data vào bảng phụ lục 05-2BK
     * Created By : NQKHAI (4/11/2020)
     */
    async handleImportExcel() {
      try {
        const urlImport = this.getExcelUrl("import");
        var resImport = await SettlementDetailService.importExcelFile(
          urlImport
        );
        if (resImport.data.Success == true && resImport.data.MISACode == 200) {
          this.$_Notification.success(
            excelStatus.RESPONSE_STATUS.SUCCESS,
            excelStatus.EXCEL_IMPORT_STATUS[1].NAME
          );

          this.finishImport();
        } else {
          // this.isNextProgress = false;
          this.$_Notification.error(
            excelStatus.RESPONSE_STATUS.FAILED,
            excelStatus.EXCEL_IMPORT_STATUS[2].NAME
          );
        }
      } catch (e) {
        this.isNextProgress = false;
        this.$_Notification.error(
          excelStatus.RESPONSE_STATUS.FAILED,
          excelStatus.EXCEL_IMPORT_STATUS[2].NAME
        );
      }
    },

    /**
     * Hàm xử lý sự kiện download file excel mẫu
     * Created by bvbao (2/11/2020)
     */
    async downloadSampleFile() {
      const url = this.getDownloadUrl("settlementdetail05_2bk");
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
          a.download = excelStatus.FILE_NAME[0].NAME;
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
    /**
     * Lấy đường dẫn request để download file excel mẫu
     * Created by bvbao (2/11/2020)
     */
    getDownloadUrl(mode) {
      var options = {};
      return SettlementDetailService.getURLDownloadExcel(mode, options);
    },

    /**
     * Lấy đường dẫn request đẩy file excel lên server
     * created by nvbinh2 30.10.2020
     */
    getExcelUrl(mode) {
      var options = {
        declarationSettelementID: this.declarationSettelementID,
        overrideData: this.isOverride,
        cancellationToken: null,
        importKey: this.importKey,
      };
      return SettlementDetailService.getURLExcel(mode, options);
    },

    /**
     * Hàm lấy dữ liệu danh sách người nộp thuế (phân trang) từ store
     * Created By : NQKHAI(3/11/2020)
     * Modified by bvbao(4/11/2020)
     */
    getData() {
      var data = this.$store.getters.listExcel;
      filterData();
      if (data != null) {
        let pageSize = this.filterBar.pageSize;
        let pageIndex = this.filterBar.pageIndex;
        let search = this.filterBar.inputSearch;
        //filter theo input search
        this.allFilterData = data.filter(
          (data) =>
            data.EmployeeCode.toLowerCase().includes(search.toLowerCase()) ||
            data.FullName.toLowerCase().includes(search.toLowerCase()) ||
            data.CitizenIdentityNo.toLowerCase().includes(
              search.toLowerCase()
            ) ||
            data.TaxNo.toLowerCase().includes(search.toLowerCase())
        );

        this.importData = this.allFilterData
          .skip((pageIndex - 1) * pageSize)
          .limit(pageSize);

        this.isEmpty = false;
      } else {
        this.isEmpty = true;
        this.importData = [];
      }
    },

    /**
     * Hàm xử lý sự kiện phân trang
     * Khi thay đổi trang hiện tại hoặc pageSize
     * Created By : NQKHAI(3/11/2020)
     */
    getFilter(val) {
      this.filterBar.pageSize = val.limit;
      this.filterBar.pageIndex = val.page;
      this.getData();
    },
  },
  watch: {
    importData: {
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
    screenChange: {
      handler(val) {
        if (
          val.isDuplicateTemporary ||
          val.isDuplicateExcel ||
          val.isWrongFormat
        ) {
          this.showfooter = false;
        } else {
          this.showfooter = true;
        }
      },
      deep: true,
    },
  },
  created() {
    this.url = declarationDetailAPI.postImportExcelFile(
      true,
      this.$route.params.id
    );
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
#import-ex {
  .el-dialog__footer {
    text-align: right !important;
    padding: 12px 24px 12px 24px;
    height: 60px;
    background-color: $bg-color-2;
    border-bottom-right-radius: 5px;
    border-bottom-left-radius: 5px;
    position: absolute;
    bottom: 0;
    right: 0;
    width: 100%;
  }
  .el-upload-list__item .el-icon-close {
    display: none;
  }
  .el-upload-list__item-name {
    display: none;
  }
  .el-dialog__wrapper .el-dialog {
    border-radius: 0px;
  }

  #two-step-progress {
    justify-content: center;
  }
  #new-payer-excel-dialog {
    border-radius: 5px;
    width: calc(100% - 48px);
    height: calc(100% - 25px);
    margin: 25px 24px 0px 24px;

    .el-dialog__header {
      padding: 0;
    }

    .el-dialog__body {
      height: calc(100% - 60px);
      padding: 0;

      .two-step-container {
        height: 100%;

        .import-payer-tab {
          height: 67px;
          display: flex;
          justify-content: center;
          align-items: center;

          .title {
            font-size: 15px;
            font-weight: bold;
            position: absolute;
            left: 24px;
            margin: 0;
          }
          .tab-bar {
            height: 38px;
            position: absolute;
            top: 16px;
            z-index: 1;
          }
        }

        .import-payer-main {
          background-color: $background-grey;
          height: calc(100% - 67px);
          position: relative;
          .import-section {
            background-color: $white;
            position: absolute;
            top: 50%;
            left: 50%;
            width: calc(100% - 524px);
            height: calc(100% - 50px);
            transform: translate(-50%, -50%);
            border-radius: 5px;

            .btn-download-file {
              position: relative;
              margin-right: 26px;
              margin-top: 26px;
              border: 1px;
              border-color: $bg-grey;
            }

            .upload-excel .el-upload-dragger {
              border: 2px dashed $border-opacity-60;

              border-radius: 6px;
              height: calc(100% - 93px - 25px);
              width: calc(100% - 50px);
              top: 50%;
              left: 50%;
              transform: translate(-50%, -50%);
              margin-top: 33px;
              position: absolute;
              .upload-body {
                top: 50%;
                left: 50%;
                width: 100vh;
                transform: translate(-50%, -50%);
                position: absolute;

                .text-drag-excel {
                  margin-top: 44px;
                }
                .upload-button {
                  display: flex;
                  flex-direction: column-reverse;
                  margin-top: 25px;
                }
                .text-noti-duplicate {
                  margin-top: 25px;
                  word-break: break-all;
                  span {
                    word-break: break-word;
                  }
                }
              }
            }

            .upload-excel-after {
              // border: 2px dashed $border-opacity-60;
              border-radius: 6px;
              height: calc(100% - 50px);
              width: calc(100% - 50px);
              top: 50%;
              left: 50%;
              transform: translate(-50%, -50%);
              position: absolute;

              .upload-body-after {
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                position: absolute;
                align-items: center;
                display: flex;
                flex-direction: column;
                width: 100vh;

                .upload-icon-wrap {
                  position: relative;
                  padding: 8px;
                }
                .icon-cancle-upload {
                  position: absolute !important;
                  right: 0 !important;
                  top: 0 !important;
                  cursor: pointer;
                }
                .name-file-excel {
                  span {
                    font-size: 15px;
                  }
                  margin-top: 15px;
                  // color: $hover-primary;
                }
                .text-duplicate-data {
                  margin-top: 42px;
                }
                .radio-upload {
                  margin-top: 28px;
                }
                .text-noti-duplicate {
                  color: $red;
                  margin-top: 25px;
                  word-break: break-all;
                  display: flex;

                  span {
                    word-break: break-word;
                  }
                }
              }
            }
          }
        }
      }
    }

    .check-data {
      background-color: $background-grey;
      padding-left: 26px;
      padding-right: 26px;
      .filter-info-data {
        padding-top: 12px;
        .filter {
          display: flex;
          height: 43px;
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
              align-items: center;
            }
            .total-row {
              font-size: 16px;
              height: 100%;
              margin-left: auto;
              display: flex;
              align-items: center;
              font-weight: bold;
              .el-icon-arrow-right {
                width: 40px;
                height: 100%;
                display: flex;
                align-items: center;
                justify-content: center;
                font-weight: bold;
                color: $dark;
                cursor: pointer;
              }
            }
            .violet {
              color: $violet-duplicate;
            }
            .red {
              color: $red-wrong;
            }
            .blue {
              color: $blue-duplicate;
            }
          }
        }
        .info-total {
          height: 40px;
          display: flex;
          align-items: center;
          .info-warning {
            font-style: italic;
            font-family: $_fontregular;
            color: $light-seconds;
            display: flex;
            align-items: center;
            .icon {
              margin-right: 10px;
            }
          }
          .total-num {
            margin-left: auto;
            display: flex;
            .content-total {
              margin-left: 30px;
              font-weight: bold;
              font-size: 15px;
            }
          }
        }
      }
      .table-data .tableData .el-table__body-wrapper {
        height: calc(100% - 53px);
        overflow-x: inherit;
        overflow-y: initial;
      }
    }
    .el-input__inner {
      height: 43px;
    }
    .pagination-container .el-pagination .el-select .el-input .el-input__inner {
      height: 36px;
    }
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
}
.border-excel-class {
  border: 2px dashed $border-opacity-60;
}
.border-excel-error-class {
  border: 2px dashed $red;
}
.text-error-upload {
  color: $red;
}
.text-upload {
  color: $hover-primary;
}
</style>