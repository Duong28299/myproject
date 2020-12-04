<template>
  <div class="new-tax-profile">
    <!-- dialog mở khi nhấn thêm hồ sơ -->
    <dialogel
      id="tax-profile-dialog"
      :titleDialog="taxRegisterDialogTitle"
      :dialogFormVisible="isTaxRegisterDialogOpen"
      @close-dialog="cancelInsert"
      :loading="loadingDialog"
      close-on-press-Escape="cancelInsert"
      xl="4,5"
      lg="4,5"
      md="4,8"
      sm="2,5"
    >
      <template v-slot:body>
        <el-form class="main" ref="formNewTaxProfile" :model="formData">
          <!-- Chọn năm đăng ký -->
          <el-row :gutter="24">
            <el-col :span="12">
              <div class="label" style="margin-bottom: 4px">
                Chọn năm đăng ký
              </div>
              <el-form-item>
                <input-number
                  type="input-btn-dec_inc"
                  :valueInput="formData.year"
                  @value="setValueYear"
                  prefixString="Năm"
                  :max="currentYear"
                ></input-number>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <div class="label" style="margin-bottom: 4px">
                Chọn tháng đăng ký
              </div>
              <el-form-item>
                <datetime2
                  id="MonthRegister"
                  type="month"
                  :value="maxMonthOption"
                  :width="'166px'"
                  placeholder="__/____"
                  @getValue="getStartMonth"
                ></datetime2>
              </el-form-item>
            </el-col>
          </el-row>
          <!-- Chọn loại đăng ký -->
          <div class="label">Chọn loại đăng ký</div>
          <el-row :gutter="24" class="cb-register-type">
            <el-col :span="12" class="new-register">
              <btn                
                :type="'btn-checkbox-icon'"
                :name="'Đăng ký thuế'"
                @getValue="setTaxRegisterMode"
                :ckValue="formData.taxMode"
                :disabled="formMode == 1 && !formData.taxMode"
                :radioType="true"
              >
                <template v-slot:icon>
                  <img
                    v-if="formData.taxMode"
                    class="icon"
                    src="@/icons/ico-add-user@2x.png"
                  />
                  <img
                    v-if="!formData.taxMode"
                    class="icon"
                    src="@/icons/Group 19239@2x.png"
                  />
                </template>
              </btn>
            </el-col>
            <el-col :span="12" class="change-register">
              <btn                
                :type="'btn-checkbox-icon'"
                :name="'Thay đổi thông tin'"
                @getValue="setTaxChangeInfoMode"
                :ckValue="!formData.taxMode"
                :disabled="formMode == 1 && formData.taxMode"
                :radioType="true"
              >
                <template v-slot:icon>
                  <img
                    v-if="formData.taxMode"
                    class="icon"
                    src="@/icons/Group 19244@2x.png"
                  />
                  <img
                    v-if="!formData.taxMode"
                    class="icon"
                    src="@/icons/Group 19245@2x.png"
                  />
                </template>
              </btn>
            </el-col>
          </el-row>
          <!-- Tên hồ sơ đăng ký mã số thuế-->
          <el-form-item
            label="Tên hồ sơ đăng ký mã số thuế"
            prop="tableName"
            :rules="[
              {
                required: true,
                message: 'Tên hồ sơ không được bỏ trống',
                trigger: ['blur', 'change'],
              },
            ]"
            :show-message="validate.tableName"
          >
            <!-- <div class="label">Tên hồ sơ đăng kí mã số thuế</div> -->
            <form-input
              id="tableName"
              type="field-input"
              @value="formData.tableName = $event"
              @hover="validate.tableName = $event"
              :valueInput="formData.tableName"
              placeholder="Nhập tên bảng tính"
            ></form-input>
          </el-form-item>
        </el-form>
      </template>

      <template v-slot:footer class="dialog-footer">
        <el-button @click="cancelInsert" class="mr-10 btn-grey"
          >Hủy</el-button
        >
        <el-button
          v-if="formMode == 0"
          class="ml-11 btn-primary"
          @click="onSubmit"
          :disabled="isDisableSubmit"
          >Tạo mới</el-button
        >
        <el-button
          v-if="formMode == 1"
          class="ml-11 btn-primary"
          @click="onSubmit"
          :disabled="isDisableSubmit"
          >Cập nhật</el-button
        >
      </template>
    </dialogel>
  </div>
