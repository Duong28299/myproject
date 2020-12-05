<template>
  <div>
    <payer-dialog
      :titleDialog="title"
      :id="dialogid"
      :dialogFormVisible="visiableDialog"
      :closeonclickmodal="false"
      @close-dialog="getClose()"
      :fullscreen="true"
    >
      <template v-slot:body >
          <div class="content-dialog" style="height:100%">
            <div class="tab-content">
              <div
                class="tab"
                :class="{ active: activeTab == 'infor' }"
                @click="activeTab = 'infor'"
              >
                1. Thông tin cá nhân
              </div>
              <div
                class="tab"
                :class="{ active: activeTab == 'income' }"
                @click="activeTab = 'income'"
              >
                2. Thu nhập - Bảo hiểm
              </div>
              <div
                class="tab"
                :class="{ active: activeTab == 'dependent' }"
                @click="activeTab = 'dependent'"
              >
                3. Thông tin người phụ thuộc
              </div>
              <div
                class="tab"
                :class="{ active: activeTab == 'history' }"
                @click="activeTab = 'history'"
              >
                4. Lịch sử thu nhập thuế
              </div>
            </div>
            <secret-infor
              class="main-content"
              v-if="activeTab == 'infor'"
              :payerDetail="taxPayerDetail"
              :formMode="formMode"
            >
            </secret-infor>

            <income-infor
              class="main-content"
              v-if="activeTab == 'income'"
              :payerDetail="taxPayerDetail"
              :formMode="formMode"
            ></income-infor>

            <dependent-infor
              class="main-content"
              v-if="activeTab == 'dependent'"
              :payerDetail="taxPayerDetail"
              :formMode="formMode"
            ></dependent-infor>

            <history-infor @click-row-history="openHistoryDialog"
              class="main-content"
              v-if="activeTab == 'history'"
              :employeeID="employeeID"
              :formMode="formMode"
              :resetData="!visiableDialog"
            ></history-infor>
          </div>
      </template>

      <template v-slot:footer>
        <btn
          type="multi-button"
          :name="'Lưu và đóng'"
          :handleClick="sendSubmit"
        />
        <btn
          type="btn-grey"
          style="margin-right: 10px"
          name="Hủy"
          :handleClick="resetForm"
        />
      </template>
    </payer-dialog>
    <detail-info
      :visiableDialog="showDetailHistory"
      @close-history-dialog="showDetailHistory = $event"
      :infoHistory="infoHistory"
      :resetData="!visiableDialog"
    >
    </detail-info>
  </div>
</template>

<script>
import Dialog from "@/components/dialog";
import Button from "@/components/buttons";
import SecretInfor from "./components/secret-infor";
import IncomeInfor from "./components/income-infor";
import HistoryInfor from "./components/history-infor";
import DependentInfor from "./components/dependent-infor";
import dialogHistory from "./components/history-info/history-detail"
import TaxPayer from "@/api/etax/employee.js"

export default {
  components: {
    "payer-dialog": Dialog,
    btn: Button,
    "secret-infor": SecretInfor,
    "income-infor": IncomeInfor,
    "history-infor": HistoryInfor,
    "dependent-infor": DependentInfor,
    "detail-info": dialogHistory,
  },

  props: {
    visiableDialog: {
      type: Boolean,
      default: false,
    },
    // payerDetail: {
    //   type: Object,
    //   default: () => {},
    // },
    formMode: {
      type: Number,
      default: 0,
    },
    employeeID:{
      type:String,
      default:null
    }
  },

  data() {
    return {
      title: "THÔNG TIN CHI TIẾT NGƯỜI NỘP THUẾ",
      dialogid: "tax-payer-dialog",
      activeTab: "infor",
      showDetailHistory: false,
      infoHistory: {},
      taxPayerDetail:{},
      payerInfo:{},
      clearDataDialog:false,
    };
  },

  methods: {
    /**
     * đóng dialog
     * nvnguyen 17/11/2020
     */
    getClose() {
      this.$emit("close-dialog", false);
    },

    /**
     * Lưu thông tin trong dialog
     * todo nvnguyen 17/11/2020
     */
    sendSubmit() {},

    /**
     * reset lại form dialog
     * todo nvnguyen 17/11/2020
     */
    resetForm() {},

    openHistoryDialog(obj){
      this.showDetailHistory= obj.open;
      this.infoHistory= obj.info;
    },
  },

  watch:{
    async employeeID(val){
      if(this.visiableDialog && val){
        var res = await TaxPayer.getTaxPayerByID(val);
        console.log(res.data.Data)
        this.taxPayerDetail = res.data.Data;
      }
    }
  },
  mounted() {
    
  },
};
</script>

<style lang="scss" scoped>
@import "@/styles/variables.scss";
.tab-content {
  display: flex;
  background-color: $background-grey;
  z-index: 10;
  top:0px;
  padding-top: 8px;
  padding-left: 24px;
  height: 48px;
  position: sticky;

  .tab {
    padding: 0 16px 0 16px;
    text-align: center;
    height: 40px;
    border-radius: 5px 5px 0 0;
    line-height: 40px;
    color: $medium-dark-grey;
    cursor: pointer;
  }
  .active {
    background-color: $white;
    color: $primary;
  }
}
.content-dialog {
  background-color: $white;
}

</style>
