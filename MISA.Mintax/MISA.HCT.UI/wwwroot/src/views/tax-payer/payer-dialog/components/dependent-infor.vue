<template>
  <div>
    <el-form>
      <el-row :gutter="20" class="container-content">
        <el-col :lg="4" :md="4" :sm="4" :span="4" class="col-left-infor">
          <div class="left-infor">
            <el-row :gutter="4" class="infor-payer">
              <el-col :span="12">
                <div>Người nộp thuế:</div>
              </el-col>
              <el-col :span="12">
                Nguyễn Văn Nguyên
              </el-col>
            </el-row>
            <el-row :gutter="4" class="infor-payer">
              <el-col :span="12">
                <div>Mã nhân viên:</div>
              </el-col>
              <el-col :span="12">
                B24-0036
              </el-col>
            </el-row>
            <el-row :gutter="4" class="infor-payer">
              <el-col :span="12">
                <div>Mã số thuế cá nhân:</div>
              </el-col>
              <el-col :span="12">
                1234567890
              </el-col>
            </el-row>
            <el-row :gutter="4" class="infor-payer">
              <el-col :span="12">
                <div>Bộ phận/Phòng ban:</div>
              </el-col>
              <el-col :span="12">
                Văn Phòng MISA Hà Nội
              </el-col>
            </el-row>
            <el-row :gutter="4" class="infor-payer">
              <el-col :span="12">
                <div>Vị trí/Chức vụ:</div>
              </el-col>
              <el-col :span="12">
                Developer
              </el-col>
            </el-row>
          </div>
        </el-col>
        <el-col :lg="20" :md="20" :sm="20" :span="20" class="col-right-img">
          <div class="background-dependent-infor">
            <div class="image-class">
              <img src="@/assets/images/Group 19459.png" class="center"/>
            </div>
              <!--btn thêm người phụ thuộc-->
              <div class="btn-class">
                <m-btn
                :type="'btn-white2-icon'"
                :name="'Thêm người đăng ký'"
                class="bg-white"
              >
                <template v-slot:icon>
                  <img class="icon" src="@/icons/Component 97 – 61.png" />
                </template>
              </m-btn>
              </div>
          </div>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>

<script>
import Button from "@/components/buttons";
export default {
  components:{
    "m-btn": Button,
  }
}
</script>

<style lang="scss" scoped>
@import "@/styles/variables.scss";
.infor-payer{
  padding: 10px 0;
}
.el-dialog__body{
  padding-bottom: 0 !important;
}
.container-content{
  // padding-top: 10px;
  height: 766px;
  .col-right-img{
    height: 100%;
    .background-dependent-infor{
      width: 100%;
      height: 100%;
      background-color: $background-grey;
      .image-class{
        display: block;
        text-align: center;
        padding-top: 15%;
      }
      .btn-class{
        width: 100%;
        display: flex;
        justify-content: center;
      }
      // .center {
      //   display: block;
      //   margin:auto;
      // }
    }
  }
}
</style>