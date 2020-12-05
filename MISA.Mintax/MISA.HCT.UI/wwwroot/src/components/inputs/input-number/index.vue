<template>
  <div>
    <span @mouseover="hover = true" @mouseleave="hover = false">
      <!-- Input number thường -->
      <el-input
        :id="id"
        class="input-number"
        v-if="type == 'field-input'"
        v-model="getInput"
        :style="getStyleinput"
        :disabled="disabled"
        :maxlength="maxlength"
        :placeholder="placeholder"
        :clearable="true"
      />
      <!-- Field Input Icon -->
      <el-input
        id="input-icon"
        class="input-number"
        :style="getStyleinput"
        v-if="type == 'input-icon'"
        v-model="getInput"
        :disabled="disabled"
        :maxlength="maxlength"
        :clearable="true"
      >
        <i slot="prefix" class="el-input__icon el-icon-search"></i>
      </el-input>
      <!-- Input có đuôi disabled -->
      <div id="input-append" v-if="type == 'input-append'">
        <el-input
          :id="id"
          class="input-number"
          v-model="getInput"
          :maxlength="maxlength"
          :style="getStyleinput"
          :disabled="disabled"
          @focus="clickInput"
          v-on:input="showClearBtn($event)"
          @blur="isShow = false"
        >
          <template slot="append">{{ append }}</template>
          <i
            slot="prefix"
            class="el-input__icon el-icon-close btn-clear-inputApend"
            v-if="isShow || hover"
            @click="clearText()"
          ></i>
        </el-input>
      </div>
      <!-- Input có phần đầu disabled -->
      <div id="input-prepend" v-if="type == 'input-prepend'">
        <el-input
          :id="id"
          class="input-number"
          v-model="getInput"
          :maxlength="maxlength"
          :style="getStyleinput"
          :disabled="disabled"
          :clearable="true"
        >
          <template slot="prepend">{{ prepend }}</template>
        </el-input>
      </div>
      <!-- Input số điện thoại -->
      <el-input
        :id="id"
        class="input-number"
        v-if="type == 'input-phoneNumber'"
        v-model="getInput"
        :maxlength="maxlength"
        :style="getStyleinput"
        :clearable="true"
      />
      <!--Input number kèm nút tăng giảm, UI same "|<| Label + Number |>| "-->
      <div type="input-btn-dec_inc" v-if="type == 'input-btn-dec_inc'">
        <span
          class="el-input-number__decrease"
          role="button"
          @click="decrease"
          :class="{ 'is-disabled': minDisabled }"
          @keydown.enter="decrease"
        >
          <i class="el-icon-arrow-left"></i>
        </span>
        <span
          class="el-input-number__increase"
          role="button"
          @click="increase"
          :class="{ 'is-disabled': maxDisabled }"
          @keydown.enter="increase"
        >
          <i class="el-icon-arrow-right"></i>
        </span>
        <el-input
          v-model="displayInput"
          :max="max"
          :min="min"
          @keydown.up.native.prevent="increase"
          @keydown.down.native.prevent="decrease"
          :readonly="true"
        ></el-input>
      </div>
    </span>
  </div>
</template>
<script>
export default {
  name: "InputMoney",
  props: {
    /**
     * Truyền xuống id để check validate
     */
    id: {
      type: String,
      default: "",
    },
    /**
     * Truyền vào loại input cần dùng
     */
    type: {
      type: String,
      default: "field-input",
    },
    /**
     * Có disable trường nhập liệu không
     */
    disabled: {
      type: Boolean,
      default: false,
    },
    /**
     * Truyền vào chiều dài của trường input
     */
    width: {
      type: String,
      default: "100%",
    },
    /**
     * Truyền vào đuôi append
     */
    append: {
      type: String,
      default: "",
    },
    /**
     * Truyền vào đầu prepend
     */
    prepend: {
      type: String,
      default: "",
    },
    /**
     * Truyền vào maxlength
     */
    maxlength: {
      type: Number,
      default: 18,
    },
    /**
     * Truyền vào có format tiền hay không
     */
    money: {
      type: Boolean,
      default: false,
    },
    /**
     * Truyền vào placeholder
     */
    placeholder: {
      type: String,
      default: "false",
    },
    /**
     * Truyền vào giá trị hiển thị trước số trong ô input
     * vd tháng, năm, ngày ...
     */
    prefixString: {
      type: String,
      default: "",
    },
    /**
     * Giá trị ô nhập liệu
     */
    valueInput: {
      type: [String, Number],
      default: "",
    },
    max: {
      type: Number,
      default: Infinity,
    },
    min: {
      type: Number,
      default: -Infinity,
    },
    step: {
      type: Number,
      default: 1,
    },
  },
  data() {
    return {
      hover: false,
      inputnumber: 0,
      isShow: false,
    };
  },
  created() {},
  methods: {
    /**
     * Hàm tự bôi đen khi click input
     */
    clickInput() {
      if (this.id) {
        const el = document.getElementById(`${this.id}`);
        var elemLen = el.value.length;
        el.selectionStart = 0;
        el.selectionEnd = elemLen;
        el.focus();
        this.isShow = true;
      } else {
        this.isShow = false;
      }
    },
    /**
     * Hàm tăng giảm value
     * createdby ndhuy 14.09.2020
     */
    increase() {
      if (!this.maxDisabled) {
        this.$emit("value", this.valueInput + this.step);
      }
    },
    decrease() {
      if (!this.minDisabled) this.$emit("value", this.valueInput - this.step);
    },

    /**
     * Hàm bắt sự kiện hiển thị but clear
     * nvnguyen (03/11/2020)
     */
    showClearBtn(event) {
      if (event) {
        this.isShow = true;
      } else {
        this.isShow = false;
      }
    },
    /**
     * hàm xóa text trong ô input
     * nvnguyen(03/11/2020)
     */
    clearText() {
      this.getInput = "";
      this.isShow = false;
      this.$emit("clearEvent", this.getInput);
    },
  },
  computed: {
    getStyleinput() {
      return "width: " + this.width + ";";
    },
    getInput: {
      get() {
        if (this.money == true) {
          if (this.valueInput == "") {
            return "";
          }
          return this.valueInput
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        } else {
          return this.valueInput.toString();
        }
      },
      set(val) {
        if (val == "") {
          this.$emit("value", val);
        } else {
          val = val
            .split("")
            .filter((x) => x !== ",")
            .join("");
          val = val.replace(/\D/g, "");
          if (this.money == true) this.$emit("value", val * 1);
          else this.$emit("value", val);
        }
      },
    },
    displayInput: {
      get() {
        return this.prefixString + " " + this.valueInput;
      },
    },
    maxDisabled() {
      return this.valueInput + this.step > this.max;
    },
    minDisabled() {
      return this.valueInput - this.step < this.min;
    },
  },
  watch: {
    hover(val) {
      this.$emit("hover", val);
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
div[type="input-btn-dec_inc"] {
  .el-input__inner {
    padding: 0px 50px !important;
  }
  span {
    height: 38px;
  }
}

//css nuts clear input
.el-input .btn-clear-inputApend {
  width: 16px;
  height: 16px;
  background-color: $light-grey;
  border-radius: 50%;
  margin-left: 0px;
  margin-top: calc((100% - 16px) / 2);
  cursor: pointer;
  line-height: 16px;
}
.el-input .btn-clear-inputApend::before {
  font-size: 12px;
  color: $grey;
  font-weight: $_fontbold;
}
//end css nuts clear input
.el-input--prefix .el-input__inner {
  padding-left: 36px;
}
</style>
