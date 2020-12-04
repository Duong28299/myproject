<template>
  <div id="menu">
      <div class="logo">
        <img class="img-menu" src="@/assets/images/Union 28@2x.png"/>
        <img class="img-logo" src="@/assets/images/Group 17877@2x.png"/>
      </div>
      <router-link class="menu-item" to="/provider">
        <div class="active"></div>
        <div>
          <svg class="icon-menu">
            <use xlink:href="@/assets/images/Group17853.svg#svgh"></use>
          </svg>
        </div>
        <span class="txt-menu">Trần Cảnh Dương</span>
      </router-link>
      <!-- LƯU Ý KHI MERGE CODE K ĐƯỢC BẬT IMG LÊN. LIÊN HỆ VDTHANG -->
      <!-- <img v-if="this.$store.getters.sidebar.opened == true" class="footer" src="@/assets/images/Group 18200@2x.png"/> -->
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
    data(){
        return{
            widthscreen: 0,
        }
    },
    computed: {
        ...mapGetters([
            'sidebar'
        ])
    },
    methods:{
    toggleSideBar() {
      this.$store.dispatch('app/toggleSideBar')
    },
  }
}
</script>

<style lang="scss" scoped>
@import "@/styles/sidebar.scss";
.icon-menu{
  fill: #000;
}

.router-link-active .icon-menu{
  fill: #fff;
}

@media only screen and (max-height: 580px) {
  #menu .footer{
    display: none;
  }
}
</style>