</template>

<script>
import dialogel from "@/components/dialog";
import formInput from "@/components/inputs";
import inputNumber from "@/components/inputs/input-number";
import Button from "@/components/buttons";
import taxRegisterType from "@/enums/taxRegister";
import ApiDeclaration from "@/api/etax/declaration.js";
import datetime2 from "@/components/date-time-pickers/date-time-picker-v2";
export default {
  components: {
    dialogel: dialogel,
    "form-input": formInput,
    "input-number": inputNumber,
    datetime2,
    btn: Button,
  },

  props: {
    isTaxRegisterDialogOpen: {
      type: Boolean,
      default: false,
    },

    taxProfile: {
      type: Object,
      default: () => {},
    },

    formMode: {
      type: Number,
      default: 0,
    },
  },

  data() {
    return {
      loadingDialog:false,
      taxRegisterDialogTitle: "THÊM MỚI HỒ SƠ ĐĂNG KÝ MÃ SỐ THUẾ",
      currentYear: 0,
      currentMonth: 0,
      isWarningYear: false,
      isDisableSubmit: false,
      formData: {
        tableName: "",
        year: null,
        taxMode: true, //chế độ đăng kí: true-đăng kí thuế, false-thay đổi thông tin
        month: null,
      },
      validate: {
        tableName: false,
      },
      listRegisterType: [],
    };
  },

  created() {
    var date = new Date();
    this.currentYear = date.getFullYear();
    this.currentMonth = date.getMonth() + 1;
    this.setTableName();
    this.listRegisterType = taxRegisterType.TAX_REGISTER_TYPE;
  },

  watch: {
    isTaxRegisterDialogOpen(value) {
      if (value == true && this.formMode == 0) this.initFormData();
    },

    "formData.year": {
      handler(val) {
        if (val < this.currentYear) {
          this.formData.month = 12;
          this.setTableName();
        } else if (val == this.currentYear) {
          var date = new Date();
          this.currentYear = date.getFullYear();
          this.currentMonth = date.getMonth() + 1;
          this.formData.month = this.currentMonth;
          this.setTableName();
        }
      },
      deep: true,
    },

    "formData.tableName": {
      handler(val) {
        this.isDisableSubmit = false;
        if (!val) this.isDisableSubmit = true;
      },
      deep: true,
    },

    taxProfile: {
      handler(val) {
        if (this.formMode == 1) {
          //chế độ chỉnh sửa
          this.taxRegisterDialogTitle = "CHỈNH SỬA HỒ SƠ ĐĂNG KÝ MÃ SỐ THUẾ";
          this.formData.tableName = val.DeclarationName;
          this.formData.year = val.PeriodYear;
          this.formData.taxMode = val.GroupType == 21 ? true : false;
          this.formData.month = val.PeriodMonthOrQuarter;
        } else if (this.formMode == 0) {
          this.taxRegisterDialogTitle = "THÊM MỚI HỒ SƠ ĐĂNG KÝ MÃ SỐ THUẾ";
          this.initFormData();
        }
      },
      deep: true,
    },
  },

  computed: {
    maxMonthOption: function () {
      if (this.formData.month < 10) {
        return "0" + this.formData.month + "-" + this.formData.year;
      } else {
        return this.formData.month + "-" + this.formData.year;
      }
    },
  },

  methods: {
    /**
     * Hàm xử lý khi đóng form
     * createdby ndhuy 15.09.2020
     */
    cancelInsert() {
      this.$refs["formNewTaxProfile"].resetFields();
      this.$emit("cancel-insert", false);
    },

    /**
     * Hàm cập nhật lại formData.year từ input
     * @param value giá trị year mới
     * createdby ndhuy 15.09.2020
     */
    setValueYear(value) {
      this.formData.year = value;
      this.isWarningYear = false;
      if (this.formData.year < this.currentYear) this.isWarningYear = true;
      this.setTableName();
    },

    /**
     * truyền giá trị cho formData
     *  created by NQTrung 12/11/2020
     */
    getStartMonth(val) {
      var month = parseInt(val.split("-")[0]);
      var year = parseInt(val.split("-")[1]);
      this.formData.month = month;
      this.setTableName();
      this.checkMonth(month, year);
    },

    /**
     * Kiểm tra tháng hợp lệ
     *  created by NQTrung 12/11/2020
     */
    checkMonth(month, year) {
      if (year === this.currentYear) {
        if (month > this.currentMonth) {
          return this.$_Popup.warn(
            "Cảnh báo",
            "Tháng quyết toán không được phép chọn tháng tương lai",
            [
              {
                text: "Đóng",
                class: "btn-primary",
                callback: this.setUpdate,
              },
            ]
          );
        }
      }
    },

    /**
     * Set lại giá trị của formData khi chọn tháng tương lai
     * created by NQTrung 12/11/2020
     */
    setUpdate() {
      if (this.currentMonth < 3) {
        this.formData.year = this.currentYear - 1;
        this.formdata.month = 12;
      }
      this.formData.month = this.currentMonth;
      this.formData.year = this.currentYear;
    },

    /**
     * Khởi tạo giá trị cho form
     */
    initFormData() {
      this.formData.year = this.currentYear;
      this.formData.month = this.currentMonth;
      this.formData.taxMode = true;
      this.isWarningYear = false;
      this.setTableName();
    },

    /**
     * Hàm sử lý sự kiện chọn radio button Đăng ký thuế
     * createdby ndhuy 15.09.2020
     */
    setTaxRegisterMode() {
      if (this.formMode == 0) {
        this.formData.taxMode = true;
        this.setTableName();
      }
    },

    /**
     * Hàm sử lý sự kiên chọn radio button Thay đổi thông tin
     * createdby ndhuy 15.09.2020
     */
    setTaxChangeInfoMode() {
      if (this.formMode == 0) {
        this.formData.taxMode = false;
        this.setTableName();
      }
    },

    /**
     * Hàm tạo tên hồ sơ dựa trên năm và loại đăng kí
     * createdby ndhuy 15.09.2020
     */
    setTableName() {
      var month = this.formData.month;
      var prefix = this.formData.taxMode
        ? "Đăng ký mã số thuế"
        : "Thay đổi thông tin mã số thuế";
      this.formData.tableName =
        prefix + " - Tháng " + month + " – Năm " + this.formData.year;
    },

    /**
     * Hàm thực hiện gọi service thêm mới hồ sơ đăng ký thuế
     * createdby ndhuy 15.09.2020
     */
    onSubmit() {
      //checkvalidate
      this.$refs["formNewTaxProfile"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById([`${Object.keys(index)[0]}`]).focus();
            }, 200);
          }
        }
        if (
          this.formMode == 1 &&
          this.formData.year < this.currentYear &&
          this.formData.year != this.taxProfile.PeriodYear
        ) {
          this.isWarningYear = true;
        }

        if (valid) {
          //Hiển thị thông báo khi năm được chọn nhỏ hơn năm hiện tại
          if (this.isWarningYear !== true) {
            this.insertTaxRegister();
          } else {
            this.buttons = [
              {
                text: "Không",
                class: "btn-grey none-border",
                callback: () => {},
              },
              {
                text: "Có",
                class: "btn-primary",
                callback: () => {
                  this.insertTaxRegister();
                },
              },
            ];
            this.$_Popup.warn(
              "Cảnh báo",
              "Năm đăng ký nhỏ hơn năm hiện tại. Bại có muốn tiếp tục?",
              this.buttons
            );
          }
        }
      });
    },

    /**
     * Gọi service thêm mới hồ sơ đăng ký thuế
     * createdby ndhuy 16.09.2020
     */
    async insertTaxRegister(_confirm = false, _NumberOfDeclaration = 1) {
      var suffix =
        _NumberOfDeclaration == 1 ? "" : " lần " + _NumberOfDeclaration;
      if (this.formMode == 1) {
        this.updateTaxRegister();
      } else if (this.formMode == 0) {
        var obj = {
          declarationName: this.formData.tableName + suffix,
          periodYear: this.formData.year,
          groupType: this.formData.taxMode
            ? this.listRegisterType[0].VALUE
            : this.listRegisterType[1].VALUE,
          taxType: 1,
          periodMonthOrQuarter: this.formData.month,
          confirm: _confirm,
        };
        this.loadingDialog=true;
        const res = await ApiDeclaration.postDeclaration(obj);
        this.loadingDialog=false;
        if (res.data.Success === true) {
          this.$emit("show-popup", {
            type: "success",
            message: "Tạo hồ sơ đăng ký thành công!",
          });

          this.$router.push({
            name: "TaxRegisterDetails",
            params: {
              id: res.data.Data,
              DeclarationName: obj.declarationName,
              DeclarationStatus: 1,
            },
          });
        } else if (res.data.Success === false && res.data.Data.Reason == 1) {
          if (res.data.Data.DeclarationStatus == 1) {
            this.buttons = [
              {
                text: "Không",
                class: "btn-grey none-border",
                callback: () => {
                  this.insertTaxRegister(
                    true,
                    res.data.Data.NumberOfDeclaration + 1
                  );
                },
              },
              {
                text: "Có",
                class: "btn-primary",
                callback: () => {
                  this.$router.push({
                    name: "TaxRegisterDetails",
                    params: {
                      id: res.data.Data.DeclarationID,
                    },
                  });
                },
              },
            ];
            this.$_Popup.warn(
              "Cảnh báo",
              "Hồ sơ <b>" +
                obj.declarationName +
                "</b>" +
                " đã được tạo. Bạn có muốn tiếp tục cập nhật hồ sơ đã có",
              this.buttons
            );
          } else {
            this.insertTaxRegister(true, res.data.Data.NumberOfDeclaration + 1);
          }
        } else {
          this.$emit("show-popup", {
            type: "false",
            message: "Tạo hồ sơ đăng ký thất bại!",
          });
        }
      }
    },

    /**
     * Kiểm tra form bị thay đổi hay chưa
     * createdby ndhuy 22.09.2020
     */
    checkEdited() {
      var check = false;
      check =
        this.taxProfile.DeclarationName != this.formData.tableName
          ? true
          : check;
      check = this.taxProfile.PeriodYear != this.formData.year ? true : check;
      check =
        this.taxProfile.PeriodMonthOrQuarter != this.formData.month
          ? true
          : check;
      return check;
    },

    /**
     * Gọi service cập nhật hồ sơ
     * createdby ndhuy 22.09.2020
     */
    async updateTaxRegister() {
      if (!this.checkEdited()) {
        this.cancelInsert();
        return;
      }
      var obj = { ...this.taxProfile };
      obj.DeclarationName = this.formData.tableName;
      obj.PeriodYear = this.formData.year;
      obj.PeriodMonthOrQuarter = this.formData.month;
      this.loadingDialog=true;
      const res = await ApiDeclaration.updateDeclaration(obj);
      this.loadingDialog=false;
      if (res.data.Success === true) {
        this.$emit("show-popup", {
          type: "success",
          message: "Cập nhật hồ sơ đăng ký thành công!",
        });
        this.cancelInsert();
        this.$emit("refresh-table", this.formData.year.toString());
      } else {
        this.$emit("show-popup", {
          type: "false",
          message: "Cập nhật hồ sơ đăng ký thất bại!",
        });
      }
    },
  },
};
</script>

<style lang="scss">
.new-tax-profile {
  .el-dialog__header {
    margin-bottom: 14px;
  }

  .label {
    line-height: 18px;
    margin-bottom: 4px;
  }

  .cb-register-type {
    margin: 0 0 16px 0 !important;

    .change-register{
      padding: 0 0 0 12px !important;
    }

    .new-register{
      padding: 0 12px 0 0;
    }
  }

  .label {
    line-height: 17px;
  }

  .el-form-item__label {
    line-height: 17px;
    margin-bottom: 4px;
    font-weight: normal;
    font-size: 13px;
  }

  .el-form-item__error {
    transform: translate(-100px, -25px);
  }

  .el-dialog__footer {
    .is-disabled {
      &:hover {
        background-color: #ffffff !important;
        color: #c0c4cc !important;
        border-color: #ebeef5;
      }
    }
  }
}
</style>