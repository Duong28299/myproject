<template>
  <div class="tbl-main" style="height: 100%">
    <el-table
      border
      ref="reftable"
      v-loading="loading"
      :style="getHeight()"
      :data="dataTable"
      width="100%"
      class="tableData"
      :summary-method="getSummaries"
      :show-summary="summary"
      :row-class-name="getStyleRow"
      tooltip-effect="light"
      @row-click="handleRowClick"
      @row-dblclick="handleRowDBLClick"
      @selection-change="handleSelectionChange"
      @select-all="handleSelectAll"
      :id="id"
    >
</template>
<script>
export default {
    
}
</script>