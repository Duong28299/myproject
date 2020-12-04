<template>
  <div class="form-taxpayer-wrapper">
    <div class="form-container" id="dialog-declaration">
      <div class="form-content">
        <el-form ref="form" :model="form" label-width="120px" label-position="top">
          <el-row>
            <el-col :span="24">
              <button-icon
                name="Lưu"
                type="multi-button"
                class="ml-8"
                :dataDropdown="dataButton"
                :handleClick="handleClick"
                @AddAndOut="onSubmit('form')"
                :disabled="disabled"
              ></button-icon>
              <button-icon
                name="Hủy"
                type="btn-grey"
                :handleClick="exitSubmit"
                :disabled="disabled"
              ></button-icon>
            </el-col>
          </el-row>
          <el-row :gutter="90">
            <el-col :span="24">
              <h1 class="form-title">Thông tin cá nhân</h1>
            </el-col>
            <el-col :lg="8" :md="12" :sm="24">
              <el-form-item
                label="Mã nhân viên"
                prop="employeeCode"
                :rules="[
                  {
                    required: true,
                    message: 'Mã nhân viên không được bỏ trống',
                    trigger: 'blur',
                  },
                ]"
                :show-message="validate.employeeCode"
              >
                <input-field
                  @hover="validate.employeeCode = $event"
                  type="field-input"
                  @value="getValueEmployeeCode"
                  :valueInput="form.employeeCode"
                  id="employeeCode"
                  width="100%"
                />
              </el-form-item>
              <el-form-item
                label="Họ và tên"
                prop="fullName"
                :rules="[
                  {
                    required: true,
                    message: 'Họ và tên không được bỏ trống',
                    trigger: 'blur',
                  },
                ]"
                :show-message="validate.fullName"
              >
                <input-field
                  @hover="validate.fullName = $event"
                  type="field-input"
                  :isUpercasefirst="true"
                  @value="getValueFullName"
                  :valueInput="form.fullName"
                  id="fullName"
                  width="100%"
                />
              </el-form-item>
              <el-form-item label="Vị trí/Chức vụ">
                <combo-box
                  type="label-none"
                  :filterable="true"
                  placeholder="Chọn Vị trí/Chức vụ"
                  :defaultString="form.positionName"
                  :list="positions"
                  label="label"
                  field="value"
                  @value="getValuePositionName"
                />
              </el-form-item>
              <el-form-item label="Phòng ban/Bộ phận">
                <combo-box
                  type="label-none"
                  placeholder="Chọn Phòng ban/Bộ phận"
                  :defaultString="form.organizationUnitName"
                  :list="organization"
                  label="label"
                  field="value"
                  @value="getValueOrganizationUnitName"
                />
              </el-form-item>
              <el-row :gutter="40">
                <el-col :lg="12" :md="10">
                  <el-form-item label="Ngày sinh">
                    <date-picker
                      Valueformat="MM-dd-yyyy"
                      type="date"
                      @getValue="form.dateOfBirth = $event"
                      :value="form.dateOfBirth"
                      format="dd-MM-yyyy"
                    ></date-picker>
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item label="Giới tính">
                    <el-radio-group v-model="form.gender">
                      <el-radio :label="0">Nam</el-radio>
                      <el-radio :label="1">Nữ</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
            </el-col>
            <el-col :lg="8" :md="12" :sm="24">
              <el-form-item
                label="Số CMND/CCCD/Hộ chiếu"
                prop="citizenIdentityNo"
                :rules="[
                  {
                    required: true,
                    message: 'Số CMND/CCCD/Hộ chiếu không được bỏ trống',
                    trigger: 'blur',
                  },
                ]"
                :show-message="validate.citizenIdentityNo"
              >
                <input-field
                  @hover="validate.citizenIdentityNo = $event"
                  type="field-input"
                  @value="getValueCitizenIdentityNo"
                  :valueInput="form.citizenIdentityNo"
                  id="citizenIdentityNo"
                  width="100%"
                />
              </el-form-item>
              <el-row :gutter="40">
                <el-col :span="11">
                  <el-form-item label="Ngày cấp">
                    <date-picker
                      type="date"
                      @getValue="form.citizenIdentityDate = $event"
                      Valueformat="MM-dd-yyyy"
                      :value="form.citizenIdentityDate"
                      format="dd-MM-yyyy"
                    ></date-picker>
                  </el-form-item>
                </el-col>
                <el-col :lg="13" :md="14">
                  <el-form-item label="Nơi cấp">
                    <combo-box
                      type="label-none"
                      placeholder="Chọn Nơi cấp"
                      :filterable="true"
                      :list="city"
                      label="LocationName"
                      field="ID"
                      :defaultString="form.citizenIdentityPlaceName"
                      @value="getValueCitizenIdentityPlaceName"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-form-item label="Quốc tịch">
                <combo-box
                  type="label-none"
                  placeholder="Chọn quốc tịch"
                  :filterable="true"
                  :list="nation"
                  label="LocationName"
                  field="ID"
                  :defaultString="form.nationalityID"
                  @value="getValueNationalityID"
                />
              </el-form-item>
              <el-form-item label="Điện thoại liên hệ">
                <el-input v-model="form.phone" size="medium"></el-input>
              </el-form-item>
              <el-form-item
                prop="email"
                label="Email"
                :rules="[
                  {
                    type: 'email',
                    message: 'Email chưa đúng định dạng',
                    trigger: ['blur', 'change'],
                  },
                ]"
                :show-message="validate.email"
              >
                <input-field
                  @hover="validate.email = $event"
                  type="field-input"
                  @value="getValueEmail"
                  :valueInput="form.email"
                  id="email"
                  width="100%"
                />
              </el-form-item>
            </el-col>
            <el-col :lg="8" :md="12" :sm="24">
              <el-form-item label="Mã số thuế cá nhân">
                <input-field
                  type="field-input"
                  @value="getValueTaxNo"
                  :valueInput="form.taxNo"
                  width="100%"
                />
              </el-form-item>
              <el-form-item label="Tên chủ tài khoản">
                <input-field
                  type="field-input"
                  @value="getValueBankAccountName"
                  :valueInput="form.bankAccountName"
                  width="100%"
                />
              </el-form-item>
              <el-form-item label="Số tài khoản ngân hàng">
                <input-field
                  type="field-input"
                  @value="getValueBankAccountNo"
                  :valueInput="form.bankAccountNo"
                  width="100%"
                />
              </el-form-item>
              <el-form-item label="Ngân hàng">
                <input-field
                  type="field-input"
                  @value="getValueBankName"
                  :valueInput="form.bankName"
                  width="100%"
                />
              </el-form-item>
              <el-form-item label="Chi nhánh ngân hàng">
                <combo-box
                  type="label-none"
                  placeholder="Chọn Chi nhánh ngân hàng"
                  :filterable="true"
                  :list="bank"
                  label="label"
                  field="value"
                  :defaultString="bankBranch"
                  @value="getValueBankBranch"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="90">
            <el-col :span="24">
              <h4 class="form-title">Thông tin thu nhập</h4>
            </el-col>
            <el-col :lg="8" :md="12" :sm="24">
              <el-form-item
                label="Thu nhập chịu thuế (1)"
                prop="incomeTaxable"
                :rules="[
                  {
                    required: true,
                    message: 'Thu nhập chịu thuế không được bỏ trống',
                    trigger: 'blur',
                  },
                ]"
                :show-message="validate.incomeTaxable"
              >
                <el-input
                  class="input-money"
                  v-model="incomeTaxable"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                  ref="incomeTaxable"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
              <el-form-item label="Thu nhập không chịu thuế (2)">
                <el-input
                  class="input-money"
                  v-model="incomeNotTaxable"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
              <el-form-item label="Tổng thu nhập (3) = (1) + (2)">
                <el-input
                  class="input-money"
                  readonly
                  type="text"
                  size="medium"
                  min="0"
                  v-model="totalIncome"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
              <el-form-item>
                <el-row style="margin-bottom : 10px">
                  <el-col :span="18">
                    <span>Cá nhân cư trú</span>
                  </el-col>
                  <el-col :span="6" class="input-checkbox">
                    <el-checkbox v-model="form.individualResident" name="type"></el-checkbox>
                  </el-col>
                </el-row>
                <el-row>
                  <el-col :span="18" style="margin-bottom : 10px">
                    <span>Hợp đồng lao động từ 3 tháng trở lên</span>
                  </el-col>
                  <el-col :span="6" class="input-checkbox">
                    <el-checkbox name="type" v-model="form.contractOffical"></el-checkbox>
                  </el-col>
                </el-row>
                <el-row v-if="form.contractOffical === false">
                  <el-col :span="18">
                    <span>Có khấu trừ thuế TNCN vào thu nhập không?</span>
                  </el-col>
                  <el-col :span="6" class="input-checkbox">
                    <el-checkbox v-model="form.isTaxable" name="type"></el-checkbox>
                  </el-col>
                </el-row>
              </el-form-item>
            </el-col>
            <el-col :lg="8" :md="12" :sm="24">
              <el-form-item label="Bảo hiểm phải nộp (4)">
                <el-input
                  class="input-money"
                  v-model="insurancePayment"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
              <el-form-item label="Giảm trừ khác (5)">
                <el-input
                  class="input-money"
                  v-model="otherReduction"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
              <el-form-item label="Số người phụ thuộc (6)">
                <el-input
                  class="input-money"
                  v-model="dependentNumber"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                >
                  <template slot="append">Người</template>
                </el-input>
              </el-form-item>
              <el-form-item label="Giảm trừ 1 người phụ thuộc (7)">
                <el-input
                  class="input-money"
                  readonly
                  v-model="dependentReduction"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
              <el-form-item
                label="Giảm trừ bản thân (8)"
                v-if="form.contractOffical && form.individualResident"
              >
                <el-input
                  class="input-money"
                  readonly
                  v-model="selfReduction"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
              <el-form-item label="Tổng giảm trừ gia cảnh (9) = (6) * (7) + (8)">
                <el-input
                  class="input-money"
                  readonly
                  v-model="totalReduction"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :lg="8" :md="12" :sm="24">
              <el-form-item readonly label="Biểu thuế áp dụng">
                <el-input v-model="TariffsApply" size="medium"></el-input>
              </el-form-item>
              <el-form-item label="Tổng thu nhập tính thuế (10) = (1) - (4) - (5) - (9)">
                <el-input
                  class="input-money"
                  readonly
                  type="text"
                  size="medium"
                  min="0"
                  v-model="totalIncomeTaxable"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
              <el-form-item label="Thuế thu nhập cá nhân khấu trừ">
                <el-input
                  class="input-money"
                  readonly
                  v-model="totalTaxWithHeld"
                  type="text"
                  size="medium"
                  min="0"
                  placeholder="0"
                >
                  <template slot="append">VNĐ</template>
                </el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="border-top">
            <el-col :span="24">
              <!-- <button-icon
                name="Lưu"
                type="multi-button"
                class="ml-8"
                :dataDropdown="dataButton"
                @AddAndOut="onSubmit('form')"
              ></button-icon>-->
              <button-icon
                name="Lưu"
                type="multi-button"
                class="ml-8"
                :dataDropdown="dataButton"
                :handleClick="handleClick"
                @AddAndOut="onSubmit('form')"
                :disabled="disabled"
              ></button-icon>
              <button-icon
                name="Hủy"
                type="btn-grey"
                :handleClick="exitSubmit"
                :disabled="disabled"
              ></button-icon>
            </el-col>
          </el-row>
        </el-form>
      </div>
    </div>
    <!-- Dialog thông báo -->
  </div>
