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
    title: {
      type: Array,
      default: function () {
        return [];
      },
    },
    valueFormat: {
      type: String,
      default: "id",
    },
    value: {
      type: [Array, Object, String],
      default: null,
    },
    arrayOptions: {
      type: [Array, Object],
      //   default: []
    },
    multiple: {
      type: Boolean,
      default: true,
    },
    searchable: {
      type: Boolean,
      default: true,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    maxHeight: {
      type: Number,
      default: 200,
    },
    openOnClick: {
      default: true,
    },
    openOnFocus: {
      default: false,
    },
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
      optionValue: [],
      //   valueConsistsOf: "BRANCH_PRIORITY",
      options: [],
      lstId: [], // mảng chứa tất cả id của obj truyền vào
    };
  },
  components: {
    Treeselect,
  },
  created() {
    this.getArrayId(this.arrayOptions);
    this.mappingData(this.arrayOptions);
    if (this.value) {
      this.updateOptionValue(this.value);
    }
    if (this.title) {
      this.updateTitleValue(this.title);
    }
  },
  methods: {
    // hàm thực hiện get tất cả node con
    getAllChild(parentNode, lstOption) {
      if (parentNode.children && parentNode.children.length > 0) {
        parentNode.children.forEach((ele) => {
          let arrNodeChildMapping = [];
          let arrNodeChild = Array.from(
            lstOption.filter((f) => {
              if (f.MISACode && ele.misaCode) {
                return (
                  f.MISACode.startsWith(ele.misaCode) &&
                  f.MISACode.split("/").length - 1 ===
                    ele.misaCode.split("/").length
                );
              } else {
                return false;
              }
            })
          );
          if (arrNodeChild && arrNodeChild.length > 0) {
            // có mảng node child thì thì tiếp tục tìm kiếm xem trong mảng có tồn tại cấp tiếp theo k
            arrNodeChild.forEach((nodeChild) => {
              arrNodeChildMapping.push({
                id: nodeChild.OrganizationUnitID,
                label: nodeChild.OrganizationUnitName,
                isParent: nodeChild.IsParent,
                amisId: nodeChild.AMISOrganizationUnitID,
                misaCode: nodeChild.MISACode,
              });
            });
            ele.children = Array.from(arrNodeChildMapping);
            this.getAllChild(ele, lstOption);
          }
        });
      } else {
        return;
      }
    },

    /**
     * hàm thực hiện ánh xạ từ dạng mảng thàng thạng obj
     * ntngoc - 16/06/2020
     */
    mappingData(newVal) {
      let lstOptions = Array.from(newVal);
      let lstOptionMapping = [];
      // map các thuộc tính trong mảng truyền vào
      if (lstOptions && lstOptions.length > 0) {
        let parentNode = lstOptions.find((f) => f.ParentID == null);
        // let nodeObj = {};
        // bulid obj để đưa vào mảng lstOptionMapping
        if (parentNode) {
          this.buildTreeOption(parentNode, lstOptions, lstOptionMapping);
        } else {
          let lstLengthMISACode = [];
          lstLengthMISACode = lstOptions.map(
            (item) => item.MISACode.split("/").length
          );

          let minLengthMISACode = Math.min(...lstLengthMISACode);
          let lstParentCode = lstOptions.filter(
            (f) => f.MISACode.split("/").length == minLengthMISACode
          );
          lstParentCode.forEach((element) => {
            this.buildTreeOption(element, lstOptions, lstOptionMapping);
          });
        }
      }
      this.options = lstOptionMapping;
    },

    /**
     * Hàm thực hiện build cây obj
     */
    buildTreeOption(parentNode, lstOptions, lstOptionMapping) {
      let nodeObj = {};
      nodeObj.id = parentNode.OrganizationUnitID;
      nodeObj.label = parentNode.OrganizationUnitName;
      nodeObj.isParent = parentNode.IsParent;
      nodeObj.amisId = parentNode.AMISOrganizationUnitID;
      nodeObj.misaCode = parentNode.MISACode;
      let arrNodeChildMapping = [];

      let arrNodeChild = Array.from(
        lstOptions.filter((f) => {
          if (f.MISACode && nodeObj.misaCode) {
            return (
              f.MISACode.startsWith(nodeObj.misaCode) &&
              f.MISACode.split("/").length - 1 ===
                nodeObj.misaCode.split("/").length
            );
          } else {
            return false;
          }
        })
      );

      if (arrNodeChild && arrNodeChild.length > 0) {
        // có mảng node child ==> map lại các thuộc tính cho từng obj trong arrNodeChild
        arrNodeChild.forEach((nodeChild) => {
          arrNodeChildMapping.push({
            id: nodeChild.OrganizationUnitID,
            label: nodeChild.OrganizationUnitName,
            isParent: nodeChild.IsParent,
            amisId: nodeChild.AMISOrganizationUnitID,
            misaCode: nodeChild.MISACode,
          });
        });
        nodeObj.children = Array.from(arrNodeChildMapping);
        this.getAllChild(nodeObj, lstOptions);
      }
      lstOptionMapping.push(nodeObj);
    },

    /**
     * hàm thực hiện get mảng Id của obj truyền vào
     * ntngoc - 07/07/2020
     */
    getArrayId(arrayObj) {
      let arrayId = [];
      if (arrayObj && arrayObj.length > 0) {
        arrayObj.forEach((element) => {
          if (element.organizationUnitID) {
            arrayId.push(element.organizationUnitID);
          } else {
            arrayId.push(element.OrganizationUnitID);
          }
        });
      }
      this.lstId = arrayId;
    },

    /**
     * Hàm thực hiện kiểm tra nếu không tồn tại id đầu vào trong mảng lstId thì không cho hiển thị lên
     * ntngoc - 07/07/2020
     */
    updateOptionValue(value) {
      const me = this;
      this.optionValue = value;
      if (
        this.optionValue &&
        Array.isArray(this.optionValue) &&
        this.optionValue.length > 0
      ) {
        this.optionValue.forEach((element) => {
          if (!me.lstId.includes(element)) {
            let index = me.optionValue.indexOf(element);
            if (index >= 0) {
              me.optionValue.splice(index, 1);
            }
          }
        });
      } else if (this.optionValue) {
        if (!me.lstId.includes(this.optionValue)) {
          me.optionValue = null;
        }
      } else {
        this.optionValue = null;
      }
    },
    
    /**
     *
     */
    updateTitleValue(value) {
      const me = this;
      let arrObj = [];
      if (value && Array.isArray(value) && value.length > 0) {
        value.forEach((element) => {
          if (!me.lstId.includes(element)) {
            let index = value.indexOf(element);
            if (index >= 0) {
              value.splice(index, 1);
            }
          }
        });
        value.forEach((element) => {
          let obj = this.arrayOptions?.find((f) => {
            return f.OrganizationUnitID == element;
          });
          arrObj.push({
            id: obj.OrganizationUnitID,
            label: obj.OrganizationUnitName,
            isParent: obj.IsParent,
            amisId: obj.AMISOrganizationUnitID,
          });
        });
      }
      this.optionValue = arrObj;
    },
  },
  watch: {
    arrayOptions: {
      deep: true,
      handler: function (newVal) {
        this.getArrayId(newVal);
        this.mappingData(newVal);
      },
    },
    value: {
      deep: true,
      handler: function (value) {
        this.updateOptionValue(value);
      },
    },
    optionValue: {
      deep: true,
      handler: function (value) {
        this.$emit("change", value);
      },
    },
    title: {
      deep: true,
      handler: function (value) {
        this.updateTitleValue(value);
      },
    },
  },
};
</script>
<style lang="scss" scope>
@import "@/assets/scss/components/_treeselect.scss";
</style>
