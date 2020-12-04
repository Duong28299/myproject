<template>
  <div class="getting-started-layout">
    <Header :currentMission="missionRoute"/>
    <div class="getting-started-main" style>
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import Header from "../main/components/header";

export default {
  components: {
    Header,
  },
  data(){
    return{
      missionRoute:"getting-started"
    }
  },
  created() {
    // lấy route hiện tại
    this.missionRoute = this.$route.path;
  },
};
</script>

<style>
</style>