<template>
  <div>
    <div class="logo">
      <img class="img-menu" src="@/assets/images/Union 28@2x.png" />
      <img class="img-logo" src="@/assets/images/Group 17877@2x.png" />
    </div>
    <div class="company">
      <div class="company-container">
        <div class="company-unit">
          <div class="company-title">
            <h1>Danh sách đơn vị bạn quản lý</h1>
          </div>

          <div class="btn-company-mode">
            <button-icon
              type="btn-primary-icon"
              name="Thêm mới đơn vị"
              :handleClick="() => $router.push('getting-started/unit')"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group 17812.png" />
              </template>
            </button-icon>
          </div>
        </div>
        <div class="company-body">
          <div :class="{ 'opacity-background': loading }"></div>
          <div v-loading="true" v-if="loading" :class="icon - loading"></div>
          <div class="test" v-if="!loading">
            <div
              class="company-body-header"
              @click="dblClick(Organ.OrganizationID, Organ.OrganizationName)"
            >
              <div class="company-icon-main">
                <img class="icon-main" src="@/icons/Group 18663.png" />
              </div>
              <div class="company-child-content">
                <h3>{{ Organ.OrganizationName }}</h3>
                <div class="d-flex container-child">
                  <div class="pre-title">Mã số thuế:</div>
                  <div class="content-title">{{ Organ.OrganizationTaxNo }}</div>
                  
                </div>
                <div class="d-flex container-child">
                  <div class="pre-title">Cấp tổ chức:</div>
                  <div class="content-title">Tổng công ty</div>                   
                </div>
              </div>
            </div>
            <div v-for="(formData, index) in formDatas" v-bind:key="index">
              <div
                class="company-body-child"
                @click="
                  dblClick(formData.OrganizationID, formData.OrganizationName)
                "
                v-if="formData.OrganizationID != Organ.OrganizationID"
              >
                <div class="company-child-content">
                  <h3>{{ formData.OrganizationName }}</h3>
                  <div class="d-flex container-child">
                    <div class="pre-title">Trực thuộc đơn vị:</div>
                    <div class="content-title">{{ Organ.OrganizationName }}</div>
                  </div>
                  <div class="d-flex container-child">
                    <div class="pre-title">Mã số thuế:</div>
                    <div class="content-title">{{ formData.OrganizationTaxNo }}</div>
                  </div>
                  <div class="d-flex container-child">
                    <div class="pre-title">Cấp tổ chức:</div>
                    <div class="content-title">{{ formData.OrganizationUnit }}</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        <img
          class="img-menu"
          src="@/assets/images/Group 18759.png"
          style="position: absolute; bottom: 0px; right: 120px"
        />
        <img
          class="img-menu"
          src="@/assets/images/Group 18772.png"
          style="position: absolute; bottom: 0px; left: 50px"
        />
      </div>
    </div>
  </div>