</template>

<script>
import DatePicker from "@/components/date-time-pickers";
import Button from "@/components/buttons";
import TaxPayer from "@/api/etax/tax-payer";
import TaxInfo from "@/api/etax/tax-info";
import EmployeeInfo from "@/api/etax/temporary-tax-detail";
import { getTaxInfo, getSelfReduce } from "@/enums/tax-info";
import { formatMoney2 } from "@/utils/index.js";
import { formatDate, sortData } from "@/utils/index.js";
import Inputfield from "@/components/inputs";
import ComboBox from "@/components/select";

import locationService from "@/api/etax/locations";

export default {
  name: "CreateEmployee",
  components: {
    "date-picker": DatePicker,
    "button-icon": Button,
    "input-field": Inputfield,
    "combo-box": ComboBox,
  },
  data() {
    return {
      validate: {
        fullName: false,
        employeeCode: false,
        citizenIdentityNo: false,
        email: false,
        incomeTaxable: false,
      },
      bankBranch: "", //Trường đặt tạm cho
      incomeTaxable: "", //Trường text lấy dữ liệu cho form.incomeTaxable
      incomeNotTaxable: "", //Trường text lấy dữ liệu cho form.incomeNotTaxable
      insurancePayment: "", //Trường text lấy dữ liệu cho form.insurancePayment
      otherReduction: "", // Trường text lấy dữ liệu cho form.otherReduction
      dependentNumber: "", // Trường text lấy dữ liệu cho form.dependentNumber
      totalIncome: "", // Trường text lấy dữ liệu cho form.totalIncome
      totalReduction: "", // Trường text lấy dữ liệu cho form.totalReduction
      dependentReduction: "", // Trường text lấy dữ liệu cho form.dependentReduction
      selfReduction: "", // Trường text lấy dữ liệu cho form.selfReduction
      totalIncomeTaxable: "", // Trường text lấy dữ liệu cho form.totalIncomeTaxable
      DateFormant: "",
      form: {
        // đối tượng gửi lên service
        temporaryTaxID: "",
        incomeNotTaxable: 0,
        incomeTaxable: "",
        insurancePayment: 0,
        otherReduction: 0,
        dependentNumber: 0,
        dependentReduction: 0,
        selfReduction: 0,
        totalReduction: "",
        totalIncomeTaxable: "",
        totalIncome: 0,
        taxInfoID: 0,
        isTaxable: true,
        totalTaxWithHeld: "",
        employeeCode: "",
        employeeID: "",
        firstName: "",
        lastName: "",
        fullName: "",
        representFor: "",
        taxNo: "",
        email: "",
        phone: "",
        positionName: "",
        dateOfBirth: "",
        gender: 0,
        nationalityID: "",
        organizationUnitName: "",
        organizationID: "2ef7003b-c2a3-11ea-9491-5203cfc96bc9",
        ethnicID: 0,
        citizenIdentityNo: "",
        citizenIdentityPlaceName: "",
        citizenIdentityDate: "",
        bankName: "",
        bankAccountNo: "",
        bankAccountName: "",
        contractOffical: true,
        individualResident: true,
        temporaryTaxDetailID: "",
        TariffsApply: "",
      },
      taxInfo: "",
      positions: [
        // Fake dữ liệu chức vụ
        {
          label: "Giám đốc",
          value: "Giám đốc",
        },
        {
          label: "Nhân viên lập trình",
          value: "Nhân viên lập trình",
        },
      ],
      organization: [
        // Fake dữ liệu phòng ban
        {
          label: "Phòng giám đốc",
          value: "Phòng giám đốc",
        },
        {
          label: "Phòng đào tạo",
          value: "Phòng đào tạo",
        },
      ],
      city: [], // danh sách tên tỉnh/TP
      bank: [
        // Fake dữ liệu ngân hàng
        {
          label: "Hà Nội",
          value: "Hà Nội",
        },
        {
          label: "Hồ Chí Minh",
          value: "Hồ Chí Minh",
        },
      ],
      nation: [], // danh sách tên quốc gia
      dataButton: [
        // Thông tin dropdown button
        {
          name: "Lưu và đóng",
          nameVal: "AddAndOut",
        },
        {
          name: "Lưu và thêm mới",
        },
      ],
      organizationID: "2ef7003b-c2a3-11ea-9491-5203cfc96bc9", // ID tổ chức
      buttonDialog: [],
      disabled: false,
    };
  },
  directives: {
    focus: {
      // directive definition
      inserted: function (el) {
        el.focus();
      },
    },
  },
  methods: {
    checkValue(str, max) {
      if (str.charAt(0) !== "0" || str == "00") {
        var num = parseInt(str);
        if (isNaN(num) || num <= 0 || num > max) num = 1;
        str =
          num > parseInt(max.toString().charAt(0)) && num.toString().length == 1
            ? "0" + num
            : num.toString();
      }
      return str;
    },
    /**
     * method lấy danh sách quốc gia
     * created by: N.T. Dũng 28.07.2020
     */
    async getNational() {
      this.nation = await locationService.getLocation(0, "");
      sortData(this.nation);
    },

    /**
     * method lấy danh sách tỉnh/thành phố thuộc VN
     * created by: N.T. Dũng 28.07.2020
     */
    async getCity() {
      this.city = await locationService.getLocation(1, "VN");
      sortData(this.city);
    },

    handleClick() {
      this.onSubmit("form");
    },
    /**
     * Lấy giá trị từ các trường input vào @param form
     * Created by: CVCuong - 28/7/2020
     */
    getValueBankName(val) {
      this.form.bankName = val;
    },
    getValueBankBranch(val) {
      this.form.bankName = val;
    },
    getValueBankAccountNo(val) {
      this.form.bankAccountNo = val;
    },
    getValueBankAccountName(val) {
      this.form.bankAccountName = val;
    },
    getValueTaxNo(val) {
      this.form.taxNo = val;
    },
    getValueEmail(val) {
      this.form.email = val;
    },
    getValueCitizenIdentityNo(val) {
      this.form.citizenIdentityNo = val;
    },
    getValueEmployeeCode(val) {
      this.form.employeeCode = val;
    },
    getValueFullName(val) {
      this.form.fullName = val;
    },
    getValuePositionName(val) {
      this.form.positionName = val;
    },
    getValueOrganizationUnitName(val) {
      this.form.organizationUnitName = val;
    },
    getValueCitizenIdentityPlaceName(val) {
      this.form.CitizenIdentityPlaceName = val;
    },
    getValueNationalityID(val) {
      this.form.NationalityID = val;
    },

    /**
     * Thực hiện service thêm người lao động
     * Created by : NVHUNG - 16/7/2020
     * Modified by: N.T. Dũng 21.07.2020
     * Content modification: thêm phần xử lý sự kiện sửa người nộp thuế
     */

    async onSubmit(form) {
      /**
       * Format dữ liệu đầu vào
       * Created by : NVHUNG - 16/7/2020
       */
      this.$refs[form].validate(async (valid, index) => {
        if (Object.keys(index)[0] != null) {
          document.getElementById([`${Object.keys(index)[0]}`]).focus();
        }
        if (valid) {
          this.disabled = true;
          // Convert dữ liệu đầu vào hợp lệ
          this.form.isTaxable = this.convertBoolValue(this.form.isTaxable);
          this.form.contractOffical = this.convertBoolValue(
            this.form.contractOffical
          );
          this.form.individualResident = this.convertBoolValue(
            this.form.individualResident
          );
          if (this.form.dateOfBirth === "") {
            delete this.form.dateOfBirth;
          }
          if (this.form.citizenIdentityDate === "") {
            delete this.form.citizenIdentityDate;
          }
          this.form.TariffsApply = this.TariffsApply;
          this.form.totalTaxWithHeld = this.convertStringtoNumber(
            this.totalTaxWithHeld
          );

          // check điều kiện nếu có idTemDetail thì sửa, không thì thêm mới
          if (this.$route.params.idTempDetail) {
            // phần xử lý sửa thông tin người nộp thuế
            const editResponse = await TaxPayer.editTaxPayer(this.form);
            if (editResponse.data.Success === true) {
              this.buttonDialog = [
                {
                  text: "Đóng",
                  class: "btn-primary",
                  callback: this.closeDialog,
                },
              ];
              this.$_Popup.success(
                "Thành công",
                "Sửa người lao động thành công",
                this.buttonDialog
              );
            }
            if (editResponse.data.Success === false) {
              this.buttonDialog = [
                {
                  text: "Đóng",
                  class: "btn-primary",
                  callback: this.closeDialog,
                },
              ];
              this.$_Popup.error(
                "Thất bại",
                "Sửa người lao động thất bại",
                this.buttonDialog
              );
            }
          } else {
            // phần xử lý thêm mới người nộp thuế
            const res = await TaxPayer.createTaxPayer(this.form);

            if (res.data.Success === true) {
              this.buttonDialog = [
                {
                  text: "Đóng",
                  class: "btn-primary",
                  callback: this.closeDialog,
                },
              ];
              this.$_Popup.success(
                "Thành công",
                "Thêm người lao động thành công",
                this.buttonDialog
              );
            } else if (res.data.Success === false) {
              if (res.data.Data.Message.includes("Duplicate")) {
                this.buttonDialog = [
                  {
                    text: "Đóng",
                    class: "btn-primary",
                    callback: () => {},
                  },
                ];
                this.$_Popup.error(
                  "Thất bại",
                  "Trùng mã nhân viên, vui lòng thử lại",
                  this.buttonDialog
                );
              } else {
                this.buttonDialog = [
                  {
                    text: "Đóng",
                    class: "btn-primary",
                    callback: this.closeDialog,
                  },
                ];
                this.$_Popup.error(
                  "Thất bại",
                  "Thêm người lao động thất bại",
                  this.buttonDialog
                );
              }
            }
          }
        } else {
          return false;
        }
      });
    },

    /**
     * method convert dữ liệu từ chuỗi thành số
     * created by: NVHUNG 23.07.2020
     */
    convertStringtoNumber(value) {
      return (
        value
          .split("")
          .filter((x) => x !== ",")
          .join("") * 1
      );
    },

    /**
     * method convert dữ liệu 0/1 thành true/false
     * created by: N.T. Dũng 21.07.2020
     */
    convertBoolValue(data) {
      if (data === 1) {
        return true;
      } else if (data === 0) {
        return false;
      } else if (data === true) {
        return 1;
      } else if (data === false) {
        return 0;
      }
    },

    /**
     * Hàm đóng popup
     */
    closeDialog() {
      this.$router.go(-1);
    },
    /**
     * Service lấy thông tin kiểu tính thuế
     * Created by : NVHUNG - 16/7/2020
     */
    async GetTaxInfoService() {
      this.form.temporaryTaxID = this.$route.params.id;
      const TemporaryTaxType = this.$route.params.id;
      const data = await TaxInfo.getTaxInfo(
        TemporaryTaxType,
        this.organizationID
      );

      if (data === undefined) {
        this.buttonDialog = [
          {
            text: "Đóng",
            class: "btn-primary",
            callback: this.closeDialog,
          },
        ];
        this.$_Popup.error(
          "Thất bại",
          "Lỗi hệ thống vui lòng thử lại",
          this.buttonDialog
        );
      } else if (data.Data.length !== 0) {
        this.taxInfo = data.Data;
        if (this.taxInfo) {
          const SelfReduce = getSelfReduce(data.Data);
          this.form.selfReduction = SelfReduce.SelfReduction;
          this.selfReduction = formatMoney2(
            SelfReduce.SelfReduction.toString(),
            ","
          );
          this.form.dependentReduction = SelfReduce.DependentReduction;
          this.dependentReduction = formatMoney2(
            SelfReduce.DependentReduction.toString(),
            ","
          );
        } else {
          this.form.selfReduction = 0;
          this.form.dependentReduction = 0;
        }
      } else {
        this.buttonDialog = [
          {
            text: "Đóng",
            class: "btn-primary",
            callback: this.closeDialog,
          },
        ];
        this.$_Popup.error(
          "Thất bại",
          "Hiện tại chỉ cho phép thêm người lao động Bảng tính thuế TNCN từ tiền lương, tiền công",
          this.buttonDialog
        );
      }
    },

    /**
     * lấy thông tin của nhân viên nộp thuế cần sửa từ service trả về
     * created by: N.T. Dũng 20.07.2020
     */
    async GetInfoEmployee() {
      let idDetail = this.$route.params.idTempDetail;
      const employeeInfo = await EmployeeInfo.getTemporaryDetailDataByID(
        idDetail
      );
      this.mapDataEdit(employeeInfo.data.Data);
    },

    /**
     * map dữ liệu trả về thông tin của người nộp thuế
     * created by: N.T. Dũng 17.07.2020
     */
    mapDataEdit(data) {
      let values = Object.fromEntries(
        // convert to array, map, and then fromEntries gives back the object
        Object.entries(data).map(([key, value]) => [
          key.charAt(0).toLowerCase() + key.slice(1),
          value,
        ])
      );
      // gán các trường tính toán và convert để hiển thị dữ liệu
      this.incomeNotTaxable = values.incomeNotTaxable.toString();
      this.incomeTaxable = values.incomeTaxable.toString();
      this.insurancePayment = values.insurancePayment.toString();
      this.otherReduction = values.otherReduction.toString();
      this.dependentNumber = values.dependentNumber.toString();
      // gán form bằng giá trị của object values
      this.form = values;
      // convert các trường true/false thành 0/1
      this.form.individualResident = this.convertBoolValue(
        values.individualResident
      );
      this.form.contractOffical = this.convertBoolValue(values.contractOffical);
      this.form.isTaxable = this.convertBoolValue(values.isTaxable);
      // check ngày sinh và ngày cấp không null
      if (this.form.dateOfBirth && this.form.citizenIdentityDate) {
        this.form.dateOfBirth = formatDate(values.dateOfBirth, "mm-dd-yyyy");
        this.form.citizenIdentityDate = formatDate(
          values.citizenIdentityDate,
          "mm-dd-yyyy"
        );
      }
    },

    /**
     *  Format dữ liệu tiền cho các trường
     *  Created by : NVHUNG - 21/7/2020
     */
    settotalIncome() {
      this.form.totalIncome =
        this.form.incomeTaxable * 1 + this.form.incomeNotTaxable * 1;
      var val = this.form.totalIncome.toString();
      this.totalIncome = formatMoney2(val, ",");
    },
    settotalReduction() {
      this.form.totalReduction =
        this.form.dependentNumber * this.form.dependentReduction +
        this.form.selfReduction * 1;
      var val = this.form.totalReduction.toString();
      this.totalReduction = formatMoney2(val, ",");
    },
    settotalIncomeTaxable() {
      let ToInTax =
        this.form.incomeTaxable -
        this.form.insurancePayment -
        this.form.otherReduction -
        this.form.totalReduction;
      if (ToInTax < 0) ToInTax = 0;
      this.form.totalIncomeTaxable = ToInTax;
      var val = this.form.totalIncomeTaxable.toString();
      this.totalIncomeTaxable = formatMoney2(val, ",");
    },

    /**
     * Hàm bắt nút hủy
     */
    exitSubmit() {
      this.buttonDialog = [
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
        {
          text: "Có",
          class: "btn-primary",
          callback: this.closeDialog,
        },
      ];
      this.$_Popup.warn(
        "Cảnh báo",
        "Thông tin vừa nhập liệu sẽ không được lưu lại. Bạn có chắc chắn muốn thoát không?",
        this.buttonDialog
      );
    },
  },

  async created() {
    this.getNational(); // call method lấy danh sách quốc gia
    this.getCity(); // call method lấy danh sách tỉnh/TP

    /**
     * Lấy thông tin phần trăm thuế
     * Created by : NVHUNG - 16/7/2020
     */
    await this.GetTaxInfoService();

    /**
     * binding thông tin người nộp thuế lên các trường tương ứng ở trang sửa người nộp thuế
     * created by: N.T. Dũng 17.07.2020
     */
    if (this.$route.params.idTempDetail) {
      this.GetInfoEmployee();
    }
  },

  /**
   * Lắng nghe input format tiền.
   * Created by : NVHUNG - 21/7/2020
   */
  watch: {
    incomeTaxable(val) {
      if (isNaN(this.convertStringtoNumber(val)) || val.charAt(0) === "0") {
        return (this.incomeTaxable = this.form.incomeTaxable = 0);
      }
      this.incomeTaxable = formatMoney2(val, ",");
      this.form.incomeTaxable = this.convertStringtoNumber(this.incomeTaxable);

      this.settotalIncome();
      this.settotalIncomeTaxable();
    },
    incomeNotTaxable(val) {
      if (isNaN(this.convertStringtoNumber(val)) || val.charAt(0) === "0") {
        return (this.incomeNotTaxable = 0);
      }
      this.incomeNotTaxable = formatMoney2(val, ",");
      this.form.incomeNotTaxable = this.convertStringtoNumber(
        this.incomeNotTaxable
      );
      this.settotalIncome();
    },
    insurancePayment(val) {
      if (isNaN(this.convertStringtoNumber(val)) || val.charAt(0) === "0") {
        return (this.insurancePayment = 0);
      }
      this.insurancePayment = formatMoney2(val, ",");
      this.form.insurancePayment = this.convertStringtoNumber(
        this.insurancePayment
      );
      this.settotalIncomeTaxable();
    },
    otherReduction(val) {
      if (isNaN(this.convertStringtoNumber(val)) || val.charAt(0) === "0") {
        return (this.otherReduction = 0);
      }
      this.otherReduction = formatMoney2(val, ",");
      this.form.otherReduction = this.convertStringtoNumber(
        this.otherReduction
      );
      this.settotalIncomeTaxable();
    },
    dependentNumber(val) {
      if (isNaN(this.convertStringtoNumber(val)) || val.charAt(0) === "0") {
        return (this.dependentNumber = 0);
      }
      this.dependentNumber = formatMoney2(val, ",");
      this.form.dependentNumber = this.convertStringtoNumber(
        this.dependentNumber
      );
      this.settotalReduction();
      this.settotalIncomeTaxable();
    },
    dependentReduction() {
      this.settotalReduction();
    },
  },

  /**
   * Lắng nghe và xử lý các sự thay đổi ràng buộc các trường cần tính toán
   * Created by : NVHUNG - 16/7/2020
   */
  computed: {
    // Trường biểu thuế áp dụng
    TariffsApply: {
      get: function () {
        /**
         * Check lỗi server TaxInfo
         * Created by : NVHUNG - 17/7/2020
         */
        if (!this.taxInfo || this.taxInfo.length === 0) {
          return "";
        }
        const taxPayer = {
          individualResident: this.form.individualResident ? 1 : 0,
          contractOffical: this.form.contractOffical ? 1 : 0,
        };
        const taxInfoRes = getTaxInfo(
          this.form.totalIncomeTaxable,
          this.taxInfo,
          taxPayer
        ).taxPercent;
        return taxInfoRes;
      },
    },
    // Trường thuế nhu nhập cá nhân khấu trừ
    totalTaxWithHeld: {
      get: function () {
        /**
         * Check lỗi server TaxInfo
         * Created by : NVHUNG - 17/7/2020
         */
        if (!this.taxInfo || this.taxInfo.length === 0) return 0;
        const taxPayer = {
          individualResident: this.form.individualResident ? 1 : 0,
          contractOffical: this.form.contractOffical ? 1 : 0,
        };
        const totalTaxRes = getTaxInfo(
          this.form.totalIncomeTaxable,
          this.taxInfo,
          taxPayer
        ).money;
        return formatMoney2(totalTaxRes.toString(), ",");
      },
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";

.input-checkbox {
  text-align: right;
}

.form {
  &-taxpayer-wrapper {
    padding: 20px 30px 50px 30px;
    background-color: #fff;

    .input-money .el-input__inner {
      text-align: right;
      border-top-right-radius: 0;
      border-bottom-right-radius: 0;
    }

    .el-form--label-top .el-form-item__label {
      padding: 0 0 4px 0;
    }

    .el-form-item__content .form-unit {
      position: absolute;
      right: -40px;
      top: 5px;
    }
    .fl-r {
      float: right;
    }

    .el-form-item.el-form-item--medium {
      margin-bottom: 16px;
    }

    .border-top {
      margin-top: 40px;
      border-top: 1px solid #ccc;
      padding-top: 20px;
    }
    .el-date-editor.el-input,
    .el-date-editor.el-input__inner {
      width: 100% !important;
    }

    .el-input-group__append {
      border-top-right-radius: 8px;
      border-bottom-right-radius: 8px;
    }

    #dialog-declaration .el-form-item__label {
      position: relative;
    }

    .el-form-item.is-required:not(.is-no-asterisk)
      > .el-form-item__label:before,
    .el-form-item.is-required:not(.is-no-asterisk)
      .el-form-item__label-wrap
      > .el-form-item__label:before {
      position: absolute;
      right: -13px;
    }
  }

  &-title {
    font-size: 20px;
    letter-spacing: 0.5px;
    border-bottom: 2px solid $primary;
    height: 30px;
    display: inline-block;
  }
}

@media only screen and (max-width: 1366px) {
  .form {
    &-taxpayer-wrapper {
      .el-radio {
        margin-right: 8px !important;
      }

      .el-radio__label {
        font-size: 13px !important;
      }

      .el-date-editor.el-input {
        width: 125px !important;
      }
    }
  }
}
</style>
