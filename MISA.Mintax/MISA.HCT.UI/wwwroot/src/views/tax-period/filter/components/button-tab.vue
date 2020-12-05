<template>
  <div class="mode-change d-flex">
    <m-btn name="Bảng tính thuế" type="btn-tab" :active="active" :handleClick="getTemporaryMode"></m-btn>
    <m-btn name="Tờ khai" type="btn-tab" :active="!active" :handleClick="getDeclarationMode"></m-btn>  
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
      
    };
  },
  computed: {
    active: {
      get() {
        const mode = this.$store.getters.modeTaxPeriod;
        if (mode == "temporaryMode") return true;
        else return false;
      },
      set() {},
    },
  },
  methods: {
    /**
     * SỰ kiện chuyển sang bên mode xem danh sách tạm tính và lưu cookie
     */
    getTemporaryMode() {
      this.active = true;
      this.$store.dispatch("taxPeriod/setMode", "temporaryMode");
    },
    /**
     * Sự kiện chuyển sang bên mode xem danh sách tờ khai và lưu cookie
     */
    getDeclarationMode() {
      this.active = false;
      this.$store.dispatch("taxPeriod/setMode", "declarationMode");
    },
    /**
     * emit biến refresh table
     */
    refreshTable(){
      this.$store.dispatch('filter/addRefresh', true);
    }
  },
  
};
</script>
<style lang="scss">
@import "~@/styles/variables.scss";
@import "~@/styles/btn.scss";

.btn-icon-right {
  position: absolute;
  right: 16px !important;
}

.mode-change {
  background-color: $white;
  margin-left: -24px;
  margin-right: -24px;

  button {
    height: 52px !important;
  }
  .btn-icon-right {
    button {
      height: 40px !important;
      margin-top: 5px;
    }
  }
  .button-container {
    button {
      width: 140px;
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
</style>