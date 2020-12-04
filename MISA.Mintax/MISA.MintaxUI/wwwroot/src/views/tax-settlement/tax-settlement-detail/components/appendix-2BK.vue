<template>
  <div class="appendix-2bk">
    <div class="title">
      BẢNG KÊ THU NHẬP CHỊU THUẾ VÀ THUẾ THU NHẬP CÁ NHÂN ĐỐI VỚI THU NHẬP TỪ
      TIỀN LƯƠNG, TIỀN CÔNG CỦA CÁ NHÂN THUỘC DIỆN TÍNH THUẾ SUẤT TOÀN PHẦN
    </div>
    <div class="filter-2bk">
      <el-row
        class="filter-bar"
        justify="space-between"
        v-if="DeclarationStatus == 1"
      >
        <el-col :span="14" v-if="!isEmpty">
          <el-col :span="14">
            <field-input
              type="input-icon"
              classicon="icon-search"
              :placeholder="'Tìm kiếm họ và tên, mã nhân viên, mã số thuế ...'"
              @value="filter.keyword = $event"
              :valueInput="filter.keyword"
              :clearable="true"
            >
            </field-input>
          </el-col>
        </el-col>
        <el-col :span="14" v-if="isEmpty" style="height: 40px"></el-col>
        <el-col :span="10" class="left-header">
          <span class="currency-unit mr-10">Đơn vị tiền tệ: VNĐ</span>
          <m-btn
            type="btn-white-icon"
            name="Nhập file excel"
            :handleClick="openImportPayerExcelDialog"
          >
            <template v-slot:icon>
              <img class="icon" src="@/icons/Component 98 – 22.png" />
            </template>
          </m-btn>
          <m-btn
            style="padding-left: 10px"
            type="btn-primary-icon"
            name="Thêm người nộp thuế"
            :handleClick="openDialog"
          >
            <template v-slot:icon>
              <img class="icon" src="@/icons/Group 17812.png" />
            </template>
          </m-btn>
        </el-col>
      </el-row>
    </div>
    <import-payer-excel
      :visibleDL="isImportPayerExcelDialog"
      @closeDialog="isImportPayerExcelDialog = $event"
      @fetchData="isFetch = $event"
    ></import-payer-excel>
    <div v-if="!isEmpty" class="tbl-2bk">
      <div
        v-if="loadingFirst && isFirst"
        v-loading="loadingFirst"
        class="loading-first"
      ></div>
      <div
        v-if="loadingAfter && !isFirst"
        v-loading="loadingAfter"
        class="loading-after"
      ></div>
      <tbl
        :typeTable="'tableData'"
        :style="setStyle()"
        :listColumn="listColumn"
        :dataTable="dataTable"
        :haveAction="true"
        :loading="false"
        :total="numberOfRecord"
        :summary="true"
        :positionSummary="positionSummary"
        :pageSizes="pageSizes"
        :displayIndexRow="true"
        @pagination="getFilter"
        typepage="người nộp thuế"
        :listIndex="listIndex"
        :isAdjustNumber="true"
        @row-action="handleClickTool"
      />
      <div
        class="temporary-mode-btn"
        v-if="numberOfRecord == 0 && !isEmpty && !loadingFirst && !loadingAfter"
      >
        <img
          src="@/assets/images/Group 18279.png"
          alt="not have data"
          style="position: relative"
        />
        <span>Không tìm thấy kết quả phù hợp</span>
      </div>
    </div>
    <!-- IF Data Table rỗng -->
    <div class="empty-content" v-if="isEmpty && !loadingFirst && !loadingFirst">
      <img src="@/assets/images/Group 18279.png" />
      <p>Không có thông tin người phụ thuộc.</p>
    </div>

    <add-dialog
      :visiableDL="isDialogOpen"
      @closeDialog="isDialogOpen = $event"
      :formMode="formMode"
      @success="reload = $event"
      :title="titledialog"
      :EmployeeSelected="EmployeeSelected"
    />
  </div>
