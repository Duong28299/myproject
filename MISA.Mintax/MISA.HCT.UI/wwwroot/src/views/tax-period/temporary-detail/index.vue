<template>
  <div class="temporary-detail" style="height: 100%">
    <!-- phần page title -->
    <PageTitle :haveData="true">
      <div v-if="!isEmpty" style="position: absolute; right: 24px">
        <m-btn
          type="btn-white-onlyicon"
          tooltip="Tải về"
          :handleClick="openExport"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Component 95 – 1.png" />
          </template>
        </m-btn>
        <m-btn
          class="mr-8 fl"
          :type="'btn-white-icon'"
          :name="'Xem tờ khai'"
          style="margin-right: 16px"
          :handleClick="openDecaleration"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/Group 18561.png" />
          </template>
        </m-btn>
      </div>
    </PageTitle>
    <!-- /phần page title -->

    <el-row
      v-if="!isEmpty && !loadingFirst && !loadingAfter"
      style="margin-top: 24px"
    >
      <el-col :span="16">
        <!-- vùng tìm kiếm -->
        <fieldinput
          class="search-filter"
          type="input-icon"
          :placeholder="'Tìm kiếm theo họ và tên, mã nhân viên, mã số thuế cá nhân'"
          classicon="icon-search"
          width="415px"
          :valueInput="filter.keyword"
          @value="filter.keyword = $event"
        />
        <!-- /vùng tìm kiếm -->
      </el-col>
      <!-- button xóa -->
      <div v-if="isCheck">
        <m-btn
          type="btn-white3-onlyicon"
          :iconel="'el-icon-delete'"
          tooltip="Xóa"
          :handleClick="callDeleteMulti"
        ></m-btn>
      </div>
      <!-- /button xóa -->

      <!-- button thêm mới người nộp thuế -->
      <m-btn
        v-else
        type="btn-primary-icon"
        name="Thêm người nộp thuế"
        :handleClick="addEmployee"
      >
        <template v-slot:icon>
          <img class="icon" src="@/icons/Group 17812.png" />
        </template>
      </m-btn>
      <!-- /button thêm mới người nộp thuế -->
    </el-row>

    <!-- Vùng hiển thị table -->
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
      ref="temporaryDetail"
      class="tbl-order-normal tbl-temporary-tax-detail"
      :listColumn="listColumn"
      :dataTable="dataTable"
      :haveCheck="haveCheck"
      :summary="true"
      :havePagination="true"
      :total="numberOfRecord"
      :layoutPaging="layoutPaging"
      :loading="false"
      :positionSummary="positionSummary"
      :tooltip="true"
      :haveAction="true"
      :listaction="listaction"
      typepage="người nộp thuế"
      @row-action="getRowAction"
      @pagination="getFilter"
      @selection="getSelection"
      @handleSelectionChange="handleSelectionChange"
      @handleRowDBLClick="handleRowDBLClick"
    />
    <!-- /vùng hiển thị table -->

    <!--Hiển thị thông báo không tìm thấy người nộp thuế-->
    <div
      class="empty-search"
      v-if="numberOfRecord == 0 && !isEmpty && !loadingFirst && !loadingAfter"
    >
      <img
        src="@/assets/images/Group 18279.png"
        alt="not have data"
        style="position: relative; margin-bottom: 16px"
      />
      <div>Không tìm thấy kết quả phù hợp</div>
    </div>
    <!--/Hiển thị thông báo không tìm thấy người nộp thuế-->

    <!-- Hiển thị button thêm mới khi danh sách người nộp thuế trống -->
    <div class="empty-case" v-if="isEmpty && !loadingFirst && !loadingFirst">
      <img
        src="@/assets/images/Group 18279.png"
        alt="not have data"
        style="position: relative; margin-bottom: 16px"
        class="img-empty"
      />
      <!-- Button thêm mới người lao động -->
      <div class="btn-temporary-mode">
        <m-btn
          :type="'multi-button'"
          :name="'Thêm người nộp thuế'"
          :dataDropdown="optionAddEmployee"
          :handleClick="addEmployee"
          :customWidth="'custom-width-multi-button'"
        >
          <template v-slot:icon>
            <img class="icon" src="@/icons/ico-AddUser@2x.png" />
          </template>
        </m-btn>
      </div>
      <!-- /Button thêm mới người lao động -->
    </div>
    <!-- /Hiển thị button thêm mới khi danh sách người nộp thuế trống -->

    <!-- Vùng hiển thị Dialog xem chi tiết tờ khai -->
    <dialog-sample-declaration
      :dialogFormVisible="dialogSampleDeclaration"
      @close-dialog="closeDialogDeclaration"
      :declarationData="declarationData"
    ></dialog-sample-declaration>
    <!-- /vùng hiển thị dialog xem tờ khai -->

    <!-- Vùng hiển thị Dialog thêm mới người lao động -->
    <dialog-new-employee
      :dialogFormVisible="dialogNewEmployee"
      @close-dialog="closeDialogNewEmployee"
      :title="EmployeeTitle"
      :employeeProp="EmployeeObj"
      :status="EmployeeStatus"
      :handleClickProp="refreshData"
    ></dialog-new-employee>
    <!-- /vùng hiển thị dialog thêm mới người lao động -->
  </div>
