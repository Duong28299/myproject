<template>
  <div class="view-declaration">
    <dialogel
      :id="'dependent-register-declaration-dialog'"
      :titleDialog="title"
      :dialogFormVisible="viewDeclarationFlag"
      @close-dialog="getClosing"
      :customClass="'dependent-register-declaration-dialog'"
      xl="20,21"
      lg="20,21"
      md="20,21"
      sm="20,21"
      xs="20,21"
    >
      <template v-slot:body>
        <!-- Phần thông tin chi tiết -->
        <!-- cột bên trái -->
        <section class="upper-content-sticky">
          <div class="the-first-infor">
            <div class="detail">
              <div class="label-space label-title">
                [01] Kỳ tính thuế:
                <strong class="highlight-text">
                  Năm {{ dependenDeclarationInfor.PeriodYear }}</strong
                >
              </div>
              <div class="label" style="padding-left: 14px">
                <strong class="highlight-text">
                  Kê khai lần
                  {{ dependenDeclarationInfor.NumberOfDeclaration }}</strong
                >
              </div>
            </div>
            <div class="header-title bold">
              <h3 class="main-title">THÔNG TIN ĐƠN VỊ KÊ KHAI</h3>
              <el-button type="info" @click="hidenButton" id="btn-hide-display"
                >{{ CollapseOrExpand
                }}<i style="margin-left: 8px" :class="iconCollapse"></i
              ></el-button>
            </div>
          </div>
        </section>
        <!-- Phần thông tin chi tiết -->
        <!-- cột bên trái -->
        <section class="upper-content">
          <div class="head-info" v-if="isHidenInfo">
            <el-row :gutter="24">
              <!-- phần bên trái -->
              <el-col :lg="10" :md="10" :sm="10">
                <!-- Thông tin người nộp thuế -->
                <el-row>
                  <el-col :span="12" class="text-left contract-tax pb-4">
                    <div style="font-weight: bold">Người nộp thuế</div>
                    <div class="tab-active" />
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">[04] Tên người nộp thuế:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.OrganizationName }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">[05] Mã số thuế:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.OrganizationTaxNo }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">[06] Địa chỉ:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxPlaceName }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">[09] Điện thoại liên hệ:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.Phone }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">[10] Email:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.Email }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">[48] Cơ quan thuế cấp cục:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.CAUnitName }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">[49] Cơ quan thuế quản lý:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.CAUnitManage }}
                    </div>
                  </el-col>
                </el-row>
                <el-row>
                  <el-col
                    :span="12"
                    class="text-left contract-tax header-inblock pb-4"
                  >
                    <div style="font-weight: bold">Nhân viên đại lý thuế</div>
                    <div class="tab-active" />
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">Họ và tên:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxAgentEmployee }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="8">
                    <div class="label-title">Chứng chỉ hành nghề số:</div>
                  </el-col>
                  <el-col :span="16">
                    <div class="label-content">
                      {{
                        dependenDeclarationInfor.TaxAgentPracticingCertifacateNo
                      }}
                    </div>
                  </el-col>
                </el-row>
              </el-col>
              <el-col :lg="14" :md="14" :sm="14">
                <!-- Thông tin đại lý thuế -->
                <el-row>
                  <el-col :span="12" class="text-left contract-tax pb-4">
                    <div style="font-weight: bold">Đại lý thuế</div>
                    <div class="tab-active" />
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">[12] Tên đại lý thuế:</div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxAgentPlaceName }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">[13] Mã số thuế:</div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxAgentFaxNo }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">[6] Địa chỉ:</div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxAgentPlaceName }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">[09] Điện thoại liên hệ:</div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxAgentPhone }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">[10] Email:</div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxAgentEmail }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">
                      [20] Hợp đồng với đại lý thuế:
                    </div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxAgentContractNo }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">Ngày:</div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.TaxDate }}
                    </div>
                  </el-col>
                </el-row>
                <el-row>
                  <el-col
                    :span="12"
                    class="text-left contract-tax header-inblock pb-4"
                  >
                    <div style="font-weight: bold">Thông tin ký</div>
                    <div class="tab-active" />
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">Họ và tên người ký:</div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.SignBy }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="24" class="info-row">
                  <el-col :span="6">
                    <div class="label-title">Ngày ký:</div>
                  </el-col>
                  <el-col :span="18">
                    <div class="label-content">
                      {{ dependenDeclarationInfor.SignDate }}
                    </div>
                  </el-col>
                </el-row>
              </el-col>
            </el-row>
          </div>
        </section>
        <!-- Phần biểu bảng -->
        <div
          class="table-declaration-dependent-data"
          v-if="isShowTaxNoAlreadyTable || isShowTaxNoYetTable"
        >
          <div
            class="table-dependent table-register"
            v-if="isShowTaxNoYetTable"
          >
            <div class="title-table">
              ĐĂNG KÝ CẤP MÃ SỐ THUẾ CHO NGƯỜI PHỤ THUỘC
            </div>
            <tbl
              :typeTable="'tableData'"
              :listColumn="listColumn"
              :dataTable="dependenceTaxNoYet"
              :summary="false"
              :loading="registerTableLoading"
              :tooltip="true"
              :havePagination="true"
              :haveAction="false"
              :haveAdjust="false"
              :something="false"
              :displayIndexRow="true"
              :total="dependenDeclarationInfor.TotalTaxNoYet"
              :pageSizes="pageSizes"
              @pagination="getFilterTaxNoYetTable"
            >
            </tbl>
          </div>
          <div
            class="table-dependent table-changes"
            v-if="isShowTaxNoAlreadyTable"
          >
            <div class="title-table">ĐĂNG KÝ THAY ĐỔI VỀ NGƯỜI PHỤ THUỘC</div>
            <tbl
              :typeTable="'tableData'"
              :listColumn="listColumn"
              :dataTable="dependenceTaxNoAlready"
              :summary="false"
              :loading="changesTableLoading"
              :tooltip="true"
              :havePagination="true"
              :haveAction="false"
              :haveAdjust="false"
              :something="false"
              :displayIndexRow="true"
              :total="dependenDeclarationInfor.TotalTaxNoAlready"
              :pageSizes="pageSizes"
              @pagination="getFilterTaxNoAlreadyTable"
            >
            </tbl>
          </div>
        </div>
      </template>
      <template v-slot:footer>
        <m-btn
          name="Ký nộp qua mTax"
          type="btn-primary-icon"
          style="padding-left: 10px"
          v-if="isSign"
          :handleClick="registerByMTAX"
        ></m-btn>
        <m-btn name="Hủy" type="btn-grey" :handleClick="getClosing"></m-btn>
      </template>
    </dialogel>
  </div>
