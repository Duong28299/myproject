<template>
  <div id="appendix-total">
    <el-row>
      <el-col :span="24">
        <h2 class="title-appendix-total">
          BẢNG TỔNG HỢP QUYẾT TOÁN THUẾ THU NHẬP CÁ NHÂN
        </h2>
      </el-col>
    </el-row>
    <div>
      <el-row>
        <el-col :span="15">
          <span class="tax-deduction"
            >I. Nghĩa vụ khấu trừ thuế của tổ chức, cá nhân trả thu nhập
          </span>
        </el-col>
        <el-col :span="9" class="currency-unit">
          <p style="margin-top: 0px">Đơn vị tiền tệ: VNĐ</p>
        </el-col>
      </el-row>
    </div>
    <div v-if="loading" v-loading="loading"></div>
    <div class="total-table-tax-deduction">
      <tbl
        :typeTable="'tableData'"
        :style="setStyle()"
        :listColumn="listColumn"
        :dataTable="dataTable"
        :summary="false"
        :havePagination="false"
        :loading="false"
        :tooltip="true"
        :haveAction="false"
        :haveAdjust="false"
        :something="false"
        :autoDisplayOrdinalNumber="false"
      />
    </div>
    <el-row style="display: block; margin-top: 22px; margin-bottom: 16px">
      <el-col :span="15">
        <span class="settlement-instead"
          >II. Nghĩa vụ quyết toán thay cho cá nhân</span
        >
      </el-col>
    </el-row>
    <div class="total-table-settlement-instead">
      <tbl
        :typeTable="'tableData'"
        :style="setStyle1()"
        :listColumn="listColumn1"
        :dataTable="dataTable1"
        :summary="false"
        :havePagination="false"
        :loading="false"
        :tooltip="true"
        :haveAction="false"
        :haveAdjust="false"
        :something="false"
        :autoDisplayOrdinalNumber="false"
      />
    </div>
  </div>
</template>

