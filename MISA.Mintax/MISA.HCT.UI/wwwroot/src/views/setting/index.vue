<template>
  <div class="setting">
    <div class="page-title">
      <h1>Thiết lập</h1>
      <div class="btn-icon-right">
        <m-btn
          type="btn-white2-onlyicon-big"
          tooltip="Làm mới"
          positionTooltip="left"
          :handleClick="getDataOrganization"
        >
          <template v-slot:icon>
            <img
              class="icon"
              src="@/icons/Group 20233.png"
              style="width: 20px; height: 20px"
            />
          </template>
        </m-btn>
      </div>
    </div>
    <div class="tab-content">
      <div class="tab">Thông tin đơn vị</div>
    </div>
    <div class="content-dialog">
      <div v-if="loading" v-loading="loading" class="ct-loading"></div>
      <div class="button-modified">
        <m-btn
          type="btn-white-icon"
          name="Sửa"
          class="btn-edit"
          :handleClick="handleEdit"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18651@2x.png" />
          </template>
        </m-btn>
      </div>
      <div class="body-info">
        <el-row :gutter="24">
          <el-col :lg="12" :md="12" :sm="12">
            <el-row>
              <el-col :span="12" class="text-left contract-tax pb-4">
                <div class="title-info">THÔNG TIN CHUNG</div>
                <div class="tab-active" />
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Mã số thuế:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">
                  {{ organization.OrganizationTaxNo }}
                </div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Tên đơn vị:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">
                  {{ organization.OrganizationName }}
                </div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row" v-if="isOrganizationUnit">
              <el-col :span="8">
                <div class="label-title">Cấp tổ chức:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">
                  {{ organization.OrganizationUnit }}
                </div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row" v-if="isAccounting">
              <el-col :span="8">
                <div class="label-title">Hình thức hạch toán:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">
                  {{ organization.ShowAccounting }}
                </div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Địa chỉ:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.TaxPlaceName }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Tỉnh/Thành phố:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.ProvinceName }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Quận/Huyện:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.DistrictName }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Số điện thoại:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.Phone }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Email:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.Email }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Fax:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.FaxNo }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Cơ quan thuế cấp cục:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">
                  {{ organization.TaxAuthoritiesName }}
                </div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Cơ quan thuế quản lý:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">
                  {{ organization.TaxAuthoritiesManageName }}
                </div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Ngân hàng:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.BankName }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Số tài khoản ngân hàng:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.BankNo }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Người ký tờ khai:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ organization.SignBy }}</div>
              </el-col>
            </el-row>
          </el-col>
          <el-col :lg="12" :md="12" :sm="12">
            <!-- Thông tin đại lý thuế -->
            <el-row>
              <el-col :span="12" class="text-left contract-tax pb-4">
                <div class="title-info">THÔNG TIN ĐẠI LÝ THUẾ</div>
                <div class="tab-active" />
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Tên đại lý thuế:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.TaxAgentName }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Mã số thuế đại lý thuế:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.TaxAgentTaxNo }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Địa chỉ:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.PlaceName }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Tỉnh/Thành phố:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.ProvinceName }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Quận/Huyện:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.DistrictName }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Số điện thoại:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.Phone }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Email:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.Email }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Fax:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.FaxNo }}</div>
              </el-col>
            </el-row>
            <el-row class="title-contract"> Hợp đồng đại lý thuế </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Số hợp đồng:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.ContractNo }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Ngày hợp đồng:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.ContractDate }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Nhân viên đại lý thuế:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">{{ taxAgent.TaxAgentEmployee }}</div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">Chứng chỉ hành nghề số:</div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">
                  {{ taxAgent.PracticingCertifacateNo }}
                </div>
              </el-col>
            </el-row>
            <el-row :gutter="24" class="info-row">
              <el-col :span="8">
                <div class="label-title">
                  Hiển thị thông tin đại lý thuế trên tờ khai:
                </div>
              </el-col>
              <el-col :span="16">
                <div class="label-content">
                  {{ taxAgent.IsShowDeclaration === true ? "Có" : "Không" }}
                </div>
              </el-col>
            </el-row>
          </el-col>
        </el-row>
      </div>
    </div>

    <!-- dialog edit  -->
    <edit-setting-dialog
      :visibleDialog="visibleDialog"
      :organization="organization"
      :taxAgent="taxAgent"
      :listProvincials="listProvincials"
      :listDistricts="listDistricts"
      :listBanks="listBanks"
      :listTaxAuthorities="listTaxAuthorities"
      :listTaxAuthoritiesParent="listTaxAuthoritiesParent"
      @closeEditDialog="visibleDialog = $event"
      @reload="refresh = $event"
    >
    </edit-setting-dialog>
  </div>
