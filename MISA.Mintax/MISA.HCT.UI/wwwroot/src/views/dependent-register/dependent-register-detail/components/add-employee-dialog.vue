<template>
  <div>
    <!-- Dialog thêm người phụ thuộc -->
    <add-dialog
      :id="dialogid"
      :dialogFormVisible="visiableDL"
      @close-dialog="getClose"
      :fullscreen="true"
    >
      <template v-slot:body>
        <div class="company-container" v-if="!tableTab">
          <div class="add-employee-tab">
            <span class="title">{{ title }}</span>
            <TwoStepProgress
              class="tab-bar"
              id="two-step-progress"
              step1Title="Người nộp thuế"
              step2Title="Người phụ thuộc"
              step3Title="Thành công"
              :isNext="isNextProgress"
            ></TwoStepProgress>
          </div>

          <div class="add-employee-main">
            <!-- Tab 1 -->
            <el-form
              ref="FormEmployee"
              :model="FormEmployee"
              v-show="employeeTab"
              label-width="120px"
              label-position="top"
            >
              <el-row :gutter="24">
                <el-col :lg="24" :md="24" :sm="24">
                  <el-row :gutter="24">
                    <el-col :lg="24" :md="24" :sm="24">
                      <el-form-item label="Mã nhân viên" prop="EmployeeCode">
                        <input-field
                          type="field-input"
                          id="EmployeeCode"
                          placeholder="Nhập mã nhân viên"
                          :valueInput="FormEmployee.EmployeeCode"
                          @value="FormEmployee.EmployeeCode = $event"
                          @blur="checkEmployeeCode"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-form-item
                    label="Họ và tên"
                    prop="FullName"
                    :rules="[
                      {
                        required: true,
                        message: 'Tên Nhân viên không được bỏ trống.',
                        trigger: ['blur', 'change'],
                      },
                    ]"
                    :show-message="validate.EmployeeName"
                  >
                    <input-field
                      type="field-input"
                      id="FullName"
                      onlyText
                      @hover="validate.EmployeeName = $event"
                      placeholder="Nhập họ và tên"
                      :valueInput="FormEmployee.FullName"
                      @value="FormEmployee.FullName = $event"
                      :isUpercasefirst="true"
                    />
                  </el-form-item>
                  <el-row :gutter="16">
                    <el-col :lg="12" :md="12" :sm="24">
                      <el-form-item label="Bộ phận/Phòng ban">
                        <tree-select
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
                      <el-form-item label="Vị trí/Chức vụ">
                        <m-sel
                          :allowCreate="true"
                          type="label-none"
                          label="PositionName"
                          field="PositionID"
                          :list="PositionNameList"
                          placeholder="Chọn vị trí/chức vụ"
                          @value="FormEmployee.PositionID = $event"
                          :clearable="true"
                          :defaultString="FormEmployee.PositionID"
                          :filterable="true"
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row :gutter="24">
                    <el-col :lg="24" :md="24" :sm="24">
                      <el-form-item
                        label="Mã số thuế"
                        prop="TaxNo"
                        :rules="[
                          {
                            required: true,
                            message: 'Mã số thuế không được bỏ trống.',
                            trigger: ['blur', 'change'],
                          },
                        ]"
                        :show-message="validate.EmployeeTaxNo"
                        :error="getErrorMessage"
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
                </el-col>
                <el-col :lg="12" :md="12" :sm="24"></el-col>
              </el-row>
            </el-form>
            <!-- /Tab 1 -->

            <!-- ref="FormDependent" -->
            <!-- Tab 2 dialog-->
            <div class="add-dependent" v-show="dependentTab">
              <div class="employee-info">
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Người nộp thuế:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{
                      createdEmployee.FullName
                    }}</span>
                  </el-col>
                </el-row>
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Mã nhân viên:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{
                      createdEmployee.EmployeeCode
                    }}</span>
                  </el-col>
                </el-row>
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Mã số thuế cá nhân:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{
                      createdEmployee.TaxNo
                    }}</span>
                  </el-col>
                </el-row>
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Bộ phận/Phòng ban:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{
                      getOrganizationUnitName
                    }}</span>
                  </el-col>
                </el-row>
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Vị trí/Chức vụ:</span>
                  </el-col>
                  <el-col :span="12">
                    <span v-if="checkPositionID" class="detail-info-value">{{
                      getPositionNameFromDB
                    }}</span>
                    <span v-if="!checkPositionID" class="detail-info-value">{{
                      FormEmployee.PositionName
                    }}</span>
                  </el-col>
                </el-row>
              </div>
              <div class="main-dependent-register">
                <div
                  class="img-dependent"
                  v-if="!listtableTab || getDataTable.length < 1"
                >
                  <img class="img-src" src="@/assets/images/Group 19459.svg" />
                  <btn
                    class="btn-add-dependent"
                    type="btn-white-icon"
                    name="Thêm người phụ thuộc"
                    :handleClick="setAddDependent"
                  >
                    <template v-slot:icon>
                      <img class="icon" src="@/icons/Component 9756.svg" />
                    </template>
                  </btn>
                </div>

                <div
                  class="container-tableTab"
                  v-if="listtableTab && getDataTable.length > 0"
                >
                  <el-row>
                    <el-col>
                      <el-row class="header-list-table">
                        <el-col :span="12">
                          <span class="title-list-table">{{
                            titleListTable
                          }}</span>
                        </el-col>
                        <el-col :span="12">
                          <btn
                            :type="'btn-white-icon'"
                            :name="'Thêm người phụ thuộc'"
                            :handleClick="setAddMode"
                            class="bg-white btn-add-dependent"
                          >
                            <template v-slot:icon>
                              <img
                                class="icon"
                                src="@/icons/Component 9756.svg"
                              />
                            </template>
                          </btn>
                        </el-col>
                      </el-row>
                      <el-row
                        class="del"
                        v-if="isCheckRow && getDataTable.length > 0"
                      >
                        <el-col :span="5">
                          <btn
                            type="btn-white3-onlyicon"
                            :iconel="'el-icon-delete'"
                            tooltip="Xóa người nộp thuế"
                            :handleClick="openPopup"
                          >
                            <template v-slot:icon></template>
                          </btn>
                          <span class="del-title" @click="clearSelection"
                            >Bỏ chọn {{ select.length }} người phụ thuộc</span
                          >
                        </el-col>
                      </el-row>
                      <el-row class="table" v-if="getDataTable.length > 0">
                        <tbl-dependent-person
                          ref="dependent-person"
                          :listColumn="listColumn"
                          :dataTable="getDataTable"
                          :haveCheck="true"
                          :havePagination="false"
                          :tooltip="true"
                          :listaction="listaction"
                          :chipArray="chipArray"
                          :isStatus="false"
                          typepage="tờ khai"
                          @handleSelectionChange="getRowsSelection"
                          @handleSelectAll="handleSelectAll"
                          @handleRowDBLClick="handleRowDBLClick"
                          :loading="false"
                          @row-action="getRowAction"
                        ></tbl-dependent-person>
                      </el-row>
                    </el-col>
                  </el-row>
                </div>
              </div>
            </div>
            <!-- / Tab 2 -->
          </div>
        </div>
      </template>
      <template v-slot:footer>
        <!-- button employeeTab-->
        <btn
          v-if="employeeTab == true"
          name="Tiếp tục"
          type="btn-primary"
          class="ml-10 btn-footer"
          :handleClick="moveToDependenPersonTab"
        ></btn>
        <btn
          v-if="employeeTab == true"
          name="Hủy"
          type="btn-grey"
          class="ml-10 btn-footer"
          :handleClick="closeemployeeTab"
        ></btn>
        <!-- //// -->

        <!-- button dependentTab -->
        <btn
          v-if="dependentTab == true"
          name="Hoàn thành"
          type="btn-primary"
          class="ml-10 btn-footer"
          :handleClick="handleFinish"
        ></btn>
        <btn
          v-if="dependentTab == true"
          name="Quay lại"
          type="btn-seconds"
          class="ml-10 btn-footer"
          :handleClick="returnemployeeTab"
        ></btn>
        <btn
          v-if="dependentTab == true"
          name="Hủy"
          type="btn-grey"
          class="ml-10 btn-footer none-boder"
          :handleClick="closedependentTab"
        ></btn>
        <!-- ///// -->
      </template>
    </add-dialog>
    <!-- / Dialog thêm hồ sơ người phụ thuộc -->
    <add-dialog
      ref="dialogDependent"
      :titleDialog="title"
      :id="dialogAddDependentID"
      :dialogFormVisible="isAddDependent"
      :closeonclickmodal="false"
      @close-dialog="closeDependentDialog"
      xl="9,10"
      lg="9,10"
      md="9,10"
      sm="4,3"
      xs="7,8"
    >
      <template v-slot:body>
        <el-form
          ref="FormDependent"
          :model="FormDependentPerson"
          v-show="dependentTab"
          :rules="triggerVali ? rules_2 : rules_1"
          label-width="120px"
          label-position="top"
          @validate="validateFormDependent"
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
                  onlyText
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
                    <el-radio class="radio-align" v-model="hasTaxNo" label="1"
                      >Có</el-radio
                    >
                  </el-form-item>
                </el-col>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item>
                    <el-radio class="radio-align" v-model="hasTaxNo" label="2"
                      >Không</el-radio
                    >
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
                    <el-radio v-model="isPassport" label="1" class="radio-align"
                      >Có</el-radio
                    >
                  </el-form-item>
                </el-col>
                <el-col :lg="12" :md="12" :sm="24">
                  <el-form-item>
                    <el-radio class="radio-align" v-model="isPassport" label="2"
                      >Không</el-radio
                    >
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
                        @value="FormDependentPerson.BirthCertificateDistrictID = $event"
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
          v-if="dependentTab == true"
          name="Lưu và đóng"
          :dataDropdown="buttons"
          type="multi-button"
          class="ml-10 btn-footer"
          :handleClick="moveToTableTab"
          @addandnew="AddandNew"
        ></btn>
        <btn
          v-if="dependentTab == true"
          name="Hủy"
          type="btn-seconds"
          class="ml-10 btn-footer none-boder"
          :handleClick="closeDependentDialog"
        ></btn>
        <!-- Button -->
      </template>
    </add-dialog>

    <addDependentDialog
      :title="titleDialog"
      :formMode="formMode"
      :DependentSelected="DependentSelected"
      @success="resetTable"
      :visiableDL="isDialog"
      :declarationID="declarationDetailID"
      :EmployeeName="createdEmployee.FullName"
      @closeDialog="isDialog = $event"
    />
  </div>
