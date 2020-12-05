<template>
  <div class="block" :id="selectID">
    <div class="btn-clear-select" @click="clearSelectValue">
      <i class="el-icon-close icon-close"></i>
    </div>
    <el-date-picker v-if="borderType == 'border-white'" class="picker date-picker-white" v-model="valueInput" :type="type" :placeholder="placeholder" :default-value="defaultValue" :format="formatInput" :value-format="Valueformat" :disabled="disabled" :size="size" :picker-options="shortcutOptions" :prefix-icon="icon" :style="getWidth" @change="getValueInput" :clearable="clearable"></el-date-picker>
    <el-date-picker v-else-if="borderType == 'none-border-white'" class="picker date-picker-none-white" id="date-picker-none-white" v-model="valueInput" :type="type" :placeholder="placeholder" :default-value="defaultValue" :format="formatInput" :value-format="Valueformat" :disabled="disabled" :size="size" :picker-options="shortcutOptions" :prefix-icon="icon" :style="getWidth" @change="getValueInput" :clearable="false"></el-date-picker>
    <el-date-picker v-else class="picker" v-model="valueInput" :type="type" :placeholder="placeholder" :default-value="defaultValue" :format="formatInput" :value-format="Valueformat" :disabled="disabled" :size="size" :picker-options="shortcutOptions" :prefix-icon="icon" :style="getWidth" @change="getValueInput" :clearable="clearable"></el-date-picker>
  </div>
</template>

<script>
/**
 * Thêm component Date Picker
 * Author: NVHUNG - 6/7/2020
 * Modifiby: CVCuong - 27/7/2020: Không được đẩy trực tiếp các giá trị props lên trên các trường lấy dữ liệu(đã sửa)
 */
import $ from "jquery";
export default {
  props: {
    /**
     * Giá trị truyên vào
     */
    value: {
      type: [ String ],
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

    /**
     * Tùy chọn ngày nhanh
     */
    shortcutOptions: {
      type: Object,
      default: null,
    },

    clearable:{
      type:Boolean,
      default:false,
    },

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
     * Format dữ liệu truyền vào - https://element.eleme.io/#/en-US/component/date-picker#date-formats
     */
    formatInput: {
      type: String,
      default: "dd-MM-yyyy",
    },

    /**
     * Format dữ liệu đầu ra -https://element.eleme.io/#/en-US/component/date-picker#date-formats
     */
    Valueformat: {
      type: String,
      default: "dd-MM-yyyy",
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

    // set border white
    borderType: {
      type: String,
      default: "",
    },
    //ID của select được chọn
    selectID: {
      type: String,
      default: ""
    },
  },
  data() {
    return {
      valueInput: null,
      /**
       * Khóa ngày tương lai
       */
      disabledDateF: {
        disabledDate(time) {
          return time.getTime() > Date.now();
        },
      },

      /**
       * Khóa ngày quá khứ
       */
      disabledDateP: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        },
      },
    };
  },
  methods: {
    getValueInput() {
      this.$emit("getValue", this.valueInput);
    },
    setWidth(width) {
      return `width : ${width}px`;
    },

    //hàm xóa text filter trong select box (nvnguyen 11/11/2020)  
    clearSelectValue(){
      this.valueInput = "";
      this.getValueInput();
      $("#" + this.selectID + ' .btn-clear-select').css("z-index", 0);
    },
  },
  watch: {
    value: {
      handler(val) {
        this.valueInput = val;
      },
    },
  },
  computed: {
    getWidth() {
      return "width: " + this.width + ";";
    },
  },
  created() {
    this.valueInput = this.value;
  },
};

// :picker-options="
//         disabledDatePick
//           ? disabledDatePick === 'future'
//             ? { ...disabledDateF, ...shortcutOptions }
//             : { ...disabledDateP, ...shortcutOptions }
//           : ''
//       "
</script>

<style lang="scss">
@import "@/styles/variables.scss";
.el-input--suffix .el-input__inner {
  padding-right: 40px;
}
.picker input {
  padding-left: 16px !important;
}
.picker {
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

// set datepicker border white
.date-picker-white {
  .el-input__inner {
    border: 1px solid $white;
    border-radius: 5px;
    &:focus {
      border: 1px solid $light-primary;
    }
  }
}

.block {
  .el-input__prefix {
    right: 5px;
    left: unset;
    color: $dark;
  }
  //định dạng nut clear
  position: relative;
  .btn-clear-select{
    width: 16px;
    height: 16px;
    position: absolute;
    top: calc((100% - 16px)/2);
    right: 30px;
    background-color: $light-grey;
    text-align: center;
    border-radius: 50%;
    cursor: pointer;
    .icon-close{
      position: absolute;
      top: 2px;
      right: 2px;
    }
    .icon-close::before{
      font-weight: $_fontbold;
      color: $grey !important;
      font-size: 12px;
    }
  }
   &:hover .btn-clear-select{
    z-index: 1 !important;
  }
}

// điểu chỉnh lại margin icon date-picker
.date-picker-none-white{
  .el-input__prefix{
    .el-input__icon.el-icon-date {
      margin-right: -16px !important;
    }
  }
}

#date-picker-none-white{
  &:focus{
    border:1px solid $light-primary !important;
  }
}
</style>
