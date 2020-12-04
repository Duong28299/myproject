<template>
  <div class="styleguideline">
    <label> demo wrong table</label>
       <tbl
        propertyError="ImportValidError"
        :styleFullError='{
            "border-top":"1px solid red",
            "border-left":"1px solid red",
            "border-color":"red"}'
        :styleHalfError='{
                "border-left":"1px solid red",
                "border-color": "red"}'
        typeTable="tableExcel"
        :loading="loading"
        :style="setStyle()"
        :dataTable="tableData4"
        :listColumn="listColumn4"
        :havePagination="true"
        width="100%"
        :tooltip="true"
        :statusFlag="true"
        :statusExcelStatus="excelStatus()"
      >
      </tbl>
    <div class="row">
      <div class="title-row">Danh sách button:</div>
      <div class="body-row">
        <btn
          class="mr-8 fl bg-white"
          :type="'multi-button'"
          :name="'Thêm người nộp thuế'"
          :dataDropdown="dataDropdown"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18241@2x.png" />
          </template>
        </btn>
        <btn
          class="mr-8 fl"
          :type="'multi-button'"
          :name="'Thêm người nộp thuế'"
          :dataDropdown="dataDropdown"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18241@2x.png" />
          </template>
        </btn>
        <btn
          class="mr-8 fl"
          :type="'btn-checkbox-icon'"
          :name="'Đăng ký thuế'"
          @getValue="getTEST"
          :ckValue="valTEST"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/ico-add-user@2x.png" />
          </template>
          <template v-slot:icon2>
            <img class="icon" src="@/icons/Group 19239@2x.png" />
          </template>
        </btn>
        <btn class="mr-8 fl" :type="'btn-primary'" :name="'btn-primary'"></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-primary-icon'"
          :iconel="'el-icon-s-custom'"
          :name="'btn-primary-icon'"
        ></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-primary-onlyicon'"
          :iconel="'el-icon-s-custom'"
          tooltip="primary-onlyicon"
        ></btn>
      </div>
      <div class="body-row">
        <btn class="mr-8 fl" :type="'btn-white'" :name="'btn-white'"></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-white-icon'"
          :iconel="'el-icon-s-custom'"
          :name="'btn-white-icon'"
        ></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-white-onlyicon'"
          :iconel="'el-icon-s-custom'"
          :tooltip="'btn-white-onlyicon'"
        ></btn>
      </div>
      <div class="body-row">
        <btn class="mr-8 fl" :type="'btn-white2'" :name="'btn-white2'"></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-white2-icon'"
          :iconel="'el-icon-s-custom'"
          :name="'btn-white2-icon'"
        ></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-white2-onlyicon'"
          :iconel="'el-icon-s-custom'"
          :tooltip="'btn-white2-onlyicon'"
        ></btn>
      </div>
      <div class="body-row">
        <btn class="mr-8 fl" :type="'btn-seconds'" :name="'btn-seconds'"></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-seconds-icon'"
          :iconel="'el-icon-s-custom'"
          :name="'btn-seconds-icon'"
        ></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-seconds-onlyicon'"
          :iconel="'el-icon-s-custom'"
          :name="'btn-seconds-icon'"
          tooltip="btn-seconds-onlyicon"
        ></btn>
      </div>
      <div class="body-row">
        <btn class="mr-8 fl" :type="'btn-delete'" :name="'btn-delete'"></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-delete-icon'"
          :iconel="'el-icon-s-custom'"
          :name="'btn-delete-icon'"
        ></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-delete-onlyicon'"
          :iconel="'el-icon-s-custom'"
          tooltip="btn-onlydelete"
        ></btn>
      </div>
      <div class="body-row">
        <btn class="mr-8 fl" :type="'btn-grey'" :name="'btn-grey'"></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-grey-icon'"
          :iconel="'el-icon-s-custom'"
          :name="'btn-grey-icon'"
        ></btn>
        <btn
          class="mr-8 fl"
          :type="'btn-grey-onlyicon'"
          :iconel="'el-icon-s-custom'"
          tooltip="btn-grey-onlyicon"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-white3-onlyicon'"
          :iconel="'el-icon-delete'"
          tooltip="btn-white3-onlyicon"
        ></btn>
      </div>
      <!-- <div class="body-row">
            <btn class="mr-8 fl" :type="'btn-warn'" :name="'Button warn'"></btn>
            <btn class="mr-8 fl" :type="'btn-warn'" :iconel="'el-icon-s-custom'" :name="'Button warn'"></btn>
      </div>-->
    </div>
    <div class="row">
      <div class="title-row">Danh sách chip:</div>
      <div class="body-row">
        <el-tag type>Đã tiếp nhận</el-tag>
      </div>
      <div class="body-row">
        <el-tag type="success">Chấp nhận</el-tag>
      </div>
      <div class="body-row">
        <el-tag type="info">Chưa nộp</el-tag>
      </div>
      <div class="body-row">
        <el-tag type="warning">Đã nộp</el-tag>
      </div>
      <div class="body-row">
        <el-tag type="danger">Không chấp nhận</el-tag>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Radio:</div>
      <div class="body-row">
        <el-radio v-model="radio" label="1">Option 1</el-radio>
        <el-radio v-model="radio" label="2">Option 2</el-radio>
        <el-radio v-model="radio" label="3">Option 3</el-radio>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Check box:</div>
      <div class="body-row">
        <el-checkbox v-model="checked">Option 1</el-checkbox>
      </div>
      <div class="body-row">
        <el-checkbox v-model="checked1">Option 2</el-checkbox>
      </div>
      <div class="body-row">
        <el-checkbox v-model="checked2">Option 3</el-checkbox>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Danh sách Popup:</div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-primary'"
          :name="'Popup success'"
          :handleClick="callPopupsuccess"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-warn'"
          :name="'Popup warn'"
          :handleClick="callPopupwarn"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-delete'"
          :name="'Popup delete'"
          :handleClick="callPopupdelete"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-seconds'"
          :name="'Popup info'"
          :handleClick="callPopupinfo"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-primary'"
          :name="'Popup loading'"
          :handleClick="callPopuploading"
        ></btn>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Danh sách Notifications:</div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-primary'"
          :name="'Notification success'"
          :handleClick="callNotificationsuccess"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-warn'"
          :name="'Notification warn'"
          :handleClick="callNotificationwarning"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-delete'"
          :name="'Notification error'"
          :handleClick="callNotificationerror"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-grey'"
          :name="'Notification info'"
          :handleClick="callNotificationinfo"
        ></btn>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Drop down:</div>
      <div class="body-row">
        <selectinput
          type="label-top"
          style="width: 200px"
          :list="list"
          label="MONTH_NAME"
          field="MONTH"
          :defaultNumber="0"
        ></selectinput>
      </div>
      <div class="body-row">
        <selectinput
          :value="'Selected'"
          :width="'130px'"
          :iconel="'el-icon-s-custom'"
          :data="data"
        ></selectinput>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Multi button:</div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'multi-button'"
          :name="'Multi Button'"
          :dataDropdown="dataDropdown"
        ></btn>
      </div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'multi-button'"
          :name="'Multi Button'"
          :dataDropdown="dataDropdown"
          :iconel="'el-icon-s-custom'"
        ></btn>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Danh sách các loại input:</div>
      <div class="body-row">
        <span>- Input nhập liệu thường:</span>
      </div>
      <div class="body-row">
        <fieldinput
          type="field-input"
          :placeholder="'Field Input'"
          width="300px"
        />
      </div>
      <div class="body-row">
        <span>- Input nhập liệu thường có icon:</span>
      </div>
      <div class="body-row">
        <fieldinput
          type="input-icon"
          :placeholder="'Tìm kiếm theo tên bảng tạm tính'"
          classicon="icon-search"
          width="300px"
        />
      </div>
      <div class="body-row">
        <span>- Input nhập liệu vô hiệu nhập:</span>
      </div>
      <div class="body-row">
        <fieldinput
          type="field-input"
          :placeholder="'Field Input'"
          :disabled="true"
          width="300px"
        />
      </div>
      <div class="body-row">
        <span>- Input nhập liệu số:</span>
      </div>
      <div class="body-row">
        <inputnumber type="field-input" width="300px" />
      </div>

      <div class="body-row">
        <span>- Input nhập liệu có đuôi disabled:</span>
      </div>
      <div class="body-row">
        <fieldinput
          type="input-append"
          :placeholder="'Input Append'"
          :append="'VNĐ'"
          width="300px"
        />
      </div>
      <div class="body-row">
        <inputnumber type="input-append" :append="'VNĐ'" width="300px" />
      </div>
      <div class="body-row">
        <span>- Input nhập liệu có phần đầu disabled:</span>
      </div>
      <div class="body-row">
        <fieldinput
          type="input-prepend"
          :placeholder="'Input Prepend'"
          :prepend="'Http://'"
          width="300px"
        />
      </div>
      <div class="body-row">
        <inputnumber type="input-prepend" :prepend="'Người'" width="300px" />
      </div>
      <div class="body-row">
        <span>- Input nhập liệu họ và tên người, địa danh:</span>
      </div>
      <div class="body-row">
        <fieldinput
          type="field-input"
          :isUpercasefirst="true"
          :placeholder="'Họ và Tên'"
          width="300px"
        />
      </div>
    </div>
    <div class="row">
      <div class="title-row">Date time picker ngày tháng năm:</div>
      <div class="body-row">
        <datetime value="27-07-2020" :width="'150px'"></datetime>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Date time picker tháng năm:</div>
      <div class="body-row">
        <datetime type="month" :value="'7-2020'" :width="'130px'"></datetime>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Date time picker quí năm:</div>
      <div class="body-row">
        <datequarter :value="'2020-1'" @getValue="getQuarter"></datequarter>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Date time picker từ ngày đến ngày:</div>
      <div class="body-row">
        <daterange :value="demodaterang" @getValue="test"></daterange>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Select Dropdown (Combo Box):</div>
      <div class="body-row">
        <selectinput
          type="label-none"
          :filterable="true"
          :list="list"
          label="MONTH_NAME"
          field="MONTH"
          style="width: 200px"
        ></selectinput>
      </div>
    </div>
    <div class="row" style="width: 300px">
      <div class="title-row">Date time picker ngày tháng năm V2:</div>
      <div class="body-row">
        <datetime2
          value="27-07-2020"
          :width="'150px'"
          @getValue="getValueDate"
        ></datetime2>
      </div>
    </div>
    <div class="row" style="width: 300px">
      <div class="title-row">Date time picker tháng năm V2:</div>
      <div class="body-row">
        <datetime2
          type="month"
          value="07-2020"
          :width="'150px'"
          @getValue="getValueDate2"
        ></datetime2>
      </div>
    </div>
    <div class="row" style="width: 300px">
      <div class="title-row">Date time picker năm V2:</div>
      <div class="body-row">
        <datetime2
          type="year"
          value="2023"
          :width="'150px'"
          @getValue="getValueDate3"
        ></datetime2>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Demo Table:</div>
      <div class="body-table">
        <tbl
          :style="setStyle()"
          :listColumn="listColumn"
          :dataTable="dataTable"
          :haveCheck="true"
          :summary="true"
          :positionSummary="positionSummary"
          :havePagination="true"
          :total="numberOfRecord"
          :loading="loading"
          :tooltip="true"
          :listaction="listaction"
          @pagination="getFilter"
          @handleSelectionChange="handleSelectionChange"
          @handleSelectAll="handleSelectAll"
        />
      </div>

      <div class="body-table">
        <tbl
          :typeTable="'tableData'"
          :style="setStyle()"
          :listColumn="listColumn2"
          :dataTable="dataTable2"
          :summary="true"
          :positionSummary="positionSummary"
          :havePagination="true"
          :total="numberOfRecord"
          :loading="loading"
          :tooltip="true"
          :listaction="listaction"
          @pagination="getFilter"
          @handleSelectionChange="handleSelectionChange"
          @handleSelectAll="handleSelectAll"
          typepage="bảng tính thuế"
          :optionsSelect="optionsSelect"
          @row-change="handleRowChange"
        />
      </div>

      <div class="body-table-excel">
        <tbl typeTable='tableExcel'
          :loading="loading"
          :style="setStyle()"
          :dataTable="dataTable3"
          :listColumn="listColumn3"
          width="100%"
          :statusFlag="true"
          :statusExcelStatus="excelStatus()"
          
        >

        </tbl>
      </div>
    </div>
    <div class="row">
      <div class="title-row">Demo form(validate kèm dialog):</div>
      <div class="body-row">
        <btn
          class="mr-8 fl"
          :type="'btn-primary'"
          :handleClick="openDialogDemo"
          :name="'Open Dialog'"
        ></btn>
        <dialogdemo
          titleDialog="Demo Form Validate"
          width="500px"
          :dialogFormVisible="openDialog"
          :isWarnClose="isWarnClose"
          @close-dialog="closeDialog"
        >
          <template v-slot:body>
            <el-form :model="demoDialog" ref="demoDialog" class="demo-dynamic">
              <el-form-item
                prop="fullname"
                label="Họ và tên"
                :rules="{
                  required: true,
                  message: 'Bạn cần nhập thông tin này',
                  trigger: ['blur', 'change'],
                }"
                :show-message="hover"
              >
                <fieldinput
                  id="fullname"
                  @hover="hover = $event"
                  type="field-input"
                  :valueInput="demoDialog.fullname"
                  @value="getfullname"
                  :isUpercasefirst="true"
                  :placeholder="'Họ và tên'"
                />
              </el-form-item>
              <el-form-item
                prop="email"
                label="Email"
                :rules="[
                  {
                    required: true,
                    message: 'Bạn cần nhập thông tin này',
                    trigger: ['blur', 'change'],
                  },
                  {
                    type: 'email',
                    message: 'Bạn cần nhập đúng định dạng',
                    trigger: 'blur',
                  },
                ]"
                :show-message="hover2"
              >
                <fieldinput
                  id="email"
                  @hover="hover2 = $event"
                  type="field-input"
                  :valueInput="demoDialog.email"
                  @value="getEmail"
                  :placeholder="'Email'"
                />
              </el-form-item>
              <el-form-item prop="phone" label="Số Điện Thoại">
                <fieldinput
                  type="field-input"
                  :valueInput="demoDialog.phone"
                  @value="getPhone"
                  :placeholder="'Số điện thoại'"
                />
              </el-form-item>
            </el-form>
          </template>
          <template v-slot:footer>
            <btn
              :type="'btn-primary'"
              :name="'Submit'"
              :handleClick="sendSubmit"
            ></btn>
            <el-button
              style="margin-right: 10px"
              @click="resetForm('demoDialog')"
              >Reset</el-button
            >
          </template>
        </dialogdemo>
      </div>
    </div>
    <div class="row" style="width: 300px">
      <div class="title-row">Input nhập liệu số điện thoại:</div>
      <div class="body-row">
        <inputnumber type="input-phoneNumber" />
      </div>
    </div>

    <div>
      <tree :value="result" :arraySetting="arraySetting" :multiple="false">
      </tree>
    </div>
  </div>
