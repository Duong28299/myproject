<template>
  <dialog-component
    :id="'history-detail-dialog'"
    :titleDialog="getTitleDialog()"
    :dialogFormVisible="visiableDialog"
    v-if="visiableDialog"
    @close-dialog="getClose"
    :version="2"
    xl="8,9"
    lg="8,9"
    md="9,10"
    sm="4,3"
    xs="7,8"
    :closeonclickmodal="false"
  >
    <template v-slot:body>
      <el-form class="content-dialog">
        <el-row :gutter="40" class="general-info">
          <el-col :span="6" class="general-cell first-cell" style="">
            <div class="general-cell-main">
              <div class="general-cell-info">Tổng thu nhập</div>
              <div class="general-cell-value">
                {{ formatMoney(infoHistory.TotalIncome) }} {{ currencyType }}
              </div>
            </div>
          </el-col>
          <el-col :span="6" class="general-cell" style="">
            <div class="general-cell-main">
              <div class="general-cell-info">Tổng giảm trừ</div>
              <div class="general-cell-value">
                {{ formatMoney(infoHistory.TotalReduction) }} {{ currencyType }}
              </div>
            </div>
          </el-col>
          <el-col :span="6" class="general-cell" style="">
            <div class="general-cell-main">
              <div class="general-cell-info">Thu nhập tính thuế</div>
              <div class="general-cell-value">
                {{ formatMoney(infoHistory.TotalIncomeTaxable) }} {{ currencyType }}
              </div>
            </div>
          </el-col>
          <el-col :span="6" class="general-cell last-cell" style="">
            <div class="general-cell-main">
              <div class="general-cell-info">Thuế thu nhập cá nhân</div>

              <div class="general-cell-value">
                {{ formatMoney(infoHistory.TotalTaxWithHeld) }} {{ currencyType }}
              </div>
            </div>
          </el-col>
        </el-row>

        <el-row :gutter="40" class="main-history-info">
          <el-col :span="12" class="main-history-content-left">
            <div class="title-bottom-red">
              <div class="main-title">THÔNG TIN THU NHẬP, THUẾ</div>
              <div class="red-line" />
            </div>
            <div>
              <div class="info">
                <div class="mt-8">
                  Thu nhập chịu thuế<span class="span-money"><span class="span-medium">{{ formatMoney(infoHistory.IncomeTaxable) }}</span> {{ currencyType }}</span> 
                </div>
                <div class="mt-8">
                  Thu nhập không chịu thuế<span class="span-money"><span class="span-medium">{{ formatMoney(infoHistory.IncomeNotTaxable) }}</span> {{ currencyType }}</span>
                </div>
              </div>
            </div>
          </el-col>
          <el-col :span="12" class="main-history-content-right">
            <div class="title-bottom-red">
              <div class="main-title">THÔNG TIN GIẢM TRỪ</div>
              <div class="red-line" />
            </div>
            <div class="info">
              <div class="reduce-info-container">
                <div class="reduce-info-general">
                  Tổng giảm trừ gia cảnh<span class="span-money"><span class="span-bold">{{formatMoney(infoHistory.TotalReduction)}}</span> {{ currencyType }}</span>
                </div>
                <div class="reduce-detail-info mt-8">
                  Giảm trừ bản thân<span class="span-money"><span class="span-medium">{{ formatMoney(infoHistory.SelfReduction) }}</span> {{ currencyType }}</span>
                </div>
                <div class="reduce-detail-info mt-8">
                  Giảm trừ người phụ thuộc<span class="span-money"><span class="span-medium">{{ formatMoney(infoHistory.DependentReduction) }}</span> {{ currencyType }}</span>
                </div>
              </div>
              <div class="reduce-info-container mt-16">
                <div class="reduce-info-general">Bảo hiểm</div>
                <div class="reduce-detail-info mt-8">
                  Bảo hiểm phải nộp<span class="span-money"><span class="span-medium">{{ formatMoney(infoHistory.InsurancePayment) }}</span> {{ currencyType }}</span>
                </div>  
              </div>
              <div class="reduce-info-container mt-16">
                <div class="reduce-info-general">
                  Giảm trừ khác<span class="span-money"><span class="span-bold">{{ formatMoney(infoHistory.OtherReduction) }}</span> {{ currencyType }}</span>
                </div>
                <div class="reduce-detail-info mt-8">
                  Từ thiện, nhân đạo, khuyến học<span class="span-money"><span class="span-medium">{{ formatMoney(infoHistory.IncomeNotTaxable) }}</span> {{ currencyType }}</span>
                </div>
                <div class="reduce-detail-info mt-8">
                  Quỹ hưu trí tự nguyện<span class="span-money"><span class="span-medium">{{ formatMoney(infoHistory.IncomeNotTaxable) }}</span> {{ currencyType }}</span>
                </div>
              </div>
            </div>
          </el-col>
        </el-row>
      </el-form>
    </template>
    <template v-slot:footer>
      <btn
        type="btn-primary"
        style="margin-right: 10px"
        name="Đóng"
        :handleClick="resetForm"
      />
    </template>
  </dialog-component>
