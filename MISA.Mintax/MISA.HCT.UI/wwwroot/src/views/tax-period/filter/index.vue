<template>
  <div class="filter-container">
    <!-- Component chọn xem tờ khai hay xem bảng tính -->
    <btn-tab class="btn-tab"></btn-tab>
    <!-- Bộ lọc  -->
    <div class="filter-comp d-flex" v-if="!isEmpty">
      <!-- Nút nhập tìm kiếm -->
      <fieldinput
        class="search-filter"
        @clearEvent="getClearText()"
        type="input-icon"
        classicon="icon-search"
        width="368px"
        :placeholder="'Tìm kiếm'"
        :valueInput="filter.keyword"
        @value="filter.keyword = $event"
      ></fieldinput>

      <input-dropdown
        selectID="period-time-select"
        @clearSelect="clearValue()"
        :list="listMonth"
        style="width: 160px"
        type="select-border-color"
        label="MONTH_NAME"
        field="MONTH"
        placeholder="Kỳ tính thuế"
        @value="filter.month = $event"
        :clearable="true"
      ></input-dropdown>
      <!-- Nút tìm kiếm theo tháng năm -->

      <!-- <div class="label text">Năm</div> -->
      <quarter-picker
        selectID="period-time-datepicker"
        type="year"
        formatInput="yyyy"
        Valueformat="yyyy"
        size="medium"
        :width="'130px'"
        placeholder="Chọn năm"
        :value="defaultValue"
        @getValue="filter.year = $event"
        :borderType="'none-border-white'"
      ></quarter-picker>

      <input-dropdown
        selectID="income-type-select"
        v-if="mode == 'temporaryMode'"
        :list="listTaxType"
        style="width: 210px"
        type="select-border-color"
        label="STYLE"
        field="VALUE"
        :clearable="true"
        :placeholder="'Loại thu nhập'"
        @value="filter.taxType = $event"
      ></input-dropdown>

      <input-dropdown
        selectID="status-select"
        v-if="mode != 'temporaryMode'"
        :list="listStatus"
        style="width: 160px"
        type="select-border-color"
        label="name"
        field="value"
        :clearable="true"
        :placeholder="'Trạng thái'"
        @value="filter.declarationStatus = $event"
      ></input-dropdown>

      <!-- button thêm BTT, Tờ khai / button xóa khi selected -->
      <div class="btn-mode">
        <div v-if="mode == 'temporaryMode'">
          <div v-if="isCheckAllRow">
            <m-btn
              type="btn-white3-onlyicon"
              :iconel="'el-icon-delete'"
              tooltip="Xóa"
              :handleClick="callDeleteMulti"
            >
              <template v-slot:icon></template>
            </m-btn>
            <!--button tai ve-->
            <m-btn
              style="position: absolute; right: 56px"
              type="btn-white2-onlyicon"
              tooltip="Tải về"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Component 95 – 1.png" />
              </template>
            </m-btn>
            <!--/button tai ve-->
          </div>
          <div v-else>
            <m-btn
              type="btn-primary-icon"
              name="Thêm bảng tính thuế"
              :handleClick="addNewTemporary"
            >
              <template v-slot:icon
                ><img class="icon" src="@/icons/Group 17812.png"
              /></template>
            </m-btn>
          </div>
        </div>

        <div v-if="mode != 'temporaryMode'">
          <div v-if="isCheckAllRow">
            <m-btn
              v-if="isSign"
              style="right: 56px"
              type="btn-white3-onlyicon"
              tooltip="Ký nộp qua mTax"
              :iconel="'el-icon-s-promotion'"
              :handleClick="callMultiSignDeclaration"
            >
              <template v-slot:icon> </template>
            </m-btn>

            <m-btn
              style="right: 0px"
              type="btn-white3-onlyicon"
              tooltip="Xóa"
              :iconel="'el-icon-delete'"
            >
              <template v-slot:icon></template>
            </m-btn>
          </div>

          <div v-else>
            <m-btn
              type="btn-primary-icon"
              name="Thêm tờ khai thuế"
              :handleClick="addNewDeclaration"
            >
              <template v-slot:icon
                ><img class="icon" src="@/icons/Group 17812.png"
              /></template>
            </m-btn>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DatePicker from "@/components/date-time-pickers";
import Dropdown from "@/components/select";
import fieldinput from "@/components/inputs";
import ButtonTab from "./components/button-tab";
import Button from "@/components/buttons";
import listMonth from "@/enums/filter";
import listTaxType from "@/enums/taxType";
import listStatus from "@/enums/chipEnum";

import $ from "jquery";

