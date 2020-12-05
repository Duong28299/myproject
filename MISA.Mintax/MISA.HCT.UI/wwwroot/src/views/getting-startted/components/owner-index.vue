<template>
  <div>
    <div class="logo">
      <img class="img-menu" src="@/assets/images/Union 28@2x.png" />
      <img class="img-logo" src="@/assets/images/Group 17877@2x.png" />
    </div>
    <div class="company">
      <div class="company-container">
        <div class="Gettingstartted-title">
          <h1 class="company-title">KHỞI TẠO THÔNG TIN ĐƠN VỊ CHỦ QUẢN</h1>
        </div>
        <div class="Gettingstartted-tab">
          <TwoStepProgress
            class="tab-bar"
            id="two-step-progress"
            step1Title="Thông tin chung"
            step2Title="Thông tin đại lý thuế"
            step3Title="Thành công"
            :isNext="isNextProgress"
          ></TwoStepProgress>
        </div>
        <div class="Gettingstartted-main">
          <el-form
            ref="formDataGeneral"
            v-show="tab1"
            :model="formDataGeneral"
            label-width="120px"
            label-position="top"
          >
            <el-row :gutter="24" style="padding-top: 12px">
              <el-col :lg="12" :md="12" :sm="24">
                <el-row :gutter="24">
                  <el-col :span="12">
                    <el-form-item
                      label="Mã số thuế"
                      prop="OrganizationTaxNo"
                      :rules="[
                        {
                          required: true,
                          message: 'Mã số thuế không được bỏ trống.',
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.OrganizationTaxNo"
                    >
                      <input-field
                        :number="true"
                        @hover="validate.OrganizationTaxNo = $event"
                        type="field-input"
                        :id="'OrganizationTaxNo'"
                        @value="getValueOrganizationTaxNo"
                        :valueInput="formDataGeneral.OrganizationTaxNo"
                        width="100%"
                        placeholder="Nhập mã số thuế"
                        :maxlength="13"
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
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.OrganizationName"
                    >
                      <input-field
                        @hover="validate.OrganizationName = $event"
                        type="field-input"
                        :isUpercasefirst="true"
                        @value="getValueOrganizationName"
                        :valueInput="formDataGeneral.OrganizationName"
                        id="OrganizationName"
                        width="100%"
                        placeholder="Nhập tên đơn vị"
                        :maxlength="255"
                      />
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
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.TaxPlaceName"
                    >
                      <input-field
                        @hover="validate.TaxPlaceName = $event"
                        type="field-input"
                        @value="getValueTaxPlaceName"
                        :id="'TaxPlaceName'"
                        :valueInput="formDataGeneral.TaxPlaceName"
                        width="100%"
                        placeholder="Nhập địa chỉ"
                        :maxlength="500"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="12">
                    <el-form-item label="Tỉnh/Thành phố">
                      <combo-box
                        type="label-none"
                        :filterable="true"
                        :clearable="true"
                        placeholder="Chọn tỉnh/thành phố"
                        :list="Provincals"
                        @value="getValueCustomerProvincial"
                        label="ProvinceName"
                        field="ProvinceID"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item label="Quận/Huyện">
                      <combo-box
                        type="label-none"
                        :filterable="true"
                        :clearable="true"
                        placeholder="Chọn quận/huyện"
                        :list="Districts"
                        :defaultString="formDataGeneral.DistrictID"
                        @value="getValueCustomerDistrict"
                        label="DistrictName"
                        field="DistrictID"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
              </el-col>

              <el-col :lg="12" :md="12" :sm="24">
                <el-row :gutter="24">
                  <el-col :span="12">
                    <el-form-item
                      prop="Phone"
                      label="Điện thoại"
                      :rules="[
                        {
                          required: true,
                          message: 'SĐT không được bỏ trống',
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.Phone"
                    >
                      <input-field
                        :number="true"
                        @hover="validate.Phone = $event"
                        :maxlength="12"
                        type="field-input"
                        @value="getValuePhoneGeneral"
                        :valueInput="formDataGeneral.Phone"
                        :id="'Phone'"
                        width="100%"
                        placeholder="Nhập số điện thoại"
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
                          message: 'Email không được bỏ trống',
                          trigger: ['blur', 'change'],
                        },
                        {
                          type: 'email',
                          message: 'Email chưa đúng định dạng',
                          trigger: ['blur'],
                        },
                        {
                          required: true,
                          message: 'Email không được bỏ trống',
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.Email"
                    >
                      <input-field
                        @hover="validate.Email = $event"
                        type="field-input"
                        @value="getValueEmail"
                        :valueInput="formDataGeneral.Email"
                        :id="'Email'"
                        width="100%"
                        placeholder="Nhập email"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="12" class="text-left">
                    <el-form-item label="Fax">
                      <input-field
                        :number="true"
                        type="field-input"
                        :maxlength="12"
                        @value="getValueFaxNoGeneral"
                        :valueInput="formDataGeneral.FaxNo"
                        placeholder="Nhập số fax"
                        width="100%"
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
                          message: 'Cơ quan thuế cấp cục không được bỏ trống.',
                          trigger: ['blur', 'change'],
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
                        :list="TaxAuthorities"
                        :defaultString="formDataGeneral.CAUnitName"
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
                          message: 'Cơ quan thuế quản lý không được bỏ trống.',
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.CAUnitManage"
                    >
                      <combo-box
                        type="label-none"
                        @hover="validate.CAUnitManage = $event"
                        :filterable="true"
                        placeholder="Chọn cơ quan thuế quản lý"
                        label="TaxAuthoritiesName"
                        :id="'CAUnitManage'"
                        :defaultString="formDataGeneral.CAUnitManage"
                        @value="getValueCAUnitManage"
                        :list="TaxAuthoritiesParent"
                        field="TaxAuthoritiesCode"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="12">
                    <el-form-item label="Số tài khoản ngân hàng">
                      <input-field
                        type="field-input"
                        width="100%"
                        @value="getValueBankNo"
                        :valueInput="formDataGeneral.BankNo"
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
                        label="BankName"
                        :list="Banks"
                        :defaultString="formDataGeneral.BankID"
                        @value="getValueBankID"
                        field="BankId"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row>
                  <el-col>
                    <el-form-item
                      label="Người ký tờ khai"
                      @keyup.tab="tabEvent"
                    >
                      <input-field
                        type="field-input"
                        width="100%"
                        @value="getValueSignBy"
                        :valueInput="formDataGeneral.SignBy"
                        placeholder="Nhập tên người ký tờ khai"
                        @keyup.tab="tabEvent"
                        :maxlength="125"
                        :isUpercasefirst="true"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
              </el-col>
            </el-row>
          </el-form>

          <el-form
            ref="formDataDetail"
            :model="formDataDetail"
            label-width="120px"
            label-position="top"
            v-show="tab2"
          >
            <el-row :gutter="24" style="padding-top: 12px">
              <el-col :lg="12" :md="12" :sm="24">
                <el-row>
                  <el-col :span="24">
                    <el-form-item label="Tên đại lý thuế" prop="TaxAgentName">
                      <input-field
                        @value="getValueTaxAgentName"
                        :valueInput="formDataDetail.TaxAgentName"
                        type="field-input"
                        :isUpercasefirst="true"
                        id="TaxAgentName"
                        width="100%"
                        placeholder="Nhập tên đại lý thuế"
                        :maxlength="255"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="12">
                    <el-form-item
                      label="Mã số thuế của đại lý thuế"
                      prop="TaxAgentTaxNo"
                    >
                      <input-field
                        :number="true"
                        type="field-input"
                        width="100%"
                        @value="getValueTaxAgentTaxNo"
                        :valueInput="formDataDetail.TaxAgentTaxNo"
                        placeholder="Nhập mã số thuế"
                        :maxlength="13"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row>
                  <el-col :span="24">
                    <el-form-item label="Địa chỉ trụ sở" prop="PlaceName">
                      <input-field
                        type="field-input"
                        width="100%"
                        @value="getValuePlaceName"
                        :valueInput="formDataDetail.PlaceName"
                        placeholder="Nhập địa chỉ"
                        :maxlength="500"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="12">
                    <el-form-item label="Tỉnh/Thành phố">
                      <combo-box
                        type="label-none"
                        :filterable="true"
                        :clearable="true"
                        placeholder="Chọn tỉnh/thành phố"
                        :list="Provincals"
                        @value="getValueAgencyProvincial"
                        label="ProvinceName"
                        field="ProvinceID"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item label="Quận/Huyện">
                      <combo-box
                        type="label-none"
                        :filterable="true"
                        :clearable="true"
                        placeholder="Chọn quận/huyện"
                        :list="Districts"
                        :defaultString="formDataDetail.DistrictID"
                        @value="getValueAgencyDistrict"
                        label="DistrictName"
                        field="DistrictID"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row style="padding-top: 16px">
                  <el-col :span="24">
                    <el-checkbox v-model="formDataDetail.IsDisplayTaxAgent"
                      >Hiển thị thông tin đại lý thuế trên tờ khai</el-checkbox
                    >
                  </el-col>
                </el-row>
              </el-col>

              <el-col :lg="12" :md="12" :sm="24">
                <el-row :gutter="24">
                  <el-col :sm="12">
                    <el-form-item prop="Phone" label="Điện thoại">
                      <input-field
                        :number="true"
                        type="field-input"
                        @value="getValuePhoneDetail"
                        :maxlength="12"
                        :valueInput="formDataDetail.Phone"
                        id="Phone"
                        width="100%"
                        placeholder="Nhập số điện thoại"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :sm="12">
                    <el-form-item
                      prop="Email"
                      label="Email"
                      :rules="[
                        {
                          required: false,
                          type: 'email',
                          message: 'Email chưa đúng định dạng',
                          trigger: ['blur'],
                        },
                      ]"
                      :show-message="validateTaxAgent.Email"
                    >
                      <input-field
                        type="field-input"
                        @hover="validateTaxAgent.Email = $event"
                        @value="getValueAgencyEmail"
                        :valueInput="formDataDetail.Email"
                        id="Email"
                        width="100%"
                        placeholder="Nhập email"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="12" class="text-left">
                    <el-form-item label="Fax">
                      <input-field
                        :number="true"
                        type="field-input"
                        :maxlength="12"
                        placeholder="Nhập số fax"
                        @value="getValueFaxNoDetail"
                        :valueInput="formDataDetail.FaxNo"
                        width="100%"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="12" class="text-left contract-tax">
                    <el-form-item
                      label="Hợp đồng đại lý thuế số"
                    ></el-form-item>
                  </el-col>
                </el-row>

                <el-row :gutter="24">
                  <el-col :span="12" class="text-left">
                    <el-form-item label="Số hợp đồng">
                      <input-field
                        type="field-input"
                        :maxlength="10"
                        @value="getValueAgencyNumberContract"
                        :valueInput="formDataDetail.ContractNo"
                        placeholder="Nhập số hợp đồng"
                        width="100%"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :lg="12" :md="10">
                    <el-form-item label="Ngày hợp đồng">
                      <date-picker
                        Valueformat="MM/dd/yyyy"
                        type="date"
                        formatInput="dd/MM/yyyy"
                        @getValue="getValueAgencyDateContract"
                        :value="formDataDetail.ContractDate"
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
                      @keyup.tab="tabEvent"
                    >
                      <input-field
                        type="field-input"
                        width="100%"
                        @value="getValueTaxAgentEmployee"
                        :valueInput="formDataDetail.TaxAgentEmployee"
                        placeholder="Nhập tên nhân viên đại lý thuế số"
                        @keyup.tab="tabEvent"
                        :maxlength="125"
                        :isUpercasefirst="true"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row>
                  <el-col>
                    <el-form-item
                      label="Chứng chỉ hành nghề"
                      @keyup.tab="tabEvent"
                    >
                      <input-field
                        type="field-input"
                        width="100%"
                        @value="getValuePracticingCertifacateNo"
                        :valueInput="formDataDetail.PracticingCertifacateNo"
                        placeholder="Nhập chứng chỉ hành nghề số"
                        @keyup.tab="tabEvent"
                        :maxlength="125"
                        :isUpercasefirst="true"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
              </el-col>
            </el-row>
          </el-form>
        </div>
      </div>
      <div class="Gettingstartted-footer">
        <div class="button-footer">
          <button-icon
            v-if="!isbutton"
            name="Tiếp tục"
            type="btn-primary"
            class="ml-10 btn-footer"
            :handleClick="handleClickNext"
            @AddAndOut="onClickNext('form')"
          ></button-icon>

          <div v-if="isCheck">
            <button-icon
              v-if="isbutton"
              name="Hoàn thành"
              type="btn-grey"
              class="ml-10 btn-footer btn-grey-right"
              :handleClick="handleClick"
              @AddAndOut="onSubmit('form')"
              :disabled="true"
            ></button-icon>
            <button-icon
              v-if="isbutton"
              name="Bỏ qua"
              type="btn-primary"
              class="btn-footer"
              :handleClick="handleClickBreack"
              @AddAndOut="onSubmit('form')"
            ></button-icon>
          </div>
          <div v-else>
            <button-icon
              v-if="isbutton"
              name="Hoàn thành"
              type="btn-primary"
              class="ml-10 btn-footer"
              :handleClick="handleClick"
              @AddAndOut="onSubmit('form')"
            ></button-icon>
            <button-icon
              v-if="isbutton"
              name="Bỏ qua"
              type="btn-white"
              class="btn-footer"
              :handleClick="handleClickBreack"
              @AddAndOut="onSubmit('form')"
            ></button-icon>
          </div>

          <button-icon
            v-if="isbutton"
            name="Quay lại"
            type="btn-grey"
            class="mr-8 fl btn-footer btn-grey-left"
            :handleClick="handleClickBack"
          ></button-icon>
        </div>
      </div>
      <div>
        <img
          class="img-background"
          src="@/assets/images/Group 18759.png"
          style="position: absolute; bottom: 0px; right: 5%"
        />

        <img
          class="img-background"
          src="@/assets/images/Group 18772.png"
          style="position: absolute; bottom: 0px; left: 3%"
        />
      </div>
    </div>
  </div>
</template>
<script>
import Button from "@/components/buttons";
import Inputfield from "@/components/inputs";
import DatePicker from "@/components/date-time-pickers/date-time-picker-v2";
import ComboBox from "@/components/select";
import TwoStepProgress from "@/components/progress";
import apiGetOrganizationUnit from "@/api/etax/organization.js";
import apiOrganizationUnit from "@/api/etax/organization-unit.js";
import { getTokenClaimFromToken } from "@/utils/token";
import CategoryDictionary from "@/api/etax/category-dictionary";
import Cookie from "@/utils/cookie.js";
export default {
  name: "OwnerOrganization",
  components: {
    TwoStepProgress,
    "button-icon": Button,
    "input-field": Inputfield,
    "combo-box": ComboBox,
    "date-picker": DatePicker,
    // "input-field": InputNumber,
  },
  props: {},
  data() {
    return {
      isbutton: false,
      tab1: true,
      tab2: false,
      isCheck: false, // Biến theo dõi sự thay đổi của formdata
      /**
       * Biến để kiểm tra xem đã chuyển sang bước tiếp theo hay chưa
       */
      isNextProgress: false,
      validate: {
        OrganizationName: false, // Tên đơn vị
        OrganizationTaxNo: false, // Mã số thuế đơn vị
        TaxPlaceName: false, // Địa chỉ đơn vị
        Phone: false, // SSĐT đơn vị
        Email: false, // Emai đơn vị
        CAUnitName: false,
        CAUnitManage: false,
      },
      validateTaxAgent: {
        Email: true,
      },
      formDataGeneral: {
        OrganizationName: "",
        OrganizationTaxNo: "",
        TaxPlaceName: "",
        SignBy: "",
        FaxNo: "",
        DistrictID: null,
        ProvinceID: null,
        Phone: "",
        Email: "",
        BankNo: "",
        CAUnitName: null,
        CAUnitManage: null,
        AgencyBankNo: "",
        CreatingBussinessType: "",
        DateOfIssue: "",
        Inactive: "",
        IsBusiness: "",
        IsParent: 1,
        IsProduce: "",
        IsSupport: "",
        MISACode: "",
        MainTask: "",
        OrganizationUnitCode: "",
        OrganizationUnitID: null,
        OrganizationUnitTypeID: null,
        OrganizationUnitTypeName: "",
        OrganizationAmisID: null,
        ParentID: null,
        PlaceOfIssue: "",
        QuantityEmployee: "",
        TaxAgent: null,
      },
      formDataDetail: {
        TaxAgentEmployee: "",
        FaxNo: "",
        TaxAgentName: "",
        TaxAgentTaxNo: "",
        PlaceName: "",
        Phone: "",
        Email: "",
        ProvinceID: null,
        DistrictID: null,
        ContractNo: "",
        ContractDate: null,
        PracticingCertifacateNo: "",
        IsDisplayTaxAgent: "", //có hiển thị thông tin đại lý thuế trên tờ khai không
      },
      Provincals: [], //tỉnh thành phố
      Districts: [], //quận huyện
      TaxAuthorities: [], //co quan thue cấp cục
      TaxAuthoritiesParent: [], //co quan thue quản lí
      Banks: [],
    };
  },
  methods: {
    tabEvent() {
    },
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

    /**
     * Lấy giá trị các trường input vào @param form
     * Created by: TVPhi 20/8/2020
     */
    getValueOrganizationName(val) {
      this.formDataGeneral.OrganizationName = val;
    },
    getValueOrganizationTaxNo(val) {
      this.formDataGeneral.OrganizationTaxNo = val;
    },
    getValueTaxPlaceName(val) {
      this.formDataGeneral.TaxPlaceName = val;
    },
    getValueCustomerProvincial(val) {
      this.formDataGeneral.ProvinceID = val;
    },
    getValueCustomerDistrict(val) {
      this.formDataGeneral.DistrictID = val;
    },
    getValuePhoneGeneral(val) {
      this.formDataGeneral.Phone = val;
    },
    getValueEmail(val) {
      this.formDataGeneral.Email = val;
    },
    getValueFaxNoGeneral(val) {
      this.formDataGeneral.FaxNo = val;
    },
    getValueCAUnitName(val) {
      this.formDataGeneral.CAUnitName = val;
    },
    getValueCAUnitManage(val) {
      this.formDataGeneral.CAUnitManage = val;
    },
    getValueBankID(val) {
      this.formDataGeneral.BankID = val;
    },
    getValueBankNo(val) {
      this.formDataGeneral.BankNo = val;
    },
    getValueSignBy(val) {
      this.formDataGeneral.SignBy = val;
    },

    getValueTaxAgentName(val) {
      this.formDataDetail.TaxAgentName = val;
    },
    getValueTaxAgentTaxNo(val) {
      this.formDataDetail.TaxAgentTaxNo = val;
    },
    getValuePlaceName(val) {
      this.formDataDetail.PlaceName = val;
    },
    getValueAgencyProvincial(val) {
      this.formDataDetail.ProvinceID = val;
    },
    getValueAgencyDistrict(val) {
      this.formDataDetail.DistrictID = val;
    },
    getValuePhoneDetail(val) {
      this.formDataDetail.Phone = val;
    },
    getValueAgencyEmail(val) {
      this.formDataDetail.Email = val;
    },
    getValueFaxNoDetail(val) {
      this.formDataDetail.FaxNo = val;
    },
    getValueAgencyNumberContract(val) {
      this.formDataDetail.ContractNo = val;
    },
    getValueAgencyDateContract(val) {
      this.formDataDetail.ContractDate = val;
    },
    getValueTaxAgentEmployee(val) {
      this.formDataDetail.TaxAgentEmployee = val;
    },
    /**
     * Số chứng chỉ hành nghề
     */
    getValuePracticingCertifacateNo(val) {
      this.formDataDetail.PracticingCertifacateNo = val;
    },

    handleClickNext() {
      this.onClickNext("formDataGeneral");
    },
    handleClickBack() {
      this.tab2 = false;
      this.tab1 = true;
      this.isbutton = false;
      this.isNextProgress = false;
    },
    handleClickBreack() {
      this.onSubmit(true);
    },
    handleClick() {
      this.onSubmit(false);
    },

    /**
     * Hàm gọi service
     * Created by : TVPhi 21/08/2020
     */
    async getOrganizationUnit() {
      var res = await apiGetOrganizationUnit.getOrganizationByTaxCode("", 1);
      if (typeof res.data.Data != "undefined" && res.data.Success) {
        this.mapDataEdit(res.data.Data);
      } else {
        //this.formData = [];
        this.popUpFalse(
          "Thất bại",
          "Lỗi service, Không thêm được đơn vị chủ quản"
        );
      }
      this.$refs["formDataGeneral"].resetFields();
    },

    async onClickNext(formData) {
      var res = await apiGetOrganizationUnit.checkDuplicateTaxCode(
        this.formDataGeneral.OrganizationTaxNo
      );
      if (res.data.Success) {
        this.popUpFalse(
          "Thất bại",
          "Mã số thuế bị trùng với đơn vị đã có trên hệ thống. Vui lòng kiểm tra lại."
        );
      } else {
        this.$refs[formData].validate(async (valid, index) => {
          if (index !== undefined) {
            if (Object.keys(index)[0] != null) {
              this.tab1 = true;
              this.tab2 = false;
              this.isbutton = false;
              this.isNextProgress = false;
              setTimeout(() => {
                document.getElementById([`${Object.keys(index)[0]}`]).focus();
              }, 200);
            }
          }
          if (valid) {
            this.tab2 = true;
            this.tab1 = false;
            this.isbutton = true;
            this.isNextProgress = true;
            this.isCheck = true;

            //Kiểm tra các trường trong thông tin đại lý thuế, nếu tất cả các trường không có dữ liệu thì không enable button hoàn thành
            if (
              this.formDataDetail.TaxAgentEmployee == "" &&
              this.formDataDetail.FaxNo == "" &&
              this.formDataDetail.TaxAgentName == "" &&
              this.formDataDetail.TaxAgentTaxNo == "" &&
              this.formDataDetail.PlaceName == "" &&
              this.formDataDetail.Phone == "" &&
              this.formDataDetail.Email == "" &&
              this.formDataDetail.ProvinceID == null &&
              this.formDataDetail.DistrictID == null &&
              this.formDataDetail.ContractNo == "" &&
              this.formDataDetail.ContractDate == null &&
              this.formDataDetail.PracticingCertifacateNo == ""
            ) {
              this.isCheck = true;
            } else this.isCheck = false;
          }
        });
      }
    },

    /**
     * Thực hiện service đơn vị chủ quản
     * Created by : TVPhi - 20/08/2020
     * Content modification: thêm phần xử lý sự kiện sửa người nộp thuế
     */
    async onSubmit(isGeneral) {
      /**
       * Format dữ liệu đầu vào
       * Created by : TVPhi - 20/08/2020
       */
      if (isGeneral) {
        var objectSend = this.formDataGeneral;

        const res = await this.insertOrganizationUnit(objectSend);
        if (res.data.Success) {
          Cookie.setOrganizationCookie(res.data.Data);
          var oranizationName = await this.getOrganizationById(res.data.Data);
          await this.installOrganizationUnit();
          this.setSession("oranizationName", oranizationName);
          this.$_Popup.success(
            "CHÚC MỪNG BẠN",
            "Khởi tạo đơn vị thành công",
            []
          );
          setTimeout(() => {
            this.$_Popup.resetState(); // Xóa popup thông báo
            this.$router.push("/tax-period");
          }, 3000);
        } else {
          this.popUpFalse("Thất bại", "Đã xảy ra lỗi vui lòng kiểm tra lại.");
        }
      } else {
        this.$refs["formDataDetail"].validate(async (valid, index) => {
          if (index !== undefined) {
            if (Object.keys(index)[0] != null) {
              setTimeout(() => {
                document.getElementById([`${Object.keys(index)[0]}`]).focus();
              }, 200);
            }
          }
          if (valid) {
            this.formDataGeneral.TaxAgent = this.formDataDetail;
            this.objectSend = this.formDataGeneral;
            const res = await this.insertOrganizationUnit(objectSend);
            if (res.data.Success) {
              await this.installOrganizationUnit();
              this.$_Popup.success(
                "CHÚC MỪNG BẠN",
                "Khởi tạo đơn vị thành công",
                []
              );
              setTimeout(() => {
                this.$_Popup.resetState(); // Xóa popup thông báo
                this.$router.push("tax-period");
              }, 3000);
            } else {
              this.popUpFalse(
                "Thất bại",
                "Đã xảy ra lỗi vui lòng kiểm tra lại 123."
              );
            }
          }
        });
      }
    },

    /**
     * Hàm gọi pupup khi service bị lỗi không thêm được đơn vị
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
     * Map dữ liệu trả về thông tin đơn vị chủ quản
     * Created by: TVPhi 21/08/2020
     */

    mapDataEdit(data) {
      var token = localStorage.getItem("mt-token");
      var tenantCookie = getTokenClaimFromToken("tenant_id", token);
      this.formDataGeneral.OrganizationAmisID = tenantCookie;
      var result = [];
      var item = { ...data };
      var obj = {};
      // Dữ liệu phía thông tin chung
      obj.OrganizationAmisID = item["OrganizationUnitID"];
      obj.OrganizationName = item["OrganizationUnitName"];
      obj.OrganizationTaxNo = item["TaxCode"];
      if (obj.OrganizationTaxNo == null) {
        obj.OrganizationTaxNo = item["BusinessRegistrationCode"];
      } else {
        obj.OrganizationTaxNo = item["OrganizationTaxNo"];
      }
      if (obj.OrganizationTaxNo == null) obj.OrganizationTaxNo = "";
      obj.TaxPlaceName = item["Address"];
      (obj.OrganizationUnitTypeName = item["OrganizationUnitTypeName"]),
        (obj.OrganizationUnitTypeID = item["OrganizationUnitTypeID"]),
        (obj.OrganizationUnitCode = item["OrganizationUnitCode"]),
        (obj.MISACode = item["MISACode"]),
        (obj.ParentID = item["ParentID"]),
        (obj.PlaceOfIssue = item["PlaceOfIssue"]),
        (obj.QuantityEmployee = item["QuantityEmployee"]),
        result.push(obj);
      this.formDataGeneral = { ...this.formDataGeneral, ...result[0] };
    },

    /**
     * Service thêm đơn vị
     * Created by TVPhi - 21/8/2020
     */
    async insertOrganizationUnit(obj) {
      return await apiGetOrganizationUnit.insertOrganization(obj);
    },

    /**
     * Cài đặt các bộ phận/phòng ban khi cài đặt thành công đơn vị chủ quản
     */
    async installOrganizationUnit() {
      return await apiOrganizationUnit.installOrganizationUnit();
    },
    /**
     * Lấy đơn vị chủ quản theo Id
     * Created by TCDUONG - 9/9/2020
     */
    async getOrganizationById(val) {
      var organization = await apiGetOrganizationUnit.getOrganizationById(val);
      return organization.data.Data.OrganizationName;
    },
    setSession(name, val) {
      if (sessionStorage.getItem(name) !== null) {
        sessionStorage.removeItem(name);
      }
      // tạo sessionStorage
      sessionStorage.setItem(name, val);
      return sessionStorage.getItem(name);
    },
    /**
     * lay danh sach tinh thanh pho
     */
    async getProvincials() {
      const res = await CategoryDictionary.getProvincials();
      this.Provincals = res.data.Data;
    },

    async getTaxAuthorities() {
      const res = await CategoryDictionary.getTaxAuthorities();
      this.TaxAuthorities = res.data.Data;
    },
    async getListTaxAuthoritiesParent(val) {
      const res = await CategoryDictionary.getListTaxAuthoritiesParent(val);
      this.TaxAuthoritiesParent = res.data.Data;
    },
    async getBanks() {
      const res = await CategoryDictionary.getBanks();
      this.Banks = res.data.Data;
    },
    async getDistricts(val) {
      const res = await CategoryDictionary.getDistrictsByDistrictsID(val);
      this.Districts = res.data.Data;
    },
  },
  created() {
    this.getOrganizationUnit();
    this.getProvincials();
    this.getTaxAuthorities();
    this.getBanks();
  },
  watch: {
    formDataDetail: {
      handler(val) {
        if (
          val.TaxAgentEmployee == "" &&
          val.FaxNo == "" &&
          val.TaxAgentName == "" &&
          val.TaxAgentTaxNo == "" &&
          val.PlaceName == "" &&
          val.Phone == "" &&
          val.Email == "" &&
          val.ProvinceID == null &&
          val.DistrictID == null &&
          val.ContractNo == "" &&
          val.ContractDate == null &&
          val.PracticingCertifacateNo == ""
        ) {
          this.isCheck = true;
        } else this.isCheck = false;
      },
      deep: true,
    },
    "formDataGeneral.ProvinceID": {
      handler(val) {
        this.Districts.splice(0, this.Districts.length);
        this.formDataGeneral.DistrictID = null;
        if (val !== null) this.getDistricts(val);
      },
    },
    "formDataDetail.ProvinceID": {
      handler(val) {
        this.Districts.splice(0, this.Districts.length);
        this.formDataDetail.DistrictID = null;
        if (val !== null) {
          this.getDistricts(val);
        }
      },
    },
    "formDataGeneral.CAUnitName": {
      handler(val) {
        this.getListTaxAuthoritiesParent(val);
      },
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
@media screen and (max-width: 1300px) {
  .img-background {
    display: none !important;
  }
}
.getting-started-layout {
  .logo {
    display: flex;
    height: 60px;
    align-items: center;
    z-index: 100;
    box-shadow: 0px 4px 4px -4px $box-shadow-grey;
    background-color: $white;
    position: absolute;
    top: 0;
    left: 0;
    width: 210px;

    .img-menu {
      width: 16px;
      height: 16px;
      margin-left: 20px;
      margin-right: 20px;
    }

    .img-logo {
      height: 40px;
    }
  }
  .company {
    ::placeholder {
      font-size: 11px !important;
    }

    background-color: $background-light-grey;
    min-width: 800px;
    .el-checkbox__label {
      color: $dark;
    }
    .el-form-item.is-required:not(.is-no-asterisk)
      > .el-form-item__label:before,
    .el-form-item.is-required:not(.is-no-asterisk)
      .el-form-item__label-wrap
      > .el-form-item__label:before {
      right: -13px;
    }
    .el-form-item {
      margin-bottom: 0px !important;
    }
    .el-form-item__label {
      margin: 0 !important;
    }
    .el-form {
      overflow-y: auto;
      height: 100%;
      overflow-x: hidden;
    }
    position: absolute;
    left: 0px;
    right: 0px;
    bottom: 0px;
    top: 60px;
    //background-color: $bg-color-2;
    .el-form-item__label {
      margin-bottom: 0;
      font-family: "GoogleSansRegular";
      font-weight: normal;
      font-size: 13px;
      color: $dark;
    }
    &-container {
      overflow: hidden;
      width: 792px;
      height: calc(100% - 122px);
      position: fixed;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      background-color: $white;
      .Gettingstartted-title {
        height: 70px;
        text-align: center;
        .company-title {
          font-size: 20px;
          font-family: $_fontbold;
          padding-top: 30px;
        }
      }
      .Gettingstartted-tab {
        border-bottom: 1px solid $bg-color-3;
        height: 60px;
        display: flex;
        justify-content: center;
        .tab-bar {
          height: 40px;
        }
      }
      .Gettingstartted-main {
        height: calc(100% - 152px);
        padding: 0 24px 10px;
        .contract-tax .el-form-item__label {
          font-size: 13px !important;
          font-family: $_fontbold !important;
        }
      }
    }
    .Gettingstartted-footer {
      height: 73px;
      position: fixed;
      bottom: 0px;
      width: 100%;
      .button-footer {
        background-color: $white;
        padding-right: 24px;
        border-top: 1px solid $bg-color-3;
        width: 792px;
        height: 70px;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        .btn-footer {
          padding: 16px 0 0 0;
          .btn-grey {
            border: none !important;
          }

          .el-button.is-disabled {
            background-color: #e8eef5 !important;
          }
        }
        .btn-grey-left {
          margin-left: 24px !important;
        }
        .fl {
          float: left !important;
        }
      }
    }
    .input-field .el-input__inner {
      text-align: left;
    }
  }
}
</style>
