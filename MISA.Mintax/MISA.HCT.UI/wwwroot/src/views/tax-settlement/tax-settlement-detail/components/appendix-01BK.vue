<template>
  <div class="appendix-bk1-container">
    <div class="total-header-content">
      BẢNG KÊ THU NHẬP CHỊU THUẾ VÀ THUẾ THU NHẬP CÁ NHÂN ĐỐI VỚI THU NHẬP TỪ
      TIỀN LƯƠNG, TIỀN CÔNG CỦA CÁ NHÂN THUỘC DIỆN TÍNH THUẾ THEO BIỂU LŨY TIẾN
      TỪNG PHẦN
    </div>
    <div class="total-filter-bar" v-if="!isEmpty">
      <div class="total-search">
        <fieldinput
          type="input-icon"
          width="356px"
          classicon="icon-search"
          :placeholder="'Tìm kiếm họ tên, mã nhân viên, mã số thuế,...'"
          @value="filterBar.keyword = $event"
          :valueInput="filterBar.keyword"
          :clearable="true"
        />
      </div>
      <div class="total-unit">Đơn vị tiền tệ: VNĐ</div>
    </div>
    <div class="appendix-bk1-table" v-if="!isEmpty">
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
        :listColumn="listColumn2"
        :dataTable="dataTable2"
        :summary="true"
        :positionSummary="positionSummary"
        :havePagination="true"
        :total="numberOfRecord"
        :loading="false"
        :tooltip="true"
        :haveAction="false"
        :listaction="listaction"
        @pagination="getFilter"
        typepage="người nộp thuế"
        :optionsSelect="optionsSelect"
        :displayIndexRow="true"
        :listIndex="listIndex"
        :isAdjustNumber="true"
        @selectChange="selectChange"
      />
      <div
        class="temporary-mode-btn"
        v-if="numberOfRecord == 0 && !isEmpty && !loadingFirst && !loadingFirst"
      >
        <img
          src="@/assets/images/Group 18279.png"
          alt="not have data"
          style="position: relative"
        />
        <span>Không tìm thấy kết quả phù hợp</span>
      </div>
    </div>
    <div class="empty-content" v-if="isEmpty && !loadingFirst && !loadingFirst">
      <img src="@/assets/images/Group 18279.png" />
      <p>Không có thông tin người phụ thuộc.</p>
    </div>
  </div>
</template>

<script>
import tbl from "@/components/tables";
import fieldinput from "@/components/inputs";
import declarationSettlement from "@/api/etax/declaration-settlement.js";

