<template>
  <div class="tax-period-container">
    <div class="page-title d-flex">
      <h1>{{ getTitle }}</h1>
      <div class="btn-icon-right">
        <m-btn type="btn-white2-onlyicon-big" tooltip="Làm mới" positionTooltip="left" :handleClick="refreshTable">
          <template v-slot:icon>
            <img class="icon" src="@/icons/AddNew.svg" style="width: 20px;height: 20px;" />
          </template>
        </m-btn>
      </div>
    </div>
    <div class="grid-content">
      <filter-bar
        :newTemporaryTax="newTemporaryTax"
        :newDeclaration="newDeclaration"
        @filterValue="filterBar = $event"
        :isEmpty="isEmpty"
        :isCheckAllRow="isCheckAllRow"
        @createTemporary="newTemporaryTax"
        @setModeAdd="setModeAdd"
      ></filter-bar>
      <component
        :is="currentRole"
        style="height:calc(100% - 132px)"
        :filterBar="filterBar"
        @createTemporary="openDialogNewTemporary"
        @createDeclaration="openDialogNewDeclaration"
        @setTemporaryTax="setTemporaryTax"
        @isEmpty="isEmptyFunction"
        @isCheckAllRow="isCheckAllRowFunction"
        @setModeAdd="setModeAdd"
      />

      <new-temporary-tax
        :dialogFormVisible="dialogNewTemporary"
        @close-dialog="closeDialogTemporary"
        :temporaryTax="temporaryTax"
        :modeType="modeType"
      ></new-temporary-tax>
    </div>
  </div>
</template>

<script>
import filter from "./filter";
import newTemporaryTax from "./temporary-mode/components/dialog-temporary";
import declarationMode from "./declaration-mode";
import temporaryMode from "./temporary-mode";
import Button from "@/components/buttons";
export default {
  name: "TaxPeriod",
  components: {
    declarationMode: declarationMode,
    temporaryMode: temporaryMode,
    "filter-bar": filter,
    "new-temporary-tax": newTemporaryTax,
    "m-btn": Button,
  },
  data() {
    return {
      dialogNewDeclaration: false,
      dialogNewTemporary: false,
      filterBar: {},
      temporaryTax: {},
      isCheckAllRow: false,
      isEmpty: true,
      deleteMulti: false, //Biến truyền xuống cho phép xóa nhiều
      modeType: "", // Biến truyền kiểu thêm mới hay nhân bản
    };
  },
  computed: {
    getTitle() {
      return this.$route.meta.title;
    },

    currentRole() {
      return this.$store.getters.modeTaxPeriod;
    },
  },
  watch:{
  },
  methods: {
    refreshTable() {
      this.$store.dispatch("filter/addRefresh", true);
    },

    isEmptyFunction(val) {
      this.isEmpty = val;
    },
    isCheckAllRowFunction(val) {
      this.isCheckAllRow = val;
    },

    /**
     * Sử kiện mở modal thêm mới bảng tính
     * author:nvbinh
     */
    newTemporaryTax() {
      this.dialogNewTemporary = true;
    },

    /**
     * Sự kiện mở modal thêm mới tờ khai
     */
    newDeclaration() {
      this.dialogNewDeclaration = true;
    },
    /** Sự kiện đóng modal */
    closeDialogTemporary(val) {
      this.dialogNewTemporary = val;
    },
    closeDialogDeclaration(val) {
      this.dialogNewDeclaration = val;
    },

    /**
     * Sự kiển mở modal thêm mới tờ khai
     */
    openDialogNewTemporary(val) {
      this.dialogNewTemporary = val;
    },

    /**
     *
     * Sự kiển mở modal
     */
    openDialogNewDeclaration(val) {
      this.dialogNewDeclaration = val;
    },

    setTemporaryTax(val) {
      this.temporaryTax = val;
    },
    /**
     * Truyền xuống con mode thêm mới hay nhân bản
     * created: NTNgoc - 18/8/2020
     *  */
    setModeAdd(val) { 
      this.modeType = val;
    },
  },
  created() {
    if (this.$store.getters.modeTaxPeriod == ""){
      this.$store.dispatch("taxPeriod/setMode", "temporaryMode");
    }
  },
};
</script>
<style lang="scss" scoped>
.tax-period-container {
  height: 100%;

  .grid-content {
    height: calc(100% - 70px);
  }

  .page-title {
    margin: 0 -24px 0 -24px;
    background-color: #fff;

    h1 {
      padding: 24px 24px 16px 24px;
      background-color: #fff;
      margin: 0;
    }

    .btn-icon-right{
      top: 15px
    }
  }
}
</style>
