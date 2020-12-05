<template>
  <dialogel
    :id="'tax-register-declaration-dialog'"
    :titleDialog="title"
    :dialogFormVisible="isDeclarationDialog"
    :closeonclickmodal="false"
    @close-dialog="closeForm"
    :customClass="'tax-register-declaration-dialog'"
    xl="20,21"
    lg="20,21"
    md="20,21"
    sm="20,21"
    xs="20,21"
  >
    <template v-slot:body>
      <div class="content">
        <div class="header">
          <div class="title-brand bold">
            TỜ KHAI ĐĂNG KÝ THUẾ TỔNG HỢP CHO CÁ NHÂN CÓ THU NHẬP TỪ TIỀN LƯƠNG,
            TIỀN CÔNG THÔNG QUA CHI TRẢ THU NHẬP
          </div>
          <div v-if="isTaxRegister && !isChangeTaxRegister" class="content-tag">
            Đăng ký mã số thuế cá nhân
          </div>
          <div v-else class="content-tag">
            Thay đổi thông tin mã số thuế cá nhân
          </div>
        </div>
        <div class="body">
          <div class="info-organization">
            <div v-if="loadingInfo" v-loading="loadingInfo"></div>
            <el-row :gutter="24">
              <!-- phần bên trái -->
              <el-col :lg="8" :md="8" :sm="24">
                <!-- Thông tin cá nhân -->
                <el-row>
                  <el-col :span="12" class="text-left contract-tax">
                    <div class="label-bold">Thông tin đơn vị</div>
                    <div class="tab-active" />
                  </el-col>
                </el-row>
                <!--mã số thuế và mã nhân viên text input -->
                <el-row :gutter="24" class="info-row">
                  <el-col :span="11">
                    <div class="label-title">Tên cơ quan chi trả thu nhập:</div>
                  </el-col>
                  <el-col :span="13">
                    <div class="label-content">{{ organizationName }}</div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="11">
                    <div class="label-title">Mã số thuế:</div>
                  </el-col>
                  <el-col :span="13">
                    <div class="label-content">{{ organizationTaxNo }}</div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="11">
                    <div class="label-title">Cơ quan thuế quản lý:</div>
                  </el-col>
                  <el-col :span="13">
                    <div class="label-content">{{ TaxAgentName }}</div>
                  </el-col>
                </el-row>
              </el-col>
              <el-col :lg="8" :md="8" :sm="24">
                <!-- Thông tin Nhân viên đại lý thuế -->
                <el-row>
                  <el-col :span="12" class="text-left contract-tax">
                    <div class="label-bold">Nhân viên đại lý thuế</div>
                    <div class="tab-active" />
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="11">
                    <div class="label-title">Họ và tên:</div>
                  </el-col>
                  <el-col :span="13">
                    <div class="label-content">{{ TaxAgentEmployee }}</div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="11">
                    <div class="label-title">Chứng chỉ hành nghề số:</div>
                  </el-col>
                  <el-col :span="13">
                    <div class="label-content">
                      {{ TaxAgentPracticingCertifacateNo }}
                    </div>
                  </el-col>
                </el-row>
              </el-col>
              <el-col :lg="8" :md="8" :sm="24">
                <!-- Thông tin ký -->
                <el-row>
                  <el-col :span="12" class="text-left contract-tax">
                    <div class="label-bold">Thông tin ký</div>
                    <div class="tab-active" />
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="11">
                    <div class="label-title">Họ và tên người ký:</div>
                  </el-col>
                  <el-col :span="13">
                    <div class="label-content">{{ SignBy }}</div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="11">
                    <div class="label-title">Ngày ký:</div>
                  </el-col>
                  <el-col :span="13">
                    <div class="label-content">
                      {{ formatDate(SignDate, "dd/mm/yyyy") }}
                    </div>
                  </el-col>
                </el-row>
              </el-col>
            </el-row>
          </div>
          <div class="table-declaration-tax-data" v-if="dataViewDeclaration">
            <tbl
              :typeTable="'tableData'"
              :listColumn="listColumn"
              :dataTable="dataViewDeclaration"
              :summary="false"
              :loading="false"
              :tooltip="true"
              :havePagination="true"
              :haveAction="false"
              :haveAdjust="false"
              :something="false"
              :displayIndexRow="true"
              :total="numberOfRecord"
              @pagination="getFilter"
              v-loading="loadingTable"
            />
          </div>
          <div class="mode-btn" v-else>
            <img
              src="@/assets/images/Group 18279.png"
              alt="not have data"
              style="position: relative"
            />
            <span>Không có dữ liệu người nộp thuế</span>
          </div>
        </div>
      </div>
    </template>
    <template v-slot:footer>
      <div>
        <m-btn
          type="btn-primary-icon"
          name="Ký nộp qua mTax"
          style="padding-left: 10px"
          :handleClick="registerByMTAX"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18580@2x.png" />
          </template>
        </m-btn>
        <m-btn type="btn-grey" name="Đóng" :handleClick="closeForm"></m-btn>
      </div>
    </template>
  </dialogel>