export default {
  components: {
    tbl,
    fieldinput,
  },

  data() {
    return {
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
          width: "170",
        },

        {
          valueIndex: "[11]",
          width: "120",
        },
        {
          valueIndex: "[12]",
          width: "140",
        },
        {
          valueIndex: "[13]",
          width: "120",
        },
        {
          valueIndex: "[14]",
          width: "120",
        },
        {
          valueIndex: "[15]",
          width: "120",
        },
        {
          valueIndex: "[16]",
          width: "120",
        },
        {
          valueIndex: "[17]",
          width: "120",
        },
        {
          valueIndex: "[18]",
          width: "120",
        },
        {
          valueIndex: "[19]",
          width: "120",
        },
        {
          valueIndex: "[20]",
          width: "120",
        },
        {
          valueIndex: "[21]",
          width: "120",
        },
        {
          valueIndex: "",
          width: "120",
        },
        {
          valueIndex: "[22]",
          width: "120",
        },
        {
          valueIndex: "[23]",
          width: "120",
        },
        {
          valueIndex: "[24]",
          width: "120",
        },
      ],
      listColumn2: [
        {
          label: "Họ và tên",
          propertyName: "FullName",
          align: "left",
          width: "",
          minWidth: "160",
          type: "",
          prop: "FullName",
          sortable: false,
        },
        {
          label: "Mã số thuế",
          propertyName: "TaxNo",
          align: "left",
          width: "",
          minWidth: "120",
          type: "",
          prop: "TaxNo",
          sortable: false,
        },
        {
          label: "Số CMND/ Hộ chiếu",
          propertyName: "CitizenIdentityNo",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "CitizenIdentityNo",
          sortable: false,
        },
        {
          label: "Cá nhân ủy quyền quyết toán thay",
          propertyName: "IsAuthorize",
          width: "170",
          align: "center",
          type: "dropdown",
          prop: "IsAuthorize",
          sortable: false,
          optionsSelect: [
            {
              value: 0,
              label: "Không ủy quyền",
            },
            {
              value: 1,
              label: "Ủy quyền",
            },
          ],
        },
        {
          label: "Thu nhập chịu thuế",
          propertyName: "",
          align: "center",
          width: "",
          minWidth: "140",
          type: "",
          prop: "",
          sortable: false,
          group: [
            {
              label: "Tổng số",
              propertyName: "TotalIncomeTaxable",
              align: "right",
              width: "",
              minWidth: "120",
              type: "decimal",
              prop: "TotalIncomeTaxable",
              sortable: false,
            },
            {
              label: "Trong đó: TNCN được giảm thuế",
              propertyName: "",
              align: "center",
              width: "",
              minWidth: "260",
              type: "",
              prop: "",
              sortable: false,
              group: [
                {
                  label: "Làm việc trong KKT",
                  propertyName: "TotalIncomeKKT",
                  align: "right",
                  width: "",
                  minWidth: "140",
                  type: "decimal",
                  prop: "TotalIncomeKKT",
                  sortable: false,
                },
                {
                  label: "Theo Hiệp định",
                  propertyName: "TotalIncomeAgreement",
                  align: "right",
                  width: "",
                  minWidth: "120",
                  type: "decimal ",
                  prop: "TotalIncomeAgreement",
                  sortable: false,
                },
              ],
            },
          ],
        },

        {
          label: "Các khoản giảm trừ",
          propertyName: "",
          align: "center",
          width: "",
          minWidth: "480",
          type: "",
          prop: "",
          sortable: false,
          group: [
            {
              label: "Số lượng NPT tính giảm trừ giảm trừ",
              propertyName: "DependentNumber",
              minWidth: "120",
              align: "right",
              type: "decimal",
              prop: "DependentNumber",
              sortable: false,
            },
            {
              label: "Tổng số tiền giảm trừ gia cảnh",
              propertyName: "TotalReduction",
              minWidth: "120",
              align: "right",
              type: "decimal",
              prop: "TotalReduction",
              sortable: false,
            },
            {
              label: "Từ thiện, nhân đạo, khuyến học",
              propertyName: "OtherRedution",
              minWidth: "120",
              align: "right",
              type: "decimal",
              prop: "OtherRedution",
              sortable: false,
            },
            {
              label: "Bảo hiểm được trừ",
              propertyName: "InsurranceReduction",
              minWidth: "120",
              align: "right",
              type: "decimal",
              prop: "InsurranceReduction",
              sortable: false,
            },
            {
              label: "Quỹ hưu trí tự nguyện được trừ",
              propertyName: "RetireFundRedution",
              minWidth: "120",
              align: "right",
              type: "decimal",
              prop: "RetireFundRedution",
              sortable: false,
            },
          ],
        },
        {
          label: "Thu nhập tính thuế",
          propertyName: "TotalIncomeTax",
          minWidth: "120",
          align: "right",
          type: "decimal",
          prop: "TotalIncomeTax",
          sortable: false,
        },
        {
          label: "Số thuế TNCN đã khấu trừ",
          propertyName: "TotalTaxWithHeld",
          minWidth: "120",
          align: "right",
          type: "decimal",
          prop: "TotalTaxWithHeld",
          sortable: false,
        },
        {
          label: "Số thuế TNCN được giảm do làm việc trong KKT",
          propertyName: "KKTReduction",
          minWidth: "120",
          align: "right",
          type: "decimal",
          prop: "KKTReduction",
          sortable: false,
        },
        {
          label: "Cá nhân nước ngoài ủy quyền quyết toán dưới 12 tháng/năm",
          propertyName: "IsAuthorizeForeign",
          minWidth: "120",
          align: "center",
          type: "dropdown",
          prop: "IsAuthorizeForeign",
          sortable: false,
          optionsSelect: [
            {
              value: 0,
              label: "Không",
            },
            {
              value: 1,
              label: "Có",
            },
          ],
        },
        {
          label: "Chi tiết kết quả quyêt toán thay cho cá nhân nộp thuế",
          propertyName: "",
          minWidth: "400",
          align: "center",
          type: "",
          prop: "",
          sortable: false,
          group: [
            {
              label: "Tổng số thuế phải nộp",
              propertyName: "TotalTaxPayable",
              minWidth: "120",
              align: "right",
              type: "decimal",
              prop: "TotalTaxPayable",
              sortable: false,
            },
            {
              label: "Số thuế đã nộp thừa",
              propertyName: "TaxOverpaid",
              minWidth: "120",
              align: "right",
              type: "decimal",
              prop: "TaxOverpaid",
              sortable: false,
            },
            {
              label: "Số thuế còn phải nộp",
              propertyName: "PayableTaxAmount",
              minWidth: "120",
              align: "right",
              type: "decimal",
              prop: "PayableTaxAmount",
              sortable: false,
            },
          ],
        },
      ],

      dataTable2: [],
      optionsSelect: [
        {
          value: 0,
          label: "Không ủy quyền",
        },
        {
          value: 1,
          label: "Ủy quyền",
        },
      ],
      listAuthorizeForeign: [
        {
          value: 0,
          label: "Không",
        },
        {
          value: 1,
          label: "Có",
        },
      ],
      numberOfRecord: 0,
      loadingFirst: true,
      loadingAfter: true,
      isFirst: true,
      listaction: [
        {
          value: 0,
          name: "Nhân bản",
        },
        {
          value: 1,
          name: "Sửa",
        },
        {
          value: 2,
          name: "Xóa",
        },
      ],
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
            PropertyName: "TotalIncomeKKT",
            PropertyIndex: 7,
          },
          {
            PropertyName: "TotalIncomeAgreement",
            PropertyIndex: 8,
          },
          {
            PropertyName: "DependentNumber",
            PropertyIndex: 9,
          },
          {
            PropertyName: "TotalReduction",
            PropertyIndex: 10,
          },
          {
            PropertyName: "OtherRedution",
            PropertyIndex: 11,
          },
          {
            PropertyName: "InsurranceReduction",
            PropertyIndex: 12,
          },
          {
            PropertyName: "RetireFundRedution",
            PropertyIndex: 13,
          },
          {
            PropertyName: "TotalIncomeTax",
            PropertyIndex: 14,
          },
          {
            PropertyName: "TotalTaxWithHeld",
            PropertyIndex: 15,
          },
          {
            PropertyName: "KKTReduction",
            PropertyIndex: 16,
          },
          {
            PropertyName: "TotalTaxPayable",
            PropertyIndex: 18,
          },
          {
            PropertyName: "TaxOverpaid",
            PropertyIndex: 19,
          },
          {
            PropertyName: "PayableTaxAmount",
            PropertyIndex: 20,
          },
        ],
      },
      isEmpty: false,
      dataSettlementDetail: {},
      filterBar: {
        settlementID: "",
        declarationSettlementDetailType: 0,
        keyword: "", //Tìm kiếm theo họ tên, mã nhân viên , mã số thuế
        pageIndex: 1,
        pageSize: 25,
      },
      serviceData: [], // Dữ liệu danh sách ddetail service gửi về
    };
  },

  methods: {
    /**
     * Hàm gọi dữ liệu lấy danh sách phụ lục 05-1BK
     * Created by NQTrung 24/10/2020
     */
    async getData() {
      this.loadingFirst = true;
      this.loadingAfter = true;
      var res = await declarationSettlement.getDeclarationSettlementDetail(
        this.filterBar
      );
      if (res.data.Success && !res.data.Data.IsEmpty) {
        this.serviceData = res.data.Data.DeclarationSettlementDetails;
        this.mapdata(res.data.Data.DeclarationSettlementDetails);
        this.numberOfRecord = res.data.Data.TotalDeclarationSettlementDetail;
        this.isEmpty = res.data.Data.IsEmpty;
      } else {
        this.dataTable2 = [];
        this.isEmpty = true;
      }
      this.loadingFirst = false;
      this.loadingAfter = false;
      this.isFirst = false;
    },

    /**
     *Hàm map dữ liệu vào bảng
     */
    mapdata(val) {
      var datas = [];
      this.dataTable2 = [];
      // datas.push(this.indexs);
      val.forEach((item) => {
        var data = {};
        data.DeclarationSettlementDetailID =
          item["DeclarationSettlementDetailID"];
        data.FullName = item["FullName"];
        data.TaxNo = item["TaxNo"];
        data.CitizenIdentityNo = item["CitizenIdentityNo"];
        data.IsAuthorize = this.findAuthorize(item["IsAuthorize"]);
        data.TotalIncomeTaxable = item["TotalIncomeTaxable"];
        data.TotalIncomeKKT = 0;
        data.TotalIncomeAgreement = 0;
        data.DependentNumber = item["DependentNumber"];
        data.TotalReduction = item["TotalReduction"];
        data.OtherRedution = item["OtherRedution"];
        data.InsurranceReduction = item["InsurranceReduction"];
        data.RetireFundRedution = item["RetireFundRedution"];
        data.TotalIncomeTax = item["TotalIncomeTax"];
        data.TotalTaxWithHeld = item["TotalTaxWithHeld"];
        data.KKTReduction = item["KKTReduction"];
        data.IsAuthorizeForeign = this.findAuthorizeForeign(
          item["IsAuthorizeForeign"]
        );
        data.TotalTaxPayable = item["TotalTaxPayable"];
        data.TaxOverpaid = item["TaxOverpaid"];
        data.PayableTaxAmount = item["PayableTaxAmount"];
        data.IsChangeData = item["IsChangeData"];
        datas.push(data);
      });
      this.dataTable2.push(...datas);
    },

    /**
     * Chuyển từ value -> lable ủy quyền/ không ủy quyền
     * Created by NQTrung 31/10/2020
     */
    findAuthorize(value) {
      for (var i in this.optionsSelect) {
        if (this.optionsSelect[i].value == value) {
          return this.optionsSelect[i].label;
        }
      }
    },

    /**
     * Chuyển từ value -> lable ủy quyền/ không ủy quyền của cá nhân nước ngoài
     */
    findAuthorizeForeign(value) {
      for (var i in this.listAuthorizeForeign) {
        if (this.listAuthorizeForeign[i].value == value) {
          return this.listAuthorizeForeign[i].label;
        }
      }
    },

    /**
     * Chuyển ngược lại từ lable -> value
     */
    convertIsAuthorize(value) {
      for (var i in this.optionsSelect) {
        if (this.optionsSelect[i].label == value) {
          return this.optionsSelect[i].value;
        }
      }
    },

    /**
     * Chuyển ngược lại từ lable -> value
     */
    convertIsAuthorizeForeign(value) {
      for (var i in this.listAuthorizeForeign) {
        if (this.listAuthorizeForeign[i].label == value) {
          return this.listAuthorizeForeign[i].value;
        }
      }
    },

    /**
     * Hàm chỉnh sửa thông tin ủy quyền / không ủy quyền của tờ khai quyết toán
     * created by NQTrung 02/11/2020
     */
    async selectChange(row) {
      var res = await declarationSettlement.getDeclarationSettlementDetailById(
        row.DeclarationSettlementDetailID
      );
      this.dataSettlementDetail = { ...res.data.Data };
      if (typeof row.IsAuthorize == "number") {
        this.dataSettlementDetail.IsAuthorize = row.IsAuthorize;
      } else {
        this.dataSettlementDetail.IsAuthorize = this.convertIsAuthorize(
          row.IsAuthorize
        );
      }
      if (typeof row.IsAuthorizeForeign == "number") {
        this.dataSettlementDetail.IsAuthorizeForeign = row.IsAuthorizeForeign;
      } else {
        this.dataSettlementDetail.IsAuthorizeForeign = this.convertIsAuthorizeForeign(
          row.IsAuthorizeForeign
        );
      }
      var selectRow = this.serviceData.find(
        (x) =>
          x.DeclarationSettlementDetailID == row.DeclarationSettlementDetailID
      );
      selectRow.IsAuthorizeForeign = this.dataSettlementDetail.IsAuthorizeForeign;
      selectRow.IsAuthorize = this.dataSettlementDetail.IsAuthorize;
      // Cập nhật cá nhân không ủy quyền hay ủy quyền
      var updateRes = await declarationSettlement.putDeclarationSettlementDetail(
        selectRow
      );

      if (!updateRes.data || updateRes.data.Success == false) {
        this.$_Notification.error(
          "Thông báo",
          `Cập nhật thông tin người nộp thuế ${selectRow.FullName} thất bại`,
          []
        );
        this.mapdata(this.serviceData);
      } else {
        this.$_Notification.success(
          "Thông báo",
          `Cập nhật thông tin người nộp thuế ${selectRow.FullName} thành công`,
          []
        );
      }

      this.emitUpdateSettlement();
    },

    emitUpdateSettlement() {
      this.$emit("reload", true);
    },

    setStyle() {
      return { height: "100%" };
    },

    getFilter(val) {
      this.filterBar.pageIndex = val.page;
      this.filterBar.pageSize = val.limit;
    },
  },

  created() {
    this.filterBar.settlementID = this.$route.params.id;
  },

  watch: {
    filterBar: {
      async handler() {
        await this.getData();
      },
      deep: true,
    },
  },
};
</script>

