<template>
  <div>
    <!-- Dialog thêm người nộp thuế -->
    <add-dialog
      :titleDialog="title"
      :id="dialogid"
      :loading="loadingDialog"
      :dialogFormVisible="visiableDL"
      :closeonclickmodal="false"
      @close-dialog="getClose()"
      xl="8,9"
      lg="8,9"
      md="9,10"
      sm="4,3"
      xs="7,8"
    >
      <template v-slot:body>
        <el-form
          ref="FormEmployee"
          :model="FormEmployee"
          label-width="120px"
          label-position="top"
          @validate="validateForm"
        >
          <el-row :gutter="24" style="padding-top: 12px">
            <el-col :lg="12" :md="12" :sm="24">
              <el-form-item label="Mã nhân viên" prop="EmployeeCode">
                <input-field
                  id="EmployeeCode"
                  type="field-input"
                  placeholder="Nhập mã nhân viên"
                  :valueInput="FormEmployee.EmployeeCode"
                  @value="FormEmployee.EmployeeCode = $event"
                />
              </el-form-item>
              <el-form-item
                label="Họ và tên"
                prop="FullName"
                :rules="[
                  {
                    required: true,
                    message: 'Họ và tên nhân viên không được bỏ trống.',
                    trigger: ['blur', 'change'],
                  },
                ]"
                :show-message="validate.FullName"
              >
                <input-field
                  id="FullName"
                  type="field-input"
                  placeholder="Nhập họ và tên"
                  @hover="validate.FullName = $event"
                  :valueInput="FormEmployee.FullName"
                  :onlyText="true"
                  @value="FormEmployee.FullName = $event"
                  :isUpercasefirst="true"
                />
              </el-form-item>
              <el-row :gutter="24">
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item
                    label="Bộ phận/Phòng ban"
                    :show-message="validate.OrganizationUnitID"
                    prop="OrganizationUnitID"
                    :rules="[
                      {
                        required: true,
                        message: 'Bộ phận/Phòng ban không được bỏ trống.',
                        trigger: ['blur'],
                      },
                    ]"
                  >
                    <tree-select
                      id="OrganizationUnitID"
                      @hover="validate.OrganizationUnitID = $event"
                      :arraySetting="arraySetting"
                      :data="OrganUnitList"
                      :valueSelect="FormEmployee.OrganizationUnitID"
                      :returnObject="true"
                      @change="getOrganizationUnitValue"
                      :multiple="false"
                    >
                    </tree-select>
                  </el-form-item>
                </el-col>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item
                    label="Mã số thuế"
                    prop="TaxNo"
                    :rules= ruleTaxNo
                    :show-message="validate.EmployeeTaxNo"
                    :error="customError"
                  >
                    <input-field
                      id="TaxNo"
                      type="field-input"
                      :number="true"
                      placeholder="Nhập mã số thuế"
                      @hover="validate.EmployeeTaxNo = $event"
                      :valueInput="FormEmployee.TaxNo"
                      @value="FormEmployee.TaxNo = $event"
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <el-row>
                <el-form-item
                  label="Loại giấy tờ tùy thân"
                  prop="IndentityType"
                  :rules="[
                    {
                      required: true,
                      message: 'Loại giấy tờ tùy thân được bỏ trống.',
                      trigger: ['blur', 'change'],
                    },
                  ]"
                  :show-message="validate.IndentityType"
                >
                  <m-sel
                    id="IndentityType"
                    type="label-top"
                    label="DBOptionName"
                    field="DBOptionID"
                    @hover="validate.IndentityType = $event"
                    :defaultString="FormEmployee.IndentityType"
                    @value="FormEmployee.IndentityType = $event"
                    :list="IdentityList"
                  />
                </el-form-item>
              </el-row>
              <el-row>
                <el-form-item
                  :label="'Số ' + IndentityName"
                  prop="CitizenIdentityNo"
                  :rules="[
                    {
                      required: true,
                      message: `Số ${IndentityName} không được bỏ trống`,
                      trigger: ['blur', 'change'],
                    },
                  ]"
                  :show-message="validate.CitizenIdentityNo"
                >
                  <input-field
                    id="CitizenIdentityNo"
                    type="field-input"
                    :number="isPassport"
                    placeholder="Nhập số hiệu giấy tờ"
                    @hover="validate.CitizenIdentityNo = $event"
                    :valueInput="FormEmployee.CitizenIdentityNo"
                    @value="FormEmployee.CitizenIdentityNo = $event"
                  />
                </el-form-item>
              </el-row>
            </el-col>
            <el-col :lg="12" :md="12" :sm="24">
              <!-- row 2 -->
              <el-form-item
                label="Cá nhân cư trú ?"
                :rules="[
                  {
                    required: true,
                    message: 'Cá nhân cư trú không được bỏ trống',
                    trigger: ['blur', 'change'],
                  },
                ]"
              >
                <el-radio v-model="FormEmployee.IndividualResident" :label="1"
                  >Có</el-radio
                >
                <el-radio v-model="FormEmployee.IndividualResident" :label="0"
                  >Không</el-radio
                >
              </el-form-item>
            </el-col>
            <el-col :lg="12" :md="12" :sm="24">
              <el-form-item
                label="Thu nhập chịu thuế"
                :rules="[
                  {
                    required: true,
                    message: 'Thu nhập chịu thuế không được bỏ trống',
                    trigger: ['blur', 'change'],
                  },
                ]"
                :show-message="validate.TotalIncomeTaxable"
                prop="TotalIncomeTaxable"
              >
                <inputnumber
                  @hover="validate.TotalIncomeTaxable = $event"
                  :valueInput="FormEmployee.TotalIncomeTaxable"
                  @value="FormEmployee.TotalIncomeTaxable = $event"
                  id="TotalIncomeTaxable"
                  type="input-append"
                  :append="'VNĐ'"
                  :money="true"
                  width="350px"
                />
              </el-form-item>
              <div class="tax-info">
                <div class="title">Biểu thuế áp dụng</div>
                <div class="number">{{ percentTax }} %</div>
              </div>
              <div class="tax-info">
                <div class="title">Số thuế TNCN đã khấu trừ</div>
                <div class="number append">VNĐ</div>
                <div class="number bolder">{{ textMoney }}</div>
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
          :disabled="isDisabled"
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
    <!-- / Dialog thêm người nộp thuế -->
  </div>
