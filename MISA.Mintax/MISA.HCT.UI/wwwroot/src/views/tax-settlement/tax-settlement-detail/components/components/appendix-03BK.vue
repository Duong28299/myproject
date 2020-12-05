<template>
  <div id="list-dependent">
    <el-row>
      <el-col :span="24">
        <h2 class="title-03BK">
          BẢNG KÊ THÔNG TIN NGƯỜI PHỤ THUỘC GIẢM TRỪ GIA CẢNH
        </h2>
      </el-col>
    </el-row>
    <div>
      <el-row>
        <el-col :span="15" class="input-search">
          <field-input
            type="input-icon"
            classicon="icon-search"
            :placeholder="'Tìm kiếm theo họ tên người nộp thuế/người phụ thuộc, mã số thuế người nộp thuế/người phụ thuộc'"
            @value="filterBar.keyword = $event"
            :valueInput="filterBar.keyword"
            :clearable="true"
          ></field-input>
        </el-col>
        <el-col :span="9" class="currency-unit">
          <p>Đơn vị tiền tệ: VNĐ</p>
        </el-col>
      </el-row>
    </div>
    <div id="dependent-table" v-if="!isEmpty">
      <div v-if="loading" v-loading="loading"></div>
      <tbl
        :typeTable="'tableData'"
        :style="setStyle()"
        :listColumn="listColumn"
        :dataTable="dataTable"
        :summary="false"
        :havePagination="true"
        :total="numberOfRecord"
        :loading="false"
        :tooltip="true"
        :haveAction="false"
        :haveAdjust="false"
        :something="false"
        :pageSizes="pageSizes"
        @pagination="getFilter"
        typepage="bảng tính thuế"
      />
    </div>
    <div
      class="temporary-mode-btn"
      v-if="numberOfRecord == 0 && isEmpty == false && !loading"
    >
      <img
        src="@/assets/images/Group 18279.png"
        alt="not have data"
        style="position: relative"
      />
      <span>Không tìm thấy kết quả phù hợp</span>
    </div>
    <!-- IF Data Table rỗng -->
    <div class="empty-content" v-if="isEmpty">
      <img style="width: 40%" src="@/assets/images/Group 18279.png" /><br />
      <p>Hiện chưa có người phụ thuộc.</p>
    </div>
    <!-- /IF Data Table rỗng -->
  </div>
</template>

