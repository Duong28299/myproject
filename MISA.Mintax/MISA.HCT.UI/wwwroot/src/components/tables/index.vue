<template>
  <div class="tbl-main" style="height: 100%">
    <el-table
      v-if="typeTable === 'tableExcel'"
      border
      v-loading="loading"
      :style="getHeight()"
      :data="dataTable"
      width="100%"
      :show-summary="summary"
      :cell-style="finalCellStyle"
      class="tableData"
      tooltip-effect="light"
      @row-click="handleRowClick"
      @row-dblclick="handleRowDBLClick"
      id="table-excel"
    >
      <el-table-column
        v-for="(item, index) in listColumn"
        :key="index"
        :label="item.label"
        :tooltip="item.label"
        :property="item.propertyName"
        :width="item.width"
        :align="item.align"
        :header-align="item.alignHeader"
        :min-width="item.minWidth"
        :prop="item.prop"
        :sortable="item.sortable"
        :show-overflow-tooltip="true"
      >
        <template slot="header" v-if="tooltip">
          <el-tooltip
            v-if="item.tooltip"
            class="item"
            effect="light"
            :content="item.tooltip"
            placement="top"
          >
            <span>{{ item.label }}</span
            ><span
              class="validate-style"
              v-if="item.validate && item.validate == true"
              >*</span
            >
          </el-tooltip>
        </template>
        <template slot="header">
          <span>{{ item.label }}</span
          ><span
            class="validate-style"
            v-if="item.validate && item.validate == true"
          >
            *</span
          >
        </template>
        <template slot-scope="scope">
          <span>{{ scope.row.SomeData }}</span>
        </template>
        <template slot-scope="{ row }">
          <!-- Dạng thường -->
          <span v-if="typeof item.type == 'undefinded' || item.type == ''">
            <el-tooltip v-if="isErrorCell(item.propertyName, row)">
              <div
                slot="content"
                placement="top-start"
                style="text-align: center; width: 150px"
              >
                {{ bindErrorTextFromCode(item.propertyName, row) }}
              </div>
              <div>{{ row[item.propertyName] }}</div>
            </el-tooltip>
            <div v-else>{{ row[item.propertyName] }}</div>
          </span>

          <!-- Dạng ngày -->
          <span v-if="item.type === 'date'">{{
            formatDate(row[item.propertyName])
          }}</span>

          <!-- Dạng tháng mm/yyyy -->
          <span v-if="item.type === 'month'">{{
            formatDate(row[item.propertyName], "mm/yyyy")
          }}</span>

          <!-- Dạng số -->
          <span v-if="item.type === 'decimal'">{{
            formatMoney(row[item.propertyName])
          }}</span>

          <!-- Dạng kiểu hiển thị chip -->
          <div v-if="item.type === 'chip'">
            <el-tag
              v-if="item.type === 'chip'"
              :type="convertType(row[item.propertyName])"
              >{{ convertName(row[item.propertyName]) }}</el-tag
            >
          </div>
          <div v-if="item.type === 'status-excel' && statusFlag">
            <!-- Xét tooltip cho icon cảnh báo dữ liệu trùng nếu dữ liệu trùng lặp trong db -->
            <el-tooltip
              v-if="getContentTooltip(row[item.propertyName])"
              :visible-arrow="true"
              placement="top"
            >
              <div slot="content" style="text-align: center; width: 170px">
                {{ contentTooltip }}
              </div>
              <div>
                <img
                  :src="
                    require('@/icons/' +
                      getExcelStatusByValue(row[item.propertyName]).ICON)
                  "
                /><img />
              </div>
            </el-tooltip>
            <div v-else-if="getExcelStatusByValue(row[item.propertyName]).ICON">
              <img
                :src="
                  require('@/icons/' +
                    getExcelStatusByValue(row[item.propertyName]).ICON)
                "
              /><img />
            </div>
          </div>
        </template>
      </el-table-column>
    </el-table>

    <el-table
      v-else-if="typeTable === 'tableData'"
      border
      ref="reftable"
      v-loading="loading"
      :style="getHeight()"
      :data="dataTable"
      width="100%"
      class="tableData table-data"
      :summary-method="getSummaries"
      :show-summary="summary"
      :row-class-name="getStyleRow"
      tooltip-effect="light"
      @row-click="handleRowClick"
      @row-dblclick="handleRowDBLClick"
      @selection-change="handleSelectionChange"
      @select-all="handleSelectAll"
    >
      <el-table-column align="center" width="50" label="" v-if="isAdjustNumber">
        <template
          slot-scope="{ row }"
          v-if="!row['Header'] && row.IsChangeData == 1"
        >
          <m-btn
            type="btn-white2-onlyicon-big"
            tooltip="Điều chỉnh số hiệu"
            class="btn-adjust-number"
          >
            <template v-slot:icon>
              <img
                class="icon"
                src="@/icons/Group 20019@2x.png"
                style="width: 16px; height: 16px"
              />
            </template>
          </m-btn>
        </template>
      </el-table-column>

      <el-table-column
        width="50"
        type="index"
        label="STT"
        v-if="autoDisplayOrdinalNumber"
      >
      </el-table-column>
      <custom-column
        v-for="(item, index) in listColumn"
        :key="index"
        :listColumn="item"
        :optionsSelect="optionsSelect"
        @selectChange="selectChange"
      >
      </custom-column>
      <el-table-column
        v-if="haveAction"
        split-button="false"
        align="center"
        width="103"
        label="Hành động"
      >
        <template
          class="action"
          slot-scope="{ row }"
          v-if="!row['Header'] && row.Status == 1"
        >
          <m-btn
            @click.native="handleUpdate(row, actionEnum().StatusTableExcel.Del)"
            type="btn-white2-onlyicon-big"
            tooltip="Xóa"
            class="btn-action-del"
          >
            <template v-slot:icon>
              <img
                class="icon"
                src="@/icons/ico-trash-bold@2x.png"
                style="width: 16px; height: 16px"
              />
            </template>
          </m-btn>
          <m-btn
            @click.native="
              handleUpdate(row, actionEnum().StatusTableExcel.Edit)
            "
            type="btn-white2-onlyicon-big"
            tooltip="Sửa"
            positionTooltip="left"
            class="btn-action-edit"
          >
            <template v-slot:icon>
              <img
                class="icon"
                src="@/icons/ico-edit-basic@2x.png"
                style="width: 16px; height: 16px"
              />
            </template>
          </m-btn>
        </template>
      </el-table-column>
    </el-table>

    <el-table
      v-else
      ref="reftable"
      v-loading="loading"
      :style="getHeight()"
      :data="dataTable"
      width="100%"
      :class="classTable"
      class="table-data"
      :pageSizes="pageSizes"
      :summary-method="getSummaries"
      :show-summary="summary"
      highlight-current-row
      :row-class-name="getStyleRow"
      tooltip-effect="light"
      @row-click="handleRowClick"
      @row-dblclick="handleRowDBLClick"
      @selection-change="handleSelectionChange"
      @select-all="handleSelectAll"
      :default-sort="ruleSort"
      :border="border"
    >
      <el-table-column
        v-if="haveCheck"
        type="selection"
        width="40"
      ></el-table-column>
      <el-table-column
        v-for="(item, index) in listColumn"
        :key="index"
        :label="item.label"
        :tooltip="item.tooltip"
        :width="item.width"
        :align="item.align"
        :min-width="item.minWidth"
        :prop="item.prop"
        :sortable="item.sortable"
        :show-overflow-tooltip="true"
      >
        <template slot="header" v-if="tooltip">
          <el-tooltip
            v-if="item.tooltip"
            class="item"
            effect="light"
            :content="item.tooltip"
            placement="top"
          >
            <span>{{ item.label }}</span>
          </el-tooltip>
        </template>
        <template slot-scope="scope">
          <span>{{ scope.row.SomeData }}</span>
        </template>
        <template slot-scope="{ row }">
          <!-- Dạng thường -->
          <span v-if="typeof item.type == 'undefinded' || item.type == ''">{{
            row[item.propertyName]
          }}</span>
          <!-- Dạng ngày -->
          <span v-if="item.type === 'date'">{{
            formatDate(row[item.propertyName])
          }}</span>
          <!-- Dạng tháng mm/yyyy -->
          <span v-if="item.type === 'month'">{{
            formatDate(row[item.propertyName], "mm/yyyy")
          }}</span>
          <!-- Dạng số -->
          <span v-if="item.type === 'decimal'">{{
            formatMoney(row[item.propertyName])
          }}</span>
          <!-- Dạng kiểu hiển thị chip -->
          <div v-if="item.type === 'chip'">
            <el-tag
              v-if="item.type === 'chip'"
              :type="convertType(row[item.propertyName])"
              >{{ convertName(row[item.propertyName]) }}</el-tag
            >
          </div>
          <div v-if="item.type == 'btn-chip'" class="btn-chip-type">
            <m-btn
              @click.native="handleClickChip(row)"
              type="btn-chip-info"
              size="small"
              name="Chi tiết"
            ></m-btn>
          </div>
        </template>
      </el-table-column>
      <el-table-column
        v-if="haveAction"
        split-button="false"
        align="center"
        width="60"
      >
        <template class="action" slot-scope="{ row }">
          <!-- <span v-if="[1].includes(row[destiStatus])"> -->
          <span v-if="!destiArrStatus.includes(row[destiStatus]) || !isStatus">
            <el-dropdown trigger="click">
              <span style="background-color: $light-grey">
                <img
                  style="width: 24px; height: 24px; padding: 4px"
                  src="@/icons/ico-more@2x.png"
                />
              </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item
                  v-for="(item, index) in listaction"
                  :key="index"
                  @click.native="handleUpdate(row, item.value)"
                  :class="item.class"
                  >{{ item.name }}</el-dropdown-item
                >
              </el-dropdown-menu>
            </el-dropdown>
          </span>
        </template>
      </el-table-column>
    </el-table>

    <div
      style="height: 60px; padding: 0px; background-color: $light-grey"
      v-if="havePagination"
    >
      <pagination
        v-show="total > 0"
        :total="total"
        :page="listQuery.page"
        :limit.sync="listQuery.limit"
        @page="listQuery.page = $event"
        @pagination="getPagination"
        :layout="layoutPaging"
        :pageSizes="pageSizes"
        :typepage="typepage"
      />
    </div>
  </div>
