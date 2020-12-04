<template>
  <div :id="selectID">
    <span @mouseover="hover = true" @mouseleave="hover = false">
      <!-- Selevt dropdown với label nằm trên  -->
      <div
        class="label-input"
        :class="className"
        type="label-top"
        v-if="type == 'label-top'"
      >
        <div class="label mb-4">{{ labelForm }}</div>
        <el-select
          class="combobox"
          :clearable="clearable"
          v-if="filterable == true"
          v-model="value"
          :placeholder="placeholder"
          filterable
          :popper-class="customSelect"
          :disabled="disabled"
          :default-first-option="true"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
        <el-select
          :clearable="clearable"
          v-if="filterable == false"
          v-model="value"
          :placeholder="placeholder"
          :popper-class="customSelect"
          :disabled="disabled"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
      </div>

      <!-- Select dropdown voi label nằm trái -->
      <el-form-item
        :clearable="clearable"
        :label="labelForm"
        label-position="left"
        type="label-left"
        v-else-if="type == 'label-left'"
      >
        <el-select
          class="combobox"
          v-if="filterable == true"
          v-model="value"
          filterable
          :placeholder="placeholder"
          :popper-class="customSelect"
          :disabled="disabled"
          :default-first-option="true"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :popper-class="customSelect"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
        <el-select
          :clearable="clearable"
          v-if="filterable == false"
          v-model="value"
          :placeholder="placeholder"
          :popper-class="customSelect"
          :disabled="disabled"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :popper-class="customSelect"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
      </el-form-item>

      <!-- Select không có label -->
      <div v-else-if="type == 'label-none'">
        <el-select
          class="combobox"
          :clearable="clearable"
          v-if="allowCreate == true && filterable == true"
          v-model="value"
          allow-create
          filterable
          :placeholder="placeholder"
          :popper-class="customSelect"
          :disabled="disabled"
          :default-first-option="true"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
        <el-select
          class="combobox"
          :clearable="clearable"
          v-if="allowCreate == false && filterable == true"
          v-model="value"
          :placeholder="placeholder"
          filterable
          :popper-class="customSelect"
          :disabled="disabled"
          :default-first-option="true"
          @change="getInfo"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
        <el-select
          :clearable="clearable"
          v-if="filterable == false"
          v-model="value"
          :placeholder="placeholder"
          :popper-class="customSelect"
          :disabled="disabled"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
      </div>

      <!-- Select không có label v2 -->
      <div v-else-if="type == 'label-none-v2'" class="label-none-v2">
        <div class="btn-clear-select" @click="clearSelectValue($event)">
          <i class="el-icon-close"></i>
        </div>
        <el-select
          class="combobox"
          v-if="allowCreate == true && filterable == true"
          v-model="value"
          allow-create
          filterable
          :placeholder="placeholder"
          :popper-class="customSelect"
          :disabled="disabled"
          :default-first-option="true"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
        <el-select
          class="combobox"
          v-if="allowCreate == false && filterable == true"
          v-model="value"
          :placeholder="placeholder"
          filterable
          :popper-class="customSelect"
          :disabled="disabled"
          :default-first-option="true"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
        <el-select
          class="select-no-filter"
          @change="showClearSelect($event)"
          :clearable="clearable"
          v-if="filterable == false"
          v-model="value"
          :placeholder="placeholder"
          :popper-class="customSelect"
          :disabled="disabled"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
      </div>
      <!--select border white-->
      <div v-if="type == 'select-border-white'">
        <el-select
          :clearable="clearable"
          v-model="value"
          :placeholder="placeholder"
          class="selectWhite"
          :popper-class="customSelect"
          :disabled="disabled"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
      </div>
      <!--/select border white-->

      <!--select border color-->
      <div v-if="type == 'select-border-color'" class="select-filter">
        <div class="btn-clear-select" @click="clearSelectValue($event)">
          <i class="el-icon-close"></i>
        </div>
        <el-select
          v-model="value"
          :placeholder="placeholder"
          class="selectColor"
          :popper-class="customSelect"
          :disabled="disabled"
          @change="showClearSelect($event)"
        >
          <el-option
            v-for="(item, index) in list"
            :key="index"
            :label="item[label]"
            :value="item[field]"
            :class="{ 'el-icon-check': item[field] == value }"
          ></el-option>
        </el-select>
      </div>
      <!--select border color-->

      <!--select Organization-->
      <div
        v-if="type == 'choose-units'"
        class="cb-organizations"
        id="select-unit"
      >
        <div class="unit-short-name">
          <avatar :username="shortName" class="short-name"></avatar>
        </div>
        <el-select
          v-model="curentOrgamization"
          placeholder="Chọn đơn vị"
          class="select-organize"
          id="select-organize"
          @change="chooseOrganization($event)"
        >
          <el-option
            v-for="item in listUnits"
            :key="item.OrganizationID"
            :value="item.OrganizationID"
            :label="item.OrganizationName"
            :class="{
              'el-icon-check': curentOrgamization == item.OrganizationID,
            }"
          >
          </el-option>
        </el-select>
      </div>
      <!--select Organization-->
    </span>
  </div>
