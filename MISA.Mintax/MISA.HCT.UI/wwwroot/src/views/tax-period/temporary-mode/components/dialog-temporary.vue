<template>
  <div class="add-temporary">
    <dialogel
      :loading="loadingDialog"
      id="add-temporary-dialog"
      :titleDialog="titleTemporary"
      :dialogFormVisible="dialog"
      @close-dialog="cancelInsert"
      close-on-press-Escape="true"
      xl="4,5"
      lg="4,5"
      md="4,5"
      sm="2,3"
    >
      <template v-slot:body>
        <el-form class="main" ref="form" :model="form">
          <!-- Loại thu nhập  -->
          <el-form-item>
            <div class="label">Chọn loại thu nhập</div>
            <select-dropdown
              :list="listTaxType"
              type="label-none"
              label="NAME"
              field="VALUE"
              :disabled="isDisable"
              :defaultNumber="selectTaxType.incomeType"
              :value="selectTaxType.incomeType"
              customSelect="custom-select"
              placeholder="Chọn bảng tính"
              @value="getTaxType"
            ></select-dropdown>
          </el-form-item>

          <!-- // Component chọn kì kê khai -->
          <el-form-item>
            <div class="label">Chọn kỳ tính thuế</div>
            <div class="d-flex">
              <el-col :span="12" class="mr-8">
                <select-dropdown
                  :list="listPeriodType"
                  type="label-none"
                  label="NAME"
                  field="VALUE"
                  size="small"
                  :disabled="isDisable"
                  :defaultNumber="getTemporaryTaxType"
                  @value="getPeriod"
                  placeholder="Chọn kỳ tính thuế"
                ></select-dropdown>
              </el-col>
              <!-- giá trị tháng năm -->
              <el-col :span="12">
                <period-picker
                  v-if="selectTaxType.temporaryTaxType == 1"
                  type="month"
                  formatInput="MM-yyyy"
                  placeholder="Chọn tháng"
                  size="medium"
                  @getValue="getTime"
                  Valueformat="MM-yyyy"
                  :value="formatMonth(selectTaxType.time)"
                  :width="'100%'"
                  :borderType="'none-border-white'"
                ></period-picker>
                <quarter-picker
                  v-if="selectTaxType.temporaryTaxType == 2"
                  :value="formatQuarter(selectTaxType.time)"
                  @getValue="getTime"
                  :width="'100%'"
                ></quarter-picker>
              </el-col>
            </div>
          </el-form-item>

          <!-- Tên bảng tính thuế-->
          <el-form-item
            prop="tableName"
            :rules="{
              required: true,
              message: 'Bạn cần nhập thông tin này',
              trigger: ['blur', 'change'],
            }"
            label="Tên bảng tính"
            :show-message="hover"
          >
            <form-input
              id="tableName"
              type="field-input"
              @value="getTableNameFromInput"
              @hover="hover = $event"
              :valueInput="form.tableName"
              placeholder="Nhập tên bảng tính"
            >
            </form-input>
          </el-form-item>
        </el-form>
      </template>

      <template v-slot:footer class="dialog-footer">
        <el-button @click="cancelInsert" class="mr-10 btn-grey"
          >Hủy bỏ</el-button
        >
        <el-button
          class="ml-11 btn-primary"
          @click="insertTemporayTax"
          :disabled="isDisableButtonAdd"
          >Tạo mới</el-button
        >
      </template>
    </dialogel>
  </div>
</template>