</template>

<script>
import chipEnum from "@/enums/chipEnum.js";
import Pagination from "@/components/pagination";
import { formatMoney } from "@/utils/index.js";
import { formatDate } from "@/utils/index.js";
import Button from "@/components/buttons";
import CustomColumn from "./column";
import actionEnum from "@/enums/actionTable.js";
// import excelStatus from "@/enums/excelStatus.js";

import { IMPORT_VALID_ERROR } from "@/enums";
export default {
  name: "TableSummaryPaging",
  components: {
    pagination: Pagination,
    "m-btn": Button,
    "custom-column": CustomColumn,
  },
  props: {
    //Cho hiển thị cột điều chỉnh số hiệu
    isAdjustNumber: {
      type: Boolean,
      default: false,
    },
    //Danh sách index của cột
    listIndex: {
      type: Array,
      default: null,
    },
    //có hiển thị dòng index không
    displayIndexRow: {
      type: Boolean,
      default: false,
    },
    //Có tự động hiển thị cột số thứ tự không
    autoDisplayOrdinalNumber: {
      type: Boolean,
      default: true,
    },
    // danh sách select cho table mới
    optionsSelect: {
      type: Array,
      default: null,
    },
    typeTable: {
      type: String,
      default: "",
    },
    pageSizes: {
      type: Array,
      default() {
        return [25, 50, 75, 100];
      },
    },
    layoutPaging: {
      type: String,
      default: "prev, pager, next, sizes",
    },
    reftable: {
      type: String,
      default: "",
    },
    dataTable: {
      type: Array,
      default: null,
    },
    listColumn: {
      type: Array,
      default: null,
    },
    haveCheck: {
      type: Boolean,
      default: false,
    },
    havePagination: {
      type: Boolean,
      default: true,
    },
    haveAction: {
      type: Boolean,
      default: true,
    },
    chipArray: {
      type: String,
      default: null,
    },
    summary: {
      type: Boolean,
      default: false,
    },
    total: {
      type: Number,
      default: 0,
    },
    loading: {
      type: Boolean,
      default: true,
    },
    classTable: {
      type: String,
      default: "tbl-summary",
    },
    positionSummary: {
      type: Object,
      default: null,
    },
    tooltip: {
      type: Boolean,
      default: false,
    },
    /**
     * Danh sách truyền vào cột hành động
     */
    listaction: {
      type: Array,
      default: null,
    },
    /**
     *Truyền xuống tên loại bảng
     */
    typepage: {
      type: String,
      default: null,
    },
    /**
     * Truyền xuống có xem trạng thái hay không
     */
    viewStatus: {
      type: Boolean,
      default: true,
    },
    /**
     * Tên trường xét xem trạng thái
     */
    destiStatus: {
      type: String,
      default: "",
    },
    /**
     * Giá trị xét xem trạng thái
     */
    destiArrStatus: {
      type: Array,
      default: () => [],
    },
    /**
     * Table có trường trạng thái hay không
     */
    isStatus: {
      type: Boolean,
      default: true,
    },
    ruleSort: {
      type: Object,
      default: () => {},
    },

    /**
     * Cờ trạng thái excel
     */
    statusFlag: {
      type: Boolean,
      default: false,
    },
    /**
     * Danh sách trạng thái excel
     */
    statusExcelStatus: {
      type: Array,
      default: null,
    },

    // Tên thuộc tính định dạng lỗi. Thuộc tính này sẽ là mảng có PropertyName
    propertyError: {
      type: String,
      default: "",
    },
    border: {
      type: Boolean,
      default: false,
    },
    /**
     * style cho ô của hàng đầu tiên hoặc ô có ô trên nó ko lỗi
     */
    styleFullError: {
      type: Object,
      default: null,
    },

    /**
     * style cho ô của hàng có ô trên cũng lỗi
     */
    styleHalfError: {
      type: Object,
      default: null,
    },

    /**
     * Danh sách tooltip cho icon cảnh báo dữ liệu trùng
     */
    tooltipStatus: {
      type: Array,
      default: null,
    },
    keyOfRow: {
      type: String,
      default: "DeclarationSettlementDetailId",
    },
    nameofRow: {
      type: String,
      default: "FullName",
    },
  },
  data() {
    return {
      listQuery: {
        page: 1,
        limit: 10,
        importance: undefined,
        title: undefined,
        type: undefined,
        sort: "+id",
      },
      selection: [],
      contentTooltip: "", //Nội dung tooltip cho icon cảnh báo
      valueSelect: "",
      textErrorTooltip: "",
    };
  },
  watch: {
    displayIndexRow(val) {
      this.autoGenarateIndexs(val);
    },
  },
  mounted() {
    /**
     * Generate index của cột
     * created by tcduong 5.11.2020
     */
    if (this.typeTable === "tableData" && this.displayIndexRow) {
      var thead = document.querySelector(
        ".tableData .el-table__header-wrapper"
      );
      var indexs = document.createElement("tr");
      var td = "";
      this.listIndex.forEach((element) => {
        td += `<td class="index-row" style="min-width:${element.width}px;">${element.valueIndex}</td>`;
      });
      indexs.innerHTML = td;
      thead.append(indexs);
    }
  },
  computed: {
    selectionA() {
      return this.selection;
    },
  },
  methods: {
    selectChange(row) {
      this.$emit("selectChange", row);
    },

    /**
     * Generate index của cột
     * created by tcduong 5.11.2020
     */
    autoGenarateIndexs(val) {
      var thead = document.querySelector(
        ".tableData .el-table__header-wrapper"
      );
      var indexs = document.createElement("tr");
      var td = "";
      if (this.typeTable === "tableData" && val) {
        this.listIndex.forEach((element) => {
          td += `<td class="index-row" style="min-width:${element.width}px;">${element.valueIndex}</td>`;
        });
        indexs.innerHTML = td;
        thead.append(indexs);
      } else {
        var indexRows = document.querySelectorAll(".index-row");
        if (indexRows.length > 0) {
          indexRows.forEach((item) => {
            item.remove();
          });
        }
        indexs = document.createElement("tr");
        thead.append(indexs);
      }
    },
    /**
     * Style cho từng ô
     * CreatedBy NVANH 11.03.2020
     */
    finalCellStyle({ row, column, rowIndex }) {
      let wrongFormatCode = [
        IMPORT_VALID_ERROR.NULL,
        IMPORT_VALID_ERROR.TOO_LONG,
        IMPORT_VALID_ERROR.WRONG_TYPE,
        IMPORT_VALID_ERROR.WRONG_FORMAT,
        IMPORT_VALID_ERROR.DUPLICATE_AND_NOT_UPDATE,
        IMPORT_VALID_ERROR.DUPLICATE_IN_FILE_AND_NOT_UPDATE,
        IMPORT_VALID_ERROR.DUPLICATE_IN_DB_AND_NOT_UPDATE,
      ];
      if (!this.propertyError) {
        return {};
      }
      for (var i = 0; i < row[this.propertyError].length; i++) {
        var item = row[this.propertyError];
        if (wrongFormatCode.find((x) => x == item[i].Code) != null) {
          if (column.property == item[i].PropertyName && rowIndex == 0) {
            return this.styleFullError;
          }
          if (column.property == item[i].PropertyName && rowIndex != 0) {
            var PrevRow = this.dataTable[rowIndex - 1];
            var [obj] = PrevRow[this.propertyError].filter(
              (error) => error.PropertyName == item[i].PropertyName
            );
            if (obj && wrongFormatCode.find((x) => x == obj.Code))
              return this.styleHalfError;
            else return this.styleFullError;
          }
        }
      }
      return {};
    },

    /**
     * Hàm bắt sự kiện slect trên table mới
     * created by : NVHUNG - 16/10/2020
     */
    handleRowChange(row, value) {
      this.$emit("row-change", { row, value });
    },

    /**
     * Hàm tạo index ( đang bỏ hàng đầu tiên)
     * created by : NVHUNG 16/10/2020
     */
    // indexMethod(index) {
    //   if (index > 0) {
    //     return index;
    //   } else {
    //     return null;
    //   }
    // },

    /**
     * Hàm thay đổi color các row được chọn bởi checkbox
     * created by CVCuong
     */
    getStyleRow({ row }) {
      var i = this.selectionA.indexOf(row);
      if (i >= 0) return "row-color";
      return "";
    },

    /**
     * Hàm thực hiện handle sự kiện dbClick
     * created by vdthang 04.07.2020
     */
    handleRowDBLClick(dataRow) {
      this.$emit("handleRowDBLClick", dataRow);
    },

    /**
     * Hàm thực hiện click một row
     * creadted bt cvcuong
     */
    handleRowClick(data) {
      this.$refs.reftable.setCurrentRow(data);
      this.$emit("handleRowClick", data);
    },

    /**
     *
     */
    handleChipClick(data) {
      this.$emit("chip-emit", data);
    },

    /**
     * Hàm thực hiện tính tổng của từng cột trong bảng
     * created by vdthang 03.07.2020
     */
    getSummaries(param) {
      var sums = [];
      var datas = param.data;
      var positionSummary = this.positionSummary.TextSummary;
      var listSum = this.positionSummary.Summary;
      if (positionSummary)
        sums[positionSummary.TextPosition] = positionSummary.TextString;

      //Thực hiện duyệt từng cột bản ghi tính tổng
      listSum.forEach((item, index) => {
        if (index == 0) sums[item.PropertyIndex] = "Tổng";
        else {
          sums[item.PropertyIndex] = 0;
          datas.forEach((data) => {
            if (index > 0)
              sums[item.PropertyIndex] =
                (sums[item.PropertyIndex] * 10 + data[item.PropertyName] * 10) /
                10;
          });
          sums[item.PropertyIndex] = this.formatMoney(sums[item.PropertyIndex]);
        }
      });
      return sums;
    },

    /**
     * Hàm update hàng trong bảng,
     *
     */
    handleUpdate(row, value) {
      this.$emit("row-action", { row, value });
    },

    /**
     * Chuyển sang tên của chip
     * created by vdthang 01.12.2020
     */
    convertName(value) {
      var temp = chipEnum[this.chipArray];
      for (let i = 0; i < temp.length; i++) {
        if (temp[i].value == value) {
          return temp[i].name;
        }
      }
    },

    /**
     * Chuyển sang type của chip
     * created by vdthang 01.12.2020
     */
    convertType(value) {
      var temp = chipEnum[this.chipArray];
      for (let i = 0; i < temp.length; i++) {
        if (temp[i].value == value) {
          return temp[i].type;
        }
      }
    },

    /**
     * Hàm xét chiều cao cho table dựa vào có hoặc không có phân trang
     */
    getHeight() {
      if (this.havePagination) {
        return "height:calc(100% - 60px); border-top-left-radius: 5px; border-top-right-radius: 5px;";
      } else return "height:100%; border-radius: 5px;";
    },

    /**
     * Handle select checkbox
     * created by vdthang 01.12.2020
     */
    handleSelectionChange(val) {
      this.selection = val;
      this.$emit("handleSelectionChange", val);
    },

    /**
     * Handle select all
     * created by vdthang 01.12.2020
     */
    handleSelectAll(val) {
      this.selection = val;
      this.$emit("handleSelectAll", val);
    },

    /**
     * Lấy thông tin phân trang
     * created by vdthang 01.12.2020
     */
    getPagination(val) {
      this.$emit("pagination", val);
    },

    /**
     * Hàm thực hiện format cột hiển thị số
     * created by vdthang 01.12.2020
     */
    formatMoney(data) {
      return formatMoney(data, ",");
    },

    /**
     * Hàm định dạng ngày
     * created by vdthang 01.12.2020
     */
    formatDate(data, type = "dd/mm/yyyy") {
      return formatDate(data, type);
    },

    /**
     * Hàm bỏ chọn all checkbox
     * created by vdthang 01.12.2020
     */
    clearSelection(rows) {
      if (rows) {
        rows.forEach((row) => {
          this.$refs.reftable.toggleRowSelection(row);
        });
      } else {
        this.$refs.reftable.clearSelection();
      }
    },

    /**
     * Lấy lỗi
     * created by vdthang 01.12.2020
     */
    getExcelStatusByValue(val) {
      var res = this.statusExcelStatus.find((x) => x.VALUE == val);
      return res;
    },

    /**
     * Lấy enum cho cột hành động
     * created by vdthang 01.12.2020
     */
    actionEnum() {
      return actionEnum;
    },

    isErrorCell(val, row) {
      var valError = row.ImportValidError.find((x) => x.PropertyName == val);
      if (valError && val) {
        return true;
      } else {
        return false;
      }
    },

    bindErrorTextFromCode(val, row) {
      var valError = row.ImportValidError.find((x) => x.PropertyName == val);
      switch (valError.Code) {
        case IMPORT_VALID_ERROR.NULL:
          return "Thiếu thông tin";
        case IMPORT_VALID_ERROR.TOO_LONG:
          return "Thông tin quá dài";
        case IMPORT_VALID_ERROR.WRONG_TYPE:
          return "Sai kiểu dữ liệu";
        case IMPORT_VALID_ERROR.WRONG_FORMAT:
          return "Sai định dạng dữ liệu";
        case IMPORT_VALID_ERROR.DUPLICATE_IN_FILE:
        case IMPORT_VALID_ERROR.DUPLICATE_IN_FILE_AND_NOT_UPDATE:
          return `Trùng thông tin ${this.duplicatePropertyName(
            valError.PropertyName
          )} của người lao động ${this.duplicateValueName(valError.ExtendInfo)}
          trong file `;
        case IMPORT_VALID_ERROR.DUPLICATE_IN_DATABASE:
        case IMPORT_VALID_ERROR.DUPLICATE_IN_DB_AND_NOT_UPDATE:
          return `Trùng thông tin ${this.duplicatePropertyName(
            valError.PropertyName
          )} của người lao động ${this.duplicateValueName(valError.ExtendInfo)}
          trong phụ lục`;
      }
    },

    duplicatePropertyName(val) {
      var res = this.listColumn.find((x) => x.propertyName == val);
      return res ? res.label : "";
    },

    duplicateValueName(val) {
      var res = this.dataTable.find((x) => x[this.keyOfRow] == val);
      return res ? res[this.nameofRow] : "";
    },

    /**
     * Lấy giá trị nội dung cho tooltip và xử lý ẩn tooltip nếu không cần thiết
     * created by vdthang 01.12.2020
     */
    getContentTooltip(val) {
      for (var i in this.tooltipStatus) {
        if (this.tooltipStatus[i].value == val) {
          this.contentTooltip = this.tooltipStatus[i].status;
          return true;
        }
      }
      return false;
    },

    /**
     * Hàm thực hiện click vào ô dữ liệu dạng chip
     * created by vdthang 01.12.2020
     */
    handleClickChip(row) {
      this.$emit("click-chip", row);
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";
.tbl-main .el-dropdown {
  border: none;
  right: 0px;
  top: 2px;
  height: 24px !important;
  width: 24px !important;
}

.validate-style {
  color: $red;
}

.el-table .el-dropdown {
  padding-left: 0 !important;
  padding-right: 0px !important;
  cursor: pointer;

  img {
    border-radius: 5px;
    &:hover {
      background-color: $bg-color-3 !important;
    }
  }
}

.row-color {
  background-color: $hover-blue !important;
}

.text-red {
  color: $red;
}

.btn-rg-chip {
  position: absolute;
  right: 5px;
  top: 0;
  padding: 10px;
}

.table-data {
  display: flex;
  flex-direction: column;
  .el-table__header-wrapper {
    order: 0;
  }

  .index-row {
    height: 36px;
    padding: 9px 0;
    font-family: $_fontmedium;
    text-align: center;
    border-bottom: 1px solid $boder-row-table;
  }

  .el-table__header-wrapper {
    order: 1;
    position: relative;
  }
  .el-table__body-wrapper {
    order: 3;
    position: relative;
    height: calc(100% - 148px);

    .btn-adjust-number .btn-white2-onlyicon-big {
      padding-left: 17px;
      background: none;
      border: none;
    }

    .btn-adjust-number .btn-white2-onlyicon-big:hover {
      background: none !important;
      border: none !important;
    }
    .btn-action-del button {
      display: flex;
      justify-content: center;
      width: 36px !important;
      height: 36px;
      border: none;
      background: none;
    }
    .btn-action-edit button {
      display: flex;
      justify-content: center;
      width: 36px !important;
      height: 36px;
      border: none;
      margin-right: 10px;
      background: none;
    }
    .btn-action-del button:hover {
      border: none !important;
      background-color: $white !important;
    }
    .btn-action-edit button:hover {
      border: none !important;
      background-color: $white !important;
    }
  }

  .el-table__footer-wrapper {
    order: 2;
    position: relative;
    display: block !important;
  }

  .row-dropdown {
    height: 32px;
    min-width: 80px;

    .el-button--medium {
      padding: 0 10px;
    }

    .el-select__caret {
      color: $white;
    }

    input {
      height: 32px;
      background-color: $blue;
      border-color: $blue;
      color: $white;
    }
  }
}

.tbl-order-normal {
  .el-table__footer-wrapper {
    order: 3;
  }
}
</style>