</template>

<script>
import $ from "jquery";
import OrganizationCookie from "@/utils/cookie.js";
import Avatar from "vue-avatar";
import OganizeRegex from "@/enums/regexOrganization.js";
export default {
  components: {
    avatar: Avatar,
  },
  name: "TableSummaryPaging",
  props: {
    // Tên đơn vị
    oranizationName: {
      type: String,
      default: "",
    },
    listUnits: {
      type: Array,
      default: null,
    },

    //ID của select được chọn
    selectID: {
      type: String,
      default: "",
    },

    // Danh sách các
    list: {
      type: Array,
      default: null,
    },

    // dạng label. top- bên trên, left- bên trái
    type: {
      type: String,
      default: "",
    },

    // label cho form
    labelForm: {
      type: String,
      default: "",
    },

    // label sử dụng như tên
    label: {
      type: String,
      default: "",
    },

    // Property sử dụng là biến
    field: {
      type: String,
      default: "",
    },

    placeholder: {
      type: String,
      default: "",
    },

    /**
     * Giá trị ban đầu nếu value là number
     */
    defaultNumber: {
      type: Number,
      default: null,
    },

    /**
     * Giá trị ban đầu nếu value là string
     */
    defaultString: {
      type: String,
      default: null,
    },

    /**
     * class chỉnh css cho menu sổ xuống
     */
    customSelect: {
      type: String,
      default: "",
    },

    /**
     * thêm class cho ô input label top
     */
    className: {
      type: String,
      default: "w-100",
    },

    /**
     * Biến bật disable ô nhập liệu
     */
    disabled: {
      type: Boolean,
      default: false,
    },

    /**
     * Có cho phép nhập vào để lọc không
     */
    filterable: {
      type: Boolean,
      default: false,
    },

    /** Cho phép thêm trường */
    allowCreate: {
      type: Boolean,
      default: false,
    },

    /**
     * Có sử dụng icon xóa không
     */
    clearable: {
      type: Boolean,
      default: false,
    },

    /**
     * lấy value từ select box
     */
    getInfo: {
      type: Function,
      default: () => {},
    },
  },
  data() {
    return {
      shortName: "hello",
      lstOrganization: [],
      value: null,
      hover: false,
      isShow: false,
      listfilter: [],
      isRefresh: false,
      curentOrgamization: "",
      listPlaceOfIssues: [],
    };
  },
  watch: {
    value(val) {
      this.$emit("value", val);
    },
    defaultString(val) {
      this.value = val;
    },
    hover(val) {
      this.$emit("hover", val);
    },
    defaultNumber(val) {
      this.value = val;
    },
  },
  methods: {
    //Hàm show nut clear select box (nvnguyen 02/11/2020)
    showClearSelect(val) {
      if (val == 0 || val != "") {
        $("#" + this.selectID + " .select-filter .btn-clear-select").css(
          "z-index",
          1
        );
        $("#" + this.selectID + " .label-none-v2 .btn-clear-select").css(
          "z-index",
          1
        );
      }
    },

    //hàm xóa text filter trong select box (nvnguyen 02/11/2020)
    clearSelectValue() {
      $("#" + this.selectID + " .select-filter .btn-clear-select").css(
        "z-index",
        0
      );
      $("#" + this.selectID + " .label-none-v2 .btn-clear-select").css(
        "z-index",
        0
      );
      this.value = null;
    },

    // /**
    //  * hàm lấy set thông tin theo đơn vị vào cookie valf load lại data khi thay đổi
    //  * nvnguyen (04/11/2020)
    //  */
    // chooseOrganization(val) {
    //   OrganizationCookie.setOrganizationCookie(val);
    //   this.listUnits.forEach(function (item) {
    //     if (item.OrganizationID == val) {
    //       OrganizationCookie.setOganizationNameCookie(item.OrganizationName);
    //       //giải pháp tạm thời cho phần set đọ dài chọn đơn vị. ai có cách làm thì xóa phần này đi
    //       $("#select-organize").width(item.OrganizationName.length * 6 + 20);
    //     }
    //   });
    //   window.location.reload();
    // },
  },

  mounted() {
    if (this.defaultString == null) {
      this.value = this.defaultNumber;
    } else {
      this.value = this.defaultString;
    }
    this.shortName = OrganizationCookie.getOganizationNameCookie();
    //giải pháp tạm thời cho phần set độ dài chọn đơn vị. ai có cách làm thì xóa phần này đi
    //$("#select-organize").width(this.shortName.length * 6 + 20);
  },

  created() {
    //lấy id đơn vị nvnguyen
    this.curentOrgamization = OrganizationCookie.getOganizationNameCookie();
    let regex = OganizeRegex.ORGANIZATIONREGEX;
    // match tên đơn vị theo regex và thực hiện lấy tên
    regex.forEach((item) => {
      if (item.value.test(this.shortName)) {
        let temp = this.shortName.replace(item.value, "");
        let reg = /(\w+\d*)/gi;
        let found = temp.match(reg);
        this.shortName = found.toString().replaceAll(",", " ");
      }
    });
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
.combobox {
  .el-input__suffix {
    width: 38px;
    right: 1px;
    top: 1px;
    height: 38px;
    border: 0px;
    color: $grey;
    font-family: $_fontbold;
    border-left: 1px solid $bg-grey;
    border-top-right-radius: 5px !important;
    border-bottom-right-radius: 5px !important;
  }
  &.el-select .el-input--suffix .el-input__inner {
    padding-right: 40px !important;
  }
  .is-focus {
    .el-input__suffix {
      background-color: $hover-grey;
      border-top-right-radius: 5px;
      border-bottom-right-radius: 5px;
      border: 0px;
    }
  }
}

// select border white
.selectWhite {
  .el-input--suffix .el-input__inner {
    border: none !important;
    border-radius: 5px !important;
    &:focus {
      border: none !important;
    }
  }
}

//label-none-v2
.label-none-v2 {
  position: relative;
  .btn-clear-select {
    width: 16px;
    height: 16px;
    position: absolute;
    top: calc((100% - 16px) / 2);
    right: 31px;
    background-color: $light-grey;
    text-align: center;
    border-radius: 50%;
    cursor: pointer;
  }
  .el-icon-close::before {
    font-weight: $_fontbold;
    color: $grey;
    font-size: 12px;
    position: absolute;
    top: 3px;
    right: 2px;
  }
}

//css base cho nuts clear text select border color
.select-filter {
  position: relative;
  .btn-clear-select {
    width: 16px;
    height: 16px;
    position: absolute;
    top: calc((100% - 16px) / 2);
    right: 28px;
    // z-index: 0;
    background-color: $light-grey;
    text-align: center;
    border-radius: 50%;
    cursor: pointer;
  }
  .el-icon-close::before {
    font-weight: $_fontbold;
    color: $grey;
    font-size: 12px;
  }
}

//select border color
.selectColor {
  .el-input.el-input--suffix.el-input--suffix {
    .el-input__inner {
      border-radius: 5px;
      border: 1px solid transparent;
      padding-right: 54px;
      &:focus {
        border: 1px solid $light-primary;
      }
    }
  }
}

//Css phần chọn đơn vị
.cb-organizations {
  display: flex;
  margin-left: 24px;
  height: 36px;
  .select-organize {
    .el-input--suffix {
      .el-input__inner {
        height: 36px;
        border: none;
        font-family: $_fontregular;
        padding-left: 12px;
      }
    }
  }

  .unit-short-name {
    width: 36px;
    height: 36px;
    .short-name {
      display: flex;
      width: 36px !important;
      height: 36px !important;
      border-radius: 8px !important;
      font-size: 13px;
      align-items: center;
      justify-content: center;
      text-align: center;
      user-select: none;
      font-family: $_fontbold;
      background-color: $light-blue-unit !important;
      color: $hover-primary !important;
    }
  }
}
</style>