<script>
import tbl from "@/components/tables";
import { formatMoney } from "@/utils/index.js";
export default {
  name: "appendix-total",
  components: {
    tbl,
  },
  props: {
    DeclarationSettlementDetail: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      loading: true,
      isReload: false,
      listColumn: [
        {
          label: "STT",
          propertyName: "OrdinalNumber",
          align: "center",
          width: "",
          minWidth: "47",
          type: "",
          prop: "OrdinalNumber",
          sortable: false,
        },
        {
          label: "Chỉ tiêu",
          propertyName: "Targets",
          align: "left",
          width: "",
          minWidth: "628",
          type: "",
          prop: "Targets",
          sortable: false,
        },
        {
          label: "Mã chỉ tiêu",
          propertyName: "TargetsCode",
          align: "center",
          width: "",
          minWidth: "100",
          type: "",
          prop: "TargetsCode",
          sortable: false,
        },
        {
          label: "Đơn vị tính",
          propertyName: "CalculationUnit",
          align: "center",
          width: "",
          minWidth: "100",
          type: "",
          prop: "CalculationUnit",
          sortable: false,
        },
        {
          label: "Số người/Số tiền",
          propertyName: "NumberOfPeopleOrAmount",
          align: "right",
          width: "",
          minWidth: "230",
          type: "",
          prop: "NumberOfPeopleOrAmount",
          sortable: false,
        },
      ],
      listColumn1: [
        {
          label: "#",
          propertyName: "OrdinalNumber",
          align: "center",
          width: "",
          minWidth: "47",
          type: "",
          prop: "OrdinalNumber",
          sortable: false,
        },
        {
          label: "Chỉ tiêu",
          propertyName: "Targets",
          align: "left",
          width: "",
          minWidth: "628",
          type: "",
          prop: "Targets",
          sortable: false,
        },
        {
          label: "Mã chỉ tiêu",
          propertyName: "TargetsCode",
          align: "center",
          width: "",
          minWidth: "100",
          type: "",
          prop: "TargetsCode",
          sortable: false,
        },
        {
          label: "Đơn vị tính",
          propertyName: "CalculationUnit",
          align: "center",
          width: "",
          minWidth: "100",
          type: "",
          prop: "CalculationUnit",
          sortable: false,
        },
        {
          label: "Số người/Số tiền",
          propertyName: "NumberOfPeopleOrAmount",
          align: "right",
          width: "",
          minWidth: "230",
          type: "",
          prop: "NumberOfPeopleOrAmount",
          sortable: false,
        },
      ],
      dataTable: [],
      dataTable1: [],
    };
  },
  methods: {
    setStyle() {
      return { height: "1070px !important" };
    },
    setStyle1() {
      return { height: "300px !important" };
    },
    mapData() {
      this.dataTable = [
        {
          OrdinalNumber: "1",
          Targets: "Tổng số người lao động",
          TargetsCode: "[21]",
          CalculationUnit: "Người",
          NumberOfPeopleOrAmount: this.DeclarationSettlementDetail
            .TotalEmployee,
        },
        {
          OrdinalNumber: "1.1",
          Targets: "Trong đó: Cá nhân cư trú có hợp đồng lao động",
          TargetsCode: "[22]",
          CalculationUnit: "Người",
          NumberOfPeopleOrAmount: this.DeclarationSettlementDetail
            .TotalResidentWithContract,
        },
        {
          OrdinalNumber: "2",
          Targets: "Tổng số cá nhân đã khấu trừ thuế [23]=[24]+[25]",
          TargetsCode: "[23]",
          CalculationUnit: "Người",
          NumberOfPeopleOrAmount:
            this.DeclarationSettlementDetail.TotalResidentTaxWithHeld +
            this.DeclarationSettlementDetail.TotalNonResidentTaxWithHeld,
        },
        {
          OrdinalNumber: "2.1",
          Targets: "Cá nhân cư trú",
          TargetsCode: "[24]",
          CalculationUnit: "Người",
          NumberOfPeopleOrAmount: this.DeclarationSettlementDetail
            .TotalResidentTaxWithHeld,
        },
        {
          OrdinalNumber: "2.2",
          Targets: "Cá nhân không cư trú",
          TargetsCode: "[25]",
          CalculationUnit: "Người",
          NumberOfPeopleOrAmount: this.DeclarationSettlementDetail
            .TotalNonResidentTaxWithHeld,
        },
        {
          OrdinalNumber: "3",
          Targets:
            "Tổng số cá nhân thuộc diện được miễn, giảm thuế theo Hiệp định tránh đánh thuế hai lần",
          TargetsCode: "[26]",
          CalculationUnit: "Người",
          NumberOfPeopleOrAmount: this.DeclarationSettlementDetail
            .TotalLawReduction,
        },
        {
          OrdinalNumber: "4",
          Targets: "Tổng số cá nhân giảm trừ gia cảnh",
          TargetsCode: "[27]",
          CalculationUnit: "Người",
          NumberOfPeopleOrAmount: this.DeclarationSettlementDetail
            .TotalReduction,
        },
        {
          OrdinalNumber: "5",
          Targets:
            "Tổng thu nhập chịu thuế (TNCT) trả cho cá nhân [28]=[29]+[30]",
          TargetsCode: "[28]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            parseFloat(
              this.DeclarationSettlementDetail.TotalResidentIncomeTaxable
            ) +
              parseFloat(
                this.DeclarationSettlementDetail.TotalNonResidentIncomeTaxable
              ),
            ","
          ),
        },
        {
          OrdinalNumber: "5.1",
          Targets: "Cá nhân cư trú",
          TargetsCode: "[29]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            parseFloat(
              this.DeclarationSettlementDetail.TotalResidentIncomeTaxable
            ),
            ","
          ),
        },
        {
          OrdinalNumber: "5.2",
          Targets: "Cá nhân không cư trú",
          TargetsCode: "[30]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalNonResidentIncomeTaxable,
            ","
          ),
        },
        {
          OrdinalNumber: "6",
          Targets:
            "Tổng TNCT trả cho cá nhân thuộc diện phải khấu trừ thuế [31]=[32]+[33]",
          TargetsCode: "[31]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxwithHoldingResident +
              this.DeclarationSettlementDetail.TotalTaxwithHoldingNonResident,
            ","
          ),
        },
        {
          OrdinalNumber: "6.1",
          Targets: "Cá nhân cư trú",
          TargetsCode: "[32]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxwithHoldingResident,
            ","
          ),
        },
        {
          OrdinalNumber: "6.2",
          Targets: "Cá nhân không cư trú",
          TargetsCode: "[33]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxwithHoldingNonResident,
            ","
          ),
        },
        {
          OrdinalNumber: "7",
          Targets:
            "Tổng số thuế thu nhập cá nhân (TNCN) đã khấu trừ [34]=[35]+[36]",
          TargetsCode: "[34]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxwithHeldResident +
              this.DeclarationSettlementDetail.TotalTaxwithHeldNonResident,
            ","
          ),
        },
        {
          OrdinalNumber: "7.1",
          Targets: "Cá nhân cư trú",
          TargetsCode: "[35]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxwithHeldResident,
            ","
          ),
        },
        {
          OrdinalNumber: "7.2",
          Targets: "Cá nhân không cư trú",
          TargetsCode: "[36]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxwithHeldNonResident,
            ","
          ),
        },
        {
          OrdinalNumber: "8",
          Targets:
            "Tổng số thuế được giảm do làm việc tại khu kinh tế [37]=[38]+[39]",
          TargetsCode: "[37]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxEconomicReductionResident +
              this.DeclarationSettlementDetail
                .TotalTaxEconomicReductionNonResident,
            ","
          ),
        },
        {
          OrdinalNumber: "8.1",
          Targets: "Cá nhân cư trú",
          TargetsCode: "[38]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxEconomicReductionResident,
            ","
          ),
        },
        {
          OrdinalNumber: "8.2",
          Targets: "Cá nhân không cư trú",
          TargetsCode: "[39]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail
              .TotalTaxEconomicReductionNonResident,
            ","
          ),
        },
        {
          OrdinalNumber: "9",
          Targets:
            "Tổng số thuế TNCN từ tiền phí mua bảo hiểm nhân thọ, bảo hiểm không bắt buộc khác của doanh nghiệp bảo hiểm  bảo hiểm không thành lập tại Việt Nam cho người lao động",
          TargetsCode: "[40]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxInsurrance,
            ","
          ),
        },
        {
          OrdinalNumber: "10",
          Targets:
            "Tổng số thuế TNCN đã khấu trừ trên tiền phí mua bảo hiểm nhân thọ, bảo hiểm không bắt buộc khác của doanh nghiệp bảo hiểm không thành lập tại Việt Nam cho người lao động",
          TargetsCode: "[41]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxwithHeldInsurrance,
            ","
          ),
        },
      ];
      this.dataTable1 = [
        {
          OrdinalNumber: "1",
          Targets:
            "Tổng số cá nhân uỷ quyền cho tổ chức, cá nhân trả thu nhập quyết toán thay",
          TargetsCode: "[42]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalEmployeeAuthorize,
            ","
          ),
        },
        {
          OrdinalNumber: "2",
          Targets: "Tổng số thuế TNCN đã khấu trừ",
          TargetsCode: "[43]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxWithHeld,
            ","
          ),
        },
        {
          OrdinalNumber: "3",
          Targets: "Tổng số thuế TNCN phải nộp",
          TargetsCode: "[44]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTax,
            ","
          ),
        },
        {
          OrdinalNumber: "4",
          Targets: "Tổng số thuế TNCN còn phải nộp NSNN",
          TargetsCode: "[45]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalTaxPayable,
            ","
          ),
        },
        {
          OrdinalNumber: "5",
          Targets: "Tổng số thuế TNCN đã nộp thừa ",
          TargetsCode: "[46]",
          CalculationUnit: "VNĐ",
          NumberOfPeopleOrAmount: formatMoney(
            this.DeclarationSettlementDetail.TotalExcessTax,
            ","
          ),
        },
      ];
    },
  },
  mounted() {
    this.isReload = true;
  },
  watch: {
    DeclarationSettlementDetail: {
      handler() {
        this.mapData();
        this.loading = false;
      },
      deep: true,
    },
    isReload: {
      handler() {
        this.mapData();
        this.loading = false;
      },
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
#appendix-total {
  position: relative;

  .el-loading-parent--relative {
    position: absolute !important;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    z-index: 2;

    .el-loading-mask{
        opacity: 1;
    }
  }

  .tax-deduction {
    font-size: 15px;
    font-family: $_fontmedium;
  }

  .settlement-instead {
    font-size: 15px;
    font-family: $_fontmedium;
  }

  .tbl-main .el-table__header div {
    font-family: $_fontmedium !important;
  }

  .title-appendix-total {
    font-family: $_fontbold;
    font-size: 20px;
    margin: 24px 0 16px 0;
  }

  .currency-unit {
    float: right;
    text-align: right;
  }

  .total-table-settlement-instead {
    margin-bottom: 24px;
    .el-table th.is-right {
      padding-right: 10px !important;
    }
  }

  .el-table__body-wrapper {
    height: calc(100% - 50px) !important;
  }

  .total-table-tax-deduction {
    .el-loading-parent--relative {
      position: absolute !important;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
    }

    .el-table th.is-right {
      padding-right: 10px !important;
    }

    .el-table__body-wrapper .el-table__row:nth-child(1) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(3) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(6) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(7) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(8) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(11) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(14) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(17) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(20) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }

    .el-table__body-wrapper .el-table__row:nth-child(21) {
      span {
        font-family: $_fontmedium;
      }
      background-color: $hover-table-row;
    }
  }
}
</style>