</template>
<script>
import fieldinput from "@/components/inputs";
import btn from "@/components/buttons";
import tbl from "@/components/tables";
import declarationService from "@/api/etax/declaration.js";
import importPayerExcel from "./../tax-settlement-progressive/new-tax-payer-excel";
import addDialog from "./components/add-employee-dialog.vue";
import actionEnum from "@/enums/actionTable.js";
import declarationSettlementDetailService from "@/api/etax/declaration_settlement_detail.js";
import declarationSettlementDetailEnum from "@/enums/declaration-settlement.js";
export default {
  components: {
    "m-btn": btn,
    "field-input": fieldinput,
    tbl,
    "import-payer-excel": importPayerExcel,
    "add-dialog": addDialog,
  },
  props: {
    DeclarationStatus: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
      titledialog: "THÊM MỚI NGƯỜI NỘP THUẾ",
      listID: [],
      EmployeeSelected: {},
      formMode: declarationSettlementDetailEnum.FormMode.Add,
      reload: false,
      isDialogOpen: false,
      listIndex: [
        {
          valueIndex: "",
          width: "50",
        },
        {
          valueIndex: "[06]",
          width: "50",
        },
        {
          valueIndex: "[07]",
          width: "160",
        },
        {
          valueIndex: "[08]",
          width: "120",
        },
        {
          valueIndex: "[09]",
          width: "140",
        },

        {
          valueIndex: "[10]",
          width: "70",
        },

        {
          valueIndex: "[11]",
          width: "160",
        },
        {
          valueIndex: "[12]",
          width: "280",
        },
        {
          valueIndex: "[13]",
          width: "140",
        },
        {
          valueIndex: "[14]",
          width: "120",
        },
        {
          valueIndex: "[15]",
          width: "160",
        },
        {
          valueIndex: "[16]",
          width: "280",
        },
        {
          valueIndex: "[17]",
          width: "140",
        },
        {
          valueIndex: "",
          width: "120",
        },
      ],
      pageSizes: [25, 50, 75, 100],
      numberOfRecord: 0,
      loadingFirst: true,
      loadingAfter: true,
      isFirst: true,
      listColumn: [
        {
          label: "Họ và tên",
          prop: "FullName",
          propertyName: "FullName",
          align: "left",
          sortable: false,
          width: "",
          minWidth: "160",
          type: "",
        },
        {
          label: "Mã số thuế",
          prop: "TaxNo",
          propertyName: "TaxNo",
          align: "left",
          sortable: false,
          width: "",
          minWidth: "120",
          type: "",
        },
        {
          label: "Số CMND/ Hộ chiếu",
          prop: "CitizenIdentityNo",
          propertyName: "CitizenIdentityNo",
          align: "left",
          sortable: false,
          width: "",
          minWidth: "140",
          type: "",
        },
        {
          label: "Cá nhân không cư trú",
          prop: "IndividualResident",
          propertyName: "IndividualResident",
          align: "left",
          sortable: false,
          width: "",
          minWidth: "70",
          type: "",
        },
        {
          label: "Thu nhập chịu thuế",
          align: "center",
          sortable: false,
          width: "",
          minWidth: "140",
          type: "",
          group: [
            {
              label: "Tổng số",
              prop: "TotalIncomeTaxable",
              propertyName: "TotalIncomeTaxable",
              align: "right",
              sortable: false,
              width: "",
              minWidth: "160",
              type: "decimal",
            },
            {
              label:
                "TNCN từ phí mua bảo hiểm nhân thọ, BH không bắt buộc khác của DNBH không thành lập tại Việt Nam cho người lao động",
              prop: "IncomeTaxPurchasing",
              propertyName: "IncomeTaxPurchasing",
              align: "right",
              sortable: false,
              width: "",
              minWidth: "280",
              type: "decimal",
            },
            {
              label: "Trong đó TNCT được giảm thuế",
              align: "center",
              sortable: false,
              width: "",
              minWidth: "140",
              type: "",
              group: [
                {
                  label: "Làm việc trong KKT",
                  prop: "KKTReduction",
                  propertyName: "KKTReduction",
                  align: "right",
                  sortable: false,
                  width: "",
                  minWidth: "140",
                  type: "decimal",
                },
                {
                  label: "Theo hiệp định",
                  prop: "KKTReduction",
                  propertyName: "KKTReduction",
                  align: "right",
                  sortable: false,
                  width: "",
                  minWidth: "120",
                  type: "decimal",
                },
              ],
            },
          ],
        },
        {
          label: "Số thuế thu nhập cá nhân (TNCN) đã khấu trừ",
          align: "center",
          sortable: false,
          width: "",
          minWidth: "140",
          type: "",
          group: [
            {
              label: "Tổng số",
              prop: "TotalTaxWithHeld",
              propertyName: "TotalTaxWithHeld",
              align: "right",
              sortable: false,
              width: "",
              minWidth: "160",
              type: "decimal",
            },
            {
              label:
                "TNCN từ phí mua bảo hiểm nhân thọ, BH không bắt buộc khác của DNBH không thành lập tại Việt Nam cho người lao động",
              prop: "IncomeTaxPurchasing",
              propertyName: "IncomeTaxPurchasing",
              align: "right",
              sortable: false,
              width: "",
              minWidth: "280",
              type: "decimal",
            },
          ],
        },
        {
          label: "Số thuế TNCN được giảm do làm việc tại  KKT",
          prop: "KKTReduction",
          propertyName: "KKTReduction",
          align: "right",
          sortable: false,
          width: "",
          minWidth: "140",
          type: "decimal",
        },
      ],
      filter: {
        keyword: "",
        SettlementID: "",
        DeclarationSettlementDetailType: 1,
        pageIndex: 1,
        pageSize: 25,
      },
      dataTable: [],
      positionSummary: {
        Summary: [
          {
            PropertyName: "",
            PropertyIndex: 0,
          },
          {
            PropertyName: "TotalIncomeTaxable",
            PropertyIndex: 6,
          },
          {
            PropertyName: "IncomeTaxPurchasing",
            PropertyIndex: 7,
          },
          {
            PropertyName: "KKTReduction",
            PropertyIndex: 8,
          },
          {
            PropertyName: "KKTReduction",
            PropertyIndex: 9,
          },
          {
            PropertyName: "TotalTaxWithHeld",
            PropertyIndex: 10,
          },
          {
            PropertyName: "IncomeTaxPurchasing",
            PropertyIndex: 11,
          },
          {
            PropertyName: "KKTReduction",
            PropertyIndex: 12,
          },
        ],
      },
      isEmpty: false,
      isImportPayerExcelDialog: false,
      isFetch: false,
    };
  },

  computed: {},
  watch: {
    filter: {
      handler() {
        this.getData();
      },
      deep: true,
    },
    reload: {
      async handler(val) {
        if (val) {
          await this.getData();
          this.$emit("reload", true);
          this.reload = false;
        }
      },
    },
    isFetch: function handleFetch() {
      if (this.isFetch === true) {
        this.$emit("reload", true);
        this.isFetch = false;
        this.getData();
      }
    },
  },
  methods: {
    openDialog() {
      this.formMode = declarationSettlementDetailEnum.FormMode.Add;
      this.titledialog = "THÊM MỚI NGƯỜI NỘP THUẾ";
      this.isDialogOpen = true;
    },
    setStyle() {
      return { height: "100%" };
    },
    /**
     * Lấy dữ liệu cho filter
     * created by nqhiep 28.10.2020
     */
    getFilter(val) {
      this.filter.pageSize = val.limit;
      this.filter.pageIndex = val.page;
    },
    /**
     * Lấy dữ liệu cho phụ lục 05-2BK
     * created by nqhiep 28.10.2020
     */
    async getData() {
      this.loadingFirst = true;
      this.loadingAfter = true;
      var res = await declarationService.getDeclarationSettlementDetail(
        this.filter
      );
      this.dataTable = [];
      if (res.data && res.data.Success) {
        res.data.Data.DeclarationSettlementDetails.forEach((element) => {
          element.IndividualResident =
            element.IndividualResident === 1 ? "Có" : "Không";
        });
        this.dataTable = [...res.data.Data.DeclarationSettlementDetails];
        this.isEmpty = res.data.Data.IsEmpty;
        this.numberOfRecord = res.data.Data.TotalDeclarationSettlementDetail;
      } else {
        this.isEmpty = true;
      }
      this.loadingFirst = false;
      this.loadingAfter = false;
      this.isFirst = false;
    },

    /**
     * Mở dialog Import file
     * Created By : NQKHAI(29/10/2020)
     */
    openImportPayerExcelDialog() {
      this.isFetch = false;
      this.isImportPayerExcelDialog = true;
    },

    /**
     * Hàm xử lý 2 nút sửa xóa
     * CreatedBy: NVANH 5.11.2020
     */
    handleClickTool(val) {
      if (val.value == actionEnum.StatusTableExcel.Edit) {
        this.formMode = declarationSettlementDetailEnum.FormMode.Edit;
        this.titledialog = "CHI TIẾT NGƯỜI NỘP THUẾ";
        this.EmployeeSelected = Object.assign({}, val.row);
        this.isDialogOpen = true;
      }
      if (val.value == actionEnum.StatusTableExcel.Del) {
        this.listID = [];
        this.listID.push(val.row.DeclarationSettlementDetailID);
        var buttons = [
          {
            text: "Có",
            class: "btn-delete",
            callback: this.deleteDeclarationSettlementDetailByID,
          },
          {
            text: "Không",
            class: "btn-grey",
            callback: () => {},
          },
        ];
        this.$_Popup.delete(
          "Xóa người nộp thuế",
          `Ban có chắc chắn muốn xóa <b>${val.row.FullName} không?</b>`,
          buttons
        );
      }
    },

    async deleteDeclarationSettlementDetailByID() {
      var res = await declarationSettlementDetailService.deleteDeclarationSettlementDetailByID(
        this.listID
      );
      if (res.data.Success && res.data.MISACode == 200) {
        this.$_Notification.success(
          "Thông báo",
          `Xóa người nộp thuế thành công`
        );
        this.$emit("reload", true);
        this.getData();
      } else
        this.$_Notification.warning("Thông báo", `Xóa người nộp thuế thất bại`);
    },
  },

  async created() {
    this.filter.SettlementID = this.$route.params.id;
  },
};
</script>
<style lang="scss" >
@import "@/styles/variables.scss";

