<template>
  <div class="new-employee">
    <el-form
      ref="formData"
      :model="formData"
      label-width="120px"
      label-position="top"
    >
      <dialogel
        id="tax-period-new-employee-dialog"
        :loading="loadingDialog"
        :titleDialog="title"
        :dialogFormVisible="dialogFormVisible"
        v-if="dialogFormVisible"
        @close-dialog="getClose"
        xl="8,9"
        lg="8,9"
        md="9,10"
        sm="4,3"
        xs="7,8"
        :close-on-click-modal="false"
        :isWarnClose="false"
      >
        <template v-slot:body>
          <div class="tab-bar">
            <button-icon
              type="btn-tab"
              :active="tab1"
              name="1. Thông tin cá nhân"
              :handleClick="changetab1"
            />
            <button-icon
              type="btn-tab"
              :active="tab2"
              name="2. Thông tin thu nhập"
              :handleClick="changetab2"
            />
          </div>
          <el-row :gutter="24" v-show="tab1" style="padding-top: 12px">
            <el-col :span="12">
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item label="Mã nhân viên" prop="EmployeeCode">
                    <input-field
                      type="field-input"
                      @value="getValueEmployeeCode"
                      :valueInput="formData.EmployeeCode"
                      id="EmployeeCode"
                      width="100%"
                      placeholder="Nhập mã nhân viên"
                      :maxlength="25"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="Họ và tên"
                    prop="FullName"
                    :rules="[
                      {
                        required: true,
                        message: 'Họ và tên không được bỏ trống.',
                        trigger: ['blur', 'change'],
                      },
                    ]"
                    :show-message="validate.FullName"
                  >
                    <input-field
                      @hover="validate.FullName = $event"
                      type="field-input"
                      onlyText
                      :isUpercasefirst="true"
                      @value="getValueFullName"
                      :valueInput="formData.FullName"
                      id="FullName"
                      width="100%"
                      placeholder="Nhập họ tên người nộp thuế"
                      :maxlength="125"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row :gutter="24" style="height: 75px">
                <el-col :span="12" :xs="24">
                  <el-form-item label="Ngày sinh">
                    <date-picker
                      type="date"
                      @getValue="GetDateOfBirth"
                      :value="formData.DateOfBirth"
                      width="100%"
                      placeholder="_ _ /_ _ /_ _ _ _"
                    ></date-picker>
                  </el-form-item>
                </el-col>
                <el-col :span="12" :xs="24">
                  <el-form-item label="Giới tính">
                    <el-radio-group v-model="formData.Gender">
                      <el-radio :label="0">Nam</el-radio>
                      <el-radio :label="1">Nữ</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item label="Loại giấy tờ" prop="IndentityType">
                    <combo-box
                      selectID="indentityType"
                      type="label-none-v2"
                      placeholder="Chọn loại giấy tờ"
                      :list="dboptions"
                      label="DBOptionName"
                      field="DBOptionValue"
                      :defaultString="IndentityType"
                      @value="getValueIndentityType"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row :gutter="24">
                <el-col
                  :lg="12"
                  :md="12"
                  :sm="14"
                  v-if="IndentityType == '1010'"
                >
                  <el-form-item
                    label="Chứng minh thư nhân dân"
                    prop="CitizenIdentityNo"
                    :rules="[
                      {
                        required: true,
                        message: 'Chứng minh thư nhân dân không được bỏ trống.',
                        trigger: ['blur', 'change'],
                      },
                    ]"
                    :show-message="validate.CitizenIdentityNo"
                  >
                    <input-field
                      @hover="validate.CitizenIdentityNo = $event"
                      type="field-input"
                      @value="getValueCitizenIdentityNo"
                      :valueInput="formData.CitizenIdentityNo"
                      id="CitizenIdentityNo"
                      width="100%"
                      placeholder="Nhập Chứng minh thư nhân dân"
                      :number="true"
                    />
                  </el-form-item>
                </el-col>

                <el-col
                  :lg="12"
                  :md="12"
                  :sm="14"
                  v-else-if="IndentityType == '1020'"
                >
                  <el-form-item
                    label="Hộ chiếu"
                    prop="CitizenIdentityNo"
                    :rules="[
                      {
                        required: true,
                        message: 'Hộ chiếu không được bỏ trống',
                        trigger: ['blur', 'change'],
                      },
                    ]"
                    :show-message="validate.CitizenIdentityNo"
                  >
                    <input-field
                      @hover="validate.CitizenIdentityNo = $event"
                      type="field-input"
                      @value="getValueCitizenIdentityNo"
                      :valueInput="formData.CitizenIdentityNo"
                      id="CitizenIdentityNo"
                      width="100%"
                      number
                      placeholder="Nhập Hộ chiếu"
                    />
                  </el-form-item>
                </el-col>

                <el-col
                  :lg="12"
                  :md="12"
                  :sm="14"
                  v-else-if="IndentityType == '2080'"
                >
                  <el-form-item
                    label="Căn cước công dân"
                    prop="CitizenIdentityNo"
                    :rules="[
                      {
                        required: true,
                        message: 'Căn cước công dân không được bỏ trống.',
                        trigger: ['blur', 'change'],
                      },
                    ]"
                    :show-message="validate.CitizenIdentityNo"
                  >
                    <input-field
                      @hover="validate.CitizenIdentityNo = $event"
                      type="field-input"
                      @value="getValueCitizenIdentityNo"
                      :valueInput="formData.CitizenIdentityNo"
                      id="CitizenIdentityNo"
                      width="100%"
                      placeholder="Nhập Căn cước công dân"
                      :number="true"
                    />
                  </el-form-item>
                </el-col>

                <el-col :lg="12" :md="12" :sm="14" v-else></el-col>

                <el-col :lg="12" :md="12" :sm="10">
                  <el-form-item label="Quốc tịch">
                    <combo-box
                      type="label-none"
                      placeholder="Chọn/Nhập quốc tịch"
                      :filterable="true"
                      :list="nation"
                      label="NationalityName"
                      field="NationalityID"
                      :defaultNumber="formData.NationalityID"
                      @value="getValueNationalityID"
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row :gutter="24">
                <el-col :span="12" :xs="24">
                  <el-form-item label="Ngày cấp">
                    <date-picker
                      type="date"
                      @getValue="formData.CitizenIdentityDate = $event"
                      :value="formData.CitizenIdentityDate"
                      width="100%"
                      placeholder="_ _ /_ _ /_ _ _ _"
                    ></date-picker>
                  </el-form-item>
                </el-col>
                <el-col :span="12" :xs="24">
                  <el-form-item label="Nơi cấp">
                    <combo-box
                      type="label-none"
                      placeholder="Chọn/Nhập nơi cấp"
                      :filterable="true"
                      :list="PlaceOfIssue"
                      label="PlaceOfIssueName"
                      field="PlaceOfIssueID"
                      :defaultNumber="formData.CitizenIdentityPlaceID"
                      @value="getValueCitizenIdentityPlaceName"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-col>
            <el-col :span="12">
              <el-row :gutter="24">
                <el-col :span="12" :xs="24">
                  <el-form-item label="Phòng ban/Bộ phận">
                    <tree-select
                      filterable
                      :value="organizationUnit"
                      :arraySetting="arraySetting"
                      :data="organizationUnit"
                      @change="getOrganizationUnitValue"
                      :checkStrictly="false"
                      :multiple="false"
                    >
                    </tree-select>
                  </el-form-item>
                </el-col>
                <el-col :span="12" :xs="24">
                  <el-form-item label="Vị trí/Chức vụ">
                    <combo-box
                      type="label-none"
                      :filterable="true"
                      :allowCreate="true"
                      placeholder="Nhâp Vị trí/Chức vụ"
                      :defaultString="formData.PositionID"
                      :list="positions"
                      label="PositionName"
                      field="PositionID"
                      @value="getValuePositionName"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row :gutter="24">
                <el-col :span="12" :xs="24">
                  <el-form-item label="Điện thoại liên hệ">
                    <input-field
                      type="field-input"
                      placeholder="Nhập số điện thoại"
                      :maxlength="20"
                      @value="getValuePhone"
                      :valueInput="formData.Phone"
                    ></input-field>
                  </el-form-item>
                </el-col>
                <el-col :span="12" :xs="24">
                  <el-form-item
                    prop="Email"
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
                      :valueInput="formData.Email"
                      id="Email"
                      width="100%"
                      placeholder="Nhập email"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col>
                  <el-form-item label="Ngân hàng">
                    <combo-box
                      type="label-none"
                      :filterable="true"
                      placeholder="Nhập/Chọn ngân hàng"
                      :defaultString="formData.BankName"
                      :list="banks"
                      label="BankName"
                      field="BankCode"
                      @value="getValueBankName"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col>
                  <el-form-item label="Chi nhánh ngân hàng">
                    <input-field
                      id="BankBranch"
                      type="field-input"
                      @value="getValueBankBranch"
                      :valueInput="formData.BankBranch"
                      width="100%"
                      placeholder="Nhập chi nhánh ngân hàng"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item label="Số tài khoản">
                    <input-field
                      type="field-input"
                      @value="getValueBankAccountNo"
                      :valueInput="formData.BankAccountNo"
                      width="100%"
                      id="BankNo"
                      placeholder="Nhập số tài khoản"
                      :maxlength="50"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12" :xs="24">
                  <el-form-item
                    label="Mã số thuế cá nhân"
                    :error="getTaxNoError()"
                  >
                    <input-field
                      type="field-input"
                      :valueInput="formData.TaxNo"
                      @value="getValueTaxNo"
                      :maxlength="10"
                      placeholder="Nhập mã số thuế cá nhân"
                      width="100%"
                      :number="true"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col>
                  <el-form-item label="Chủ tài khoản" @keyup.tab="tabEvent">
                    <input-field
                      type="field-input"
                      @value="getValueBankAccountName"
                      :valueInput="formData.BankAccountName"
                      width="100%"
                      placeholder="Nhập tên chủ tài khoản"
                      @keyup.tab="tabEvent"
                      :maxlength="250"
                      id="BankAccountName"
                      :isUpercasefirst="true"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-col>
          </el-row>
          <el-row :gutter="24" v-show="tab2" style="padding-top: 12px">
            <el-col :span="12">
              <el-form-item label="Thu nhập chịu thuế (1)">
                <input-number
                  type="input-append"
                  :valueInput="formData.IncomeTaxable"
                  @value="getIncomeTaxable"
                  append="VNĐ"
                  :money="true"
                  :id="'IncomeTaxable'"
                />
              </el-form-item>
              <el-form-item label="Thu nhập không chịu thuế (2)">
                <input-number
                  type="input-append"
                  :valueInput="formData.IncomeNotTaxable"
                  @value="getIncomeNotTaxable"
                  append="VNĐ"
                  :money="true"
                  :id="'IncomeNotTaxable'"
                />
              </el-form-item>
              <el-form-item>
                <el-row>
                  <el-col :span="12">
                    <span
                      class="el-form-item__label"
                      style="margin-right: 3.5px"
                      >Tổng thu nhập (3)</span
                    >
                    <img
                      src="@/icons/Group 18553.png"
                      alt="info"
                      style="position: absolute; top: 11px"
                    />
                  </el-col>
                  <el-col :span="12" style="text-align: right">
                    {{ TotalIncome }}
                    <span class="currency">VNĐ</span>
                  </el-col>
                </el-row>
              </el-form-item>
              <el-form-item label="Bảo hiểm phải nộp (4)">
                <input-number
                  type="input-append"
                  :valueInput="formData.InsurancePayment"
                  @value="getInsurancePayment"
                  append="VNĐ"
                  :money="true"
                  :id="'InsurancePayment'"
                />
              </el-form-item>
              <el-form-item label="Giảm trừ khác (5)">
                <input-number
                  type="input-append"
                  :valueInput="formData.OtherReduction"
                  @value="getOtherReduction"
                  append="VNĐ"
                  :money="true"
                  :id="'OtherReduction'"
                />
              </el-form-item>
              <el-form-item>
                <el-checkbox
                  class="input-checkbox"
                  v-model="formData.IndividualResident"
                  name="type"
                ></el-checkbox>
                <span class="checkbox-label">Cá nhân cư trú</span>
              </el-form-item>
              <el-form-item
                label="Hợp đồng lao động ?"
                v-if="formData.IndividualResident"
              >
                <el-radio-group v-model="formData.ContractOffical">
                  <el-radio :label="0">Dưới 3 tháng</el-radio>
                  <el-radio :label="1">Từ 3 tháng trở lên</el-radio>
                </el-radio-group>
              </el-form-item>
              <el-form-item
                v-if="
                  formData.IndividualResident && formData.ContractOffical == 0
                "
              >
                <el-checkbox
                  v-model="formData.IsTaxable"
                  name="type"
                ></el-checkbox>
                <span class="checkbox-label"
                  >Có khấu trừ thuế TNCN vào thu nhập không?</span
                >
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-row>
                <el-col :span="12" :xs="24">
                  <el-form-item
                    label="Số người phụ thuộc (6)"
                    v-if="
                      formData.ContractOffical && formData.IndividualResident
                    "
                  >
                    <input-number
                      type="input-append"
                      :valueInput="formData.DependentNumber"
                      @value="getDependentNumber"
                      append="Người"
                      :money="true"
                      :id="'DependentNumber'"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-form-item
                v-if="formData.ContractOffical && formData.IndividualResident"
              >
                <el-row>
                  <el-col :span="14">
                    <span class="el-form-item__label"
                      >Giảm trừ 01 người phụ thuộc (7)</span
                    >
                  </el-col>
                  <el-col :span="10" style="text-align: right">
                    {{ DependentReduction }}
                    <span class="currency">VNĐ</span>
                  </el-col>
                </el-row>
              </el-form-item>
              <el-form-item
                v-if="formData.ContractOffical && formData.IndividualResident"
              >
                <el-row>
                  <el-col :span="14">
                    <span class="el-form-item__label"
                      >Giảm trừ bản thân (8)</span
                    >
                  </el-col>
                  <el-col :span="10" style="text-align: right">
                    {{ SelfReductionMoney }}
                    <span class="currency">VNĐ</span>
                  </el-col>
                </el-row>
              </el-form-item>
              <el-form-item
                v-if="formData.ContractOffical && formData.IndividualResident"
              >
                <el-row>
                  <el-col :span="14">
                    <span
                      class="el-form-item__label"
                      style="margin-right: 3.5px"
                      >Tổng giảm trừ gia cảnh (9)</span
                    >
                    <img
                      src="@/icons/Group 18553.png"
                      alt="info"
                      style="position: absolute; top: 11px"
                    />
                  </el-col>
                  <el-col :span="10" style="text-align: right">
                    {{ TotalReduction }}
                    <span class="currency">VNĐ</span>
                  </el-col>
                </el-row>
              </el-form-item>
              <el-form-item>
                <el-row>
                  <el-col :span="14">
                    <span class="el-form-item__label">Biểu thuế áp dụng</span>
                  </el-col>
                  <el-col :span="10" style="text-align: right">{{
                    TariffsApply
                  }}</el-col>
                </el-row>
              </el-form-item>
              <el-form-item>
                <el-row>
                  <el-col :span="14">
                    <span
                      class="el-form-item__label"
                      style="margin-right: 3.5px"
                      >Tổng thu nhập chịu thuế (10)</span
                    >
                    <img
                      src="@/icons/Group 18553.png"
                      alt="info"
                      style="position: absolute; top: 11px"
                    />
                  </el-col>
                  <el-col :span="10" style="text-align: right">
                    {{ TotalIncomeTaxable }}
                    <span class="currency">VNĐ</span>
                  </el-col>
                </el-row>
              </el-form-item>
              <el-form-item>
                <el-row>
                  <el-col :span="14">
                    <span class="el-form-item__label"
                      >Thuế thu nhập cá nhân khấu trừ</span
                    >
                  </el-col>
                  <el-col :span="10" style="text-align: right">
                    {{ totalTaxWithHeld }}
                    <span class="currency">VNĐ</span>
                  </el-col>
                </el-row>
              </el-form-item>
            </el-col>
          </el-row>
        </template>

        <template v-slot:footer class="dialog-footer">
          <el-row class="border-top">
            <el-col :span="24">
              <button-icon
                name="Lưu và đóng"
                type="multi-button"
                class="ml-8"
                :dataDropdown="dataButton"
                :handleClick="handleClick"
                @AddAndOut="onSubmit('formData')"
              ></button-icon>
            </el-col>
          </el-row>
        </template>
      </dialogel>
    </el-form>
  </div>
