<template>
  <el-table-column 
    :prop="listColumn.prop"
    :label="listColumn.label"
    :align="listColumn.align"
    :sortable="listColumn.sortable"
    :tooltip="listColumn.label"
    :width="listColumn.width"
    :min-width="listColumn.minWidth"
    :show-overflow-tooltip="true"
  >
    <template v-if="listColumn.group !== undefined" >
      <my-column 
        v-for="(item, index) in listColumn.group"
        :key="index"
        :listColumn="item"
      >
      </my-column>
    </template>
      
    <template v-if="listColumn.group === undefined" slot-scope="{ row }"> 
          <!-- Dạng thường -->
          <span
            v-if="
              typeof listColumn.type == 'undefinded' ||
              listColumn.type == '' ||
              row['Header']
            "
            >{{ row[listColumn.propertyName] }}
          </span>

          <!-- Dạng ngày -->
          <span v-if="listColumn.type === 'date' && !row['Header']">{{
            formatDate(row[listColumn.propertyName])
          }}</span>

          <!-- Dạng tháng mm/yyyy -->
          <span v-if="listColumn.type === 'month'">{{
            formatDate(row[listColumn.propertyName], "mm/yyyy")
          }}</span>

          <!-- Dạng giới tính -->
          <span v-if="listColumn.type === 'gender'">{{
            formatGender(row[listColumn.propertyName])
          }}</span>

          <!-- Dạng số -->
          <span v-if="listColumn.type === 'decimal'">{{
            formatMoney(row[listColumn.propertyName])
          }}</span>
          
          <el-select @change="selectChange(row)" v-model="row[listColumn.propertyName]" placeholder="Select" v-if="listColumn.type === 'dropdown' && !row['Header']"
            class="row-dropdown">
            <el-option
              v-for="item in listColumn.optionsSelect"
              :key="item.value"
              :label="item.label"
              :value="item.value"
              :class="{ 'el-icon-check':  row[listColumn.propertyName] == item.value }"
              >
            </el-option>
          </el-select>                 
    </template>    
  </el-table-column>
</template>

<script>
import { formatMoney } from "@/utils/index.js";

import { formatDate } from "@/utils/index.js";


export default {
  name: 'MyColumn',
  props: {
    listColumn: {
      type: Object
    },
    optionsSelect:{
      type: Array,
      default: null
    },
  },
  
  methods: {
    /**
     * Hàm thực hiện format cột hiển thị số
     */
    formatMoney(data) {
      if (typeof data === "string")
        return null;
      return formatMoney(data, ",");
    },
    /**
     * Hàm định dạng ngày
     */
    formatDate(data, type = "dd/mm/yyyy") {
      if (typeof data === "string" && data[0] === "[")
        return data;
      return formatDate(data, type);
    },
    
    selectChange(row){
      this.$emit("selectChange",row);
    },

    /**
     * Hàm định dạng giới tính trong bảng dữ liệu
     * created by vdthang 24.11.2020
     */
    formatGender(data){
      if(data == 1){
        return "Nam"
      }else{
        return "Nữ"
      }
    }
  }
}
</script>
<style lang="scss">
</style>