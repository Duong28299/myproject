<template>
  <div>
    <el-form>
      <el-row :gutter="40" class="container-content">
        <!--cột 1-->
         <el-col :lg="8" :md="8" :sm="24" :span="8">
          <!--Label Thông tin thu nhập-->
          <el-row>
            <el-col :span="24" class="title-bottom-red">
              <div class="main-title ">THÔNG TIN THU NHẬP</div>
              <div class="red-line" />
            </el-col>
          </el-row>
          <!--thu nhập chịu thuế-->
          <el-row :gutter="8">
            <el-col :span="24" class="col-class">
              <el-form-item label="Thu nhập chịu thuế">
                <input-number
                  type="input-append"
                  :valueInput="formData.IncomeTaxable"
                  @value="getIncomeTaxable"
                  append="VNĐ"
                  :money="true"
                  :id="'IncomeTaxable'"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <!--thu nhập không chịu thuế-->
          <el-row :gutter="8">
            <el-col :span="24" class="col-class">
              <el-form-item label="Thu nhập không chịu thuế">
                <input-number
                  type="input-append"
                  :valueInput="formData.IncomeNotTaxable"
                  @value="getIncomeNotTaxable"
                  append="VNĐ"
                  :money="true"
                  :id="'IncomeNotTaxable'"
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-col>
        <!--end cột 1-->
        <!--cột 2-->
         <el-col :lg="8" :md="8" :sm="24" :span="8">
           <!--Label Thông tin đóng bảo hiểm-->
          <el-row>
            <el-col :span="24" class="title-bottom-red">
              <div class="main-title">THÔNG TIN ĐÓNG BẢO HIỂM</div>
              <div class="red-line" />
            </el-col>
          </el-row>
          <!--Mức đóng bảo hiểm-->
          <!--Mức đóng bảo hiểm-->
          <el-row :gutter="8">
            <el-col :span="24" class="col-class">
              <el-form-item label="Mức đóng bảo hiểm">
                <input-number
                  type="input-append"
                  :valueInput="formData.InsurancePayment"
                  @value="getInsurancePayment"
                  append="VNĐ"
                  :money="true"
                  :id="'InsurancePayment'"
                />
              </el-form-item>
            </el-col>
          </el-row>

          <!--Tỉ lệ trích bảo hiểm-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class">
              <div class="label-combo-class" for="InsuranceRate">Tỉ lệ trích bảo hiểm</div>
              
                <combo-box
                  selectID="InsuranceRate"
                  type="label-none-v2"
                  label="InsuranceRateName"
                  field="InsuranceRateName"
                  :list="listInsuranceRate"
                  placeholder="Chọn tỉ lệ trích bảo hiểm"
                  :defaultString="InsuranceRate"
                  @value="getValueInsuranceRate"
                />
              
            </el-col>
            <el-col :span="12" class="col-class"></el-col>
          </el-row>
          <!--Bảo hiểm phải nộp-->
          <el-row :gutter="8">
            <el-col :span="12" class="col-class" style="text-align:right;">
              <el-form-item label="Bảo hiểm phải nộp" prop="InsurancePaymentRate ">
              </el-form-item>
              <span>{{0}} VND</span>
            </el-col>
            <el-col :span="12" class="col-class"></el-col>
          </el-row>
         </el-col>
        <!--end cột 2-->
      </el-row>
    </el-form>
  </div>
</template>

<script>
import InputNumber from "@/components/inputs/input-number";
import ComboBox from "@/components/select";
export default {
  components:{
    "input-number": InputNumber,
    "combo-box": ComboBox,
  },
  data() {
    return {
      formData: {
        IncomeTaxable:"",
        IncomeNotTaxable:"",
        InsurancePayment:""
      },
      InsuranceRate:"",
      listInsuranceRate:[
        {
          InsuranceRateName:"12",
        },
         {
          InsuranceRateName:"20",
        }
      ],
    }
  },
  methods: {
    /**
     * Lấy thông tin thu nhập chịu thuế
     * nvnguyen 19/11/2020
     */
    getIncomeTaxable(val) {
      this.formData.IncomeTaxable = val;
    },

    /**
     * tính thu nhập không chịu thuế
     * nvnguyen 19/11/2020
     */
    getIncomeNotTaxable(val) {
      this.formData.IncomeNotTaxable = val;
    },

    /**
    * Lấy mức đóng bảo hiểm
    * nvnguyen 19/11/2020
    */
    getInsurancePayment(val) {
      this.formData.InsurancePayment = val;
    },

    /**
    * Lấy tỉ lệ trích bảo hiểm
    */
    getValueInsuranceRate(){},
  },
};
</script>

<style lang="scss" scoped>
@import "@/styles/variables.scss";
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
// .container-content{
//   padding-top: 10px;
// }

.label-combo-class{
  padding-top: 14px;
  padding-bottom: 5px;
  font-family: $_fontregular;
  font-size: 13px;
  font-weight: bold;
}
</style>