</template>

<script>
import dialogel from "@/components/dialog";
import Button from "@/components/buttons";
import ApiDeclaration from "@/api/etax/declaration.js";
import Message from "@/enums/signusbtoken.js";
import { SignMinTax } from "@/utils/signUsb.js";
import tbl from "@/components/tables";
import Cookies from "js-cookie";
export default {
  name: "declaration-view",
  components: {
    dialogel,
    "m-btn": Button,
    tbl,
  },

  data() {
    return {
      title: " TỜ KHAI ĐĂNG KÝ NGƯỜI PHỤ THUỘC",
      tableHeight: window.innerHeight - 500, // 500 là chiều cao của các phần tử khác
      maxlength: 255, // số ký tự tối đa được nhập vào input
      dependenDeclarationInfor: Object,
      listDeclarationDetailDependents: Object,
      dependenceTaxNoAlready: [],
      dependenceTaxNoYet: [],
      isShowTaxNoAlreadyTable: false,
      isShowTaxNoYetTable: false,
      registerTableLoading: true,
      changesTableLoading: true,
      TaxCode: "0101243150999",
      DataToSign: {},
      CollapseOrExpand: "Thu gọn",
      iconCollapse: "el-icon-arrow-up",
      isHidenInfo: true,
      listColumn: [
        {
          label: "Họ và tên người nộp thuế (NNT)",
          propertyName: "EmployeeFullName",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "EmployeeFullName",
          sortable: false,
        },
        {
          label: "Mã số thuế người nộp thuế (NNT)",
          propertyName: "EmployeeTaxNo",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "EmployeeTaxNo",
          sortable: false,
        },
        {
          label: "Họ và tên người phụ thuộc (NPT)",
          propertyName: "DependentName",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "DependentName",
          sortable: false,
        },
        {
          label: "Ngày sinh người phụ thuộc (NPT)",
          propertyName: "DateOfBirth",
          align: "center",
          width: "",
          minWidth: "140",
          type: "date",
          prop: "DateOfBirth",
          sortable: false,
        },
        {
          label: "Mã số thuế người phụ thuộc (NPT)",
          propertyName: "DependentTaxNo",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "DependentTaxNo",
          sortable: false,
        },
        {
          label: "Quốc tịch người phụ thuộc (NPT)",
          propertyName: "NationalityName",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "NationalityName",
          sortable: false,
        },
        {
          label: "CMND/CCCD/Hộ chiếu/ Số định danh cá nhân của NPT",
          propertyName: "IndentityTypeName",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "IndentityTypeName",
          sortable: false,
        },
        {
          label: "Quan hệ với người nộp thuế",
          propertyName: "RelationName",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "RelationName",
          sortable: false,
        },
        {
          label: `Thông tin trên giấy khai sinh của người phụ thuộc
                        (Nếu người phụ thuộc không có MST, CMND và Hộ chiếu)`,
          align: "center",
          width: "",
          minWidth: "140",
          type: "",
          sortable: false,
          group: [
            {
              label: "Số",
              propertyName: "BirthCertificateNumber",
              align: "left",
              width: "",
              minWidth: "140",
              type: "",
              prop: "BirthCertificateNumber",
              sortable: false,
            },
            {
              label: "Quyển số",
              propertyName: "BirthCertificateBook",
              align: "left",
              width: "",
              minWidth: "140",
              type: "",
              prop: "BirthCertificateBook",
              sortable: false,
            },
            {
              label: "Nơi đăng ký",
              align: "center",
              width: "",
              minWidth: "50",
              type: "",
              sortable: false,
              group: [
                {
                  label: "Quốc gia",
                  propertyName: "BirthCertificateNationalityName",
                  align: "left",
                  width: "",
                  minWidth: "140",
                  type: "",
                  prop: "BirthCertificateNationalityName",
                  sortable: false,
                },
                {
                  label: "Tỉnh/ Thành phố",
                  propertyName: "BirthCertificateProvinceName",
                  align: "left",
                  width: "",
                  minWidth: "140",
                  type: "",
                  prop: "BirthCertificateProvinceName",
                  sortable: false,
                },
                {
                  label: "Quận/ Huyện",
                  propertyName: "BirthCertificateDistrictName",
                  align: "left",
                  width: "",
                  minWidth: "140",
                  type: "",
                  prop: "BirthCertificateDistrictName",
                  sortable: false,
                },
                {
                  label: "Phường/ Xã",
                  propertyName: "BirthCertificateWardName",
                  align: "left",
                  width: "",
                  minWidth: "140",
                  type: "",
                  prop: "BirthCertificateWardName",
                  sortable: false,
                },
              ],
            },
          ],
        },
        {
          label: "Thời gian tính giảm trừ",
          align: "center",
          width: "",
          minWidth: "140",
          type: "",
          sortable: false,
          group: [
            {
              label: "Từ tháng",
              propertyName: "StartDependentDate",
              align: "center",
              width: "",
              minWidth: "140",
              type: "month",
              prop: "StartDependentDate",
              sortable: false,
            },
            {
              label: "Đến tháng",
              propertyName: "FinishDependentDate",
              align: "center",
              width: "",
              minWidth: "140",
              type: "month",
              prop: "FinishDependentDate",
              sortable: false,
            },
          ],
        },
      ],
      pageSizes: [25, 50, 75, 100],
      filterFirstTime: {
        pageIndex: 1,
        pageSize: 25,
        isGetDeclaration: true, //Mở dialog lần đầu nên lấy thông tin về tờ khai
        hasTaxNo: 2, //Lấy danh sách cả các người phụ thuộc có và chưa có mã số thuế
      },
      filterTaxNoYetTable: {
        pageIndex: 1,
        pageSize: 25,
        isGetDeclaration: false, //Paging trong table không cần lấy thông tin về tờ khai nữa
        hasTaxNo: 0, //Lấy danh sách người phụ thuộc chưa có mã số thuế
      },
      filterTaxNoAlreadyTable: {
        pageIndex: 1,
        pageSize: 25,
        isGetDeclaration: false, //Paging trong table không cần lấy thông tin về tờ khai nữa
        hasTaxNo: 1, //Lấy danh sách người phụ thuộc đã có mã số thuế
      },
    };
  },
  props: {
    viewDeclarationFlag: {
      type: Boolean,
      default: false,
    },
    declarationID: {
      type: String,
      default: null,
    },
    isSign: {
      type: Boolean,
      default: false,
    },
    isExpired: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    async getFilterTaxNoYetTable(val) {
      this.registerTableLoading = true;
      this.filterTaxNoYetTable.pageSize = val.limit;
      this.filterTaxNoYetTable.pageIndex = val.page;
      await this.getDependentDeclaration(this.filterTaxNoYetTable);
    },
    async getFilterTaxNoAlreadyTable(val) {
      this.changesTableLoading = true;
      this.filterTaxNoAlreadyTable.pageSize = val.limit;
      this.filterTaxNoAlreadyTable.pageIndex = val.page;
      await this.getDependentDeclaration(this.filterTaxNoAlreadyTable);
    },
    /**
     * Hàm Ký nộp tờ khai thuế qua MTAX
     * Created by: CVCuong created date: 20/8/2020
     */
    registerByMTAX() {
      if (this.isExpired == true) {
        //Show thông báo đã hết hạn sử dụng MTax
        this.$_Popup.warn("Cảnh báo", Message.findMessage(404), [
          {
            text: "Đóng",
            class: "btn-primary",
            callback: this.closeLoading,
          },
        ]);
      } else {
        let arrSign = [];
        arrSign.push(this.DataToSign.DeclarationID);
        const validate = SignMinTax(this.TaxCode, arrSign);
        if (validate === true) {
          this.$_Popup.warn("Cảnh báo", Message.findMessage(402), [
            {
              text: "Đóng",
              class: "btn-primary",
              callback: () => {},
            },
          ]);
        } else if (validate === false) {
          this.$_Popup.warn("Cảnh báo", Message.findMessage(403), [
            {
              text: "Đóng",
              class: "btn-primary",
              callback: () => {},
            },
          ]);
        }
      }
    },
    getClosing() {
      this.dependenceTaxNoAlready = [];
      this.dependenceTaxNoYet = [];
      this.$emit("view-get-closing", false);
    },
    /**
     * Lấy dữ liệu cho tờ khai đăng ký người phụ thuộc
     * Create by : Nguyễn Tấn Đông 30/09/2020
     */
    async getDependentDeclaration(filter) {
      var res = await ApiDeclaration.getViewDeclarationPaging(
        this.declarationID ? this.declarationID : this.$route.params.id,
        filter
      );
      if (typeof res.data != "undefined" && res.data.Success) {
        this.DataToSign = JSON.parse(JSON.stringify(res.data.Data));
        if (filter.isGetDeclaration) {
          this.dependenDeclarationInfor = JSON.parse(
            JSON.stringify(res.data.Data)
          );
          this.listDeclarationDetailDependents = JSON.parse(
            JSON.stringify(res.data.Data.DeclarationDetailDependents)
          );
          this.devideDependenceIntoDifferenceTable();
        } else if (filter.hasTaxNo == 0) {
          this.dependenceTaxNoYet = JSON.parse(
            JSON.stringify(res.data.Data.DeclarationDetailDependents)
          );
          this.registerTableLoading = false;
        } else if (filter.hasTaxNo == 1) {
          this.dependenceTaxNoAlready = JSON.parse(
            JSON.stringify(res.data.Data.DeclarationDetailDependents)
          );
          this.changesTableLoading = false;
        }
      }
    },
    /**
     *
     */
    devideDependenceIntoDifferenceTable() {
      this.listDeclarationDetailDependents.forEach((item) => {
        if (item.DeclarationDetailDependents !== null) {
          if (item.DependentTaxNo != "") {
            this.dependenceTaxNoAlready.push(item);
          } else {
            this.dependenceTaxNoYet.push(item);
          }
        }
        this.dependenceTaxNoYet.length > 0
          ? (this.isShowTaxNoYetTable = true)
          : (this.isShowTaxNoYetTable = false);
        this.dependenceTaxNoAlready.length > 0
          ? (this.isShowTaxNoAlreadyTable = true)
          : (this.isShowTaxNoAlreadyTable = false);
        this.registerTableLoading = false;
        this.changesTableLoading = false;
      });
    },
    hidenButton() {
      if (Cookies.get("isHidenInfo") != null) Cookies.remove("isHidenInfo");
      Cookies.set("isHidenInfo", !this.isHidenInfo);

      this.isHidenInfo = Boolean(Cookies.get("isHidenInfo") == "false" ? 0 : 1);
    },
    setHeightTable() {
      if (this.dependenceTaxNoYet.length > 10) {
        document.querySelector(
          ".table-register .el-table__body-wrapper"
        ).style.height = `calc(100vh - 480px)`;
      } else if (this.isShowTaxNoYetTable) {
        document.querySelector(
          ".table-register .el-table__body-wrapper"
        ).style.height = `calc(100%)`;
      }
      if (this.dependenceTaxNoAlready.length > 10) {
        document.querySelector(
          ".table-changes .el-table__body-wrapper"
        ).style.height = `calc(100vh - 480px)`;
      } else if (this.isShowTaxNoAlreadyTable) {
        document.querySelector(
          ".table-changes .el-table__body-wrapper"
        ).style.height = `calc(100%)`;
      }
    },
  },

  watch: {
    async viewDeclarationFlag(val) {
      if (val) {
        await this.getDependentDeclaration(this.filterFirstTime);
      }
      this.setHeightTable();
    },
    isHidenInfo(val) {
      if (!val) {
        this.iconCollapse = "el-icon-arrow-down";
        this.CollapseOrExpand = "Chi tiết";
        if (this.isShowTaxNoAlreadyTable && !this.isShowTaxNoYetTable) {
          document.querySelector(
            ".table-changes .el-table__body-wrapper"
          ).style.height = `calc(100vh - 459px)`;
        } else if (!this.isShowTaxNoAlreadyTable && this.isShowTaxNoYetTable) {
          document.querySelector(
            ".table-register .el-table__body-wrapper"
          ).style.height = `calc(100vh - 459px)`;
        }
      } else {
        this.iconCollapse = "el-icon-arrow-up";
        this.CollapseOrExpand = "Thu gọn";
        this.setHeightTable();
      }
    },
  },

  // async created() {
  //   await this.getDependentDeclaration(this.filterFirstTime);
  // },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
.dependent-register-declaration-dialog {
  height: 100%;
  border-radius: 5px !important;
}

#btn-hide-display {
  margin-left: 24px;
  background: none;
  color: $light-primary;
  border: none;

  &:hover {
    background-color: $background-grey;
  }

  span {
    font-family: $_fontbold;
  }
}

