<template>
  <div>
    <edit-dialog
      :titleDialog="title"
      :id="dialogid"
      :dialogFormVisible="visibleDialog"
      :loading="loadingDialog"
      :closeonclickmodal="false"
      @close-dialog="closeForm"
      :isWarnClose="isWarnClose"
      :beforeClose="beforeClose"
      xl="8,9"
      lg="8,9"
      md="9,10"
      sm="4,3"
      xs="7,8"
    >
      <template v-slot:body>
        <div>
          <div class="tab-content">
            <div
              class="tab"
              :class="{ active: activeTab == 'organization' }"
              @click="activeTab = 'organization'"
            >
              {{ titleTab1 }}
            </div>
            <div
              class="tab"
              :class="{ active: activeTab == 'agent' }"
              @click="activeTab = 'agent'"
            >
              {{ titleTab2 }}
            </div>
          </div>
          <div class="content-dialog">
            <!-- organization tab -->
            <el-form
              v-show="activeTab == 'organization'"
              class="organization-tab"
              ref="organizationTab"
              :model="formOrganizationData"
            >
              <el-row :gutter="40">
                <el-col :lg="12" :md="12" :sm="24">
                  <el-row :gutter="8">
                    <el-col :lg="12" :md="12" :sm="24">
                      <el-form-item
                        label="Mã số thuế"
                        prop="OrganizationTaxNo"
                        :rules="[
                          {
                            required: true,
                            message: 'Mã số thuế không được bỏ trống.',
                            trigger: ['change'],
                          },
                        ]"
                        :show-message="validate.OrganizationTaxNo"
                      >
                        <input-field
                          :clearable="true"
                          @hover="validate.OrganizationTaxNo = $event"
                          type="field-input"
                          :id="'OrganizationTaxNo'"
                          @value="getValueOrganizationTaxNo"
                          :valueInput="formOrganizationData.OrganizationTaxNo"
                          width="100%"
                          placeholder="Nhập mã số thuế"
                          :maxlength="13"
                          :number="true"
                          @blur="autoFillOrganization"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="24">
                      <el-form-item
                        label="Tên đơn vị"
                        prop="OrganizationName"
                        :rules="[
                          {
                            required: true,
                            message: 'Tên đơn vị không được bỏ trống.',
                            trigger: ['change'],
                          },
                        ]"
                        :show-message="validate.OrganizationName"
                      >
                        <input-field
                          :clearable="true"
                          @hover="validate.OrganizationName = $event"
                          type="field-input"
                          :isUpercasefirst="true"
                          @value="getValueOrganizationName"
                          :valueInput="formOrganizationData.OrganizationName"
                          id="OrganizationName"
                          width="100%"
                          placeholder="Nhập tên đơn vị"
                          :maxlength="125"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row v-if="formOrganizationData.IsParent === 0">
                    <el-col :span="24">
                      <el-form-item
                        label="Cấp tổ chức"
                        prop="OrganizationUnit"
                        :rules="[
                          {
                            required: true,
                            message: 'Cấp tổ chức không được bỏ trống.',
                            trigger: ['change'],
                          },
                        ]"
                        :show-message="validate.OrganizationUnit"
                      >
                        <combo-box
                          @hover="validate.OrganizationUnit = $event"
                          type="label-none"
                          :filterable="true"
                          :id="'OrganizationUnit'"
                          placeholder="Chọn cấp tổ chức"
                          :list="listOrganizationUnit"
                          :defaultString="formOrganizationData.OrganizationUnit"
                          @value="getOrganizationUnit"
                          label="UnitName"
                          field="UnitValue"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row
                    v-if="formOrganizationData.OrganizationUnit == 'Chi nhánh'"
                  >
                    <el-col :span="24" class="text-left contract-tax">
                      <el-form-item
                        label="Hình thức hạch toán"
                        :rules="[
                          {
                            required: true,
                            message: 'Hình thức hạch toán không được bỏ trống.',
                            trigger: ['change'],
                          },
                        ]"
                      >
                        <el-radio-group
                          v-model="formOrganizationData.Accounting"
                        >
                          <el-radio :label="0">Hạch toán độc lập</el-radio>
                          <el-radio :label="1">Hạch toán phụ thuộc</el-radio>
                        </el-radio-group>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="24">
                      <el-form-item
                        label="Địa chỉ"
                        prop="TaxPlaceName"
                        :rules="[
                          {
                            required: true,
                            message: 'Địa chỉ không được bỏ trống.',
                            trigger: ['change'],
                          },
                        ]"
                        :show-message="validate.TaxPlaceName"
                      >
                        <input-field
                          :clearable="true"
                          @hover="validate.TaxPlaceName = $event"
                          type="field-input"
                          @value="getValueTaxPlaceName"
                          :id="'TaxPlaceName'"
                          :valueInput="formOrganizationData.TaxPlaceName"
                          width="100%"
                          placeholder="Nhập địa chỉ"
                          :maxlength="125"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row :gutter="8">
                    <el-col :span="12">
                      <el-form-item label="Tỉnh/Thành phố">
                        <combo-box
                          type="label-none"
                          :filterable="true"
                          placeholder="Chọn tỉnh/thành phố"
                          :list="listProvincials"
                          :defaultNumber="formOrganizationData.ProvinceID"
                          @value="getValueProvinceID"
                          label="ProvinceName"
                          field="ProvinceID"
                          :allowCreate="true"
                          :clearable="true"
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item label="Quận/Huyện">
                        <combo-box
                          type="label-none"
                          :filterable="true"
                          placeholder="Chọn quận/huyện"
                          :list="listCurrentOrganizationDistrict"
                          :defaultNumber="formOrganizationData.DistrictID"
                          @value="getValueDistrictID"
                          :label="'DistrictName'"
                          :field="'DistrictID'"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                </el-col>

                <el-col :lg="12" :md="12" :sm="24">
                  <el-row :gutter="8">
                    <el-col :span="12">
                      <el-form-item
                        prop="Phone"
                        label="Điện thoại"
                        :rules="[
                          {
                            required: true,
                            message: 'SĐT không được bỏ trống',
                            trigger: ['change'],
                          },
                        ]"
                        :show-message="validate.Phone"
                      >
                        <input-field
                          :clearable="true"
                          @hover="validate.Phone = $event"
                          type="field-input"
                          @value="getValuePhone"
                          :valueInput="formOrganizationData.Phone"
                          :id="'Phone'"
                          width="100%"
                          placeholder="Nhập số điện thoại"
                          :number="true"
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item
                        prop="Email"
                        label="Email"
                        :rules="[
                          {
                            required: true,
                            type: 'email',
                            message: 'Email chưa đúng định dạng',
                            trigger: ['change'],
                          },
                        ]"
                        :show-message="validate.Email"
                      >
                        <input-field
                          :clearable="true"
                          @hover="validate.Email = $event"
                          type="field-input"
                          @value="getValueEmail"
                          :valueInput="formOrganizationData.Email"
                          :id="'Email'"
                          width="100%"
                          placeholder="Nhập email"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row :gutter="8">
                    <el-col :span="12" class="text-left">
                      <el-form-item label="Fax">
                        <input-field
                          id="faxNo"
                          :clearable="true"
                          type="field-input"
                          :maxlength="10"
                          @value="getValueFaxNo"
                          :valueInput="formOrganizationData.FaxNo"
                          placeholder="Nhập số fax"
                          width="100%"
                          :number="true"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col>
                      <el-form-item
                        label="Cơ quan thuế cấp cục"
                        prop="CAUnitName"
                        :rules="[
                          {
                            required: true,
                            message:
                              'Cơ quan thuế cấp cục không được bỏ trống.',
                            trigger: ['change'],
                          },
                        ]"
                        :show-message="validate.CAUnitName"
                      >
                        <combo-box
                          @hover="validate.CAUnitName = $event"
                          type="label-none"
                          :filterable="true"
                          :id="'CAUnitName'"
                          placeholder="Chọn cơ quan thuế cấp cục"
                          :list="listTaxAuthorities"
                          :defaultString="formOrganizationData.CAUnitName"
                          @value="getValueCAUnitName"
                          label="TaxAuthoritiesName"
                          field="TaxAuthoritiesCode"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col>
                      <el-form-item
                        label="Cơ quan thuế quản lý"
                        prop="CAUnitManage"
                        :rules="[
                          {
                            required: true,
                            message:
                              'Cơ quan thuế quản lý không được bỏ trống.',
                            trigger: ['change'],
                          },
                        ]"
                        :show-message="validate.CAUnitManage"
                      >
                        <combo-box
                          @hover="validate.CAUnitManage = $event"
                          type="label-none"
                          :filterable="true"
                          :id="'CAUnitManage'"
                          placeholder="Chọn cơ quan thuế quản lý"
                          :list="listCurrentlistTaxAuthoritiesParent"
                          :defaultString="formOrganizationData.CAUnitManage"
                          @value="getValueCAUnitManage"
                          label="TaxAuthoritiesName"
                          field="TaxAuthoritiesCode"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row :gutter="8">
                    <el-col :span="12">
                      <el-form-item label="Số tài khoản ngân hàng">
                        <input-field
                          id="bankNo"
                          type="field-input"
                          width="100%"
                          @value="getValueBankNo"
                          :valueInput="formOrganizationData.BankNo"
                          placeholder="Nhập số tài khoản"
                          :maxlength="50"
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item label="Ngân hàng">
                        <combo-box
                          type="label-none"
                          :filterable="true"
                          placeholder="Chọn/nhập ngân hàng"
                          :list="listBanks"
                          :defaultString="formOrganizationData.BankID"
                          @value="getValueBankID"
                          label="BankName"
                          field="BankId"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col>
                      <el-form-item label="Người ký tờ khai">
                        <input-field
                          id="signBy"
                          :clearable="true"
                          type="field-input"
                          width="100%"
                          @value="getValueSignBy"
                          :valueInput="formOrganizationData.SignBy"
                          placeholder="Nhập tên người ký tờ khai"
                          :maxlength="250"
                          :isUpercasefirst="true"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                </el-col>
              </el-row>
            </el-form>

            <!-- tax agent tab -->
            <el-form
              v-show="activeTab == 'agent'"
              class="agent-tab"
              ref="agentTab"
              :model="formAgentData"
              :rules="rules"
            >
              <el-row :gutter="40">
                <el-col :lg="12" :md="12" :sm="24">
                  <el-row :gutter="8">
                    <el-col :span="12">
                      <el-form-item
                        class="tax-agent-taxno"
                        label="Mã số thuế của đại lý thuế"
                        prop="TaxAgentTaxNo"
                      >
                        <input-field
                          type="field-input"
                          width="100%"
                          :id="'TaxAgentTaxNo'"
                          @value="getValueAgencyTaxNo"
                          :valueInput="formAgentData.TaxAgentTaxNo"
                          placeholder="Nhập mã số thuế"
                          :maxlength="13"
                          :number="true"
                          @blur="autoFillTaxAgent"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="24">
                      <el-form-item label="Tên đại lý thuế" prop="TaxAgentName">
                        <input-field
                          @value="getValueAgencyName"
                          :valueInput="formAgentData.TaxAgentName"
                          type="field-input"
                          :isUpercasefirst="true"
                          id="TaxAgentName"
                          width="100%"
                          placeholder="Nhập tên đại lý thuế"
                          :maxlength="125"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="24">
                      <el-form-item label="Địa chỉ" prop="PlaceName">
                        <input-field
                          type="field-input"
                          width="100%"
                          :id="'PlaceName'"
                          @value="getValueAgencyAddress"
                          :valueInput="formAgentData.PlaceName"
                          placeholder="Nhập địa chỉ"
                          :maxlength="125"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row :gutter="8">
                    <el-col :span="12">
                      <el-form-item label="Tỉnh/Thành phố" prop="ProvinceName">
                        <combo-box
                          type="label-none"
                          :filterable="true"
                          placeholder="Chọn tỉnh/thành phố"
                          :list="listProvincials"
                          :defaultNumber="formAgentData.ProvinceID"
                          @value="getValueProvincialCode"
                          label="ProvinceName"
                          field="ProvinceID"
                          :allowCreate="true"
                          :clearable="true"
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item label="Quận/Huyện" prop="DistrictName">
                        <combo-box
                          type="label-none"
                          :filterable="true"
                          placeholder="Chọn quận/huyện"
                          :list="listCurrentAgentDistrict"
                          :defaultNumber="formAgentData.DistrictID"
                          @value="getValueDistrictCode"
                          label="DistrictName"
                          field="DistrictID"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row :gutter="8">
                    <el-col :span="12">
                      <el-form-item label="Số điện thoại" prop="Phone">
                        <input-field
                          type="field-input"
                          @value="getValueAgencyPhone"
                          :valueInput="formAgentData.Phone"
                          :id="'AgentPhone'"
                          width="100%"
                          placeholder="Nhập số điện thoại"
                          :number="true"
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item prop="Email" label="Email">
                        <input-field
                          type="field-input"
                          @value="getValueAgencyEmail"
                          :valueInput="formAgentData.Email"
                          :id="'AgentEmail'"
                          width="100%"
                          placeholder="Nhập email"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row :gutter="8">
                    <el-col :span="12" class="text-left">
                      <el-form-item label="Fax" prop="Fax">
                        <input-field
                          id="TaxNo"
                          type="field-input"
                          :maxlength="10"
                          placeholder="Nhập số fax"
                          @value="getValueAgencyFax"
                          :valueInput="formAgentData.FaxNo"
                          width="100%"
                          :number="true"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                </el-col>

                <el-col :lg="12" :md="12" :sm="24" class="right-info">
                  <el-row :gutter="8">
                    <el-col :lg="12" :md="12" :sm="24" class="text-left">
                      <el-form-item label="Số hợp đồng" prop="ContractNo">
                        <input-field
                          type="field-input"
                          :maxlength="10"
                          :id="'ContractNo'"
                          @value="getValueAgencyNumberContract"
                          :valueInput="formAgentData.ContractNo"
                          placeholder="Nhập số hợp đồng"
                          width="100%"
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :lg="12" :md="12" :sm="24">
                      <el-form-item label="Ngày hợp đồng" prop="ContractDate">
                        <date-picker
                          class="contract-date-input"
                          type="date"
                          :id="'ContractDate'"
                          @getValue="getValueAgencyDateContract"
                          :value="formAgentData.ContractDate"
                          width="100%"
                          placeholder="_ _ /_ _ /_ _ _ _"
                        ></date-picker>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col>
                      <el-form-item
                        label="Nhân viên đại lý thuế"
                        prop="TaxAgentEmployee"
                      >
                        <input-field
                          type="field-input"
                          width="100%"
                          @value="getValueEmpTax"
                          :valueInput="formAgentData.TaxAgentEmployee"
                          placeholder="Nhập tên nhân viên đại lý thuế"
                          :maxlength="250"
                          :isUpercasefirst="true"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col>
                      <el-form-item
                        label="Chứng chỉ hành nghề số"
                        prop="PracticingCertifacateNo"
                      >
                        <input-field
                          id="PracticingCertifacateNo"
                          type="field-input"
                          width="100%"
                          @value="getValuePracticingCertifacateNo"
                          :valueInput="formAgentData.PracticingCertifacateNo"
                          placeholder="Nhập chứng chỉ hành nghề số"
                          :maxlength="250"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <div class="checkbox-info">
                    <el-checkbox v-model="formAgentData.IsShowDeclaration">
                      Hiển thị thông tin đại lý thuế trên tờ khai</el-checkbox
                    >
                  </div>
                </el-col>
              </el-row>
            </el-form>
          </div>
        </div>
      </template>

      <template v-slot:footer>
        <!-- Btn organization tab -->
        <btn type="btn-primary" :name="'Lưu'" :handleClick="sendSubmit" />
        <btn
          type="btn-grey"
          style="margin-right: 10px"
          name="Hủy"
          :handleClick="cancelSubmit"
        />
      </template>
    </edit-dialog>
  </div>