<script>
import dialogel from "@/components/dialog";
import formInput from "@/components/inputs";
import taxType from "@/enums/taxType.js";
import periodType from "@/enums/periodType.js";
import selectDropdown from "@/components/select";
import Dialog from "@/mixins/dialog";
import temporaryService from "@/api/etax/temporary.js";
import messageCode from "@/enums/messageCode.js";
import DateQuarter from "@/components/date-time-pickers/date-time-pickers-quarter";
import DatePicker from "@/components/date-time-pickers";
export default {
  components: {
    dialogel: dialogel,
    "select-dropdown": selectDropdown,
    "form-input": formInput,
    "period-picker": DatePicker,
    "quarter-picker": DateQuarter,
  },
  props: {
    temporaryTax: {},
    modeType: String, // Nhận mode từ cha
  },
  mixins: [Dialog],
  data() {
    return {
      loadingDialog: false,
      titleTemporary: "THÊM MỚI BẢNG TÍNH THUẾ TNCN",
      isDisable: false,
      listTaxType: [],
      listPeriodType: [],
      hover: false, // biến validate trường input
      //OrganizationID: "2ef7003b-c2a3-11ea-9491-5203cfc96bc9",
      selectTaxType: {
        //Chứa các trường thông tin của form
        incomeType: 0, // Loại thu nhập
        temporaryTaxType: 0, //Kỳ tính thuế
        time: "", // Thời gian
        tableName: "", // Tên bảng tính
      },
      dataCheck: {
        // Nội dung bảng tính cần check
        incomeType: "",
        temporaryTaxType: "",
        periodMonthOrQuarter: "",
        year: "",
        groupType: "",
      },
      isDisableButtonAdd: false, // trạng thái enable/disable của nút Đồng ý
      defaultDate: "",
      form: {
        tableName: "",
      },
      isCopy: false,
      ischange: false,
    };
  },
  created() {
    this.listTaxType = taxType.TAX_TYPE;
    this.listPeriodType = periodType.PERIOD_TAX;
  },
  computed: {
    getTemporaryTaxType() {
      return this.selectTaxType.temporaryTaxType;
    },
  },
  watch: {
    /**
     * Theo dõi biến trạng thái thêm mới hay nhân bản để thay đổi thông tin trong form
     * created: NTNgoc - 18/8/2020
     */
    modeType: {
      handler(val) {
        if (val == "Nhân bản") {
          this.titleTemporary = "NHÂN BẢN BẢNG TÍNH THUẾ TNCN";
          this.isCopy = true;
          this.isDisable = true;
        } else if (val == "Thêm mới") {
          this.isCopy = false;
          this.selectTaxType.incomeType = 1;
          this.selectTaxType.temporaryTaxType = 1;
          this.titleTemporary = "THÊM MỚI BẢNG TÍNH THUẾ TNCN";
          var time =
            new Date().getMonth().toString() +
            "-" +
            new Date().getFullYear().toString(); // hàm xác định tháng hiện tại theo đúng format: MM-YYYY
          if (time.split("-")[0].length == 1)
            this.selectTaxType.time = "0" + time;
          else this.selectTaxType.time = time;
          this.isDisable = false;
          // set lại giá trị cho temporary
          this.temporaryTax = {};
        }
      },
      deep: true,
    },

    /**
     * Theo dõi trường thông tin trong form để cập nhật lên datacheck
     * created: ntngoc 15/8/2020
     */
    selectTaxType: {
      handler(val) {
        this.dataCheck.incomeType = val.incomeType;
        this.dataCheck.temporaryTaxType = val.temporaryTaxType;
        this.dataCheck.groupType = this.findInArray(
          val.incomeType,
          this.listTaxType,
          "VALUE",
          "GROUPTYPE"
        );
        if (val.temporaryTaxType == 1) {
          // Nếu kì tính thuế là tháng, time có dạng: MM-YYYY
          this.dataCheck.periodMonthOrQuarter = val.time.split("-")[0];
          this.dataCheck.year = val.time.split("-")[1];
        } else if (val.temporaryTaxType == 2) {
          // Nếu kỳ tính thuế là quý thì time có dạng: YYYY-Q
          this.dataCheck.periodMonthOrQuarter = val.time.split("-")[1];
          this.dataCheck.year = val.time.split("-")[0];
        }
        this.form.tableName = this.getTableName(val);
        // Cập nhật trạng thái enable/disable button Thêm mới
        if (
          !val.incomeType ||
          !val.temporaryTaxType ||
          !val.time ||
          !this.form.tableName
        )
          this.isDisableButtonAdd = true;
        else this.isDisableButtonAdd = false;
      },
      deep: true,
    },

    "form.tableName": {
      handler(val) {
        if (!val) this.isDisableButtonAdd = true;
        else this.isDisableButtonAdd = false;
      },
      deep: true,
    },

    /**
     * Theo dõi sự thay đổi của temporaryTax truyền xuống từ cha để cập nhật cho selectTaxType
     * created: ntngoc 15/8/2020
     */
    temporaryTax: {
      handler(val) {
        if (this.modeType == "Nhân bản") {
          this.selectTaxType.incomeType = this.findInArray(
            val.IncomeName,
            this.listTaxType,
            "STYLE",
            "VALUE"
          );
          this.selectTaxType.temporaryTaxType = val.TemporaryTaxType;
          this.isDisable = true;
          if (val.TemporaryTaxType == 1) {
            // nếu là tháng
            var date =
              (new Date().getMonth() + 1).toString() +
              "-" +
              new Date().getFullYear().toString();
            if (date.split("-")[0].length == 1) date = "0" + date;
            this.selectTaxType.time = date;
          } else if (val.TemporaryTaxType == 2) {
            // nếu là quý
            this.selectTaxType.time =
              new Date().getFullYear().toString() +
              "-" +
              Math.floor((new Date().getMonth() + 2) / 3).toString();
          }
        }
      },
      deep: true,
    },
  },

  methods: {
    /**
     * Hàm tìm kiếm trong mảng
     * created: NVBinh 12/7/2020
     */
    findInArray(value, array, propertyInput, propertyOutput) {
      for (let i in array)
        if (array[i][propertyInput] == value) {
          return array[i][propertyOutput];
        }
    },

    /**
     * truyền lên form
     */
    getTableNameFromInput(val) {
      this.form.tableName = val;
    },

    /**
     * truyền lên selectTaxType
     */
    getTaxType(val) {
      this.selectTaxType.incomeType = val;
    },

    /**
     * truyền lên selectTaxType
     */
    getPeriod(val) {
      this.selectTaxType.temporaryTaxType = val;
    },

    /**
     * truyền lên selectTaxType
     */
    getTime(val) {
      this.selectTaxType.time = val;
    },

    /**
     * Hàm định dạng lại ngày tháng theo đúng kiểu dữ liệu để truyền vào quarter picker
     * created:  NTNgoc 23/7/2020
     */
    formatQuarter(val) {
      if (val.split("-")[1].length > 1) {
        var time =
          new Date().getFullYear().toString() +
          "-" +
          Math.floor(new Date().getMonth() / 3).toString(); // Trả về :quý hiện tại
        this.selectTaxType.time = time;
        return time;
      } else {
        return val;
      }
    },

    /**
     * Hàm định dạng lại ngày tháng theo đúng kiểu dữ liệu để truyền vào date time picker
     * created:  NTNgoc 23/7/2020
     */
    formatMonth(val) {
      if (val.split("-")[0].length > 2) {
        var time =
          new Date().getMonth().toString() +
          "-" +
          new Date().getFullYear().toString(); // hàm xác định tháng hiện tại theo đúng format: MM-YYYY
        this.selectTaxType.time = time;
        return time;
      } else {
        return val;
      }
    },

    /**
     * Hàm set tên bảng tính thuế tự động
     * created: ntngoc 15/8/2020
     */
    getTableName(val) {
      if (!val.time) return "";
      else {
        var name = this.findInArray(
          val.incomeType,
          this.listTaxType,
          "VALUE",
          "NAME"
        );
        name = name.replace("Thu nhập", "");
        var tableName = "Bảng tính thuế TNCN" + name;
        if (val.temporaryTaxType == 1) {
          tableName = tableName + " Tháng " + val.time;
          return tableName;
        } else if (val.temporaryTaxType == 2) {
          tableName =
            tableName +
            " Quý " +
            val.time.split("-")[1] +
            "-" +
            val.time.split("-")[0];
          return tableName;
        } else return "";
      }
    },

    /**
     * Hàm check data trước khi thêm
     * created: ntngoc 15/8/2020
     */
    async checkDataTemporaryTax(data) {
      var checkRes = await temporaryService.checkTemporaryTax(data);
      return checkRes.data.Data;
    },

    /**
     * Hàm xử lí sự kiện khi ấn vào nút Đồng ý thêm mới bảng tinh thuế
     * created: ntngoc 15/8/2020
     */
    async insertTemporayTax() {
      var buttontype1 = [
        // loại 1: chỉ có 1 nút Đồng ý
        {
          text: "Đồng ý",
          class: "btn-primary",
          callback: () => {},
        },
      ];
      var buttontype2 = [
        // loại 2: có 2 nút đồng ý, hủy bỏ
        {
          text: "Hủy bỏ",
          class: "btn-grey",
          callback: () => {},
        },
        {
          text: "Đồng ý",
          class: "btn-primary",
          callback: () => {
            this.addNewTemporaryTax();
          },
        },
      ];
      if (
        this.dataCheck.year != null &&
        this.dataCheck.periodMonthOrQuarter != null &&
        this.dataCheck.incomeType != 0 &&
        this.dataCheck.temporaryTaxType != null
      ) {
        this.loadingDialog = true;
        // kiểm tra xem các trường có dữ liệu để check chưa
        var checkRes = await temporaryService.checkTemporaryTax(this.dataCheck);
        var message = messageCode.findInArrayMessageCode(checkRes.data.Data);
        /// Trong khi dữ liệu chưa hợp lệ
        /// Hiển thị dialog thông báo + disable button thêm
        if (
          checkRes.data.Data == 1 ||
          checkRes.data.Data == 5 ||
          checkRes.data.Data == 6 ||
          checkRes.data.Data == 7
        ) {
          this.showDialog = true;
          this.contentDialog = message;
          this.$_Popup.warn("Cảnh báo", this.contentDialog, buttontype1);
        } else if (checkRes.data.Data == 3 || checkRes.data.Data == 4) {
          this.showDialog = true;
          this.contentDialog = message + ". Bạn có muốn tiếp tục?";
          this.$_Popup.warn("Cảnh báo", this.contentDialog, buttontype2);
        } else if (checkRes.data.Data == 2) {
          if (this.dataCheck.temporaryTaxType == 1) {
            // Nếu đã tồn tại bảng tính thuế theo quý mà lại muốn thêm bảng tính theo các tháng trong quý đã tồn tại đó
            this.contentDialog =
              "Đã tồn tại tờ khai theo <strong>Quý " +
              Math.floor(
                (parseInt(this.dataCheck.periodMonthOrQuarter) + 2) / 3
              ) +
              "</strong>. Bạn không thể tiếp tục.";
            this.$_Popup.warn("Cảnh báo", this.contentDialog, buttontype1);
          } // Nếu đã tồn tại bảng tính thuế theo tháng mà lại muốn thêm bảng tính  theo quý chứa các tháng đã tồn tại đó
          else {
            this.contentDialog =
              "Đã tồn tại tờ khai theo tháng trong <strong>Quý " +
              this.dataCheck.periodMonthOrQuarter +
              "</strong>. Bạn không thể tiếp tục.";
            this.$_Popup.warn("Cảnh báo", this.contentDialog, buttontype1);
          }
          this.showDialog = true;
        }
        ///Dữ liệu hợp lệ thì enable button thêm và  thực hiện thêm mới
        else {
          await this.addNewTemporaryTax();
        }
        this.loadingDialog = false;
      }
    },

    /**
     * Hàm thêm mới bảng tính thuế
     * created: ntngoc 15/8/2020
     */
    async addNewTemporaryTax() {
      // Map dữ liệu bảng tính thuế thêm mới vào biến data
      var data = {};
      data.TemporaryTaxType = this.dataCheck.temporaryTaxType;
      data.TemporaryTaxName = this.form.tableName;
      data.PeriodYear = this.dataCheck.year;
      data.PeriodMonthOrQuarter = this.dataCheck.periodMonthOrQuarter;
      data.IncomeType = this.dataCheck.incomeType;
      data.GroupType = this.dataCheck.groupType;
      data.IsCopy = this.isCopy;
      data.TemporaryTaxCopyID = this.temporaryTax.TemporaryTaxID;
      // check đã tồn tại sessionStorage chưa, có thì xóa
      if (sessionStorage.getItem("objectName") !== null) {
        sessionStorage.clear();
      }
      // tạo sessionStorage
      sessionStorage.setItem("objectName", data.TemporaryTaxName);

      data = JSON.stringify(data);
      var res = await temporaryService.insertTemporaryTax(data);
      // Thêm mới bảng tính thuế vào database
      if (res.data.Success === true) {
        this.closeDialog(); // Đóng dialog thêm mới bảng tính thuế
        //   this.$store.commit("taxPeriod/setData", JSON.parse(data)); // Lưu dữ liệu bảng tính thuế thêm mới vào store
        this.$router.push("/tax-period/" + res.data.Data + "/3"); // chuyển đến trang chi tiết bảng tính thuế
        this.isDisableButtonAdd = false;
      } else {
        this.contentDialog =
          "Lỗi khi thêm mới bảng tính thuế. Vui lòng liên hệ với Misa để khắc phục"; // Nội dung lỗi
        this.buttonsPopup = [
          {
            text: "Đóng",
            class: "btn-primary",
            callback: () => {},
          },
        ];
        this.$_Popup.delete("Cảnh báo", this.contentDialog, this.buttonsPopup);
      }
    },

    /**
     * Hàm Nhân bản bảng tính thuế
     * created: ntngoc 15/8/2020
     */
    async copyTemporaryTax() {},

    /**
     * Hàm đóng dialog
     * created: ntngoc 17/8/2020
     */
    cancelInsert() {
      this.$refs["form"].resetFields();
      //if (this.modeType == "Thêm mới"){
      this.selectTaxType.temporaryTaxType = 1;
      this.selectTaxType.incomeType = 1;
      // } else {
      //     this.selectTaxType.incomeType = this.temporaryTax.IncomeType
      //     this.selectTaxType.temporaryTaxType = this.temporaryTax.TemporaryTaxType
      //     //this.selectTaxType.time =
      // }
      this.closeDialog();
    },
  },
};
</script>

<style lang="scss">
.custom-select {
  width: 362px !important;
}
.new-temporary {
  .el-form-item__error {
    transform: translate(-50%, 4%);
  }
}
.add-temporary {
  .el-form-item__error {
    transform: translate(-50%, 0%) !important;
  }
}
.add-temporary {
  .el-form-item {
    margin-bottom: 8px;
    input {
      padding-right: 0;
    }
  }
}
</style>
