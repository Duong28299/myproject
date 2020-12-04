<template>
  <div class="button-container">
    <el-button
      @click="handleClick"
      type="success"
      v-if="type == 'btn-success'"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >
    <!-- warning button -->
    <el-button
      @click="handleClick"
      type="warning"
      v-if="type == 'btn-waring'"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >
    <!-- danger button -->
    <el-button
      @click="handleClick"
      type="danger"
      v-if="type == 'btn-danger'"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >

    <!-- infor button -->
    <el-button
      @click="handleClick"
      type="infor"
      id="btn-tab"
      v-if="type == 'btn-infor'"
      :class="{ 'btn-infor-active': active == true }"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >

    <!-- text button -->
    <el-button
      @click="handleClick"
      type="text"
      v-if="type == 'btn-text'"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >
    <el-button
      class="btn-tool"
      @click="handleClick"
      type="tool"
      v-if="type == 'btn-tool'"
      :icon="getIcon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >

    <!-- Multi Buton (V1.0)-->
    <div id="multi-button" v-if="type == 'multi-button'" >
      <el-dropdown
        split-button
        class="btn-primary-icon"
        :size="size"
        trigger="click"
        @click="handleClick"
      >
        <slot name="icon"></slot>
        {{ name }}
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item
            v-for="(item, index) in dataDropdown"
            :key="index"
            :icon="item.icon"
            style="padding: 0px"
            :class="customWidth"
          >
            <el-button type="text" @click="onClick" :nameVal="item.nameVal">{{
              item.name
            }}</el-button>
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </div>

    <!-- Multi Buton 2 (V1.0) -->
    <div id="multi-button2" v-if="type == 'multi-button2'">
      <el-dropdown trigger="click">
        <el-button
          :icon="icon"
          class="btn-white-icon"
          style="padding-right: 10px"
        >
          <i :class="iconel" id="icon" />
          <slot name="icon"></slot>
          {{ name }}
          <i class="el-icon-arrow-down el-icon--right"></i>
        </el-button>
        <el-dropdown-menu slot="dropdown">
          <!-- <slot name="menu"></slot> -->
          <el-dropdown-item
            v-for="(item, index) in dataDropdown"
            :key="index"
            :icon="item.icon"
            style="padding: 0px"
          >
            <el-button type="text" @click="onClick" :nameVal="item.nameVal">{{
              item.name
            }}</el-button>
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </div>

    <div id="multi-button3" v-if="type == 'multi-button3'">
      <el-dropdown
        split-button
        class="btn-white-icon"
        :size="size"
        trigger="click"
        @click="handleClick"
        width="100%"
      >
        <slot name="icon"></slot>
        {{ name }}
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item
            v-for="(item, index) in dataDropdown"
            :key="index"
            :icon="item.icon"
            style="padding: 0px"
          >
            <el-button type="text" @click="onClick" :nameVal="item.nameVal">{{
              item.name
            }}</el-button>
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </div>

    <div id="multi-button4" v-if="type == 'multi-button4'">
      <el-dropdown trigger="click">
        <el-tooltip
          effect="light"
          :content="tooltip"
          :placement="positionTooltip"
          popper-class="tooltip"
        >
          <el-button
            :icon="icon"
            class="btn-white3-onlyicon"
            style="padding-right: 10px"
          >
            <i :class="iconel" id="icon" />
            <slot name="icon"></slot>
            {{ name }}
          </el-button>
        </el-tooltip>
        <el-dropdown-menu slot="dropdown">
          <!-- <slot name="menu"></slot> -->
          <el-dropdown-item
            v-for="(item, index) in dataDropdown"
            :key="index"
            :icon="item.icon"
            style="padding: 0px"
          >
            <el-button type="text" @click="onClick" :nameVal="item.nameVal">{{
              item.name
            }}</el-button>
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </div>

    <!-- Button primary -->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-primary'"
      class="btn-primary"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >
    <!-- Button primary icon-->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-primary-icon'"
      class="btn-primary-icon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <slot name="icon"></slot>
      <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      {{ name }}
    </el-button>

    <!-- Button primary mình icon-->
    <el-tooltip
      effect="light"
      :content="tooltip"
      :placement="positionTooltip"
      popper-class="tooltip"
    >
      <el-button
        @click="handleClick"
        v-if="type == 'btn-primary-onlyicon'"
        class="btn-primary-onlyicon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
      >
        <slot name="icon"> </slot>
        <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      </el-button>
    </el-tooltip>

    <!-- Button grey -->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-grey'"
      class="btn-grey"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >

    <!-- Button grey icon-->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-grey-icon'"
      class="btn-grey-icon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <slot name="icon"></slot>
      <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      {{ name }}
    </el-button>

    <!-- Button grey mình icon-->
    <el-tooltip effect="light" :content="tooltip" :placement="positionTooltip" >
      <el-button
        @click="handleClick"
        v-if="type == 'btn-grey-onlyicon'"
        class="btn-grey-onlyicon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
      >
        <slot name="icon"> </slot>
        <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      </el-button>
    </el-tooltip>

    <!-- Button delete -->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-delete'"
      class="btn-delete"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >

    <!-- Button delete icon-->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-delete-icon'"
      class="btn-delete-icon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <slot name="icon"></slot>
      <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      {{ name }}
    </el-button>

    <!-- Button grey mình icon-->
    <el-tooltip effect="light" :content="tooltip" :placement="positionTooltip">
      <el-button
        @click="handleClick"
        v-if="type == 'btn-delete-onlyicon'"
        class="btn-delete-onlyicon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
      >
        <slot name="icon"> </slot>
        <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      </el-button>
    </el-tooltip>

    <!-- Button button warn -->
    <el-button
      @click="handleClick"
      v-if="type == 'button warn'"
      class="button warn"
      :icon="icon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <i :class="iconel" id="icon"></i>
      {{ name }}
    </el-button>

    <!-- Button seconds -->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-seconds'"
      class="btn-seconds"
      :icon="getIcon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <i :class="iconel" id="icon"></i>
      {{ name }}
    </el-button>

    <!-- Button seconds text icon -->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-seconds-icon'"
      class="btn-seconds-icon"
      :icon="getIcon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <slot name="icon"></slot>
      <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      {{ name }}
    </el-button>

    <!-- Button seconds mifnh icon-->
    <el-tooltip effect="light" :content="tooltip" :placement="positionTooltip">
      <el-button
        @click="handleClick"
        v-if="type == 'btn-seconds-onlyicon'"
        class="btn-seconds-onlyicon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
      >
        <slot name="icon"> </slot>
        <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      </el-button>
    </el-tooltip>

    <!-- Button white -->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-white'"
      class="btn-white"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >
    <!-- Button white icon-->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-white-icon'"
      class="btn-white-icon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <slot name="icon"></slot>
      <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      {{ name }}
    </el-button>

    <!-- Button white mình icon-->
    <el-tooltip
      effect="light"
      :content="tooltip"
      :placement="positionTooltip"
      popper-class="tooltip"
    >
      <el-button
        @click="handleClick"
        v-if="type == 'btn-white-onlyicon'"
        class="btn-white-onlyicon"
        :icon="getIcon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
      >
        <slot name="icon"> </slot>
        <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      </el-button>
    </el-tooltip>

    <!-- Button white2 (không có border)-->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-white2'"
      class="btn-white2"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      >{{ name }}</el-button
    >
    <!-- Button white2 icon-->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-white2-icon'"
      class="btn-white2-icon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <slot name="icon"></slot>
      <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      {{ name }}
    </el-button>

    <!-- Button white 2 mình icon-->
    <el-tooltip
      effect="light"
      :content="tooltip"
      :placement="positionTooltip"
      popper-class="tooltip"
    >
      <el-button
        @click="handleClick"
        v-if="type == 'btn-white2-onlyicon'"
        class="btn-white2-onlyicon"
        :icon="getIcon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
      >
        <slot name="icon"> </slot>
        <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      </el-button>
    </el-tooltip>

    <!-- Button white 2 mình icon size icon 20px-->
    <el-tooltip
      effect="light"
      :content="tooltip"
      :placement="positionTooltip"
      popper-class="tooltip"
    >
      <el-button
        @click="handleClick"
        v-if="type == 'btn-white2-onlyicon-big'"
        class="btn-white2-onlyicon-big"
        :icon="getIcon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
      >
        <slot name="icon"> </slot>
        <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      </el-button>
    </el-tooltip>

    <!-- Button white border primary mình icon-->
    <el-tooltip
      effect="light"
      :content="tooltip"
      :placement="positionTooltip"
      popper-class="tooltip"
    >
      <el-button
        @click="handleClick"
        v-if="type == 'btn-white3-onlyicon'"
        class="btn-white3-onlyicon"
        :icon="getIcon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
      >
        <slot name="icon"> </slot>
        <i v-if="iconel != null" :class="iconel" id="iconel"></i>
      </el-button>
    </el-tooltip>

    <!-- Button button cảnh báo -->
    <el-button
      @click="handleClick"
      v-if="type == 'btn-warn'"
      class="btn-warn"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <i :class="iconel" id="icon"></i>
      {{ name }}
    </el-button>

    <!-- button tab -->
    <div v-if="type == 'btn-tab'" class="tabbar">
      <el-button
        @click="handleClick"
        class="btn-tab"
        :class="{ 'btn-tab-active': active == true }"
        :icon="getIcon"
        :size="size"
        :plain="plain"
        :round="round"
        :circle="circle"
        :loading="loading"
        :disabled="disabled"
        >{{ name }}</el-button
      >
      <div v-if="active" class="tab-active" />
    </div>

    <!-- button checkbox icon-->
    <el-button
      @click="handleClickBtnicon"
      :class="{ active: value }"
      v-if="type == 'btn-checkbox-icon'"
      class="btn-chk-icon"
      :icon="getIcon"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
    >
      <slot name="icon"></slot>
      {{ name }}
      <el-checkbox name="type" v-model="value"></el-checkbox>
    </el-button>

    <!-- <div v-if="type == 'btn-checkbox-icon'" class="btn-chk-icon">
      <div class="btn-chk-icon--container" :class="{ active: ckValue }">
        <slot name="icon"></slot>
        <img :src="icon" class="icon-chk" />
        {{ name }}
        <el-checkbox name="type" v-model="ckValue"></el-checkbox>
      </div>
    </div> -->

    <el-button @click="handleClick" v-if="type == 'btn-chip-info'" class="btn-chip-info" :icon="getIcon" :size="size"  >
      <slot name="icon"></slot>
      {{ name }}
    </el-button>
  </div>