</template>

<script>
import Button from "@/components/buttons";
import dialogel from "@/components/dialog";
import tbl from "@/components/tables";
import signDeclaration from "@/mixins/signDeclaration";
import { formatDate } from "@/utils/index.js";
import declarationDetailService from "@/api/etax/declaration-detail.js";

export default {
  components: {
    dialogel,
    "m-btn": Button,
    tbl,
  },
  props: {
    isDeclarationDialog: {
      type: Boolean,
      default: false,
    },
    declarationID: {
      type: String,
      default: "",
    },
  },
  mixins: [signDeclaration],
  data() {
    return {
      title: "Tờ khai đăng ký mã số thuế cá nhân",
      formData: {},
      isWarnClose: false,
      listColumn: [
        {
          label: "Mã số thuế người nộp thuế",
          propertyName: "TaxNo",
          align: "left",
          width: "",
          minWidth: "120",
          type: "",
          prop: "TaxNo",
          sortable: false,
        },
        {
          label: "Họ và tên người nộp thuế",
          propertyName: "FullName",
          align: "left",
          width: "",
          minWidth: "180",
          type: "",
          prop: "FullName",
          sortable: false,
        },
        {
          label: "Ngày sinh",
          propertyName: "DateOfBirth",
          align: "center",
          width: "",
          minWidth: "120",
          type: "date",
          prop: "DateOfBirth",
          sortable: false,
        },
        {
          label: "Giới tính",
          propertyName: "Gender",
          align: "left",
          width: "",
          minWidth: "60",
          type: "gender",
          prop: "Gender",
          sortable: false,
        },
        {
          label: "Quốc tịch",
          propertyName: "NationalityName",
          align: "left",
          width: "",
          minWidth: "120",
          type: "",
          prop: "NationalityName",
          sortable: false,
        },
        {
          label: "CMTND kê khai sai",
          propertyName: "CitizenIdentityNoStatus",
          align: "left",
          width: "",
          minWidth: "120",
          type: "",
          prop: "CitizenIdentityNoStatus",
          sortable: false,
        },
        {
          label: "Loại giấy tờ",
          propertyName: "IndentityTypeName",
          align: "left",
          width: "",
          minWidth: "160",
          type: "",
          prop: "IndentityTypeName",
          sortable: false,
        },
        {
          label: "Số",
          propertyName: "CitizenIdentityNo",
          align: "left",
          width: "",
          minWidth: "120",
          type: "",
          prop: "CitizenIdentityNo",
          sortable: false,
        },
        {
          label: "Ngày cấp",
          propertyName: "CitizenIdentityDate",
          align: "left",
          width: "",
          minWidth: "120",
          type: "date",
          prop: "CitizenIdentityDate",
          sortable: false,
        },
        {
          label: "Nơi cấp",
          propertyName: "CitizenIdentityPlaceName",
          align: "left",
          width: "",
          minWidth: "120",
          type: "",
          prop: "CitizenIdentityPlaceName",
          sortable: false,
        },
        {
          label: `Địa chỉ đăng ký theo hộ khẩu`,
          align: "center",
          width: "",
          minWidth: "140",
          type: "",
          sortable: false,
          group: [
            {
              label: "Số nhà, đường phố, thôn, xóm",
              propertyName: "AddressByHousehold",
              align: "left",
              width: "",
              minWidth: "160",
              type: "",
              prop: "AddressByHousehold",
              sortable: false,
            },
            {
              label: "Xã/Phường",
              propertyName: "WardNameByHousehold",
              align: "left",
              width: "",
              minWidth: "160",
              type: "",
              prop: "WardNameByHousehold",
              sortable: false,
            },
            {
              label: "Quận/Huyện",
              propertyName: "DistrictNameByHousehold",
              align: "left",
              width: "",
              minWidth: "160",
              type: "",
              prop: "DistrictNameByHousehold",
              sortable: false,
            },
            {
              label: "Tỉnh/Thành phố",
              propertyName: "ProvinceNameByHousehold",
              align: "left",
              width: "",
              minWidth: "160",
              type: "",
              prop: "ProvinceNameByHousehold",
              sortable: false,
            },
            {
              label: "Quốc gia",
              propertyName: "NationalNameByHousehold",
              align: "left",
              width: "",
              minWidth: "120",
              type: "",
              prop: "NationalNameByHousehold",
              sortable: false,
            },
          ],
        },
        {
          label: `Địa chỉ cư trú`,
          align: "center",
          width: "",
          minWidth: "160",
          type: "",
          sortable: false,
          group: [
            {
              label: "Số nhà, đường phố, thôn, xóm",
              propertyName: "AddressByResident",
              align: "left",
              width: "",
              minWidth: "160",
              type: "",
              prop: "AddressByResident",
              sortable: false,
            },
            {
              label: "Xã/Phường",
              propertyName: "WardNameByResident",
              align: "left",
              width: "",
              minWidth: "160",
              type: "",
              prop: "WardNameByResident",
              sortable: false,
            },
            {
              label: "Quận/Huyện",
              propertyName: "DistrictNameByResident",
              align: "left",
              width: "",
              minWidth: "160",
              type: "",
              prop: "DistrictNameByResident",
              sortable: false,
            },
            {
              label: "Tỉnh/Thành phố",
              propertyName: "ProvinceNameByResident",
              align: "left",
              width: "",
              minWidth: "160",
              type: "",
              prop: "ProvinceNameByResident",
              sortable: false,
            },
            {
              label: "Quốc gia",
              propertyName: "NationalNameByResident",
              align: "left",
              width: "",
              minWidth: "120",
              type: "",
              prop: "NationalNameByResident",
              sortable: false,
            },
          ],
        },
        {
          label: "Số điện thoại liên hệ",
          propertyName: "Phone",
          align: "left",
          width: "",
          minWidth: "120",
          type: "",
          prop: "Phone",
          sortable: false,
        },
        {
          label: "Email",
          propertyName: "Email",
          align: "left",
          width: "",
          minWidth: "160",
          type: "",
          prop: "Email",
          sortable: false,
        },
      ],
      loadingInfo: true,
      loadingTable: true,
      numberOfRecord: 0,
      filterBar: {
        declarationID: this.declarationID,
        pageIndex: 1,
        pageSize: 25,
        isFirst: true,
      },
      organizationTaxNo: "",
      organizationName: "",
      SignBy: "",
      SignDate: "",
      TaxAgentEmployee: "",
      TaxAgentPracticingCertifacateNo: "",
      TaxAgentName: "",
      isTaxRegister: false,
      isChangeTaxRegister: false,
      dataViewDeclaration: {},
    };
  },
  computed: {},
  filters: {},
  watch: {
    isDeclarationDialog(val) {
      if (val) {
        this.getDataDeclarationDetailTaxRegisterDialog();
      }
    },
  },
  mounted() {},
  methods: {
    /**
     * Lấy dữ liệu cho dialog declaration
     * createdby vdthang 21.11.2020
     */
    async getDataDeclarationDetailTaxRegisterDialog() {
      this.loadingInfo = true;
      this.loadingTable = true;
      var res = await declarationDetailService.getDeclarationDetailTaxRegister(
        this.filterBar
      );
      if (typeof res.data != "undefined" && res.data.Success) {
        // Nếu lần đầu call lấy dữ liệu
        if (this.filterBar.isFirst) {
          this.organizationTaxNo = res.data.Data.OrganizationTaxNo;
          this.organizationName = res.data.Data.OrganizationName;
          this.SignBy = res.data.Data.SignBy;
          this.SignDate = res.data.Data.SignDate;
          this.TaxAgentEmployee = res.data.Data.TaxAgentEmployee;
          this.TaxAgentPracticingCertifacateNo =
            res.data.Data.TaxAgentPracticingCertifacateNo;
          this.TaxAgentName = res.data.Data.TaxAgentName;
          this.isTaxRegister = res.data.Data.GroupType === 21 ? true : false;
          this.isChangeTaxRegister =
            res.data.Data.GroupType === 20 ? true : false;
          this.dataViewDeclaration =
            res.data.Data.DeclarationDetailList.DeclarationDetails;
          this.numberOfRecord =
            res.data.Data.DeclarationDetailList.TotalDeclarationDetail;
          this.loadingTable = false;
          this.loadingInfo = false;
        } else {
          this.numberOfRecord = res.data.Data.TotalDeclarationDetail;
          this.dataViewDeclaration = res.data.Data.DeclarationDetails;
          this.loadingInfo = false;
          this.loadingTable = false;
        }
      }
    },

    /**
     * Chuyển data về đúng kiểu cho tờ khai
     * createdby nqhiep 22.09.2020
     */
    formatDataDeclaration(val) {
      val.map((item, index) => {
        item.Serial = index + 1;
        item.DateOfBirth = this.formatDate(item.DateOfBirth);
        item.CitizenIdentityDate = this.formatDate(item.CitizenIdentityDate);
        item.Gender = item.Gender ? "Nữ" : "Nam";
      });
      return val;
    },

    /**
     * Hàm thực hiện định dạng ngày ký nộp
     * created by vdthang 20.11.2020
     */
    formatDate(data, type = "dd/mm/yyyy") {
      return formatDate(data, type);
    },

    /**
     * Hàm thực hiện build filter tìm kiếm trong dialog
     * created by vdthang 20.11.2020
     */
    getFilter(val) {
      this.filterBar.pageIndex = val.page;
      this.filterBar.pageSize = val.limit;
      this.filterBar.isFirst = false;
      this.getDataDeclarationDetailTaxRegisterDialog();
    },

    /**
     * Sự kiện khi click nút đóng trên header
     * createdby nqhiep 23.09.2020
     */
    closeForm() {
      this.$emit("close-dialog", false);
    },

    /**
     * Khởi tạo giá trị đối với mixin kí tờ khai
     * created by nvbinh2 11.11.2020
     */
    initDataMixins() {
      this.keyProperty = "DeclarationID";
      this.mode = "Declaration";
      this.nameOfDeclaration = "DeclarationName";
    },
  },
  created() {
    this.initDataMixins();
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
#tax-register-declaration-dialog {
  border-radius: 5px;
  width: calc(100% - 48px);
  height: calc(100% - 25px);
  margin: 25px 24px 0px 24px;

  .content {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;

    .body {
      height: calc(100% + 14px);

      .info-organization {
        position: relative;

        .el-loading-parent--relative{
          position: absolute !important;
          top: 0;
          left: 0;
          right: 0;
          bottom: 0;
          z-index: 1;

          .el-loading-mask{
            background-color: $white !important;
          }
        }

        .label-bold {
          font-family: $_fontbold;
        }

        .tab-active {
          margin-top: 4px;
          width: 72px;
          bottom: 0px;
          height: 1px;
          background-color: $seconds;
          position: unset;
        }
      }
      .table-declaration-tax-data {
        padding: 16px;
        background-color: $background-grey;
        margin-top: 16px;
        height: calc(100% - 131px);
      }
    }
  }
}

.info-row {
  height: 31px;
  line-height: 31px;
}

.el-table .cell {
  word-break: break-word;
  white-space: unset;
}

.header {
  padding: 24px 0px;
  border-top: 1px solid $background-grey;
  position: sticky;
  top: 0;
  background-color: $white;
  z-index: 10;

  .title-brand {
    font-size: 20px;
    color: $primary;
    font-family: $_fontbold;
    height: 26px;
    align-self: center;
  }

  .content-tag {
    background-color: $hover-blue;
    height: 32px;
    border-radius: 5px;
    display: flex;
    width: fit-content;
    padding: 0 16px;
    margin-top: 10px;
    align-items: center;
    color: $light-primary;
    justify-content: center;
  }
}

.bold {
  font-weight: bold;
  font-size: 15px;
}

.mode-btn {
  position: absolute;
  top: 57%;
  left: 50%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  transform: translate(-50%, -50%);
}

.tax-register-declaration-dialog {
  height: 100%;

  .el-dialog__header {
    padding-bottom: 10px;
  }

  .el-dialog__body {
    height: calc(100% - 120px);
    padding: 0 24px;
    position: relative;
  }

  .label-title {
    color: $medium-dark-grey;
    font-family: $_fontregular;
    font-size: 12px;
  }

  .label-content {
    font-family: $_fontmedium;
    font-size: 12px;
  }

  .table-declaration-tax-data {
    .el-table__body-wrapper {
      height: calc(100% - 83px) !important;
    }
  }
}

.gutter {
  display: none;
}
</style>