</template>

<script>
import Dialog from "@/components/dialog";
import Button from "@/components/buttons";
import Inputfield from "@/components/inputs";
import ComboBox from "@/components/select";
import DatePicker from "@/components/date-time-pickers/date-time-picker-v2";
import taxAgentSerice from "@/api/etax/taxagent.js";
import { formatDate } from "@/utils/index.js";
import organizationService from "@/api/etax/organization.js";
import dictionaryService from "@/api/etax/category-dictionary.js";
import taxAuthorities from "@/api/etax/tax-authorities.js";

export default {
  components: {
    "edit-dialog": Dialog,
    btn: Button,
    "input-field": Inputfield,
    "combo-box": ComboBox,
    "date-picker": DatePicker,
  },

  props: {
    organization: Object,
    listProvincials: Array,
    listDistricts: Array,
    // props organization tab
    listTaxAuthorities: Array,
    listTaxAuthoritiesParent: Array,
    listBanks: Array,

    //props tax agent
    taxAgent: Object,

    visibleDialog: {
      type: Boolean,
      default: false,
    },
  },

  data() {
    return {
      title: "THAY ĐỔI THÔNG TIN ĐƠN VỊ",
      dialogid: "edit-setting-dialog",
      activeTab: "organization",
      titleTab1: "Thông tin chung",
      titleTab2: "Thông tin đại lý thuế",
      loadingDialog: false,
      isWarnClose: false,
      formOrganizationData: {},
      formAgentData: {},
      listCurrentOrganizationDistrict: [],
      listCurrentAgentDistrict: [],
      listCurrentlistTaxAuthoritiesParent: [],
      validate: {
        OrganizationName: false, // Tên đơn vị
        OrganizationTaxNo: false, // Mã số thuế đơn vị
        TaxPlaceName: false, // Địa chỉ đơn vị
        Phone: false, // SSĐT đơn vị
        Email: false, // Emai đơn vị
        CAUnitName: false,
        CAUnitManage: false,
        OrganizationUnit: false,
        AgentEmail: false,
      },
      rules: {
        TaxAgentName: [],
        TaxAgentTaxNo: [],
        PlaceName: [],
        Phone: [
          {
            type: "phone",
            message: "Số điện thoại chưa đúng định dạng",
            trigger: ["change"],
          },
        ],
        Email: [
          {
            type: "email",
            message: "Email chưa đúng định dạng",
            trigger: ["change"],
          },
        ],
        FaxNo: [],
        ContractNo: [],
        ContractDate: [],
        TaxAgentEmployee: [],
        PracticingCertifacateNo: [],
      },
      listOrganizationUnit: [
        {
          UnitName: "Công ty con",
          UnitValue: "Công ty con",
        },
        {
          UnitName: "Chi nhánh",
          UnitValue: "Chi nhánh",
        },
      ],
      suggestOrganization: {},
      suggestProvinceID: 0,
      suggestTaxAgent: {},
    };
  },

  mounted() {
    this.formAgentData = this.taxAgent;
    this.listCurrentOrganizationDistrict = [...this.listDistricts];
    this.listCurrentlistTaxAuthoritiesParent = [
      ...this.listTaxAuthoritiesParent,
    ];
  },

  filters: {
    formatDateToView: function (val) {
      return formatDate(val, "dd-mm-yyyy");
    },
  },

  watch: {
    /**
     * Nấu mở form thì mặc định mở tab thông tin chung
     * created by vdthang 26.11.2020
     */
    visibleDialog:{
      handler(val){
        if(val){
          this.activeTab = "organization";
        }
      }
    },

    organization: {
      async handler(val) {
        this.mapDataEdit(val);
      },
      deep: true,
    },

    /**
     * Nếu prop taxAgent thay đổi thì map lại dữ liệu
     * creted by: lttuan 05.09.20
     */
    taxAgent: {
      async handler(val) {
        this.mapAgentDataEdit(val);
      },
      deep: true,
    },

    isTaxAgentsDialog: {
      handler() {
        this.$nextTick(() => {
          document.getElementById("TaxAgentName").focus();
        });
      },
    },
  },

  methods: {
    /**
     * map dữ liệu trả về thông tin của organization
     * createdby ndhuy 31.08.2020
     */
    mapDataEdit() {
      this.formOrganizationData = { ...this.organization };
    },

    mapAgentDataEdit() {
      this.formAgentData = { ...this.taxAgent };
    },

    /**
     * Lấy giá trị các trường input vào form thông tin chung
     * Created by: LTTUAN 31/8/2020
     */
    getValueOrganizationName(val) {
      this.formOrganizationData.OrganizationName = val;
    },
    getValueOrganizationTaxNo(val) {
      this.formOrganizationData.OrganizationTaxNo = val;
    },
    getValueTaxPlaceName(val) {
      this.formOrganizationData.TaxPlaceName = val;
    },
    getValueProvinceID(val) {
      if (this.formOrganizationData.ProvinceID != val)
        this.formOrganizationData.DistrictID = "";
      this.formOrganizationData.ProvinceID = val;
      this.getListDistricts(val, "organization");
    },
    getValueDistrictID(val) {
      this.formOrganizationData.DistrictID = val;
    },
    getValuePhone(val) {
      this.formOrganizationData.Phone = val;
    },
    getValueEmail(val) {
      this.formOrganizationData.Email = val;
    },
    getValueFaxNo(val) {
      this.formOrganizationData.FaxNo = val;
    },
    getValueCAUnitName(val) {
      if (this.formOrganizationData.CAUnitName != val)
        this.formOrganizationData.CAUnitManage = "";
      this.formOrganizationData.CAUnitName = val;
      this.getListTaxAuthoritiesParent(val);
    },
    getValueCAUnitManage(val) {
      this.formOrganizationData.CAUnitManage = val;
    },
    getValueBankID(val) {
      if (this.formOrganizationData.BankID !== val)
        this.formOrganizationData.BankNo = "";
      this.formOrganizationData.BankID = val;
    },
    getValueBankNo(val) {
      this.formOrganizationData.BankNo = val;
    },
    getValueSignBy(val) {
      this.formOrganizationData.SignBy = val;
    },
    getOrganizationUnit(val) {
      this.formOrganizationData.OrganizationUnit = val;
    },
    /**
     * Lấy giá trị các trường input vào form thông tin đại lý thuế
     * Created by: LTTUAN 31/8/2020
     */

    getValueAgencyName(val) {
      this.formAgentData.TaxAgentName = val;
    },
    getValueAgencyTaxNo(val) {
      this.formAgentData.TaxAgentTaxNo = val;
    },
    getValueAgencyAddress(val) {
      this.formAgentData.PlaceName = val;
    },
    getValueProvincialCode(val) {
      if (this.formAgentData.ProvinceID !== val)
        this.formAgentData.DistrictID = "";
      this.formAgentData.ProvinceID = val;
      this.getListDistricts(val, "agent");
    },
    getValueDistrictCode(val) {
      this.formAgentData.DistrictID = val;
    },
    getValueAgencyPhone(val) {
      this.formAgentData.Phone = val;
    },
    getValueAgencyEmail(val) {
      this.formAgentData.Email = val;
    },
    getValueAgencyFax(val) {
      this.formAgentData.FaxNo = val;
    },
    getValueAgencyNumberContract(val) {
      this.formAgentData.ContractNo = val;
    },
    getValueAgencyDateContract(val) {
      this.formAgentData.ContractDate = val;
      this.testDate = val;
    },
    getValueEmpTax(val) {
      this.formAgentData.TaxAgentEmployee = val;
    },
    /**
     * Số chứng chỉ hành nghề
     */
    getValuePracticingCertifacateNo(val) {
      this.formAgentData.PracticingCertifacateNo = val;
    },

    /**
     * Hàm gọi service lấy dữ liệu quận huyện theo mã code tỉnh thành phố
     * @param provinceCode mã code tỉnh thành phố
     * createdby ndhuy 01.09.2020
     */
    async getListDistricts(provinceCode, formData) {
      var res = await dictionaryService.getDistrictsByDistrictsID(provinceCode);
      if (typeof res.data != "undefined" && res.data.Success) {
        if (formData === "organization") {
          this.listCurrentOrganizationDistrict = res.data.Data;
        } else this.listCurrentAgentDistrict = res.data.Data;
      } else this.listDistricts = [];
    },

    /**
     * Hàm gọi service lấy dữ liệu quận huyện theo mã code tỉnh thành phố
     * @param provinceCode mã code tỉnh thành phố
     * createdby nqkhai (25/11/2020)
     */
    async getListDistrictsSuggest(provinceCode, formData, districtName) {
      var res = await dictionaryService.getDistrictsByDistrictsID(provinceCode);
      if (typeof res.data != "undefined" && res.data.Success) {
        if (formData === "organization") {
          this.formOrganizationData.DistrictID = this.findInArray(
            districtName,
            res.data.Data,
            "DistrictName",
            "DistrictID"
          );
          // this.listCurrentOrganizationDistrict = res.data.Data;
        } else {
          this.formAgentData.DistrictID = this.findInArray(
            districtName,
            res.data.Data,
            "DistrictName",
            "DistrictID"
          );
          //      this.listCurrentAgentDistrict = res.data.Data;
        }
      } else this.listDistricts = [];
    },

    /**
     * Hàm gọi service lấy dữ liệu cơ quan thuế quản lý theo cơ quan thuế cấp cục
     * createdby ndhuy 01.09.2020
     */
    async getListTaxAuthoritiesParent(val) {
      const res = await dictionaryService.getListTaxAuthoritiesParent(val);
      if (typeof res.data != "undefined" && res.data.Success)
        this.listCurrentlistTaxAuthoritiesParent = res.data.Data;
    },

    sendSubmit() {
      this.onSubmitOrganization();
      // this.onSubmit("agentTab");
    },

    /**
     * Sự kiện khi click vào nút đóng form
     * Created by: LTTUAN 31/8/2020
     */
    closeForm() {
      this.$refs["organizationTab"].resetFields();
      this.$refs["agentTab"].resetFields();
      this.$emit("closeEditDialog", false);
    },

    /**
     * Sự kiện khi click nút quay lại
     * createdby ndhuy 07.09.2020
     */
    cancelSubmit() {
      this.checkEdited();
      if (this.isWarnClose == true) {
        this.buttons = [
          {
            text: "Không",
            class: "btn-grey",
            callback: () => {},
          },
          {
            text: "Có",
            class: "btn-primary",
            callback: this.closeForm,
          },
        ];
        this.$_Popup.warn(
          "Cảnh báo",
          "Thông tin vừa nhập liệu sẽ không được lưu lại. Bạn có chắc chắn muốn thoát không?",
          this.buttons
        );
      } else {
        this.closeForm();
      }
    },

    /**
     * Kiểm tra các ô nhập liệu đã bị thay đổi hay chưa
     * thay đổi biến hiện cảnh báo khi form thay đổi
     * createdby ndhuy 09.09.2020
     */
    checkEdited() {
      this.isWarnClose = false;
      var organizationData = this.formOrganizationData;
      var agentData = this.formAgentData;
      for (const prop in organizationData) {
        if (!organizationData[prop]) {
          //check null and ""
          if (this.organization[prop]) {
            this.isWarnClose = true;
          }
        } else if (organizationData[prop] != this.organization[prop]) {
          this.isWarnClose = true;
        }
      }
      for (const prop in agentData) {
        if (!agentData[prop]) {
          if (this.taxAgent[prop]) {
            this.isWarnClose = true;
          }
        } else if (agentData[prop] != this.taxAgent[prop]) {
          this.isWarnClose = true;
        }
      }
    },

    /**
     * Hàm kiểm tra  trùng lặp tỉnh/thành phố của công ty cha và công ty con
     * Created By : NQKHAI(23/11/2020)
     */
    async validateDependentProvince() {
      if (this.formOrganizationData.IsParent === 0) {
        if (
          this.formOrganizationData.OrganizationUnit === "Chi nhánh" &&
          this.formOrganizationData.Accounting === 1
        ) {
          var childProvinceID = this.formOrganizationData.ProvinceID;
          var res = await organizationService.getOrganizationById(
            this.formOrganizationData.OrganizationParentID
          );
          if (res.data.Success) {
            if (childProvinceID === res.data.Data.ProvinceID) {
              let warning =
                "Đơn vị hạch toán phụ thuộc này cùng Tỉnh/Thành phố với đơn vị chủ quản. Bạn có muốn tiếp tục?";
              this.popUpWarning(warning);
            } else {
              this.handleSubmit();
            }
          } else {
            this.popUpFalse("Thất bại", "Cập nhật đơn vị thất bại");
          }
        } else {
          this.handleSubmit();
        }
      } else {
        var parentProvinceID = this.formOrganizationData.ProvinceID;
        var result = await organizationService.getOrganizationDependent(
          this.organization.OrganizationID
        );
        if (result.data.Success) {
          var duplicateProvince = "";
          var temp = 0;
          for (var i = 0; i < result.data.Data.length; i++) {
            if (parentProvinceID === result.data.Data[i].ProvinceID) {
              temp = temp + 1;
              duplicateProvince =
                duplicateProvince +
                result.data.Data[i].OrganizationTaxNo +
                " - " +
                result.data.Data[i].OrganizationName +
                ",<br> ";
            }
          }
          if (temp > 0) {
            let warning = `Đơn vị chủ quản cùng Tỉnh/Thành phố với đơn vị: \n <b>${duplicateProvince.substring(
              0,
              duplicateProvince.length - 6
            )}</b>. <br> Bạn có muốn tiếp tục?`;
            this.popUpWarning(warning);
          } else {
            this.handleSubmit();
          }
        } else {
          this.popUpFalse("Thất bại", "Cập nhật đơn vị thất bại");
        }
      }
    },

    /**
     * Hàm thực thi trước khi form bị đóng
     * createdby ndhuy 10.09.2020
     */
    beforeClose(done) {
      this.checkEdited();
      if (this.isWarnClose == true) {
        this.buttons = [
          {
            text: "Không",
            class: "btn-grey",
            callback: () => {},
          },
          {
            text: "Có",
            class: "btn-primary",
            callback: done,
          },
        ];
        this.$_Popup.warn(
          "Cảnh báo",
          "Thông tin vừa nhập liệu sẽ không được lưu lại. Bạn có chắc chắn muốn thoát không?",
          this.buttons
        );
      } else {
        done();
      }
    },

    /**
     * Hàm xử lý sự kiện cập nhật thông tin đơn vị
     * createdby nqkhai(24/11/2020)
     */
    async onSubmitOrganization() {
      this.$refs["organizationTab"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById([`${Object.keys(index)[0]}`]).focus();
            }, 200);
          }
        }

        if (valid) {
          if (
            this.formOrganizationData.OrganizationTaxNo ===
            this.organization.OrganizationTaxNo
          ) {
            this.validateDependentProvince();
          } else {
            var res = await organizationService.checkDuplicateTaxCode(
              this.formOrganizationData.OrganizationTaxNo
            );
            if (res.data.Success) {
              this.popUpFalse(
                "Thất bại",
                `Mã số thuế trùng với đơn vị <b> ${res.data.Data.OrganizationName}</b> . Vui lòng kiểm tra lại.`
              );
            } else {
              //Auto focus trường validate
              this.validateDependentProvince();
            }
          }
        }
      });
    },

    /**
     * Thực hiện gọi service chỉnh sửa thông tin đơn vị
     * Created By : NQKHAI(23/11/2020)
     */
    async handleSubmit() {
      this.loadingDialog = true;

      var obj = {
        OrganizationID: this.formOrganizationData.OrganizationID,
        OrganizationName: this.formOrganizationData.OrganizationName,
        OrganizationTaxNo: this.formOrganizationData.OrganizationTaxNo,
        TaxPlaceName: this.formOrganizationData.TaxPlaceName,
        TaxPlaceID: this.formOrganizationData.TaxPlaceID,
        NationalityID: this.formOrganizationData.NationalityID,
        ProvinceID: this.formOrganizationData.ProvinceID,
        DistrictID: this.formOrganizationData.DistrictID,
        WardID: this.formOrganizationData.WardID,
        FaxNo: this.formOrganizationData.FaxNo,
        TaxDate: this.formOrganizationData.TaxDate,
        Email: this.formOrganizationData.Email,
        Phone: this.formOrganizationData.Phone,
        EmployeeID: this.formOrganizationData.EmployeeID,
        EmployeeName: this.formOrganizationData.EmployeeName,
        SignDate: this.formOrganizationData.SignDate,
        SignBy: this.formOrganizationData.SignBy,
        PracticingCertifacateNo: this.formOrganizationData
          .PracticingCertifacateNo,
        IsUseMTax: this.formOrganizationData.IsUseMTax,
        CreatedDate: this.formOrganizationData.CreatedDate,
        CreatedBy: this.formOrganizationData.CreatedBy,
        ModifiedDate: this.formOrganizationData.ModifiedDate,
        ModifiedBy: this.formOrganizationData.ModifiedBy,
        BankID: this.formOrganizationData.BankID,
        BankNo: this.formOrganizationData.BankNo,
        CAUnitName: this.formOrganizationData.CAUnitName,
        CAUnitManage: this.formOrganizationData.CAUnitManage,
        OrganizationParentId: this.formOrganizationData.OrganizationParentId,
        OrganizationAmisId: this.formOrganizationData.OrganizationAmisId,
        OrganizationUnit: this.formOrganizationData.OrganizationUnit,
        TenantID: this.formOrganizationData.TenantID,
        IsParent: this.formOrganizationData.IsParent,
        Accounting: this.formOrganizationData.Accounting,
      };

      const editResponse = await organizationService.updateOrganization(obj);

      if (editResponse.data.Success === true) {
        this.onAgentSubmit();
      } else {
        this.popUpFalse("Thất bại", "Cập nhật đơn vị thất bại");
        this.loadingDialog = false;
      }
    },

    /**
     * Thực hiện gọi service chỉnh sửa thông tin đơn vị
     * createdby lttuan 03.09.2020
     */
    async onAgentSubmit() {
      if (this.formAgentData) {
        var taxAgent = {
          TaxAgentID: this.formAgentData.TaxAgentID,
          TaxAgentName: this.formAgentData.TaxAgentName,
          TaxAgentTaxNo: this.formAgentData.TaxAgentTaxNo,
          PlaceName: this.formAgentData.PlaceName,
          ProvinceID: this.formAgentData.ProvinceID,
          DistrictID: this.formAgentData.DistrictID,
          Phone: this.formAgentData.Phone,
          Email: this.formAgentData.Email,
          FaxNo: this.formAgentData.FaxNo,
          ContractNo: this.formAgentData.ContractNo,
          ContractDate: this.formAgentData.ContractDate,
          TaxAgentEmployee: this.formAgentData.TaxAgentEmployee,
          PracticingCertifacateNo: this.formAgentData.PracticingCertifacateNo,
          IsShowDeclaration:
            this.formAgentData.IsShowDeclaration === true ? 1 : 0,
          OrganizationID: this.organization.OrganizationID,
        };
        //  this.loadingDialog=true;
        const editResponse = await taxAgentSerice.putTaxAgent(taxAgent);
        this.loadingDialog = false;
        if (editResponse.data.Success === true) {
          // this.popUpSuccess("Cập nhật đơn vị thành công");
          this.$_Notification.success(
            "Thành công",
            "Cập nhật đơn vị thành công !"
          );
          this.closeForm();
          this.$emit("reload", true);
        } else {
          this.popUpFalse("Thất bại", "Cập nhật đơn vị thất bại");
          this.closeForm();
        }
      } else {
        this.loading = false;
        this.$_Notification.success(
          "Thành công",
          "Cập nhật đơn vị thành công !"
        );
        this.closeForm();
      }
    },

    /**
     * Hàm hiển thị popup thất bại
     * createdby ndhuy 03.09.2020
     */
    popUpFalse(title, message) {
      this.buttonDialog = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];
      this.$_Popup.warn(title, message, this.buttonDialog);
    },

    /**
     * Hàm hiển thị popup cảnh báo khi trùng tỉnh/thành phố
     * Created By : NQKHAI(24/11/2020)
     */
    popUpWarning(warning) {
      this.buttons = [
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
        {
          text: "Có",
          class: "btn-primary",
          callback: this.handleSubmit,
        },
      ];
      this.$_Popup.warn("Cảnh báo", warning, this.buttons);
    },

    /**
     * Hàm tìm kiếm trong mảng
     * createdby ndhuy 01.09.2020
     */
    findInArray(value, array, propertyInput, propertyOutput) {
      for (let i in array) {
        if (array[i][propertyInput].toLowerCase() == value.toLowerCase())
          return array[i][propertyOutput];
      }
      return null;
    },

    /**
     * Hàm xử lý sự kiện autofill thông tin khi nhập mã số thuế (đơn vị chính)
     * Created By : NQKHAI(25/11/2020)
     * Modified by : BVBAO(25/11/2020)
     */
    async autoFillOrganization() {
      this.suggestOrganization.OrganizationTaxNo = this.formOrganizationData.OrganizationTaxNo;
      if (this.suggestOrganization.OrganizationTaxNo) {
        var res = await organizationService.getOrganizationSuggest(
          this.suggestOrganization.OrganizationTaxNo
        );
        if (res.data.Success && res.data.Data) {
          this.formOrganizationData.BankNo = "";
          this.formOrganizationData.BankID = "";
          this.formOrganizationData.SignBy = "";
          this.formOrganizationData.ProvinceID = "";
          this.formOrganizationData.DistrictID = "";

          this.formOrganizationData.OrganizationTaxNo = this.suggestOrganization.OrganizationTaxNo;
          this.formOrganizationData.OrganizationName =
            res.data.Data.companyName;
          this.formOrganizationData.TaxPlaceName = res.data.Data.address;
          this.formOrganizationData.Phone = res.data.Data.contactPhoneNumber;
          this.formOrganizationData.Email = res.data.Data.email;
          this.formOrganizationData.FaxNo = res.data.Data.fax;

          this.formOrganizationData.ProvinceID = this.findInArray(
            res.data.Data.province,
            this.listProvincials,
            "ProvinceName",
            "ProvinceID"
          );

          this.getListDistrictsSuggest(
            this.formOrganizationData.ProvinceID,
            "organization",
            res.data.Data.district
          );

          var result = await taxAuthorities.getTaxAuthoritiesByName(
            res.data.Data.addressForTaxPayment
          );
          if (result.data.Success && result.data.Data) {
            this.formOrganizationData.CAUnitManage =
              result.data.Data[0].TaxAuthoritiesCode;
            this.formOrganizationData.CAUnitName =
              result.data.Data[0].TaxAuthoritiesParentCode;
          } else {
            this.formOrganizationData.CAUnitManage = "";
            this.formOrganizationData.CAUnitName = "";
          }
        } else return;
      } else return;
    },

    /**
     * Hàm xử lý sự kiện autofill thông tin khi nhập mã số thuế (đại lý thuế)
     * Created By : NQKHAI(25/11/2020)
     * Modified by : BVBAO(25/11/2020)
     */
    async autoFillTaxAgent() {
      this.suggestTaxAgent.TaxAgentTaxNo = this.formAgentData.TaxAgentTaxNo;
      if (this.suggestTaxAgent.TaxAgentTaxNo) {
        var res = await organizationService.getOrganizationSuggest(
          this.suggestTaxAgent.TaxAgentTaxNo
        );
        if (res.data.Success && res.data.Data) {
          this.formAgentData.ProvinceID = "";
          this.formAgentData.DistrictID = "";
          this.formAgentData.ContractNo = "";
          this.formAgentData.ContractDate = "";
          this.formAgentData.TaxAgentEmployee = "";
          this.formAgentData.PracticingCertifacateNo = "";

          this.formAgentData.TaxAgentTaxNo = this.suggestTaxAgent.TaxAgentTaxNo;
          this.formAgentData.TaxAgentName = res.data.Data.companyName;
          this.formAgentData.PlaceName = res.data.Data.address;
          this.formAgentData.Phone = res.data.Data.contactPhoneNumber;
          this.formAgentData.Email = res.data.Data.email;
          this.formAgentData.FaxNo = res.data.Data.fax;

          this.formAgentData.ProvinceID = this.findInArray(
            res.data.Data.province,
            this.listProvincials,
            "ProvinceName",
            "ProvinceID"
          );

          this.getListDistrictsSuggest(
            this.formAgentData.ProvinceID,
            "agent",
            res.data.Data.district
          );
        } else return;
      } else return;
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
#edit-setting-dialog {
  .tab-content {
    left: 0px;
    margin-left: -24px;
    margin-right: -24px;
    height: 60px;
    display: flex;
    padding-top: 12px;
    padding-left: 24px;
    background-color: $background-grey;
    z-index: 10;

    .tab {
      padding: 0 16px 0 16px;
      text-align: center;
      height: 48px;
      border-radius: 5px 5px 0 0;
      line-height: 48px;
      color: $medium-dark-grey;
      cursor: pointer;
    }
    .active {
      background-color: $white;
      color: $dark;
      font-family: $_fontbold;
    }
  }
  .content-dialog {
     margin-top: 25px;
    margin-bottom: 10px;
    width: 100%;
    background-color: $white;

    .organization-tab {
      .el-dialog__title {
        font-size: 18px;
        font-family: $_fontbold;
      }
      .el-form-item {
        margin-bottom: 5px;
        .el-form-item__label {
          margin-bottom: 0;
          font-family: $_fontregular;
          font-weight: normal;
          font-size: 13px;
          color: $dark-grey;
        }
        ::placeholder {
          color: $bg-grey;
          font-size: 11px !important;
        }
      }
      .el-radio-group {
        margin-left: 6px;
        margin-top: 8px;
        margin-bottom: 10px;
      }
      .el-radio__label {
        font-size: 13px;
      }

      .el-form-item.is-required:not(.is-no-asterisk)
        > .el-form-item__label:before,
      .el-form-item.is-required:not(.is-no-asterisk)
        .el-form-item__label-wrap
        > .el-form-item__label:before {
        right: 0px;
      }

      .checkbox-label {
        padding-left: 10px;
      }

      .el-form-item--medium .el-form-item__label {
        line-height: 30px;
      }

      .currency {
        opacity: 0.5;
      }

      .text-left input {
        text-align: left;
        min-width: 0 !important;
      }

      .el-form-item__error {
        transform: translate(-50%, 0%) !important;
      }
      .el-select-dropdown .el-popper {
        min-width: 348px;
      }
    }
    .agent-tab {
      .el-form-item__content .el-form-item__error {
        transform: translate(-50%, 0%) !important;
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

      .el-form-item.is-required:not(.is-no-asterisk)
        > .el-form-item__label:before,
      .el-form-item.is-required:not(.is-no-asterisk)
        .el-form-item__label-wrap
        > .el-form-item__label:before {
        right: 0px;
      }

      .checkbox-label {
        padding-left: 10px;
      }

      .el-form-item--medium .el-form-item__label {
        line-height: 30px;
      }

      .el-select .el-input--suffix .el-input__inner {
        padding-right: 40px !important;
      }
      .date-time {
        top: 30px;
      }
      .checkbox-info {
        padding-top: 8px;
        .el-checkbox__label {
          font-size: 13px;
        }
        .el-checkbox__input.is-checked + .el-checkbox__label {
          color: $dark !important;
          font-size: 13px;
        }
      }
      .tax-agent-taxno {
        .el-form-item__label {
          width: 100%;
          display: flex;
          // width: 100%;
          // margin-left: -7px;
        }
      }
    }
  }
}
</style>