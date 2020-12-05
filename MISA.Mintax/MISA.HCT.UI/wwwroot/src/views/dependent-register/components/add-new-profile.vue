<template>
  <div>
    <!-- Dialog thêm hồ sơ người phụ thuộc -->
    <add-dialog
      :titleDialog="title"
      :id="dialogid"
      :dialogFormVisible="visiableDL"
      :closeonclickmodal="false"
      :loading="loadingDialog"
      @close-dialog="getClose()"
      lg="4,5"
      xl="4,5"
      md="4,5"
    >
      <template v-slot:body>
        <el-form :model="formdata" ref="formdata">
          <el-row :gutter="24">
            <el-col :span="12">
              <p class="title-row">Chọn năm đăng ký</p>
              <el-form-item prop="year">
                <input-number
                  type="input-btn-dec_inc"
                  :valueInput="parseInt(formdata.PeriodYear)"
                  @value="formdata.PeriodYear = $event.toString()"
                  prefixString="Năm"
                  :max="NowYear"
                ></input-number>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <p class="title-row">Chọn tháng đăng ký</p>
              <el-form-item prop="month">
                <datetime2
                  id="PeriodMonthOrQuarter"
                  type="month"
                  :value="registerMonth"
                  :width="'100%'"
                  placeholder="__/____"
                  @getValue="getRegisterMonth"
                  @hover="validate.PeriodMonthOrQuarter = $event"
                ></datetime2>
              </el-form-item>
            </el-col>
            <el-col :lg="24" :md="24" :sm="24">
              <el-form-item
                prop="DeclarationName"
                label="Tên hồ sơ đăng ký người phụ thuộc"
                :rules="[
                  {
                    required: true,
                    message: 'Tên hồ sơ đăng ký không được bỏ trống.',
                    trigger: ['blur', 'change'],
                  },
                ]"
                :show-message="validate.DeclarationName"
              >
                <input-field
                  id="DeclarationName"
                  type="field-input"
                  @hover="validate.DeclarationName = $event"
                  :valueInput="formdata.DeclarationName"
                  @value="getDeclarationName"
                ></input-field>
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
      </template>
      <template v-slot:footer>
        <btn
          type="btn-primary"
          :name="button"
          :handleClick="sendSubmit"
          :disabled="isDisableSubmit"
        ></btn>
        <btn
          type="btn-grey"
          style="margin-right: 10px"
          name="Hủy"
          :handleClick="resetForm"
        />
      </template>
    </add-dialog>
    <!-- / Dialog thêm hồ sơ người phụ thuộc -->
  </div>
