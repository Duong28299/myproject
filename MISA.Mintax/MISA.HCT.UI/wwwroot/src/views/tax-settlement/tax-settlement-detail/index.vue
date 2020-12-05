<template>
  <div class="tax-settlement-detail">
    <div class="main">
      <!-- Phần page header -->
      <div v-if="loading" v-loading="loading" class="total-loading"></div>
      <el-row class="page-detail-header" v-if="!loading">
        <el-col :span="14" class="left">
          <div class="header-detail">
            <span id="img-back" @click="goBack">
              <img src="@/icons/ico-expand.png" />
            </span>
            <span class="page-title">
              <p class="heading">
                {{ this.DeclarationSettlementDetail.DeclarationSettlementName }}
              </p>
            </span>
          </div>
          <div class="detail">
            <div class="label">
              [01] Kỳ tính thuế:
              <strong class="highlight-text">
                Năm {{ DeclarationSettlementDetail.SettlementYear }}</strong
              >
            </div>
            <div class="label">
              <strong class="highlight-text">
                {{ declarationtypeString }}</strong
              >
            </div>
            <div class="label">{{ settlementTime }}</div>
            <div class="label">
              Từ <strong class="highlight-text">{{ startMonth }}</strong> đến
              <strong class="highlight-text">{{ endMonth }}</strong>
            </div>
          </div>
        </el-col>

        <el-col :span="10" class="right">
          <m-btn
            class="fl"
            :type="'btn-primary-icon'"
            :name="'Ký nộp qua mTax'"
            :handleClick="sendDeclaration"
          >
            <template v-slot:icon>
              <img class="icon" src="@/icons/Group 18580@2x.png" />
            </template>
          </m-btn>
          <m-btn
            style="margin-right: 16px"
            :type="'multi-button2'"
            :name="'Tải về'"
          >
            <template v-slot:icon>
              <img class="icon" src="@/icons/Component 95 – 1.png" />
            </template>
          </m-btn>
        </el-col>
      </el-row>
      <!--Hết Phần page header -->
      <div class="content" v-if="!loading">
        <div class="info-declare">
          <el-row v-if="visiablereason" style="padding-top: 24px">
            <el-col :lg="12" :md="12" :sm="24" class="reason">
              <div class="reason-title">Lý do</div>
              <field-input
                id="reason-input"
                type="field-input"
                :valueInput="DeclarationSettlementDetail.Reason"
                @value="DeclarationSettlementDetail.Reason = $event"
                :submitFuntion="submitReason"
                :disabled="UpdateDeclaration"
                placeholder="Nhập lý do"
              />
            </el-col>
            <el-col :lg="12" :md="12" :sm="0"></el-col>
          </el-row>
          <div class="info-declare-title">
            <h3 class="main-title">THÔNG TIN ĐƠN VỊ KÊ KHAI</h3>
            <el-button type="info" @click="dropdownInfo" id="btn-hide-display"
              >{{ CollapseOrExpand
              }}<i style="margin-left: 8px" :class="iconCollapse"></i
            ></el-button>
          </div>
          <div class="info-declare-main" v-if="!isCollapse">
            <el-row :gutter="24">
              <el-col :lg="12" :md="12" :sm="24">
                <div class="small-title">
                  <strong>Người nộp thuế</strong>
                  <div class="border-bottom"></div>
                </div>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">[04] Tên người nộp thuế:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">{{
                      DeclarationSettlementDetail.OrganizationName
                    }}</span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">[05] Mã số thuế:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">{{
                      DeclarationSettlementDetail.OrganizationTaxNo
                    }}</span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">[06] Địa chỉ:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.TaxPlace }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">
                      [09] Số điện thoại liên hệ:
                    </div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.OrganizationPhone }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">[10] Email:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.OrganizationEmail }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">
                      [48] Cơ quan thuế cấp cục:
                    </div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.CaunitName }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">
                      [49] Cơ quan thuế quản lý:
                    </div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.CaunitManage }}
                    </span>
                  </el-col>
                </el-row>
              </el-col>
              <el-col :lg="12" :md="12" :sm="24" v-if="!disableTaxAgent">
                <div class="small-title">
                  <strong>Đại lý thuế</strong>
                  <div class="border-bottom"></div>
                </div>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">[12] Tên đại lý thuế:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.TaxAgentName }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">[13] Mã số thuế:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.TaxAgentTaxNo }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">[06] Địa chỉ:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.TaxAgentPlaceName }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">
                      [09] Số điện thoại liên hệ:
                    </div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.TaxAgentPhone }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">[10] Email:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.TaxAgentEmail }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">
                      [20] Hợp đồng với đại lý thuế:
                    </div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.ContractNo }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">Ngày:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{
                        convertDateTime(
                          DeclarationSettlementDetail.ContractDate
                        )
                      }}
                    </span>
                  </el-col>
                </el-row>
              </el-col>
            </el-row>
          </div>
          <div
            class="info-declare-main"
            v-if="!isCollapse"
            style="margin-top: 15px"
          >
            <el-row :gutter="24">
              <el-col :lg="12" :md="12" :sm="24">
                <div class="small-title">
                  <strong>Thông tin ký</strong>
                  <div class="border-bottom"></div>
                </div>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">Họ và tên người ký:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.SignBy }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">Ngày ký:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{
                        convertDateTime(DeclarationSettlementDetail.SignDate)
                      }}
                    </span>
                  </el-col>
                </el-row>
              </el-col>
              <el-col :lg="12" :md="12" :sm="24" v-if="!disableTaxAgent">
                <div class="small-title">
                  <strong>Nhân viên đại lý thuế</strong>
                  <div class="border-bottom"></div>
                </div>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">Họ tên:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.TaxAgentEmployee }}
                    </span>
                  </el-col>
                </el-row>
                <el-row :gutter="24">
                  <el-col :span="8">
                    <div class="color-text-label">Chứng chỉ hành nghề số:</div>
                  </el-col>
                  <el-col :span="16">
                    <span class="txt-value-lable">
                      {{ DeclarationSettlementDetail.PracticingCertifacateNo }}
                    </span>
                  </el-col>
                </el-row>
              </el-col>
            </el-row>
          </div>
        </div>
      </div>
      <div class="tab" v-if="!loading">
        <div class="tool-tab">
          <btn
            name="Tờ khai 05-QTT-TNCN"
            type="btn-tab"
            :active="activeTab == listTab.gentab"
            :handleClick="
              () => {
                activeTab = listTab.gentab;
                activeTabSelected();
              }
            "
          />
          <btn
            name="Phụ lục 05-1BK-QTT-TNCN"
            type="btn-tab"
            :active="activeTab == listTab.total"
            :handleClick="
              () => {
                activeTab = listTab.total;
                activeTabSelected();
              }
            "
          />
          <btn
            name="Phụ lục 05-2BK-QTT-TNCN"
            type="btn-tab"
            :active="activeTab == listTab.progressive"
            :handleClick="
              () => {
                activeTab = listTab.progressive;
                activeTabSelected();
              }
            "
          />
          <btn
            name="Phụ lục 05-3BK-QTT-TNCN"
            type="btn-tab"
            :active="activeTab == listTab.dependent"
            :handleClick="
              () => {
                activeTab = listTab.dependent;
                activeTabSelected();
              }
            "
          />
        </div>
      </div>
      <div class="contain-tab" v-if="!loading">
        <div v-if="activeTab == listTab.gentab">
          <AppendixTotal
            :DeclarationSettlementDetail="DeclarationSettlementDetail"
          />
        </div>
        <div v-if="activeTab == listTab.total">
          <appendix1bk @reload="reload = $event" />
        </div>
        <div v-if="activeTab == listTab.progressive">
          <appendix2bk
            :DeclarationStatus="DeclarationSettlementDetail.DeclarationStatus"
            @reload="reload = $event"
          />
        </div>
        <div v-if="activeTab == listTab.dependent">
          <DependentComponent />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Button from "@/components/buttons";