</template>
<script>
import dialog from "@/components/dialog";
import btn from "@/components/buttons";
import fieldinput from "@/components/inputs";
import actionTable from "@/enums/actionTable.js";
import TwoStepProgress from "@/components/progress";
import select from "@/components/select";
import Table from "@/components/tables";
import CategoryDictionaryAPI from "@/api/etax/category-dictionary.js";
import EmployeeAPI from "@/api/etax/employee.js";
import declaration from "@/api/etax/declaration.js";
import relation from "@/enums/relation.js";
import addDependentDialog from "@/views/dependent-register/dependent-register-detail/components/add-dependent-dialog";
import organizationUnit from "@/api/etax/organization-unit.js";
import List_INDENTITY from "@/enums/listIndentity.js";
import apiPosition from "@/api/etax/position.js";
import tree from "@/components/trees/index.vue";
import datetime2 from "@/components/date-time-pickers/date-time-picker-v2";
export default {
  components: {
    "add-dialog": dialog,
    "input-field": fieldinput,
    btn,
    "m-sel": select,
    "tbl-dependent-person": Table,
    addDependentDialog,
    TwoStepProgress,
    "tree-select": tree,
    datetime2,
  },
  props: {
    visiableDL: {
      type: Boolean,
      default: true,
    },
    declarationID: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      checkPositionID: true, //Check vị trí có trong DB chưa
      isClearValidate: false,
      arraySetting: {
        value: "OrganizationUnitID",
        label: "OrganizationUnitName",
        children: "OrganizationUnitsChildren",
      },
      addDependentFirst: false,
      isListEmployee: false,
      listEmployeeCode: [], // List Các người lao động có trùng mã nhân viên
      employeeExistByTaxNo: {},
      employeeExist: {},
      errorTaxNo: false, // lỗi mã số thuế bị trùng
      DependentSelected: {},
      createdEmployee: {},
      formMode: 1,
      titleDialog: "THÊM MỚI NGƯỜI PHỤ THUỘC",
      select: [], //mảng để lưu dòng được chọn
      listIdDelete: [],
      isCheckRow: false, //check để hiển thị button xóa
      title: "ĐĂNG KÝ NGƯỜI PHỤ THUỘC",
      dialogid: "add-employee-dialog",
      dialogAddDependentID: "add-dependent-dialog",
      isDialog: false,
      declarationDetailID: "", //Lưu ID của người nộp thuế mà service trả về
      NowYear: new Date().getFullYear(),
      NowMonth: new Date().getMonth() + 1,
      NowDate: new Date().getDate(),
      PersonalInfoType: "",
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

      OrganUnitList: [],
      PositionNameList: [],
      Relation: [],
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
      IdentityList: [],
      NationalList: [
        {
          EntityState: 0,
          NationalityID: 84,
          NationalityName: "Việt Nam",
          Status: 0,
        },
      ],
      listColumn: [
        {
          label: "Họ và tên",
          prop: "DependentName",
          propertyName: "DependentName",
          width: "",
          minWidth: "125",
          align: "left",
          type: "",
          sortable: true,
        },
        {
          label: "Mã số thuế",
          prop: "DependentTaxNo",
          propertyName: "DependentTaxNo",
          width: "",
          minWidth: "100",
          align: "left",
          type: "",
          sortable: true,
        },
        {
          label: "Ngày sinh",
          prop: "DateOfBirth",
          propertyName: "DateOfBirth",
          width: "",
          minWidth: "100",
          align: "left",
          type: "date",
          sortable: true,
        },
        {
          label: "Quan hệ với người nộp thuế",
          prop: "RelationID",
          propertyName: "RelationID",
          width: "",
          minWidth: "75",
          align: "left",
          type: "",
          sortable: true,
        },
        {
          label: "Loại phụ thuộc",
          prop: "DependentType",
          propertyName: "DependentType",
          width: "",
          minWidth: "",
          align: "left",
          type: "chip",
          sortable: true,
        },
        {
          label: "Ngày bắt đầu",
          prop: "StartDependentDate",
          propertyName: "StartDependentDate",
          width: "",
          minWidth: "100",
          align: "center",
          type: "month",
          sortable: true,
        },
        {
          label: "Ngày kết thúc",
          prop: "FinishDependentDate",
          propertyName: "FinishDependentDate",
          width: "",
          minWidth: "100",
          align: "center",
          type: "month",
          sortable: true,
        },
      ],
      chipArray: "DEPENDENT_TYPE",
      listaction: [],
      dataTable: [],
      ProvinceList: [],
      DistrictList: [],
      WardList: [],
      hasTaxNo: "1",
      isPassport: "1",
      FormEmployee: {
        OrganizationUnitName: "",
        EmployeeCode: "",
        FullName: "",
        OrganizationUnitID: null,
        PositionID: null,
        TaxNo: "",
        Confirm: true,
        DeclarationDetailDependents: [],
      },
      FormDependentPerson: {
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
        DateOfBirth: "",
        DependentType: 0,
        IndentityType: List_INDENTITY.LIST_INDENTITY[0].DBOptionID,
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
      employeeTab: true,
      dependentTab: false,
      tableTab: false,
      position: {
        PositionName: "",
      },
      isCheckdependentTab: true, //bien disable button hoan thanh
      isNextProgress: false,
      filterEmployee: {
        employeeCode: "",
        taxNo: "",
        employeeID: "",
        citizenIdentityNo: "",
      },
      filterDeclarationDetail: {
        declarationID: "",
        taxNo: "",
        citizenIdentityNo: "",
        employeeCode: "",
      },
      isAddDependent: false,
      timer: null, // delay cho việc tìm kiếm theo mã code
      listtableTab: false,
      titleListTable: "Danh sách người phụ thuộc",
      buttons: [
        {
          name: "Lưu và thêm mới",
          nameVal: "addandnew",
        },
      ],
    };
  },
  watch: {
    isAddDependent() {
      this.$refs["FormDependent"].clearValidate();
    },
    "FormDependentPerson.StartDependentDate": {
      handler(val) {
        if (val) {
          this.idValidateDate = "StartDependentDate";
        }
      },
      deep: true,
    },
    "FormDependentPerson.FinishDependentDate": {
      handler(val) {
        if (val) {
          this.idValidateDate = "FinishDependentDate";
        }
      },
      deep: true,
    },

    "FormEmployee.EmployeeTaxNo": {
      handler(val) {
        if (val) {
          this.checkEmployeeTaxNo();
        }
      },
      deep: true,
    },

    FormDependentPerson: {
      handler(val) {
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
          val.DependentName ||
          val.StartDependentDate ||
          val.FinishDependentDate ||
          val.DateOfBirth ||
          val.DependentTaxNo ||
          val.BirthCertificateBook ||
          val.BirthCertificateNumber ||
          val.BirthCertificateProvinceID ||
          val.BirthCertificateDistrictID ||
          val.BirthCertificateWardID ||
          val.CitizenIdentityNo
        ) {
          this.isCheckdependentTab = false;
        } else {
          this.isCheckdependentTab = true;
        }
      },
      deep: true,
    },
    "FormDependentPerson.BirthCertificateProvinceID": {
      handler(val) {
        this.FormDependentPerson.BirthCertificateDistrictID = null;
        this.FormDependentPerson.BirthCertificateWardID = null;
        if (val) this.setProvince(val);
      },
    },
    "FormDependentPerson.BirthCertificateDistrictID": {
      handler(val) {
        console.log(val);
        this.FormDependentPerson.BirthCertificateWardID = null;
        if (val) this.setDistrict(val);
      },
    },
    tableTab: function (val) {
      if (val) {
        document.querySelector(".el-dialog__body").classList.add("istableTab");
      } else {
        document
          .querySelector(".el-dialog__body")
          .classList.remove("istableTab");
      }
    },
    dependentTab: function (val) {
      if (val)
        this.PersonalInfoType = this.IdentityList.filter(
          ({ DBOptionID }) =>
            DBOptionID == this.FormDependentPerson.IndentityType
        )[0].DBOptionName;
    },
    "FormDependentPerson.IndentityType": function (val) {
      if (val) {
        this.PersonalInfoType = this.IdentityList.filter(
          ({ DBOptionID }) =>
            DBOptionID == this.FormDependentPerson.IndentityType
        )[0].DBOptionName;
      }
    },
    visiableDL: function (val) {
      if (val) {
        this.$refs["FormEmployee"].clearValidate();
        document.getElementById("EmployeeCode").focus();
      }
    },
  },
  methods: {
    async validateFormDependent(prop) {
      if (
        this.isClearValidate &&
        (prop == "DependentTaxNo" ||
          prop == "BirthCertificateWardID" ||
          prop == "CitizenIdentityNo")
      ) {
        await this.$refs["FormDependent"].clearValidate();
        this.isClearValidate = false;
      }
    },

    // validateForm(prop) {
    //   if (this.isClearValidate && prop == "TaxNo") {
    //     this.$refs["FormEmployee"].clearValidate();
    //     this.isClearValidate = false;
    //   }
    // },
    /**
     * Lấy giá trị được emit từ "tree"
     * created by tcduong 5.10.2020
     */
    getOrganizationUnitValue(val) {
      this.FormEmployee.OrganizationUnitID = val.OrganizationUnitID;
      this.FormEmployee.OrganizationUnitName = val.OrganizationUnitName;
    },
    /**
     * Bỏ tất cả chọn bản ghi
     * Created bt tcduong 23.9.2020
     */
    clearSelection() {
      this.$refs["dependent-person"].clearSelection();
    },

    /**
     * Tìm kiếm theo mã code có timeout
     * created by nvbinh2 14.10.2020
     */
    findEmployeeTimeOut(val) {
      if (this.timer) {
        clearInterval(this.timer);
        this.timer = null;
      }
      this.timer = setInterval(() => {
        this.checkEmployeeCode(val);
      }, 3000);
    },
    /**
     * Lấy danh sách nhân viên theo filter
     * Created by: NVANH created date: 23.09.2020
     */
    async checkEmployeeCode() {
      this.filterEmployee.employeeCode = this.FormEmployee.EmployeeCode;
      this.filterEmployee.taxNo = "";
      if (!this.FormEmployee.EmployeeCode) return;
      const res = await EmployeeAPI.getEmployee(this.filterEmployee);
      if (!res.data.Data || res.data.MISACode != 200 || !res.data.Success) {
        this.employeeExist = {};
        this.FormEmployee.TaxNo = "";
        this.FormEmployee.FullName = "";
        this.FormEmployee.OrganizationUnitID = "";
        this.FormEmployee.OrganizationUnitName = "";
        this.FormEmployee.PositionID = "";
        this.isClearValidate = true;
        return;
      }
      this.employeeExist.FullName = res.data.Data.FullName;
      this.employeeExist.OrganizationUnitID = res.data.Data.OrganizationUnitID;
      this.employeeExist.PositionID = res.data.Data.PositionID;
      this.employeeExist.TaxNo = res.data.Data.TaxNo;
      //Bind dữ liệu vào form
      this.FormEmployee.FullName = res.data.Data.FullName;
      this.FormEmployee.OrganizationUnitID = res.data.Data.OrganizationUnitID;
      this.FormEmployee.OrganizationUnitName =
        res.data.Data.OrganizationUnitName;
      this.FormEmployee.PositionID = res.data.Data.PositionID || null;
      this.FormEmployee.TaxNo = res.data.Data.TaxNo;
    },

    /**
     * Lấy thông tin người đăng kí từ danh sách trùng mã nhân viên
     */
    getEmployeeFromCode(val) {
      for (var i of this.listEmployeeCode) {
        if (i.EmployeeID == val) {
          this.employeeExist.FullName = i.FullName;
          this.employeeExist.OrganizationUnitID = i.OrganizationUnitID;
          this.employeeExist.PositionID = i.PositionID;
          this.employeeExist.TaxNo = i.TaxNo;
          this.employeeExist.EmployeeID = i.EmployeeID;
          this.employeeExist.EmployeeCode = i.EmployeeCode;
          //Bind dữ liệu vào form
          this.FormEmployee.FullName = i.FullName;
          this.FormEmployee.OrganizationUnitID = i.OrganizationUnitID;
          this.FormEmployee.OrganizationUnitName = i.OrganizationUnitName;
          this.FormEmployee.PositionID = i.PositionID || null;
          this.FormEmployee.TaxNo = i.TaxNo;
          this.FormEmployee.EmployeeID = i.EmployeeID;
          break;
        }
      }
    },

    /**
     * Check object
     * created by tcduong 29.9.2020
     */
    checkObject(object) {
      if (
        object.EmployeeID !== this.FormEmployee.EmployeeID ||
        object.EmployeeCode != this.FormEmployee.EmployeeCode ||
        object.FullName != this.FormEmployee.FullName ||
        object.OrganizationUnitID != this.FormEmployee.OrganizationUnitID ||
        object.PositionID != this.FormEmployee.PositionID ||
        object.TaxNo != this.FormEmployee.TaxNo
      ) {
        return false;
      }
      return true;
    },
    /**
     * Map dữ liệu vào ô input khi mã nhân viên đã tồn tại
     * @param{object là dữ liệu service trả về khi nhập trùng mã nhân viên}
     */
    mapDataToInput(object) {
      if (!object) {
        object = this.employeeExist;
      }
      this.FormEmployee.FullName = object.FullName;
      this.FormEmployee.OrganizationUnitID = object.OrganizationUnitID;
      this.FormEmployee.OrganizationUnitName = object.OrganizationUnitName;
      this.FormEmployee.PositionID = object.PositionID;
      this.FormEmployee.TaxNo = object.TaxNo;
    },

    /**
     * Lấy danh sách nhân viên theo filter
     * created by nvanh 25.9.2020
     */
    async checkEmployeeTaxNo() {
      this.filterEmployee.employeeCode = "";
      this.filterEmployee.taxNo = this.FormEmployee.TaxNo;
      const res = await EmployeeAPI.getEmployee(this.filterEmployee);
      return res;
    },

    /**
     * Lấy danh sách nhân viên theo filter
     * created by nvanh 25.9.2020
     */
    async checkDeclarationDetailTaxNo() {
      this.filterDeclarationDetail.taxNo = this.FormEmployee.TaxNo;
      const res = await declaration.getFilterDeclarationDetail(
        this.filterDeclarationDetail
      );
      return res;
    },

    setAddMode() {
      this.formMode = 1;
      this.openAddDepentdent();
    },

    /**
     * Mở dialog thêm người phụ thuộc
     * created by nvanh 22.9.2020
     */
    openAddDepentdent() {
      if (this.formMode == 1) {
        this.titleDialog = "THÊM MỚI NGƯỜI PHỤ THUỘC";
        this.isDialog = true;
      } else {
        this.titleDialog = "CHỈNH SỬA NGƯỜI PHỤ THUỘC";
        this.isDialog = true;
      }
    },

    /**
     * Hàm cảnh báo Thời gian tính giảm trừ
     * created by NVANH 24.09.2020
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
              this.FormDependentPerson.StartDependentDate = "";
              this.FormDependentPerson.FinishDependentDate = "";
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

    /**
     * Hàm thực hiện sự kiện khi click vào 1 dòng
     * @params{rows là mảng thông tin của từng dòng}
     */
    getRowsSelection(rows) {
      if (rows.length > 0) {
        var listId = rows.map(function (item) {
          return item.DeclarationDetailDependentID;
        });

        this.listIdDelete = [...listId];
        this.select = rows;
        this.isCheckRow = true;
      } else {
        this.isCheckRow = false;
      }
    },

    /**
     * Hàm mở cảnh báo xóa người phụ thuộc
     * Created by TCDUONG 20.09.2020
     */
    async openPopup() {
      var title = "Xóa người phụ thuộc";
      var contentPopup = "";
      if (this.select.length == 1) {
        contentPopup = `Bạn có chắc chắn muốn xóa người phụ thuộc <b>${this.select[0].DependentName}</b> không?`;
      } else {
        contentPopup =
          "Danh sách người phụ thuộc được chọn sẽ bị xóa khỏi hệ thống. Bạn có chắc chắn muốn xóa không?";
      }
      var buttons = [
        {
          text: "Có",
          class: "btn-delete",
          callback: this.deleteSelectedRow,
        },
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
      ];
      this.$_Popup.delete(title, contentPopup, buttons);
    },

    /**
     * Hàm xóa người phụ thuộc
     * Created by TCDUONG 22.09.2020
     * Modified by NVANH 24.05.2020
     */
    async deleteSelectedRow() {
      var res = await declaration.deleteDeclarationDetailDependent(
        this.listIdDelete
      );
      if (res.data.Success && res.data.MISACode == 200) {
        this.getDeclarationDependent(this.declarationDetailID);
        this.$_Notification.success(
          "Thành công",
          `Xóa thành công ${this.listIdDelete.length} người phụ thuộc`
        );
      } else
        this.$_Notification.error(
          "Thất bại",
          "Bạn đã xóa người phụ thuộc thất bại.Vui lòng kiểm tra lại."
        );
    },
    handleRowDBLClick() {},
    handleSelectAll() {},

    /**
     * Hàm để lấy action xóa hoặc sửa, nếu acton = 1 là xóa
     * created by tcduong 20.9.2020
     */
    async getRowAction(val) {
      if (val.value == 1) {
        this.listIdDelete = [];
        this.listIdDelete.push(val.row.DeclarationDetailDependentID);
        this.select = [];
        this.select.push(val.row);
        this.openPopup();
      } else {
        this.formMode = 0;
        await this.getEditDependent(val.row.DeclarationDetailDependentID);
        this.openAddDepentdent();
      }
    },
    /**
     * Gọi service thông người phụ thuộc để bind lên dialog
     * created by NQTrung 26/09/2020
     */
    async getEditDependent(id) {
      var res = await declaration.getDeclarationDetailDependentByID(id);
      if (res.data.Success && res.data.MISACode == 200) {
        this.DependentSelected = res.data.Data;
      } else {
        this.$_Notification.error(
          "Thất bại",
          " Lấy người phụ thuộc thất bại.Vui lòng kiểm tra lại."
        );
      }
    },
    async setUpForm() {
      const res = await CategoryDictionaryAPI.getNationalites();
      this.NationalList = res.data.Data;
    },

    /**
     * Lấy danh sách Tỉnh thành phố, quận huyện, phường xã
     * created by NVANH 24.09.2020
     * */
    async setNational() {
      const res = await CategoryDictionaryAPI.getProvincials();
      this.ProvinceList = res.data.Data;
      this.DistrictList = [];
      this.WardList = [];
    },

    async setProvince(province) {
      const res = await CategoryDictionaryAPI.getDistrictsByDistrictsID(
        province
      );
      this.DistrictList = res.data.Data;
      this.WardList = [];
    },
    async setDistrict(distric) {
      const res = await CategoryDictionaryAPI.getWardByDistrictsID(distric);
      this.WardList = res.data.Data;
    },

    /**
     * Xử lý nút Tiếp tục khi Di chuyển sang tab Thêm người phụ thuộc
     * Created by: NVANH 18.09.2020
     */
    moveToDependenPersonTab() {
      this.$refs["FormEmployee"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById(`${Object.keys(index)[0]}`).focus();
            }, 200);
          }
        }
        if (valid) {
          this.createdEmployee = Object.assign({}, this.FormEmployee);
          const res = await this.checkDeclarationDetailTaxNo();
          if (res.data.MISACode !== 200) {
            this.$_Popup.warn("Cảnh báo", "Đã có lỗi xảy ra!", [
              {
                text: "Đóng",
                class: "btn-primary",
                callback: () => {},
              },
            ]);
            return;
          }
          if (res.data.Data && res.data.MISACode == 200) {
            this.$_Popup.warn(
              "Cảnh báo",
              `Mã số thuế <b>${res.data.Data.TaxNo}</b> đã được đăng ký cho người nộp thuế <b>${res.data.Data.FullName}</b> trên tờ khai hiện tại. Vui lòng kiểm tra lại`,
              [
                {
                  text: "Đóng",
                  class: "btn-primary",
                  callback: () => {
                    this.errorTaxNo = true;
                  },
                },
              ]
            );
            return;
          } else {
            //Nếu dữ liệu được fill theo mã nhân viên(mã nhân viên trùng)
            if (Object.keys(this.employeeExist).length !== 0) {
              //Nếu trùng thì cho next tab
              if (this.checkObject(this.employeeExist)) {
                this.nextToDependenPersonTab();
              } else {
                //Nếu object khác thì check mã số thuế trước

                // Trường hợp chỉ thay đổi MST
                if (this.FormEmployee.TaxNo != this.employeeExist.TaxNo) {
                  const res = await this.checkEmployeeTaxNo();
                  if (!res.data.Data && res.data.MISACode === 200)
                    this.warningChangeData(1);
                  else {
                    if (this.checkObject(res.data.Data)) {
                      this.nextToDependenPersonTab();
                    } else {
                      if (
                        res.data.Data.EmployeeID != this.FormEmployee.EmployeeID
                      )
                        this.warningChangeData(2, res.data.Data);
                      else this.warningChangeData(1, res.data.Data);
                    }
                  }
                } else {
                  //Không thay đổi MST thì hiển thị cảnh báo dữ liệu bị thay đổi
                  this.warningChangeData(1);
                }
              }
            } else {
              //Nếu tự nhập thông tin(mã nv không trùng)
              const res = await this.checkEmployeeTaxNo();
              if (!res.data.Data && res.data.MISACode === 200) {
                this.nextToDependenPersonTab();
                return;
              } else {
                if (this.checkObject(res.data.Data)) {
                  this.nextToDependenPersonTab();
                } else {
                  this.warningChangeData(2, res.data.Data);
                }
              }
            }
          }
        }
      });
    },

    warningChangeData(val, object) {
      var buttons = [];
      if (val == 1) {
        buttons = [
          {
            text: "Không",
            class: "btn-grey",
            callback: () => {
              this.mapDataToInput();
              this.createdEmployee = Object.assign({}, this.FormEmployee);
            },
          },
          {
            text: "Có",
            class: "btn-primary",
            callback: () => {
              this.FormEmployee.Confirm = true;
              this.nextToDependenPersonTab();
            },
          },
        ];
      } else if (val == 2) {
        buttons = [
          {
            text: "Đóng",
            class: "btn-primary",
            callback: () => {},
          },
        ];
      }
      var content = "";

      if (val == 1)
        content =
          "Thông tin người nộp thuế đã bị thay đổi, bạn có muốn cập nhật không?";
      if (val == 2)
        content = `Mã số thuế <b>${object.TaxNo}</b> đã được đăng ký cho người nộp thuế <b>${object.FullName}</b> trên hố sơ lao động. Vui lòng kiểm tra lại`;
      this.$_Popup.warn("Cảnh báo", content, buttons);
    },

    nextToDependenPersonTab() {
      this.getPositionName();
      this.FormEmployee.Confirm = true;
      this.employeeTab = false;
      this.dependentTab = true;
      this.tableTab = false;
      this.isNextProgress = true;
    },

    /**
     * Hàm xử lý nút di chuyển sang tab List Table
     * Created by: NVANH 18.09.2020
     */
    moveToTableTab() {
      this.isClearValidate = false;
      this.$refs["FormDependent"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById(`${Object.keys(index)[0]}`).focus();
            }, 200);
          }
        }
        if (valid) {
          await this.AddDeclarationDetail(true);
        }
      });
    },

    /**
     * Reset validate form
     */
    resetForm(formName) {
      if (this.$refs[formName]) {
        this.$refs[formName].resetFields();
      }
    },
    // Các hàm xử lý đóng tab
    closeemployeeTab() {
      this.getClose();
      this.resetFormEmployee();
    },
    // hàm xử lý đóng dialog thêm người phụ thuộc
    closeDependentDialog() {
      this.isAddDependent = false;
      //resetFormDependentPerson()
      this.resetForm("FormDependent");
    },

    closedependentTab() {
      this.resetFormDependentPerson();
      this.getClose();
    },
    async AddandClose() {
      await declaration.postDeclarationDetailDependent(
        this.FormDependentPerson
      );
    },
    async AddandNew() {
      this.isClearValidate = false;
      this.$refs["FormDependent"].validate(async (valid, index) => {
        if (index !== undefined) {
          if (Object.keys(index)[0] != null) {
            setTimeout(() => {
              document.getElementById(`${Object.keys(index)[0]}`).focus();
            }, 200);
          }
        }
        if (valid) {
          await this.AddDeclarationDetail(false);
        }
      });
    },

    // Các hàm quay trở lại tab
    returnemployeeTab() {
      this.employeeTab = true;
      this.dependentTab = false;
      this.isNextProgress = false;
    },
    resetTab() {
      this.employeeTab = true;
      this.dependentTab = false;
      this.tableTab = false;
    },

    // reset form
    resetFormEmployee() {
      this.hasTaxNo = "1";
      this.isPassport = "1";
      this.FormEmployee = {
        Confirm: false,
        OrganizationUnitName: "",
        EmployeeCode: "",
        FullName: "",
        OrganizationUnitID: null,
        PositionID: null,
        TaxNo: "",
        DeclarationDetailDependents: [],
      };
    },

    resetFormDependentPerson() {
      this.isClearValidate = true;
      this.hasTaxNo = "1";
      this.isPassport = "1";
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
        DateOfBirth: "",
        IndentityType: List_INDENTITY.LIST_INDENTITY[0].DBOptionID,
        DependentType: 0,
      };
    },
    resetFormDialog() {},
    getClose() {
      this.resetFormEmployee();
      this.resetFormDependentPerson();
      this.employeeExist = {};
      this.resetTab();
      this.$emit("closeDialog", false);
      this.isNextProgress = false;
      this.isAddDependent = false;
      this.listtableTab = false;
      this.addDependentFirst = false;
    },

    /**
     * Hàm thêm hồ sơ chi tiết
     * Created by tcduong 19.09.2020
     */
    async AddDeclarationDetail(isClose) {
      //Nếu property nào không có dữ liệu thì romove khỏi object
      if (!this.FormDependentPerson.StartDependentDate)
        delete this.FormDependentPerson.StartDependentDate;

      if (!this.FormDependentPerson.FinishDependentDate)
        delete this.FormDependentPerson.FinishDependentDate;

      this.FormEmployee.DeclarationID = this.declarationID;
      this.FormEmployee.DeclarationDetailDependents = [];
      this.FormEmployee.DeclarationDetailDependents.push(
        this.FormDependentPerson
      );

      var res;
      //Nếu chưa thêm người phụ thuộc lần nào thì gọi API thêm cả người lao động và người phụ thuộc
      if (!this.addDependentFirst) {
        res = await declaration.postDeclarationDetail(this.FormEmployee);
        if (res.data.Success && res.data.MISACode == "200") {
          this.$emit("addSuccess", true); //emit sự kiện để load lại data
          this.addDependentFirst = true;
          this.declarationDetailID = res.data.Data;

          await this.getDeclarationDependent(res.data.Data); //gọi service lấy danh sách người phụ thuộc
          this.getPosition(); //Lấy lại danh sách Position vì có thể được thêm mới
          var inforDeclarationDetail = await declaration.getDeclarationDetailById(
            res.data.Data
          ); //Lấy thông tin người nộp thuế vừa tạo thành công
          this.createdEmployee = { ...inforDeclarationDetail.data.Data }; //thông tin người nộp thuế vừa tạo thành công vào object để bind thông tin qua danh sách người phụ thuộc

          this.$_Notification.success("Thành công", "Thêm mới thành công");
          if (isClose) this.nextToTableDepentdent();
          else {
            this.listtableTab = true;
            this.resetFormDependentPerson();
          }
          return true;
        } else if (res.data.MISACode == 300 && res.data.Data.Reason == 1) {
          this.FormEmployee.Confirm = true;
          this.declarationDetailID = res.data.Data.DeclarationDetailID;
          //Nếu người nộp thuế đã có thì đổi Confirm=true để service update
          this.openPopupWarn();
        } else {
          this.$_Notification.error(
            "Thất bại",
            "Bạn đã thêm người nộp thuế thất bại.Vui lòng kiểm tra lại."
          );
          return false;
        }
      }
      //Nếu chưa thêm người phụ thuộc 1 lần rồi thì chỉ lấy ID của người nộp thuế vừa thêm trước đó và chỉ thêm người phụ thuộc
      else if (this.addDependentFirst && this.declarationDetailID) {
        this.FormDependentPerson.DeclarationDetailID = this.declarationDetailID;
        res = await declaration.postDeclarationDetailDependent(
          this.FormDependentPerson
        );

        if (!res.data.Success) {
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
            this.nextToTableDepentdent();
          } else {
            this.resetFormDependentPerson();
          }
          await this.getDeclarationDependent(this.declarationDetailID); //gọi service lấy danh sách người phụ thuộc
          this.$_Notification.success(
            "Thành công",
            `Thêm người phụ thuộc cho nhân viên ${this.FormEmployee.EmployeeName} thành công`
          );
        }
      }
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
     * Next qua tab danh sách người phụ thuộc
     * created by tcduong 26.9.2020
     */
    nextToTableDepentdent() {
      // this.resetFormEmployee();
      // this.resetFormDependentPerson();
      // this.employeeTab = false;
      // this.dependentTab = true;
      this.isAddDependent = false;
      this.listtableTab = true;
      // this.title = "DANH SÁCH NGƯỜI PHỤ THUỘC";
    },

    async resetTable() {
      await this.getDeclarationDependent(this.declarationDetailID);
    },

    /**
     * Bật Popup cảnh báo
     * created by tcduong 25.9.2020
     */
    openPopupWarn() {
      var buttons = [
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
        {
          text: "Có",
          class: "btn-primary",
          callback: this.updateDeclarationDetail,
        },
      ];

      this.$_Popup.warn(
        "Cảnh báo",
        "Người nộp thuế đã tồn tại, bạn có muốn cập nhật không?",
        buttons
      );
    },
    /**
     * Update khi người nộp thuế bị trùng
     * created by tcduong 25.9.2020
     * @param{data là thông tin của người nộp thuế và người phụ thuộc}
     */
    async updateDeclarationDetail() {
      var res = await declaration.postDeclarationDetail(this.FormEmployee);
      if (res.data.Success && res.data.MISACode == 200) {
        this.$emit("addSuccess", true); //emit sự kiện để load lại data
        await this.getDeclarationDependent(this.declarationDetailID);
        //CHuyển tab
        //this.nextToTableDepentdent();
        this.$_Notification.success("Thành công", "Thêm mới thành công");
        return true;
      } else {
        this.$_Notification.error(
          "Thất bại",
          "Bạn đã thêm người nộp thuế thất bại.Vui lòng kiểm tra lại."
        );
        return false;
      }
    },
    /**
     * Lấy danh sách người phụ thuộc
     * created by tcduong 22.9.2020
     * @param{id là Id của người nộp thuế}
     */
    async getDeclarationDependent(id) {
      var res = await declaration.getDeclarationDetailDependent(id);
      this.mapdata(res.data.Data);
      return res;
    },
    mapdata(val) {
      var datas = [];
      val.forEach((item) => {
        var data = {};
        data.DeclarationDetailDependentID =
          item["DeclarationDetailDependentID"];
        data.DependentName = item["DependentName"];
        data.DependentTaxNo = item["DependentTaxNo"];
        data.DateOfBirth = item["DateOfBirth"];
        data.CitizenIdentityNo = item["CitizenIdentityNo"];

        data.RelationID = relation.findInArray(item["RelationID"]);
        data.StartDependentDate = item["StartDependentDate"];
        data.FinishDependentDate = item["FinishDependentDate"];
        data.DependentType = item["DependentType"];
        datas.push(data);
      });
      this.dataTable = [...datas];
    },
    gettoday() {
      this.FormDependentPerson.DateOfBirth = `${this.NowMonth}-${this.NowDate}-${this.NowYear}`;
    },

    /**
     * Lấy danh sách position từ servie
     * created by tcduong 25.9.2020
     */
    async getPosition() {
      var res = await apiPosition.getPosition();
      if (!res.data.Data || !res.data.Success || res.data.MISACode != 200)
        return [];
      else this.PositionNameList = [...res.data.Data];
    },
    /**
     * Lấy danh sách bộ phân/phòng ban từ servie
     * created by tcduong 5.10.2020
     */
    async getOrganizationUnits() {
      var res = await organizationUnit.getOrganizationUnit("tree", 0);
      if (!res.data.Success) {
        return [];
      }
      return res.data.Data;
    },

    /**
     * Hàm hiển thị dialog thêm người phụ thuộc
     * Created by NQKHAI(12/11/2020)
     */
    setAddDependent() {
      this.isAddDependent = true;
    },

    /**
     * Lấy tên Vị chí/chức vụ của người lao động khi người lao động tự nhập vị trí/chức vụ
     */
    getPositionName() {
      if (this.FormEmployee.PositionID)
        this.checkPositionID = this.PositionNameList.some(
          (item) => item.PositionID == this.FormEmployee.PositionID
        );
      if (!this.checkPositionID) {
        this.FormEmployee.PositionName = this.FormEmployee.PositionID;
        this.FormEmployee.PositionID = "";
      } else this.FormEmployee.PositionName = "";
    },
    /**
     * Hàm xử lý sự kiện click nút hoàn thành trong dialog
     * Created By : NQKHAI(19/11/2020)
     */
    handleFinish() {
      var buttons = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];

      if (this.getDataTable.length < 1) {
        this.$_Popup.warn(
          "Cảnh báo",
          "Vui lòng thêm mới người phụ thuộc trước khi hoàn thành",
          buttons
        );
      } else {
        this.getClose();
      }
    },
  },

  async created() {
    // await this.setUpForm();
    this.setNational();
    this.listaction = actionTable.TAX_REGISTER;
    this.Relation = relation.RELATION;
    this.OrganUnitList.push(await this.getOrganizationUnits());
    this.IdentityList = List_INDENTITY.LIST_INDENTITY;
    this.filterDeclarationDetail.declarationID = this.$route.params.id;
    this.getPosition();
  },
  computed: {
    getDataTable() {
      return this.dataTable;
    },
    getPersonalInfoType() {
      return this.PersonalInfoType;
    },
    getOrganizationUnitName() {
      return this.createdEmployee.OrganizationUnitName;
    },
    getPositionNameFromDB() {
      let positionobj = this.PositionNameList.filter(
        ({ PositionID }) => PositionID == this.createdEmployee.PositionID
      );
      if (positionobj.length > 0) return positionobj[0].PositionName;
      return "";
    },
    // Thông báo lỗi trùng mã số thuế
    getErrorMessage() {
      if (this.errorTaxNo) {
        return "Mã số thuế đã tồn tại";
      }
      return null;
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
@import "@/styles/typo.scss";
#add-employee-dialog {
  border-radius: 5px;
  width: calc(100% - 48px);
  height: calc(100% - 25px);
  margin: 25px 24px 0px 24px;

  .el-dialog__header {
    padding: 0;
  }

  .el-dialog__body {
    padding: 0 24px;
    height: calc(100% - 62px);

    .company-container {
      height: 100%;
      z-index: 0;

      .add-employee-tab {
        height: 67px;
        border-bottom: 1px solid $background-grey;
        display: flex;
        justify-content: center;
        align-items: center;
        margin: 0 -24px 0 -24px;

        .tab-bar {
          height: 40px;
          position: absolute;
          top: 13px;
          z-index: 1;
        }

        .title {
          font-size: 15px;
          font-family: $_fontbold;
          position: absolute;
          left: 0;
        }
      }

      .add-employee-main {
        position: relative;
        height: calc(100% - 67px);

        .add-dependent {
          display: flex;
          margin-left: -24px;
          margin-right: -24px;
          height: 100%;
        }

        .main-dependent-register {
          background-color: $background-grey;
          width: 75%;
          justify-content: center;
          display: flex;
          position: relative;

          .img-dependent {
            display: grid;
            top: calc(50% - 25px);
            transform: translateY(-50%);
            position: absolute;

            .btn-add-dependent {
              position: relative;
              justify-content: center;
              display: flex;

              button {
                width: 188px;
                border-color: $white;

                span {
                  color: $hover-primary;
                }
              }
            }
          }

          .container-tableTab {
            width: 100%;
            height: 100%;

            .header-list-table {
              padding: 24px 24px 16px 0;
              display: flex;
              align-items: center;
            }

            .table {
              background-color: $background-grey;
              margin: 0 24px;
            }
          }
        }

        .el-form-item__label {
          font-family: $_fontmedium;
          line-height: 17px;
          padding: 16px 0 4px 0;
          font-size: 13px;
          margin: 0;
        }
      }
    }
  }

  .main-empty {
    width: 390px;
    margin: auto;
    display: flex;
    flex-flow: wrap;
    justify-content: center;
    position: absolute;
    top: calc(50% + 100px);
    left: calc(50% - 195px);
  }
  .del {
    margin-top: 16px;
    padding-left: 24px;
    padding-right: 24px;
    margin-bottom: 16px;
    .button-container {
      float: left !important;
    }
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
  .del-title {
    position: relative;
    top: 10px;
    left: 10px;
    cursor: pointer;
    color: #243585;
  }
  .header-dialog {
    padding-bottom: 24px;
    margin-right: 24px;
  }
  .istableTab {
    background-color: $background-grey;
    padding-top: 0px !important;
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

  .container-tableTab {
    width: 100%;
    .header-list-table {
      .title-list-table {
        margin-left: 24px;
        color: $hover-primary;
        font-family: $_fontbold;
      }
      .btn-add-dependent {
        button {
          border-color: $white;
        }
      }
    }
    .btn-white-icon span {
      color: $hover-primary !important;
      font-family: "GoogleSansMedium" !important;
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

    .add-employee-tab {
      .title {
        font-size: 15px;
        font-family: $_fontbold;
        position: absolute;
        left: 24px;
        top: 26px;
        margin: 0;
      }
      .tab-bar {
        height: 40px;
        position: absolute;
        top: 14px;
        z-index: 1;
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

    .add-employee-main {
      .contract-tax .el-form-item__label {
        font-size: 13px !important;
        font-weight: bold !important;
      }
      .el-form {
        overflow: initial;
        margin-top: 4px;
        margin-left: 0px;
        margin-right: 0px;
        position: absolute;
        left: 50%;
        width: 30.6%;
        //  height: calc(100% - 50px);
        transform: translateX(-50%);
      }

      .add-dependent {
        display: flex;
        margin-left: -24px;
        margin-right: -24px;
        height: calc(100% + 20px);
        .employee-info {
          width: 25%;
          margin-left: 24px;
          margin-top: 24px;
          .detail-info {
            margin-bottom: 20px;
          }
        }
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
  .container-tableTab {
    .infor-depentdent-person {
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
  }
  .el-dialog__footer {
    padding: 10px 24px 10px 24px !important;
    background-color: $hover-grey !important;
    .none-boder .btn-seconds {
      border: none !important;
    }
  }

  .el-table thead tr:first-child .cell {
    word-break: break-word;
    white-space: unset;
  }
  .el-button.is-disabled:hover {
    color: #c0c4cc !important;
    cursor: not-allowed;
    background-image: none;
    background-color: #ffffff !important;
    border-color: #ebeef5;
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
  .radio-align {
    margin-top: 12px;
  }
  .lable-radio {
    margin-top: 17px;
  }
  .label-taxno {
    margin-top: 3px;
  }
  .detail-info-text {
    color: $medium-dark-grey;
  }
  .detail-info-value {
    font-family: $_fontmedium;
  }
  .el-radio__label {
    font-size: 13px !important;
    color: $hover-dark !important;
  }
}
</style>
