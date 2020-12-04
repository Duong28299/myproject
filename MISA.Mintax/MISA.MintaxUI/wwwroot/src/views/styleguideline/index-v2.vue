<template>
<div>
  <!-- Line mode -->
  <el-form inline>
    <el-form-item label="Inline default:">
      <select-tree :options="options" v-model="selected" @value-checked="checked" />
    </el-form-item>
    <el-form-item label="Inline defines the width:">
      <select-tree width="200" :options="options" v-model="selected" @value-checked="checked"/>
    </el-form-item>
  </el-form>
  <!-- block mode -->
  <el-form>
    <el-form-item label="Adaptive:">
      <select-tree v-model="selected" :options="options" :props="defaultProps" @value-checked="checked" />
    </el-form-item>
  </el-form>
 </div>
</template>

<script>
import SelectTree from "@/components/tree-dropdown";

export default {
  name: "about",
  components: {
    SelectTree,
  },
  data() {
    return {
      // default selected value
      selected: "A",
      // data default field
      defaultProps: {
        parent: "parentId", // parent unique identifier
        value: "id", // Uniquely identifies
        label: "label", // tag display
        children: "children", // child
      },
      // Datasheets
      options: [
        {
          parentId: "0",
          id: "A",
          label: "label-A",
          children: [
            {
              parentId: "A",
              id: "A-1",
              label: "label-A-1",
            },
             {
              parentId: "A",
              id: "A-2",
              label: "label-A-2",
            },
          ],
        },
        {
          parentId: "0",
          value: "B",
          label: "label-B",
          children: [],
        },
      ],
    };
  },
  methods: {
    checked(val){
      console.log(val);
    }
  }
};
</script>