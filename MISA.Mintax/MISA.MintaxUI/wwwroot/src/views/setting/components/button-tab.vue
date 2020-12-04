<template class="tab">
  <div class="mode-change d-flex" style="margin: 0px !important; border-radius: 4px;">
    <m-btn
      name="Đơn vị"
      type="btn-tab"
      :active="isOrganizationTab"
      :handleClick="changeOrganizationsTab"
    ></m-btn>
    <m-btn
      name="Đại lý thuế"
      type="btn-tab"
      :active="!isOrganizationTab"
      :handleClick="changeTaxEgentsTab"
    ></m-btn>
  </div>
</template>
<script>
import Button from "@/components/buttons";
export default {
  components: {
    "m-btn": Button,
  },
  data() {
    return {
      isOrganizationTab: true,
    };
  },
  computed: {},
  methods: {
    /**
     * Hàm chuyển sang tab mới
     * @author lttuan 28/08/2020
     */
    changeOrganizationsTab() {
      this.isOrganizationTab = true;
      this.$emit("changeTab", 1);
    },

    /**
     * Hàm chuyển sang tab mới
     * @author lttuan 28/08/2020
     */
    changeTaxEgentsTab() {
      this.isOrganizationTab = false;
      this.$emit("changeTab", 2);
    },
  },
};
</script>
<style lang="scss">
@import "~@/styles/variables.scss";
@import "~@/styles/btn.scss";

.tab {
  .btn-icon-right {
    position: absolute;
    right: 16px !important;
  }

  .mode-change {
    background-color: $white;

    button {
      height: 52px !important;
    }

    .btn-icon-right {
      button {
        height: 40px !important;
        margin-top: 5px;
      }
    }

    #btn-tab {
      height: 50px;
      span {
        font-size: 15px;
      }
    }
  }
  
  .mode-change:first-child {
    padding-left: 24px !important;
  }
}
</style>