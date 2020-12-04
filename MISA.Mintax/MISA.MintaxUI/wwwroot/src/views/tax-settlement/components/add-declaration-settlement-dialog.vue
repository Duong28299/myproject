<template>
  <div>
    <!-- Dialog thêm hồ sơ người phụ thuộc -->
    <add-dialog
      :titleDialog="title"
      :id="dialogid"
      :loading="loadingDialog"
      :dialogFormVisible="visiableDL"
      :closeonclickmodal="false"
      @close-dialog="getClose()"
      lg="4,5"
      xl="4,5"
      md="8,9"
    >
      <template v-slot:body>
        <el-form :model="formdata" ref="formdata">
          <el-row :gutter="24">
            <el-col :span="12">
              <p class="title-row">Chọn năm quyết toán</p>
              <el-form-item prop="year">
                <input-number
                  type="input-btn-dec_inc"
                  :valueInput="parseInt(formdata.SettlementYear)"
                  @value="formdata.SettlementYear = $event.toString()"
                  prefixString="Năm"
                  :max="NowYear"
                ></input-number>
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <p class="title-row">Chọn tháng quyết toán</p>
              <el-row>
                <el-col :span="11">
                  <el-form-item
                    prop="StarSettlementMonth"
                    :show-message="validate.StarSettlementMonth"
                    :error="starterror"
                  >
                    <datetime2
                      id="StarSettlementMonth"
                      type="month"
                      :value="startMonth"
                      :width="'135px'"
                      placeholder="__/____"
                      @getValue="getStartMonth"
                      @hover="validate.StarSettlementMonth = $event"
                    ></datetime2>
                  </el-form-item>
                </el-col>
                <el-col class="line" :span="2">đến</el-col>
                <el-col :span="11">
                  <el-form-item
                    prop="FinishSettlementMonth"
                    :show-message="validate.FinishSettlementMonth"
                    :error="enderror"
                  >
                    <datetime2
                      id="FinishSettlementMonth"
                      @hover="validate.FinishSettlementMonth = $event"
                      type="month"
                      placeholder="__/____"
                      :value="endMonth"
                      :width="'135px'"
                      @getValue="getFinishMonth"
                    ></datetime2>
                  </el-form-item>
                </el-col>
              </el-row>
            </el-col>
            <el-col :lg="24" :md="24" :sm="24">
              <el-form-item
                prop="DeclarationSettlementName"
                label="Tên tờ khai quyết toán thuế"
                :rules="[
                  {
                    required: true,
                    message: 'Tên tờ khai Quyết toán thuế không được bỏ trống.',
                    trigger: ['blur', 'change'],
                  },
                ]"
                :show-message="validate.DeclarationSettlementName"
              >
                <input-field
                  id="DeclarationSettlementName"
                  type="field-input"
                  @hover="validate.DeclarationSettlementName = $event"
                  :valueInput="formdata.DeclarationSettlementName"
                  @value="getDeclarationSettlementName"
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
    <!-- / Dialog thêm tờ khai QTT -->
  </div>
