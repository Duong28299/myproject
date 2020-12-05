<template>
  <div class="input-component">
    <span @mouseover="hover = true" @mouseleave="hover = false">
      <div
        class="label-input"
        :class="className"
        type="label-top"
        v-if="type == 'label-top'"
      >
        <div class="label mb-8">{{ labelForm }}</div>
        <el-input
          :placeholder="placeholder"
          v-model="input"
          :disabled="disabled"
          :maxlength="maxlength"
          clearable
        ></el-input>
      </div>
      <div
        class="label-input d-flex"
        :class="className"
        type="label-left"
        v-if="type == 'label-left'"
      >
        <div class="label pb-10">{{ labelForm }}</div>
        <el-input
          :placeholder="placeholder"
          :value="input"
          :disabled="disabled"
          :maxlength="maxlength"
          clearable
        ></el-input>
      </div>
      <!-- Field Input -->
      <el-input
        :id="id"
        :style="getStyleinput"
        v-if="type == 'field-input'"
        v-model="getInput"
        @change="submitFuntion"
        :maxlength="maxlength"
        :placeholder="placeholder"
        :disabled="disabled"
        ref="fieldinput"
        v-on:input="showClearBtn($event)"
        @focus="focusEvent($event)"
        @blur="blur"
      >
       <i slot="suffix" class="el-icon-close btn-clear-fieldInput" v-bind:class="[{ active: (isShow || (!hover||hover))}, {unActive: (!isShow&&!hover)}]" @click="clearText()"></i>
      </el-input>

      <!-- Field Input Icon -->
      <el-input
        id="input-icon"
        :class="getClassIcon"
        :style="getStyleinput"
        v-if="type == 'input-icon'"
        :maxlength="maxlength"
        v-model="getInput"
        :placeholder="placeholder"
        :disabled="disabled"
        v-on:input="showClearBtn($event)"
        @focus="focusEvent($event)"
        @blur="blur"
      >
        <i slot="prefix" :class="getIcon"></i>
        <i slot="suffix" class="el-icon-close btn-clear-inputIcon" v-if="isShow || hover" @click="clearText()"></i>
      </el-input>
      <!-- Input có đuôi disabled -->
      <div id="input-append">
        <el-input
          :id="id"
          :style="getStyleinput"
          v-if="type == 'input-append'"
          v-model="getInput"
          :maxlength="maxlength"
          :placeholder="placeholder"
          :disabled="disabled"
          clearable
        >
          <template slot="append">{{ append }}</template>
        </el-input>
      </div>
      <!-- Input có phần đầu disabled -->
      <div id="input-prepend">
        <el-input
          :id="id"
          :style="getStyleinput"
          v-if="type == 'input-prepend'"
          v-model="getInput"
          :maxlength="maxlength"
          :placeholder="placeholder"
          :disabled="disabled"
          clearable
        >
          <template slot="prepend">{{ prepend }}</template>
        </el-input>
      </div>

    </span>
  </div>
