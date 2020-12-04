<template>
  <el-popover
    ref="popover"
    placement="bottom-start"
    trigger="click"
    @show="onShowPopover"
    @hide="onHidePopover">
    <el-tree
      ref="tree"
      :show-checkbox="checkbox"
      class="select-tree"
      highlight-current
      :style="`min-width: ${treeWidth}`"
      :data="data"
      :props="props"
      :expand-on-click-node="false"
      :filter-node-method="filterNode"
      @check="getCheckList"
      :default-expand-all="expand"
>
    </el-tree>
    <el-input
      slot="reference"
      ref="input"
      v-model="labelModel"
      clearable
      :style="`width: ${width}px`"
      :class="{ 'rotate': showStatus }"
      suffix-icon="el-icon-arrow-down"
      :placeholder="placeholder">
    </el-input>
  </el-popover>
</template>

<script>
import {convertListIDToString} from "@/utils/organization-unit"
export default {
  name: 'Pagination',
  props: {
    // Gia trị
    value: String,

    responeType:String,
    // Độ rộng
    width: String,
    expand: {
        type:Boolean,
        default: false,
    },
    // option data
    options: {
      type: Array,
      required: true,
    },
    // có show checkbox hay không
    checkbox:{
        type:Boolean,
        default: true,
    },
    // Giá trị mặc định
    placeholder: {
      type: String,
      required: false,
      default: 'Chọn bộ phận/phòng ban',
    },
    /**
     * Thông tin prop truyền xuống
     */
    props: {
      type: Object,
      required: false,
      default: () => ({
        parent: 'ParentID',
        value: 'OrganizationUnitID',
        label: 'OrganizationUnitName',
        children: 'OrganizationUnitsChildren',
      }),
    },
  },
  model: {
    prop: 'value',
    event: 'selected',
  },
  computed: {
    // Is it tree structure data?
    dataType() {
      const jsonStr = JSON.stringify(this.options);
      return jsonStr.indexOf(this.props.children) !== -1;
    },
    // If it is not a tree structure, it will be converted into tree structure data.
    data() {
      return this.dataType ? this.options : this.switchTree();
    },
  },
  watch: {
    labelModel(val) {
      if (!val) {
        this.valueModel = '';
      }
      // this.$refs.tree.filter(val);
    },
    value(val) {
      this.labelModel = this.queryTree(this.data, val);
    },
    '$refs.tree.getCheckedNodes' :{
        handler(val){
            console.log("Thông tin",val.label);
            this.labelModel = `${this.labelModel}; ${val.label}`;
            console.log(this.labelModel);
        },
        deep:true,
    }
  },
  data() {
    return {
      // trạng thái tree
      showStatus: false,
      // do dai của tree
      treeWidth: 'auto',
      // gia tri ket qua show
      labelModel: '',
      // actual request value
      valueModel: '0',

      valueChecked: [],
    };
  },
  created() {
    // Detect the original value of the input box and display the corresponding label
    if (this.value) {
      this.labelModel = this.queryTree(this.data, this.value);
    }
    // Lấy độ rộng của tree theo độ dài của input
    this.$nextTick(() => {
      this.treeWidth = `${(this.width || this.$refs.input.$refs.input.clientWidth) - 24}px`;
    });
  },
  methods: {
    
    /**
     * Sự kiện click vào node
     * Created by nvbinh2 27.11.2020
     * 
     */
    onClickNode(node) {
      this.labelModel = node[this.props.label];
      this.valueModel = node[this.props.value];
      this.onCloseTree();
    },
    /**
     * Chuyển dạng mảng sang dạng cây
     * Created by nvbinh2 27.11.2020
     */
    switchTree() {
      return this.cleanChildren(this.buildTree(this.options, '0'));
    },
    /**
     * Đóng cây phân cấp
     * Created by nvbinh2 27.11.2020
     * 
     */
    onCloseTree() {
      this.$refs.popover.showPopper = false;
    },
    /**
     * Sự kiện khi mở popover
     */
    onShowPopover() {
      this.showStatus = true;
      this.$refs.tree.filter(false);
    },

    /**
     * Sự kiến đóng Popover
     * created by nvbinh2 27.11.2020
     */
    onHidePopover() {
      this.showStatus = false;
      this.$emit('selected', this.valueModel);
    },
    
    /**
     * Tìm kiếm trong node
     */
    filterNode(query, data) {
      if (!query) return true;
      return data[this.props.label].indexOf(query) !== -1;
    },
    
    /**
     * Tìm kiếm theo id của node
     */
    queryTree(tree, id) {
      let stark = [];
      stark = stark.concat(tree);
      while (stark.length) {
        const temp = stark.shift();
        if (temp[this.props.children]) {
          stark = stark.concat(temp[this.props.children]);
        }
        if (temp[this.props.value] === id) {
          return temp[this.props.label];
        }
      }
      return '';
    },
    

    /**
     * Dựng mảng 1 chiều thành cây phân cấp
     * Created by nvbinh2 27.11.2020
     */
    buildTree(data, id = '0') {
      const fa = (parentId) => {
        const temp = [];
        for (let i = 0; i < data.length; i++) {
          const n = data[i];
          if (n[this.props.parent] === parentId) {
            n.children = fa(n.rowGuid);
            temp.push(n);
          }
        }
        return temp;
      };
      return fa(id);
    },
    
    /**
     * Xóa các con của node
     * Created by nvbinh2 27.11.2020
     */
    cleanChildren(data) {
      const fa = (list) => {
        list.map((e) => {
          if (e.children.length) {
            fa(e.children);
          } else {
            delete e.children;
          }
          return e;
        });
        return list;
      };
      return fa(data);
    },

    getCheckList(){
      var listChecked = this.$refs.tree.getCheckedNodes();     
      var compactResult = this.compactListChecked(listChecked);
      this.addToCheckedData(compactResult);
      
    },
    addToCheckedData(compactResult){
      this.labelModel = "";
      this.valueChecked =[]
      compactResult.forEach(element => {
        this.labelModel = (this.labelModel && this.labelModel != "") ? `${this.labelModel}; ${element[this.props.label]}` : `${element[this.props.label]}`
        this.valueChecked.push(element[this.props.value]);
      });
      this.$emit('value-checked', this.valueChecked);
    },


    compactListChecked(listChecked){
      var listResult = [];
      listChecked.forEach(element => {
          this.addChildChecked(listResult, element,listChecked);
      });
      return listResult;
    },

    addChildChecked(listResult, currentElement,listChecked){
      var duplicate = listChecked.find(x => x[this.props.value] == currentElement[this.props.parent]);
      if(!duplicate){
        listResult.push(currentElement);
      }
    },
    emitDataChecked(value){
        switch(this.responeType){
            case "string":
                this.$emit('value-checked',convertListIDToString(value));
                break;
            default:
                this.$emit('value-checked', value);   
        }
    }


  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
  .el-input.el-input--suffix {
    cursor: pointer;
    overflow: hidden;
  }
  .el-input.el-input--suffix.rotate .el-input__suffix {
    transform: rotate(180deg);
  }
  .select-tree {
    max-height: 350px;
    overflow-y: scroll;
  }
  .el-popover{
    padding: 0px !important;
  }
  /* Menu scroll bar */
  .select-tree::-webkit-scrollbar {
    z-index: 11;
    width: 6px;
  }
  .select-tree::-webkit-scrollbar-track,
  .select-tree::-webkit-scrollbar-corner {
    background: #fff;
  }
  .select-tree::-webkit-scrollbar-thumb {
    border-radius: 5px;
    width: 6px;
    background: #b4bccc;
  }
  .select-tree::-webkit-scrollbar-track-piece {
    background: #fff;
    width: 6px;
  }
  .el-tree-node__content{
    height: 32px;
  }
  .el-tree-node__content:hover{
    background-color: $bg-color-1;
  }
</style>