import fieldinput from "@/components/inputs";
import btn from "@/components/buttons";
import DeclarationEnum from "@/enums/declaration-settlement";
import DeclarationSettlementService from "@/api/etax/declaration-settlement";
import DependentComponent from "./components/appendix-03BK";
import AppendixTotal from "./components/appendix-total";
import appendix2bk from "./components/appendix-2BK";
import Cookies from "js-cookie";
import appendix1bk from "./components/appendix-01BK";
import signDeclaration from "@/mixins/signDeclaration";

export default {
  name: "DeclarationSettlementDetail",
  components: {
    "m-btn": Button,
    "field-input": fieldinput,
    btn,
    DependentComponent,
    appendix2bk,
    AppendixTotal,
    appendix1bk,
  },
  mixins: [signDeclaration],
  data() {
    return {
      loading: true,
      reload: false,
      visiablereason: false,
      listTab: {
        gentab: "gentab", // tổng
        total: "total", //toàn phần
        progressive: "progressive", //lũy tiến
        dependent: "dependent", // người phụ thuộc
      },
      CollapseOrExpand: "Chi tiết",
      DeclarationSettlement: {
        name: "",
        ID: "",
      },
      activeTab: "gentab",
      DeclarationSettlementDetail: {},
      isCollapse: true,
      iconCollapse: "el-icon-arrow-down",
      disableTaxAgent: true,
      UpdateDeclaration: true,
      originalReason: "",
    };
  },
  computed: {
    declarationtypeString() {
      var num =
        this.DeclarationSettlementDetail.NumberOfDeclaration < 10
          ? `0${this.DeclarationSettlementDetail.NumberOfDeclaration}`
          : this.DeclarationSettlementDetail.NumberOfDeclaration;
      return (
        DeclarationEnum.findInArray(
          this.DeclarationSettlementDetail.DelarationType
        ) +
        " - " +
        num
      );
    },
    settlementTime() {
      if (
        this.DeclarationSettlementDetail.FinishSettlementMonth -
          this.DeclarationSettlementDetail.StarSettlementMonth ==
        11
      ) {
        return "Quyết toán tròn năm";
      }
      return "Quyết toán không tròn năm";
    },
    startMonth() {
      var month =
        this.DeclarationSettlementDetail.StarSettlementMonth < 10
          ? `0${this.DeclarationSettlementDetail.StarSettlementMonth}`
          : this.DeclarationSettlementDetail.StarSettlementMonth;
      return `${month}/${this.DeclarationSettlementDetail.SettlementYear}`;
    },
    endMonth() {
      var month =
        this.DeclarationSettlementDetail.FinishSettlementMonth < 10
          ? `0${this.DeclarationSettlementDetail.FinishSettlementMonth}`
          : this.DeclarationSettlementDetail.FinishSettlementMonth;
      return `${month}/${this.DeclarationSettlementDetail.SettlementYear}`;
    },
  },
  watch: {
    reload: {
      async handler(val) {
        if (val) {
          await this.getDeclarationSettlement();
          this.reload = false;
        }
      },
    },
    DeclarationSettlementDetail: {
      async handler(val) {
        if (val.DeclarationStatus === 1)
          // chưa nộp
          this.UpdateDeclaration = false;
        else this.UpdateDeclaration = true;
      },
      deep: true,
    },
    isCollapse(val) {
      if (val) {
        this.iconCollapse = "el-icon-arrow-down";
        this.CollapseOrExpand = "Chi tiết";
      } else {
        this.iconCollapse = "el-icon-arrow-up";
        this.CollapseOrExpand = "Thu gọn";
      }
    },
  },
  async created() {
    this.loading = true;
    this.initDataMixins();
    this.DeclarationSettlement.ID = this.$route.params.id;
    await this.getDeclarationSettlement();
    if (this.DeclarationSettlementDetail.DeclarationStatus === 1)
      // chưa nộp
      this.UpdateDeclaration = false;
    else this.UpdateDeclaration = true;
    if (
      this.DeclarationSettlementDetail.FinishSettlementMonth -
        this.DeclarationSettlementDetail.StarSettlementMonth !=
      11
    )
      this.visiablereason = true;

    this.showTaxAgent();
    this.isCollapse = Boolean(Cookies.get("isCollapse") == "false" ? 0 : 1);
    this.loading = false;
  },
  methods: {
    /**
     * Hàm bắt sự kiện chọn tab tờ khai, phụ lục trong tax-settlement-detail
     * gán tab hiện tại vào cookie
     * nvnguyen 05/11/2020
     */
    activeTabSelected() {
      Cookies.remove("activeTab");
      switch (this.activeTab) {
        case this.listTab.gentab:
          Cookies.set("activeTab", this.listTab.gentab);
          break;
        case this.listTab.total:
          Cookies.set("activeTab", this.listTab.total);
          break;
        case this.listTab.progressive:
          Cookies.set("activeTab", this.listTab.progressive);
          break;
        case this.listTab.dependent:
          Cookies.set("activeTab", this.listTab.dependent);
          break;
      }
    },
    /**
     * Hàm sự kiện khi click mũi tên quay lại
     * createdby nvanh 15.10.2020
     */
    goBack() {
      this.$router.push("/tax-settlement");
    },

    /**
     * Hàm sử lý submit của Lý do
     * createdBy NVANH 20.10.2020
     */
    async submitReason() {
      document.getElementById("reason-input").blur();
      await this.updateDeclarationSettlement();
    },

    /**
     * Hàm xử lý nút thu gọn
     * CreatedBy NVANH 20.10.2020
     */
    dropdownInfo() {
      if (Cookies.get("isCollapse") != null) Cookies.remove("isCollapse");
      Cookies.set("isCollapse", !this.isCollapse);

      this.isCollapse = Boolean(Cookies.get("isCollapse") == "false" ? 0 : 1);
      if (this.isCollapse) {
        this.iconCollapse = "el-icon-arrow-down";
        this.CollapseOrExpand = "Chi tiết";
      } else {
        this.iconCollapse = "el-icon-arrow-up";
        this.CollapseOrExpand = "Thu gọn";
      }
    },

    /**
     * Hàm lấy dữ liệu
     * CreatedBy NVANH 20.10.2020
     */
    async getDeclarationSettlement() {
      const res = await DeclarationSettlementService.getDeclarationSettlementByID(
        this.DeclarationSettlement.ID
      );
      if (res.data.Success && res.data.Data) {
        this.DeclarationSettlementDetail = res.data.Data;
        this.originalReason = this.DeclarationSettlementDetail.Reason;
      } else {
        this.$_Notification.error("Thất bại", "Lỗi không tìm thấy dữ liệu");
      }
    },

    async updateDeclarationSettlement() {
      this.loading = true;
      const res = await DeclarationSettlementService.putDeclarationSettlement(
        this.DeclarationSettlementDetail
      );
      if (res.data.Success) {
        this.originalReason = this.DeclarationSettlementDetail.Reason;
        this.$_Notification.success(
          "Thành công",
          "Cập nhật dữ liệu tờ khai thành công"
        );
      } else {
        this.$_Notification.error("Thất bại", res.data.Message);
        this.DeclarationSettlementDetail.Reason = this.originalReason;
      }
      this.loading = false;
    },

    showTaxAgent() {
      if (
        !this.DeclarationSettlementDetail.TaxAgentName ||
        !this.DeclarationSettlementDetail.TaxAgentTaxNo ||
        !this.DeclarationSettlementDetail.TaxAgentPlaceName ||
        !this.DeclarationSettlementDetail.TaxAgentEmail ||
        !this.DeclarationSettlementDetail.TaxAgentPhone ||
        !this.DeclarationSettlementDetail.ContractNo ||
        !this.DeclarationSettlementDetail.ContractDate ||
        !this.DeclarationSettlementDetail.TaxAgentEmployee ||
        !this.DeclarationSettlementDetail.PracticingCertifacateNo
      ) {
        this.disableTaxAgent = true;
      } else {
        this.disableTaxAgent = false;
      }
    },

    convertDateTime(datetime) {
      if (datetime)
        return datetime.split("T")[0].split("-").reverse().join("-");
      else return "";
    },

    sendDeclaration() {
      this.dataSign.push(this.DeclarationSettlementDetail);
      this.registerByMTAX();
    },
    /**
     * Khởi tạo giá trị đối với mixin kí tờ khai
     * created by nvbinh2 11.11.2020
     */
    initDataMixins() {
      this.keyProperty = "DeclarationSettlementID";
      this.mode = "DeclarationSettlement";
      this.nameofDeclaration = "DeclarationSettlementName";
    },
  },
  mounted() {
    //set activeTab nvnguyen
    var cookieTab = Cookies.get("activeTab");
    if (cookieTab) {
      this.activeTab = cookieTab;
    }
  },
};
</script>