</template>
<script>
import dialog from "@/components/dialog";
import btn from "@/components/buttons";
// import datetime2 from "@/components/date-time-pickers/date-time-picker-v2";
// import datetime from "@/components/date-time-pickers";
import fieldinput from "@/components/inputs";
import actionTable from "@/enums/actionTable.js";
import select from "@/components/select";
import List_INDENTITY from "@/enums/listIndentity.js";
import organizationUnit from "@/api/etax/organization-unit.js";
import relation from "@/enums/relation.js";
import inputnumber from "@/components/inputs/input-number";
import tree from "@/components/trees/index.vue";
import TaxInfoAPI from "@/api/etax/tax-info.js";
import DeclarationSettlementDetailAPI from "@/api/etax/declaration_settlement_detail.js";
import { TaxType, TaxInfo } from "@/enums/tax-info.js";
import IdentityEnum from "@/enums/listIndentity.js";
import declarationSettlementDetailEnum from "@/enums/declaration-settlement.js";
import StatusResidentEnum from "@/enums/excelStatus.js";
import { MISA_CODE } from "@/enums/misacode.js";
export default {
  components: {
    "add-dialog": dialog,
    "input-field": fieldinput,
    btn,
    "m-sel": select,
    "tree-select": tree,
    // datetime2,
    // datetime,
    inputnumber,
  },
  props: {
    visiableDL: {
      type: Boolean,
      default: false,
    },
    DeclarationSettlementDetailID: {
      type: String,
      default: "",
    },
    title: {
      type: String,
      default: "THÊM MỚI NGƯỜI NỘP THUẾ",
    },
    formMode: {
      type: Number,
    },
    EmployeeSelected: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      customError:null,
      loadingDialog:false,
      ruleTaxNo: [
        {
          required: true,
          message: "Mã số thuế không được bỏ trống.",
          trigger: ["change"],
        },
      ],
      TaxInfo: [],
      percentTax: 0,
      textMoney: "0",
      IndentityTypeList: [
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
      IndentityName: List_INDENTITY.LIST_INDENTITY[0].DBOptionName,
      isValidate: true,
      FormEmployeeUpdate: {},
      // title: "THÊM MỚI người nộp thuế",
      dialogid: "tax-settlement-add-employee-dialog",
      NowYear: new Date().getFullYear(),
      NowMonth: new Date().getMonth() + 1,
      NowDate: new Date().getDate(),
      isClearValidate: false,
      Relation: [],

      buttons: [
        {
          name: "Lưu và thêm mới",
          nameVal: "addandnew",
        },
      ],
      rules: null,
      listaction: [],
      dataTable: [],
      arraySetting: {
        value: "OrganizationUnitID",
        label: "OrganizationUnitName",
        children: "OrganizationUnitsChildren",
      },
      IdentityList: [],
      OrganUnitList: [],
      FormEmployee: {
        EmployeeCode: "",
        FullName: "",
        OrganizationUnitID: null,
        OrganizationUnitName: "",
        TaxNo: "",
        IndentityType: List_INDENTITY.LIST_INDENTITY[0].DBOptionID,
        TotalTaxWithHeld: 0,
        CitizenIdentityNo: "",
        IndividualResident: 1,
        TotalIncomeTaxable: "",
        DeclarationSettlementDetailType: 1,
        DeclarationSettlementID: this.$route.params.id,
        Confirm: false,
        DeclarationSettlementDetailID: "",
      },
      validate: {
        TotalIncomeTaxable: false,
        EmployeeTaxNo: false,
        FullName: false,
        CitizenIdentityNo: false,
        IndentityType: false,
        OrganizationUnitID: false,
        OrganizationUnitName: false,
      },
      isDisabled: true,
      isPassport: true,
    };
  },
  watch: {
    "FormEmployee.EmployeeCode": async function (val) {
      var res = await DeclarationSettlementDetailAPI.getDeclarationSettlementDetailByEmployeeCode(
        {
          EmployeeCode: val,
          DelcarationType:
            declarationSettlementDetailEnum.DeclarationSettlementType.Bk02,
        }
      );
      var obj = res.data.Data;
      if (obj && res.data.Success) this.mapData(obj[0]);
      else {
        this.isClearValidate = true;
        this.FormEmployee = Object.assign(this.FormEmployee, {
          FullName: "",
          OrganizationUnitID: null,
          OrganizationUnitName: "",
          TaxNo: "",
          IndentityType: List_INDENTITY.LIST_INDENTITY[0].DBOptionID,
          TotalTaxWithHeld: 0,
          CitizenIdentityNo: "",
          IndividualResident: 1,
          TotalIncomeTaxable: "",
          DeclarationSettlementDetailType: 1,
          DeclarationSettlementID: this.$route.params.id,
          Confirm: false,
        });

        delete this.FormEmployee.DeclarationSettlementDetailID;
        delete this.FormEmployee.CreatedDate;
        delete this.FormEmployee.ModifiedDate;
        delete this.FormEmployee.EmployeeID;
      }
    },
    IndentityName: function (val) {
      if (val == "Hộ chiếu") {
        this.isPassport = false;
      } else {
        this.isPassport = true;
      }
    },
    FormEmployee: {
      handler(val) {
        if (
          val.EmployeeCode != "" ||
          val.FullName != "" ||
          val.OrganizationUnitID != null ||
          val.OrganizationUnitName != "" ||
          val.TaxNo != "" ||
          val.CitizenIdentityNo != "" ||
          val.TotalIncomeTaxable != 0
        ) {
          this.isDisabled = false;
        } else this.isDisabled = true;
      },
      deep: true,
    },

    EmployeeSelected: {
      handler(val) {
        this.mapData(val);
      },
    },
    "FormEmployee.IndentityType": function (val) {
      if (val) {
        this.IndentityName = this.IdentityList.filter(
          ({ DBOptionID }) => DBOptionID == this.FormEmployee.IndentityType
        )[0].DBOptionName;
      }
    },
    "FormEmployee.IndividualResident": function (val) {
      var condition = val == 1 ? "VANG_LAI_CU_TRU" : "VANG_LAI_KHONG_CU_TRU";
      this.percentTax = this.TaxInfo.filter(
        (item) => item.TaxInfoID == TaxInfo[condition]
      )[0].TaxPercent;
    },
    "FormEmployee.TotalIncomeTaxable": function (val) {
      if (val) {
        this.FormEmployee.TotalTaxWithHeld = (val * this.percentTax) / 100;
      } else this.FormEmployee.TotalTaxWithHeld = 0;
    },
    "FormEmployee.TotalTaxWithHeld": function (val) {
      this.formatMoneyText(val);
    },
    percentTax: function (val) {
      if (val && this.FormEmployee.TotalIncomeTaxable) {
        this.FormEmployee.TotalTaxWithHeld =
          (this.FormEmployee.TotalIncomeTaxable * val) / 100;
      } else this.FormEmployee.TotalTaxWithHeld = 0;
    },
    visiableDL: {
      handler(val) {
        if (val) {
          if (this.formMode == declarationSettlementDetailEnum.FormMode.Edit) {
            this.mapData(this.EmployeeSelected);
          }
        }
        if(this.$refs["FormEmployee"])
          this.$refs["FormEmployee"].clearValidate();
      },
    },
    formMode: {
      handler(val) {
        if (val == declarationSettlementDetailEnum.FormMode.Edit) {
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
    formatMoneyText(val) {
      if (val != 0) {
        this.textMoney = val
          .toString()
          .replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
      } else this.textMoney = 0;
    },
    getOrganizationUnitValue(val) {
        this.FormEmployee.OrganizationUnitID = val.OrganizationUnitID;
        this.FormEmployee.OrganizationUnitName = val.OrganizationUnitName;
    },

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
     * Thêm mới và chỉnh sửa người nộp thuế
     * Created by: NVANH created date: 22.09.2020
     */
    addEmployee(isClose) {
      this.isClearValidate = false;
      this.$refs["FormEmployee"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById(`${Object.keys(index)[0]}`).focus();
            }, 200);
          }
        }

        if (valid) {
          this.loadingDialog=true;
          //Them moi formMode == 1
          if (this.formMode == declarationSettlementDetailEnum.FormMode.Add) {
            this.clearValidateTaxNo();
            var data = Object.assign({}, this.FormEmployee);
            var res = await DeclarationSettlementDetailAPI.postDeclarationSettlementDetail(
              data
            );
            // Thêm thất bại
            if (!res.data.Success) {
              // Do Trùng CMT => Cập nhật hay k
              if(res.data.MISACode == MISA_CODE.ConfirmUpdate) {
                                  var identityName = this.IdentityList.filter(
                    ({ DBOptionID }) =>
                      DBOptionID == this.FormEmployee.IndentityType
                  )[0].DBOptionName;
                  var message = `Số ${identityName} <strong class='bold-mess'>${data.CitizenIdentityNo}</strong> đã được đăng ký cho người nộp thuế <strong class='bold-mess'>"${res.data.Data.FullName} - ${res.data.Data.EmployeeCode}".</strong><br/>
                  Bạn có muốn cập nhật lại thông tin cho <strong class='bold-mess'>"${res.data.Data.FullName} - ${res.data.Data.EmployeeCode}" ?</strong>
                  `;
                  this.$_Popup.warn(`Cảnh báo`, message, [
                    {
                      text: "Không",
                      class: "btn-grey",
                      callback: () => {},
                    },
                    {
                      text: "Có",
                      class: "btn-primary",
                      callback: async () => {
                        this.FormEmployee.Confirm = true;
                        this.FormEmployee.DeclarationSettlementDetailID =
                          res.data.Data.DeclarationSettlementDetailID;
                        await this.addEmployee(true);
                        this.FormEmployee.Confirm = false;
                        this.FormEmployee.DeclarationSettlementDetailID = "";
                      },
                    },
                  ]);
              }
              // Do trùng MST
              else if(res.data.MISACode == MISA_CODE.Exist) {
                this.customError="Mã số thuế đã tồn tại. Vui lòng kiểm tra lại";
                this.validate.EmployeeTaxNo = true;
              } 
              // Lỗi Exception, validate, ...
              else {
                this.$_Notification.error(
                  "Thất bại",
                  "Bạn đã thêm người nộp thuế thất bại. Vui lòng kiểm tra lại."
                );
              }
            } else {
              this.clearValidateTaxNo();
              this.$emit("success", true);
              this.$_Notification.success(
                "Thành công",
                `Thêm người nộp thuế thành công`
              );
              if (isClose) {
                this.getClose();
              } else {
                this.resetFormEmployee();
              }
            }
          }

          //Chỉnh sửa formMode == 0
          if (this.formMode == declarationSettlementDetailEnum.FormMode.Edit) {
            this.FormEmployee.confirm = true;
            var editObj = {
              ...this.FormEmployee,
              DeclarationSettlementDetailID: this.EmployeeSelected
                .DeclarationSettlementDetailID,
            };
            const res = await DeclarationSettlementDetailAPI.putDeclarationSettlementDetail(
              editObj
            );
            if (!res.data.Success) {
              if (res.data.MISACode == MISA_CODE.Exist) {
                this.popUpWarnDuplicateDependent(
                  `Giấy tờ tùy thân hoặc Mã số thuế đã tồn tại. Vui lòng kiểm tra lại.`
                );
              } else {
                this.$_Notification.error(
                  "Thất bại",
                  "Bạn đã chỉnh sửa người nộp thuế thất bại. Vui lòng kiểm tra lại."
                );
              }
            } else {
              this.$emit("success", true);
              this.$_Notification.success(
                "Thành công",
                "Chỉnh sửa người nộp thuế thành công"
              );
              this.getClose();
            }
          }
          this.loadingDialog=false;
        }
      });
    },

     /**
     * Hàm xóa validate custom
     * CreatedBy NVANH 24.11.2020
     */
    clearValidateTaxNo() {
      this.customError = null;
      var element = document.querySelectorAll(".is-error");
      for (let index = 0; index < element.length; index++) {
        if (element[index].querySelector(`#TaxNo`))
          element[index].classList.remove("is-error");
      }
      if (document.querySelectorAll(".is-error").length > 0)
        this.isDisableSubmit = true;
      else this.isDisableSubmit = false;
    },

    async validateForm(prop) {
      if (
        this.isClearValidate &&
        (prop == "TotalIncomeTaxable" || prop == "FullName")
      ) {
        await this.$refs["FormEmployee"].clearValidate();
        this.isClearValidate = false;
      }
    },

    /**
     *bind dữ liệu vào dialog chỉnh sửa 
     * createdby NVANH 11.11.2020
     * modifiedby NVANH 17.10.2020
     */
    mapData(val) {
      this.clearValidateTaxNo();
      this.FormEmployee = Object.assign(val, {
        DeclarationSettlementDetailType: 1,
        DeclarationSettlementID: this.$route.params.id,
        Confirm: false,
      });
      this.OrganUnitList=this.OrganUnitList.filter(item => item.OrganizationUnitID!='00000000-0000-0000-0000-000000000000');
      if(this.FormEmployee.OrganizationUnitID == '00000000-0000-0000-0000-000000000000') {
        this.OrganUnitList.push({OrganizationUnitID: this.FormEmployee.OrganizationUnitID, 
          OrganizationUnitName: this.FormEmployee.OrganizationUnitName});
      }
      if (!this.FormEmployee.IndentityType) {
        this.FormEmployee.IndentityType =
          IdentityEnum.LIST_INDENTITY[0].DBOptionID;
      }
      if (this.FormEmployee.IndividualResident==null) {
        this.FormEmployee.IndividualResident =
          StatusResidentEnum.IndividualResident.Yes;
      }
      this.IndentityName = this.IdentityList.filter(
        ({ DBOptionID }) => DBOptionID == this.FormEmployee.IndentityType
      )[0].DBOptionName;
    },

    // Thêm và đóng
    async AddandClose() {
      await this.addEmployee(true);
    },

    // Thêm và tạo mới
    async AddandNew() {
      await this.addEmployee(false);
    },
    // Reset lại form
    resetFormEmployee() {
      this.clearValidateTaxNo();
      this.isClearValidate = true;
      this.FormEmployee = {
        EmployeeCode: "",
        FullName: "",
        OrganizationUnitID: null,
        OrganizationUnitName: "",
        TaxNo: "",
        IndentityType: List_INDENTITY.LIST_INDENTITY[0].DBOptionID,
        TotalTaxWithHeld: 0,
        CitizenIdentityNo: "",
        IndividualResident: 1,
        TotalIncomeTaxable: "",
        DeclarationSettlementDetailType: 1,
        DeclarationSettlementID: this.$route.params.id,
        Confirm: false,
        DeclarationSettlementDetailID: "",
      };
      this.validateForm();
      this.IndentityName = List_INDENTITY.LIST_INDENTITY[0].DBOptionName;
    },

    // Đóng dialog
    getClose() {
      
      this.resetFormEmployee();
      this.$emit("closeDialog", false);
    },

    async getOrganizationUnits() {
      var res = await organizationUnit.getOrganizationUnit("tree", 0);
      if (!res.data.Success || !res.data.Data) {
        return [];
      }
      return res.data.Data;
    },

    async getTaxInfo() {
      var res = await TaxInfoAPI.getTaxInfoByType(
        TaxType.THU_NHAP_VANG_LAI,
        new Date().toString()
      );
      if (res.Success) this.TaxInfo = res.Data;
      else this.TaxInfo = [];
    },
  },

  async created() {
    var list =await this.getOrganizationUnits();
    this.listaction = actionTable.TAX_REGISTER;
    this.Relation = relation.RELATION;
    this.IdentityList = List_INDENTITY.LIST_INDENTITY;
    await this.getTaxInfo();
    this.percentTax = this.TaxInfo.filter(
      (item) => item.TaxInfoID == TaxInfo.VANG_LAI_CU_TRU
    )[0].TaxPercent;
    this.OrganUnitList.push(list);
    this.IndentityType = this.IdentityList[0].DBOptionName;
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
#tax-settlement-add-employee-dialog {
  .bold-mess {
    font-family: $_fontbold;
  }

  .tax-info {
    display: flow-root;
    padding-top: 15px;
    .title {
      color: $medium-dark-grey;
      float: left;
      font-size: 13px;
      font-family: $_fontregular;
      margin-bottom: unset;
    }
    .bolder {
      font-size: 15px;
      font-weight: bolder;
      font-family: $_fontbold;
    }
    .number {
      float: right;
      padding: 0 5px;
    }
  }
  #TotalIncomeTaxable {
    text-align: right;
  }
  .el-col .el-row {
    padding-bottom: unset;
    padding-top: unset;
    /* padding-bottom: 7px; */
    /* padding-top: 7px; */
  }
  .color-red {
    color: red;
  }
  #RelationID {
    margin-top: -3px;
  }
  .label-title {
    margin-bottom: 0 !important;
    font-family: $_fontregular;
    font-weight: normal;
    color: #000000;
    line-height: 22px;
    padding-top: 15px;
    font-size: 13px;
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
  .el-form-item__label {
    margin: 0 !important;
  }
  .el-form-item__label {
    margin-bottom: 0 !important;
    font-family: $_fontregular;
    font-weight: normal;
    color: $dark;
    line-height: 22px;
    padding-top: 15px;
    font-size: 13px;
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
  .is-error-Citizent {
    border: 1px solid $red;
  }
}
</style>