export default {
  components: {
    "m-btn": Button,
    "btn-tab": ButtonTab,
    "quarter-picker": DatePicker,
    "input-dropdown": Dropdown,
    fieldinput: fieldinput,
  },
  props: {
    newDeclaration: {
      type: Function,
      default: () => {},
    },
    isEmpty: {
      type: Boolean,
      default: false,
    },
    isCheckAllRow: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      defaultValue: new Date().getFullYear().toString(),
      listStatus: [],
      listMonth: [],
      listTaxType: [],
      keyword: "",
      placeholderYear: "Chọn năm",
      width: "120",
      filter: {
        keyword: "",
        month: 0,
        year: new Date().getFullYear(),
        declarationStatus: 0,
        taxType: 0,
        deleteMulti: false,
        signMulti: false,
      },
      Organization: {},
      listRow: [],
      notSubmitted: true, // Biến kiểm tra trạng thái đã nộp hay chưa để hiển thị button Ký
      checkMTax: true,
      isSign: true,
    };
  },
  watch: {
    filter: {
      handler(val) {
        this.$store.dispatch("filter/addFilter", val);
      },
      deep: true,
    },
    /**
     * Theo dõi trạng thái DeclarationStatus của các hàng được check => bật/tắt button Ký
     * created: ntngoc 26/8/2020
     */
    "$store.getters.dataOfRow": {
      handler(val) {
        this.notSubmitted = true;
        this.listRow = val;
        if (val.length == 0) this.notSubmitted = true;
        for (var i = 0; i < val.length; i++) {
          if (val[i].DeclarationStatus != 1) {
            this.notSubmitted = false;
            break;
          }
          if (i == val.length - 1 && val[i].DeclarationStatus != 1)
            this.notSubmitted = true;
        }
        if (this.checkMTax == true && this.notSubmitted == true)
          this.isSign = true;
        else this.isSign = false;
      },
      deep: true,
    },
  },
  computed: {
    mode() {
      return this.$store.getters.modeTaxPeriod;
    },
    // lấy dữ liệu từ checkbox từ state
    dataFromCheckBox() {
      return this.$store.getters.dataOfRow;
    },
  },
  methods: {
    showBtnClear() {
      if ($("#input-icon").val() != "") {
        this.$emit("isShowClear", true);
      } else {
        this.$emit("isShowClear", false);
      }
    },
    /**
     * Gọi hàm xóa nhiều trên component cha
     */
    callDeleteMulti() {
      this.$store.dispatch("filter/addDeleteMulti", true);
    },

    /**
     * Hàm truyền sự kiện thêm mới lên cha
     *
     */
    addNewTemporary() {
      this.$emit("createTemporary", true);
      this.$emit("setModeAdd", "Thêm mới");
    },
    addNewDeclaration() {},
    /**
     * Check hiển thị button Ký nộp
     * created: ntngoc 26/8/2020
     */

    /**
     * Gọi hàm ký nộp nhiều tờ khai
     * Created by CVCuong created date 1/9/2020
     */
    callMultiSignDeclaration() {
      this.$store.dispatch("filter/addSignMulti", true);
    },

    /**
     * hàm set filter key word về rỗng khi click nút clear text
     * created by nvnguyen 31/10/2020
     */
    getClearText(val) {
      if (val) {
        this.filter.keyword = "";
      }
    },

    clearValue(val) {
      if (val) {
        this.filter.month = 0;
      }
    },
  },
  async created() {
    this.listMonth = listMonth.LIST_MONTH;
    this.listTaxType = listTaxType.TAX_TYPE;
    this.listStatus = listStatus.DECLARATION;
    if (this.$store.getters.filter.year == 0) {
      this.$store.dispatch("filter/addFilter", this.filter);
    } else {
      this.filter = this.$store.getters.filter;
    }
    this.Organization = this.$store.getters.organization;
  },
};
</script>
<style lang="scss">
.filter-container {
  #date-picker-none-white {
    border-color: transparent;
  }
}
</style>

<style lang="scss" scoped>
.filter-container {
  width: 100%;
  height: 132px;

  .btn-float-right {
    position: absolute;
    right: 0;
    margin-right: 24px;
  }

  .filter-comp {
    * {
      margin-right: 16px;

      .el-input__inner {
        border: none !important;
      }
    }

    padding-top: 24px;
    position: relative;
    right: 0;
    margin-left: auto;

    .input-icon {
      width: 300px;
      margin-left: 0px;
    }

    .w-26 {
      display: inline-block;
      align-items: center;
      line-height: 40px;
      margin: 0px 16px 0px 16px;
    }

    .label {
      display: flex;
      align-items: center;
      margin: 0px 16px 0px 16px;
    }

    .btn-mode {
      div {
        position: absolute;
        right: 0px;
        margin-right: 0px !important;

        .button-container {
          margin-right: 0px !important;
        }
      }
    }
  }

  .text-tax {
    width: 70px;
  }
}

@media only screen and (max-width: 1200px) {
  .text {
    //opacity: 0;
    //clip-path: polygon(0px 0px,0px 0px,0px 0px,0px 0px);
    display: none !important;
  }
}

#bordernone {
  border: none !important;
}
</style>