.contain-tab > div:nth-child(1) {
  height: 100%;
}

.appendix-2bk {
  height: calc(100vh - 214px);
  padding-top: 24px;

  .temporary-mode-btn {
    position: absolute;
    top: 60%;
    left: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    transform: translate(-50%, -50%);

    img {
      margin-bottom: 8px;
    }
  }

  .empty-content {
    height: 208px;
    display: flex;
    align-items: center;
    flex-direction: column;
    position: absolute;
    top: calc(50% - 104px);
    left: calc(50% - 107px);

    img {
      width: 177px;
    }

    p {
      margin: 14px 0 0 0;
    }
  }
  .title {
    font-family: $_fontbold;
    font-size: 20px;
    margin-bottom: 16px;
  }

  .filter-2bk {
    height: 40px;

    .btn-white-icon {
      border-color: $white;
      color: $hover-primary !important;

      span {
        font-family: $_fontmedium !important;
      }
    }

    .currency-unit {
      color: $medium-dark-grey;
    }
  }

  .tbl-2bk {
    margin-top: 16px;
    height: calc(100% - 97px);
    position: relative;

    .el-table__body-wrapper {
      height: calc(100% - 192px);
    }

    .loading-first.el-loading-parent--relative {
      position: absolute !important;
      top: 0;
      left: 0;
      right: 0;
      z-index: 999999;
      bottom: 0;

      .el-loading-mask {
        opacity: 1;
      }
    }

    .loading-after.el-loading-parent--relative {
      position: absolute !important;
      top: 144px;
      left: 0;
      right: 0;
      z-index: 999999;
      bottom: 60px;

      .el-loading-mask {
        background-color: $white !important;
        opacity: 0.8;
      }
    }
  }

  .emptyTable {
    margin-top: 16px;
    height: calc(100% - 97px);
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    p {
      margin: 14px 0 0 0;
    }
  }

  .tableData {
    .cell {
      white-space: normal;
      word-break: break-word;
    }

    .el-table__body-wrapper {
      height: calc(100% - 192px);
    }
  }

  .left-header {
    display: flex;
    justify-content: flex-end;
    align-items: center;
  }

  .not-found {
    position: absolute;
    top: 62%;
    left: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    transform: translate(-50%, -50%);
  }
}

@media only screen and (max-width: 1366px) {
  .appendix-2bk {
    height: calc(100vh - 84px);

    .tbl-2bk {
      margin-top: 16px;
      height: calc(100% - 122px);
    }

    .temporary-mode-btn {
      top: 65%;

      img {
        display: none;
      }
    }

    .emptyTable {
      margin-top: 16px;
      height: calc(100% - 122px);
    }
  }
}
</style>