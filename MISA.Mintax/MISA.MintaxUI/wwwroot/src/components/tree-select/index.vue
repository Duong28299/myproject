<template>
  <div>
    <!-- :class="{'invalid': invalid}" -->
    <treeselect
      :multiple="multiple"
      :searchable="searchable"
      :disabled="disabled"
      :open-on-click="openOnClick"
      :open-on-focus="openOnFocus"
      :always-open="false"
      :append-to-body="false"
      :options="options"
      :max-height="maxHeight"
      v-model="optionValue"
      :value-consists-of="valueConsistsOf"
      :placeholder="placeholder"
      :class="{ invalid: invalid }"
      :disable-branch-nodes="disableBranchNodes"
      :default-expand-level="1"
      :clearable="false"
      :ClearOnSelect="false"
      :closeOnSelect="closeOnSelect"
      :flatten-search-results="true"
      :limit="limit"
      :limitText="limitText"
      :noResultsText="'Không có dữ liệu'"
      :valueFormat="valueFormat"
      :normalizer="normalizer"
    />
  </div>
</template>
<script>
// import the component
import Treeselect from "@riophae/vue-treeselect";
// import the styles
import "@riophae/vue-treeselect/dist/vue-treeselect.css";
export default {
  props: {
    /**
     * cấu hình cho mảng truyền vào
     */
    arraySetting: {
      type: Object,
      default: null,
    },

    /*
     * Giá trị truyền vào
     */
    value: {
      type: [Object, Array],
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

    /**
     * Mở bẳng thao tác click
     */
    openOnClick: {
      default: true,
    },
    /**
     * Mở khi ô được focus
     */
    openOnFocus: {
      default: false,
    },
    /**
     * Placholder
     */
    placeholder: {
      default: "Chọn bộ phận/phòng ban",
    },
    invalid: {
      type: Boolean,
      default: false,
    },
    disableBranchNodes: {
      type: Boolean,
      default: false,
    },
    valueConsistsOf: {
      default: "BRANCH_PRIORITY",
    },
    limit: {
      type: Number,
      default: Infinity,
    },
    closeOnSelect: {
      type: Boolean,
      default: true,
    },
    limitText: {
      type: Function,
      default: (count) => `còn ${count} nữa`,
    },
  },
  data() {
    return {
      options: {
        id: null,
        label: null,
        children: [],
      },
      optionValue: null,
    };
  },
  components: {
    Treeselect,
  },

  created() {
    this.options = this.value;
  },
  methods: {
    /**
     * Format khi truyền vào
     */
    normalizer(value) {
      return {
        id: value[this.arraySetting.id],
        label: value[this.arraySetting.label],
        children: value[this.arraySetting.children],
      };
    },
    getParentNode(options, value, arraySetting) {
      options.id = value[arraySetting.id];
      options.label = value[arraySetting.label];
    },
    /**
     * Format biến truyền vào thành cây tương ứng
     * createb by nvbinh2 03.10.2020
     */
    formatValue(value, arraySetting, currentOptions) {
      let current = {
        id: value[arraySetting.id],
        label: value[arraySetting.label],
        children: [],
      };
      currentOptions = current;
      var lstChild = value[arraySetting.children];
      if (lstChild && lstChild.length > 0) {
        for (var i = 0; i < lstChild.length; i++) {
          this.formatValue(
            lstChild[i],
            arraySetting,
            currentOptions[arraySetting.children][i]
          );
        }
      }
      return currentOptions;
    },
  },
  watch: {
    optionValue: {
      deep: true,
      handler: function (value) {
        this.$emit("change", value);
      },
    },
  },
};
</script>
<style lang="scss" scope>
@import "~@/styles/variables.scss";
.el-select-tree .el-input:hover:not(.is-disabled) .el-input__inner {
  border-color: $primary;
}
.el-select-tree .el-input.is-active .el-input__inner {
  border-color: $primary;
}
.vue-treeselect__control {
  height: 40px !important;
}
::v-deep .vue-treeselect {
  max-width: 400px;
}
::v-deep .vue-treeselect--open-below .vue-treeselect__menu {
  border-top-color: #d2d2d2;
}
::v-deep .vue-treeselect__option--highlight {
  background: #f4f4f4;
}
::v-deep .vue-treeselect--single .vue-treeselect__option--selected {
  background: #ebf9f4;
}
::v-deep .invalid .vue-treeselect__control {
  border: 1px solid #f00000 !important;
  border-radius: 4px;
}
::v-deep
  .vue-treeselect--focused:not(.vue-treeselect--open)
  .vue-treeselect__control {
  border-color: #01b075;
  box-shadow: none !important;
  border-radius: 4px;
}
::v-deep .vue-treeselect__control {
  border-radius: 4px !important;
  height: 40px !important;
  border: 1px solid #dbdeff !important;
}
::v-deep .vue-treeselect--open .vue-treeselect__control {
  border-color: #01b075;
  border-radius: 8px;
  height: 40px !important;
}
::v-deep .vue-treeselect__option-arrow {
  color: #01b075;
}
::v-deep
  .vue-treeselect__option-arrow-container:hover
  .vue-treeselect__option-arrow {
  color: #019160;
}
::v-deep
  .vue-treeselect__label-container:hover
  .vue-treeselect__checkbox--unchecked {
  border-color: #01b075;
}
::v-deep
  .vue-treeselect__label-container:hover
  .vue-treeselect__checkbox--indeterminate {
  border-color: #01b075;
  background: #01b075;
}
::v-deep
  .vue-treeselect__label-container:hover
  .vue-treeselect__checkbox--checked {
  border-color: #01b075;
  background: #01b075;
}
::v-deep .vue-treeselect__value-remove {
  color: #5d5c5d;
}
::v-deep .vue-treeselect__multi-value-item {
  color: black;
  background: #d0d0d0;
  border-radius: 10px;
}
::v-deep .vue-treeselect__checkbox--checked {
  border-color: #01b075;
  background: #01b075;
}
::v-deep .vue-treeselect__checkbox--indeterminate {
  border-color: #01b075;
  background: #01b075;
}
::v-deep .vue-treeselect--open-below .vue-treeselect__menu {
  border-top-color: #d2d2d2;
}
::v-deep .vue-treeselect__label-container {
  padding: 4px 0;
}
::v-deep
  .vue-treeselect--focused.vue-treeselect--searchable
  .vue-treeselect__single-value {
  color: black;
}
::v-deep .vue-treeselect__placeholder {
  color: #bdbdbd !important;
  line-height: 40px;
}
</style>
