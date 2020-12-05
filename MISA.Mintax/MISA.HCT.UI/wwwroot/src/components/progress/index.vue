<template>
  <div class="progress" v-bind:id="id">
    <div class="step1-progress step-progress active">
      <span class="progress-number">
        <template v-if="isCompleteStep1">
          <span class="tick"></span>
        </template>
        <template v-else>1</template>
      </span>
      <p class="m-0">{{step1Title}}</p>
    </div>
    <hr class="dotted" />
    <div class="step2-progress step-progress">
      <span class="progress-number">2</span>
      <p class="m-0">{{step2Title}}</p>
    </div>
  </div>
</template>
<script>
import $ from "jquery";
export default {
  name: "Progress",
  props: {
    id: {
      required: true,
      type: String,
      default: "two-step-progress",
    },
    step1Title: {
      type: String,
      default: "Bước 1",
    },
    step2Title: {
      type: String,
      default: "Bước 2",
    },
    step3Title: {
      type: String,
      default: "Bước 3",
    },
    isNext: {
      type: Boolean,
      default: false,
    },
  },
  created() {},
  data() {
    return {
      currentProgress: 1,
      isCompleteStep1: false,
    };
  },
  mounted() {},
  methods: {
    /**
             * Hàm để chạy đến bước tiếp theo

             * */
    nextProgress() {
      this.isCompleteStep1 = true;
      var step1Element = $("#" + this.id + " .step1-progress");
      var step2Element = $("#" + this.id + " .step2-progress");
      if (!step2Element.hasClass("active")) {
        step2Element.addClass("active");
        step1Element.removeClass("active");
        if (!step1Element.hasClass("complete-progress")) {
          step1Element.addClass("complete-progress");
        }
      }
    },
    backProgress() {
      this.isCompleteStep1 = false;
      var step1Element = $("#" + this.id + " .step1-progress");
      var step2Element = $("#" + this.id + " .step2-progress");
      if (!step1Element.hasClass("active")) {
        //Đặt lại các class về các class ban đầu
        step1Element.attr("class", "step1-progress step-progress active");
        step2Element.removeClass("active");
      }
    },
  },
  watch: {
    isNext(newVal) {
      // Nếu là true thì chuyển sang bước 2
      if (newVal === true) {
        this.currentProgress = 2;
        this.nextProgress();
      }
      // Nếu là false thì quay trở lại bước 1
      else {
        this.currentProgress = 1;
        this.backProgress();
      }
    },
  },
};
</script>
<style lang="scss" scoped>
@import "@/styles/twostepprogress.scss";
</style>
