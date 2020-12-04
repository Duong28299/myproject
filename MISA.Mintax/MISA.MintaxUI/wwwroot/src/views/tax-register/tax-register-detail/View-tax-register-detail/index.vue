<template>
  <div class="view-tax-register-detail">
    <dialogel
      :titleDialog="title"
      :Width="dialogWidth"
      :dialogFormVisible="isTaxRegisterDialogOpen"
      :closeonclickmodal="false"
      @close-dialog="closeForm"
      :isWarnClose="isWarnClose"
      xl="8,9"
      lg="8,9"
      md="8,9"
      sm="4,3"
      xs="6,5"
    >
      <template v-slot:body>
        <el-row :gutter="24" style="padding-top: 12px">
          <!-- phần bên trái -->
          <el-col :lg="12" :md="24" :sm="24">
            <!-- Thông tin cá nhân -->
            <el-row>
              <el-col :span="12" class="text-left contract-tax pb-4">
                <div style="font-weight: bold">Thông tin cá nhân</div>
                <div class="tab-active" />
              </el-col>
            </el-row>
            <!-- mã nhân viên text input -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Mã nhân viên:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.EmployeeCode }}</p>
              </el-col>
            </el-row>
            <!-- họ và tên text input-->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Họ và tên:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.FullName }}</p>
              </el-col>
            </el-row>

            <!-- mã số thuế text input-->
            <el-row :gutter="24" v-if="GroupType == 20">
              <el-col :span="11">
                <div>Mã số thuế:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.TaxNo }}</p>
              </el-col>
            </el-row>

            <!-- Ngày sinh -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Ngày sinh:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ convertBirthday }}</p>
              </el-col>
            </el-row>
            <!-- giới tính -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Giới tính:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ getGenderName }}</p>
              </el-col>
            </el-row>

            <!-- Loại giẩy tờ -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Loại giấy tờ:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.IndentityTypeName }}</p>
              </el-col>
            </el-row>

            <!-- Số cmnd -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>
                  {{ DeclarationObjectDetail.IndentityTypeName | convertCMND }}:
                </div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.CitizenIdentityNo }}</p>
              </el-col>
            </el-row>
            <!-- Quốc tịch -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Quốc tịch:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.NationalityName }}</p>
              </el-col>
            </el-row>
            <!-- Ngày cấp  -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Ngày cấp:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ citizenIdentityDate }}</p>
              </el-col>
            </el-row>
            <!-- Nơi cấp -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Nơi cấp:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.CitizenIdentityPlaceName }}</p>
              </el-col>
            </el-row>

            <!-- BỘ phận/phòng ban  -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Bộ phận/Phòng ban:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.OrganizationUnitName }}</p>
              </el-col>
            </el-row>
            <!-- Chức vụ -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Vị trí/Chức vụ:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.PositionName }}</p>
              </el-col>
            </el-row>

            <!-- điện thoại  -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Điện thoại liên hệ:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.Phone }}</p>
              </el-col>
            </el-row>
            <!-- Email -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Email</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.Email }}</p>
              </el-col>
            </el-row>
          </el-col>

          <!-- phần bên phải -->
          <el-col :lg="12" :md="24" :sm="24">
            <!-- header địa chỉ theo hộ khẩu -->
            <el-row>
              <el-col :span="12" class="text-left contract-tax pb-4">
                <div style="font-weight: bold">Địa chỉ theo hộ khẩu</div>
                <div class="tab-active" />
              </el-col>
            </el-row>

            <!-- quốc gia thành phố theo hộ khảu -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Quốc gia:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.NationalNameByHousehold }}</p>
              </el-col>
            </el-row>
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Tỉnh/Thành phố:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.ProvinceNameByHousehold }}</p>
              </el-col>
            </el-row>

            <!-- quận huyện, xã phường theo hộ khẩu -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Quận/Huyện:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.DistrictNameByHousehold }}</p>
              </el-col>
            </el-row>
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Xã/Phường:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.WardNameByHousehold }}</p>
              </el-col>
            </el-row>

            <!-- Số nhà, thôn xóm theo hộ khẩu -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Địa chỉ chi tiết:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ AddressByHousehold }}</p>
              </el-col>
            </el-row>

            <!-- header địa chỉ theo cư trú -->
            <el-row style="padding-top: 24px">
              <el-col :span="12" class="text-left contract-tax pb-4">
                <div style="font-weight: bold">Địa chỉ cư trú</div>
                <div class="tab-active" />
              </el-col>
            </el-row>

            <!-- quốc gia thành phố theo cư trú -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Quốc gia:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.NationalNameByResident }}</p>
              </el-col>
            </el-row>
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Tỉnh/Thành phố:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.ProvinceNameByResident }}</p>
              </el-col>
            </el-row>

            <!-- quận huyện, xã phường theo hộ khẩu -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Quận/Huyện:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.DistrictNameByResident }}</p>
              </el-col>
            </el-row>
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Xã/Phường:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ DeclarationObjectDetail.WardNameByResident }}</p>
              </el-col>
            </el-row>

            <!-- Số nhà, thôn xóm theo hộ khẩu -->
            <el-row :gutter="24">
              <el-col :span="11">
                <div>Địa chỉ chi tiết:</div>
              </el-col>
              <el-col :span="13">
                <p>{{ AddressByResident }}</p>
              </el-col>
            </el-row>
          </el-col>
        </el-row>
      </template>
      <template v-slot:footer>
        <div>
          <m-btn
            type="btn-primary"
            :name="'Đóng'"
            :handleClick="cancel"
            style="margin-left: 16px"
          ></m-btn>
          <m-btn
            v-if="Declaration.DeclarationStatus == 1"
            name=" Sửa thông tin"
            type="btn-grey-icon"
            :handleClick="openEditDialog"
          >
            <template v-slot:icon>
              <img
                style="margin-right: 4px; margin-bottom: -4px"
                src="@/icons/Group 18651.png"
              />
            </template>
          </m-btn>
        </div>
      </template>
    </dialogel>
    <!-- form sửa dữ liệu  -->
  </div>
