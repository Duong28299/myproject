<template>
  <div class="date-time" :style="getWidth" @mouseover="hover = true" @mouseleave="hover = false">
    <el-input class="input-date" :maxlength="maxlength" v-model="inputdate" @blur="outside" :placeholder="placeholder"></el-input>
    <el-date-picker class="picker-v2" v-model="valueInput" :type="type" :default-value="defaultValue" :format="formatInput" :value-format="Valueformat" :disabled="disabled" :size="size" :picker-options="disabledPast ? pickerOptions : ''" :prefix-icon="icon" @change="getValueInput" popper-class="menu-date-v2" :clearable="false"></el-date-picker>
  </div>
</template>
<script>
/**
 * Date Picker Version-2
 * Author: CVCuong - 18/8/2020
 */
export default {
  props: {
    /**
     * Giá trị truyên vào
     */
    value: {
      type: String,
      default: "",
    },
    /**
     * Kiểu chọn : year/month/date/dates/datetime/week
     */
    type: {
      type: String,
      default: "date",
    },
    /**
     * Giá trị placeholder
     */
    placeholder: {
      type: String,
      default: "",
    },
    /**
     * Tùy chọn khóa chọn ngày : future | past
     */
    disabledDatePick: {
      type: String,
      default: "",
    },
    // /**
    //  * Tùy chọn ngày nhanh
    //  */
    // shortcutOptions: {
    //   type: Object,
    //   default: null,
    // },
    /**
     * Giá trị mắc định - mặc định là hiện tại
     */
    defaultValue: {
      type: String,
      default: new Date()
        .toJSON()
        .slice(0, 10)
        .replace(/-/g, "-"),
    },
    /**
     * Tùy chọn khóa
     */
    disabled: {
      type: Boolean,
      default: false,
    },
    /**
     * Tùy chọn kích cỡ : mini - height = 28px | small - height = 32px | large - height = 40px
     */
    size: {
      type: String,
      default: "mini",
    },
    /**
     * Tùy chọn icon
     */
    icon: {
      type: String,
    },
    /**
     * Tùy chọn độ dài
     */
    width: {
      type: String,
      default: "100px",
    },
    /**
     * Khóa ngày quá khứ
     */
    disabledPast: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // Giá trị của the input khi nhập vào
      hover: false,
      pickerOptions: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        },
      },
      valueInput: null,
      inputdate: null,
      formatInput: "MM-dd-yyyy",
      Valueformat: "dd-MM-yyyy",
      maxlength: null,
    };
  },
  methods: {
    getValueInput() {
      this.inputdate = this.valueInput;
    },
    setWidth(width) {
      return `width : ${width}px`;
    },
    outside() {
      if (this.inputdate.length != 10 && this.type == "date") this.inputdate = this.valueInput;
      if (this.inputdate.length != 7 && this.type == "month") this.inputdate = this.valueInput;
      if (this.inputdate.length != 4 && this.type == "year") this.inputdate = this.valueInput;
    },
  },
  watch: {
    type: {
      immediate: true,
      handler(val) {
        if (val == "date") {
          this.formatInput = "MM-dd-yyyy";
          this.Valueformat = "dd-MM-yyyy";
        } else if (val == "month") {
          this.formatInput = "MM-yyyy";
          this.Valueformat = "MM-yyyy";
        } else if (val == "year") {
          this.formatInput = "yyyy";
          this.Valueformat = "yyyy";
        }
      },
    },
    value: {
      handler(val) {
        if (val == null || val == "") {
          this.valueInput = "";
        } else {
          if (this.type == "date") this.valueInput = val.substr(3, 2).concat("-", val.substr(0, 2), "-", val.substr(6, 4));
          else if (this.type == "date")
            this.valueInput = val
              .split("/")
              .filter((x) => x !== "-")
              .join("-");
          else if(this.type ==="month"){
            this.valueInput = val
          }
        }
      },
    },
    inputdate(val) {
      val = val.replace(/\D/g, "");
      var day, month;
      //format đầu vào trường hợp đầu vào là ngày tháng năm
      if (this.type === "date") {
        this.maxlength = 10;
        if (val.length == 2) {
          if (val.charAt(0) * 1 > 3) {
            val = val.substr(0, 0).concat("0", val.charAt(0), "/", val.charAt(1));
          }
        }
        if (val.length >= 3) {
          if (val.charAt(2) != "/") val = val.substr(0, 2).concat("/", val.substr(2, val.length - 2));
          day = val.substr(0, 2) * 1;
          if (day > 31) val = val.substr(0, 0).concat("31", val.charAt(3));
        }
        if (val.length == 5) {
          month = val.substr(3, 1) * 1;
          if (month > 1) {
            val = val.substr(0, 3).concat("0", val.substr(3, val.length - 3));
            if (val.charAt(5) != "/") val = val.substr(0, 5).concat("/", val.substr(5, val.length - 5));
          }
        } else if (val.length >= 6) {
          month = val.substr(3, 2) * 1;
          if (month > 12) {
            val = val.substr(0, 3).concat("12", val.substr(5, val.length - 5));
          } else if (month == 4 || month == 6 || month == 9 || month == 11) {
            if (day == 31) val = val.substr(0, 1).concat("0", val.substr(2, val.length - 2));
          } else if (month == 2) {
            if (day == 31 || day == 30) val = val.substr(0, 0).concat("29", val.substr(2, val.length - 2));
          }
          if (val.charAt(5) != "/") val = val.substr(0, 5).concat("/", val.substr(5, val.length - 5));
        }
        if (val.length == 10) {
          var year = val.substr(6, 4) * 1;
          if (parseInt(year / 4) * 4 != year && month == 2 && day == 29) val = val.substr(0, 1).concat("8", val.substr(2, val.length - 2));
        }
        this.inputdate = val;
        if (val.length == 10) {
          this.valueInput = this.inputdate
            .split("/")
            .filter((x) => x !== "-")
            .join("-");
          var valueemit = this.valueInput.substr(3, 2).concat("-", this.valueInput.substr(0, 2), "-", this.valueInput.substr(6, 4));
          this.$emit("getValue", valueemit);
        } else if (val.length == 0) {
          this.$emit("getValue", null);
        }
      }
      //format đầu vào trường hợp là tháng năm
      else if (this.type === "month") {
        this.maxlength = 7;
        if (val.length == 2) {
          if (val.charAt(0) * 1 > 1) {
            val = val.substr(0, 0).concat("0", val.charAt(0), "/", val.charAt(1));
          } else if (val.charAt(0) * 1 == 1) {
            if (val.charAt(1) * 1 >= 2) {
              val = val.substr(0, 1).concat("2");
            }
          }
        }
        if (val.length >= 3) {
          if (val.charAt(2) != "/") val = val.substr(0, 2).concat("/", val.substr(2, val.length - 2));
        }
        this.inputdate = val;
        if (val.length == 7) {
          if (this.disabledPast) {
            var m = new Date().getMonth();
            var y = new Date().getFullYear();
            if (val.slice(3, 7) * 1 < y || (val.slice(3, 7) * 1 == y && val.slice(0, 2) * 1 < m + 1)) {
              if (m < 10) m = "0" + m;
              this.inputdate = `${"0" + (m * 1 + 1)}/${y}`;
            }
          }
          this.valueInput = this.inputdate
            .split("/")
            .filter((x) => x !== "-")
            .join("-");
          this.$emit("getValue", this.valueInput);
        } else if (val.length == 0) {
          this.$emit("getValue", null);
        }
      }
      //format đầu vào trường hợp là năm
      else if (this.type === "year") {
        this.maxlength = 4;
        this.inputdate = val;
        if (val.length == 4) {
          if (val.charAt(0) === "0") {
            this.inputdate = "1000";
          }
          this.valueInput = this.inputdate;
          this.$emit("getValue", this.valueInput);
        } else if (val.length == 0) {
          this.$emit("getValue", null);
        }
      }
    },
    valueInput(val) {
      this.inputdate = val
        .split("-")
        .filter((x) => x !== "/")
        .join("/");
    },
    //Hiển thị message validate
    hover(val) {
      this.$emit("hover", val);
    },
  },
  computed: {
    getWidth() {
      return "width: " + this.width + ";";
    },
  },
  created() {
    if (this.type === "date") {
      return (this.valueInput = this.value.substr(3, 2).concat("-", this.value.substr(0, 2), "-", this.value.substr(6, 4)));
    }
    this.valueInput = this.value;
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
.picker-v2 {
  border-left: 1px solid $bg-grey;
  .el-input__icon {
    font-size: 16px;
    line-height: 40px;
  }
  cursor: pointer !important;
}

.el-input__prefix {
  width: 40px;
}

// Chỉnh lại padding của icon-date
.el-input__icon .el-icon-date {
  padding: 12px 12px 12px 12px;
}
.el-date-table td span:hover {
  background-color: $hover-blue;
  color: $primary;
}
.el-year-table td.current:not(.disabled) .cell {
  background-color: $primary;
  color: $white;
  border-radius: 5px;
  font-weight: normal;
}
.el-year-table .cell:hover {
  background-color: $hover-blue;
  border-radius: 5px;
  color: $primary;
}
.el-month-table td.current:not(.disabled) .cell {
  background-color: $primary;
  color: $white;
  border-radius: 5px;
  font-weight: normal;
}
.el-month-table .cell:hover {
  background-color: $hover-blue;
  border-radius: 5px;
  color: $primary;
}
.date-time {
  position: relative;
  height: 40px;
  .el-input--suffix .el-input__inner {
    padding-left: 39px !important;
  }
  .picker-v2 {
    position: absolute;
    right: 1px;
    top: 1px;
    width: 40px;
    .el-input__prefix {
      z-index: 1;
      color: $dark;
    }
    .el-input__inner {
      height: 38px !important;
    }
  }
  .input-date {
    position: absolute;
    left: 0px;
    top: 0px;
    right: 0px;
  }
  .el-input--suffix .el-input__inner {
    padding-right: 0px !important;
  }
  .el-input--suffix .el-input__inner {
    border: 0px;
  }
  .el-input--medium .el-input__inner {
    border: 1px solid $bg-grey;
    padding-left: 16px;
    &:hover {
      border-color: $light-primary;
    }
    &:focus {
      border-color: $light-primary;
    }
  }
}
.menu-date-v2 {
  margin-left: -280px !important;
}

.el-month-table td.disabled .cell:hover {
  border-radius: 18px;
}
</style>
