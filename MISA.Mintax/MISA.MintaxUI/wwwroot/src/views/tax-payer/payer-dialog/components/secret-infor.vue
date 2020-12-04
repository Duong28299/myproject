<template>
    <el-form>
      <el-row :gutter="40" class="container-content">
        <!--cột 1-->
        <el-col :lg="8" :md="8" :sm="24" :span="8">
          <!--Label Thông tin chung-->
          <el-row>
            <el-col :span="24" class="title-bottom-red ">
              <div class="main-title ">THÔNG TIN CHUNG</div>
              <div class="red-line" />
            </el-col>
          </el-row>
          <!--mã nhân viên và họ tên-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item label="Mã nhân viên" prop="EmployeeCode">
                    <input-field
                      type="field-input"
                      @value="getValueEmployeeCode"
                      :valueInput="formData.EmployeeCode"
                      id="EmployeeCode"
                      width="100%"
                      placeholder="Nhập mã nhân viên"
                      :maxlength="25"
                      :submitFuntion="submitFuntion"
                    />
                  </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class">
              <el-form-item
                label="Họ và tên"
                prop="FullName"
                :show-message="validate.TaxNo"
              >
                <input-field
                  type="field-input"
                  @value="getValueFullName"
                  :valueInput="formData.FullName"
                  id="FullName"
                  width="100%"
                  placeholder="Nhập họ và tên"
                  :maxlength="125"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <!-- Ngày sinh và giới tính -->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item>
                <el-form-item label="Ngày sinh" prop="DateOfBirth">
                  <date-picker
                    id="DateOfBirth"
                    type="date"
                    @hover="validate.DateOfBirth = $event"
                    width="100%"
                    placeholder="_ _ /_ _ /_ _ _ _"
                  ></date-picker>
                </el-form-item>
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class">
              <el-form-item
                label="Giới tính"
                prop="Gender"
                class="gender-class"
              >
                <el-radio-group>
                  <el-radio :label="0">Nam</el-radio>
                  <el-radio :label="1">Nữ</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <!-- Loại giẩy tờ và số CMND/CCCD/Hộ chiếu -->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <div class="label-combo-class" for="indentityType">Loại giấy tờ</div>
              <combo-box
                      selectID="indentityType"
                      type="label-none-v2"
                      placeholder="Chọn loại giấy tờ"
                      :list="listIndentity"
                      label="DBOptionName"
                      field="DBOptionValue"
                      :defaultString="IndentityType"
                      @value="getValueIndentityType"
                    />
               <!-- <el-form-item label="Loại giấy tờ" prop="IndentityType">
                    
                </el-form-item> -->
            </el-col>

            <el-col :span="12" class="col-class">
              <el-form-item
                :label="'CMTND/CCCD/Hộ chiếu'"
                prop="CitizenIdentityNo"
                :show-message="validate.CitizenIdentityNo"
              >
                <input-field
                  id="CitizenIdentityNo"
                  @value="getCitizenIdentityNo"
                  :valueInput="formData.CitizenIdentityNo"
                  @hover="validate.CitizenIdentityNo = $event"
                  type="field-input"
                  number
                  :isUpercasefirst="true"
                  width="100%"
                  placeholder="Nhập CMND/CCCD/Hộ chiếu"
                  :maxlength="50"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <!--Quốc tịch-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item label="Quốc tịch" prop="NationalityID">
                <combo-box
                  id="NationalityID"
                  type="label-none"
                  :filterable="true"
                  placeholder="Chọn quốc tịch"
                  :list="listNationality"
                  label="NationalityName"
                  field="NationalityID"
                  :getInfo="getPlaceOfIssueByNationalID"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class"></el-col>
          </el-row>

          <!-- Ngày cấp và nơi cấp -->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item>
                <el-form-item label="Ngày cấp" prop="CitizenIdentityDate">
                  <date-picker
                    id="CitizenIdentityDate"
                    type="date"
                    @hover="validate.CitizenIdentityDate = $event"
                    width="100%"
                    placeholder="_ _ /_ _ /_ _ _ _"
                  ></date-picker
                ></el-form-item>
              </el-form-item>
            </el-col>

            <el-col :span="12" class="col-class">
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
                  :list="listPlaceOfIssues"
                  @hover="validate.CitizenIdentityPlaceID = $event"
                  label="PlaceOfIssueName"
                  field="PlaceOfIssueID"
                  clearable
                />
              </el-form-item>
            </el-col>
          </el-row>

          <!--Bộ phận phòng ban và vị trí chức vụ-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item
                label="Bộ phận/Phòng ban"
                prop="OrganizationUnit"
              >
                <input-field
                  type="field-input"
                  id="OrganizationUnit"
                  @value="getValueOrganizationUnit"
                  :valueInput="formData.OrganizationUnit"
                  width="100%"
                  placeholder="Nhập bộ phận/Phòng ban"
                  :maxlength="25"
                />
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class">
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

          <!--Mã số thuế-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item
                label="Mã số thuế"
                prop="TaxNo"
                :show-message="validate.TaxNo"
              >
                <input-field
                  type="field-input"
                  id="TaxNo"
                  number
                  width="100%"
                  placeholder="Nhập mã số thuế"
                  :maxlength="25"
                  @hover="validate.TaxNo = $event"
                  @value="getValueTaxNo"
                  :valueInput="formData.TaxNo"
                />
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class"></el-col>
          </el-row>

          <!-- điện thoại và email -->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item label="Điện thoại" prop="Phone">
                <input-field
                  type="field-input"
                  id="Phone"
                  width="100%"
                  number
                  @value="getValuePhone"
                  :valueInput="formData.Phone"
                  placeholder="Nhập số điện thoại"
                  :maxlength="50"
                />
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class">
              <el-form-item label="Email" prop="Email">
                <input-field
                  type="field-input"
                  id="Email"
                  @value="getValueEmail"
                  :valueInput="formData.Email"
                  width="100%"
                  placeholder="Nhập email"
                  :maxlength="100"
                />
              </el-form-item>
            </el-col>
          </el-row>

          <!--Trạng thái làm việc-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <div class="label-combo-class" for="indentityType">Trạng thái làm việc</div>
                <combo-box
                  selectID="WorkStatus"
                  type="label-none-v2"
                  placeholder="Chọn trạng thái làm việc"
                  :list="listStatus"
                  label="DBOptionName"
                  field="DBOptionValue"
                  :defaultString="WorkStatus"
                  @value="getValueWorkStatus"
                />
            </el-col>
            <el-col :span="12" class="col-class"></el-col>
          </el-row>

          <!--Làm việc từ ngày và làm việc đến ngày-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item>
                <el-form-item label="Làm việc từ ngày" prop="WorkFromDate">
                  <date-picker
                    id="WorkFromDate"
                    type="date"
                    width="100%"
                    placeholder="_ _ /_ _ /_ _ _ _"
                  ></date-picker>
                </el-form-item>
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class">
              <el-form-item>
                <el-form-item label="Làm việc đến ngày" prop="WorkToDate">
                  <date-picker
                    id="WorkToDate"
                    type="date"
                    width="100%"
                    placeholder="_ _ /_ _ /_ _ _ _"
                  ></date-picker>
                </el-form-item>
              </el-form-item>
            </el-col>
          </el-row>
        </el-col>
        <!--end cột 1-->

        <!--cột 2-->
        <el-col :lg="8" :md="8" :sm="24" :span="8">
          <!--Label địa chỉ theo hộ khẩu-->
          <el-row>
            <el-col :span="24" class="title-bottom-red">
              <div class="main-title">ĐỊA CHỈ THEO HỘ KHẨU</div>
              <div class="red-line" />
            </el-col>
          </el-row>

          <!-- Quốc gia và tỉnh thành phố -->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item
                label="Quốc gia"
                prop="National"
                :show-message="validate.CitizenIdentityPlaceID"
              >
                <combo-box
                  id="CitizenIdentityPlaceID"
                  type="label-none"
                  :filterable="true"
                  placeholder="Chọn quốc gia"
                  :list="listNationality"
                  @hover="validate.CitizenIdentityPlaceID = $event"
                  label="NationnalName"
                  field="NationalID"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class">
              <el-form-item
                label="Tỉnh/Thành phố"
                prop="Province"
                :show-message="validate.CitizenIdentityPlaceID"
              >
                <combo-box
                  id="Province"
                  type="label-none"
                  :filterable="true"
                  placeholder="Chọn/Nhập thành phố"
                  :list="listPlaceOfIssues"
                  @hover="validate.CitizenIdentityPlaceID = $event"
                  label="ProvinceName"
                  field="ProvinceID"
                  clearable
                />
              </el-form-item>
            </el-col>
          </el-row>

          <!-- Quận/huyện và xã/phường -->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item
                label="Quận/Huyện"
                prop="DistrictIDByHousehold"
                :show-message="validate.CitizenIdentityPlaceID"
              >
                <combo-box
                  id="DistrictIDByHousehold"
                  type="label-none"
                  :filterable="true"
                  placeholder="Chọn/Nhập quận, huyện"
                  :list="DistrictIDByHousehold"
                  @hover="validate.CitizenIdentityPlaceID = $event"
                  label="DistrictName"
                  field="DistrictID"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class">
              <el-form-item
                label="Xã/Phường"
                prop="WardIDByHousehold"
                :show-message="validate.CitizenIdentityPlaceID"
              >
                <combo-box
                  id="WardIDByHousehold"
                  type="label-none"
                  :filterable="true"
                  placeholder="Chọn/Nhập xã, phường"
                  :list="listPlaceOfIssues"
                  @hover="validate.CitizenIdentityPlaceID = $event"
                  label="WardIDByHouseholdName"
                  field="WardIDByHouseholdID"
                  clearable
                />
              </el-form-item>
            </el-col>
          </el-row>
          <!--Số nhà/Đường phố, thôn xóm-->
          <el-row :gutter="8">
            <el-col :span="24" class="col-class">
              <el-form-item
                label="Số nhà/Đường phố, thôn xóm"
                prop="AddressByResident"
              >
                <input-field
                  type="field-input"
                  id="AddressByHousehold"
                  @value="getValueAddressByHousehold"
                  :valueInput="formData.AddressByHousehold"
                  width="100%"
                  placeholder="Số nhà/Đường phố, thôn xóm"
                  :maxlength="255"
                />
              </el-form-item>
            </el-col>
          </el-row>

          <el-row>
            <el-col :span="24">
              <!--Label địa chỉ cư trú-->
              <el-row>
                <el-col :span="24" class="mt-3 title-bottom-red">
                  <div class="main-title ">ĐỊA CHỈ CƯ TRÚ</div>
                  <div class="red-line " />
                </el-col>
              </el-row>

              <!-- Quốc gia và tỉnh thành phố -->
              <el-row :gutter="8">
                <el-col :span="12" class="col-class">
                  <el-form-item
                    label="Quốc gia"
                    prop="National"
                    :show-message="validate.CitizenIdentityPlaceID"
                  >
                    <combo-box
                      id="CitizenIdentityPlaceID"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn quốc gia"
                      :list="listNationality"
                      @hover="validate.CitizenIdentityPlaceID = $event"
                      label="NationnalName"
                      field="NationalID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12" class="col-class">
                  <el-form-item
                    label="Tỉnh/Thành phố"
                    prop="Province"
                    :show-message="validate.CitizenIdentityPlaceID"
                  >
                    <combo-box
                      id="Province"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn/Nhập thành phố"
                      :list="listPlaceOfIssues"
                      @hover="validate.CitizenIdentityPlaceID = $event"
                      label="ProvinceName"
                      field="ProvinceID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- Quận/huyện và xã/phường -->
              <el-row :gutter="8">
                <el-col :span="12" class="col-class">
                  <el-form-item
                    label="Quận/Huyện"
                    prop="DistrictIDByHousehold"
                    :show-message="validate.CitizenIdentityPlaceID"
                  >
                    <combo-box
                      id="DistrictIDByHousehold"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn/Nhập quận, huyện"
                      :list="listNationality"
                      @hover="validate.CitizenIdentityPlaceID = $event"
                      label="DistrictName"
                      field="DistrictID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12" class="col-class">
                  <el-form-item
                    label="Xã/Phường"
                    prop="WardIDByHousehold"
                    :show-message="validate.CitizenIdentityPlaceID"
                  >
                    <combo-box
                      id="WardIDByHousehold"
                      type="label-none"
                      :filterable="true"
                      placeholder="Chọn/Nhập xã, phường"
                      :list="listWards"
                      @hover="validate.CitizenIdentityPlaceID = $event"
                      label="WardIDByHouseholdName"
                      field="WardIDByHouseholdID"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <!--Số nhà/Đường phố, thôn xóm-->
              <el-row :gutter="8">
                <el-col :span="24" class="col-class">
                  <el-form-item
                    label="Số nhà/Đường phố, thôn xóm"
                    prop="AddressByResident"
                  >
                    <input-field
                      type="field-input"
                      id="AddressByResident"
                      @value="getValueAddressByAddressByResident"
                      :valueInput="formData.AddressByAddressByResident"
                      width="100%"
                      placeholder="Số nhà/Đường phố, thôn xóm"
                      :maxlength="255"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-col>
          </el-row>
        </el-col>
        <!--end cột 2-->

        <!--cột 3-->
        <el-col :lg="8" :md="8" :sm="24" :span="8">
          <!--Label thông tin ngân hàng-->
          <el-row>
            <el-col :span="24" class="title-bottom-red ">
              <div class="main-title ">THÔNG TIN NGÂN HÀNG</div>
              <div class="red-line" />
            </el-col>
          </el-row>
          <!---->
          <el-row :gutter="8">
            <el-col :span="24" class="col-class">
              <el-form-item
                label="Tên ngân hàng"
                prop="Bank"
                :show-message="validate.CitizenIdentityPlaceID"
              >
                <combo-box
                  id="Bank"
                  type="label-none"
                  :filterable="true"
                  placeholder="Chọn/Nhập tên ngân hàng"
                  :list="listBank"
                  @hover="validate.CitizenIdentityPlaceID = $event"
                  label="BankName"
                  field="BankID"
                  clearable
                />
              </el-form-item>
            </el-col>
          </el-row>
          <!--Chi nhánh ngân hàng-->
          <el-row :gutter="8">
            <el-col :span="24" class="col-class">
              <el-form-item label="Chi nhánh ngân hàng" prop="BankBranch">
                <input-field
                  type="field-input"
                  id="BankBranch"
                  @value="getValueBankBranch"
                  :valueInput="formData.BankBranch"
                  width="100%"
                  placeholder="Nhập tên chi nhánh ngân hàng"
                  :maxlength="255"
                />
              </el-form-item>
            </el-col>
          </el-row>

          <!--Số tài khoản và chủ tài khoản-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <el-form-item label="Số tài khoản" prop="BankNo">
                <input-field
                  type="field-input"
                  id="BankNo"
                  @value="getValueBankNo"
                  :valueInput="formData.BankNo"
                  width="100%"
                  number
                  placeholder="Nhập số tài khoản ngân hàng"
                  :maxlength="255"
                />
              </el-form-item>
            </el-col>
            <el-col :span="12" class="col-class">
              <el-form-item label="Chủ tài khoản" prop="BankAccountName">
                <input-field
                  type="field-input"
                  id="BankAccountName"
                  @value="getValueBankAccountName"
                  :valueInput="formData.BankAccountName"
                  width="100%"
                  placeholder="Nhập tên chủ tài khoản"
                  :maxlength="255"
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-col>
        <!--end cột 3-->
      </el-row>
    </el-form>