</template>
<script>
import dialog from "@/components/dialog";
import btn from "@/components/buttons";
import fieldinput from "@/components/inputs";
import datetime2 from "@/components/date-time-pickers/date-time-picker-v2";
import inputnumber from "@/components/inputs/input-number";
import DeclarationSettlement from "@/api/etax/declaration-settlement.js";
import DeclarationEnum from "@/enums/declaration-settlement.js";
import Cookies from 'js-cookie';
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
  },
  data() {
    return {
      isDisableSubmit: false,
      title: "",
      addtitle: "TẠO MỚI TỜ KHAI QUYẾT TOÁN THUẾ",
      addbutton: "Tạo mới",
      button: "",
      dialogid: "add-declaration-dialog",
      NowYear: new Date().getFullYear(),
      NowMonth: new Date().getMonth() + 1,
      starterror: "",
      enderror: "",
      futuremonthmessage: "Tháng quyết toán không được phép chọn tương lai",
      monthmessage: 'Giá trị "đến tháng" không được nhỏ hơn giá trị "từ tháng',
      yearmonthmessage:
        "Thời gian quyết toán phải nằm trong năm quyết toán được chọn",
      validate: {
        DeclarationSettlementName: false,
        SettlementYear: false,
        StarSettlementMonth: false,
        FinishSettlementMonth: false,
      },
      formdata: {
        SettlementYear: "",
        DeclarationSettlementName: "",
        StarSettlementMonth: null,
        FinishSettlementMonth: null,
        ConfirmUpdate: DeclarationEnum.ConfirmUpdate.DefaultValue, //Giá trị mặc định của conffirmUpdate, nếu tờ khai bị trùng thì trả về thông báo
        ConfirmEnoughTemporaryTax: false, // Giá trị mặc định ban đầu nếu đủ thì tạo tờ khai nếu k đủ hiển thị thông báo
      },
      wanringYearmessage:
        "Năm đăng ký nhỏ hơn năm hiện tại. Bạn có muốn tiếp tục ?",
      CreatedSuccess: "Tạo hồ sơ đăng ký thành công.",
      CreatedFail:
        "Bạn đã tạo tờ khai quyết toán thuế thất bại.Vui lòng kiểm tra lại.",
      DetailDeclaration: {},
      loadingDialog:false,
    };
  },
  computed: {
    startMonth: function () {
      return (
        (this.formdata.StarSettlementMonth < 10
          ? "0" + this.formdata.StarSettlementMonth
          : this.formdata.StarSettlementMonth) +
        "/" +
        this.formdata.SettlementYear
      );
    },
    endMonth: function () {
      return (
        (this.formdata.FinishSettlementMonth < 10
          ? "0" + this.formdata.FinishSettlementMonth
          : this.formdata.FinishSettlementMonth) +
        "/" +
        this.formdata.SettlementYear
      );
    },
  },
  watch: {
    "formdata.SettlementYear": {
      handler(val) {
        let month = this.NowMonth;
        this.formdata.SettlementYear = parseInt(val);
        if (parseInt(val) !== this.NowYear) {
          this.formdata.FinishSettlementMonth = 12;
        } else this.formdata.FinishSettlementMonth = month;
        this.formdata.DeclarationSettlementName = `Quyết toán thuế - Tháng ${this.startMonth} đến ${this.endMonth}`;
      },
      deep: true,
    },
    "formdata.StarSettlementMonth": {
      handler() {
        this.formdata.DeclarationSettlementName = `Quyết toán thuế - Tháng ${this.startMonth} đến ${this.endMonth}`;
      },
      deep: true,
    },
    "formdata.FinishSettlementMonth": {
      handler() {
        this.formdata.DeclarationSettlementName = `Quyết toán thuế - Tháng ${this.startMonth} đến ${this.endMonth}`;
      },
      deep: true,
    },
    visiableDL: {
      async handler(val) {
        this.title = this.addtitle;
        this.button = this.addbutton;
        if (val) {
          this.formdata.ConfirmUpdate =
            DeclarationEnum.ConfirmUpdate.DefaultValue;
          this.formdata.ConfirmEnoughTemporaryTax = false;
          // phần xử lý năm
          if (this.NowMonth <= 3) {
            this.formdata.SettlementYear = this.NowYear - 1;
            this.formdata.StarSettlementMonth = 1;
            this.formdata.FinishSettlementMonth = 12;
          } else {
            this.formdata.SettlementYear = this.NowYear;
            this.formdata.StarSettlementMonth = 1;
            this.formdata.FinishSettlementMonth = this.NowMonth;
          }
          this.formdata.DeclarationSettlementName = `Quyết toán thuế - Tháng ${this.startMonth} đến ${this.endMonth}`;
        }
      },
    },
    "formdata.DeclarationSettlementName": {
      handler(val) {
        if (!val) this.isDisableSubmit = true;
      },
    },
  },
  methods: {
    getYear(value) {
      this.formdata.SettlementYear = value;
    },

    getDeclarationSettlementName(value) {
      this.formdata.DeclarationSettlementName = value;
    },

    setUpData() {
      this.formdata.SettlementYear = this.NowYear.toString();
      this.formdata.DeclarationSettlementName = `Năm ${this.NowYear}`;
    },

    getStartMonth(val) {
      var month = parseInt(val.split("-")[0]);
      var year = parseInt(val.split("-")[1]);
      this.formdata.StarSettlementMonth = month;
      this.checkMonth(year, month, "start");
    },

    getFinishMonth(val) {
      var month = parseInt(val.split("-")[0]);
      var year = parseInt(val.split("-")[1]);
      this.formdata.FinishSettlementMonth = month;
      this.checkMonth(year, month, "finish");
    },

    /**
     * Hàm tạo tờ khai quyết toán thuế và xử lý logic
     * CreatedBY NVANH 28.10.2020
     */
    async postDeclaration() {
      this.loadingDialog = true;

      const res = await DeclarationSettlement.postDeclarationSettlement(
        this.formdata
      );
      this.loadingDialog = false;
      if (res.data.Success) {
        this.formdata.ConfirmUpdate =
          DeclarationEnum.ConfirmUpdate.DefaultValue;
        this.formdata.ConfirmEnoughTemporaryTax = false;
        if (Cookies.get('isCollapse') != null)
            Cookies.remove('isCollapse')
        Cookies.set('isCollapse', false);

        if (Cookies.get('activeTab') != null)
            Cookies.remove('activeTab')
        Cookies.set('activeTab', "gentab");

        this.$router.push({
          name: "TaxSettlementDetail",
          params: { id: res.data.Data },
        });
        this.$emit("reload", true);
        this.getClose();
        if(res.data.MISACode === 200){
            return this.$_Notification.success(
            "Thành công",
            "Cập nhật tờ khai quyết toán thuế thành công."
          );
        }else if(res.data.MISACode === 201){
          return this.$_Notification.success(
            "Thành công",
            "Tạo tờ khai quyết toán thuế thành công."
          );
        }
        
      }else{
        if (res.data.MISACode === 502) {
        return this.$_Popup.warn(
          "Cảnh báo",
          "Số lượng bảng tạm tính chưa đủ. Bạn vẫn muốn tạo tờ khai quyết toán thuế?",
          [
            {
              text: "Không",
              class: "btn-grey",
              callback: () => {},
            },
            {
              text: "Có",
              class: "btn-primary",
              callback: async () => {
                this.formdata.ConfirmEnoughTemporaryTax = true;
                await this.postDeclaration();
              },
            }            
          ]
        );
      }
      if (res.data.MISACode === 501) {
        return this.$_Popup.warn(
          "Cảnh báo",
          "Tờ khai quyết toán thuế bị trùng lặp. Bạn có muốn cập nhật tờ khai không?",
          [            
            {
              text: "Tạo mới",
              class: "btn-grey",
              callback: async () => {
                this.formdata.ConfirmUpdate = DeclarationEnum.ConfirmUpdate.New; // 2 là thêm mới
                await this.postDeclaration();
              },
            },
            {
              text: "Cập nhật",
              class: "btn-primary",
              callback: async () => {
                this.formdata.ConfirmUpdate =
                  DeclarationEnum.ConfirmUpdate.Update; // 1 là ghi đè
                await this.postDeclaration();
              },
            }
          ]
        );
      } 
      if (res.data.MISACode === 301) {
          return this.$_Notification.error(
            "Thất bại",
            "Cập nhật tờ khai quyết toán thuế thất bại."
          );
        }
      if (res.data.MISACode === 603) {
          return this.$_Notification.error(
            "Thất bại",
            "Tạo tờ khai quyết toán thuế thất bại."
          );
        }
        else{
        return this.$_Notification.error(
            "Thất bại",
            "Tạo tờ khai quyết toán thuế thất bại."
          );
      }
      }
      
      
    },

    /**
     * Hàm xóa validate theo id
     * @param {id} id của thẻ
     * CreatedBy NVANH 11.11.2020
     */
    clearValidate(id) {
      if (id === "StarSettlementMonth") {
        this.starterror = null;
      }
      if (id === "FinishSettlementMonth") {
        this.enderror = null;
      }
      var element = document.querySelectorAll(".is-error");
      for (let index = 0; index < element.length; index++) {
        if (element[index].querySelector(`#${id}`))
          element[index].classList.remove("is-error");
      }
      if (document.querySelectorAll(".is-error").length > 0)
        this.isDisableSubmit = true;
      else this.isDisableSubmit = false;
    },

    /**
     * Các hàm thêm báo lỗi cho các thẻ
     * CreatedBy NVANH 11.11.2020
     */
    addValidateForSettlementYear() {
      this.validate.SettlementYear = true;
      this.isDisableSubmit = true;
    },
    addValidateForStarSettlementMonth() {
      this.validate.StarSettlementMonth = true;
      this.isDisableSubmit = true;
    },
    addValidateForFinishSettlementMonth() {
      this.validate.FinishSettlementMonth = true;
      this.isDisableSubmit = true;
    },

    /**
     * Hàm check tháng quyết toán 28.10.2020
     * Created by NVANH
     * ModifyBy NVANH 11.11.2020
     */
    checkMonth(year, month, key) {
      if (year === this.NowYear) {
        if (month > this.NowMonth) {
          if (key == "start") {
            this.starterror = this.futuremonthmessage;
            this.addValidateForStarSettlementMonth();
          }
          if (key == "finish") {
            if (this.formdata.StarSettlementMonth < month) {
              this.clearValidate("StarSettlementMonth");
            }
            this.enderror = this.futuremonthmessage;
            this.addValidateForFinishSettlementMonth();
          }
          return;
        }
      }
      if (year !== this.formdata.SettlementYear) {
        if (key == "start") {
          this.starterror = this.yearmonthmessage;
          this.addValidateForStarSettlementMonth();
        }
        if (key == "finish") {
          this.enderror = this.yearmonthmessage;
          this.addValidateForFinishSettlementMonth();
        }
        return;
      }
      if (
        this.formdata.StarSettlementMonth > this.formdata.FinishSettlementMonth
      ) {
        if (key == "start") {
          this.starterror = this.monthmessage;
          this.addValidateForStarSettlementMonth();
        }
        if (key == "finish") {
          this.enderror = this.monthmessage;
          this.addValidateForFinishSettlementMonth();
        }
        return;
      }
      if (
        // Năm khác năm hiện tại
        (this.formdata.StarSettlementMonth <
          this.formdata.FinishSettlementMonth &&
          this.formdata.SettlementYear !== this.NowYear) ||
        //Năm cùng năm hiện tại
        (this.formdata.SettlementYear === this.NowYear &&
          this.formdata.FinishSettlementMonth <= this.NowMonth &&
          this.formdata.StarSettlementMonth <= this.NowMonth)
      ) {
        this.clearValidate("StarSettlementMonth");
        this.clearValidate("FinishSettlementMonth");
        return;
      }

      if (key == "start") {
        this.clearValidate("StarSettlementMonth");
      }
      if (key == "finish") {
        this.clearValidate("FinishSettlementMonth");
      }
    },

    setUpDate() {
      if (this.NowMonth <= 3) {
        this.formdata.SettlementYear = this.NowYear - 1;
        this.formdata.StarSettlementMonth = 1;
        this.formdata.FinishSettlementMonth = 12;
      } else {
        this.formdata.SettlementYear = this.NowYear;
        this.formdata.StarSettlementMonth = 1;
        this.formdata.FinishSettlementMonth = this.NowMonth;
      }
    },

    /**
     * Hàm xử lý nút thêm mới
     * Created by: NVANH 28.10.2020
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
          await this.postDeclaration();
        }
      });
    },

    resetForm() {
      this.getClose();
    },
    getClose() {
      this.$emit("closeDialog", false);
    },
  },
};
</script>
<style lang="scss" >
@import "@/styles/variables.scss";
#add-declaration-dialog {
  .is-error-Citizent {
    border: 1px solid $red;
  }

  .line {
    padding: 19px 0px !important;
    margin-left: -5px !important;
    margin-right: 5px !important;
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

  .el-form-item__label {
    margin-bottom: 0 !important;
    font-family: "GoogleSansRegular";
    font-weight: normal;
    color: $dark;
    line-height: 22px;
    font-size: 13px;
  }

  .el-form-item__error {
    transform: translate(-50%, -125%) !important;
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

  .el-form-item--medium .el-form-item__content {
    line-height: 38px;
  }

  .el-button.is-disabled:hover {
    color: #c0c4cc !important;
    cursor: not-allowed;
    background-image: none;
    background-color: $white !important;
    border-color: $white-bordertable;
  }
}
</style>