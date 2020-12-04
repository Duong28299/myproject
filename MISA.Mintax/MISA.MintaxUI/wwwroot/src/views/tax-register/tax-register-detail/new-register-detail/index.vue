<template>
  <div class="new-register-detail">
    <dialogel
      :titleDialog="getTitle"
      :dialogFormVisible="isNewSubcribersDialog"
      :closeonclickmodal="false"
      @close-dialog="cancel"
      id="new-register-detail-dialog"
      :loading="loadingDialog"
      :isWarnClose="isWarnClose"
      xl="8,9"
      lg="8,9"
      md="9,10"
      sm="4,3"
      xs="6,5"
    >
      <template v-slot:body>
        <el-form
          ref="formData"
          :model="formData"
          :rules="rules"
          @validate="validateForm"
        >
          <el-row :gutter="24" style="padding-top: 12px">
            <!-- phần bên trái -->
            <el-col :lg="12" :md="12" :sm="24">
              <!-- Thông tin cá nhân -->
              <el-row>
                <el-col :span="12" class="text-left contract-tax pb-4">
                  <div style="font-weight: bold">Thông tin cá nhân</div>
                  <div class="tab-active" />
                </el-col>
              </el-row>
              <!--mã số thuế và mã nhân viên text input -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item label="Mã nhân viên" prop="EmployeeCode">
                    <input-field
                      :valueInput="formData.EmployeeCode"
                      type="field-input"
                      id="EmployeeCode"
                      width="100%"
                      placeholder="Nhập mã nhân viên"
                      :maxlength="20"
                      @value="getEmployeeCode"
                      @blur="blurEmployeeCode"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12" v-if="GroupType == 20">
                  <el-form-item
                    label="Mã số thuế"
                    prop="TaxNo"
                    :show-message="validate.TaxNo"
                    :error="errorTaxNoMessage"
                  >
                    <input-field
                      :valueInput="formData.TaxNo"
                      type="field-input"
                      id="TaxNo"
                      number
                      width="100%"
                      placeholder="Nhập mã số thuế"
                      :maxlength="25"
                      @value="formData.TaxNo = $event"
                      @hover="validate.TaxNo = $event"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <!-- họ và tên text input-->
              <el-row :gutter="24">
                <el-col :span="24">
                  <el-form-item
                    label="Họ và tên"
                    prop="FullName"
                    :show-message="validate.FullName"
                  >
                    <input-field
                      id="FullName"
                      type="field-input"
                      width="100%"
                      onlyText
                      :valueInput="formData.FullName"
                      placeholder="Nhập họ và tên"
                      :maxlength="100"
                      :isUpercasefirst="true"
                      @hover="validate.FullName = $event"
                      @value="formData.FullName = $event"
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- Ngày sinh và giới tính -->
              <el-row :gutter="24" style="margin-botton: 27px">
                <el-col :span="12">
                  <el-form-item
                    label="Ngày sinh"
                    prop="DateOfBirth"
                    :show-message="validate.DateOfBirth"
                  >
                    <date-picker
                      id="DateOfBirth"
                      type="date"
                      @hover="validate.DateOfBirth = $event"
                      @getValue="formData.DateOfBirth = $event"
                      :value="formData.DateOfBirth"
                      width="100%"
                      placeholder="_ _ /_ _ /_ _ _ _"
                    ></date-picker>
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="Giới tính"
                    prop="Gender"
                    style="display: inline-block"
                  >
                    <el-radio-group v-model="formData.Gender">
                      <el-radio :label="0">Nam</el-radio>
                      <el-radio :label="1">Nữ</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- Loại giẩy tờ -->
              <el-row>
                <el-col :span="24">
                  <el-form-item label="Loại giấy tờ" prop="IndentityType">
                    <combo-box
                      id="IndentityType"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn loại giấy tờ"
                      :list="listIndentity"
                      :defaultString="formData.IndentityValue"
                      @value="getIndentityType"
                      label="DBOptionName"
                      field="DBOptionValue"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!--Chứng minh nhân dân kê khai sai -->
              <el-row>
                <el-col :span="24" v-if="GroupType == 20">
                  <el-form-item>
                    <el-checkbox
                      v-model="formData.CitizenIdentityNoStatus"
                      style="font-size: 13px; color: #000"
                    >
                      Chứng minh nhân dân kê khai sai
                    </el-checkbox>
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- Số cmnd và quốc tịch -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item
                    :label="IndentityTitle"
                    prop="CitizenIdentityNo"
                    :show-message="validate.CitizenIdentityNo"
                  >
                    <input-field
                      id="CitizenIdentityNo"
                      @value="formData.CitizenIdentityNo = $event"
                      @hover="validate.CitizenIdentityNo = $event"
                      :valueInput="formData.CitizenIdentityNo"
                      type="field-input"
                      number
                      :isUpercasefirst="true"
                      width="100%"
                      placeholder="Nhập số hiệu giấy tờ"
                      :maxlength="50"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item label="Quốc tịch" prop="NationalityID">
                    <combo-box
                      id="NationalityID"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn quốc tịch"
                      :list="listNationality"
                      :defaultNumber="formData.NationalityID"
                      @value="formData.NationalityID = $event"
                      label="NationalityName"
                      field="NationalityID"
                      :getInfo="getPlaceOfIssueByNationalID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- Ngày cấp và nơi cấp -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item
                    label="Ngày cấp"
                    prop="CitizenIdentityDate"
                    :show-message="validate.CitizenIdentityDate"
                  >
                    <date-picker
                      id="CitizenIdentityDate"
                      type="date"
                      @getValue="formData.CitizenIdentityDate = $event"
                      @hover="validate.CitizenIdentityDate = $event"
                      :value="formData.CitizenIdentityDate"
                      width="100%"
                      placeholder="_ _ /_ _ /_ _ _ _"
                    ></date-picker>
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="Nơi cấp"
                    prop="CitizenIdentityPlaceID"
                    :show-message="validate.CitizenIdentityPlaceID"
                  >
                    <combo-box
                      id="CitizenIdentityPlaceID"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn nơi cấp"
                      :defaultNumber="formData.CitizenIdentityPlaceID"
                      :list="listPlaceOfIssues"
                      @value="formData.CitizenIdentityPlaceID = $event"
                      @hover="validate.CitizenIdentityPlaceID = $event"
                      label="PlaceOfIssueName"
                      field="PlaceOfIssueID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- BỘ phận/phòng ban và chức vụ -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item
                    label="Bộ phận/phòng ban"
                    prop="OrganizationUnitName"
                  >
                    <tree-select
                      :value="listOrganizationUnit"
                      :arraySetting="arraySetting"
                      :data="listOrganizationUnit"
                      :returnObject="true"
                      @change="setOrganizationUnit"
                      :multiple="false"
                    ></tree-select>
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item label="Vị trí/chức vụ" prop="PositionName">
                    <combo-box
                      id="PositionName"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn vị trí/chức vụ"
                      :list="listPosition"
                      @value="formData.PositionID = $event"
                      label="PositionName"
                      field="PositionID"
                      :allowCreate="true"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- điện thoại và email -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item label="Điện thoại" prop="Phone">
                    <input-field
                      type="field-input"
                      @value="formData.Phone = $event"
                      :valueInput="formData.Phone"
                      id="Phone"
                      width="100%"
                      placeholder="Nhập số điện thoại"
                      :maxlength="50"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item label="Email" prop="Email">
                    <input-field
                      type="field-input"
                      @value="formData.Email = $event"
                      :valueInput="formData.Email"
                      id="Email"
                      width="100%"
                      placeholder="Nhập email"
                      :maxlength="100"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-col>

            <!-- phần bên phải -->
            <el-col :lg="12" :md="12" :sm="24" class="right-info">
              <!-- header địa chỉ theo hộ khẩu -->
              <el-row>
                <el-col :span="12" class="text-left contract-tax pb-4">
                  <div style="font-weight: bold">Địa chỉ theo hộ khẩu</div>
                  <div class="tab-active" />
                </el-col>
              </el-row>

              <!-- quốc gia thành phố theo hộ khảu -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item
                    label="Quốc gia"
                    prop="NationalIDByHousehold"
                    :show-message="validate.NationalIDByHousehold"
                  >
                    <combo-box
                      id="NationalIDByHousehold"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn quốc gia"
                      :list="listNationality"
                      :defaultNumber="formData.NationalIDByHousehold"
                      @value="getNationalIDByHousehold"
                      @hover="validate.NationalIDByHousehold = $event"
                      label="NationalityName"
                      field="NationalityID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="Tỉnh/Thành phố"
                    prop="ProvinceIDByHousehold"
                    :show-message="validate.ProvinceIDByHousehold"
                  >
                    <combo-box
                      id="ProvinceIDByHousehold"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn tỉnh/thành phố"
                      :defaultNumber="formData.ProvinceIDByHousehold"
                      :list="listProvince"
                      @value="getProvinceIDByHousehold"
                      @hover="validate.ProvinceIDByHousehold = $event"
                      label="ProvinceName"
                      field="ProvinceID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- quận huyện, xã phường theo hộ khẩu -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item
                    label="Quận/Huyện"
                    prop="DistrictIDByHousehold"
                    :show-message="validate.DistrictIDByHousehold"
                  >
                    <combo-box
                      id="DistrictIDByHousehold"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn quận/huyện"
                      :defaultNumber="formData.DistrictIDByHousehold"
                      :list="listDistrictByHousehold"
                      @value="getDistrictIDByHousehold"
                      @hover="validate.DistrictIDByHousehold = $event"
                      label="DistrictName"
                      field="DistrictID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="Xã/Phường"
                    prop="WardIDByHousehold"
                    :show-message="validate.WardIDByHousehold"
                  >
                    <combo-box
                      id="WardIDByHousehold"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn xã/phường"
                      :defaultNumber="formData.WardIDByHousehold"
                      :list="listWardByHousehold"
                      @value="formData.WardIDByHousehold = $event"
                      @hover="validate.WardIDByHousehold = $event"
                      label="WardName"
                      field="WardID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- Số nhà, thôn xóm theo hộ khẩu -->
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="Số nhà/Đường phố, Thôn xóm"
                    prop="AddressByHousehold"
                    :show-message="validate.AddressByHousehold"
                  >
                    <input-field
                      id="AddressByHousehold"
                      @value="formData.AddressByHousehold = $event"
                      :valueInput="formData.AddressByHousehold"
                      @hover="validate.AddressByHousehold = $event"
                      type="field-input"
                      width="100%"
                      placeholder="Nhập địa chỉ chi tiết"
                      :maxlength="255"
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- header địa chỉ theo cư trú -->
              <el-row>
                <el-col :span="12" class="text-left contract-tax pb-4">
                  <div style="font-weight: bold">Địa chỉ cư trú</div>
                  <div class="tab-active" />
                </el-col>
              </el-row>

              <!-- quốc gia thành phố theo cư trú -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item
                    label="Quốc gia"
                    prop="NationalIDByResident"
                    :show-message="validate.NationalIDByResident"
                  >
                    <combo-box
                      id="NationalIDByResident"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn quốc gia"
                      :list="listNationality"
                      :defaultNumber="formData.NationalIDByResident"
                      @value="getNationalIDByResident"
                      @hover="validate.NationalIDByResident = $event"
                      label="NationalityName"
                      field="NationalityID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="Tỉnh/Thành phố"
                    prop="ProvinceIDByResident"
                    :show-message="validate.ProvinceIDByResident"
                  >
                    <combo-box
                      id="ProvinceIDByResident"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn tỉnh/thành phố"
                      :defaultNumber="formData.ProvinceIDByResident"
                      :list="listProvince"
                      @value="getProvinceIDByResident"
                      @hover="validate.ProvinceIDByResident = $event"
                      label="ProvinceName"
                      field="ProvinceID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- quận huyện, xã phường theo hộ khẩu -->
              <el-row :gutter="24">
                <el-col :span="12">
                  <el-form-item
                    label="Quận/Huyện"
                    prop="DistrictIDByResident"
                    :show-message="validate.DistrictIDByResident"
                  >
                    <combo-box
                      id="DistrictIDByResident"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn quận/huyện"
                      :defaultNumber="formData.DistrictIDByResident"
                      :list="listDistrictByResident"
                      @value="getDistrictIDByResident"
                      @hover="validate.DistrictIDByResident = $event"
                      label="DistrictName"
                      field="DistrictID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="Xã/Phường"
                    prop="WardIDByResident"
                    :show-message="validate.WardIDByResident"
                  >
                    <combo-box
                      id="WardIDByResident"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn xã/phường"
                      :defaultNumber="formData.WardIDByResident"
                      :list="listWardByResident"
                      @value="formData.WardIDByResident = $event"
                      @hover="validate.WardIDByResident = $event"
                      label="WardName"
                      field="WardID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- Số nhà, thôn xóm theo hộ khẩu -->
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="Số nhà/Đường phố, Thôn xóm"
                    prop="AddressByResident"
                    :show-message="validate.AddressByResident"
                  >
                    <input-field
                      id="AddressByResident"
                      @value="formData.AddressByResident = $event"
                      @hover="validate.AddressByResident = $event"
                      :valueInput="formData.AddressByResident"
                      type="field-input"
                      width="100%"
                      placeholder="Nhập địa chỉ chi tiết"
                      :maxlength="255"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-col>
          </el-row>
        </el-form>
      </template>

      <!-- Phần các btn hành động -->
      <template v-slot:footer>
        <div>
          <m-btn
            v-if="FormMode == 1"
            type="multi-button"
            :handleClick="clickSaveAndClose"
            :name="'Lưu và đóng'"
            :dataDropdown="actionDropdown"
            class="btn-saveAndClose"
            @option1="clickSaveAndClose"
            @option2="clickSaveAndNew"
          ></m-btn>
          <m-btn
            v-if="FormMode == 0"
            type="btn-primary"
            :handleClick="clickSaveAndClose"
            :name="'Lưu'"
          >
          </m-btn>

          <m-btn
            name="Hủy"
            type="btn-grey"
            class="mr-8 btn-footer"
            :handleClick="cancel"
          ></m-btn>
        </div>
      </template>
    </dialogel>
  </div>
