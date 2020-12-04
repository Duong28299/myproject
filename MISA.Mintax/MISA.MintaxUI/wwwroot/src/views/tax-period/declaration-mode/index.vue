<template>
  <div class="declaration-mode">
    <el-dialog
      :before-close="handleClose"
      title="Kết quả gửi hồ sơ"
      :visible.sync="dialogVisible"
      width="950px"
      class="dialog-iframe"
    >
      <iframe v-if="ifSrc" :src="ifSrc" width="100%" height="668"></iframe>
    </el-dialog>
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
    <tbl-temporary
      v-if="!isEmpty"
      ref="declaration"
      class="tbl-order-normal"
      :listColumn="listColumn"
      :dataTable="dataTable"
      :haveCheck="haveCheck"
      :summary="true"
      :pageSizes="pageSizes"
      :positionSummary="positionSummary"
      :havePagination="true"
      :total="numberOfRecord"
      :layoutPaging="layoutPaging"
      :chipArray="chipArray"
      :tooltip="true"
      :listaction="listaction"
      typepage="tờ khai"
      @pagination="getFilter"
      @selection="getSelection"
      @chip-emit="getObjectEmit"
      @handleSelectionChange="getRowsSelection"
      @handleSelectAll="handleSelectAll"
      @handleRowDBLClick="handleRowDBLClick"
      :loading="false"
      destiStatus="DeclarationStatus"
      :destiArrStatus="destiArrStatus"
    />

    <div
      class="empty-search"
      v-if="numberOfRecord == 0 && !isEmpty && !loadingFirst && !loadingAfter"
    >
      <img
        src="@/assets/images/2.png"
        alt="not have data"
        style="position: relative"
      />
      <span>Không tìm thấy kết quả phù hợp</span>
    </div>
    <div class="empty-case" v-if="isEmpty && !loadingFirst && !loadingFirst">
      <img
        src="@/assets/images/2.png"
        alt="not have data"
        style="position: relative"
        class="img-empty"
      />
      <!-- Button thêm mới bảng tính -->
      <div class="btn-declaration-mode">
        <m-btn
          type="btn-primary-icon"
          name="Thêm tờ khai thuế"
          :handleClick="newDeclaration"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 17812.png" />
          </template>
        </m-btn>
      </div>
    </div>
    <dialog-confirm />

    <!-- Vùng hiển thị Dialog xem chi tiết tờ khai -->
    <dialog-sample-declaration
      :dialogFormVisible="dialogSampleDeclaration"
      @close-dialog="closeDialogDeclaration"
      :declarationData="declarationData"
    ></dialog-sample-declaration>
    <!-- End vùng hiển thị dialog xem tờ khai -->
  </div>
</template>

<script>
import Table from "@/components/tables";
import ConfirmDialog from "@/components/dialog/modules/confirm";
import Button from "@/components/buttons";
import Declaration from "@/api/etax/declaration";
import periodType from "@/enums/periodType.js";
import declarationType from "@/enums/declarationType.js";
import SampleDeclaration from "@/views/tax-period/declaration-mode/components/sample-declaration.vue";
import actionTable from "@/enums/actionTable";

