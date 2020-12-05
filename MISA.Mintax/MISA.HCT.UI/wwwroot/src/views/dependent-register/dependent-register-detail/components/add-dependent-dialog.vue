<template>
  <div>
    <!-- Dialog thêm người phụ thuộc -->
    <add-dialog
      :titleDialog="title"
      :id="dialogid"
      :dialogFormVisible="visiableDL"
      :closeonclickmodal="false"
      @close-dialog="getClose()"
      xl="9,10"
      lg="9,10"
      md="9,10"
      sm="4,3"
      xs="7,8"
    >
      <template v-slot:body>
        <el-form
          ref="FormDependentPerson"
          :model="FormDependentPerson"
          label-width="120px"
          label-position="top"
          :rules="triggerVali ? rules_2 : rules_1"
          @validate="validateForm"
        >
          <el-row :gutter="48">
            <el-col :lg="12" :md="12" :sm="24">
              <el-form-item
                label="Họ và tên người phụ thuộc"
                prop="DependentName"
                :rules="[
                  {
                    required: true,
                    message: 'Họ và tên người phụ thuộc không được bỏ trống.',
                    trigger: ['blur', 'change'],
                  },
                ]"
                :show-message="validate.DependentName"
              >
                <input-field
                  id="DependentName"
                  type="field-input"
                  placeholder="Nhập họ và tên"
                  @hover="validate.DependentName = $event"
                  :valueInput="FormDependentPerson.DependentName"
                  @value="FormDependentPerson.DependentName = $event"
                  :isUpercasefirst="true"
                />
              </el-form-item>
              <el-row :gutter="16">
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item
                    label="Ngày sinh"
                    prop="DateOfBirth"
                    :rules="[
                      {
                        required: true,
                        message:
                          'Ngày sinh người phụ thuộc không được bỏ trống.',
                        trigger: ['blur'],
                      },
                    ]"
                    :show-message="validate.DateOfBirth"
                  >
                    <datetime2
                      id="DateOfBirth"
                      :width="'100%'"
                      type="date"
                      placeholder="__/__/____"
                      @hover="validate.DateOfBirth = $event"
                      :value="FormDependentPerson.DateOfBirth"
                      @getValue="FormDependentPerson.DateOfBirth = $event"
                    ></datetime2>
                  </el-form-item>
                </el-col>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item
                    label="Quan hệ với NNT"
                    prop="RelationID"
                    :rules="[
                      {
                        required: true,
                        message: 'Quan hệ với người nộp không được bỏ trống.',
                        trigger: ['blur', 'change'],
                      },
                    ]"
                    :show-message="validate.RelationID"
                  >
                    <m-sel
                      id="RelationID"
                      type="label-top"
                      label="label"
                      field="value"
                      :defaultNumber="FormDependentPerson.RelationID"
                      :list="Relation"
                      @hover="validate.RelationID = $event"
                      @value="FormDependentPerson.RelationID = $event"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row :gutter="16">
                <div class="label-title">
                  <span>
                    Loại khai báo
                    <span class="color-red">*</span>
                  </span>
                </div>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item>
                    <el-radio
                      class="radio-align"
                      :label="0"
                      v-model="FormDependentPerson.DependentType"
                      >Báo tăng</el-radio
                    >
                  </el-form-item>
                </el-col>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item>
                    <el-radio
                      class="radio-align"
                      :label="1"
                      v-model="FormDependentPerson.DependentType"
                      >Báo giảm</el-radio
                    >
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row :gutter="16">
                <el-form-item>
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item
                      label="Ngày bắt đầu"
                      prop="StartDependentDate"
                      :show-message="validate.StartDependentDate"
                    >
                      <datetime2
                        id="StartDependentDate"
                        type="month"
                        defaultValue
                        :value="FormDependentPerson.StartDependentDate"
                        :width="'100%'"
                        placeholder="__/____"
                        @getValue="
                          FormDependentPerson.StartDependentDate = $event
                        "
                        @hover="validate.StartDependentDate = $event"
                      ></datetime2>
                    </el-form-item>
                  </el-col>
                  <!-- <el-col class="line" :span="2">đến</el-col> -->
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item
                      label="Ngày kết thúc"
                      prop="FinishDependentDate"
                      :show-message="validate.FinishDependentDate"
                    >
                      <datetime2
                        id="FinishDependentDate"
                        @hover="validate.FinishDependentDate = $event"
                        type="month"
                        defaultValue
                        placeholder="__/____"
                        :value="FormDependentPerson.FinishDependentDate"
                        :width="'100%'"
                        @getValue="
                          FormDependentPerson.FinishDependentDate = $event
                        "
                      ></datetime2>
                    </el-form-item>
                  </el-col>
                </el-form-item>
              </el-row>
            </el-col>
            <el-col :lg="12" :md="12" :sm="24">
              <el-row :gutter="16">
                <div class="label-title">
                  <span>
                    Có mã số thuế cá nhân hay không ?
                    <span class="color-red">*</span>
                  </span>
                </div>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item>
                    <el-radio class="radio-align" v-model="hasTaxNo" label="1">Có</el-radio>
                  </el-form-item>
                </el-col>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item>
                    <el-radio class="radio-align" v-model="hasTaxNo" label="2">Không</el-radio>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :lg="12" :md="12" :sm="24" v-if="hasTaxNo !== '2'">
                  <el-form-item
                    class="label-taxno"
                    label="Mã số thuế cá nhân"
                    prop="DependentTaxNo"
                    :rules="[
                      {
                        required: true,
                        message: 'Mã số thuế cá nhân không được bỏ trống',
                        trigger: ['blur', 'change'],
                      },
                    ]"
                    :show-message="validate.DependentTaxNo"
                  >
                    <input-field
                      id="DependentTaxNo"
                      :number="true"
                      type="field-input"
                      placeholder="Nhập mã số thuế cá nhân"
                      :valueInput="FormDependentPerson.DependentTaxNo"
                      @hover="validate.DependentTaxNo = $event"
                      @value="FormDependentPerson.DependentTaxNo = $event"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row :gutter="16" v-if="hasTaxNo === '2'"> 
                <div class="label-passport">
                  <span>
                    Có CMND/CCCD/Hộ chiếu hay không?
                    <span class="color-red">*</span>
                  </span>
                </div>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item>
                    <el-radio v-model="isPassport" label="1" class="radio-align">Có</el-radio>
                  </el-form-item>
                </el-col>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item>
                    <el-radio   class="radio-align" v-model="isPassport" label="2">Không</el-radio>
                  </el-form-item>
                </el-col>
              </el-row>
              <!-- Thông tin giấy tờ -->
              <div
                class="PersonInfoTab"
                v-if="hasTaxNo === '2' && isPassport === '1'"
              >
                <el-row>
                  <div class="label-title2">
                    <span>
                      Loại giấy tờ
                      <span class="color-red">*</span>
                    </span>
                  </div>
                  <el-form-item prop="PersonalInfoType">
                    <m-sel
                      id="PersonalInfoType"
                      type="label-top"
                      label="label"
                      field="value"
                      :defaultString="PersonalInfoType"
                      @value="
                        PersonalInfoType =
                          PersonalInfoTypeList[$event - 1].label
                      "
                      :list="PersonalInfoTypeList"
                    />
                  </el-form-item>
                </el-row>
                <el-row>
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item
                      :label="'Số hiệu giấy tờ'"
                      prop="CitizenIdentityNo"
                      :rules="[
                        {
                          required: true,
                          message: `Số hiệu giấy tờ không được bỏ trống`,
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.PersonalInfoType"
                    >
                      <input-field
                        :number="true"
                        id="CitizenIdentityNo"
                        type="field-input"
                        placeholder="Nhập số hiệu giấy tờ"
                        @hover="validate.PersonalInfoType = $event"
                        :valueInput="FormDependentPerson.CitizenIdentityNo"
                        @value="FormDependentPerson.CitizenIdentityNo = $event"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :lg="12" :md="12" :sm="24"></el-col>
                </el-row>
              </div>
              <!-- Thông tin giấy khai sinh -->
              <div
                class="PersonInfoTab"
                v-if="hasTaxNo === '2' && isPassport === '2'"
              >
                <el-row :gutter="16">
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item
                      class="birth-certificate"
                      label="Giấy khai sinh số"
                      :rules="[
                        {
                          required: true,
                          message: `Số hiệu giấy khai sinh không được bỏ trống`,
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.BirthCertificateNumber"
                      prop="BirthCertificateNumber"
                    >
                      <input-field
                        type="field-input"
                        id="BirthCertificateNumber"
                        placeholder="Nhập số hiệu giấy khai sinh"
                        @hover="validate.BirthCertificateNumber = $event"
                        :valueInput="FormDependentPerson.BirthCertificateNumber"
                        @value="
                          FormDependentPerson.BirthCertificateNumber = $event
                        "
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item label="Quyển số" prop="FullName">
                      <input-field
                        type="field-input"
                        placeholder="Nhập số hiệu giấy khai sinh"
                        :valueInput="FormDependentPerson.BirthCertificateBook"
                        @value="
                          FormDependentPerson.BirthCertificateBook = $event
                        "
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="16">
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item label="Quốc gia" class="label-title3">
                      <m-sel
                        type="label-top"
                        label="NationalityName"
                        field="NationalityID"
                        :disable="true"
                        :defaultNumber="84"
                        @value="
                          FormDependentPerson.BirthCertificateNationalityID = $event
                        "
                        :list="NationalList"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item
                      class="label-title3"
                      label="Tỉnh/Thành phố"
                      :rules="[
                        {
                          required: true,
                          message: `Tỉnh/Thành phố không được bỏ trống`,
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.BirthCertificateProvinceID"
                      prop="BirthCertificateProvinceID"
                    >
                      <m-sel
                        id="BirthCertificateProvinceID"
                        @hover="validate.BirthCertificateProvinceID = $event"
                        type="label-top"
                        :filterable="true"
                        placeholder="Chọn tỉnh/thành phố"
                        label="ProvinceName"
                        field="ProvinceID"
                        :list="ProvinceList"
                        :defaultNumber="
                          FormDependentPerson.BirthCertificateProvinceID
                        "
                        @value="FlagBirthCertificateProvinceID = $event"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row :gutter="16">
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item
                      label="Quận/Huyện"
                      :rules="[
                        {
                          required: true,
                          message: `Quận/Huyện phố không được bỏ trống`,
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.BirthCertificateDistrictID"
                      prop="BirthCertificateDistrictID"
                    >
                      <m-sel
                        id="BirthCertificateDistrictID"
                        @hover="validate.BirthCertificateDistrictID = $event"
                        type="label-top"
                        :filterable="true"
                        label="DistrictName"
                        field="DistrictID"
                        :defaultNumber="
                          FormDependentPerson.BirthCertificateDistrictID
                        "
                        @value="FlagBirthCertificateDistrictID = $event"
                        :list="DistrictList"
                        placeholder="Chọn quận/huyện"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :lg="12" :md="12" :sm="24">
                    <el-form-item
                      label="Phường/Xã"
                      :rules="[
                        {
                          required: true,
                          message: `Phường/Xã không được bỏ trống`,
                          trigger: ['blur', 'change'],
                        },
                      ]"
                      :show-message="validate.BirthCertificateWardID"
                      prop="BirthCertificateWardID"
                    >
                      <m-sel
                        id="BirthCertificateWardID"
                        @hover="validate.BirthCertificateWardID = $event"
                        type="label-top"
                        label="WardName"
                        field="WardID"
                        :filterable="true"
                        :defaultNumber="
                          FormDependentPerson.BirthCertificateWardID
                        "
                        @value="
                          FormDependentPerson.BirthCertificateWardID = $event
                        "
                        placeholder="Chọn Phường/Xã"
                        :list="WardList"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>
              </div>
            </el-col>
          </el-row>
        </el-form>
      </template>
      <template v-slot:footer>
        <!-- Button -->
        <btn
          name="Lưu và đóng"
          type="multi-button"
          :dataDropdown="buttons"
          class="ml-10 btn-footer"
          @addandclose="AddandClose"
          @addandnew="AddandNew"
          :handleClick="AddandClose"
          :disabled="true"
        ></btn>
        <btn
          name="Hủy"
          type="btn-grey"
          class="ml-10 btn-footer none-boder"
          :handleClick="getClose"
        ></btn>
        <!-- Button -->
      </template>
    </add-dialog>
    <!-- / Dialog thêm hồ sơ người phụ thuộc -->
  </div>
</template>
<script>
import dialog from "@/components/dialog";
import btn from "@/components/buttons";
import datetime2 from "@/components/date-time-pickers/date-time-picker-v2";
// import datetime from "@/components/date-time-pickers";
import fieldinput from "@/components/inputs";
import actionTable from "@/enums/actionTable.js";
import select from "@/components/select";
import CategoryDictionaryAPI from "@/api/etax/category-dictionary.js";
import declaration from "@/api/etax/declaration.js";
import relation from "@/enums/relation.js";
// import inputnumber from "@/components/inputs/input-number";
import { formatDate } from "@/utils/index.js";

export default {
  components: {
    "add-dialog": dialog,
    "input-field": fieldinput,
    btn,
    "m-sel": select,
    datetime2,
    // datetime,
    // "input-number": inputnumber,
  },
  props: {
    visiableDL: {
      type: Boolean,
      default: false,
    },
    declarationID: {
      type: String,
      default: "",
    },
    EmployeeName: {
      type: String,
      default: "Tên nhân viên",
    },
    title: {
      type: String,
      default: "THÊM MỚI NGƯỜI PHỤ THUỘC",
    },
    formMode: {
      type: Number,
    },
    DependentSelected: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      isValidate: true,
      // FormDependentPersonUpdate:{},
      filter: {
        taxNo: "",
        citizenIdentityNo: "",
        birthCertificateNumber: "",
      },
      // title: "THÊM MỚI NGƯỜI PHỤ THUỘC",
      dialogid: "add-dependent-dialog",
      NowYear: new Date().getFullYear(),
      NowMonth: new Date().getMonth() + 1,
      NowDate: new Date().getDate(),
      PersonalInfoType: "1",
      isClearValidate: false,
      Relation: [],
      triggerVali: false,
      rules_1: {
        StartDependentDate: [
          {
            required: true,
            message: "Thời gian tính giảm trừ không được bỏ trống.",
            trigger: ["blur", "change"],
          },
        ],
        FinishDependentDate: [
          {
            required: false,
            message: "Thời gian tính giảm trừ không được bỏ trống.",
            trigger: ["blur", "change"],
          },
        ],
      },
      rules_2: {
        StartDependentDate: [
          {
            required: false,
            message: "Thời gian tính giảm trừ không được bỏ trống.",
            trigger: ["blur", "change"],
          },
        ],
        FinishDependentDate: [
          {
            required: true,
            message: "Thời gian tính giảm trừ không được bỏ trống.",
            trigger: ["blur", "change"],
          },
        ],
      },

      buttons: [
        {
          name: "Lưu và thêm mới",
          nameVal: "addandnew",
        },
      ],
      PersonalInfoTypeList: [
        {
          label: "Chứng minh nhân dân",
          value: 1,
        },
        {
          label: "Căn cước công dân",
          value: 2,
        },
        {
          label: "Hộ chiếu",
          value: 3,
        },
      ],
      rules: null,
      NationalList: [
        {
          EntityState: 0,
          NationalityID: 84,
          NationalityName: "Việt Nam",
          Status: 0,
        },
      ],
      listaction: [],
      dataTable: [],
      ProvinceList: [],
      DistrictList: [],
      WardList: [],
      hasTaxNo: "1",
      isPassport: "1",
      FlagBirthCertificateProvinceID: null,
      FlagBirthCertificateDistrictID: null,
      FormDependentPerson: {
        DeclarationDetailDependentID: "",
        DeclarationDetailID: "",
        EmployeeCode: "",
        DependentName: "",
        RelationID: 1,
        StartDependentDate: null,
        FinishDependentDate: null,
        CitizenIdentityNo: "",
        DependentTaxNo: "",
        BirthCertificateBook: "",
        BirthCertificateNumber: "",
        BirthCertificateNationalityID: "",
        BirthCertificateProvinceID: null,
        BirthCertificateDistrictID: null,
        BirthCertificateWardID: null,
        DateOfBirth: null,
        Confirm: false,
        DependentType: 0,
      },
      validate: {
        DependentTaxNo: false,
        EmployeeName: false,
        EmployeeTaxNo: false,
        DependentName: false,
        RelationID: false,
        StartDependentDate: false,
        FinishDependentDate: false,
        CitizenIdentityNo: false,
        BirthCertificateNumber: false,
        BirthCertificateBook: false,
        BirthCertificateNationalityID: false,
        BirthCertificateProvinceID: false,
        BirthCertificateDistrictID: false,
        BirthCertificateWardID: false,
        PersonalInfoType: false,
        DateOfBirth: false,
      },
      isDisabled: true,
    };
  },
  watch: {
    FormDependentPerson: {
      handler(val) {
        //Chỉnh sủa trường bắt buộc nhập
        //Modified by NQTrung 13/10
        if (val.DependentType == 0) {
          this.triggerVali = false;
        } else {
          this.triggerVali = true;
        }

        if (val.StartDependentDate && val.FinishDependentDate) {
          if (
            this.getYearFromDateMonth(val.FinishDependentDate) <
            this.getYearFromDateMonth(val.StartDependentDate)
          ) {
            this.warnPickDateFormTo();
          }
          if (
            this.getYearFromDateMonth(val.FinishDependentDate) ==
            this.getYearFromDateMonth(val.StartDependentDate)
          ) {
            if (
              this.getMonthFromDateMonth(val.FinishDependentDate) <
              this.getMonthFromDateMonth(val.StartDependentDate)
            ) {
              this.warnPickDateFormTo();
            }
          }
        }

        if (
          val.DependentName != "" ||
          val.StartDependentDate != null ||
          val.FinishDependentDate != null ||
          val.DateOfBirth != null ||
          val.DependentTaxNo != "" ||
          val.BirthCertificateBook != "" ||
          val.BirthCertificateNumber != "" ||
          val.BirthCertificateProvinceID != null ||
          val.BirthCertificateDistrictID != null ||
          val.BirthCertificateWardID != null ||
          val.CitizenIdentityNo != ""
        ) {
          this.isDisabled = false;
        } else this.isDisabled = true;
      },
      deep: true,
    },

    // Cập nhật giá trị Quận huyện khi tỉnh thành phố thay đổi
    FlagBirthCertificateProvinceID: {
      handler(val) {
        if (val !== this.FormDependentPerson.BirthCertificateProvinceID) {
          this.FormDependentPerson.BirthCertificateProvinceID = val;
          this.FormDependentPerson.BirthCertificateDistrictID = null;
          this.FormDependentPerson.BirthCertificateWardID = null;
        }
        if (val) this.setProvince(val);
      },
    },
    // Cập nhật giá trị Xã phường khi tỉnh Quận Huyện thay đổi
    FlagBirthCertificateDistrictID: {
      handler(val) {
        this.FormDependentPerson.BirthCertificateDistrictID = val; //
        this.FormDependentPerson.BirthCertificateWardID = null;
        if (val) this.setDistrict(val);
      },
    },
    DependentSelected: {
      handler(val) {
        this.mapData(val);
      },
    },
    "FormDependentPerson.DepedentTaxNo": {
      handler(val) {
        if (!val) {
          this.hasTaxNo = 1;
        }
      },
    },
    visiableDL: {
      handler() {
        this.$refs["FormDependentPerson"].clearValidate();
      },
    },
    formMode: {
      handler(val) {
        if (val == 0) {
          this.buttons = [];
        } else {
          this.buttons = [
            {
              name: "Lưu và thêm mới",
              nameVal: "addandnew",
            },
          ];
        }
      },
    },
    
  },
  methods: {
    /**
     * Hàm cảnh báo nhập sai Thời gian tính giảm trừ
     */
    warnPickDateFormTo() {
      this.$_Popup.warn(
        "Cảnh báo",
        "Thời gian tính giảm trừ không hợp lệ. Vui lòng kiểm tra lại",
        [
          {
            text: "Đóng",
            class: "btn-primary",
            callback: () => {
              this.FormDependentPerson.StartDependentDate = null;
              this.FormDependentPerson.FinishDependentDate = null;
            },
          },
        ]
      );
    },

    getMonthFromDateMonth(value) {
      const monthyear = value.split("-");
      return parseInt(monthyear[0]);
    },

    getYearFromDateMonth(value) {
      const monthyear = value.split("-");
      return parseInt(monthyear[1]);
    },

    getRowsSelection(val) {
      console.log(val);
    },
    
    handleRowDBLClick() {},

    handleSelectAll() {},

    getRowAction() {},
    
    async setUpForm() {
      const res = await CategoryDictionaryAPI.getNationalites();
      this.NationalList = res.data.Data;
    },

    /**
     * Lấy Danh sách Quận huyện theo Quốc gia mặc định là Viêt Nam
     * Created by: NVANH created date: 22.09.2020
     */
    async setNational() {
      const res = await CategoryDictionaryAPI.getProvincials();
      this.ProvinceList = res.data.Data;
      this.DistrictList = [];
      this.WardList = [];
    },

    /**
     * Lấy Danh sách Quận huyện theo tỉnh/thành phố
     * Created by: NVANH created date: 22.09.2020
     */
    async setProvince(province) {
      const res = await CategoryDictionaryAPI.getDistrictsByDistrictsID(
        province
      );
      this.DistrictList = res.data.Data;
      this.WardList = [];
    },

    /**
     * Lấy Danh sách phường/ xã theo quận/huyện
     * Created by: NVANH created date: 22.09.2020
     */
    async setDistrict(distric) {
      const res = await CategoryDictionaryAPI.getWardByDistrictsID(distric);
      this.WardList = res.data.Data;
    },

    /**
     * Format lại ngày tháng năm sinh
     * Created by: NVANH created date: 22.09.2020
     */
    revertDateofBirth(value) {
      const date = value.split("-");
      return `${date[2]}-${date[0]}-${date[1]}`;
    },
    /**
     * Hiển thị cảnh báo khi thêm mới người phụ thuộc mà bị trùng thông tin
     * created by tcduong 10.10.2020
     */
    popUpWarnDuplicateDependent(content) {
      this.buttons = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];
      // gọi dialog
      this.$_Popup.warn(`Cảnh báo`, content, this.buttons);
    },
    /**
     * Thêm mới và chỉnh sửa người phụ thuộc
     * Created by: NVANH created date: 22.09.2020
     * Modified by NQTrung modified date 26/09/2020
     */
    addDependentPerson(isClose) {
      this.isClearValidate = false;
      this.$refs["FormDependentPerson"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById(`${Object.keys(index)[0]}`).focus();
            }, 200);
          }
        }

        if (valid) {
          //Them moi formMode == 1
          if (this.formMode == 1) {
            var data = Object.assign({}, this.FormDependentPerson);
            data.DeclarationDetailID = this.declarationID;
            if (!data.DateOfBirth) delete data.DateOfBirth;
            else data.DateOfBirth = this.revertDateofBirth(data.DateOfBirth);
            if (!data.StartDependentDate) delete data.StartDependentDate;

            if (!data.FinishDependentDate) delete data.FinishDependentDate;

            var res = await declaration.postDeclarationDetailDependent(data);
            if (!res.data.Success) {
              /**
               * lttuan sửa lỗi 5331 sai câu thông báo lỗi đăng ký người phụ thuôc
               * modify date: 091020
               * modify by tcduong
               * modify date 10.10.2020
               *
               * Thay đổi nội dung câu thông báo cho đúng với yêu cầu
               * modify lttuan 131020
               */
              if (res.data.MISACode == 300 && res.data.Data.DependentTaxNo) {
                this.popUpWarnDuplicateDependent(
                  `Mã số thuế <b>${this.FormDependentPerson.DependentTaxNo}</b> trùng với người phụ thuộc đã tồn tại`
                );
              } else if (
                res.data.MISACode == 300 &&
                res.data.Data.CitizenIdentityNo
              ) {
                this.popUpWarnDuplicateDependent(
                  `Số ${this.PersonalInfoType} <b>${this.FormDependentPerson.CitizenIdentityNo}</b> trùng với người phụ thuộc đã tồn tại`
                );
              } else if (
                res.data.MISACode == 300 &&
                res.data.Data.BirthCertificateNumber
              ) {
                this.popUpWarnDuplicateDependent(
                  `Số giấy khai sinh <b>${this.FormDependentPerson.BirthCertificateNumber}</b> trùng với người phụ thuộc đã tồn tại`
                );
              } else {
                this.$_Notification.error(
                  "Thất bại",
                  "Bạn đã thêm người phụ thuộc thất bại. Vui lòng kiểm tra lại."
                );
              }
            } else {
              if (isClose) {
                this.getClose();
              } else {
                this.resetFormDependentPerson();
              }
              this.$emit("success", true);
              this.$_Notification.success(
                "Thành công",
                `Thêm người phụ thuộc cho nhân viên ${this.EmployeeName} thành công`
              );
            }
          }
          //Chỉnh sửa formMode == 0
          if (this.formMode == 0) {
            this.FormDependentPerson.confirm = true;

            var editObj = { ...this.FormDependentPerson };
            const res = await declaration.putDeclarationDetailDependent(
              editObj
            );
            if (!res.data.Success) {
              /**
               * Thay đổi nội dung câu thông báo cho đúng với yêu cầu
               * created lttuan 131020
               */
              if (res.data.MISACode == 300) {
                this.buttons = [
                  {
                    text: "Đóng",
                    class: "btn-primary",
                    callback: () => {},
                  },
                ];
                // gọi dialog
                this.$_Popup.warn(
                  `Cảnh báo`,
                  `Người phụ thuộc ${this.FormDependentPerson.DependentName} đã tồn tại. Vui lòng kiểm tra lại.`,
                  this.buttons
                );
              } else {
                this.$_Notification.error(
                  "Thất bại",
                  "Bạn đã chỉnh sửa người phụ thuộc thất bại. Vui lòng kiểm tra lại."
                );
              }
            } else {
              this.$emit("success", true);
              this.$_Notification.success(
                "Thành công",
                "Chỉnh sửa người phụ thuộc thành công"
              );
              this.getClose();
            }
          }
        }
      });
    },

    async validateForm(prop) {
      if (
        this.isClearValidate &&
        (prop == "DependentTaxNo" ||
          prop == "BirthCertificateWardID" ||
          prop == "CitizenIdentityNo")
      ) {
        await this.$refs["FormDependentPerson"].clearValidate();
        this.isClearValidate = false;
      }
    },
    //bind dữ liệu vào dialog chỉnh sửa
    mapData(val) {
      this.FormDependentPerson.DeclarationDetailDependentID =
        val.DeclarationDetailDependentID;
      this.FormDependentPerson.DependentName = val.DependentName;
      this.FormDependentPerson.DateOfBirth = val.DateOfBirth;

      // sửa lỗi bind ngày tháng vào form bị sai
      // modify lttuan 131020
      if (val.DateOfBirth) {
        this.FormDependentPerson.DateOfBirth = formatDate(
          val.DateOfBirth,
          "mm-dd-yyyy"
        );
      }

      this.FormDependentPerson.DeclarationDetailID = val.DeclarationDetailID;
      this.FormDependentPerson.RelationID = val.RelationID;
      this.FormDependentPerson.DependentType = val.DependentType;
      if (val.StartDependentDate)
        this.FormDependentPerson.StartDependentDate = val.StartDependentDate.split(
          "-"
        )
          .splice(0, 2)
          .reverse()
          .join("-");
      if (val.FinishDependentDate)
        this.FormDependentPerson.FinishDependentDate = val.FinishDependentDate.split(
          "-"
        )
          .splice(0, 2)
          .reverse()
          .join("-");
      this.FormDependentPerson.CitizenIdentityNo = val.CitizenIdentityNo;
      this.FormDependentPerson.DependentTaxNo = val.DependentTaxNo;
      this.FormDependentPerson.BirthCertificateBook = val.BirthCertificateBook;
      this.FormDependentPerson.BirthCertificateNumber =
        val.BirthCertificateNumber;
      this.FormDependentPerson.BirthCertificateNationalityID =
        val.BirthCertificateNationalityID;
      this.FormDependentPerson.BirthCertificateProvinceID =
        val.BirthCertificateProvinceID;
      this.FormDependentPerson.BirthCertificateDistrictID =
        val.BirthCertificateDistrictID;
      this.FormDependentPerson.BirthCertificateWardID =
        val.BirthCertificateWardID;
      if (
        !this.FormDependentPerson.BirthCertificateNumber &&
        !this.FormDependentPerson.CitizenIdentityNo
      ) {
        this.hasTaxNo = "1";
      } else {
        this.hasTaxNo = "2";
        if (!this.FormDependentPerson.CitizenIdentityNo) {
          this.isPassport = "2";
        } else {
          this.isPassport = "1";
        }
      }
    },

    // Thêm và đóng
    async AddandClose() {
      await this.addDependentPerson(true);
    },

    // Thêm và tạo mới
    async AddandNew() {
      await this.addDependentPerson(false);
    },
    // Reset lại form
    resetFormDependentPerson() {
      this.isClearValidate = true;
      this.hasTaxNo = "1";
      this.isPassport = "1";
      this.FormEmployee = {
        EmployeeCode: "",
        FullName: "",
        OrganizationUnitID: "",
        PositionName: "",
        TaxNo: "",
      };
      this.FormDependentPerson = {
        EmployeeCode: "",
        DependentName: "",
        RelationID: 1,
        StartDependentDate: null,
        FinishDependentDate: null,
        CitizenIdentityNo: "",
        DependentTaxNo: "",
        BirthCertificateBook: "",
        BirthCertificateNumber: "",
        BirthCertificateNationalityID: "",
        BirthCertificateProvinceID: null,
        BirthCertificateDistrictID: null,
        BirthCertificateWardID: null,
        DateOfBirth: null,
        DependentType: 0,
      };
    },

    // Đóng dialog
    getClose() {
      this.resetFormDependentPerson();
      this.$emit("closeDialog", false);
    },
  },

  created() {
    // this.setUpForm();
    this.setNational();
    this.listaction = actionTable.TAX_REGISTER;
    this.Relation = relation.RELATION;
  },
  mounted() {},
  computed: {
    getPersonalInfoType() {
      return this.PersonalInfoType;
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
#add-dependent-dialog {
  .color-red {
    color: red;
  }

  .el-form-item__label {
    font-family: $_fontmedium;
    color: $dark;
    line-height: 17px;
    padding: 16px 0 4px 0;
    margin: 0;
    font-size: 13px;
  }

  .label-title {
    margin: 0;
    line-height: 17px;
    padding: 16px 8px 4px 8px;

    span{
      font-family: $_fontmedium;
    }
  }

  #RelationID {
    margin-top: -3px;
  }
  
  .header-dialog {
    padding-bottom: 24px;
    background-color: $white;
    padding-left: -14px;
    margin-left: -24px;
    margin-right: -24px;
  }
  .float-left {
    float: left;
  }
  #btn-tab {
    span {
      font-weight: bold;
      margin-left: -24px !important;
    }
  }
  #two-step-progress {
    justify-content: center;
  }
  .title-name {
    font-size: 15px !important;
    color: #6f6f6f;
  }
  .e-name {
    font-size: 15px !important;
    color: $dark;
    font-weight: bold;
  }
  .el-form-item {
    margin-bottom: 0px !important;
  }

  
  .container-tab3 {
    min-height: 507px;
    .tab-bar {
      display: flex;
      height: 90px;
      .general-detail {
        width: 50%;
      }
      .add-button {
        width: 50%;
        button {
          position: relative;
          bottom: 0px;
          right: 0px;
        }
      }
    }
  }
  .w-100 {
    padding-bottom: 0px !important;
  }
  .middle-date {
    padding-left: 15px;
  }
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

  .company-container {
    .Gettingstartted-title {
      height: 70px;
      text-align: center;
      .company-title {
        font-size: 20px;
        font-weight: bold;
        padding-top: 30px;
      }
    }
    .Add-Emplouee-tab {
      border-bottom: 1px solid $bg-color-3;
      height: 60px;
      display: flex;
      margin-left: -24px;
      padding-bottom: 20px;
      border: -45px;
      border-bottom: 1px solid #d1d7e0;
      margin-right: -24px;
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
    .Add-Emplouee-main {
      height: calc(100% - 212px);
      min-height: 445px;
      padding: 0 0px 10px;
      .contract-tax .el-form-item__label {
        font-size: 13px !important;
        font-weight: bold !important;
      }
      .el-form {
        overflow: initial;
      }
    }
  }
  .Add-Emplouee-footer {
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
  .container-tab3 {
    .infor-depentdent-person {
      width: 209px;
      height: 71px;
      padding-top: 22px;
      .pb-10 {
        padding-bottom: 10px;
      }
      .color-label {
        color: #454545;
      }
      .color-infor {
        color: $dark;
      }
    }
    .table {
      background-color: $background-grey;
      margin-left: -10px;
      margin-right: -10px;
      padding-top: 16px;
    }
  }
  .el-dialog__footer {
    background-color: $hover-grey !important;
    .none-boder .btn-seconds {
      border: none !important;
    }
  }

  .el-table thead tr:first-child .cell {
    word-break: break-word;
    white-space: unset;
  }
  .el-dialog__body {
    margin-bottom: 14px;
  }
  .tab2-dependent-register {
    padding-top: 5px;
  }
  .label-title2 {
    padding-top: 16px;
  }
  .label-title3 {
    margin-top: -5px;
  }
  .label-passport {
    padding-top: 21px;
  }
  .radio-align{ 
    margin-top: 12px;
  }
  .lable-radio{
    margin-top: 17px;
  }
  .label-taxno{
    margin-top: 3px;
  }
}
</style>