</template>

<script>
import Button from "@/components/buttons";
import dialogel from "@/components/dialog";
import Inputfield from "@/components/inputs";
import ComboBox from "@/components/select";
import DatePicker from "@/components/date-time-pickers/date-time-picker-v2";
import treeSelect from "@/components/trees";
import declarationService from "@/api/etax/declaration.js";
import dictionaryService from "@/api/etax/category-dictionary.js";
import employeeService from "@/api/etax/employee.js";

import { formatDate } from "@/utils/index.js";
import Dialog from "@/mixins/dialog";
import listIndentity from "@/enums/listIndentity.js";
import NationalID from "@/enums/nationalID.js";

export default {
  components: {
    dialogel,
    "input-field": Inputfield,
    "combo-box": ComboBox,
    "m-btn": Button,
    "date-picker": DatePicker,
    "tree-select": treeSelect,
  },

  props: {
    isNewSubcribersDialog: {
      type: Boolean,
      default: false,
    },

    // form Chỉnh sửa là 0, form thêm mới là 1
    FormMode: {
      type: Number,
      default: -1,
    },

    DeclarationObjectDetail: {
      type: Object,
      default: () => {},
    },

    declarationID: {
      type: String,
      default: "",
    },

    // GroupType 20 à chỉnh sửa, 21 là thêm mới
    GroupType: {
      type: Number,
      default: 0,
    },

    listOrganizationUnit: {
      type: Array,
      default: () => {},
    },
  },

  mixins: [Dialog],

  data() {
    return {
      IndentityTitle: "Số CMND",
      isDisableSubmit: true,
      formData: {},
      isWarnClose: false,
      resetValidate: false, //true -
      errorTaxNo: false, // mã số thuế có bị trùng hay không
      isUpdate: false,
      arraySetting: {
        value: "OrganizationUnitID",
        label: "OrganizationUnitName",
        children: "OrganizationUnitsChildren",
      },
      validate: {
        TaxNo: false,
        FullName: false,
        DateOfBirth: false,
        CitizenIdentityNo: false,
        CitizenIdentityDate: false,
        NationalIDByHousehold: false,
        ProvinceIDByHousehold: false,
        WardIDByHousehold: false,
        DistrictIDByHousehold: false,
        AddressByHousehold: false,
        CitizenIdentityPlaceID: false,
        AddressByResident: false,
        WardIDByResident: false,
        ProvinceIDByResident: false,
        DistrictIDByResident: false,
        NationalIDByResident: false,
      },

      resultValidate: {
        TaxNo: this.GroupType == 20 ? false : true,
        FullName: false,
        DateOfBirth: false,
        CitizenIdentityNo: false,
        CitizenIdentityDate: false,
        NationalIDByHousehold: false,
        ProvinceIDByHousehold: false,
        WardIDByHousehold: false,
        DistrictIDByHousehold: false,
        AddressByHousehold: false,
        CitizenIdentityPlaceID: false,
        AddressByResident: false,
        WardIDByResident: false,
        ProvinceIDByResident: false,
        DistrictIDByResident: false,
        NationalIDByResident: false,
      },

      // Thông tin nhân viên được tự động gọi từ service
      employeeAvailable: {},
      // check xem giá trị employee có bị thay đổi k
      isEmployeeCodeChange: false,
      // luật validate dữ liệu
      rules: {
        TaxNo: [
          {
            required: true,
            message: "Mã số thuế không được để trống",
            trigger: ["blur", "change"],
          },
        ],
        Email: [
          {
            type: "email",
            message: "Email không đúng định dạng",
            trigger: ["blur", "change"],
          },
        ],
        FullName: [
          {
            required: true,
            message: "Tên nhân viên không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        DateOfBirth: [
          {
            required: true,
            message: "Ngày sinh không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        Gender: [
          {
            required: true,
            message: "Ngày sinh không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        IndentityType: [
          {
            required: true,
            message: "Loại giấy tờ không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        CitizenIdentityNo: [
          {
            required: true,
            message: "Thông tin không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        CitizenIdentityDate: [
          {
            required: true,
            message: "Ngày cấp không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        CitizenIdentityPlaceID: [
          {
            required: true,
            message: "Nơi cấp không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        NationalIDByHousehold: [
          {
            required: true,
            message: "Quốc gia không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        ProvinceIDByHousehold: [
          {
            required: true,
            message: "Tỉnh/Thành phố không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        DistrictIDByHousehold: [
          {
            required: true,
            message: "Quận/Huyện không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        WardIDByHousehold: [
          {
            required: true,
            message: "Xã/Phường không được bỏ trống",
            trigger: ["change"],
          },
        ],
        AddressByHousehold: [
          {
            required: true,
            message: "Địa chỉ không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        AddressByResident: [
          {
            required: true,
            message: "Địa chỉ không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        WardIDByResident: [
          {
            required: true,
            message: "Xã/Phường không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        DistrictIDByResident: [
          {
            required: true,
            message: "Quận/Huyện không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        ProvinceIDByResident: [
          {
            required: true,
            message: "Tỉnh/Thành phố không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
        NationalIDByResident: [
          {
            required: true,
            message: "Quốc gia không được bỏ trống",
            trigger: ["blur", "change"],
          },
        ],
      },
      // danh sách action ở footer
      actionDropdown: [{ name: "Lưu và thêm mới", nameVal: "option2" }],
      // danh sách loại giấy tờ
      listIndentity: [],
      // danh sách chức vụ
      listPosition: [],
      // danh sách quốc gia
      listNationality: [],
      // danh sách thành phố
      listProvince: [],
      // danh sách quận huyện theo hộ khẩu
      listDistrictByHousehold: [],
      // danh sách xã phường theo hộ khẩu
      listWardByHousehold: [],
      // danh sách quận huyện theo cư trú
      listDistrictByResident: [],
      // danh sách xã phường theo cư trú
      listWardByResident: [],
      // danh sách nơi cấp loại giấy tờ
      listPlaceOfIssues: [],

      isPressSubmit: false, //true khi nút submit được nhấn lần đầu
      listPlaceOfIssuesByNationID: [],
      loadingDialog:false,
    };
  },

  computed: {
    /**
     * Hàm lấy giá trị title của form
     * createdby lttuan 23/09/2020
     */
    getTitle() {
      if (this.FormMode == 0 && this.GroupType == 21)
        return "CHỈNH SỬA NGƯỜI ĐĂNG KÝ MÃ SỐ THUẾ";
      if (this.FormMode == 1 && this.GroupType == 21)
        return "THÊM NGƯỜI ĐĂNG KÝ MÃ SỐ THUẾ";
      if (this.FormMode == 0 && this.GroupType == 20)
        return "CHỈNH SỬA THÔNG TIN NGƯỜI ĐĂNG KÝ THAY ĐỔI MÃ SỐ THUẾ";
      if (this.FormMode == 1 && this.GroupType == 20)
        return "THÊM THÔNG TIN NGƯỜI ĐĂNG KÝ THAY ĐỔI MÃ SỐ THUẾ";

      return "THÊM NGƯỜI ĐĂNG KÝ MÃ SỐ THUẾ";
    },
    errorTaxNoMessage() {
      if (this.errorTaxNo) {
        return "Mã số thuế đã tồn tại";
      } else return null;
    },
  },

  watch: {
    /**
     * theo dõi biến validate CitizenIdentify
     * create by :ntdong 10/10/2020
     */
    "validate.CitizenIdentityNo": {
      handler(val) {
        if (!val) {
          var element = document.getElementById("CitizenIdentityNo");
          element.classList.remove("is-error-Citizent");
        }
      },
    },
    /**
     * theo dõi biến validate TaxNo
     * create by :ntdong 10/10/2020
     */
    "validate.TaxNo": {
      handler(val) {
        if (!val) {
          var element = document.getElementById("TaxNo");
          element.classList.remove("is-error-Citizent");
          this.errorTaxNo = false;
        }
      },
    },

    /**
     * theo dõi biến dialog, nếu thay đổi thì reset form
     * createdby lttuan 23/09/2020
     */
    isNewSubcribersDialog(val) {
      if (!val) {
        this.resetForm();
      } else {
        this.isPressSubmit = false;
        this.employeeAvailable = {};
        this.mapData();
      }
    },

    /**
     * Sử dụng để reset lại validate khi chọn chế đô lưu và thêm mới
     */
    // resultValidate:{
    //   handler(){
    //     if(this.resetValidate)
    //       this.$refs["formData"].clearValidate();
    //   },
    //   deep:true,
    // }
  },
  created() {},

  mounted() {
    // lấy các danh sách
    this.getListIndentity();
    //this.mapData();
    this.getListNational();
    //this.getListProvincials();
    this.getListPosition();
    //this.getPlaceOfIssueID();
  },

  methods: {
    /**
     * Hàm thực hiện khi click vào nút lưu và đóng
     *
     * createdby lttuan 021020
     */
    async clickSaveAndClose() {
      await this.saveAndClose(true);
    },

    /**
     * Hàm thực hiện khi click vào nút lưu và thêm mới
     * createdby lttuan 021020
     */
    async clickSaveAndNew() {
      await this.saveAndClose(false);
    },

    /**
     * Hàm thực hiện check giá trị employecode khi blur
     * @param val giá trị input
     * @author lttuan
     * @date 250920
     */
    async blurEmployeeCode(val) {
      if (val != this.employeeAvailable.EmployeeCode && this.FormMode == 1) {
        this.employeeAvailable = await this.getEmployeebyCode(val);
        if (!Object.keys(this.employeeAvailable).length == 0)
          this.handlerAvailableEmployeeCode();
      }
    },

    /**
     * Hàm thực hiện mỗi khi có một trường input validate thay đổi
     * @param prop prop của ô input
     * @param valid true-valid, false invalid
     * createdby ndhuy 23.09.2020
     */
    async validateForm(prop, valid) {
      this.resultValidate[prop] = valid;
      for (const property in this.resultValidate) {
        if (this.resultValidate[property] == false) {
          break;
        }
      }

      if (this.resetValidate && prop == "AddressByHousehold") {
        await this.$refs["formData"].clearValidate();
        this.resetValidate = false;
      }
    },

    /**
     * Thêm mới nhân viên vào tờ khai
     * @param confirm biến xác nhân chỉnh sửa
     * @param data dữ liệu lao động gửi đi
     * createdby ndhuy 30.09.2020
     */
    async insertEmployee(confirm, data) {
      var newobj = { ...data };
      newobj.confirm = confirm;
      newobj.DeclarationID = this.declarationID;
      if (!this.checkPositionAvailable(newobj.PositionID)) {
        newobj.PositionName = newobj.PositionID;
        newobj.PositionID = "";
      }
      const editResponse = await declarationService.postDeclarationDetail(
        newobj
      );
      return editResponse;
    },

    /**
     * Cập nhật nhân viên vào tờ khai
     * createdby ndhuy 28.09.2020
     */
    async updateEmployee(confirm, data) {
      var newobj = { ...data };
      newobj.confirm = confirm;
      newobj.DeclarationID = this.declarationID;
      if (!this.checkPositionAvailable(newobj.PositionID)) {
        newobj.PositionName = newobj.PositionID;
        newobj.PositionID = "";
      }
      
      const editResponse = await declarationService.putDeclarationDetail(
        newobj
      );
      return editResponse;
    },

    /**
     * Hàm thực hiện xác nhận cập nhật lại lao động
     * createdby ndhuy 28.09.2020
     */
    async acceptUpdate(data, isClose) {
      var res = await this.insertEmployee(true, data);
      if (res.data.MISACode == 200 && res.data.Success == true) {
        this.popUpSuccess("Thêm người đăng ký mã số thuế thành công");
        if (isClose) this.closeForm();
        else this.resetForm();
        this.$emit("reset-table");
      }
    },

    /**
     * hàm thực hiện btn SaveAndClose
     * createdby lttuan 23/09/2020
     */
    async saveAndClose(isClose) {
      // focus vào input validate sai
      this.$refs["formData"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById(`${Object.keys(index)[0]}`).focus();
            }, 300);
          }
        }
        if (!valid) {
          this.isPressSubmit = true;
        }
        //Xử lý gửi
        else {
          this.loadingDialog=true;
          this.resetValidate = isClose ? false : true;
          //Trường hợp thêm mới
          var res = null;
          var message = "";
          if (this.FormMode == 1 && !this.isUpdate) {
            res = await this.insertEmployee(false, this.formData);

            if (res.data.MISACode == 200 && res.data.Success == true) {
              if (isClose) this.closeForm();
              else this.mapData();
              if (this.GroupType == 21) {
                message = "Thêm người đăng ký mã số thuế thành công";
              } else if (this.GroupType == 20)
                message = "Thêm người chỉnh sửa đăng ký mã số thuế thành công";
              this.popUpSuccess(message);

              this.$emit("reset-table");

              //reason=1: Đã tồn tại trong hồ sơ
            } else if (res.data.Success == false && res.data.MISACode == 300) {
              var existNumber = ""; // Số bị trùng
              var existType = "";
              var existEmployeeName = "";
              var existEmployeeCode = "";
              var existEmployeeID = "";
              var existDeclarationDetailID = "";
              for (var exist of res.data.Data) {
                if (
                  exist.CitizenIdentityNo == this.formData.CitizenIdentityNo
                ) {
                  existNumber = exist.CitizenIdentityNo;
                  existType = this.IndentityTitle;
                  existEmployeeName = exist.FullName;
                  existEmployeeCode = exist.EmployeeCode
                    ? exist.EmployeeCode
                    : "";
                  existEmployeeID = exist.EmployeeID;
                  existDeclarationDetailID = exist.DeclarationDetailID;
                  break;
                }
              }
              // Kiểm tra xem mã số thuế có bị trùng hay không
              for (var existTaxNo of res.data.Data) {
                if (
                  existTaxNo.TaxNo == this.formData.TaxNo &&
                  existTaxNo.TaxNo != ""
                ) {
                  if (existNumber == "") {
                    existDeclarationDetailID = exist.DeclarationDetailID;
                    existNumber = exist.TaxNo;
                    existType = "Mã số thuế";
                    existEmployeeName = existTaxNo.FullName;
                    existEmployeeCode = exist.EmployeeCode
                      ? exist.EmployeeCode
                      : "";
                  } else if (existNumber !== existTaxNo.CitizenIdentityNo) {
                    // Thực hiện validate trường TaxNo
                    this.addValidateForTaxNo();
                    this.errorTaxNo = true;
                  }
                }
              }

              this.buttons =
                this.formData.CitizenIdentityNo == existNumber
                  ? [
                      {
                        text: "Không",
                        class: "btn-grey none-border",
                        callback: () => {},
                      },
                      {
                        text: "Có",
                        class: "btn-primary",
                        callback: !this.errorTaxNo
                          ? () => {
                              this.formData.EmployeeID = existEmployeeID;
                              this.formData.DeclarationDetailID = existDeclarationDetailID;
                              this.acceptUpdate(this.formData, isClose);
                            }
                          : () => {
                              this.isUpdate = true;
                              this.formData.EmployeeID = existEmployeeID;
                              this.formData.DeclarationDetailID = existDeclarationDetailID;
                            },
                      },
                    ]
                  : [
                      {
                        text: "Đóng",
                        class: "btn-primary",
                        callback: () => {
                          this.errorTaxNo = true;
                        },
                      },
                    ];
              var stringUpdate =
                existNumber == this.formData.CitizenIdentityNo
                  ? ". Bạn có muốn cập nhật lại thông tin cho " +
                    "<b>" +
                    existEmployeeName +
                    "-" +
                    existEmployeeCode +
                    "</b>"
                  : ". Vui lòng kiểm tra lại";
              // gọi dialog
              this.$_Popup.warn(
                `Cảnh báo`,
                existType +
                  " <b>" +
                  existNumber +
                  "</b>" +
                  " đã đăng kí cho người nộp thuế " +
                  "<b>" +
                  existEmployeeName +
                  "-" +
                  existEmployeeCode +
                  "</b>" +
                  stringUpdate,
                this.buttons
              );

              //reason=2: Đã tồn tại trong hệ thống
            } else if (res.data.Success == false && res.data.MISACode == 310) {
              //grouptype=21: Loại hồ sơ đăng ký mst
              if (
                this.GroupType == 21 &&
                res.data.Data &&
                res.data.Data.TaxNo
              ) {
                this.popUpFalse(
                  "Thất bại",
                  "Nhân viên được chọn hiển thị đã có mã số thuế. Vui lòng kiểm tra lại"
                );
              } else if (
                (this.GroupType == 21 && !res.data.Data.TaxNo) ||
                this.GroupType == 20
              ) {
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
                      this.formData.EmployeeID = res.data.Data.EmployeeID;
                      this.acceptUpdate(this.formData, isClose);
                    },
                  },
                ];
                this.$_Popup.warn(
                  "Cảnh báo",
                  "Bạn có chắc chắn muốn cập nhật thông tin của người lao động <b>" +
                    res.data.Data.FullName +
                    "-" +
                    (res.data.Data.EmployeeCode
                      ? res.data.Data.EmployeeCode
                      : "") +
                    "</b>",
                  this.buttons
                );
              }
            }

            //Trường hợp chỉnh sửa
          } else if (this.FormMode == 0 || this.isUpdate == true) {
            // gửi thông tin update
            res = await this.updateEmployee(true, this.formData);
            // thông báo loại thông tin cập nhập theo Group type
            if (res.data.MISACode == 200 && res.data.Success == true) {
              if (this.GroupType == 21) {
                message = "Cập nhật người đăng ký mã số thuế thành công";
              } else if (this.GroupType == 20)
                message =
                  "Cập nhật người chỉnh sửa đăng ký mã số thuế thành công";
              // hiển thị thông báo ra popup
              this.popUpSuccess(message);
              if (isClose) this.closeForm();
              // sau khi thêm nếu người dùng không đóng form thì thực hiện reset
              else this.resetForm();
              this.$emit("reset-table");
            } // trường hợp thông tin cập đưa lên bị trùng
            else if (res.data.MISACode == 300 && res.data.Success == false) {
              var listExist = res.data.Data;
              var mess = "";
              // kiểm tra trùng CitizenIdentify
              listExist.forEach((element) => {
                var existIn = element.Reason == 1 ? " tờ khai" : " hệ thống";
                if (
                  element.CitizenIdentityNo &&
                  element.CitizenIdentityNo == this.formData.CitizenIdentityNo
                ) {
                  // [Note]
                  mess =
                    this.IndentityTitle +
                    " " +
                    element.CitizenIdentityNo +
                    " đã được đăng ký cho người nộp thuế " +
                    element.FullName +
                    " ở trong" +
                    existIn;
                  this.buttons = [
                    {
                      text: "Đóng",
                      class: "btn-primary",
                      callback: () => {
                        this.addValidateForCitizenIdentityNo();
                      },
                    },
                  ];
                  // gọi dialog
                  this.$_Popup.warn(`Cảnh báo`, mess, this.buttons);
                }
              });
              // kiểm tra trùng Tax No
              listExist.forEach((element) => {
                var existIn = element.Reason == 1 ? " tờ khai" : " hệ thống";
                if (element.TaxNo && element.TaxNo == this.formData.TaxNo) {
                  if (mess == "")
                    mess =
                      "Mã số thuế  " +
                      element.CitizenIdentityNo +
                      " đã được đăng ký cho người nộp thuế " +
                      element.FullName +
                      " ở trong" +
                      existIn;
                  this.buttons = [
                    {
                      text: "Có",
                      class: "btn-primary",
                      callback: () => {
                        this.addValidateForTaxNo();
                      },
                    },
                  ];
                  // gọi dialog
                  this.$_Popup.warn(`Cảnh báo`, mess, this.buttons);
                }
              });
            } else {
              this.$_Notification.error(
                "Thất bại",
                " Bạn đã chỉnh sửa người đăng ký mã số thuế thất bại.Vui lòng kiểm tra lại."
              );
            }
          }
          this.loadingDialog=false;
        }
      });
    },
    /**
     * map dữ liệu trả về thông tin của organization
     * createdby lttuan 31.08.2020
     */
    mapData(
      data = this.DeclarationObjectDetail,
      isMapValiableEmployee = false
    ) {
      // 0 là chỉnh sửa
      if (this.FormMode == 0 || isMapValiableEmployee == true) {
        this.formData = { ...data };
        this.formData.IndentityValue = this.findInArray(
          this.formData.IndentityType,
          listIndentity.LIST_INDENTITY,
          "DBOptionID",
          "DBOptionValue"
        );
        if (this.formData.CitizenIdentityDate) {
          this.formData.CitizenIdentityDate = formatDate(
            data.CitizenIdentityDate,
            "mm-dd-yyyy"
          );
        }
        if (this.formData.DateOfBirth) {
          this.formData.DateOfBirth = formatDate(
            data.DateOfBirth,
            "mm-dd-yyyy"
          );
        }
        this.getPlaceOfIssueID();
      }
      // 1 là thêm mới
      else if (this.FormMode == 1) {
        this.resetForm();
        this.getPlaceOfIssueID();
      }
      this.clearValidate();
    },

    /**
     * Hàm tìm kiếm trong mảng
     * createdby lttuan 22092020
     */
    findInArray(value, array, propertyInput, propertyOutput) {
      for (let i in array) {
        if (array[i][propertyInput] == value) return array[i][propertyOutput];
      }
      return null;
    },

    /**
     * Kiểm tra các ô nhập liệu đã bị thay đổi hay chưa
     * thay đổi biến hiện cảnh báo khi form thay đổi
     * createdby lttuan 10.09.2020
     */
    checkEdited() {
      this.isWarnClose = false;
      var val = this.formData;
      for (const prop in val) {
        if (!val[prop]) {
          if (this.taxAgent[prop]) {
            this.isWarnClose = true;
          }
        } else if (val[prop] != this.taxAgent[prop]) {
          this.isWarnClose = true;
        }
      }
    },

    /**
     * Hàm thực thi khi form bị đóng
     * createdby lttuan 10.09.2020
     */
    beforeClose(done) {
      this.checkEdited();
      if (this.isWarnClose == true) {
        this.buttons = [
          {
            text: "Không",
            class: "btn-grey none-border",
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
     * Sự kiện khi click vào nút đóng form
     * Created by: LTTUAN 31/8/2020
     */
    closeForm() {
      //this.$refs["formData"].resetFields();
      this.resetForm();
      this.$emit("close-dialog", false);
      // this.clearValidate();
    },

    getErorrTaxMessage() {
      if (this.errorTaxNo) {
        return "Mã số thuế đã tồn tại";
      } else return null;
    },
    /**
     * Hàm remove validate
     * Created by: LTTUAN 2/10/2020
     */
    clearValidate() {
      var element = document.querySelectorAll(".is-error");
      for (let index = 0; index < element.length; index++) {
        element[index].classList.remove("is-error");
      }
    },
    /**
     * Thêm red border vào input CitizenIdentityNo
     * ntdong 10/10/2020
     */
    addValidateForCitizenIdentityNo() {
      var element = document.getElementById("CitizenIdentityNo");
      element.classList.add("is-error-Citizent");
      this.validate.CitizenIdentityNo = true;
    },
    /**
     * Thêm red border vào input TaxNo
     * ntdong 10/10/2020
     */
    addValidateForTaxNo() {
      var element = document.getElementById("TaxNo");
      element.classList.add("is-error-Citizent");
      this.validate.TaxNo = true;
    },

    /**
     * Sự kiện khi click nút quay lại
     * createdby lttuan 07.09.2020
     */
    cancel() {
      if (this.isWarnClose == true) {
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
              this.resetForm();
              this.$emit("close-dialog", false);
            },
          },
        ];

        this.$_Popup.warn(
          "Cảnh báo",
          "Thông tin vừa nhập liệu sẽ không được lưu lại. Bạn có chắc chắn muốn thoát không?",
          this.buttons
        );
      }
      if (this.isWarnClose == false) {
        this.$refs["formData"].resetFields();
        this.$emit("close-dialog", false);
      }
    },

    /**
     * Hàm hiển thị popup thất bại
     * createdby lttuan 03.09.2020
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
     * Hàm  hiển thị popup thành công
     * createdby lttuan 03.09.2020
     */
    popUpSuccess(message) {
      // this.getClose();
      this.$_Notification.success("Thành công", message);
    },

    /**
     * get giá trị mã nhân viên
     * @param {val} any
     * createdby lttuan 22092020
     */
    async getEmployeeCode(val) {
      this.isEmployeeCodeChange = true;

      if (val != this.formData.EmployeeCode) {
        this.formData.EmployeeCode = val;
        this.isEmployeeCodeChange = true;
      }
    },

    /**
     * Lấy thông tin nhân vine theo mã code
     */
    async getEmployeebyCode(code) {
      var filterEmployee = {
        employeeCode: code,
        employeeID: "",
        taxNo: "",
        citizenIdentityNo: "",
      };
      var res = await employeeService.getEmployee(filterEmployee);
      if (res.data.Success && res.data.Data != null) return res.data.Data;
      else return {};
    },

    /**
     * Hàm xử lý việc duplicate employcode
     * createdby lttuan 24092020
     */
    handlerAvailableEmployeeCode() {
      // khai báo button
      this.mapData(this.employeeAvailable, true);
    },

    /**
     * get list danh sách nơi cấp và set lại tên loại label loại giấy tờ
     * @param {val} any
     * createdby lttuan 22092020
     */
    getIndentityType(val) {
      if (this.formData.IndentityValue != val) {
        if (val == listIndentity.LIST_INDENTITY[0].DBOptionValue)
          this.IndentityTitle = "Số CMND";
        if (val == listIndentity.LIST_INDENTITY[2].DBOptionValue)
          this.IndentityTitle = "Số CCCD";
        if (val == listIndentity.LIST_INDENTITY[1].DBOptionValue)
          this.IndentityTitle = "Số hộ chiếu";

        this.formData.IndentityValue = val;
        this.formData.CitizenIdentityNo = null;
        this.formData.CitizenIdentityPlaceID = null;
        this.getPlaceOfIssueID();

        this.formData.IndentityType = this.findInArray(
          this.formData.IndentityValue,
          listIndentity.LIST_INDENTITY,
          "DBOptionValue",
          "DBOptionID"
        );
      }
    },

    setOrganizationUnit(val) {
      this.formData.OrganizationUnitID = val.OrganizationUnitID;
      this.formData.OrganizationUnitName = val.OrganizationUnitName;
    },

    /**
     * get list quốc gia, quốc tịch, nơi cấp
     * createdby lttuan 22092020
     */
    async getListNational() {
      var res = await dictionaryService.getNationalites();
      if (typeof res.data != "undefined" && res.data.Success)
        this.listNationality = res.data.Data;
      else this.listNationality = [];
    },

    /**
     * Hàm gọi service lấy dữ liệu tỉnh thành phố
     * createdby lttuan 22092020
     */
    async getListProvincials() {
      var res = await dictionaryService.getProvincials();
      if (typeof res.data != "undefined" && res.data.Success)
        this.listProvince = res.data.Data;
      else this.listProvince = [];
    },

    /**
     * get danh sách quận huyện theo hộ khẩu
     * createdby lttuan 22092020
     */

    async getListDistrictByHousehold() {
      this.loadingDialog=true;
      if (this.formData.ProvinceIDByHousehold) {
        var res = await dictionaryService.getDistrictsByProvinceID(
          this.formData.ProvinceIDByHousehold
        );
        if (typeof res.data != "undefined" && res.data.Success)
          this.listDistrictByHousehold = res.data.Data;
      }
      this.loadingDialog=false;
    },

    /**
     * get danh sách quận huyện theo cư trú
     * createdby lttuan 22092020
     */
    async getListDistrictByResident() {
      this.loadingDialog=true;
      if (this.formData.ProvinceIDByResident) {
        var res = await dictionaryService.getDistrictsByProvinceID(
          this.formData.ProvinceIDByResident
        );
        if (typeof res.data != "undefined" && res.data.Success)
          this.listDistrictByResident = res.data.Data;
      }
      this.loadingDialog=false;
    },

    /**
     * get danh sách quận huyện theo hộ khẩu
     * createdby lttuan 22092020
     */
    async getListWardByHousehold() {
      this.loadingDialog=true;
      if (this.formData.DistrictIDByHousehold) {
        var res = await dictionaryService.getWardByDistrictsID(
          this.formData.DistrictIDByHousehold
        );
        if (typeof res.data != "undefined" && res.data.Success)
          this.listWardByHousehold = res.data.Data;
      }
      this.loadingDialog=false;
    },

    /**
     * get danh sách quận huyện theo cư trú
     * createdby lttuan 22092020
     */
    async getListWardByResident() {
      this.loadingDialog=true;
      if (this.formData.DistrictIDByResident) {
        var res = await dictionaryService.getWardByDistrictsID(
          this.formData.DistrictIDByResident
        );
        if (typeof res.data != "undefined" && res.data.Success)
          this.listWardByResident = res.data.Data;
      }
      this.loadingDialog=false;
    },

    /**
     * get list loại giấy tờ
     * createdby lttuan 22092020
     */
    async getListIndentity() {
      var res = await dictionaryService.getDBOPtionByType("IndentityType");
      if (typeof res.data != "undefined" && res.data.Success)
        this.listIndentity = res.data.Data;
    },

    /**
     * get list nơi cấp
     * createdby lttuan 22092020
     */
    async getPlaceOfIssueID() {
      var res = await dictionaryService.getPlaceOfIssuesByIssueTypeCode(
        this.formData.IndentityValue
      );

      if (typeof res.data != "undefined" && res.data.Success) {
        this.listPlaceOfIssues = res.data.Data;
      }
    },

    /**
     * get list nơi cấp theo nationalID
     * created by nvnguyen 07/11/2020
     */
    async getPlaceOfIssueByNationalID(event) {
      if (event != NationalID.NATIONALID[0].value) {
        var res = await dictionaryService.getPlaceOfIssuesByNationalID(event);
        if (typeof res.data != "undefined" && res.data.Success) {
          this.formData.CitizenIdentityPlaceID = null;
          this.listPlaceOfIssuesByNationID = res.data.Data;
          this.listPlaceOfIssues = this.listPlaceOfIssuesByNationID.Data;
        }
      } else {
        this.getPlaceOfIssueID();
      }
    },

    /**
     * get list vị trí, chức vụ
     * createdby ntdong 24/09/2020
     */ async getListPosition() {
      var res = await dictionaryService.getPosition();
      this.listPosition = res.data.Data;
    },

    /**
     * Hàm nhận giá trị từ input quốc gia theo hộ khẩu và xử lý
     * createdby lttuan 03.09.2020
     */
    getNationalIDByHousehold(val) {
      if (this.formData.NationalIDByHousehold != val) {
        if (val != 268) {
          this.formData.ProvinceIDByHousehold = 64;
          this.formData.DistrictIDByHousehold = 715;
          this.formData.WardIDByHousehold = 11558;

          this.listProvince = [{ ProvinceID: 64, ProvinceName: "Khác" }];
          this.listDistrictByHousehold = [
            { DistrictID: 715, DistrictName: "Khác" },
          ];
          this.listWardByHousehold = [{ WardID: 11558, WardName: "Khác" }];
        }
        if (val == 268) {
          this.formData.ProvinceIDByHousehold = null;
          this.formData.DistrictIDByHousehold = null;
          this.formData.WardIDByHousehold = null;
          this.listDistrictByHousehold = [];
          this.listWardByHousehold = [];
          this.getListProvincials();
        }
      }
      this.formData.NationalIDByHousehold = val;
    },

    /**
     * Hàm nhận giá trị từ input quốc gia theo cư trú và xử lý
     * createdby lttuan 03.09.2020
     */
    getNationalIDByResident(val) {
      if (this.formData.NationalIDByResident != val) {
        if (val != 268) {
          this.formData.ProvinceIDByResident = 64;
          this.formData.DistrictIDByResident = 715;
          this.formData.WardIDByResident = 11558;

          this.listProvince = [{ ProvinceID: 64, ProvinceName: "Khác" }];
          this.listDistrictByResident = [
            { DistrictID: 715, DistrictName: "Khác" },
          ];
          this.listWardByResident = [{ WardID: 11558, WardName: "Khác" }];
        }
        this.formData.NationalIDByResident = val;
      }
      if (val == 268) {
        this.formData.ProvinceIDByResident = null;
        this.formData.DistrictIDByResident = null;
        this.formData.WardIDByResident = null;
        this.listDistrictByResident = [];
        this.listWardByResident = [];
        this.getListProvincials();
      }
    },

    /**
     * Hàm nhận giá trị từ input thành phó theo hộ khẩu và xử lý
     * createdby lttuan 03.09.2020
     */
    getProvinceIDByHousehold(val) {
      if (this.formData.ProvinceIDByHousehold != val) {
        this.formData.DistrictIDByHousehold = null;
        this.formData.WardIDByHousehold = null;
        this.listDistrictByHousehold = [];
        this.listWardByHousehold = [];
      }
      this.formData.ProvinceIDByHousehold = val;
      this.getListDistrictByHousehold();
    },

    /**
     * Hàm nhận giá trị từ input thành phố theo cư trú và xử lý
     * createdby lttuan 03.09.2020
     */
    getProvinceIDByResident(val) {
      if (this.formData.ProvinceIDByResident != val) {
        this.formData.DistrictIDByResident = null;
        this.formData.WardIDByResident = null;
        this.listDistrictByResident = [];
        this.listWardByResident = [];
      }
      this.formData.ProvinceIDByResident = val;
      this.getListDistrictByResident();
    },

    /**
     * Hàm nhận giá trị từ input quận huyện theo hộ khẩu và xử lý
     * createdby lttuan 03.09.2020
     */
    getDistrictIDByHousehold(val) {
      if (this.formData.DistrictIDByHousehold != val) {
        this.formData.WardIDByHousehold = null;
        this.listWardByHousehold = [];
      }
      this.formData.DistrictIDByHousehold = val;
      this.getListWardByHousehold();
    },

    /**
     * Hàm nhận giá trị từ input quạn huyện theo cư trú và xử lý
     * createdby lttuan 03.09.2020
     */
    getDistrictIDByResident(val) {
      if (this.formData.DistrictIDByResident != val) {
        this.formData.WardIDByResident = null;
        this.listWardByResident = [];
      }
      this.formData.DistrictIDByResident = val;
      this.getListWardByResident();
    },

    resetForm() {
      var defaultIndentityType = this.findInArray(
        listIndentity.LIST_INDENTITY[0].DBOptionID,
        listIndentity.LIST_INDENTITY,
        "DBOptionID",
        "DBOptionValue"
      );
      this.isUpdate = false;
      this.formData = {
        EmployeeCode: "",
        FullName: "",
        DateOfBirth: null,
        IndentityType: listIndentity.LIST_INDENTITY[0].DBOptionID,
        CitizenIdentityNo: "",
        CitizenIdentityDate: null,
        OrganizationUnitName: "",
        NationalityID: 268,
        CitizenIdentityPlaceID: null,
        PositionName: "",
        PositionID: "",
        Phone: "",
        Gender: 0,
        Email: "",
        NationalIDByHousehold: 268,
        ProvinceIDByHousehold: null,
        DistrictIDByHousehold: null,
        WardIDByHousehold: null,
        AddressByHousehold: "",
        NationalIDByResident: 268,
        ProvinceIDByResident: null,
        DistrictIDByResident: null,
        WardIDByResident: null,
        AddressByResident: "",
        CitizenIdentityNoStatus: null,
        TaxNo: null,
        declarationID: this.declarationID,
        OrganizationUnitID: "",
      };
      this.formData.IndentityValue = defaultIndentityType;

      this.duplicateEmployee = null;

      this.IndentityTitle = "Số CMND";

      //this.setButtonAdd(false);
      this.validate.CitizenIdentityNo = false;
      this.validate.TaxNo = false;
    },

    /**
     * Kiểm tra vị trí chức vụ đã tồn tại hay chưa
     * CreateBy:ntdong 24/09/2020
     */
    checkPositionAvailable(val) {
      for (var i = 0; i < this.listPosition.length; i++) {
        if (val == this.listPosition[i].PositionID) {
          return true;
        }
      }
      return false;
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";

.new-register-detail {
  .el-form-item__label {
    margin-bottom: 0;
    font-family: $_fontregular;
    font-weight: normal !important;
    font-size: 13px !important;
    color: $dark-grey;
  }

  .el-dialog__body {
    max-height: 80vh;
  }

  .el-dialog__footer {
    .btn-primary-icon {
      border: 0px;

      .el-button-group {
        background-color: $bg-color-1;
        button.is-disabled {
          &:hover {
            background-color: $white !important;
            color: #c0c4cc !important;
          }
        }
      }
    }
  }

  .el-form-item {
    margin-bottom: 5px;
  }

  .checkbox-label {
    padding-left: 10px;
  }

  .el-form-item--medium .el-form-item__label {
    line-height: 17px;
    margin-bottom: 4px;
  }

  .el-select .el-input--suffix .el-input__inner {
    padding-right: 40px !important;
  }

  .date-time {
    top: 30px;
  }

  .el-form-item__error {
    transform: translate(-100px, -24px);
  }
  .is-error-Citizent {
    border: 1px solid $red;
  }

  .tab-active {
    margin-bottom: 12px;
    width: 60px;
    bottom: 0px;
    height: 3px;
    background-color: $seconds;
    border-top-left-radius: 50px;
    border-top-right-radius: 50px;
    position: unset;
  }

  .el-form-item {
    margin-bottom: 16px;
  }
  .date-time {
    top: 20px;
  }
}
</style>