import signDeclaration from "@/mixins/signDeclaration";
export default {
  name: "DeclarationMode",
  components: {
    "tbl-temporary": Table,
    "m-btn": Button,
    "dialog-confirm": ConfirmDialog,
    "dialog-sample-declaration": SampleDeclaration,
  },
  mixin: [signDeclaration],
  data() {
    return {
      listaction: [],
      // thay đổi giá trị các loại trạng thái tờ khai lttuan 131020
      destiArrStatus: [2, 3, 10, 11, 12],
      ifSrc: "",
      dialogVisible: false,
      listColumn: [
        {
          label: "Tên tờ khai",
          propertyName: "DeclarationName",
          width: "",
          minWidth: "500",
          align: "left",
          type: "",
          prop: "DeclarationName",
          sortable: true,
        },
        {
          label: "Kỳ tính thuế",
          propertyName: "PeriodTax",
          width: "",
          minWidth: "120",
          align: "left",
          type: "",
          prop: "PeriodTax",
          sortable: true,
        },
        {
          label: "Loại tờ khai",
          propertyName: "DeclarationType",
          width: "",
          minWidth: "160",
          align: "left",
          type: "",
          prop: "DeclarationType",
          sortable: true,
        },
        {
          label: "Lần nộp",
          propertyName: "NumberOfDeclaration",
          width: "",
          minWidth: "120",
          align: "right",
          type: "decimal",
          prop: "NumberOfDeclaration",
          sortable: true,
        },
        {
          label: "Ngày lập",
          propertyName: "CreatedDate",
          width: "",
          minWidth: "120",
          align: "center",
          type: "date",
          prop: "CreatedDate",
          sortable: true,
        },
        {
          label: "Ngày nộp",
          propertyName: "SendDate",
          width: "",
          minWidth: "120",
          align: "center",
          type: "date",
          prop: "SendDate",
          sortable: true,
        },
        {
          label: "Trạng thái",
          propertyName: "DeclarationStatus",
          width: "180",
          minWidth: "180",
          align: "left",
          type: "chip",
          prop: "DeclarationStatus",
          sortable: true,
        },
        {
          label: "Thuế TNCN khấu trừ",
          propertyName: "TotalTaxWithHeld",
          width: "",
          minWidth: "165",
          align: "right",
          type: "decimal",
          tooltip: "Thuế thu nhập cá nhân khấu trừ",
          prop: "TotalTaxWithHeld",
          sortable: true,
        },
      ],
      positionSummary: {
        Summary: [
          {
            PropertyName: "",
            PropertyIndex: 0,
          },
          {
            PropertyName: "TotalTaxWithHeld",
            PropertyIndex: 8,
          },
        ],
      },
      dataTable: [],
      haveCheck: true,
      chipArray: "DECLARATION",
      layoutPaging: "prev, pager, next, sizes",
      selectionObject: [],
      loadingFirst: true,
      loadingAfter: true,
      isFirst: true,
      isEmpty: false,
      numberOfRecord: 0,
      filter: {
        organizationID: "",
        month: 0,
        year: "",
        pageIndex: 1,
        pageSize: 25,
        keyword: "",
        declarationStatus: 0,
        groupMode: "TemporaryMode",
      },
      pageSizes: [25, 50, 75, 100],
      select: [], // hàng đã chọn
      totalSelect: 0, // tổng số hàng đã chọn
      isDelete: false, // Có cho phép xóa hay không - nếu đã gửi thì không cho phép, chếu chưa gửi thì cho phép xóa
      dialogSampleDeclaration: false, // Bật hoặc tắt dialog
      declarationData: {}, // Thông tin của tờ khai thuế
      organizationId: "", // ID của tổ chức
      isCheckAllRow: false,
      TaxCode: "0101243150", //Mã taxcode đơn vị tạm thời fix cứng
      isNotExpired: false, //Đã hết hạn sử dụng MTax chưa
    };
  },

  computed: {
    filterBar() {
      return this.$store.getters.filter;
    },
  },

  watch: {
    /**
     * Kiểm tra sự thay đổi giá trị filter trong store
     */
    "$store.getters.filter": {
      async handler(val) {
        if (val.refresh) {
          this.getListDeclaration(this.filter);
          this.$store.dispatch("filter/addRefresh", false);
        }
        if (val.signMulti) {
          await this.multiSignDeclaration();
          this.$store.dispatch("filter/addSignMulti", false);
        }
        return Object.assign(this.filter, val);
      },
      deep: true,
    },

    /**
     * Kiểm tra sự thay đổi giá trị filterBar
     */
    filterBar: {
      handler(val) {
        if (val) {
          this.filter = Object.assign(this.filter, val);
        }
      },
      deep: true,
    },

    /**
     * Kiểm tra sự thay đổi giá trị filter
     */
    filter: {
      async handler(val) {
        if (val) {
          this.getListDeclaration();
        }
      },
      deep: true,
    },
  },

  /**
   * Hàm chạy khi khởi tạo Instance
   */
  async created() {
    this.initDataMixins();
    Object.assign(this.filter, this.$store.getters.filter);
    this.$emit("isCheckAllRow", false);
    this.listaction = actionTable.DECLARATION;
  },

  methods: {
    /**
     * Hàm đóng reset src iframe
     */
    handleClose(done) {
      setTimeout(() => {
        this.ifSrc = "";
      }, 500);
      done();
    },

    /**
     * Hàm lấy dữ liệu danh sách tờ khai từ api
     * NVHUNG - 13/7/2020
     * Modified by vdthang 27.11.2020 - sửa lại logic xử lý khi có 2 loading và gọi service
     */
    async getListDeclaration() {
      this.loadingFirst = true;
      this.loadingAfter = true;
      var res = await Declaration.getDeclaration(this.filter);
      this.dataTable = [];
      if (res.data && res.data.Success) {
        this.numberOfRecord = res.data.Data.TotalDeclaration;
        this.mapDataServiceToTable(res.data.Data.DeclarationLists);
        this.isEmpty = res.data.Data.IsEmpty;
      } else {
        this.isEmpty = true;
      }
      this.emitEmpty(this.isEmpty);
      this.loadingFirst = false;
      this.loadingAfter = false;
      this.isFirst = false;
    },

    /**
     * Hàm thực hiện chuyển đổi data lấy từ API về dữ liệu trong bảng tờ khai
     * created by vdthang 27.11.2020
     */
    mapDataServiceToTable(data) {
      this.dataTable = data.map((item) => {
        item["PeriodTax"] = `${periodType.findInArray(
          "PERIOD_TAX",
          item["TaxType"]
        )} ${item.PeriodMonthOrQuarter}/${item.PeriodYear}`;
        item["DeclarationType"] = declarationType.findInArray(
          item["DeclarationType"]
        );
      });
      this.dataTable = data;
    },

    /**
     * Hàm gán lại số trang và số bản ghi/trang khi thay đổi ở paging
     * created by vdthang 27.11.2020
     */
    getFilter(val) {
      this.filter.pageSize = val.limit;
      this.filter.pageIndex = val.page;
    },

    /**
     * Hàm thực hiện emit lại lên cha giá trị isEmpty
     * created by vdthang 27.11.2020
     */
    emitEmpty(val) {
      this.$emit("isEmpty", val);
    },

    /**
     * Hàm gọi service lấy src cho iframe
     * created by vdthang 27.11.2020
     */
    getObjectEmit(val) {
      const taxCode = "0101243150999";
      const submissionCode = val.SubmissionCode
        ? val.SubmissionCode
        : "11420201001895800";
      const res = Declaration.getPreviewNotification(taxCode, submissionCode);
      this.dialogVisible = true;
      this.ifSrc = res;
    },

    /**
     * Hàm gọi khi chọn các check box bản ghi
     * created by vdthang 27.11.2020
     */
    getSelection(val) {
      this.selectionObject = val;
    },

    /**
     * Hàm bắt sự kiện select all check box table
     * created by vdthang 06.07.2020
     */
    handleSelectAll(val) {
      if (val.length == 0) {
        this.$emit("isCheckAllRow", false);
      } else {
        this.$emit("isCheckAllRow", true);
      }
    },

    /**
     * Hàm bỏ chọn tất cả các ô check box
     * Auth: NTNgoc
     * Created date: 13/7/2020
     */
    clearAllSelection(rows) {
      if (rows) {
        rows.forEach((row) => {
          this.$refs.declaration.toggleRowSelection(row);
        });
      } else {
        this.$refs.declaration.clearSelection();
      }
    },

    /**
     * Lấy thông tin hàm click
     * Check trạng thái enable/disable của button Sửa dựa trên số lượng hàng đã chọn + thêm hiệu ứng
     * auth: NTNgoc
     * created date:10/7/2020
     */
    getRowsSelection(row) {
      this.$store.commit("taxPeriod/setDataOfRow", row);
      this.select = row; // Lấy thông tin hàm click
      //nếu chọn nhiều hơn hoặc bằng 1 thì hiện button xóa.
      if (row.length >= 1) {
        this.$emit("isCheckAllRow", true);
      } else {
        this.$emit("isCheckAllRow", false);
      }
    },

    /**
     * Truyền sang component để mở modal thêm mới bảng tính
     */
    newDeclaration() {
      this.$emit("createDeclaration", true);
    },

    checkRow() {
      if (typeof this.select != "undefined" && this.select.length > 0) {
        this.totalSelect = this.select.length;
        return true;
      } else {
        return false;
      }
    },

    /**
     * Xử lí sự kiện Sửa, Xóa, Gửi tờ khai khi ấn vào nút
     * Auth: NTNgoc
     * Created date: 13/7/2020
     */
    handlerClickEventByIndex() {},

    /**
     * Hàm trả về trạng thái tờ khai
     * NVHUNG - 13/7/2020
     */
    getFilterDeclaration(type) {
      let typeResult = "";
      switch (type) {
        case 1:
          typeResult = "Chưa nộp";
          break;
        case 2:
          typeResult = "Đã nộp";
          break;
        case 3:
          typeResult = "Đã tiếp nhận";
          break;
        case 4:
          typeResult = "Đã chấp nhận";
          break;
        case 5:
          typeResult = "Không chấp nhận";
          break;
        default:
          typeResult = "error";
      }
      return typeResult;
    },

    /**
     * Hàm xử lý double click vào một hàng trên danh sách tờ khai
     * created by nhdoanh 20.07.2020
     */
    async handleRowDBLClick(val) {
      var res = await Declaration.getDeclarationID(val.DeclarationID);

      if (res.data.Success === true && res.data.Data !== null) {
        this.declarationData = res.data.Data;
        // Mở dialog xem chi tiết tờ khai thuế
        this.dialogSampleDeclaration = true;
      }
    },

    /**
     * Hàm đóng dialog xem chi tiết tờ khai thuế
     * created by nhdoanh 20.07.2020
     */
    closeDialogDeclaration(val) {
      this.dialogSampleDeclaration = val;
    },

    /**
     * Hàm ký nộp nhiều tờ khai
     * Created by CVCuong created date 1/9/2020
     */
    async multiSignDeclaration() {
      this.dataSign = this.select;

      await this.registerByMTAX();
    },

    /**
     * Khởi tạo giá trị đối với mixin kí tờ khai
     * created by nvbinh2 11.11.2020
     */
    initDataMixins() {
      this.keyProperty = "DeclarationID";
      this.mode = "Declaration";
      this.nameofDeclaration = "DeclarationName";
      this.callReloadPage = async function () {
        await this.getListDeclaration();
      };
    },
  },
};
</script>
<style lang="scss" >
@import "@/styles/variables.scss";
.declaration-mode {
  .tbl-order-normal {
    .el-table__body-wrapper {
      height: calc(100% - 100px) !important;
    }
  }

  .loading-first.el-loading-parent--relative {
    height: 100%;

    .el-loading-mask {
      opacity: 1;
    }
  }

  .loading-after.el-loading-parent--relative {
    position: absolute !important;
    top: 52px;
    bottom: 60px;
    left: 0;
    right: 0;
    z-index: 10;

    .el-loading-mask {
      background-color: $white !important;
    }
  }
}
</style>

<style lang="scss" scoped>
@import "@/styles/variables.scss";
.declaration-mode {
  position: relative;

  .empty-search {
    position: absolute;
    top: 50%;
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

  .empty-case {
    position: absolute;
    top: 45%;
    left: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    transform: translate(-50%, -50%);

    .btn-declaration-mode {
      margin-top: 8px;
    }
  }
}

@media only screen and (max-width: 1366px) {
  .declaration-mode {
    .empty-search {
      top: 40%;

      img {
        width: 50%;
      }
    }

    .empty-case {
      top: 40%;

      .img-empty{
        width: 85%;
      }
    }
  }
}
</style>