</template>
<script>
// import header from "@/layout/components/header.vue";
import Button from "@/components/buttons";
import apiGetOrganizationUnit from "@/api/etax/organization.js";
import OrganizationCookie from "@/utils/cookie.js";
export default {
  name: "ListOrganizations",
  components: {
    "button-icon": Button,
  },
  props: {},
  data() {
    return {
      isbutton: false,
      tab1: true,
      tab2: false,
      SessionID: "ac45a234fd744188b4b9d763cf654c20",
      formDatas: [],

      // Dữ liệu từ Organization
      Organization: {
        type: Object,
        default: "",
      },
      Organ: {},
      loading: false,
    };
  },
  methods: {
    dblClick(id, name) {
      OrganizationCookie.setOrganizationCookie(id);
      OrganizationCookie.setOganizationNameCookie(name);
      this.$router.push("tax-period");
    },

    async getListOrganization() {
      var res = await apiGetOrganizationUnit.getListOrganization();
      if (typeof res.data.Data != "undefined" && res.data.Success) {
        if (res.data.Data == null || res.data.Data.length == 0) {
          this.$router.push("/getting-started/owner");
        } else {
          this.mapDataEdit(res.data.Data);
        }
      } else {
        this.formDatas = [];
      }
    },

    /**
     * Map dữ liệu trả về thông tin đơn vị chủ quản
     * Created by: TVPhi 21/08/2020
     */
    mapDataEdit(data) {
      var result = [];
      this.Organ = data.find((item) => item.IsParent == true);
      data.forEach((item) => {
        var obj = {};
        // Dữ liệu phía thông tin chung
        obj.OrganizationID = item["OrganizationID"];
        obj.OrganizationName = item["OrganizationName"];
        obj.TaxPlaceName = item["TaxPlaceName"]; // Địa chỉ
        obj.OrganizationUnit = item["OrganizationUnit"]; // Cơ cấu tổ chức
        obj.OrganizationTaxNo = item["OrganizationTaxNo"]; // Mã số thuế
        obj.OrganizationUnitTypeID = item["OrganizationUnitTypeID"]; //Kiểm tra xem có phải trực thuộc đơn vị
        obj.OrganizationUnitCode = item["OrganizationUnitCode"];
        obj.IsParent = item["IsParent"];
        result.push(obj);
      });
      this.formDatas = result;
      this.loading = false;
    },
  },

  created() {
    this.loading = true;
    this.getListOrganization();
  },
};
</script>
<style lang="scss" scoped>
@import "@/styles/variables.scss";
.getting-started-layout {
  .container-child{
    height: 17px;
    line-height: 17px;
    margin-bottom: 10px;
  }

  .pre-title{
    min-width: 105px;
    color: $medium-dark-grey;
    margin-right: 16px;
  }

  .content-title{
    font-family: $_fontmedium;
  }

  .logo {
    display: flex;
    height: 60px;
    align-items: center;
    z-index: 100;
    box-shadow: 0px 4px 4px -4px $box-shadow-grey;
    background-color: $white;
    position: absolute;
    top: 0;
    left: 0;
    width: 210px;
    .img-menu {
      width: 16px;
      height: 16px;
      margin-left: 20px;
      margin-right: 20px;
    }
    .img-logo {
      height: 40px;
    }
  }
  .company {
    background-color: $background-light-grey;
    position: absolute;
    left: 0px;
    right: 0px;
    bottom: 0px;
    top: 60px;
    &-container {
      width: 792px;
      height: 100%;
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      background-color: $white;
    }
  }

  .company-unit {
    position: absolute;
    right: 0px;
    bottom: 0px;
    top: 24px;
    height: 60px;
    display: flex;
    width: 100%;

    .company-title {
      position: absolute;
      left: 120px;
    }

    .btn-company-mode {
      position: absolute;
      padding-top: 10px;
      right: 120px;
    }
  }

  .company-body {
    align-items: center;
    justify-content: center;
    position: absolute;
    top: 100px;
    bottom: 24px;
    left: 0;
    right: 0;
    overflow: auto;
    display: flex;

    .opacity-background {
      background-color: $white;
    }

    .test {
      height: 100%;

      .company-body-header {
        cursor: pointer;
        margin-top: 16px;
        height: 110px;
        width: 552px;
        border: 1px solid $bg-color-3;
        border-radius: 4px;

        &:hover {
          border-color: $primary;
        }

        .company-icon-main {
          float: right;
          margin-right: 10px;
        }

        .company-child-content {
          margin: 16px;

          h3 {
            font-size: 15px;
            font-family: $_fontbold;
            margin-bottom: 16px;
          }
        }
      }

      .company-body-child {
        margin-top: 16px;
        height: 137px;
        width: 552px;
        border: 1px solid $bg-color-3;
        border-radius: 4px;

        &:hover {
          border-color: $primary;
          cursor: pointer;
        }

        .company-icon-main {
          float: right;
          margin-right: 10px;
        }

        .company-child-content {
          margin: 16px;
          h3 {
            margin-bottom: 16px;
            font-size: 15px;
            font-family: $_fontbold;
          }
        }
      }
    }
  }

  //opacity khi hiện loading
  .opacity-background {
    width: 100%;
    height: 100%;
    position: absolute;
    opacity: 0.6;
    z-index: 1000;
    background-color: $background-light-grey;
  }

  .el-loading-parent--relative{
    height: 100%;
  }
}
</style>