</template>

<script>
import PageTitle from "@/layout/main/components/page-title";
import fieldinput from "@/components/inputs";
import Button from "@/components/buttons";
import Table from "@/components/tables";
import SampleDeclaration from "@/views/tax-period/declaration-mode/components/sample-declaration.vue";
import NewEmployee from "@/views/tax-period/temporary-detail/components/new-employee.vue";
import DeclarationService from "@/api/etax/declaration.js";
import apiTemporayTaxDetail from "@/api/etax/temporary-tax-detail";
import EmployeeInfo from "@/api/etax/temporary-tax-detail";
import actionTable from "@/enums/actionTable";

export default {
  name: "TaxPeriodDetail",
  components: {
    "m-btn": Button,
    "tbl-temporary": Table,
    "dialog-sample-declaration": SampleDeclaration,
    "dialog-new-employee": NewEmployee,
    PageTitle,
    fieldinput,
  },
  data() {
    return {
      optionAddEmployee: [
        // tùy chọn cho multi-button thêm mới
        { icon: "", name: "Nhập khẩu" },
        { icon: "", name: "Đồng bộ từ AMIS" },
      ],
      title: "", // title page
      positionSummary: {
        // object chứa các trường tính tổng
        Summary: [
          {
            PropertyName: "",
            PropertyIndex: 0,
          },
          {
            PropertyName: "IncomeTaxable",
            PropertyIndex: 4,
          },
          {
            PropertyName: "TotalReduction",
            PropertyIndex: 5,
          },
          {
            PropertyName: "InsurancePayment",
            PropertyIndex: 6,
          },
          {
            PropertyName: "TotalTaxWithHeld",
            PropertyIndex: 7,
          },
          {
            PropertyName: "TotalIncomeTaxable",
            PropertyIndex: 8,
          },
        ],
      },
      listaction: [],
      checkAllStatus: true, // biến dùng để ẩn phần button thêm mới khi check all
      buttonView: true, // biến dùng để ẩn button xem bảng tạm tính
      listColumn: [
        {
          label: "Mã nhân viên",
          propertyName: "EmployeeCode",
          width: "",
          minWidth: "135",
          align: "left",
          type: "",
          prop: "EmployeeCode",
          sortable: true,
        },
        {
          label: "Họ và tên",
          propertyName: "FullName",
          width: "",
          minWidth: "160",
          align: "left",
          type: "",
          prop: "FullName",
          sortable: true,
        },
        {
          label: "Bộ phận/Phòng ban",
          propertyName: "OrganizationUnitName",
          width: "",
          minWidth: "170",
          align: "left",
          type: "",
          prop: "PositionName",
          sortable: true,
        },
        {
          label: "Thu nhập chịu thuế",
          propertyName: "IncomeTaxable",
          width: "",
          minWidth: "170",
          align: "right",
          type: "decimal",
          sortable: true,
          prop: "IncomeTaxable",
        },
        {
          label: "Tổng GTGC",
          propertyName: "TotalReduction",
          width: "",
          minWidth: "130",
          align: "right",
          type: "decimal",
          sortable: true,
          tooltip: "Tổng giảm trừ gia cảnh",
          prop: "TotalReduction",
        },
        {
          label: "Bảo hiểm phải nộp",
          propertyName: "InsurancePayment",
          width: "",
          minWidth: "165",
          align: "right",
          type: "decimal",
          sortable: true,
          prop: "InsurancePayment",
        },
        {
          label: "Thuế TNCN khấu trừ",
          propertyName: "TotalTaxWithHeld",
          width: "",
          minWidth: "175",
          align: "right",
          type: "decimal",
          sortable: true,
          tooltip: "Thuế thu nhập cá nhân khấu trừ",
          prop: "TotalTaxWithHeld",
        },
        {
          label: "Thu nhập tính thuế",
          propertyName: "TotalIncomeTaxable",
          width: "",
          minWidth: "175",
          align: "right",
          type: "decimal",
          sortable: true,
          prop: "TotalIncomeTaxable",
        },
      ],
      dataTable: [], // list chứa data được gọi từ service
      filter: {
        pageSize: 25, // Kích thước cho 1 bảng
        pageIndex: 1, // Trang chọn
        keyword: "", // Từ khóa tìm kiếm
        organizationId: "", // ID của tổ chức
        temporaryTaxID: "", // id bảng tạm tính test in advance
      },
      haveCheck: true, // kiểm tra trạng thái checkbox
      numberOfRecord: 0, // tổng số bản ghi
      layoutPaging: " prev, pager, next, sizes", // phân trang
      selectionObject: [], // row đã chọn
      listOrganization: [],
      loadingFirst: true,
      loadingAfter: true,
      isFirst: true,
      isEmpty: false,
      dataDropdown: [
        // danh mục của nút thêm mới
        { icon: "", name: "Nhập khẩu" },
        { icon: "", name: "Thêm mới người nộp thuế", nameVal: "addEmployee" },
        { icon: "", name: "Đồng bộ từ AMIS hồ sơ" },
      ],
      dialogVisible: false, // trạng thái hiển thị modal thêm mới
      input: "", // theo dõi khi nhập dữ liệu
      organizationId: "", // ID của tổ chức
      dependentStatus: false, // ẩn hiện phần kê khai thông tin người phụ thuộc
      isCheck: false, // trạng thái checkbox
      dialogSampleDeclaration: false, // Bật hoặc tắt dialog
      dialogNewEmployee: false, // Bật hoặc tắt dialog người nộp thuế
      declarationData: {}, // Thông tin của tờ khai thuế
      contentPopup: "", //Nội dung truyền vào Popup
      buttonsPopup: [], //Mảng button truyền vào Popup,
      EmployeeTitle: "", // title thêm người lao động
      EmployeeObj: {}, // Object employee để chỉnh sửa
      EmployeeStatus: "", // status employee để chỉnh sửa
      ObjectDelete: [], //Đối tượng xóa ở action table
    };
  },

  computed: {
    /**
     * Tính tổng thu nhập
     * created by vdthang 27.11.2020
     */
    totalIncome() {
      return this.personalIncomeTax + this.personalNotIncomeTax;
    },
  },

  watch: {
    /**
     * load dữ liệu & cập nhật lại dữ liệu chi tiết bảng tạm tính khi click vào số trang
     * created by: N.T. Dũng 14.07.2020
     */
    filter: {
      handler(val) {
        if (val) {
          this.getDataTemporaryDetail();
        }
      },
      deep: true,
    },
  },

  methods: {
    /**
     * method gọi refresh table khi thêm, sửa người lao động
     * created by : NVHung 8.8.2020
     */
    refreshData() {
      this.getDataTemporaryDetail();
    },

    /**
     * method gọi service lấy dữ liệu chi tiết bảng tạm tính
     * created by: N.T. Dũng 14.07.2020
     */
    async getDataTemporaryDetail() {
      this.loadingFirst = true;
      this.loadingAfter = true;
      var res = await apiTemporayTaxDetail.getTemporaryDetailData(this.filter);
      this.dataTable = [];
      if (res.data && res.data.Success) {
        this.dataTable = res.data.Data.TemporaryTaxDetail;
        this.numberOfRecord = res.data.Data.TotalRecord;
        this.isEmpty = res.data.Data.IsEmpty;
      } else {
        this.numberOfRecord = 0;
      }
      this.loadingFirst = false;
      this.loadingAfter = false;
      this.isFirst = false;
    },

    /**
     * Mở dialog xem chi tiết tờ khai thuế
     * created by: nhdoanh (11/7/2020)
     * modified by: tvphi (17/7/2020)
     *
     */
    async openDecaleration() {
      // Lấy dữ liệu thông tin tờ khai thuế từ service
      this.dialogSampleDeclaration = true;
      var res = await DeclarationService.getDeclarationTemporaryTaxID(
        this.$route.params.id,
        this.organizationId
      );
      if (
        res.data.Success === true &&
        res.data.Data !== null &&
        res.data.Data.DeclarationPartials != null
      ) {
        if (res.data.Data.DeclarationPartials[0].NumberOfEmployee > 0) {
          this.declarationData = res.data.Data;
          // Mở dialog xem chi tiết tờ khai thuế
        } else {
          this.contentPopup =
            "Không có dữ liệu tờ khai, vui lòng kiếm tra lại.";
          this.buttonsPopup = [
            {
              text: "Đóng",
              class: "btn-primary",
              callback: () => {},
            },
          ];
          this.$_Popup.warn("Cảnh báo", this.contentPopup, this.buttonsPopup);
        }
      } else {
        this.contentPopup = "Không có dữ liệu tờ khai, vui lòng kiếm tra lại.";
        this.buttonsPopup = [
          {
            text: "Đóng",
            class: "btn-primary",
            callback: () => {},
          },
        ];
        this.$_Popup.warn("Cảnh báo", this.contentPopup, this.buttonsPopup);
      }
    },

    /**
     * method xử lý phần xuất khẩu
     */
    openExport() {
      this.contentPopup = "Chức năng đang phát triển.";
      this.buttonsPopup = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];
      this.$_Popup.info("Cảnh báo", this.contentPopup, this.buttonsPopup);
    },

    /**
     * Check có hàng nào đang chọn hay không
     * created by: N.T. Dũng 08.07.2020
     */
    checkRow() {
      if (this.selectionObject.length > 0) {
        this.isCheck = true;
      } else {
        this.isCheck = false;
      }
    },

    /**
     * lấy dữ liệu khi click vào số trang
     * created by: N.T. Dũng 09.07.2020
     */
    getFilter(val) {
      this.filter.pageSize = val.limit;
      this.filter.pageIndex = val.page;
    },

    /**
     * Hàm thực hiện khi chọn check box bản ghi
     * created by vdthang 27.11.2020
     */
    getSelection(val) {
      this.selectionObject = val;
    },

    /**
     * Hàm bỏ chọn tất cả các ô check box
     * Auth: NTNgoc
     * Created date: 13/7/2020
     */
    clearAllSelection(rows) {
      if (rows) {
        rows.forEach((row) => {
          this.$refs.temporaryDetail.toggleRowSelection(row);
        });
      } else {
        this.$refs.temporaryDetail.clearSelection();
      }
    },

    /**
     * Hàm trả về đối tượng thực hiện action ở table
     * Author: CVCuong Created date: 6/8/2020
     * @param val.name Tên hành động thực
     * @param val.row Đối tượng thực thi hành động
     * Modified by: N.T. Dũng - 6.8.2020 (thêm chức năng sửa)
     */
    getRowAction(val) {
      if (val.value === 1) {
        this.ObjectDelete = [];
        this.ObjectDelete.push(val.row);
        var content =
          "Bạn có chắc chắn muốn xóa người nộp thuế <strong>" +
          val.row.FullName +
          "</strong>?";
        var buttons = [
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
        this.$_Popup.delete("Xóa người lao động", content, buttons);
      }
      // Nơi xử lý sửa thông tin người nộp thuế
      else if (val.value === 0) {
        this.callUpdateEmployee(val);
      }
    },

    /**
     * Gọi service và xử lý sửa người lao động
     */
    callUpdateEmployee(val) {
      // Gọi và truyền dữ liệu qua popup sửa người lao động
      this.EmployeeTitle = "CẬP NHẬT NGƯỜI NỘP THUẾ";
      this.getInfoEmployee(val.row.TemporaryTaxDetailID).then((res) => {
        this.EmployeeObj = { ...res.data.Data };
      });
      this.EmployeeStatus = "1";
      setTimeout(() => {
        this.dialogNewEmployee = true;
      }, 100);
    },

    /**
     * Hàm phụ trợ xóa người nộp thuế ở action table
     * Auth: CVCuong
     * Created date: 8/8/2020
     */
    deleteOnlyRow() {
      this.deleteTemporaryTaxDetail(this.ObjectDelete);
    },

    /**
     * Hàm xóa nhiều người nộp thuế
     * Auth: CVCuong
     * @param {val bool chứa giá trị đồng ý xóa hay không}
     * Created date: 14/07/2020
     */
    callDeleteMulti() {
      this.buttonsPopup = [
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
        {
          text: "Có",
          class: "btn-delete",
          callback: this.deleteMultiRow,
        },
      ];
      if (this.selectionObject.length == 1)
        this.contentPopup =
          "Bạn có chắc chắn muốn xóa người nộp thuế <strong>" +
          this.selectionObject[0].FullName +
          "</strong>?";
      else
        this.contentPopup =
          "Bạn có chắc chắn muốn xóa người nộp thuế đã chọn trong danh sách?";
      this.$_Popup.delete(
        "Xóa người lao động",
        this.contentPopup,
        this.buttonsPopup
      );
    },

    /**
     * Hàm phụ trợ xóa nhiều người nộp thuế
     * Auth: CVCuong
     * Created date: 8/8/2020
     */
    deleteMultiRow() {
      this.deleteTemporaryTaxDetail(this.selectionObject);
    },

    /**
     * Hàm xóa người nộp thuế
     * Auth: CVCuong
     * @param {val bool chứa giá trị đồng ý xóa hay không}
     * Created date: 14/07/2020
     */
    async deleteTemporaryTaxDetail(val) {
      var data = [];
      val.forEach((element) => {
        data.push(element.TemporaryTaxDetailID);
      });
      var res = await apiTemporayTaxDetail.deleteTemporaryTaxDetail(data);
      if (res.data.Data[0] == 1) {
        if (res.data.Success == true) {
          this.$_Notification.success(
            "Thành công",
            "Bạn đã xóa thành công người nộp thuế."
          );
        } else {
          this.$_Notification.warning(
            "Cảnh báo",
            "Đã xóa người nộp thuế, nhưng có lỗi trong hệ thống cần kiểm tra lại."
          );
        }
      } else {
        this.$_Notification.error(
          "Thất bại",
          "Không thể xóa người nộp thuế. Vui lòng liên hệ MISA để nhận hỗ trợ."
        );
      }

      //Gọi service load lại bảng tạm tính chi tiết
      this.getDataTemporaryDetail();
    },

    /**
     * Hàm bắt sự kiện select checkbox từng hàng
     * created by: vdthang 06.07.2020
     * modified by: N.T. Dũng 07.07.2020
     */
    handleSelectionChange(val) {
      this.selectionObject = val;
      this.checkRow();
    },

    /**
     * Hàm đóng dialog xem chi tiết tờ khai thuế
     * created by nhdoanh 10.07.2020
     */
    closeDialogDeclaration(val) {
      this.dialogSampleDeclaration = val;
    },

    /**
     * Hàm đóng dialog thêm người lao động
     * created by nvhung 6.8.2020
     */
    closeDialogNewEmployee() {
      this.dialogNewEmployee = false;
    },

    /**
     * Hàm thêm mới người nộp thuế
     * created by nvhung 6.8.2020
     */
    addEmployee() {
      this.EmployeeTitle = "THÊM MỚI NGƯỜI NỘP THUẾ";
      this.EmployeeStatus = "0";
      // Reset đối tượng người nộp thuế
      this.EmployeeObj = {
        BankAccountName: null,
        BankAccountNo: null,
        BankID: null,
        BankName: null,
        BankBranch: null,
        CitizenIdentityDate: null,
        CitizenIdentityNo: null,
        IndentityType: null,
        CitizenIdentityPlaceID: null,
        ContractOffical: 1,
        DateOfBirth: null,
        DependentNumber: 0,
        DependentReduction: 0,
        Email: "",
        EmployeeCode: "",
        EmployeeID: null,
        EntityState: 0,
        EthnicID: null,
        FirstName: null,
        FullName: null,
        Gender: 0,
        IncomeNotTaxable: 0,
        IncomeTaxable: 0,
        IndividualResident: 1,
        InsurancePayment: 0,
        IsTaxable: 1,
        LastName: null,
        NationalityID: null,
        OrganizationID: "2ef7003b-c2a3-11ea-9491-5203cfc96bc9",
        OrganizationUnitName: null,
        OrganizationUnitID: null,
        OtherReduction: 0,
        Phone: "",
        PositionName: null,
        RepresentFor: null,
        SelfReduction: 0,
        TariffsApply: "",
        TaxInfoID: 0,
        TaxNo: "",
        TemporaryTaxID: "",
        TotalIncome: 0,
        TotalIncomeTaxable: 0,
        TotalReduction: 0,
        TotalTaxWithHeld: 0,
        Confirm: 0,
      };
      this.dialogNewEmployee = true;
    },

    /**
     * lấy thông tin của nhân viên nộp thuế cần sửa từ service trả về
     * created by: N.T. Dũng 20.07.2020
     */
    async getInfoEmployee(idDetail) {
      return await EmployeeInfo.getTemporaryDetailDataByID(idDetail);
    },

    /**
     * hiển thị form xem chi tiết khi double click vào row
     */
    handleRowDBLClick(val) {
      // Gọi và truyền dữ liệu qua popup sửa người lao động
      this.EmployeeTitle = "CẬP NHẬT NGƯỜI NỘP THUẾ";
      this.getInfoEmployee(val.TemporaryTaxDetailID).then((res) => {
        this.EmployeeObj = { ...res.data.Data };
      });
      this.EmployeeStatus = "1";
      this.dialogNewEmployee = true;
    },
  },

  /**
   * Hàm khởi tạo instance khi mở trang chi tiết bảng tạm tính
   * created by vdthang 27.11.2020
   */
  async created() {
    this.filter.temporaryTaxID = this.$route.params.id;
    this.title = sessionStorage.getItem("objectName");
    this.listaction = actionTable.TEMPORARY_TAX_DETAIL;
  },
};
</script>