.el-table .cell {
  word-break: break-word;
  white-space: unset;
}

.view-declaration {
  #dependent-register-declaration-dialog {
    border-radius: 5px;
    width: calc(100% - 48px);
    height: calc(100% - 25px);
    margin: 25px 24px 0px 24px;
    .el-dialog__body {
      height: calc(100% - 120px);
      padding: 0px 24px;
    }
    .upper-content-sticky {
      position: sticky;
      top: 0;
      background-color: #ffffff;
      z-index: 3000;
    }
  }
  .detail {
    display: flex;
  }
  .label-space {
    color: $medium-dark-grey;
    padding-right: 14px;
    border-right: 1px solid $bg-grey;
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

  .highlight-text {
    font-family: $_fontbold;
    color: $hover-primary;
  }

  .el-dialog__header {
    padding-bottom: 10px;
  }
  .header-title {
    display: flex;
    margin-top: 24px;
    margin-bottom: 16px;
    height: 32px;
    align-items: center;

    .main-title {
      font-family: $_fontbold;
      font-size: 15px;
    }
  }
  .bold {
    font-weight: bold;
    font-size: 15px;
    margin-top: 15px;
  }
  .upper-content {
    .head-info {
      .tab-active {
        margin-top: 5px;
        width: 72px;
        bottom: 0px;
        height: 1px;
        background-color: $seconds;
        position: unset;
      }
    }
  }
  .table-declaration-dependent-data {
    padding: 16px 16px 0px 16px;
    background-color: #e9ebee;
    margin-top: 14px;
    .table-dependent {
      padding-bottom: 16px;
      .pagination-container {
        background-color: #e9ebee;
      }
      .title-table {
        background-color: #243585;
        color: #ffffff;
        height: 36px;
        border-left: 4px solid #ee0000;
        display: flex;
        align-items: center;
        padding-left: 15px;
      }
    }
  }

  .info-row {
    margin-bottom: 15px;
    margin-top: 15px;
  }

  .header-inblock {
    margin-top: 20px;
  }

  .taxno-already {
    margin-top: 5px;
    border-bottom: none;
  }

  .gutter {
    display: none;
  }
}
</style>