</template>

<script>
import "@/styles/variables.scss";
export default {
  name: "Button",
  props: {
    /**
     * Tên icon muốn thêm vào
     */
    iconel: {
      type: String,
      default: null,
    },
    /**
     * Dạng button = btn-primary / btn-success / btn-warning / btn-danger / btn-infor / btn-text / multi-button / multi-button2
     * Mặc định: btn-primary
     */
    type: {
      type: String,
      default: "primary", //default:
    },
    /**
     * Hàm xử lý click của button
     */
    handleClick: {
      type: Function,
      default: () => {},
    },
    /**
     * Tên button
     */
    name: {
      type: String,
      default: null,
    },
    /**
     * icon hiển thị trên button
     */
    icon: {
      type: String,
      default: null,
    },
    /**
     * icon change hiển thị trên button
     */
    iconChange: {
      type: String,
      default: null,
    },
    /**
     * Kích thước của button = mini / small / medium
     * Mặc định: medium
     */
    size: {
      type: String,
      default: "medium",
    },
    /**
     * Kiểu button có background mờ
     * Mặc định: false
     */
    plain: {
      type: Boolean,
      default: false,
    },
    /**
     * Kiểu button có đường viền tròn
     * Mặc định: false
     */
    round: {
      type: Boolean,
      default: false,
    },
    /**
     * Kiểu button hình tròn
     * Mặc định: false
     */
    circle: {
      type: Boolean,
      default: false,
    },
    /**
     * Kiểu button có trạng thái loading
     * Nếu loading = true thì icon loading sẽ thay thế icon trong button
     * Mặc định: false
     */
    loading: {
      type: Boolean,
      default: false,
    },
    /**
     * Tắt button
     * Mặc định: false
     */
    disabled: {
      type: Boolean,
      default: false,
    },
    /**
     * Tên các subButton bên trong button dạng dropdown
     * Gồm các tham số truyền vào:
     *  name -> Tên của hiển thị của dropdown
     *  icon -> Icon hiển thị trên dropdown
     *  openModal -> Tên sự kiện để bắt event người dùng click vào dropdown
     * Mặc định: null
     */
    dataDropdown: {
      type: Array,
      default: null,
    },
    /**
     * Sử dụng custom class
     * Mặc định: false
     */
    active: {
      type: Boolean,
      default: false,
    },
    /**
     * Truyền vào tooltip
     */
    tooltip: {
      type: String,
      default: null,
    },

    /**
     * Vị trí hiển thị tooltip
     * (top|left|right|bottom)+("",start, end)
     */
    positionTooltip: {
      type: String,
      default: "top",
    },
    ckValue: {
      type: Boolean,
      default: false,
    },

    /**
     * Sử dụng cho "button checkbox icon"
     * true: Hoạt động giống radio button, button đang được chọn sẽ không bị bỏ chọn nếu nhấn lại lần nữa
     */
    radioType: {
      type: Boolean,
      default: false,
    },

    customWidth:{
      type:String,
      default:null
    }
  },
  data() {
    return {
      value: false,
    };
  },
  created() {
    this.value = this.ckValue;
  },
  watch: {
    ckValue(value) {
      this.value = value;
    },
  },
  methods: {
    /**
     * Hàm bắt sự kiện click của các dropdown
     */
    onClick(e) {
      let name = e.currentTarget.getAttribute("nameVal");
      this.$emit(name);
    },
    handleClickBtnicon() {
      if (!this.radioType) this.value = !this.value;
      this.$emit("getValue", this.value);
    },
  },
  computed: {
    getIcon: {
      get() {
        return "@/icons/" + this.icon + ".png";
      },
      set() {},
    },
  },
};
</script>
<style lang="scss">
@import "@/styles/variables.scss";
.tabbar {
  position: relative;
  width: auto;
  height: auto;
}
.tab-active {
  position: absolute;
  width: 100%;
  bottom: 0px;
  height: 3px;
  background-color: $seconds;
  // border-top-left-radius: 50px;
  // border-top-right-radius: 50px;
}
.btn-tab {
  border: none;
  &:hover{
    border-radius: 5px 5px 0 0 !important;
  }
}
// css tab
.tabbar{
  .btn-tab:hover{
    background-color: $bg-color-1;
  }
}

#multi-button2, #multi-button4 {
  .el-dropdown {
    border: 0px !important;
  }
}

#multi-button3 {
  .el-dropdown {
    border: 0px !important;
  }
  .el-button-group {
    display: flex;
    height: 100%;

    .el-button {
      float: left;
      border: none;
      color: $hover-primary;
      &:hover {
        background-color: $bg-color-2;
      }
    }
  }

  .btn-white-icon button {
    display: inline-flex;
    .icon {
      margin: 0px !important;
    }
  }
}

.btn-primary-icon button {
  display: inline-flex;
  .icon {
    margin: 0px !important;
  }
}
// chỉnh chiều rông dropdown xổ xuống
.custom-width-multi-button{
  width: 223px;
}

</style>