</template>

<script>
import dialogel from "@/components/dialog";
import Inputfield from "@/components/inputs";
import InputNumber from "@/components/inputs/input-number";
import ComboBox from "@/components/select";
import DatePicker from "@/components/date-time-pickers/date-time-picker-v2";
import Button from "@/components/buttons";
import { formatDate } from "@/utils/index.js";
import TaxPayer from "@/api/etax/tax-payer";
import TaxInfo from "@/api/etax/tax-info";
import CategoryDictionary from "@/api/etax/category-dictionary";
import { getTaxInfo, getSelfReduce } from "@/enums/tax-info";
import { formatMoney2 } from "@/utils/index.js";
import treeSelect from "@/components/trees";
import organizationUnitApi from "@/api/etax/organization-unit.js";
import { getFromTree } from "@/utils";
export default {
  components: {
    dialogel,
    "tree-select": treeSelect,
    "date-picker": DatePicker,
    "button-icon": Button,
    "input-field": Inputfield,
    "input-number": InputNumber,
    "combo-box": ComboBox,
  },

  props: {
    dialogFormVisible: {
      type: Boolean,
      default: false,
    },
    title: String,
    employeeProp: Object,
    status: {
      type: String,
    },
    handleClickProp: {
      type: Function,
      default: () => {},
    },
  },

  data() {
    return {
      loadingDialog: false,
      arraySetting: {
        value: "OrganizationUnitID",
        label: "OrganizationUnitName",
        children: "OrganizationUnitsChildren",
      },
      style: "tree",
      flagIndentityType: "",
      flag: 0,
      SelfReductionMoney: 0,
      IndentityType: "",
      checkStatus: 0,
      formCompare: {},
      isWarnClose: false,
      formData: {},
      tab1: true,
      tab2: false,
      loading: true,
      validate: {
        TaxNo: false,
        FullName: false,
        EmployeeCode: false,
        CitizenIdentityNo: false,
        email: false,
        IncomeTaxable: false,
      },
      dataButton: [
        // Thông tin dropdown button
        {
          name: "Lưu và thêm mới",
        },
      ],
      organizationUnit: [], // danh sách đơn vị
      nation: "", // danh sách tên Quốc gia
      PlaceOfIssue: [], // danh sách tên tỉnh/TP
      dboptions: [],
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
        {
          label: "BA",
          value: "BA",
        },
        {
          label: "HR",
          value: "HR",
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
        {
          label: "Phòng sản xuất",
          value: "Phòng sản xuất",
        },
      ],
      banks: [],
      Country: "",
      bankBranch: "", //Trường đặt tạm cho
      taxInfo: "", // danh sách kiểu tính thuế
      errorTaxNo: false, // Thông tin mã số thuế bị trùng
    };
  },

  methods: {
    tabEvent() {},
    /**
     * hàm chuyển sang tab 1
     */
    changetab1() {
      this.tab1 = true;
      this.tab2 = false;
    },
    /**
     * hàm chuyển sang tab 2
     */
    changetab2() {
      this.tab2 = true;
      this.tab1 = false;
    },
    // hàm bắt sự kiện submit
    handleClick() {
      this.onSubmit("formData");
    },

    // Hàm đóng dialog
    getClose() {
      this.isWarnClose = false;
      setTimeout(() => {
        this.tab1 = true;
        this.tab2 = false;
      }, 200);
      this.$emit("close-dialog", false);
    },
    /**
     * Lấy giá trị từ các trường input vào @param form
     * Created by: CVCuong - 28/7/2020
     */
    GetDateOfBirth(val) {
      this.formData.DateOfBirth = val;
    },

    getValueBankName(val) {
      this.formData.BankName = val;
    },
    getValueBankBranch(val) {
      this.formData.BankBranch = val;
    },
    getValueBankAccountNo(val) {
      this.formData.BankAccountNo = val;
    },
    getValueBankAccountName(val) {
      this.formData.BankAccountName = val;
    },
    getValuePhone(val) {
      this.formData.Phone = val;
    },
    getValueTaxNo(val) {
      this.formData.TaxNo = val;
    },
    getValueEmail(val) {
      this.formData.Email = val;
    },
    getValueCitizenIdentityNo(val) {
      this.formData.CitizenIdentityNo = val;
    },
    getValueEmployeeCode(val) {
      this.formData.EmployeeCode = val;
    },
    getValueFullName(val) {
      this.formData.FullName = val;
      this.formData.BankAccountName = val;
    },
    getValuePositionName(val) {
      this.formData.PositionID = val;
    },
    getValueOrganizationUnitName(val) {
      this.formData.OrganizationUnitName = val;
    },
    getValueCitizenIdentityPlaceName(val) {
      this.formData.CitizenIdentityPlaceID = val;
    },
    getValueNationalityID(val) {
      this.formData.NationalityID = val;
    },
    getIncomeTaxable(val) {
      this.formData.IncomeTaxable = val;
      this.settotalIncome();
      this.settotalIncomeTaxable();
    },
    getIncomeNotTaxable(val) {
      this.formData.IncomeNotTaxable = val;
      this.settotalIncome();
    },
    getInsurancePayment(val) {
      this.formData.InsurancePayment = val;
      this.settotalIncomeTaxable();
    },
    getOtherReduction(val) {
      this.formData.OtherReduction = val;
      this.settotalIncomeTaxable();
    },
    getDependentNumber(val) {
      this.formData.DependentNumber = val;
      this.settotalReduction();
      this.settotalIncomeTaxable();
    },
    getValueIndentityType(val) {
      this.IndentityType = val;
    },
    getOrganizationUnitValue(val) {
      getFromTree(val, this.organizationUnit, this.arraySetting);
      this.formData.OrganizationUnitID = val;
    },

    // Lấy thông tin bị trùng
    getTaxNoError() {
      if (this.errorTaxNo && this.validate.TaxNo) {
        return "Mã số thuế bị trùng";
      } else return null;
    },
    /**
     * method lấy danh sách quốc gia
     * created by: N.T. Dũng 28.07.2020
     *
     * modified by : N.V.Hung 28.08.2020
     */
    async getNational() {
      const res = await CategoryDictionary.getNationalites();
      this.nation = res.data.Data;
    },

    /**
     * method lấy danh sách ngân hàng
     * created by: N.V.Hung 28.08.2020
     */
    async getBanks() {
      const res = await CategoryDictionary.getBanks();
      if (res.data.Success) {
        this.banks = res.data.Data;
      }
    },

    async getDBOption() {
      const res = await CategoryDictionary.getDBOPtion();
      if (res.data.Success) {
        this.dboptions = res.data.Data;
      }
    },

    async getPosition() {
      const res = await CategoryDictionary.getPosition();
      if (res.data.Success) {
        this.positions = res.data.Data;
      }
    },

    /**
     * method lấy danh sách tỉnh/thành phố thuộc VN dựa theo lựa chọn quốc gia
     * created by: N.T. Dũng 28.07.2020
     * Modified by : N.V.Hung 28.08.2020
     */
    async getPlaceOfIssue(val) {
      if (this.flag > 0) {
        this.getValueCitizenIdentityPlaceName(null);
      }
      const res = await CategoryDictionary.getPlaceOfIssuesByIssueTypeCode(val);
      this.PlaceOfIssue = res.data.Data;
      this.flag++;
      //sortData(this.PlaceOfIssue);
    },

    /**
     * Thực hiện service thêm người lao động
     * Created by : NVHUNG - 16/7/2020
     * Modified by: N.T. Dũng 21.07.2020
     * Content modification: thêm phần xử lý sự kiện sửa người nộp thuế
     */

    async onSubmit(formData) {
      /**
       * Format dữ liệu đầu vào
       * Created by : NVHUNG - 16/7/2020
       */
      this.$refs[formData].validate(async (valid, index) => {
        /**
         * Auto focus trường validate
         */
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            this.tab1 = true;
            this.tab2 = false;
            setTimeout(() => {
              document.getElementById([`${Object.keys(index)[0]}`]).focus();
            }, 200);
          }
        }

        if (valid) {
          // Convert dữ liệu đầu vào hợp lệ
          if (this.formData.TotalTaxWithHeld === 0) {
            this.formData.IsTaxable = false;
          }
          this.formData.IsTaxable = this.convertBoolValue(
            this.formData.IsTaxable
          );
          this.formData.IndividualResident = this.convertBoolValue(
            this.formData.IndividualResident
          );
          if (this.IndentityType) {
            this.formData.IndentityType = this.dboptions.find(
              (option) => option.DBOptionValue === this.IndentityType
            ).DBOptionID;
          }

          const position = this.positions.find(
            (position) => position.PositionID === this.formData.PositionID
          );
          if (position === undefined) {
            this.formData.PositionName = this.formData.PositionID;
            this.formData.PositionID = null;
          }

          if (this.formData.ContractOffical === 0) {
            this.formData.SelfReduction = 0;
          }

          // Xóa bỏ trường date nếu null
          if (!this.formData.DateOfBirth) {
            delete this.formData.DateOfBirth;
          }
          if (!this.formData.CitizenIdentityDate) {
            delete this.formData.CitizenIdentityDate;
          }
          this.loadingDialog = true;
          // check điều kiện status để sửa hoặc thêm mới
          if (this.status === "1") {
            // phần xử lý sửa thông tin người nộp thuế
            const editResponse = await TaxPayer.editTaxPayer(this.formData);
            if (editResponse.data.Success === true) {
              this.popUpSuccess("Cập nhật người lao động thành công");
            } else {
              this.popUpFalse("Thất bại", "Cập nhật người lao động thất bại");
            }
          } else {
            /**
             * phần xử lý thêm mới người nộp thuế
             */
            const res = await this.InsertTaxPayer(this.formData, 0);
            if (res.data.Success) {
              this.popUpSuccess("Thêm người lao động thành công");
            } else {
              if (res.data.MISACode === 300) {
                var existNumber,
                  existTemporaryTaxDetailID,
                  existName,
                  existEmployeeID = "";
                var existReason = 1;

                // LCheck xem có bị trùng chứng minh thư hay không
                for (var i of res.data.Data) {
                  if (i.CitizenIdentityNo == this.formData.CitizenIdentityNo) {
                    // Gán thông tin trùng
                    existNumber = i.CitizenIdentityNo;
                    existTemporaryTaxDetailID = i.TemporaryTaxDetailID;
                    existEmployeeID = i.EmployeeID;
                    existReason = i.Reason;
                    existName = i.FullName;
                    break;
                  }
                }

                // Check xem có bị trùng mã số thuế không
                for (var exist of res.data.Data) {
                  // Gán vào thông tin
                  if (
                    exist.TaxNo == this.formData.TaxNo &&
                    exist.TaxNo &&
                    exist.TaxNo != ""
                  ) {
                    // Nếu chưa có chứng minh thư bị trùng thì gán tồn tại vào thông tin trùng
                    if (existNumber == "") {
                      existNumber = exist.CitizenIdentityNo;
                      existTemporaryTaxDetailID = exist.TemporaryTaxDetailID;
                      existEmployeeID = exist.EmployeeID;
                      existReason = exist.Reason;
                    } else {
                      this.errorTaxNo = true;
                    }
                  }
                }
                var existIn = existReason == 1 ? "bảng tính" : "hồ sơ lao động";
                // nếu bị trùng số cmt/hộ chiếu thì có thể yêu cầu cập nhật
                if (existNumber == this.formData.CitizenIdentityNo) {
                  this.buttonDialog = [
                    {
                      text: "Không",
                      class: "btn-grey",
                      callback: () => {},
                    },
                    {
                      text: "Có",
                      class: "btn-primary",
                      callback:
                        this.errorTaxNo == false
                          ? async () => {
                              this.formData.TemporaryTaxDetailID = existTemporaryTaxDetailID;
                              this.formData.EmployeeID = existEmployeeID;
                              this.formData.Confirm = existReason;
                              await this.CheckInsertTaxPayer();
                            }
                          : () => {},
                    },
                  ];

                  this.$_Popup.warn(
                    "Cảnh báo",
                    `CMND/CCCD/Hộ chiếu <b class="title-bold-ne">${existNumber}</b>  bị trùng với người nộp thuế <b class="title-bold-ne">${existName}</b> đã có trên ${existIn}. Bạn có muốn cập nhật thông tin mới cho người nộp thuế này không?`,
                    this.buttonDialog
                  );
                }

                if (existNumber == this.formData.TaxNo) {
                  this.buttonDialog = [
                    {
                      text: "Đóng",
                      class: "btn-primary",
                      callback: () => {},
                    },
                  ];
                  this.$_Popup.warn(
                    "Cảnh báo",
                    `Mã số thuế <b>${existNumber}</b> bị trùng với người nộp thuế <b>${existName}</b>. Vui lòng kiểm tra lại`
                  );
                }
              } else {
                this.popUpFalse("Thất bại", "Thêm người lao động thất bại");
              }
            }
          }
          this.loadingDialog = false;
          /**
           * Gọi prop function reset bảng
           */
          this.handleClickProp();
          this.getPosition();
        } else {
          return false;
        }
      });
    },

    /**
     * Reset validate form
     */
    resetForm(formName) {
      this.$refs[formName].resetFields();
      this.$refs[formName].clearValidate();
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
     * Service lấy thông tin kiểu tính thuế
     * Created by : NVHUNG - 16/7/2020
     */
    async GetTaxInfoService() {
      this.formData.TemporaryTaxID = this.$route.params.id;
      const TemporaryTaxType = this.$route.params.id;
      const data = await TaxInfo.getTaxInfo(
        TemporaryTaxType,
        this.formData.organizationID
      );
      if (!data || !data.Data) {
        this.popUpFalse(
          "Thất bại",
          "Lỗi dữ liệu service, Không thêm được người lao động"
        );
        this.$router.go(-1);
      } else if (!data.Success || !data.Data.length) {
        this.popUpFalse(
          "Thất bại",
          "Hiện tại chỉ cho phép thêm người lao động Bảng tính thuế TNCN từ tiền lương, tiền công"
        );
        this.$router.go(-1);
      } else {
        this.taxInfo = data.Data;
        if (this.taxInfo) {
          const SelfReduce = getSelfReduce(data.Data);
          // this.formData.SelfReduction = SelfReduce.SelfReduction;
          this.SelfReductionMoney = SelfReduce.SelfReduction;
          this.formData.DependentReduction = SelfReduce.DependentReduction;
        } else {
          this.formData.SelfReduction = 0;
          this.formData.SependentReduction = 0;
        }
      }
    },

    /**
     * Service thêm người nộp thuế
     * Created by : NVHUNG - 17/8/2020
     */
    async InsertTaxPayer(obj) {
      return await TaxPayer.createTaxPayer(obj);
    },

    /**
     *
     */
    async CheckInsertTaxPayer() {
      const res = await this.InsertTaxPayer(this.formData);
      if (res.data.Success) {
        this.handleClickProp();
        this.popUpSuccess("Thêm người lao động thành công");
      } else {
        this.popUpFalse("Thêm người lao động thất bại");
      }
    },

    /**
     *
     */
    popUpSuccess(message) {
      const loading = this.$loading({
        lock: true,
        text: "Đang tải",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      setTimeout(() => {
        loading.close();
      }, 200);
      this.getClose();
      this.$_Notification.success("Thành công", message);
      this.resetForm("formData");
    },

    /**
     *
     */
    popUpFalse(title, message) {
      this.buttonDialog = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: this.getClose,
        },
      ];
      this.$_Popup.warn(title, message, this.buttonDialog);
    },

    /**
     * map dữ liệu trả về thông tin của người nộp thuế
     * created by: N.T. Dũng 17.07.2020
     */
    mapDataEdit(data) {
      this.formData = { ...this.employeeProp };
      this.formData.IsTaxable = this.convertBoolValue(data.IsTaxable);
      this.formData.IndividualResident = this.convertBoolValue(
        data.IndividualResident
      );
      if (this.formData.IndentityType) {
        this.IndentityType = this.dboptions.find(
          (option) => option.DBOptionID === this.formData.IndentityType
        ).DBOptionValue;
      } else {
        this.IndentityType = null;
      }
      /**
       *  check ngày sinh và ngày cấp không null
       */
      if (this.formData.DateOfBirth) {
        this.formData.DateOfBirth = formatDate(data.DateOfBirth, "mm-dd-yyyy");
      }
      if (this.formData.CitizenIdentityDate) {
        this.formData.CitizenIdentityDate = formatDate(
          data.CitizenIdentityDate,
          "mm-dd-yyyy"
        );
      }
      // this.formCompare = { ...this.formData };
    },

    /**
     *  Format dữ liệu tiền cho các trường
     *  Created by : NVHUNG - 21/7/2020
     */
    settotalIncome() {
      this.formData.TotalIncome =
        this.formData.IncomeTaxable + this.formData.IncomeNotTaxable;
    },
    settotalReduction() {
      let val =
        this.formData.DependentNumber * this.formData.DependentReduction +
        this.formData.SelfReduction;
      this.formData.TotalReduction = val;
    },
    settotalIncomeTaxable() {
      let ToInTax = 0;
      if (this.formData.ContractOffical === 1) {
        ToInTax =
          this.formData.IncomeTaxable -
          this.formData.InsurancePayment -
          this.formData.OtherReduction -
          this.formData.TotalReduction;
      } else {
        ToInTax =
          this.formData.IncomeTaxable -
          this.formData.InsurancePayment -
          this.formData.OtherReduction;
      }
      if (ToInTax < 0) ToInTax = 0;
      this.formData.TotalIncomeTaxable = ToInTax;
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
          callback: this.getClose,
        },
      ];
      this.$_Popup.warn(
        "Cảnh báo",
        "Thông tin vừa nhập liệu sẽ không được lưu lại. Bạn có chắc chắn muốn thoát không?",
        this.buttonDialog
      );
    },

    /**
     */
    CompareObj(obj_1, obj_2) {
      let obj_action_1 = { ...obj_1 };
      let obj_action_2 = { ...obj_2 };
      delete obj_action_1.SelfReduction;
      delete obj_action_1.DependentReduction;
      delete obj_action_1.TotalReduction;
      delete obj_action_1.TariffsApply;
      delete obj_action_1.TemporaryTaxID;

      delete obj_action_2.SelfReduction;
      delete obj_action_2.DependentReduction;
      delete obj_action_2.TotalReduction;
      delete obj_action_2.TariffsApply;
      delete obj_action_2.TemporaryTaxID;

      if (JSON.stringify(obj_action_1) !== JSON.stringify(obj_action_2)) {
        return true;
      }
      return false;
    },

    /**
     * Lấy danh sách đơn vị phòng ban
     */
    async getOrganizationUnit() {
      var res = await organizationUnitApi.getOrganizationUnit(this.style);
      this.organizationUnit.push(res.data.Data);
    },
  },

  /**
   * Lắng nghe input format tiền.
   * Created by : NVHUNG - 21/7/2020
   */
  watch: {
    dialogFormVisible(val) {
      if (val) {
        this.flag = 0;
        if (this.status === "0") {
          this.IndentityType = "";
          this.IndentityType = "1010";
          this.$refs["formData"].clearValidate();
        }
        this.getOrganizationUnit();
        this.getNational(); // call method lấy danh sách quốc gia
        this.getBanks(); // call method lấy danh sách ngân hàng
        this.getDBOption();
        this.getPosition();
      }
    },

    "formData.IndividualResident": {
      handler(val) {
        if (!val) {
          this.formData.ContractOffical = 0;
          this.settotalReduction();
          this.settotalIncomeTaxable();
        }
      },
    },

    "formData.ContractOffical": {
      handler(val) {
        if (val === 0) {
          this.formData.SelfReduction = 0;
        } else {
          this.formData.SelfReduction = 0;
        }
      },
    },

    formData: {
      handler(val) {
        if (val.ContractOffical || !val.IndividualResident) {
          this.formData.IsTaxable = true;
        }
        this.settotalReduction();
        this.settotalIncomeTaxable();
      },
      deep: true,
    },

    employeeProp: {
      async handler(val) {
        this.mapDataEdit(val);
        this.settotalIncomeTaxable();
        if (!this.formData.NationalityID) {
          this.formData.NationalityID = 268;
          this.formData.NationalityName = "Việt Nam";
        }
        this.resetForm("formData");

        /**
         * Lấy thông tin phần trăm thuế
         * Created by : NVHUNG - 16/7/2020
         */
        await this.GetTaxInfoService();
      },
      deep: true,
    },

    totalTaxWithHeld(val) {
      this.formData.TotalTaxWithHeld = this.convertStringtoNumber(val);
    },

    TariffsApply(val) {
      this.formData.TariffsApply = val;
    },

    IndentityType(val) {
      this.$refs["formData"].clearValidate(["CitizenIdentityNo"]);
      this.getPlaceOfIssue(val);
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
          individualResident: this.formData.IndividualResident ? 1 : 0,
          contractOffical: this.formData.ContractOffical,
        };
        const taxInfoRes = getTaxInfo(
          this.formData.TotalIncomeTaxable,
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
          individualResident: this.formData.IndividualResident ? 1 : 0,
          contractOffical: this.formData.ContractOffical,
        };
        if (!this.formData.IsTaxable) {
          return "0";
        }
        const totalTaxRes = getTaxInfo(
          this.formData.TotalIncomeTaxable,
          this.taxInfo,
          taxPayer
        ).money;
        return formatMoney2(Math.round(totalTaxRes).toString(), ",");
      },
    },

    TotalIncome: {
      get: function () {
        return formatMoney2(this.formData.TotalIncome.toString(), ",");
      },
    },

    DependentReduction: {
      get: function () {
        return formatMoney2(this.formData.DependentReduction.toString(), ",");
      },
    },

    SelfReduction: {
      get: function () {
        return formatMoney2(this.formData.SelfReduction.toString(), ",");
      },
    },

    TotalReduction: {
      get: function () {
        return formatMoney2(this.formData.TotalReduction.toString(), ",");
      },
    },

    TotalIncomeTaxable: {
      get: function () {
        return formatMoney2(this.formData.TotalIncomeTaxable.toString(), ",");
      },
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";

.title-bold-ne {
  font-size: 15px;
}

.new-employee {
  .tab-bar {
    height: 40px;
    width: 100%;
    border-bottom: 1px solid $bg-color-3;
    box-sizing: content-box;

    .button-container {
      float: left;
    }

    .btn-tab-active {
      border-bottom-color: $red !important;

      span {
        font-family: $_fontbold;
      }
    }
    #btn-tab {
      border-bottom: 1px solid $bg-color-3;
    }

    span {
      font-size: 15px;
    }
  }
  .el-dialog__title {
    font-size: 18px;
    font-family: $_fontbold;
  }

  .el-dialog__body {
    height: calc(100% - 60px - 48px);
  }

  .el-form-item__label {
    margin-bottom: 0;
    font-family: $_fontregular;
    font-weight: normal;
    font-size: 13px;
    color: $dark-grey;
  }

  .el-form-item {
    margin-bottom: 5px;
  }

  .el-form-item--medium .el-form-item__label {
    line-height: 30px;
  }

  .el-form-item.is-required:not(.is-no-asterisk) > .el-form-item__label:before,
  .el-form-item.is-required:not(.is-no-asterisk)
    .el-form-item__label-wrap
    > .el-form-item__label:before {
    right: -13px;
  }

  .checkbox-label {
    padding-left: 10px;
  }

  .el-dialog__wrapper .el-dialog {
    height: 698px;
  }

  .currency {
    opacity: 0.5;
  }

  .el-form-item__label {
    word-wrap: break-word;
  }
}

@media screen and (min-width: 1300px) and (max-width: 1400px) {
  .new-employee {
    .el-dialog__wrapper .el-dialog {
      height: 91% !important;
    }
  }
}
</style>
