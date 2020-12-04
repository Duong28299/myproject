<template>
  <div class="temporary-mode">
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
      ref="temporary"
      class="tbl-order-normal"
      :haveCheck="true"
      :havePagination="true"
      :loading="false"
      :summary="true"
      :listColumn="listColumn"
      :dataTable="dataTable"
      :pageSizes="pageSizes"
      :total="numberOfRecord"
      :positionSummary="positionSummary"
      :tooltip="true"
      :listaction="listaction"
      typepage="bảng tính thuế"
      @handleSelectionChange="getRowsSelection"
      @row-action="getRowAction"
      @pagination="getFilter"
      @handleSelectAll="handleSelectAll"
      @handleRowDBLClick="handleRowDBLClick"
    ></tbl-temporary>

    <div
      class="empty-search"
      v-if="numberOfRecord == 0 && !isEmpty && !loadingFirst && !loadingAfter"
    >
      <img
        src="@/assets/images/Group 17947.png"
        alt="not have data"
        style="position: relative"
      />
      <span>Không tìm thấy kết quả phù hợp</span>
    </div>
    <div class="empty-case" v-if="isEmpty">
      <img
        src="@/assets/images/Group 17947.png"
        alt="not have data"
        style="position: relative"
        class="img-empty"
      />
      <!-- Button thêm mới bảng tính -->
      <div class="btn-temporary-mode">
        <m-btn
          type="btn-primary-icon"
          name="Thêm bảng tính thuế"
          :handleClick="newTemporaryTax"
        >
          <template v-slot:icon
            ><img class="icon" src="@/icons/Group 17812.png"
          /></template>
        </m-btn>
      </div>
    </div>
  </div>
</template>

<script>
import Table from "@/components/tables";
import apiTemporay from "@/api/etax/temporary";
import Button from "@/components/buttons";
import enumTaxType from "@/enums/taxType";
import periodType from "@/enums/periodType";
import actionTable from "@/enums/actionTable";

