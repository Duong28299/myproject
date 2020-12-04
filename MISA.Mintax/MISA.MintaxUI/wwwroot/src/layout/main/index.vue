<template>
  <div id="app-main" :class="classObj">
    <Menu class="sidebar-container" />
    <div id="app-content">
      <Header />
      <div id="main">
        <!-- <PageTitle></PageTitle> -->
        <div style="height:100%;">
          <router-view></router-view>
        </div>
      </div>
    </div>
    <Hamburger id="hamburger-container" class="hamburger-container" @toggleClick="toggleSideBar" />
  </div>
</template>
<script>
//import PageTitle from "./components/page-title";
import Menu from "./components/menu";
import Header from "./components/header";
import { mapState } from "vuex";

import Hamburger from "@/components/hamburger";

export default {
  components: {
    Header,
    Menu,
    //PageTitle,
    Hamburger,
  },
  computed: {
    ...mapState({
      sidebar: (state) => state.app.sidebar,
    }),
    classObj() {
      return {
        hideSidebar: !this.sidebar.opened,
        openSidebar: this.sidebar.opened,
        withoutAnimation: this.sidebar.withoutAnimation,
        mobile: this.device === "mobile",
      };
    },
  },
  methods: {
    toggleSideBar() {
      this.$store.dispatch("app/toggleSideBar");
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/index.scss";
@import "@/styles/header.scss";
@import "@/styles/menu.scss";
@import "@/styles/sidebar.scss";
@import "@/styles/variables.scss";
#app-main {
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
}

#main {
  position: absolute;
  top: 60px;
  left: 200px;
  bottom: 0;
  right: 0;
  padding: 0px 24px 0px 24px;
  background-color: $background-grey;
}
#hamburger-container {
  z-index: 2000;
  background-color: $white;
  border: 1px solid $grey;
  padding: 3px 0px 0px 4px;
}
</style>