</template>

<script>
import Button from "@/components/buttons";
import dialogel from "@/components/dialog";
import { formatDate } from "@/utils/index.js";
export default {
  components: {
    dialogel,
    "m-btn": Button,
  },
  props: {
    isTaxRegisterDialogOpen: {
      type: Boolean,
      defaule: false,
    },
    DeclarationObjectDetail: {
      type: Object,
      default: null,
    },
    Declaration: {
      type: Object,
      default: null,
    },
    // GroupType 20 à chỉnh sửa, 21 là thêm mới
    GroupType: {
      type: Number,
      default: 0,
    },
  },

  data() {
    return {
      title: "THÔNG TIN CHI TIẾT NGƯỜI THAY ĐỔI THÔNG TIN MÃ SỐ THUẾ",
      isWarnClose: false,
      isNewSubcribersDialog: false,
      dialogWidth: "784px",
    };
  },

  computed: {
    getGenderName() {
      return this.DeclarationObjectDetail.Gender == 0 ? "Nam" : "Nữ";
    },

    convertBirthday() {
      return formatDate(this.DeclarationObjectDetail.DateOfBirth, "dd/mm/yyyy");
    },

    citizenIdentityDate() {
      return formatDate(
        this.DeclarationObjectDetail.CitizenIdentityDate,
        "dd/mm/yyyy"
      );
    },

    /**
     * computed trả lại giá trị địa chỉ hộ khẩu
     * created by ntdong 15/09/2020
     */
    AddressByHousehold() {
      var addressByHousehold = [];
      var arrAdd = [
        "AddressByHousehold",
        "WardNameByHousehold",
        "DistrictNameByHousehold",
        "ProvinceNameByHousehold",
        "NationalNameByHousehold",
      ];
      arrAdd.forEach((element) => {
        if (
          this.DeclarationObjectDetail[element] != null &&
          this.DeclarationObjectDetail[element] != ""
        )
          addressByHousehold.push(this.DeclarationObjectDetail[element]);
      });
      return addressByHousehold.join(", ");
    },

    /**
     * computed trả lại giá trị địa chỉ cư trú
     * created by ntdong 15/09/2020
     */
    AddressByResident() {
      var addressByResident = [];
      var arrAdd = [
        "AddressByResident",
        "WardNameByResident",
        "DistrictNameByResident",
        "ProvinceNameByResident",
        "NationalNameByResident",
      ];
      arrAdd.forEach((element) => {
        if (
          this.DeclarationObjectDetail[element] != null &&
          this.DeclarationObjectDetail[element] != ""
        )
          addressByResident.push(this.DeclarationObjectDetail[element]);
      });
      return addressByResident.join(", ");
    },
  },

  filters: {
    convertCMND(value) {
      if (value == "Chứng minh nhân dân") return "Số CMND";
      if (value == "Căn cước công dân") return "Số CCCD";
      if (value == "Hộ chiếu") return "Số hộ chiếu";
    },
  },
  mounted() {},
  created() {},
  methods: {
    /**
     * Hàm mở bảng
     * ntdong 15/09/2020
     */
    openEditDialog() {
      this.$emit("open-edit-dialog", this.DeclarationObjectDetail);
    },

    /**
     * Sự kiện khi click vào nút đóng form
     * Created by: ntdong 15/09/2020
     */
    closeForm() {
      this.$emit("close-dialog", false);
    },

    /**
     * Sự kiện khi click nút quay lại
     * createdby ntdong 15/09/2020
     */
    cancel() {
      this.$emit("close-dialog", false);
      this.checkEdited();
      if (this.isWarnClose == true) {
        this.buttons = [
          {
            text: "Không",
            class: "btn-grey none-border",
            callback: () => {},
          },
          {
            text: "Có",
            class: "btn-primary",
            callback: () => {
              this.$emit("close-dialog", false);
            },
          },
        ];
        this.$_Popup.warn(
          "Cảnh báo",
          "Thông tin vừa nhập liệu sẽ không được lưu lại. Bạn có chắc chắn muốn thoát không?",
          this.buttons
        );
      } else {
        this.$emit("close-dialog", false);
      }
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
@media screen and (min-width: 1400px) {
  .new-employee {
    .el-dialog__wrapper .el-dialog {
      height: 698px !important;
    }
  }
}
@media screen and (max-width: 991px) {
  .date-padding-small-screen {
    margin-bottom: 30px;
  }
}

.view-tax-register-detail {
  .el-col {
    .el-col-13 {
      p {
        font-size: 15px;
        margin: 0;
      }
    }
  }

  .el-row {
    margin-bottom: 13px;
  }

  .tab-active {
    position: absolute;
    width: 60px;
    bottom: 0px;
    height: 3px;
    background-color: $seconds;
    border-top-left-radius: 50px;
    border-top-right-radius: 50px;
  }
  .btn-white2-icon {
    span {
      display: flex;
      align-items: center;
    }
  }
  .el-dialog__body {
    max-height: 636px;
  }
  .dialogel {
    max-width: 784px;
  }
}
</style>