export default {
  components: {
    "m-btn": Button,
    "tbl-temporary": Table,
  },
  data() {
    return {
      pageSizes: [25, 50, 75, 100],
      useStatus: 0,
      positionSummary: {
        Summary: [
          {
            PropertyName: "",
            PropertyIndex: 0,
          },
          {
            PropertyName: "TotalTaxWithHeld",
            PropertyIndex: 6,
          },
        ],
      },
      listaction: [],
      arrayDelete: [],
      listColumn: [
        {
          prop: "TemporaryTaxName",
          label: "Tên bảng tính",
          propertyName: "TemporaryTaxName",
          width: "",
          minWidth: "300",
          align: "left",
          type: "",
          sortable: true,
        },
        {
          label: "Loại thu nhập",
          propertyName: "IncomeName",
          width: "",
          minWidth: "145",
          align: "left",
          type: "",
          prop: "IncomeName",
          sortable: true,
        },
        {
          label: "Kỳ tính thuế",
          propertyName: "TemporaryPeriod",
          width: "",
          minWidth: "130",
          align: "left",
          type: "",
          prop: "TemporaryPeriod",
          sortable: true,
        },
        {
          label: "Số người khai thuế",
          propertyName: "NumberEmployee",
          width: "",
          minWidth: "160",
          align: "right",
          type: "decimal",
          prop: "NumberEmployee",
          sortable: true,
        },
        {
          label: "Số người phải nộp thuế",
          propertyName: "NumberEmployeeTaxable",
          width: "",
          minWidth: "190",
          align: "right",
          type: "decimal",
          prop: "NumberEmployeeTaxable",
          sortable: true,
        },
        {
          label: "Thuế TNCN khấu trừ",
          propertyName: "TotalTaxWithHeld",
          tooltip: "Thuế thu nhập cá nhân khấu trừ",
          width: "",
          minWidth: "170",
          align: "right",
          type: "decimal",
          prop: "TotalTaxWithHeld",
          sortable: true,
        },
      ],
      dataTable: [], // Danh sách bảng tính thuế
      filter: {
        pageSize: 25, // Kích thước cho 1 bảng
        pageIndex: 1, // Trang chọn
        keyword: "", // Từ khóa tìm kiếm
        organizationId: "", // ID của tổ chức
        month: 0,
        year: "",
        taxType: 0,
      },
      loadingFirst: true,
      loadingAfter: true,
      isFirst: true,
      isEmpty: false,
      numberOfRecord: 0, // Tổng số bản ghi
      select: [], // hàng đã chọn
      totalSelect: 0, // tổng số hàng đã chọn
      openNoti: false, //Biến bật notifications
      titleNoti: "", // Tiêu đề truyền xuống notifications
      contentNoti: "", //Nội dung truyền xuống notifications
      typeNoti: "", // Loại thông báo của notifications
      nearestYear: "",
      modeType: "", // Trạng thái thêm mới hay Nhân bản
    };
  },
  watch: {
    //theo dõi sự thay đổi của filter
    "$store.getters.filter": {
      handler(val) {
        // Xóa nhiều bản ghi
        if (val.deleteMulti) {
          this.callDeleteMulti();
        }

        // Refresh lại bảng dữ liệu
        if (val.refresh) {
          this.getDataTemporaryTax();
          this.$store.dispatch("filter/addRefresh", false);
        }

        return Object.assign(this.filter, val);
      },
      deep: true,
    },
    filter: {
      async handler(val) {
        if (val) {
          this.getDataTemporaryTax();
        }
      },
      deep: true,
    },
  },

  methods: {
    /**
     * Hàm thực hiện set lại page index và pagesize khi chọn control pagination
     * created by vdthang 12.07.2020
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
     * Hàm bắt sự kiện select all check box table
     * created by vdthang 06.07.2020
     */
    handleSelectAll(val) {
      //chọn all => hiện button xóa
      if (val.length == 0) {
        this.$emit("isCheckAllRow", false);
      } else {
        this.$emit("isCheckAllRow", true);
      }
    },

    /**
     * Hàm thực Call service thực hiện lấy dữ liệu bảng tạm tính thuế theo kỳ
     * created by tvphi 13.07.2020
     */
    async getDataTemporaryTax() {
      this.loadingFirst = true;
      this.loadingAfter = true;
      var res = await apiTemporay.getTemporaryData(this.filter);
      this.dataTable = [];
      if (res.data && res.data.Success) {
        this.mapDataServiceToTable(res.data.Data.TemporaryTax);
        this.numberOfRecord = res.data.Data.TotalRecord;
        this.isEmpty = res.data.Data.IsEmpty;
      } else {
        this.isEmpty = true;
      }
      this.emitEmpty(res.data.Data.IsEmpty);
      this.loadingFirst = false;
      this.loadingAfter = false;
      this.isFirst = false;
    },

    /**
     * Hàm thực Chuyển đổi data lấy từ service về data trong bảng tạm tính thuế
     * created by vdthang 13.07.2020
     */
    mapDataServiceToTable(data) {
      var result = [];
      data.forEach((item) => {
        var obj = {};
        obj.TemporaryTaxName = item["TemporaryTaxName"];
        obj.IncomeName = enumTaxType.findInArray(item["IncomeType"]);
        obj.NumberEmployee = item["NumberOfEmployees"];
        obj.NumberEmployeeTaxable = item["IsTaxable"];
        obj.TotalTaxWithHeld = item["TotalTaxWithHeld"];
        obj.TemporaryPeriod = `${periodType.findInArray(
          "PERIOD_TAX",
          item["TemporaryTaxType"]
        )} ${item.PeriodMonthOrQuarter}/${item.PeriodYear}`;
        obj.DeleteStatus = item["DeleteStatus"];
        obj.TemporaryTaxID = item["TemporaryTaxID"];
        obj.CreatedDate = item["CreatedDate"];
        obj.TemporaryTaxType = item["TemporaryTaxType"];
        result.push(obj);
      });
      this.dataTable = result;
    },

    /**
     * Hàm bỏ chọn tất cả các ô check box
     * Auth: NTNgoc
     * Created date: 13/7/2020
     */
    clearAllSelection(rows) {
      if (rows) {
        rows.forEach((row) => {
          this.$refs.temporary.toggleRowSelection(row);
        });
      } else {
        this.$refs.temporary.clearSelection();
      }
    },

    /**
     * Truyền sang component để mở modal thêm mới bảng tính
     */
    newTemporaryTax() {
      this.$emit("createTemporary", true);
      this.$emit("setModeAdd", "Thêm mới");
    },

    /**
     * Hàm bắt sự kiện click vào cột check box
     * Created by: TVPhi Created date: 11/8/2020
     * @param row mảng các hàng được chọn
     */
    getRowsSelection(row) {
      this.select = row;
      //nếu chọn nhiều hơn hoặc bằng 1 thì hiện button xóa.
      if (row.length >= 1) {
        this.$emit("isCheckAllRow", true);
      } else {
        this.$emit("isCheckAllRow", false);
      }
    },

    /**
     * Hàm trả về đối tượng thực hiện action ở table
     * Author: CVCuong Created date: 6/8/2020
     * @param val.name Tên hành động thực
     * @param val.row Đối tượng thực thi hành động
     */
    getRowAction(val) {
      if (val.value === 2) {
        var content = "";
        var buttons = [];
        if (val.row.DeleteStatus > 0) {
          content =
            "Bạn không được xóa bảng tính thuế TNCN <strong>" +
            val.row.TemporaryPeriod +
            "</strong> do tờ khai khấu trừ thuế TNCN <strong>" +
            val.row.TemporaryPeriod +
            "</strong> đã nộp. Vui lòng kiểm tra lại.";
          buttons = [
            {
              text: "Đóng",
              class: "btn-primary",
              callback: this.$_Popup.resetState,
            },
          ];
          this.$_Popup.warn("Cảnh báo", content, buttons);
        } else {
          this.arrayDelete = [];
          this.arrayDelete.push(val.row);
          content =
            "<strong>Tờ khai thuế TNCN " +
            val.row.TemporaryPeriod +
            " sẽ bị xóa cùng bảng tính thuế TNCN " +
            val.row.TemporaryPeriod +
            "</strong>. Bạn có chắc chắn muốn xóa không?";
          buttons = [
            {
              text: "Không",
              class: "btn-grey",
              callback: this.$_Popup.resetState,
            },
            {
              text: "Có",
              class: "btn-delete",
              callback: this.deleteOnlyRow,
            },
          ];
          //Gọi dialog hỏi người dùng có muốn xóa hay không
          this.$_Popup.delete("Xóa bảng tạm tính", content, buttons);
        }
      }
      // Emit lên cha khi ấn vào button Nhân bản
      else if (val.value == 0) {
        this.$emit("setTemporaryTax", val.row); // truyền xuống bảng cần nhân bản
        this.$emit("createTemporary", true);
        this.$emit("setModeAdd", "Nhân bản"); // truyền xuống mode
      }
      // gọi hàm dblClick khi nhấn vào sửa
      else if (val.value == 1) {
        this.handleRowDBLClick(val.row);
      }
    },

    /**
     * Hàm phụ trợ gọi đến hàm xóa để xóa 1 đối tượng
     * Author: CVCuong Created date: 8/8/2020
     */
    deleteOnlyRow() {
      this.deleteTemporaryTax(this.arrayDelete);
    },

    /**
     * Hàm xóa nhiều đối tượng
     * Author: CVCuong Created date: 8/8/2020
     */
    callDeleteMulti() {
      this.$store.dispatch("filter/addDeleteMulti", false);
      var content = "";
      var buttons = [];
      var isDelete = true;
      this.select.forEach((element) => {
        if (element.DeleteStatus > 0) {
          isDelete = false;
          content =
            "Bạn không được xóa bảng tính thuế TNCN <strong>" +
            element.TemporaryPeriod +
            "</strong> do tờ khai khấu trừ thuế TNCN <strong>" +
            element.TemporaryPeriod +
            "</strong> đã nộp. Vui lòng kiểm tra lại.";
          buttons = [
            {
              text: "Đóng",
              class: "btn-primary",
              callback: this.$_Popup.resetState,
            },
          ];
        }
      });
      if (isDelete == true) {
        if (this.select.length == 1)
          content =
            "<strong>Tờ khai thuế TNCN " +
            this.select[0].TemporaryPeriod +
            " sẽ bị xóa cùng bảng tính thuế TNCN " +
            this.select[0].TemporaryPeriod +
            "</strong>. Bạn có chắc chắn muốn xóa không?";
        else
          content =
            "Các tờ khai thuế TNCN sẽ bị xóa cùng các bảng tính thuế TNCN đã chọn. Bạn có chắc chắn muốn xóa không?";
        buttons = [
          {
            text: "Không",
            class: "btn-grey",
            callback: this.$_Popup.resetState,
          },
          {
            text: "Có",
            class: "btn-delete",
            callback: this.deleteMultiRow,
          },
        ];
        //Gọi dialog hỏi người dùng có muốn xóa hay không
        this.$_Popup.delete("Xóa bảng tạm tính", content, buttons);
      } else {
        this.$_Popup.warn("Cảnh báo", content, buttons);
      }
    },

    /**
     * Hàm phụ trợ gọi đến hàm xóa để xóa 1 đối tượng
     * Author: CVCuong Created date: 8/8/2020
     */
    deleteMultiRow() {
      this.deleteTemporaryTax(this.select);
    },

    /**
     * Hàm thêm CSS cho table => tạo hiệu ứng khi tick/bỏ tích vào checkbox
     */
    setStyle() {
      if (this.select.length > 0) {
        return {
          transform: "translateY(0)",
          transition: " all .2s",
          height: "calc(100% - 53px)",
        };
      } else {
        return {
          transform: "translateY(-36px)",
          transition: " all .2s",
          height: "calc(100%)",
        };
      }
    },

    /**
     * Hàm xóa bảng tạm tính
     * Auth: CVCuong
     * @param {val bool chứa giá trị đồng ý xóa hay không}
     * Created date: 14/07/2020
     */
    async deleteTemporaryTax(val) {
      var data = [];
      val.forEach((element) => {
        if (element.DeleteStatus <= 0) {
          //Thêm vào mảng các đối tượng được phép xóa
          data.push(element.TemporaryTaxID);
        }
      });
      //Gọi service xóa các hàng được phép xóa
      var res = await apiTemporay.deleteTemporaryTax(
        data,
        this.filter.organizationId
      );
      if (res.data.Success == false) {
        this.$_Notification.error(
          "Thất bại",
          "Không thể xóa bảng tạm tính. " + res.data.Message
        );
      } else {
        if (res.data.Data == 0) {
          this.$_Notification.error(
            "Thất bại",
            "Không thể xóa bảng tạm tính. " + res.data.Message
          );
        } else {
          this.$_Notification.success(
            "Thành công",
            "Bạn đã xóa thành công bảng tạm tính"
          );
        }
      }

      // load lại dữ liệu bảng tạm tính
      this.getDataTemporaryTax();
    },

    /**
     * Check có hàng nào đang chọn hay không
     * created by vdthang 12/07/2020
     */
    checkRow() {
      if (typeof this.select != "undefined" && this.select.length > 0) {
        this.totalSelect = this.select.length;
        return true;
      } else return false;
    },

    /**
     * Sử kiện double click vào 1 row trong hàng
     * created by vdthang 12/07/2020
     * modified by: N.T. Dũng 14.07.2020
     * Modified by: CVCuong 29/7/2020: Tạm thời tắt tính năng DBCLICK
     */
    handleRowDBLClick(val) {
      // lấy data từ row truyền vào state
      this.$store.commit("taxPeriod/setData", val);
      this.$store.commit("taxPeriod/setTaxInfo", val);
      // check đã tồn tại sessionStorage chưa, có thì xóa
      if (sessionStorage.getItem("objectName") !== null) {
        sessionStorage.clear();
      }
      // tạo sessionStorage
      sessionStorage.setItem("objectName", val.TemporaryTaxName);
      this.$router.push({
        name: "TaxPeriodDetail",
        params: { id: val.TemporaryTaxID, mode: 1 },
      });
    },
  },

  /**
   * Hàm chạy khi khởi tạo Instance
   */
  created() {
    Object.assign(this.filter, this.$store.getters.filter);
    this.$emit("isCheckAllRow", false);
    this.listaction = actionTable.TEMPORARY_TAX;
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
.tool-bar .button-container {
  float: left;
}

.temporary-mode {
  position: relative;

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

  .tbl-order-normal {
    .el-table__body-wrapper {
      height: calc(100% - 100px);
    }
  }

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
      width: 85%;
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

    .btn-temporary-mode {
      margin-top: 8px;
    }
  }
}

@media only screen and (max-width: 1366px) {
  .temporary-mode{
    .empty-search{
      top: 40%;

      img{
        width: 50%;
      }
    }

    .empty-case{
      top: 40%;

      .img-empty{
        width: 85%;
      }
    }
  }
}
</style>
