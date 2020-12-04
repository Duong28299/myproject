<template>
  <div class="tax-settlement-dialog">
    <dialogel
      :titleDialog="getTitle"
      :dialogFormVisible="dialogSwitch"
      :closeonclickmodal="false"
      @close-dialog="cancel"
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
            <!-- Phần bên trái -->
            <el-col :lg="12" :md="12" :sm="24">
              <!-- Mã nhân viên  -->
              <el-row :gutter="24" class="dual-line">
                <el-col :span="12">
                  <span class="title-row">Mã nhân viên</span>
                  <input-field
                    type="field-input"
                    id="employeeID"
                    width="100%"
                    placeholder="Nhập mã nhân viên"
                    :maxlength="25"
                  />
                </el-col>
              </el-row>

              <!-- Họ và tên  -->
              <el-row :gutter="24">
                <span class="title-row">Họ và tên</span>
                <input-field
                  type="field-input"
                  id="employeeName"
                  onlyText
                  width="100%"
                  placeholder="Nhập họ và tên"
                  :maxlength="25"
                />
              </el-row>

              <el-row
                :gutter="24"
                class="dual-line"
              >
                <!-- Bộ phần phòng ban -->
                <el-col :span="12">
                  <span class="title-row">Bộ phận phòng ban</span>
                  <combo-box
                    id="Department"
                    type="label-none"
                    :filterable="true"
                    :defaultNumber="formData.WardIDByResident"
                    :list="DepartmentList"
                    label="WardName"
                    field="WardID"
                    clearable
                  />
                </el-col>
                <!-- Mã số thuế  -->
                <el-col :span="12">
                  <span class="title-row">Mã số thuế</span>
                  <input-field
                    type="field-input"
                    id="employeeName"
                    width="100%"
                    placeholder="Nhập mã số thuế"
                    :maxlength="25"
                  />
                </el-col>
              </el-row>

              <!-- Loại giấy tờ tùy thân -->
              <el-row :gutter="24">
                <span class="title-row">Loại giấy tờ tùy thân</span>
                <combo-box
                  id="Department"
                  type="label-none"
                  :filterable="true"
                  :defaultNumber="formData.WardIDByResident"
                  :list="DepartmentList"
                  label="WardName"
                  field="WardID"
                  clearable
                />
              </el-row>
              <!-- Số hiệu giấy tờ -->
              <el-row :gutter="24">
                <span class="title-row">Số hiệu giấy tờ</span>
                <input-field
                  type="field-input"
                  id="employeeName"
                  width="100%"
                  placeholder="Nhập số chứng minh thư nhân dân"
                  :maxlength="25"
                />
              </el-row>
            </el-col>
            <!-- Phần bên phải -->

            <el-col :lg="12" :md="12" :sm="24">
              <!-- Cá nhân cư trú -->
              <el-row :gutter="24">
                <span class="title-row"> Cá nhân cư trú ?</span>
                <div class="body-row">
                  <el-radio v-model="radio" label="1">Cư trú</el-radio>
                  <el-radio v-model="radio" label="2">Không cư trú</el-radio>
                </div>
              </el-row>

              <!-- Thu nhập chịu thuế -->
              <el-row :gutter="24">
                <span class="title-row">Thu nhập chịu thuế</span>
                <input-field
                  type="input-append"
                  id="employeeName"
                  :append="'VND'"
                  width="100%"
                  :maxlength="25"
                />
              </el-row>
              <!-- Biểu thuế áp dụng -->
              <el-row :gutter="24">
                <el-col :span="18">
                  <span class="title-row">Biểu thuế áp dụng </span>
                </el-col>
                <el-col :span="6"> <span>10</span>% </el-col>
              </el-row>

              <!-- Số thuế TNCN đã khấu trừ -->
              <el-row :gutter="24">
                <el-col :span="13">
                  <span class="title-row">Số thuế TNCN đã khấu trừ </span>
                </el-col>
                <el-col :span="11"><span>100.000.000</span>VNĐ</el-col>

                <span></span>
              </el-row>
            </el-col>
          </el-row>
        </el-form>
      </template>

      <!-- Phần các btn hành động -->
      <template v-slot:footer>
        <div>
          <btn
            v-if="formMode == 1"
            type="multi-button"
            :handleClick="clickSaveAndClose"
            :name="'Lưu và đóng'"
            :dataDropdown="actionDropdown"
            class="btn-saveAndClose"
            @option1="clickSaveAndClose"
            @option2="clickSaveAndNew"
          ></btn>
          <btn
            v-if="formMode == 0"
            type="btn-primary"
            :handleClick="clickSaveAndClose"
            :name="'Lưu'"
          >
          </btn>

          <btn
            name="Hủy"
            type="btn-grey"
            class="mr-8 btn-footer"
            :handleClick="cancel"
          ></btn>
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

export default {
  data() {
    return {
      isWarnClose: false,
      formData: {},
      DepartmentList: [],
      radio: "1",
      rules: {},
    };
  },
  components: {
    dialogel,
    btn: Button,
    "input-field": Inputfield,
    "combo-box": ComboBox,
  },
  props: {
    formMode: {
      type: Number,
      default: 0,
    },
    dialogSwitch: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    clickSaveAndClose() {},
    actionDropdown() {},
    clickSaveAndNew() {},
    cancel() {},
    validateForm() {},
  },
  computed: {
    /**
     * Hàm lấy giá trị title
     * Create ntdong 26/10/2020
     */
    getTitle() {
      if (this.formMode == 0) return "THÊM NGƯỜI NỘP THUẾ";
      else this.formMode == 1;
      return "CHI TIẾT NGƯỜI NỘP THUẾ";
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
.tax-settlement-dialog {
  .dual-line {
    margin-left: -24px !important;
    margin-right: -24px !important;

  }
}
</style>