<template>
  <div v-if="isPopup">
    <el-dialog
      :title="params.title"
      :visible.sync="isPopup"
      width="408px"
      id="noty-dialog"
      :close-on-click-modal="false"
      @close="resetState()"
      :show-close="isClose"
    >
      <div v-if="type == 'success'" style="width: 360px;text-align: center">
        <div class="bg-img">
          <img class="img-bgsuccess" src="@/assets/images/Group 1@2x.png"/>
          <img class="img-success" src="@/icons/Group 556@2x.png"/>
        </div>
        <div class="body-success">
          <div class="title-success">{{params.titlesuccess}}</div>
          <div class="content-success" v-html="params.content"></div>
        </div>
      </div>
      <div v-else-if="type == 'loading'" class="popup-loading">
        <div class="title-loading" v-html="params.content"></div>
        <div v-loading="true" class="loading"></div>
      </div>
      <div v-else class="confirm">
        <div class="icon-popup" v-if="type != 'info'">
          <img
            v-if="type=='warn'"
            style="width: 30px; height: 30; margin-right: 10px"
            src="@/icons/Group 17652@2x.png"
          />
          <img
            v-if="type=='delete'"
            style="width: 30px; height: 30px; margin-right: 10px"
            src="@/icons/Group 17653@2x.png"
          />
        </div>
        <div class="text-dialog" v-html="params.content"></div>
      </div>
      <span v-if="type != 'success' && type != 'loading'" slot="footer" class="dialog-footer">
          <el-button
            v-for="(item,index) in params.buttons"
            :key="index"
            :class="item.class"
            @click="handleClick(item)"
          >{{item.text}}</el-button>
        </span>
    </el-dialog>
  </div>
</template>
<script>
export default {
  name: "Popup",
  data() {
    return {
      title: "",
      isPopup: false,
      type: "info",
      isClose: "true",
      params: {
        title: "Đây là popup thông báo",
        titlesuccess: "CHÚC MỪNG BẠN",
        content: "Thử nghiệm lần thứ n",
        buttons: [
          {
            text: "Không",
            class: "btn-grey",
            callback: () => {},
          },
          {
            text: "Đóng",
            class: "btn-primary",
            callback: () => {},
          },
        ],
      },
    };
  },
  methods: {
    /**
     * Hàm hiển thị popup success
     */
    success: function (title, content, buttons) {
      this.resetState();
      this.type = "success";
      this.open(title, content, buttons);
    },
    /**
     * Hàm hiển thị popup warn
     */
    warn: function (title, content, buttons) {
      this.resetState();
      this.type = "warn";
      this.open(title, content, buttons);
    },

    /**
     * Hàm hiển thị popup delete
     */
    delete: function (title, content, buttons) {
      this.resetState();
      this.type = "delete";
      this.open(title, content, buttons);
    },

    /**
     * Hàm hiển thị popup loading
     */
    loading: function (title, content, buttons) {
      this.resetState();
      this.type = "loading";
      this.open(title, content, buttons);
    },

    /**
     * Hàm hiển thị popup info
     */
    info: function (title, content, buttons) {
      this.resetState();
      this.type = "info";
      this.open(title, content, buttons);
    },

    /**
     * Hàm mở popup
     */
    open: function (title, content, buttons) {
      if(this.type == "success"){
        this.params.titlesuccess = title
        this.params.title = ""
      }
      else this.params.title = title;
      if(this.type == "loading"){
        this.isClose = false
      }
      else{
        this.isClose = true
      }
      this.params.content = content;
      this.params.buttons = buttons;
      this.isPopup = true;
    },

    /**
     * Reset trạng thái của popup về mặc định
     */
    resetState: function () {
      this.isPopup = false;
      this.type = "info";
    },

    /**
     * Hàm xử lý sự kiện cho các button
     */
    handleClick: function (button) {
      this.resetState();
      if (typeof button.callback == "function") button.callback();
    },

  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
.confirm{
  display: flex;
  align-items: center;
}
.bg-img{
  position: relative;
  width: 408px;
  height: 122px;
  transform: translate(-24px, -40px);
  .img-bgsuccess{
  z-index: 0;
  width: 408px;
  height: 122px;
  }
  .img-success{
    width: 50px;
    height: 50px;
    transform: translateY(-60px);
  }
}

.body-success{
  transform: translate(0px, -20px);
  .title-success{
    font-size: 24px;
    text-transform: uppercase;
    font-family: $_fontbold;
  }
  .content-success{
    font-size: 15px;
    margin-top: 5px;
    color: $dark-grey;
  }
}
.icon-popup {
  display: flex;
  align-items: center;
  padding-left: 7px;
  width: 45px;
  height: 45px;
  border-radius: 50px;
  background-color: $bg-color-1;
  margin-right: 10px;
}
.img {
  width: 30px;
  height: 30px;
  margin-right: 10px;
}

.popup-loading{
  width: 360px;
  text-align: center;

  .title-loading{
    font-size: 15px;
    margin-bottom: 25px;
    color: $dark;
    word-break: break-word;
  }

  .loading{
    width: 100%;
    height: 50px;
  }

  .el-loading-mask{
    background-color: $white !important;
    opacity: 1;
  }
}
</style>