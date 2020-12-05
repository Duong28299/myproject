<template>
<span @mouseover="hover = true" @mouseleave="hover = false">
  <el-select-tree
    :id="id"
    v-if="show"
    :default-expand-all="defaultExpandAll"
    :multiple="multiple"
    :placeholder="placeholder"
    :disabled="disabled"
    :data="data"
    :props="arraySetting"
    :check-strictly="checkStrictly"
    :clearable="clearable"
    v-model="value"
  ></el-select-tree>
</span>
</template>


<script>
import ElSelectTree from "el-select-tree";
import { getFromTree } from "@/utils";
export default {
  props: {
    show: {
      type: Boolean,
      default: true,
    },
    arraySetting: {
      type: Object,
      default: null,
    },
    data: {
      type: [Array, Object],
      default: null,
    },

    /**
     * Cho phép chọn nhiều giá trị hay không
     */
    multiple: {
      type: Boolean,
      default: true,
    },

    /**
     * Cho phép tìm kiếm hay không
     */
    searchable: {
      type: Boolean,
      default: true,
    },
    /**
     * Disabe hay không
     */
    disabled: {
      type: Boolean,
      default: false,
    },
    /**
     * Độ rộng nhất
     */
    maxHeight: {
      type: Number,
      default: 200,
    },
    maxWidth: {
      type: Number,
      default: 0,
    },

    returnObject: {
      type: Boolean,
      default: false,
    },
    count: {
      type: Boolean,
      default: false,
    },

    valueSelect:{
      type: String ,
      default: null
    },

    /**
     * Placholder
     */
    placeholder: {
      default: "Chọn bộ phận/phòng ban",
    },
    checkStrictly: {
      type: Boolean,
      default: false,
    },
    clearable: {
      type: Boolean,
      default: true,
    },
    defaultExpandAll: {
      type: Boolean,
      default: false,
    },
    id:{
      type: String,
      default:"",
    }
  },
  components: {
    ElSelectTree,
  },
  data() {
    return {
      treeData: [],
      disabledValues: ["3"],
      valueResult: null,
      value:null,
      hover:false,
    };
  },
  created(){
    this.value = this.valueSelect;
  },
  methods: {},
  watch: {
    value: {
      handler: async function (newVal) {
        if (!this.returnObject) {
          if (this.count && this.countSelected == newVal.length)
            this.$emit("change", newVal);
        } else {
          if(newVal==""){
            this.$emit("change", {
              OrganizationUnitID:null,
              OrganizationUnitName:null
            });
          }
          var result = await getFromTree(newVal, this.data, this.arraySetting);
          this.$emit("change", result);
        }
      },
    },
    valueSelect: function (val) {
      if(val){
        this.value = val;
      }else
      {
        this.value= null;
      }
    },
    hover: function (val) {
        this.$emit("hover", val);
    }
  },
  computed: {
    countSelected() {
      var _temp = document.getElementsByClassName(
        "el-tree el-select-tree__list"
      )[0];
      if (!_temp) return 0;
      return _temp.querySelectorAll(".el-checkbox__input is-checked").length;
    },
  },
};
</script>
<style lang="scss" scope>
@import "~@/styles/variables.scss";
.el-select-tree .el-input:hover:not(.is-disabled) .el-input__inner {
  border-color: $primary !important;
}
.el-select-tree .el-input.is-active .el-input__inner {
  border-color: $primary !important;
}
.el-select-tree__item.is-selected {
  color: $primary !important;
}
.el-select-tree__item.is-selected::before {
  margin-right: 10px;
}
</style>