<script>
import fieldinput from "@/components/inputs";
import tbl from "@/components/tables";
import declarationSettlementDetailDependent from "@/api/etax/declaration_settlementDetail_dependent.js";
import relation from "@/enums/relation.js";
export default {
  name: "appendix-03BK",
  components: {
    tbl,
    "field-input": fieldinput,
  },
  data() {
    return {
      numberOfRecord: 0,
      loading: true,
      isEmpty: false,
      indexs: {
        index: "[06]",
        EmployeeFullName: "[07]",
        EmployeeTaxNo: "[08]",
        DependentName: "[09]",
        DateOfBirth: "[10]",
        DependentTaxNo: "[11]",
        NationalityName: "[12]",
        CitizenIdentityNo: "[13]",
        RelationID: "[14]",
        BirthCertificateNumber: "[15]",
        BirthCertificateBook: "[16]",
        BirthCertificateNationalityName: "[17]",
        BirthCertificateProvinceName: "[18]",
        BirthCertificateDistrictName: "[19]",
        BirthCertificateWardName: "[20]",
        StartDependentDate: "[21]",
        FinishDependentDate: "[22]",
      },
      getAddress: 0,
      listNationalites: [],
      listProvincials: [],
      listDistricts: [],
      listWard: [],
      pageSizes: [25, 50, 75, 100],
      layoutPaging: "prev, pager, next, sizes",
      filterBar: {
        keyword: "",
        pageIndex: 1,
        pageSize: 25,
        declarationsettlementID: "",
      },
      dataTable: [],
      listColumn: [
        {
          label: "Họ và tên người nộp thuế",
          propertyName: "EmployeeFullName",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "EmployeeFullName",
          sortable: false,
        },
        {
          label: "Mã số người nộp thuế",
          propertyName: "EmployeeTaxNo",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "EmployeeTaxNo",
          sortable: false,
        },
        {
          label: "Họ và tên người phụ thuộc",
          propertyName: "DependentName",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "DependentName",
          sortable: false,
        },
        {
          label: "Ngày sinh người phụ thuộc",
          propertyName: "DateOfBirth",
          align: "left",
          width: "",
          minWidth: "140",
          type: "date",
          prop: "DateOfBirth",
          sortable: false,
        },
        {
          label: "Mã số thuế người phụ thuộc",
          propertyName: "DependentTaxNo",
          align: "center",
          width: "",
          minWidth: "140",
          type: "",
          prop: "DependentTaxNo",
          sortable: false,
        },
        {
          label: "Quốc tịch người phụ thuộc",
          propertyName: "NationalityName",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "NationalityName",
          sortable: false,
        },
        {
          label: "CMND/CCCD/Hộ chiếu người phụ thuộc",
          propertyName: "CitizenIdentityNo",
          align: "center",
          width: "",
          minWidth: "140",
          type: "",
          prop: "CitizenIdentityNo",
          sortable: false,
        },
        {
          label: "Quan hệ với người nộp thuế",
          propertyName: "RelationID",
          align: "left",
          width: "",
          minWidth: "140",
          type: "",
          prop: "RelationID",
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
                  label: "Tỉnh/Thành phố",
                  propertyName: "BirthCertificateProvinceName",
                  align: "left",
                  width: "",
                  minWidth: "140",
                  type: "",
                  prop: "BirthCertificateProvinceName",
                  sortable: false,
                },
                {
                  label: "Quận huyện",
                  propertyName: "BirthCertificateDistrictName",
                  align: "left",
                  width: "",
                  minWidth: "140",
                  type: "",
                  prop: "BirthCertificateDistrictName",
                  sortable: false,
                },
                {
                  label: "Phường xã",
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
              align: "left",
              width: "",
              minWidth: "140",
              type: "month",
              prop: "StartDependentDate",
              sortable: false,
            },
            {
              label: "Đến tháng",
              propertyName: "FinishDependentDate",
              align: "left",
              width: "",
              minWidth: "140",
              type: "month",
              prop: "FinishDependentDate",
              sortable: false,
            },
          ],
        },
      ],
    };
  },
  created() {
    this.filterBar.declarationsettlementID = this.$route.params.id;
  },
  methods: {
    setStyle() {
      return { height: "calc(100vh - 177px)" };
    },
    getFilter(val) {
      this.filterBar.pageSize = val.limit;
      this.filterBar.pageIndex = val.page;
      this.getData();
    },
    /**
     * Lấy dữ liệu cho phụ lục 05-03BK
     * created by tcduong 24.10.2020
     */
    async getData() {
      var res = await declarationSettlementDetailDependent.getDeclarationSettlementDetailDependent(
        this.filterBar
      );
      if (res.data.Success && !res.data.Data.IsEmpty) {
        this.mapData(res.data.Data.DeclarationSettlementDetailDependents);
        this.isEmpty = res.data.Data.IsEmpty;
        this.loading = false;
        this.numberOfRecord =
          res.data.Data.TotalDeclarationSettlementDetailDependent;
      } else {
        this.isEmpty = true;
        this.dataTable = [];
        this.loading = false;
      }
    },
    /**
     * Ma dữ liệu vào table
     * created by tcduong 24.10.2020
     */
    mapData(val) {
      var datas = [];
      val.forEach((item) => {
        var data = {};
        data.EmployeeFullName = item["EmployeeFullName"];
        data.EmployeeTaxNo = item["EmployeeTaxNo"];
        data.DependentName = item["DependentName"];
        data.DateOfBirth = item["DateOfBirth"];
        data.DependentTaxNo = item["DependentTaxNo"];
        data.NationalityName = item["NationalityName"];
        data.CitizenIdentityNo = item["CitizenIdentityNo"];
        data.RelationID = relation.findInArray(item["RelationID"]);
        data.BirthCertificateNumber = item["BirthCertificateNumber"];
        data.BirthCertificateBook = item["BirthCertificateBook"];
        data.BirthCertificateNationalityName =
          item["BirthCertificateNationalityName"];
        data.BirthCertificateProvinceName =
          item["BirthCertificateProvinceName"];
        data.BirthCertificateDistrictName =
          item["BirthCertificateDistrictName"];
        data.BirthCertificateWardName = item["BirthCertificateWardName"];
        data.StartDependentDate = item["StartDependentDate"];
        data.FinishDependentDate = item["FinishDependentDate"];
        datas.push(data);
      });
      this.dataTable = [...datas];
      this.dataTable.unshift(this.indexs);
    },
  },
  watch: {
    filterBar: {
      handler() {
        this.getData();
      },
      deep: true,
    },
  },
};
</script>

<style lang="scss">
#list-dependent {
  .title-03BK {
    font-family: "GoogleSansBold";
    font-size: 20px;
    font-weight: bold;
    margin-bottom: 16px !important;
    margin-top: 16px !important;
  }

  .cell {
    word-break: unset;
    white-space: unset;
  }
  
  width: 100%;
  padding: 0 24px;
  height: 100%;

  .input-search {
    width: 581px;
  }

  .currency-unit {
    float: right;
    text-align: right;
  }

  #dependent-table {
    padding-top: 16px;
  }

  #table-data .el-table__body-wrapper {
    height: calc(100% - 103px) !important;
    position: relative !important;
    top: 2px !important;
    margin-bottom: -44px !important;
  }

  #table-data .el-table__body-wrapper tr:nth-child(2) td {
    height: unset;
    padding-top: unset;
  }

  .temporary-mode-btn {
    position: absolute;
    top: 57%;
    left: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    transform: translate(-50%, -50%);
  }

  .empty-content {
    width: 464px;
    height: 161px;
    margin: auto;
    display: flex;
    justify-content: center;
    flex-direction: column;
    position: absolute;
    top: calc(50% - 100px);
    left: calc(50% - 106px);
  }
}
</style>