</template>

<script>
import fieldinput from "@/components/inputs";
import ComboBox from "@/components/select";
import DatePicker from "@/components/date-time-pickers/date-time-picker-v2";
import dictionaryService from "@/api/etax/category-dictionary.js";
import NationalID from "@/enums/nationalID.js";
//import Dropdown from "@/components/select";
export default {
  components: {
    "input-field": fieldinput,
    "combo-box": ComboBox,
    "date-picker": DatePicker,
    // "input-dropdown": Dropdown,
  },
  props:{
    payerDetail:{
      type:Object,
      default:()=>{}
    },
    formMode:{
      type:Number,
      default:0,
    }
  },
  data() {
    return {
      //Loại giấy tờ
      IndentityType: "",
      // danh sách loại giấy tờ
      listIndentity: [
        {
          DBOptionName:"giấy khai sinh",
          DBOptionValue:"1"
        },
        {
          DBOptionName:"sổ bảo hiểm",
          DBOptionValue:"2"
        },
      ],
      //danh sách vị trí/ chức vụ
      listPosition: [],
      //danh sách quốc tịch
      listNationality: [],
      // danh sách nơi cấp loại giấy tờ
      listPlaceOfIssues: [],
      //danh sách trạng thái làm việc
      listStatus: [
        {
          DBOptionName:"đang làm việc",
          DBOptionValue:"1"
        },
        {
          DBOptionName:"đã nghỉ việc",
          DBOptionValue:"2"
        },
      ],
      //danh sách ngân hàng
      listBank: [],
      //danh sách quận huyện
      DistrictIDByHousehold: [],
      //danh sách xã phường
      listWards: [],
      //trạng thái làm việc
      WorkStatus:"",
      validate: {
        DeclarationSettlementName: false,
        SettlementYear: false,
        StarSettlementMonth: false,
        FinishSettlementMonth: false,
      },
      formData: {
        EmployeeCode:"",
        FullName:"",
        CitizenIdentityNo:null,
        OrganizationUnit:"",
        TaxNo: null,
        Phone:"",
        Email:"",
        AddressByHousehold:"",
        AddressByAddressByResident:"",
        BankNo:"",
        BankAccountName:"",
        BankBranch:"",
        EmployeeStatus:"",
        PositionID:""
      },
    };
  },
  methods: {
    /**
     * Lấy thông tin bộ phận phòng ban
     * nvnguyen 19/11/2020
     */
    getValueOrganizationUnit(val){
      this.formData.OrganizationUnit =val;
    },
    /**
     * Lấy thông tin CMTND/CCCD/
     * nvnguyen 19/11/2020
     */
    getCitizenIdentityNo(val){
      this.formData.CitizenIdentityNo = val;
    },
    /**
     * Lấy thông tin mã số thuế
     * nvnguyen 19/11/2020
     */
    getValueTaxNo(val){
      this.formData.TaxNo = val;
    },
    /**
     * Lấy số điện thoại
     * nvnguyen 19/11/2020
     */
    getValuePhone(val){
      this.formData.Phone =val;
    },
    /**
     * Lấy gmail
     * nvnguyen 19/11/2020
     */
    getValueEmail(val){
      this.formData.Email =val;
    },
    /**
     * lấy số nhà thôn xóm theo hộ khẩu
     * nvnguyen 19/11/2020
     */
    getValueAddressByHousehold(val){
      this.formData.AddressByHousehold =val;
    },
    /**
     * lấy số nhà thôn xóm theo địa chỉ cư trú
     * nvnguyen 19/11/2020
     */
    getValueAddressByAddressByResident(val){
      this.formData.AddressByAddressByResident =val;
    },
    /**
     * Lấy danh sách trạng thái 17/11/2020
     * todo nvnguyen
     */
    getValueStatus() {},

    //lấy mã nhân viên
    getValueEmployeeCode(val) {
      this.formData.EmployeeCode = val;
    },

    //Lấy tên nhân viên
    getValueFullName(val) {
      this.formData.FullName = val;
      this.formData.BankAccountName = val;
    },

    /**
     * Lấy chủ tài khoản
     * nvnguyen 19/11/2020
     */
    getValueBankAccountName(val){
      this.formData.BankAccountName = val;
    },
    /**
     * Lấy chi nhánh ngân hàng
     * nvnguyen 19/11/2020
     */
    getValueBankBranch(val){
      this.formData.BankBranch =val;
    },
    /**
     * lấy số tk ngân hàng
     * nvnguyen 19/11/2020
     */
    getValueBankNo(val){
      this.formData.BankNo = val;
    },
    //lấy trạng thái làm việc
    getValueWorkStatus(){},

    //lấy loại giấy tờ
    getValueIndentityType(){},
    //
    submitFuntion(){
    },
    /**
     * get list nơi cấp theo nationalID
     * created by nvnguyen 07/11/2020
     */
    async getPlaceOfIssueByNationalID(event){
      if(event != NationalID.NATIONALID[0].value){
        var res = await dictionaryService.getPlaceOfIssuesByNationalID(event);
        if (typeof res.data != "undefined" && res.data.Success){
          this.formData.CitizenIdentityPlaceID = null;
          this.listPlaceOfIssuesByNationID = res.data.Data;
          this.listPlaceOfIssues =  this.listPlaceOfIssuesByNationID.Data;
        }
      }else{
        // this.getPlaceOfIssueID();
      }
    },

    /**
     * binding giá trị lên form khi ở mode sửa
     * nvnguyen 20/11/2020
     */
  },

  watch: {
    payerDetail(val){
      this.formData = val;
    }
  },
  mounted() {
    // this.formData = this.$store.getters.taxPayerInfor;
    // this.formData = this.payerDetail;
    // this.formData.EmployeeCode = this.payerDetail.EmployeeCode;
    // this.formData.FullName = this.payerDetail.FullName;
    },
  created() {
    
  },
};
</script>
<style lang="scss" scoped>
@import "@/styles/variables.scss";
.label-infor {
  color: $primary;
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
.col-class {
  .el-form-item {
    margin-bottom: 0;
    .el-form-item__content {
      // margin-top: 30px !important;
    }
  }
  .el-form-item .el-form-item__label {
    font-size: 13px !important;
  }
}
.mt-3 {
  margin-top: 16px;
}
.date-time {
  top: 36px;
}

.label-combo-class{
  padding-top: 14px;
  padding-bottom: 5px;
  font-family: $_fontregular;
  font-size: 13px;
  font-weight: bold;
}
.gender-class{
  display: inline-block;
  padding-top:2px;
  padding-bottom:2px;
}

  
.container-content{
  margin-bottom: 24px !important;
}
</style>