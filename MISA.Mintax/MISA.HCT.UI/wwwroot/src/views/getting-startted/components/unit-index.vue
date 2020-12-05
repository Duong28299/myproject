<template>
  <div class="list-unit">
    <!-- <Header /> -->
    <div class="logo">
      <img class="img-menu" src="@/assets/images/Union 28@2x.png" />
      <img class="img-logo" src="@/assets/images/Group 17877@2x.png" />
    </div>
    <div class="company">
      <el-col class="d-flex" :span="16" style="margin: 14px 0 0 100px">
        <button-icon
          type="btn-white2-onlyicon"
          tooltip="Quay lại"
          :handleClick="openWarnDialog"
          class="btn-expand"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/ico-expand.png" />
          </template>
        </button-icon>
        <div>
          <p class="text-back" style="font-size: 13px;">Danh sách đơn vị</p>
        </div>
      </el-col>
      <div class="company-container">
        <div class="Gettingstartted-title">
          <h1 class="company-title">KHỞI TẠO ĐƠN VỊ</h1>
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
                <el-row>
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
                        :isUpercasefirst="true"
                        @value="getOrganizationTaxNo"
                        :valueInput="formDataGeneral.OrganizationTaxNo"
                        id="OrganizationTaxNo"
                        width="100%"
                        placeholder="Nhập mã số thuế"
                        :maxlength="13"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-row :gutter="24">
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
                        @value="getOrganizationName"
                        :valueInput="formDataGeneral.OrganizationName"
                        width="100%"
                        id="OrganizationName"
                        placeholder="Nhập tên đơn vị"
                        :maxlength="255"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-row>
                  <el-col>
                    <el-form-item
                      label="Cấp tổ chức"
                      prop="UnitOrgLevel"
                      :rules="[
                          {
                            required: true,
                            message: 'Cơ quan thuế cấp cục không được bỏ trống.',
                            trigger: ['blur', 'change'],
                          },
                        ]"
                      :show-message="validate.UnitOrgLevel"
                    >
                      <combo-box
                        @hover="validate.UnitOrgLevel = $event"
                        type="label-none"
                        :filterable="true"
                        placeholder="Chọn/nhập văn phòng/chi nhánh"
                        label="NAME"
                        field="VALUE"
                        :list="organizationLevel"
                        @value="getUnitOrgLevel"
                        id="UnitOrgLevel"
                        :defaultNumber="formDataGeneral.UnitOrgLevel"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-row v-if="this.formDataGeneral.UnitOrgLevel == 1">
                  <el-col :span="24" class="text-left contract-tax">
                    <el-form-item label="Hạch toán theo">
                      <el-radio-group v-model="formDataGeneral.Accounting">
                        <el-radio :label="0">Hạch toán độc lập</el-radio>
                        <el-radio :label="1">Hạch toán phụ thuộc</el-radio>
                      </el-radio-group>
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-row :gutter="24">
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
                        @value="getTaxPlaceName"
                        :valueInput="formDataGeneral.TaxPlaceName"
                        id="TaxPlaceName"
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
                        placeholder="Chọn tỉnh/thành phố"
                        label="ProvinceName"
                        field="ProvinceID"
                        :list="Provincals"
                        :defaultString="formDataGeneral.ProvinceID"
                        @value="getValueCustomerProvincial"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item label="Quận/Huyện">
                      <combo-box
                        type="label-none"
                        :filterable="true"
                        placeholder="Chọn quận/huyện"
                        label="DistrictName"
                        field="DistrictID"
                        :list="Districts"
                        @value="getValueDistrictCode"
                        :defaultString="formDataGeneral.DistrictID"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
              </el-col>

              <el-col :lg="12" :md="12" :sm="24">
                <el-row :gutter="24">
                  <el-col :span="12">
                    <el-form-item
                      label="Số điện thoại"
                      prop="Phone"
                      :rules="[
                          {
                            required: true,
                            message: 'Số điện thoại không được bỏ trống.',
                            trigger: ['blur', 'change'],
                          },
                        ]"
                      :show-message="validate.Phone"
                    >
                      <input-field
                        :number="true"
                        @hover="validate.Phone = $event"
                        type="field-input"
                        size="medium"
                        placeholder="Nhập số điện thoại"
                        :maxlength="12"
                        @value="getPhone"
                        :valueInput="formDataGeneral.Phone"
                        id="Phone"
                      ></input-field>
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item
                      prop="Email"
                      label="Email"
                      :rules="[
                          {
                            required: true,
                            message: 'Email  không được bỏ trống.',
                            trigger: ['blur'],
                          },
                          {
                            required: true,
                            type: 'email',
                            message: 'Email chưa đúng định dạng.',
                            trigger: ['blur'],
                          },
                        ]"
                      :show-message="validate.Email"
                    >
                      <input-field
                        @hover="validate.Email = $event"
                        type="field-input"
                        @value="getEmail"
                        :valueInput="formDataGeneral.Email"
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
                        type="field-input"
                        :number="true"
                        :maxlength="12"
                        @value="getFaxNo"
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
                        placeholder="Chọn cơ quan thuế cấp cục"
                        :list="TaxAuthorities"
                        label="TaxAuthoritiesName"
                        field="TaxAuthoritiesCode"
                        id="CAUnitName"
                        :defaultString="formDataGeneral.CAUnitName"
                        @value="getCAUnitName"
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
                        @hover="validate.CAUnitManage = $event"
                        type="label-none"
                        :filterable="true"
                        placeholder="Chọn cơ quan thuế quản lý"
                        label="TaxAuthoritiesName"
                        :list="TaxAuthoritiesParent"
                        field="TaxAuthoritiesCode"
                        id="CAUnitManage"
                        :defaultString="formDataGeneral.CAUnitManage"
                        @value="getCAUnitManage"
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
                        @value="getBankNo"
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
                        field="BankId"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row>
                  <el-col>
                    <el-form-item label="Người ký tờ khai" @keyup.tab="tabEvent">
                      <input-field
                        type="field-input"
                        width="100%"
                        @value="getSignBy"
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
            v-show="tab2"
            :model="formDataDetail"
            label-width="120px"
            label-position="top"
          >
            <el-row :gutter="24" style="padding-top: 12px">
              <el-col :lg="12" :md="12" :sm="24">
                <el-row>
                  <el-col :span="24">
                    <el-form-item label="Tên đại lý thuế" prop="TaxAgentName">
                      <input-field
                        @value="getTaxAgentName"
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
                    <el-form-item label="Mã số thuế của đại lý thuế" prop="TaxAgentTaxNo">
                      <input-field
                        type="field-input"
                        :number="true"
                        width="100%"
                        @value="getTaxAgentCode"
                        :valueInput="this.formDataDetail.TaxAgentTaxNo"
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
                        @value="getTaxAgentAddress"
                        :valueInput="this.formDataDetail.PlaceName"
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
                        placeholder="Chọn tỉnh/thành phố"
                        label="ProvinceName"
                        field="ProvinceID"
                        :list="Provincals"
                        :defaultString="formDataDetail.ProvinceID"
                        @value="getValueAgencyProvincial"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item label="Quận/Huyện">
                      <combo-box
                        type="label-none"
                        :filterable="true"
                        placeholder="Chọn quận/huyện"
                        label="DistrictName"
                        field="DistrictID"
                        :list="Districts"
                        @value="getValueTaxAgentDistrictCode"
                        :defaultString="formDataDetail.DistrictID"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
              </el-col>

              <el-col :lg="12" :md="12" :sm="24">
                <el-row :gutter="24">
                  <el-col :span="12">
                    <el-form-item label="Số điện thoại" prop="Phone">
                      <input-field
                        type="field-input"
                        :number="true"
                        @value="getTaxAgentPhone"
                        :valueInput="formDataDetail.Phone"
                        id="phone"
                        width="100%"
                        :maxlength="12"
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
                              required: false,
                              type: 'email',
                              message: 'Email chưa đúng định dạng.',
                              trigger: ['blur'],
                            },
                          ]"
                      :show-message="validate.Email"
                    >
                      <input-field
                        type="field-input"
                        @hover="validate.Email = $event"
                        @value="getTaxAgentEmail"
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
                    <el-form-item label="Fax" prop="FaxNo">
                      <input-field
                        type="field-input"
                        :maxlength="12"
                        :number="true"
                        placeholder="Nhập số fax"
                        width="100%"
                        @value="getTaxAgentFax"
                        :valueInput="formDataDetail.FaxNo"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-row :gutter="24">
                  <el-col :span="12" class="text-left contract-tax">
                    <el-form-item label="HỢP ĐỒNG ĐẠI LÝ SỐ"></el-form-item>
                  </el-col>
                </el-row>

                <el-row :gutter="24">
                  <el-col :span="12" class="text-left">
                    <el-form-item label="Số hợp đồng">
                      <input-field
                        type="field-input"
                        @value="getContractNo"
                        :valueInput="this.formDataDetail.ContractNo"
                        :maxlength="125"
                        placeholder="Nhập số hợp đồng"
                        width="100%"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :lg="12" :md="10">
                    <el-form-item label="Ngày hợp đồng">
                      <date-picker
                        @getValue="formDataDetail.ContractDate = $event"
                        :value="formDataDetail.ContractDate"
                        Valueformat="MM/dd/yyyy"
                        type="date"
                        formatInput="dd/MM/yyyy"
                        width="100%"
                        placeholder="_ _ /_ _ /_ _ _ _"
                      ></date-picker>
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-row>
                  <el-col>
                    <el-form-item label="Nhân viên đại lý thuế" @keyup.tab="tabEvent">
                      <input-field
                        type="field-input"
                        width="100%"
                        @value="getEmployeeName"
                        :valueInput="this.formDataDetail.TaxAgentEmployee"
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
                    <el-form-item label="Chứng chỉ hành nghề số" @keyup.tab="tabEvent">
                      <input-field
                        type="field-input"
                        width="100%"
                        @value="getPracticingCertifacateNo"
                        :valueInput="this.formDataDetail.PracticingCertifacateNo"
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
      <div class="gettingstartted-footer">
        <div class="button-footer">
          <button-icon
            v-if="tab1 == true"
            name="Tiếp tục"
            type="btn-primary"
            class="ml-10 btn-footer"
            :handleClick="handleClickNext"
          ></button-icon>
          <button-icon
            v-if="tab1 == false && isCheck == false"
            name="Hoàn thành"
            type="btn-primary"
            class="ml-10 btn-footer"
            :handleClick="handleClick"
            :disabled="true"
          ></button-icon>

          <button-icon
            v-if="tab1 == false && isCheck == true"
            name="Hoàn thành"
            type="btn-primary"
            class="ml-10 btn-footer"
            :handleClick="handleClick"
          ></button-icon>

          <button-icon
            v-if="tab1 == false && isCheck == false"
            name="Bỏ qua"
            type="btn-primary"
            class="btn-footer btn-left"
            :handleClick="ignore"
          ></button-icon>
          <button-icon
            v-if="tab1 == false && isCheck == true"
            name="Bỏ qua"
            type="btn-white"
            class="btn-footer"
            :handleClick="ignore"
          ></button-icon>
          <button-icon
            v-if="tab1 == false"
            name="Quay lại"
            type="btn-grey"
            class="mr-8 fl btn-footer"
            :handleClick="changetab1"
          ></button-icon>
        </div>
      </div>

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
</template>
<script>
// import header from "@/layout/components/header.vue";
import Button from "@/components/buttons";
import Inputfield from "@/components/inputs";
import DatePicker from "@/components/date-time-pickers/date-time-picker-v2";
import ComboBox from "@/components/select";
import Organization from "@/api/etax/organization";
import gettingStarted from "@/enums/getting-started";
import TwoStepProgress from "@/components/progress";
import CategoryDictionary from "@/api/etax/category-dictionary";
import Cookie from "@/utils/cookie.js";
//import locationService from "@/api/etax/locations";
export default {
  name: "UnitOrganization",
  components: {
    // Header: header,
    "button-icon": Button,
    "input-field": Inputfield,
    "combo-box": ComboBox,
    "date-picker": DatePicker,
    TwoStepProgress,
  },
  props: {},
  data() {
    return {
      isReturn: true, //Biến cho phép hiện popup warn khi click button quay lại danh sách đơn vị
      buttons: [], // list button trong popup warn
      organizationLevel: [],
      Provincals: [], //tỉnh thành phố
      Districts: [], //quận huyện
      TaxAuthorities: [], //co quan thue cấp cục
      TaxAuthoritiesParent: [], //co quan thue quản lí
      Banks: [],
      tab1: true,
      tab2: false,
      isCheck: false, //bien disable button hoan thanh
      isNextProgress: false,
      UnitOrgLevel: [
        {
          NAME: "Công ty con",
          VALUE: "0",
        },
        {
          NAME: "Chi nhánh",
          VALUE: "1",
        },
      ],
      validate: {
        OrganizationTaxNo: false,
        OrganizationName: false,
        UnitOrgLevel: false,
        Phone: false,
        Email: false,
        CAUnitName: false,
        CAUnitManage: false,
        TaxPlaceName: false,
      },
      formDataGeneral: {
        //OrganizationID: "",
        OrganizationTaxNo: "", //mã số thuế của đơn vị
        OrganizationName: "", //tên đơn vị
        TaxPlaceName: "", //địa chỉ đơn vị
        DistrictID: "",
        ProvinceID: "",
        UnitOrgLevel: null,
        Accounting: 0,
        Phone: "",
        BankNo: "",
        Email: "",
        FaxNo: "", //số fax
        CAUnitName: null, //cơ quan thuế cấp cục
        CAUnitManage: null, //cơ quan thuế cấp quản lí
        SignBy: "",
      },
      formDataDetail: {
        TaxAgentName: "",
        TaxAgentTaxNo: "",
        PlaceName: "",
        ProvinceID: "",
        DistrictID: "",
        Phone: "",
        Email: "",
        FaxNo: "",
        ContractNo: "",
        ContractDate: null,
        TaxAgentEmployee: "", //tên nhân viên nộp tờ khai
        PracticingCertifacateNo: "", // số chứng chỉ hành nghề
      },
    };
  },
  created() {
    this.organizationLevel = gettingStarted.ORGANIZATION_LEVEL;
    this.getProvincials();
    this.getTaxAuthorities();
    this.getBanks();
  },
  methods: {
    async getProvincials() {
      const res = await CategoryDictionary.getProvincials();
      this.Provincals = res.data.Data;
    },

    async getDistricts(val) {
      const res = await CategoryDictionary.getDistrictsByDistrictsID(val);
      this.Districts = res.data.Data;
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
    /**
     * hàm chuyển sang tab 1
     * created by nnphong - 20/08/2020
     */
    changetab1() {
      this.tab1 = true;
      this.tab2 = false;
      this.isNextProgress = false;
    },
    /**
     * Lấy các giá trị từ các trường input vào @param formData
     * created by nnphong - 20/08/2020
     */
    getOrganizationTaxNo(val) {
      this.formDataGeneral.OrganizationTaxNo = val;
    },
    getOrganizationName(val) {
      this.formDataGeneral.OrganizationName = val;
    },
    getUnitOrgLevel(val) {
      this.formDataGeneral.UnitOrgLevel = val;
    },
    getTaxPlaceName(val) {
      this.formDataGeneral.TaxPlaceName = val;
    },
    getPhone(val) {
      this.formDataGeneral.Phone = val;
    },
    getEmail(val) {
      this.formDataGeneral.Email = val;
    },
    getFaxNo(val) {
      this.formDataGeneral.FaxNo = val;
    },
    getCAUnitName(val) {
      this.formDataGeneral.CAUnitName = val;
    },
    getCAUnitManage(val) {
      this.formDataGeneral.CAUnitManage = val;
    },
    getBankNo(val) {
      this.formDataGeneral.BankNo = val;
    },
    getSignBy(val) {
      this.formDataGeneral.SignBy = val;
    },
    getValueDistrictCode(val) {
      this.formDataGeneral.DistrictID = val;
    },
    getTaxAgentName(val) {
      this.formDataDetail.TaxAgentName = val;
    },
    getTaxAgentCode(val) {
      this.formDataDetail.TaxAgentTaxNo = val;
    },
    getTaxAgentAddress(val) {
      this.formDataDetail.PlaceName = val;
    },
    getValueCustomerProvincial(val) {
      this.formDataGeneral.ProvinceID = val;
    },
    getTaxAgentPhone(val) {
      this.formDataDetail.Phone = val;
    },
    getTaxAgentEmail(val) {
      this.formDataDetail.Email = val;
    },
    getTaxAgentFax(val) {
      this.formDataDetail.FaxNo = val;
    },
    getContractNo(val) {
      this.formDataDetail.ContractNo = val;
    },
    getEmployeeName(val) {
      this.formDataDetail.TaxAgentEmployee = val;
    },
    getValueAgencyProvincial(val) {
      this.formDataDetail.ProvinceID = val;
    },
    getPracticingCertifacateNo(val) {
      this.formDataDetail.PracticingCertifacateNo = val;
    },
    getValueTaxAgentDistrictCode(val) {
      this.formDataDetail.DistrictID = val;
    },
    handleClick() {
      this.onSubmit("formDataDetail");
    },
    openWarnDialog() {
      this.buttons = [
        {
          text: "Có",
          class: "btn-primary",
          callback: this.returnConfigSetting,
        },
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
      ];
      if (this.isReturn == false) {
        this.$_Popup.warn(
          "Cảnh báo",
          'Bạn có chắc chắn muốn quay lại màn hình "Danh sách đơn vị" ? ',
          this.buttons
        );
      } else {
        this.$router.push("/getting-started");
      }
    },
    returnConfigSetting() {
      this.$router.push("/getting-started");
    },
    handleClickNext() {
      this.changetab2("formDataGeneral");
    },
    /**
     * hàm chuyển sang tab 2
     * created by nnphong - 20/08/2020
     */
    async changetab2(formData) {
      var res = await Organization.checkDuplicateTaxCode(
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
                document.getElementById(`${Object.keys(index)[0]}`).focus();
              }, 200);
            }
          }
          if (valid) {
            this.tab2 = true;
            this.tab1 = false;
            this.isbutton = true;
            this.isNextProgress = true;
          }
        });
      }
    },

    onClickNext(formData) {
      this.$refs[formData].validate((valid, index) => {
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
      });
    },

    /**
     * hàm xử lý sự kiện bỏ qua
     */
    async ignore() {
      const obj = {
        ...this.formDataGeneral,
        OrganizationUnit: gettingStarted.findInArray(
          this.formDataGeneral.UnitOrgLevel
        ),
        OrganizationParentID: Cookie.getOrganizationCookie(),
      };
      const res = await this.InsertOrganization(obj);
      if (res.data.Success) {
        this.$router.push("/getting-started");
      }
    },
    /**
     * insert on submit
     * created by nnphong 21/08/2020
     */
    async onSubmit(formData) {
      this.$refs[formData].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById([`${Object.keys(index)[0]}`]).focus();
            }, 200);
          }
        }
        if (valid) {
          const obj = {
            ...this.formDataGeneral,
            OrganizationUnit: gettingStarted.findInArray(
              this.formDataGeneral.UnitOrgLevel
            ),
            OrganizationParentID: Cookie.getOrganizationCookie(),
            TaxAgent: this.formDataDetail,
          };
          const resOrganization = await this.InsertOrganization(obj);
          if (resOrganization.data.Success) {
            setTimeout(() => {
              this.$_Popup.resetState(); // Xóa popup thông báo
              this.$router.push("/getting-started");
            }, 3000);
          } else {
            this.popUpFalse("Thất bại", "Đã xảy ra lỗi vui lòng kiểm tra lại.");
          }
        } else {
          this.popUpFalse("Thất bại", "Đã xảy ra lỗi vui lòng kiểm tra lại.");
        }
      });
    },
    // call api insert
    async InsertOrganization(obj) {
      return await Organization.insertOrganization(obj);
    },

    /**
     * Hàm gọi pupup khi service bị lỗi không thêm được đơn vị
     * created by nvanh 8/9/2020
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
     * lấy thông tin organization đơn vị bên amis qua taxCode
     * created by nnphong 21/8/2020
     */
    async callApiOrganizationUnit(val) {
      var res = await Organization.getOrganizationByTaxCode(val, 0);
      if (res.data.Success && res.data.Data) {
        var data = res.data.Data;
        var objName = [
          "OrganizationTaxNo",
          "OrganizationName",
          "TaxPlaceName",
          "UnitOrgLevel",
        ];
        var resName = [
          "BusinessRegistrationCode",
          "OrganizationUnitName",
          "Address",
          "OrganizationUnitTypeID",
        ];

        for (let i = 0; i < resName.length; i++) {
          this.formDataGeneral[objName[i]] = data[resName[i]];
        }
      }
    },
  },
  watch: {
    tab1: {
      handler(val) {
        this.validate.Email = val ? true : false;
      },
    },
    //nhập OrganizationTaxNo, gọi api lấy data đơn vị con.
    "formDataGeneral.OrganizationTaxNo": {
      handler(val) {
        if (val != "" && val != null) {
          var func = () => {
            this.callApiOrganizationUnit(val);
          };

          setTimeout(func, 500);
        }
      },
    },
    formDataDetail: {
      handler(val) {
        if (
          val.TaxAgentName == "" &&
          val.TaxAgentTaxNo == "" &&
          val.PlaceName == "" &&
          val.ProvinceID == "" &&
          val.DistrictID == "" &&
          val.Phone == "" &&
          val.Email == "" &&
          val.FaxNo == "" &&
          val.ContractNo == "" &&
          val.ContractDate == null &&
          val.TaxAgentEmployee == "" &&
          val.PracticingCertifacateNo == ""
        ) {
          this.isCheck = false;
        } else this.isCheck = true;
      },
      deep: true,
    },
    "formDataGeneral.ProvinceID": {
      handler(val) {
        this.formDataGeneral.DistrictID = "";
        this.getDistricts(val);
      },
    },
    "formDataDetail.ProvinceID": {
      handler(val) {
        this.formDataDetail.DistrictID = "";
        this.getDistricts(val);
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
@media screen and (max-width: 1099px) {
  .text-back {
    display: none !important;
  }
}
@media screen and (max-width: 1366px) {
  .d-flex {
    margin: 0px !important;
  }
}
.list-unit #header {
  position: fixed;
}
.logo {
  display: flex;
  width: 202px;
  height: 60px;
  align-items: center;
  box-shadow: -2px 1px 4px rgba(0, 0, 0, 0.16);
  position: absolute;
  z-index: 100;
  top: 0px;
  height: 60px;
  left: 0px;
  background-color: $white;
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
  background-color: #f5f8fb;
  min-width: 850px;
  .el-form-item.is-required:not(.is-no-asterisk) > .el-form-item__label:before,
  .el-form-item.is-required:not(.is-no-asterisk)
    .el-form-item__label-wrap
    > .el-form-item__label:before {
    right: -13px;
  }
  .btn-expand {
    line-height: 51px;
    button {
      background-color: transparent;
      border: none;
    }
  }
  .el-form-item {
    margin-bottom: 0px !important;
  }
  .el-form-item__label {
    margin: 0 !important;
  }
  position: absolute;
  left: 0px;
  right: 0px;
  bottom: 0px;
  top: 60px;
  .el-form-item__label {
    margin-bottom: 0;
    font-family: "GoogleSansRegular";
    font-weight: normal;
    font-size: 13px;
    color: $dark;
  }
  ::placeholder {
    color: $bg-grey;
    font-size: 11px !important;
  }
  &-container {
    width: 792px;
    height: 100%;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: $white;
    .Gettingstartted-title {
      height: 70px;
      text-align: center;
      .company-title {
        font-size: 20px;
        font-weight: bold;
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
      .progress .step-progress.complete-progress .progress-number {
        border: 2px solid #03dc2e !important;
        background-color: #ffffff !important;
      }
      .progress .step-progress.complete-progress .tick {
        border-bottom: 2px solid #03dc2e !important;
        border-right: 2px solid #03dc2e !important;
      }
    }
    .Gettingstartted-main {
      height: calc(100% - 212px);
      padding: 0 24px 10px;
      overflow-y: auto;
      .contract-tax .el-form-item__label {
        font-size: 13px !important;
        font-weight: bold !important;
      }
      .el-form {
        overflow: initial;
      }
    }
  }
  .gettingstartted-footer {
    height: 70px;
    //background-color: #F4F4F4;
    position: fixed;
    bottom: 0px;
    width: 100%;
    padding-bottom: 24px;
    .button-footer {
      background-color: $white;
      padding-right: 24px;
      border-top: 1px solid #ccc;
      width: 792px;
      height: 70px;
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      .btn-footer {
        padding: 16px 0px;
        .btn-grey {
          margin-left: 24px;
          border: none !important;
        }
        .btn-primary {
          margin-left: 10px;
        }
        .el-button.is-disabled {
          background-color: #e8eef5 !important;
        }
      }
    }
  }
  .input-number .el-input__inner {
    text-align: left;
  }
}
</style>