</template>
<script>
import { convertName } from "@/utils/index.js";
// import $ from "jquery";
export default {
  props: {
    id: {
      type: String,
      default: "",
    },
    placeholder: {
      // Thông tin placeholder của input
      type: String,
      default: "",
    },
    type: {
      // Dạng của input : 'label-top' Input có label nằm trên
      type: String,
      default: "",
    },
    disabled: {
      // Không cho nhập vào input hay không
      type: Boolean,
      default: false,
    },
    labelForm: {
      // Tên của label
      type: String,
      default: "",
    },
    className: {
      type: String,
      default: "w-100",
    },
    /**
     * Truyền giá trị cho ô nhập liệu text
     */
    valueInput: {
      type: String,
      default: "",
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
     * Có bật chế độ viết hoa chữ cái đầu không
     */
    isUpercasefirst: {
      type: Boolean,
      default: false,
    },
    /**
     * Truyền xuống tên icon thư viện element UI
     */
    iconel: {
      type: String,
      default: null,
    },
    classicon: {
      type: String,
      default: null,
    },
    /**
     * Truyền vào maxlength
     */
    maxlength: {
      type: Number,
      default: 50,
    },
    /**
     * Truyền vào chỉ nhập số
     */
    number: {
      type: Boolean,
      default: false,
    },
    onlyText:{
      type: Boolean,
      default: false,
    },
    /**
     * Truyền vào chỉ nhập chữ
     */
    text:{
      type: Boolean,
      default: false,
    },
    /**
     * Sự kiện khi keyup
     */
    keyup: {
      type: Function,
      default: () => {},
    },
    
    /**
     * Sự kiện khi submit bằng enter hoặc blur
     */
    submitFuntion: {
      type: Function,
      default:()=>{},
    },
  },
  data() {
    return {
      // Giá trị của the input khi nhập vào
      hover: false,
      input: "",
      timer: null,
      isShow:false
    };
  },
  computed: {
    getStyleinput() {
      return "min-width: 100px; width: " + this.width + ";";
    },
    getIcon() {
      return "el-input__icon " + this.iconel;
    },
    getInput: {
      get() {
        return this.valueInput;
      },
      set(val) {
        if (val != this.valueInput){
          if (this.isUpercasefirst == true) val = convertName(val);
        // chỉ là kí tự số
        if (this.number) {
          val = val.replace(/\D/g, "");
        }
        // chỉ có kí tự là chữ
        if(this.onlyText){
          var regex = /[^a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s\W]/g
          val = val.replace(regex, "");
        }
        this.$emit("value", val);
        }
      },
    },
    getClassIcon() {
      return "icon-custom " + this.classicon;
    }
  },
  methods: {
    blur() {
      this.$emit("blur", this.getInput);
      this.isShow= false;
    },
    /**
     * Hàm delay để gọi service check/tìm kiếm
     * Created by nvbinh2 25.09.2020
     */
    functionKeyup() {},
    getKeyup() {
      this.$emit("keyup1", this.getInput);
    },
    /**
     * Hàm bắt sự kiện hiển thị but clear
     * nvnguyen 03/10/2020
     */
    showClearBtn(event){
      if(event){
        this.isShow=true;
      }else{
         this.isShow=false;
      }
    },
    /**
     * hàm xóa text trong ô input
     * nvnguyen 03/10/2020
     */
    clearText(){
      this.getInput= "";
      this.isShow = false;
      this.$emit("clearEvent",this.getInput)
    },

    //ham hiện thị nut clear khi focus
    //nvnguyen 03/10/2020
    focusEvent(event){
      if(event.target.value){
        this.isShow=true
      }
      else{
        this.isShow=false
      }
    },
  },
  watch: {
    //Hiển thị message validate
    hover(val) {
      this.$emit("hover", val);
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
#input-icon {
  padding-left: 40px !important;
  .el-input__icon {
    margin-left: 16px;
    padding-right: 16px;
    width: 16px;
    font-size: 13px;
  }
}
.el-input__prefix {
  left: 0px;
  width: 40px !important;
}

.el-input .el-input__clear {
  font-size: 18px;
}

.el-input .btn-clear-inputIcon {
  width:16px;
  height: 16px;
  background-color: $light-grey;
  margin: auto;
  border-radius: 50%;
  margin-right: 5px;
  cursor: pointer;
  line-height: 16px;
}

.el-input .btn-clear-inputIcon::before {
  font-size: 12px;
  color: $grey;
  font-weight: $_fontbold;
}

.input-component {
  .el-input--suffix .el-input__inner {
    padding-right: 36px;
  }
}

//CSS đinh dạng nut clear của fieldInput
.el-input .btn-clear-fieldInput {
  width:16px;
  height: 16px;
  background-color: $light-grey;
  margin: auto;
  border-radius: 50%;
  margin-right: 5px;
  cursor: pointer;
  line-height: 16px;
}
.active{
  visibility: visible;
}
.unActive{
  visibility: hidden;
}
.el-input .btn-clear-fieldInput::before {
  font-size: 12px;
  color: $grey;
  font-weight: $_fontbold;
}
</style>