</template>
<script>
import dialog from "@/components/dialog";
import btn from "@/components/buttons";
import fieldinput from "@/components/inputs";
import inputnumber from "@/components/inputs/input-number";
import datetime2 from "@/components/date-time-pickers/date-time-picker-v2";
import DeclarationAPI from "@/api/etax/declaration.js";
export default {
  components: {
    "add-dialog": dialog,
    "input-field": fieldinput,
    btn,
    datetime2,
    "input-number": inputnumber,
  },
  props: {
    visiableDL: {
      type: Boolean,
      default: true,
    },
    isAdd: {
      type: Boolean,
      default: true,
    },
    DeclarationID: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      loadingDialog:false,
      isDisableSubmit: false,
      title: "",
      addtitle: "THÊM MỚI HỒ SƠ ĐĂNG KÝ NGƯỜI PHỤ THUỘC",
      edittitle: "SỬA HỒ SƠ ĐĂNG KÝ NGƯỜI PHỤ THUỘC",
      addbutton: "Tạo mới",
      editbutton: "Sửa",
      button: "",
      dialogid: "add-dependent-person-dialog",
      isChangeDeclarationName: false, // co cho phep thay doi ten ho so hay khong
      NowYear: new Date().getFullYear(),
      NowMonth: new Date().getMonth() + 1,
      validate: {
        DeclarationName: false,
        PeriodMonthOrQuarter: false,
      },
      formdata: {
        PeriodYear: "",
        DeclarationName: "",
        PeriodMonthOrQuarter: null,
        TaxType: 1,
        GroupType: 22, //Group Type của hồ sơ đăng ký người phụ thuộc
      },
      wanringYearmessage:
        "Năm đăng ký nhỏ hơn năm hiện tại. Bạn có muốn tiếp tục ?",
      CreatedSuccess: "Tạo hồ sơ đăng ký thành công.",
      CreatedFail:
        "Bạn đã tạo hồ sơ đăng ký người phụ thuộc thất bại.Vui lòng kiểm tra lại.",
      UpdatedSuccess: "Sửa hồ sơ đăng ký thành công.",
      UpdatedFail:
        "Bạn đã sửa hồ sơ đăng ký người phụ thuộc thất bại.Vui lòng kiểm tra lại.",
      DetailDeclaration: {},
    };
  },
  computed: {
    registerMonth: function () {
      return (this.formdata.PeriodMonthOrQuarter < 10
        ? "0" + this.formdata.PeriodMonthOrQuarter
        : this.formdata.PeriodMonthOrQuarter) +
        "/" + this.formdata.PeriodYear;
    },
  },
  watch: {
    "formdata.PeriodYear": {
      handler(val) {
        let month = this.NowMonth;
        if (parseInt(val) !== this.NowYear) month = 12;
        this.formdata.PeriodMonthOrQuarter = month;
        if (this.isChangeDeclarationName)
          this.formdata.DeclarationName = `Đăng ký người phụ thuộc - Tháng ${month} - Năm ${val}`;
        this.isChangeDeclarationName = true;
      },
    },
    visiableDL: {
      async handler(val) {
        this.title = this.isAdd ? this.addtitle : this.edittitle;
        this.button = this.isAdd ? this.addbutton : this.editbutton;
        if (val && this.isAdd) {
          this.formdata.PeriodMonthOrQuarter = this.NowMonth;
          this.formdata.PeriodYear = this.NowYear;
          this.formdata.DeclarationName = `Đăng ký người phụ thuộc - Tháng ${this.NowMonth} - Năm ${this.NowYear}`;
        }
        if (val && !this.isAdd) {
          if (!this.isAdd) await this.getDataTOEdit();
        }
      },
    },
    "formdata.DeclarationName": {
      handler(val) {
        this.isDisableSubmit = val == "" || val == null ? true : false;
      },
    },
    "formdata.PeriodMonthOrQuarter": {
      handler() {
        this.formdata.DeclarationName = `Đăng ký người phụ thuộc - Tháng ${this.formdata.PeriodMonthOrQuarter}  - Năm ${this.formdata.PeriodYear}`;
      },
      deep: true,
    },
  },
  methods: {
    async getDataTOEdit() {
      this.isChangeDeclarationName = false; // khi vao form cap nhat thì lan dau tien khong cho phep thay doi ten ho so
      const res = await DeclarationAPI.getViewDeclaration({
        declarationID: this.DeclarationID,
      });
      this.DetailDeclaration = res.data.Data;
      this.formdata.PeriodYear = res.data.Data.PeriodYear.toString();
      this.formdata.DeclarationName = res.data.Data.DeclarationName;
      this.formdata.PeriodMonthOrQuarter = res.data.Data.PeriodMonthOrQuarter;
      //this.isChangeDeclarationName=true;
    },
    getYear(value) {
      this.formdata.PeriodYear = value;
    },
    getDeclarationName(value) {
      this.formdata.DeclarationName = value;
    },
    setUpData() {
      this.formdata.PeriodYear = this.NowYear.toString();
      this.formdata.DeclarationName = `Năm ${this.NowYear}`;
    },
    /**
     * Hàm xử lý nút thêm mới
     * Created by: NVANH
     */
    async sendSubmit() {
      this.$refs["formdata"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById(`${Object.keys(index)[0]}`).focus();
            }, 200);
          }
        }
        if (valid) {
          if (parseInt(this.formdata.PeriodYear) < this.NowYear)
            await this.$_Popup.warn("Cảnh báo", this.wanringYearmessage, [
              {
                text: "Có",
                class: "btn-primary",
                callback: async () => {
                  await this.postDeleration();
                },
              },
              {
                text: "Không",
                class: "btn-info",
                callback: () => {},
              },
            ]);
          else await this.postDeleration();
        }
      });
    },
    /**
     * Gọi Service đển post thông tin hồ sơ đăng ký
     * @param _confrim biến sử dụng để service biết đây là lần gửi đầu tiên, false- gửi lần đầu, true - gửi lần 2 nếu res trả về false
     * Created by: NVANH(16.09)
     * Modified: nvbinh2 23.09.2020
     * Modified: ndhuy 09.10.2020
     */
    async postDeleration(_confirm = false) {
      // this.formdata.PeriodYear = parseInt(this.formdata.PeriodYear);
      if (!this.formdata.DeclarationName) return;
      var decla = {};
      decla = Object.assign(decla, this.formdata);
      decla.confirm = _confirm;
      if (typeof decla.PeriodYear == "string") {
        decla.PeriodYear = parseInt(decla.PeriodYear);
      }
      var res ;
      if(this.isAdd){
        this.loadingDialog=true;
        res = await DeclarationAPI.postDeclaration(decla);
        this.loadingDialog=false;
      }

      if (!this.isAdd) {
        decla = {
          ...this.DetailDeclaration,
          ...this.formdata,
        };
        this.loadingDialog=true;
        res = await DeclarationAPI.updateDeclaration(decla);
        this.loadingDialog=false;
      }
      // sucsess=false, reason=1, form them moi - đã tồn tại hồ sơ với năm, tháng, loại tương ứng
      if (!res.data.Success && res.data.Data.Reason == 1 && this.isAdd) {
        await this.$_Popup.warn(
          "Cảnh báo",
          "Hồ sơ " +
            "<b>" +
            decla.DeclarationName +
            "</b> đã được tạo. Bạn có muốn cập nhật hay không?",
          [
            {
              text: "Không",
              class: "btn-grey",
              callback: async () => {
                var suffix = res.data.Data.NumberOfDeclaration + 1;
                this.formdata.DeclarationName =
                  this.formdata.DeclarationName + " lần " + suffix;
                await this.postDeleration(true);
              },
            },
            {
              text: "Có",
              class: "btn-primary",
              callback: () => {
                this.$router.push({
                  name: "DependentRegisterDetail",
                  params: { id: res.data.Data.DeclarationID },
                });
              },
            },
          ]
        );
      } else if (res.data.Success) {
        this.$_Notification.success(
          "Thành công",
          this.isAdd ? this.CreatedSuccess : this.UpdatedSuccess
        );
        if (
          sessionStorage.getItem("objectName") !== null ||
          sessionStorage.getItem("declarationStatus")
        ) {
          sessionStorage.clear();
        }
        // tạo sessionStorage
        sessionStorage.setItem("objectName", this.formdata.DeclarationName);
        sessionStorage.setItem("declarationStatus", 1);
        if (this.isAdd)
          this.$router.push({
            name: "DependentRegisterDetail",
            params: { id: res.data.Data },
          });
        else this.$emit("reload", true);
        this.getClose();
      } else {
        this.getClose();
        this.$_Notification.error(
          "Thất bại",
          this.isAdd ? this.CreatedFail : this.UpdatedFail
        );
      }
    },

    resetForm() {
      this.getClose();
    },

    getClose() {
      this.$emit("closeDialog", false);
    },

    getRegisterMonth(val) {
      var month = parseInt(val.split("-")[0]);
      var year = parseInt(val.split("-")[1]);
      this.formdata.PeriodMonthOrQuarter = month;
      this.checkMonth(year, month);
    },

    /**
     * Hàm check tháng  28.10.2020
     * Created by NVANH
     */
    checkMonth(year, month) {
      if (year === this.NowYear) {
        if (month > this.NowMonth)
          return this.$_Popup.warn(
            "Cảnh báo",
            "Không được phép chọn tháng đăng ký lớn hơn tháng hiện tại !",
            [
              {
                text: "Đóng",
                class: "btn-primary",
                callback: () => {  this.formdata.PeriodMonthOrQuarter = this.NowMonth;},
              },
            ]
          );
      }
    },
  },
  created() {
    this.formdata.DeclarationName = `Đăng ký người phụ thuộc - Tháng ${this.NowMonth} - Năm ${this.NowYear}`;
  },
};
</script>
<style lang="scss" >
@import "@/styles/variables.scss";
#add-dependent-person-dialog {
  .btn-seconds {
    border: none !important;
  }

  label {
    font-weight: unset !important;
  }

  .title-row {
    margin: 5px 0 !important;
  }

  .el-form-item {
    margin-bottom: 0px !important;
  }

  .el-col {
    padding: 6px 0 6px 0;
  }

  .el-form-item__error {
    transform: translate(-50%, -25%) !important;
  }

  .el-form-item__label {
    margin-bottom: 0;
    font-family: $_fontregular;
    font-weight: normal;
    font-size: 13px;
    color: $dark;
  }

  .el-form-item__label {
    margin: 0 !important;
    font-family: $_fontregular !important;
  }

  .el-button.is-disabled:hover {
    color: #c0c4cc !important;
    cursor: not-allowed;
    background-image: none;
    background-color: #ffffff !important;
    border-color: #ebeef5;
  }
}
</style>