<style lang="scss" scoped>
.temporary-detail {
  .empty-search {
    position: absolute;
    top: 54%;
    left: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    transform: translate(-50%, -50%);
  }

  .empty-case {
    position: absolute;
    top: 50%;
    left: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    transform: translate(-50%, -50%);
  }

  .tbl-temporary-tax-detail {
    margin-top: 16px;
    height: calc(100% - 136px) !important;
  }
}
.input-icon {
  width: 300px;
}
// chỉnh cách mỗi dòng
.el-row {
  margin-bottom: 0px;
  align-items: center;
}
.moneyType {
  position: absolute;
  top: 10px;
  right: 25px;
}
.personal-info {
  .el-row {
    height: 40px;
  }
}

.btn-temporary-mode {
  margin-top: 16px;
}
.section-radio {
  margin-top: 8px;
}
.dialog-content {
  width: 100%;
  height: 635px;
  overflow: auto;
}
.content_dialog-content {
  width: 100%;
}
.temporary-detail {
  position: relative;
  height: 100%;
}
</style>

<style lang="scss">
.temporary-detail {
  .tbl-order-normal {
    .el-table__body-wrapper {
      height: calc(100% - 100px) !important;
    }
  }

  .el-loading-parent--relative {
    position: absolute !important;
    top: 56px;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 99999999;

    .el-loading-mask {
      opacity: 1;
    }
  }
}
</style>