</template>

<script>
import dialog from "@/components/dialog";
import button from "@/components/buttons";
import { formatMoney } from "@/utils/index.js";

export default {
  components: {
    "dialog-component": dialog,
    btn: button,
  },

  props: {
    visiableDialog: {
      type: Boolean,
      default: false,
    },
    infoHistory: {
      type: Object,
      default: null,
    },
  },

  data() {
    return {
      PeriodMonthOrQuater: 3,
      PeriodType: 1,
      PeriodyYear: 2020,
      currencyType: "VNĐ",
    };
  },

  watch: {},

  methods: {
    /**
     * Hàm thực hiện định dạng tiền tệ
     * created by vdthang 28.11.2020
     */
    formatMoney(val){
      return formatMoney(val, ",");
    },

    /**
     * Hàm thực hiện build title dialog
     * created by vdthang 28.11.2020
     */
    getTitleDialog() {
      return `Chi tiết thu nhập ${this.infoHistory.PeriodMonthOrQuarter.toLowerCase()} năm ${
        this.infoHistory.PeriodYear
      } `;
    },

    /**
     * Hàm thực hiện đòng dialog
     * created by vdthang 28.11.2020
     */
    getClose() {
      this.$emit("close-history-dialog", false);
    },

    /**
     * Hàm thực hiện reset form về giá trị mặc định ban đầu
     * created by vdthang 28.11.2020
     */
    resetForm() {
      this.$emit("close-history-dialog", false);
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/styles";
#history-detail-dialog {

  .content-dialog { 
    height: 414px;

    .general-info {
      margin: 0 !important;
      height: 77px;

      .general-cell {
        height: 100%;
        padding: 0px 8px !important;

        .general-cell-value {
          position: absolute;
          bottom: 16px;
          right: 10px;
          font-size: 15px;
          font-family: $_fontbold;

        }

        .general-cell-main {
          position: relative;
          height: 100%;
          padding: 16px 10px 16px 10px;
          border-radius: 5px;
          background-color: $bg-color-1;

          .general-cell-info{
            color: $medium-dark-grey;
          }
        }
      }

      .first-cell{
        padding-left: 0 !important;
      }

      .last-cell{
        padding-right: 0 !important;
      }
    }

    .main-history-info {
      padding-top: 16px;
      margin: 0 !important;

      .reduce-info-container {

        .reduce-info-general {
          height: 33px;
          position: relative;
          display: flex;
          align-items: center;
          font-family: $_fontbold;

          .span-money {
            position: absolute;
            right: 0;
          }
        }
      }

      .main-history-content-left{
        padding-left: 0 !important;
        padding-right: 16px !important;
      }

      .main-history-content-right{
        padding-right: 0 !important;
        padding-left: 16px !important;
      }

      .span-money {
        float: right;
        color: $medium-dark-grey;
      }

      .span-medium{
        color: $dark;
        font-family: $_fontmedium;
      }

      .span-bold {
        color: $dark;
        font-family: $_fontbold;
      }
    }
  }  
}
</style>
<style lang="scss">
#history-detail-dialog {

  .el-dialog__body {
    padding: 0px 32px;
  }

  .el-dialog__header {
    height: 68px;
    display: flex;
    align-items: center;
    padding: 0px 24px 0px 24px;
  }

  .title-bottom-red {
    height: 38px;
  }
}
</style>
