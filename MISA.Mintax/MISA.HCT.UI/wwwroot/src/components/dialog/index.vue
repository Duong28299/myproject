<template>
    <el-dialog
      :custom-class="customClass + ' dialogel'"
      :fullscreen="fullscreen"
      :title="titleDialog"
      :visible.sync="dialog"
      :width="widthDialog"
      :before-close="beforeClose"
      :close-on-click-modal="closeonclickmodal"
      :id="id"
    >
      <div style="height:100%;position:relative">
        <slot name="body"></slot>
      </div>
      <span slot="footer" v-if="showfooter">
        <slot name="footer"></slot>
      </span>
    </el-dialog>
</template>
<script>
import { Loading } from "element-ui";
import Dialog from "@/mixins/dialog";
export default {
  name: "warn",
  props: {
    /**
     * Truyền xuống tittle của dialog
     */
    titleDialog: {
      type: String,
      default: "",
    },
    /**
     * Truyền xuống width dialog
     */
    width: {
      type: String,
      default: "500px",
    },
    /**
     * Có cho phép ấn vào bên ngoài dialog không
     */
    closeonclickmodal: {
      type: Boolean,
      default: false,
    },
    /**
     * Truyền vào id để có thể tự chỉnh css nếu cần
     */
    id: {
      type: String,
      default: "",
    },
    /**
     * Truyền xuống class custom cho dialog
     */
    customClass: {
      type: String,
      default: "",
    },
    /**
     * Truyền xuống biến có full màn hình không
     */
    fullscreen: {
      type: Boolean,
      default: false,
    },
    /**
     * Có cảnh báo trước khi đóng dialog không
     */
    isWarnClose: {
      type: Boolean,
      default: false,
    },
    /**
     * Có loading k
     */
    loading: {
      type: Boolean,
      default: false,
    },
    /**
     * Hàm thực thi trước khi form đóng
     */
    beforeClose: {
      type: Function,
      default: (done) => {
        done();
      },
    },
    xs: {
      type: String,
    },
    sm: {
      type: String,
    },
    md: {
      type: String,
    },
    lg: {
      type: String,
    },
    xl: {
      type: String,
    },
    showfooter: {
      type: Boolean,
      default: true,
    },
    version:{
      type:Number,
      default:1
    }
  },
  data() {
    return {
      windowWidth: 0,
      widthDialog: "0px",
      load: null,
      widthCol: [],
      widthGutter: [],
    };
  },
  methods: {
    getWidth(ScreenWidth) {
      this.getVersionLayout()
      // Array = [xs, sm, md, lg , xl]
      const widthCols = this.widthCol;
      const widthGutters = this.widthGutter;
      if (ScreenWidth >= 1920) {
        if (this.xl) {
          const xl = this.xl.split(",");
          return (this.widthDialog =
            xl[0] * widthCols[4] + xl[1] * widthGutters[4] + "px");
        }
      } else if (ScreenWidth >= 1200) {
        if (this.lg) {
          const lg = this.lg.split(",");
          return (this.widthDialog =
            lg[0] * widthCols[3] + lg[1] * widthGutters[3] + "px");
        }
      } else if (ScreenWidth >= 992) {
        if (this.md) {
          const md = this.md.split(",");
          return (this.widthDialog =
            md[0] * widthCols[2] + md[1] * widthGutters[2] + "px");
        }
      } else if (ScreenWidth >= 768) {
        if (this.sm) {
          const sm = this.sm.split(",");
          return (this.widthDialog =
            sm[0] * widthCols[1] + sm[1] * widthGutters[1] + "px");
        }
      } else if (ScreenWidth < 768) {
        if (this.xs) {
          const xs = this.xs.split(",");
          return (this.widthDialog =
            xs[0] * widthCols[0] + xs[1] * widthGutters[0] + "px");
        }
      }
    },
    handleResize() {
      this.windowWidth = window.innerWidth;
    },

    getVersionLayout(){
      switch(this.version){
        case 1:
        this.widthCol =  [68, 177, 71, 71, 71];
        this.widthGutter = [10, 10, 16, 24, 24];
        break;
        case 2:
          this.widthCol =  [68, 177, 71, 84, 84];
         this.widthGutter = [10, 10, 16, 24, 24];
         break;
      }
    }
  },
  created() {
    this.windowWidth = window.innerWidth;
    this.getWidth(this.windowWidth);
    window.addEventListener("resize", this.handleResize);
  },
  watch: {
    windowWidth(val) {
      this.getWidth(val);
    },
    loading(val) {
      if (val) {
        var dialog = document.querySelector(`#${this.id} .el-dialog`);
        this.load = Loading.service({
          fullscreen: false,
          customClass: "loading",
          target: dialog,
        });
      } else {
        if (this.load) {
          this.load.close();
        }
      }
    },
  },
  mixins: [Dialog],
};
</script>
<style lang="scss">
.dialogel {
  min-width: 200px;
}

.el-dialog__body {
  overflow-y: auto;
}

.el-dialog__wrapper .el-dialog {
  overflow-y: initial !important;
}

</style>