<template>
  <div id="appendix-bk3-container">
    <el-row>
      <el-col :span="24">
        <h2 class="title-03BK">
          BẢNG KÊ THÔNG TIN NGƯỜI PHỤ THUỘC GIẢM TRỪ GIA CẢNH
        </h2>
      </el-col>
    </el-row>
    <div class="filter-bk3" v-if="!isEmpty">
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
        :isAdjustNumber="true"
        :listIndex="listIndex"
        :displayIndexRow="true"
        @pagination="getFilter"
        typepage="bảng tính thuế"
        :id="table-data"
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
      <span>Không có thông tin người phụ thuộc.</span>
    </div>
  </div>
</template>

<script>
import fieldinput from "@/components/inputs";
import tbl from "@/components/tables";
import declarationSettlementDetailDependent from "@/api/etax/declaration_settlementDetail_dependent.js";
export default {
  name: "appendix-03BK",
  components: {
    tbl,
    "field-input": fieldinput,
  },
  data() {
    return {
      numberOfRecord: 0,
      loadingFirst: true,
      loadingAfter: true,
      isFirst: true,
      isEmpty: false,
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
          width: "140",
        },
        {
          valueIndex: "[08]",
          width: "140",
        },
        {
          valueIndex: "[09]",
          width: "140",
        },

        {
          valueIndex: "[10]",
          width: "140",
        },

        {
          valueIndex: "[11]",
          width: "140",
        },
        {
          valueIndex: "[12]",
          width: "140",
        },
        {
          valueIndex: "[13]",
          width: "140",
        },
        {
          valueIndex: "[14]",
          width: "140",
        },
        {
          valueIndex: "[15]",
          width: "140",
        },
        {
          valueIndex: "[16]",
          width: "140",
        },
        {
          valueIndex: "[17]",
          width: "140",
        },
        {
          valueIndex: "[18]",
          width: "140",
        },
        {
          valueIndex: "[19]",
          width: "140",
        },
        {
          valueIndex: "[20]",
          width: "140",
        },
        {
          valueIndex: "[21]",
          width: "140",
        },
        {
          valueIndex: "[22]",
          width: "140",
        },
      ],
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
          align: "center",
          width: "",
          minWidth: "140",
          type: "date",
          prop: "DateOfBirth",
          sortable: false,
        },
        {
          label: "Mã số thuế người phụ thuộc",
          propertyName: "DependentTaxNo",
          align: "left",
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
          align: "left",
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
    };
  },

  created() {
    this.filterBar.declarationsettlementID = this.$route.params.id;
  },

  methods: {
    /**
     * Hàm thực hiện gán lại filter khi chọn thay đổi pageIndex, pageSize
     * created by vdthang
     */
    getFilter(val) {
      this.filterBar.pageSize = val.limit;
      this.filterBar.pageIndex = val.page;
    },

    /**
     * Lấy dữ liệu cho phụ lục 05-03BK
     * created by tcduong 24.10.2020
     */
    async getData() {
      this.loadingFirst = true;
      this.loadingAfter = true;
      var res = await declarationSettlementDetailDependent.getDeclarationSettlementDetailDependent(
        this.filterBar
      );
      if (res.data.Success && !res.data.Data.IsEmpty) {
        this.mapData(res.data.Data.DeclarationSettlementDetailDependents);
        this.isEmpty = res.data.Data.IsEmpty;
        this.numberOfRecord =
          res.data.Data.TotalDeclarationSettlementDetailDependent;
      } else {
        this.isEmpty = true;
        this.dataTable = [];
      }
      this.loadingFirst = false;
      this.loadingAfter = false;
      this.isFirst = false;
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
        data.RelationID = item["RelationName"];
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
        data.IsChangeData = item["IsChangeData"];
        datas.push(data);
      });
      this.dataTable = [...datas];
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
@import "@/styles/variables.scss";

#appendix-bk3-container {
  height: calc(100vh - 214px);

  .filter-bk3 {
    height: 40px;
  }

  .title-03BK {
    font-family: $_fontbold;
    font-size: 20px;
    margin: 24px 0 16px 0;
  }

  .cell {
    word-break: unset;
    white-space: unset;
  }

  .input-search {
    width: 581px;
  }

  .currency-unit {
    float: right;
    text-align: right;
    color: $medium-dark-grey;
  }
  #table-data .index-row {
    border-bottom: 1px solid $boder-row-table;
  }
  #dependent-table {
    margin-top: 16px;
    height: calc(100% - 121px);
    position: relative;

    th {
      padding: 0;
    }

    .index-row {
      padding: 9px 0;
      font-family: $_fontmedium;
      text-align: center;
      border-bottom: 1px solid $boder-row-table;
    }

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

    .temporary-mode-btn {
      position: absolute;
      top: 55%;
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
    display: flex;
    align-items: center;
    flex-direction: column;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);

    img {
      margin-bottom: 8px;
    }
  }
}

@media only screen and (max-width: 1366px) {
  #appendix-bk3-container {
    #dependent-table {
      height: calc(100% - 16px);

      .temporary-mode-btn {
        top: 60%;

        img {
          display: none;
        }
      }
    }

    .empty-content {
      top: 60%;

      img {
        width: 60%;
      }
    }
  }
}
</style>