<style lang="scss">
@import "@/styles/typo.scss";
@import "@/styles/variables.scss";

#main {
  overflow-x: hidden;
}

#btn-hide-display {
  margin-left: 24px;
  background: none;
  color: $light-primary;
  border: none;

  &:hover {
    background-color: $background-grey;
  }

  span {
    font-family: $_fontbold;
  }
}

.tax-settlement-detail {
  height: 100%;
  position: relative;

  .total-loading {
    position: absolute !important;
    top: 0;
    left: -24px;
    right: -24px;
    bottom: 0;

    .el-loading-mask{
      background-color: $background-grey;
    }
  }

  .page-detail-header {
    height: 101px;
    display: flex;
    margin: 0 -24px;
    padding: 0 24px;
    position: sticky;
    top: 0;
    background-color: $white;
    z-index: 10;

    .header-detail {
      width: 100%;
      display: flex;
    }

    .left {
      display: flex;
      align-items: center;
      flex-flow: column;
      width: 100%;

      #img-back {
        display: flex;
        align-items: center;
        justify-content: center;
        flex-flow: column-reverse;
        margin-top: 22px;
        margin-right: 8px;
        height: 36px;
        width: 36px;
        cursor: pointer;

        img {
          padding: 0 10px 0 10px;
        }
      }

      .page-title {
        line-height: 36px;

        .heading {
          font-family: $_fontbold;
          margin: 22px 0px 16px 0px;
        }
      }
    }

    .right {
      margin-top: 22px;
      float: right;
    }
  }

  .main {
    margin: 0 -24px 0 -24px;
    padding: 0 24px;
    background-color: $white;

    .tab {
      margin: 0 -24px;
      padding: 0 24px;
      position: sticky;
      top: 101px;
      background-color: $white;
      z-index: 10;

      .tool-tab {
        border-top: 1px solid $background-grey;
        margin-top: 24px;
        height: 52px;
        width: 100%;
        box-sizing: content-box;

        .el-button--medium {
          height: 52px !important;
          border-radius: 0px;
        }

        .button-container {
          float: left;

          .tabbar button {
            border: none;

            span {
              font-family: $_fontmedium;
              color: $grey;
            }
          }

          .btn-tab-active span {
            font-family: $_fontmedium;
            color: $dark !important;
          }
        }
      }
    }
  }

  .el-col .el-row {
    padding-bottom: 7px;
    padding-top: 7px;
  }

  .contain-tab {
    background-color: $background-grey;
    margin: 0 -24px;
    padding: 0 24px;
    position: relative;
  }

  .detail {
    display: flex;
    align-self: flex-start;
    padding: 0 10px;
    height: 16px;

    .label {
      color: $medium-dark-grey;
      padding: 0 14px;
      border-right: 1px solid $bg-grey;
    }

    .highlight-text {
      font-family: $_fontbold;
      color: $hover-primary;
    }
  }

  .content {
    height: calc(100% - 101px);
    padding: 0px 24px 0 24px;
    z-index: 10;

    .info-declare-title {
      display: flex;
      margin-top: 24px;
      margin-bottom: 16px;
      height: 32px;
      align-items: center;

      .main-title {
        font-family: $_fontbold;
        font-size: 15px;
      }
    }

    .txt-value-lable {
      font-family: $_fontmedium;
      font-size: 12px;
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

    .detail {
      margin-bottom: 20px;
      margin-left: 36px;
      display: flex;

      .label {
        color: $medium-dark-grey;
        margin: 0 14px;
      }

      .highlight-text {
        font-weight: bolder;
        color: $hover-primary;
      }
    }

    .reason {
      .reason-title {
        line-height: 40px;
        margin-right: 8px;
        font-family: $_fontmedium;
      }

      .input-component {
        width: 90%;
      }

      .reason-input {
        width: 100%;
      }
    }

    .info-declare {
      .color-text-label {
        color: $medium-dark-grey;
        font-family: $_fontregular;
        font-size: 12px;
      }

      .info-declare-main {
        .small-title {
          margin-bottom: 10px;

          strong {
            font-family: $_fontbold;
            font-size: 13px;
            margin-bottom: 6px;
          }

          .border-bottom {
            width: 72px;
            border-bottom: 1px solid $seconds;
          }
        }
      }
    }
  }

  .button-container {
    float: right;
  }

  .reason {
    display: flex;
  }
}
</style>