<style  lang="scss" >
@import "@/styles/variables.scss";
.appendix-bk1-container {
  height: calc(100vh - 214px);
  .total-header-content {
    font-size: 20px;
    font-family: $_fontbold;
    padding-top: 24px;
  }

  .total-filter-bar {
    margin-top: 16px;
    display: flex;

    .total-unit {
      align-items: center;
      padding: 12px 0;
      right: 24px;
      position: absolute;
      justify-self: flex-end;
      color: $medium-dark-grey;
    }
  }

  .appendix-bk1-table {
    margin-top: 16px;
    height: calc(100% - 146px);
    position: relative;

    .loading-first.el-loading-parent--relative {
      position: absolute !important;
      top: 0;
      bottom: 0;
      left: 0;
      right: 0;
      z-index: 9999;

      .el-loading-mask {
        opacity: 1;
      }
    }

    .el-table__body-wrapper {
      height: calc(100% - 145px);
    }

    .loading-after.el-loading-parent--relative {
      position: absolute !important;
      top: 144px;
      left: 0;
      right: 0;
      bottom: 60px;

      .el-loading-mask {
        background-color: $white !important;
        opacity: 0.8;
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

    .temporary-mode-btn {
      position: absolute;
      top: 65%;
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
}

// Thực hiện style reponsive cho màn hình kích thước <= 1366px
@media only screen and (max-width: 1366px) {
  .appendix-bk1-container {
    height: calc(100vh - 84px);
    .appendix-bk1-table {
      .temporary-mode-btn {
        top: 65%;

        img {
          display: none;
        }
      }
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
}
</style>>