</template>

<script>
import Button from "@/components/buttons";
import organizationService from "@/api/etax/organization.js";
import taxAgentSerice from "@/api/etax/taxagent.js";
import { formatDate } from "@/utils/index.js";
import organizationEnum from "@/enums/regexOrganization.js";
import EditSettingDialog from "./components/edit-setting-dialog";
import dictionaryService from "@/api/etax/category-dictionary.js";
import Cookie from "js-cookie";

export default {
  name: "Settings",
  props: {},
  components: {
    "m-btn": Button,
    "edit-setting-dialog": EditSettingDialog,
  },
  data() {
    return {
      loading: true,
      organization: {},
      OrganizationID: "",
      isAccounting: false,
      isOrganizationUnit: false,
      taxAgent: {},
      visibleDialog: false,
      listProvincials: [], //list tỉnh thành
      listDistricts: [], //list quận huyện
      listBanks: [], //list ngân hàng
      listTaxAuthorities: [], //list cơ quan thuế cấp cục
      listTaxAuthoritiesParent: [], //list cơ quan thuế quản lý
      refresh: false,
    };
  },
  watch: {
    visibleDialog: {
      async handler(val) {
        if (val) {
          this.getListProvincials();
          this.getListDistricts(this.organization.provinceID);
          this.getListBanks();
          this.getListTaxAuthorities();
          this.getListTaxAuthoritiesParent(this.organization.CAUnitName);
        }
      },
    },

    refresh: {
      async handler(val) {
        if (val) {
          this.getDataOrganization();
        }
      },
    },
  },

  async created() {
    this.OrganizationID = Cookie.get("OrganizationID");
    this.getDataOrganization();
  },

  methods: {
    handleEdit() {
      this.visibleDialog = true;
    },

    /* Hàm gọi service lấy dữ liệu tỉnh thành phố
     * createdby ndhuy 01.09.2020
     */
    async getListProvincials() {
      var res = await dictionaryService.getProvincials();
      if (typeof res.data != "undefined" && res.data.Success)
        this.listProvincials = res.data.Data;
      else this.listProvincials = [];
    },

    /**
     * Hàm gọi service lấy dữ liệu quận huyện theo mã id tỉnh thành phố
     * @param provinceID mã code tỉnh thành phố
     * createdby ndhuy 01.09.2020
     */
    async getListDistricts(provinceID) {
      var res = await dictionaryService.getDistrictsByDistrictsID(provinceID);
      if (typeof res.data != "undefined" && res.data.Success)
        this.listDistricts = res.data.Data;
      else this.listDistricts = [];
    },

    /**
     * Hàm gọi service lấy dữ liệu ngân hàng
     * createdby ndhuy 01.09.2020
     */
    async getListBanks() {
      const res = await dictionaryService.getBanks();
      this.listBanks = res.data.Data;
    },

    /**
     * Hàm gọi service lấy dữ liệu danh sách cơ quan thuế cấp cục
     * createdby ndhuy 01.09.2020
     */
    async getListTaxAuthorities() {
      const res = await dictionaryService.getTaxAuthorities();
      this.listTaxAuthorities = res.data.Data;
    },

    /**
     * Hàm gọi service lấy dữ liệu cơ quan thuế quản lý theo cơ quan thuế cấp cục
     * createdby ndhuy 01.09.2020
     */
    async getListTaxAuthoritiesParent(val) {
      const res = await dictionaryService.getListTaxAuthoritiesParent(val);
      this.listTaxAuthoritiesParent = res.data.Data;
    },
    /**
     * Hàm gọi service thực hiện lấy thông tin tổ chức
     * @param organizationId id tổ chức
     * CreatedBy ndhuy 31.08.2020
     * Modified by bvbao 24/11/2020
     */
    async getOrganizationInfo(organizationId) {
      var res = await organizationService.getOrganizationById(organizationId);
      if (typeof res.data != "undefined" && res.data.Success) {
        this.mapDataServiceToOrganization(res.data.Data);
      } else {
        this.organization = {};
      }
    },

    /**
     * Hàm thực hiện đổi data lấy từ service về obj organization
     * createdby ndhuy 31.08.2020
     * Modified by bvbao 24/11/2020
     */
    mapDataServiceToOrganization(data) {
      this.organization = Object.assign({}, data);
      if (data.OrganizationUnit == "Công ty con") {
        this.isOrganizationUnit = true;
      } else if (data.OrganizationUnit == "Chi nhánh") {
        this.isOrganizationUnit = true;
        this.isAccounting = true;
        this.organization.ShowAccounting = organizationEnum.findInArray(
          data.Accounting
        );
      } else {
        this.isOrganizationUnit = false;
        this.isAccounting = false;
      }
    },

    /**
     * Hàm gọi service thực hiện lấy thông tin tổ chức
     * @param taxAgentId id đơn vị thuế
     * CreatedBy ndhuy 31.08.2020
     */
    async getTaxAgent(organizationID) {
      var res = await taxAgentSerice.getTaxAgentByOrganizationID(
        organizationID
      );
      if (typeof res.Data != "undefined" && res.Success) {
        this.taxAgent = Object.assign({}, res.Data[0]);
        this.taxAgent.ContractDate = formatDate(
          res.Data[0].ContractDate,
          "mm-dd-yyyy"
        );
        // Hiển thị thông tin đại lý thuế trên tờ khai: 1- Có, 0- Không
        this.taxAgent.IsShowDeclaration =
          res.Data[0].IsShowDeclaration === 1 ? true : false;
      } else {
        this.taxAgent = [];
      }
    },

    async getDataOrganization() {
      this.loading = true;
      await this.getOrganizationInfo(this.OrganizationID);
      await this.getTaxAgent(this.OrganizationID);
      this.refresh = false;
      this.loading = false;
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
.setting {
  .ct-loading {
    .el-loading-mask {
      background-color: $white;
      opacity: 1;
    }
  }
}
</style>

<style lang="scss" scoped>
@import "@/styles/variables.scss";
.setting {
  height: 100%;

  .page-title {
    margin: 0 -24px 0 -24px;
    background-color: $white;
    align-items: center;
    display: flex;

    h1 {
      padding: 24px 24px 16px 24px;
      background-color: $white;
      margin: 0;
    }

    .btn-icon-right {
      position: absolute;
      right: 16px;
    }
  }

  .tab-content {
    width: 100%;
    height: 60px;
    display: flex;
    padding-top: 12px;
    z-index: 10;

    .tab {
      padding: 0 18px;
      text-align: center;
      height: 48px;
      border-radius: 5px 5px 0 0;
      line-height: 50px;
      cursor: pointer;
      font-family: $_fontbold;
      background-color: $white;
    }
  }

  .content-dialog {
    margin: 0 -24px 0 -24px;
    height: calc(100% - 130px);
    background-color: $white;
    padding: 24px 32px;
    overflow: auto;
    position: relative;

    .ct-loading {
      position: absolute;
      top: 0;
      left: 0;
      bottom: 0;
      right: 0;
      z-index: 10;

      .el-loading-mask {
        background-color: red;
        opacity: 1;
      }
    }

    .button-modified {
      height: 40px;
      .button-container {
        float: left;
        .btn-white-icon {
          padding: 0 25px !important;
        }
      }
    }

    .body-info {
      margin-top: 20px;
      .title-info {
        font-weight: bold;
        color: $primary;
        font-family: $_fontbold;
      }

      .label-title {
        color: $medium-dark-grey;
        font-family: $_fontregular;
      }

      .label-content {
        font-family: $_fontmedium;
      }

      .tab-active {
        margin-top: 5px;
        width: 44px;
        bottom: 0px;
        height: 1px;
        background-color: $seconds;
        position: unset;
      }
      .info-row {
        line-height: 30px;
      }
      .title-contract {
        font-family: $_fontbold;
        padding-top: 6px;
      }
    }
  }
}
</style>