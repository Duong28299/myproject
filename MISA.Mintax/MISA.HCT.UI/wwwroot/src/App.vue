<template>
  <div id="app">
    <router-view />
  </div>
</template>
<script>
// import { mapGetters, mapState } from "vuex";
export default {
  name: "App",
  components: {},
  data() {
    return {
      organizationId: "2ef7003b-c2a3-11ea-9491-5203cfc96bc9",
      abc: 0,
      message: "Hệ thống đang gặp lỗi",
      decription: "Vui lòng liên hệ MISA để được hỗ trợ",
      windowHeight: 0,
      windowWidth: 0,
    };
  },
  watch: {
    /**
     * hàm đóng mở menu theo chiều dài của màn hình
     */
    windowWidth(val) {
      if (val < 1366) {
        if (this.$store.getters.sidebar.opened == true)
          this.$store.dispatch("app/toggleSideBar");
      } else {
        if (this.$store.getters.sidebar.opened == false)
          this.$store.dispatch("app/toggleSideBar");
      }
    },
    isError(val) {
      if (val) {
        //Tạm khóa lại dialog thông báo lỗi
        // this.popUpFalse("Lỗi", "Vui lòng liên hệ MISA để được khắc phục");
      }
    },
  },
  computed: {
    /**
     * Cập nhật lỗi
     */
    isError() {
      return this.$store.getters.statusLog;
    },
  },
  methods: {
    /**
     * Sự kiên close modal
     */
    closeError() {
      this.$store.dispatch("errorLog/clearErrorLog");
    },
    handleResize() {
      this.windowHeight = screen.height;
      this.windowWidth = window.innerWidth;
    },
    /**
     * Mở modal thông báo lỗi
     */
    popUpFalse(title, message) {
      this.buttonDialog = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: this.closeError,
        },
      ];
      this.$_Popup.warn(title, message, this.buttonDialog);
    },
  },
  async created() {
    // Dua vao store vuejs

    window.addEventListener("resize", this.handleResize);
  },
};
</script>