</template>

<script>
import btn from "@/components/buttons";
import fieldinput from "@/components/inputs";
import inputnumber from "@/components/inputs/input-number";
import datetime from "@/components/date-time-pickers";
import datetime2 from "@/components/date-time-pickers/date-time-picker-v2";
import datequarter from "@/components/date-time-pickers/date-time-pickers-quarter";
import daterange from "@/components/date-time-pickers/date-time-pickers-range";
import selectinput from "@/components/select";
import tbl from "@/components/tables";
import dialogdemo from "@/components/dialog";

import treeDropdown from "@/components/tree-select/index.vue";

import excelStatus from "@/enums/excelStatus.js"
export default {
  name: "Setting",
  components: {
    tree: treeDropdown,
    btn,
    fieldinput,
    datetime,
    datequarter,
    daterange,
    inputnumber,
    selectinput,
    tbl,
    dialogdemo,
    datetime2,
  },
  data() {
    return {
      tableData4:[
  {
    BaseEntityState: 0,
    ImportValidState: 2,
    ImportValidError: [
      {
        ErrorMessage: "Mã số thuế không đúng định dạng",
        Code: 4,
        PropertyName: "TaxNo"
      }
    ],
    HasPropertyValueImport: true,
    DeclarationSettlementId: "4bf41f02-6b25-4c19-8bf4-d3ebef625592",
    DeclarationSettlementDetailId: "2e9be319-420c-4af0-9bcd-b8195b9dee76",
    DeclarationSettlementDetailType: 1,
    FullName: "Đặng Trọng Đức",
    TaxNo: "aaaaaaaaaa",
    CitizenIdentityNo: "001198004536",
    OrganizationUnitID: "00000000-0000-0000-0000-00000000000",
    OrganizationUnitName: "Tổng công ty",
    IsAuthorize: null,
    DependentNumber: 0,
    TotalReduction: 0,
    InsurranceReduction: 0,
    OtherRedution: 0,
    RetireFundRedution: 0,
    TotalTaxWithHeld: 10000000,
    TotalIncomeTaxable: 100000000,
    IsAuthorizeForeign: null,
    TotalIncomeTax: 0,
    TotalTaxPayable: 0,
    TaxOverpaid: 0,
    PayableTaxAmount: 0,
    IndividualResident: 1,
    IncomeTaxPurchasing: 0,
    Kktreduction: 0,
    CreatedBy: null,
    CreatedDate: null,
    ModifiedBy: null,
    ModifiedDate: null,
    EmployeeId: null,
    EmployeeCode: "NV1",
    IndentityType: '0ed62607-d95c-4d7a-ab56-72163b30d204',
    IndentityTypeName: "Chứng minh nhân dân",
    Status: 1,
    Sort: 0,
    DeclarationSettlement: null,
    IndividualResidentData: "Có",
    TotalIncomeTaxableData: "100.000.000"
  },
   {
    BaseEntityState: 0,
    ImportValidState: 2,
    ImportValidError: [
      {
        ErrorMessage: "Mã số thuế không đúng định dạng",
        Code: 4,
        PropertyName: "TaxNo"
      }
    ],
    HasPropertyValueImport: true,
    DeclarationSettlementId: "4bf41f02-6b25-4c19-8bf4-d3ebef625592",
    DeclarationSettlementDetailId: "2e9be319-420c-4af0-9bcd-b8195b9dee76",
    DeclarationSettlementDetailType: 1,
    FullName: "Nguyễn Sĩ Chuyền",
    TaxNo: "aaaaaaaaaa",
    CitizenIdentityNo: "001198004536",
    OrganizationUnitID: "00000000-0000-0000-0000-00000000000",
    OrganizationUnitName: "Phòng nghiên cứu",
    IsAuthorize: null,
    DependentNumber: 0,
    TotalReduction: 0,
    InsurranceReduction: 0,
    OtherRedution: 0,
    RetireFundRedution: 0,
    TotalTaxWithHeld: 10000000,
    TotalIncomeTaxable: 100000000,
    IsAuthorizeForeign: null,
    TotalIncomeTax: 0,
    TotalTaxPayable: 0,
    TaxOverpaid: 0,
    PayableTaxAmount: 0,
    IndividualResident: 1,
    IncomeTaxPurchasing: 0,
    Kktreduction: 0,
    CreatedBy: null,
    CreatedDate: null,
    ModifiedBy: null,
    ModifiedDate: null,
    EmployeeId: null,
    EmployeeCode: "NV2",
    IndentityType: '0ed62607-d95c-4d7a-ab56-72163b30d204',
    IndentityTypeName: "Hộ chiếu",
    Status: 1,
    Sort: 0,
    DeclarationSettlement: null,
    IndividualResidentData: "Có",
    TotalIncomeTaxableData: "100.000.000"
  },
   {
    BaseEntityState: 0,
    ImportValidState: 2,
    ImportValidError: [
      {
        ErrorMessage: "Mã số thuế không đúng định dạng",
        Code: 4,
        PropertyName: "FullName"
      }
    ],
    HasPropertyValueImport: true,
    DeclarationSettlementId: "4bf41f02-6b25-4c19-8bf4-d3ebef625592",
    DeclarationSettlementDetailId: "2e9be319-420c-4af0-9bcd-b8195b9dee76",
    DeclarationSettlementDetailType: 1,
    FullName: "Đặng Trọng Đức",
    TaxNo: "aaaaaaaaaa",
    CitizenIdentityNo: "001198004536",
    OrganizationUnitID: "00000000-0000-0000-0000-00000000000",
    OrganizationUnitName: "Tổng công ty",
    IsAuthorize: null,
    DependentNumber: 0,
    TotalReduction: 0,
    InsurranceReduction: 0,
    OtherRedution: 0,
    RetireFundRedution: 0,
    TotalTaxWithHeld: 10000000,
    TotalIncomeTaxable: 100000000,
    IsAuthorizeForeign: null,
    TotalIncomeTax: 0,
    TotalTaxPayable: 0,
    TaxOverpaid: 0,
    PayableTaxAmount: 0,
    IndividualResident: 1,
    IncomeTaxPurchasing: 0,
    Kktreduction: 0,
    CreatedBy: null,
    CreatedDate: null,
    ModifiedBy: null,
    ModifiedDate: null,
    EmployeeId: null,
    EmployeeCode: "NV1",
    IndentityType: '0ed62607-d95c-4d7a-ab56-72163b30d204',
    IndentityTypeName: "Chứng minh nhân dân",
    Status: 1,
    Sort: 0,
    DeclarationSettlement: null,
    IndividualResidentData: "Có",
    TotalIncomeTaxableData: "100.000.000"
  },
   {
    BaseEntityState: 0,
    ImportValidState: 2,
    ImportValidError: [
      {
        ErrorMessage: "Mã số thuế không đúng định dạng",
        Code: 4,
        PropertyName: "TaxNo"
      }
    ],
    HasPropertyValueImport: true,
    DeclarationSettlementId: "4bf41f02-6b25-4c19-8bf4-d3ebef625592",
    DeclarationSettlementDetailId: "2e9be319-420c-4af0-9bcd-b8195b9dee76",
    DeclarationSettlementDetailType: 1,
    FullName: "Đặng Trọng Đức",
    TaxNo: "aaaaaaaaaa",
    CitizenIdentityNo: "001198004536",
    OrganizationUnitID: "00000000-0000-0000-0000-00000000000",
    OrganizationUnitName: "Tổng công ty",
    IsAuthorize: null,
    DependentNumber: 0,
    TotalReduction: 0,
    InsurranceReduction: 0,
    OtherRedution: 0,
    RetireFundRedution: 0,
    TotalTaxWithHeld: 10000000,
    TotalIncomeTaxable: 100000000,
    IsAuthorizeForeign: null,
    TotalIncomeTax: 0,
    TotalTaxPayable: 0,
    TaxOverpaid: 0,
    PayableTaxAmount: 0,
    IndividualResident: 1,
    IncomeTaxPurchasing: 0,
    Kktreduction: 0,
    CreatedBy: null,
    CreatedDate: null,
    ModifiedBy: null,
    ModifiedDate: null,
    EmployeeId: null,
    EmployeeCode: "NV1",
    IndentityType: '0ed62607-d95c-4d7a-ab56-72163b30d204',
    IndentityTypeName: "Chứng minh nhân dân",
    Status: 1,
    Sort: 0,
    DeclarationSettlement: null,
    IndividualResidentData: "Có",
    TotalIncomeTaxableData: "100.000.000"
  },
   {
    BaseEntityState: 0,
    ImportValidState: 2,
    ImportValidError: [
      {
        ErrorMessage: "Mã số thuế không đúng định dạng",
        Code: 4,
        PropertyName: "TaxNo"
      }
    ],
    HasPropertyValueImport: true,
    DeclarationSettlementId: "4bf41f02-6b25-4c19-8bf4-d3ebef625592",
    DeclarationSettlementDetailId: "2e9be319-420c-4af0-9bcd-b8195b9dee76",
    DeclarationSettlementDetailType: 1,
    FullName: "Đặng Trọng Đức",
    TaxNo: "aaaaaaaaaa",
    CitizenIdentityNo: "001198004536",
    OrganizationUnitID: "00000000-0000-0000-0000-00000000000",
    OrganizationUnitName: "Tổng công ty",
    IsAuthorize: null,
    DependentNumber: 0,
    TotalReduction: 0,
    InsurranceReduction: 0,
    OtherRedution: 0,
    RetireFundRedution: 0,
    TotalTaxWithHeld: 10000000,
    TotalIncomeTaxable: 100000000,
    IsAuthorizeForeign: null,
    TotalIncomeTax: 0,
    TotalTaxPayable: 0,
    TaxOverpaid: 0,
    PayableTaxAmount: 0,
    IndividualResident: 1,
    IncomeTaxPurchasing: 0,
    Kktreduction: 0,
    CreatedBy: null,
    CreatedDate: null,
    ModifiedBy: null,
    ModifiedDate: null,
    EmployeeId: null,
    EmployeeCode: "NV1",
    IndentityType: '0ed62607-d95c-4d7a-ab56-72163b30d204',
    IndentityTypeName: "Chứng minh nhân dân",
    Status: 1,
    Sort: 0,
    DeclarationSettlement: null,
    IndividualResidentData: "Có",
    TotalIncomeTaxableData: "100.000.000",
  },
],
      listColumn4: [
        {
          label: "",
          propertyName: "ImportValidState",
          width: "50",
          type: "status-excel",
          align: "center",
        },
        {
          label: "STT",
          propertyName: "Sort",
          width: "",
          minWidth: "50",
          type: "",
          alignHeader: "left",
          align: "center",
        },
        {
          label: "Mã nhân viên",
          validate: true,
          prop: "EmployeeCode",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "130",
          type: "",
        },
        {
          label: "Họ và tên",
          propertyName: "FullName",
          width: "",
          validate: true,
          minWidth: "180",
          type: "",
        },
        {
          label: "Bộ phận/Phòng ban",
          propertyName: "OrganizationUnitName",
          width: "",
          validate: true,
          minWidth: "128",
          type: "",
        },
        {
          label: "Mã số thuế",
          propertyName: "TaxNo",
          width: "",
          validate: true,
          minWidth: "128",
          type: "",
        },
        {
          label: "Loại giấy tờ",
          propertyName: "IndentityTypeName",
          width: "",
          validate: true,
          minWidth: "190",
          type: "",
        },
        {
          label: "Số CMND /CCCD /Hộ chiếu",
          propertyName: "CitizenIdentityNo",
          width: "",
          validate: true,
          minWidth: "113",
          type: "",
        },
        {
          label: "Cá nhân cư trú",
          propertyName: "IndividualResidentData",
          width: "",
          validate: true,
          minWidth: "140",
          type: "",
        },
        {
          label: "Tổng thu nhập chịu thuế TNCN",
          propertyName: "TotalIncomeTaxableData",
          width: "",
          validate: true,
          minWidth: "205",
          type: "",
          alignHeader: "left",
          align: "right",
        },
      ],
      optionsSelect:[
        {
          value: 'Uỷ quyền' ,
          label: 'Ủy quyền'
        },
        {
          value: 'Không uỷ quyền' ,
          label: 'Không ủy quyền'
        }
      ],
      //tree
      result: [
        {
          OrganizationUnitID: 1,
          OrganizationUnitName: "Công ty Cha",
          OrganizationUnitChild: [
            {
              OrganizationUnitID: 2,
              OrganizationUnitName: "Công ty Công Một",
              OrganizationUnitChild: [
                {
                  OrganizationUnitID: 5,
                  OrganizationUnitName: 6,
                },
              ],
            },
            {
              OrganizationUnitID: 3,
              OrganizationUnitName: "Công ty Công hai",
            },
          ],
        },
      ],
      arraySetting: {
        id: "OrganizationUnitID",
        label: "OrganizationUnitName",
        children: "OrganizationUnitChild",
      },

      valTEST: false,
      isWarnClose: false,
      inputtest: 9876,
      check: true,
      hover: false,
      hover2: false,
      openDialog: false,
      email: "",
      input: "",
      radio: "1",
      checked: true,
      checked1: false,
      checked2: false,
      buttons: [],
      data: [
        "Tháng 1",
        "Tháng 2",
        "Tháng 3",
        "Tháng 4",
        "Tháng 5",
        "Tháng 6",
        "Tháng 7",
        "Tháng 8",
        "Tháng 9",
        "Tháng 10",
        "Tháng 11",
        "Tháng 12",
      ],
      dataDropdown: [
        { icon: "", name: "Option 1" },
        { icon: "", name: "Option 2" },
        { icon: "", name: "Option 3" },
      ],

      demodaterang: ["20/05/2020", "27/07/2020"],
      list: [
        {
          MONTH: 0,
          MONTH_NAME: "Cả năm",
        },
        {
          MONTH: 1,
          MONTH_NAME: "Tháng 1",
        },
        {
          MONTH: 2,
          MONTH_NAME: "Tháng 2",
        },
        {
          MONTH: 3,
          MONTH_NAME: "Tháng 3",
        },
      ],
      numberOfRecord: 100,
      listColumn: [
        {

        },
        {
          label: "Mã nhân viên",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "140",
          align: "left",
          type: "",
          prop: "EmployeeCode",
          sortable: true,
        },
        {
          label: "Họ và tên",
          propertyName: "FullName",
          width: "",
          minWidth: "180",
          align: "left",
          type: "",
          prop: "FullName",
          sortable: true,
        },
        {
          label: "Giới tính",
          propertyName: "Gender",
          width: "200",
          minWidth: "180",
          align: "left",
          type: "",
          prop: "Gender",
          sortable: true,
        },
        {
          label: "Công ty",
          propertyName: "Company",
          width: "250",
          minWidth: "180",
          align: "left",
          type: "",
          prop: "Company",
          sortable: true,
        },
        {
          label: "Email",
          propertyName: "Email",
          width: "250",
          minWidth: "180",
          align: "left",
          type: "",
          prop: "Email",
          sortable: true,
        },
      ],
      dataTable: [
        {
          EmployeeCode: "MF 526",
          FullName: "Cao Văn Cường",
          Gender: "Name",
          Company: "MISA",
          Email: "caocuong61299@gmail.com",
        },
        {
          EmployeeCode: "MF 527",
          FullName: "Nguyễn Ngọc Phong",
          Gender: "Name",
          Company: "MISA",
          Email: "nnphong@gmail.com",
        },
        {
          EmployeeCode: "MF 528",
          FullName: "Trần Viết Phi",
          Gender: "Name",
          Company: "MISA",
          Email: "tvphi@gmail.com",
        },
        {
          EmployeeCode: "MF 529",
          FullName: "Nguyễn Tuấn Ngọc",
          Gender: "Name",
          Company: "MISA",
          Email: "ntngoc@gmail.com",
        },
        {
          EmployeeCode: "MF 530",
          FullName: "Nguyễn Văn Bình",
          Gender: "Name",
          Company: "MISA",
          Email: "nvbinh@gmail.com",
        },
      ],

      listColumn2: [
        {
          label: "Mã nhân viên",
          propertyName: "EmployeeCode",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "EmployeeCode",
          sortable: false,
        },
        {
          label: "Họ và tên",
          align: "center",
          sortable: false,
          width: "",
          minWidth: "140",
          type: "",
          group: [
            {
              label: "Công ty",
              propertyName: "Select",
              width: "180",
              minWidth: "180",
              align: "center",
              type: "dropdown",
              prop: "Select",
              sortable: false,
            },
            {
              label: "Giới tính",
              align: "center",
              sortable: false,
              group: [
                {
                  label: "Number",
                  propertyName: "Gender",
                  width: "250",
                  minWidth: "180",
                  align: "center",
                  type: "decimal",
                  prop: "Gender",
                  sortable: false,
                },
                {
                  label: "Công ty2",
                  propertyName: "Company",
                  width: "250",
                  minWidth: "180",
                  align: "center",
                  type: "",
                  prop: "Company",
                  sortable: false,
                }
                
              ],
            },
            {
              label: "Công ty",
              propertyName: "Select",
              width: "180",
              minWidth: "180",
              align: "center",
              type: "dropdown",
              prop: "Select",
              sortable: false,
            },
          ],
        },
        {
          label: "Email",
          propertyName: "Email",
          width: "250",
          minWidth: "180",
          align: "left",
          type: "",
          prop: "Email",
          sortable: false,
        },
      ],


      dataTable2: [
        { 
          EmployeeCode: "[01]",
          FullName: "[02]",
          Gender: "[03]",
          Company: "[04]",
          Email: "[05]",
          Select: "[06]",
          Header: true,
        },
        {
          EmployeeCode: "MF 526",
          FullName: "Cao Văn Cường",
          Gender: 1000000,
          Company: 1000000,
          Email: "caocuong61299@gmail.com",
          Select: 'Ủy quyền'
        },
        {
          EmployeeCode: "MF 527",
          FullName: "Nguyễn Ngọc Phong",
          Gender: 100,
          Company: 100,
          Email: "nnphong@gmail.com",
          Select: 'Không ủy quyền'
        },
        {
          EmployeeCode: "MF 528",
          FullName: "Trần Viết Phi",
          Gender: 100,
          Company: 100,
          Email: "tvphi@gmail.com",
          Select: 'Ủy quyền'
        },
        {
          EmployeeCode: "MF 529",
          FullName: "Nguyễn Tuấn Ngọc",
          Gender: 100,
          Company: 100,
          Email: "ntngoc@gmail.com",
          Select: 'Ủy quyền'
        },
        {
          EmployeeCode: "MF 530",
          FullName: "Nguyễn Văn Bình",
          Gender: 100,
          Company: 100,
          Email: "nvbinh@gmail.com",
          Select: 'Ủy quyền'
        },
      ],

      listColumn3:[
        {
          label: "",
          propertyName: "ImportValidState",
          width:"80",
          type:"status-excel"

        },
        {
          label:"STT",
          propertyName:"Sort",
          
        },
        {
          label: "Mã nhân viên",
          propertyName:"EmployeeCode",
          type: "",
        },
        {
          label: "Họ và tên",
          propertyName:"FullName",
          type: "",
        },
        {
          label: "Mã số thuế",
          propertyName:"TaxNo",
          type: "",
        },
      ],
      dataTable3:[
        {
          ImportValidState:1,
          EmployeeCode: "B06-0536",
          FullName: "Nguyễn Văn Bình",
          TaxNo: "197865245515"
        },
        {
          ImportValidState:3,
          EmployeeCode: "B06-0536",
          FullName: "Nguyễn Văn Bình",
          TaxNo: "197865245515"
        }
      ],

      positionSummary: {
        Summary: [
          {
            PropertyName: "",
            PropertyIndex: 0,
          },
          {
            PropertyName: "Gender",
            PropertyIndex: 3,
          },
          {
            PropertyName: "Company",
            PropertyIndex: 4,
          },

        ],
      },
      listaction: [
        {
          value: 0,
          name: "Nhân bản",
        },
        {
          value: 1,
          name: "Sửa",
        },
        {
          value: 2,
          name: "Xóa",
        },
      ],
      loading: false,
      demoDialog: {
        email: "",
        fullname: "",
        phone: "",
      },
      numbertest: 0,
    };
  },
  methods: {
    handleRowChange(){
    },

    getTEST() {
    },

    resetForm(formName) {
      this.$refs[formName].resetFields();
    },

    sendSubmit() {
      this.submitForm("demoDialog");
    },

    submitForm(formName) {
      this.$refs[formName].validate((valid, index) => {
        if (Object.keys(index)[0] != null) {
          document.getElementById([`${Object.keys(index)[0]}`]).focus();
        }
        if (valid) {
          this.$_Notification.success("Thành công", "Thử nghiệm thành công");
          this.closeDialog();
        } else {
          return false;
        }
      });
    },
    getValueDate() {
    },

    getValueDate2() {
    },

    getValueDate3() {
    },

    getQuarter() {
    },

    getPhone(val) {
      this.demoDialog.phone = val;
      if (val != "") this.isWarnClose = true;
      else this.isWarnClose = false;
    },

    getfullname(val) {
      this.demoDialog.fullname = val;
      if (val != "") this.isWarnClose = true;
      else this.isWarnClose = false;
    },

    getEmail(val) {
      this.demoDialog.email = val;
      if (val != "") this.isWarnClose = true;
      else this.isWarnClose = false;
    },

    openDialogDemo() {
      this.openDialog = true;
    },

    closeDialog() {
      this.openDialog = false;
      this.isWarnClose = false;
      this.resetForm("demoDialog");
    },

    handleSelectAll() {
    },

    handleSelectionChange() {
    },

    getFilter() {
    },

    test() {
    },

    setStyle() {
      return { height: "600px" };
    },

    callPopupsuccess() {
      this.buttons = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];
      this.$_Popup.success(
        "Chúc mừng bạn",
        "Khởi tạo đơn vị thành công",
        this.buttons
      );
    },

    callPopupwarn() {
      this.buttons = [
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];
      this.$_Popup.warn(
        "Cảnh báo",
        'Bạn có chắc chắn muốn đóng form nhập "Thêm mới người nộp thuế hay không" ',
        this.buttons
      );
    },

    callPopupdelete() {
      this.buttons = [
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
        {
          text: "Có",
          class: "btn-delete",
          callback: () => {},
        },
      ];
      this.$_Popup.delete(
        "Cảnh báo",
        "Bạn có chắc chắn muốn xóa người nộp thuế <strong>Cao Văn Cường</strong>",
        this.buttons
      );
    },

    callPopupinfo() {
      this.buttons = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];
      this.$_Popup.info(
        "Thông báo",
        "Tính năng đang được bảo trì",
        this.buttons
      );
    },

    callPopuploading() {
      this.buttons = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];
      this.$_Popup.loading(
        "",
        "Đang thực hiện ký tờ khai Tháng 1 Năm 2020.<br/> Vui lòng đợi",
        this.buttons
      );
    },

    callNotificationsuccess() {
      this.$_Notification.success(
        "Thành công",
        "Bạn đã thêm thành công người nộp thuế"
      );
    },

    callNotificationwarning() {
      this.$_Notification.warning(
        "Cảnh báo",
        "Bạn chưa nhập đủ các trường thông tin"
      );
    },

    callNotificationerror() {
      this.$_Notification.error("Thất bại", "Thêm người nộp thuế thất bại");
    },

    callNotificationinfo() {
      this.$_Notification.info("Thông báo", "Tính năng đang được bảo trì");
    },

    excelStatus(){
      return excelStatus.EXCEL_STATUS;
    }
  },
  watch: {},
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
.styleguideline {
  position: absolute;
  top: 0px;
  left: 0px;
  bottom: 0px;
  right: 0px;
  background-color: $white;
  border: 1px solid $grey;
  overflow: auto;
}
.row {
  width: 100%;
  height: auto;
  .title-row {
    width: 100%;
    height: auto;
    margin: 10px;
  }
  .body-row {
    margin: 10px 10px 10px;
    width: 100%;
    height: 40px;
    .fl {
      float: left;
    }
  }
  .body-table {
    padding: 10px 10px 10px 10px;
    width: 100%;
    height: auto;
    .fl {
      float: left;
    }
  }
}
.mr-8 {
  margin-right: 8px;
}
.demo-dynamic {
  .el-form-item__error {
    position: absolute;
    height: auto;
    left: 30%;
    transform: translateY(0%);
    max-width: 200px;
    background-color: $light-red;
    color: $white;
    border-radius: 5px;
    text-align: center;
    padding: 10px 24px;
    word-break: break-word;
  }
}
</style>
