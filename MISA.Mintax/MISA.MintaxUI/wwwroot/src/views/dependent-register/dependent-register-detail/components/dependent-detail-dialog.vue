<template>
  <div>
    <!-- Dialog thêm người phụ thuộc -->
    <add-dialog
      :id="dialogid"
      :dialogFormVisible="visiableDL"
      @close-dialog="getClose"
      :fullscreen="true"
    >
      <template v-slot:body>
        <div class="company-container">
          <div class="add-employee-tab">
            <span class="title">{{ title }}</span>
          </div>

          <div class="add-employee-main">
            <div class="add-dependent">
              <div class="employee-info">
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Người nộp thuế:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{
                      dependentDetail.FullName
                    }}</span>
                  </el-col>
                </el-row>
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Mã nhân viên:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{
                      dependentDetail.EmployeeCode
                    }}</span>
                  </el-col>
                </el-row>
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Mã số thuế cá nhân:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{
                      dependentDetail.TaxNo
                    }}</span>
                  </el-col>
                </el-row>
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Bộ phận/Phòng ban:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{
                      getOrganizationUnitName
                    }}</span>
                  </el-col>
                </el-row>
                <el-row class="detail-info">
                  <el-col :span="10">
                    <span class="detail-info-text">Vị trí/Chức vụ:</span>
                  </el-col>
                  <el-col :span="12">
                    <span class="detail-info-value">{{ getPositionName }}</span>
                  </el-col>
                </el-row>
              </div>
              <div class="main-dependent-register">
                <div class="img-dependent" v-if="dataTable.length == 0">
                  <img
                    class="img-src"
                    src="@/assets/images/Group 19459.svg"
                  />
                  <btn                    
                    class="btn-add-dependent"
                    type="btn-white-icon"
                    name="Thêm người phụ thuộc"
                    :handleClick="setAddMode"
                  >
                    <template v-slot:icon>
                      <img class="icon" src="@/icons/Component 9756.svg" />
                    </template>
                  </btn>
                </div>

                <div class="container-table-tab" ref="FormDependent">
                  <el-row>
                    <el-col>
                      <el-row
                        v-if="dataTable.length > 0 && !isCheckRow"
                        class="header-list-table"
                      >
                        <el-col :span="12">
                          <span class="title-list-table">{{
                            titleListTable
                          }}</span>
                        </el-col>
                        <el-col :span="12">
                          <btn
                            :type="'btn-white-icon'"
                            :name="'Thêm người phụ thuộc'"
                            :handleClick="setAddMode"
                            class="bg-white btn-add-dependent"
                          >
                            <template v-slot:icon>
                              <img
                                class="icon"
                                src="@/icons/Component 9756.svg"
                              />
                            </template>
                          </btn>
                        </el-col>
                      </el-row>
                      <el-row
                        class="del"
                        v-if="isCheckRow && dataTable.length > 0"
                      >
                        <el-col :span="5">
                          <btn
                            type="btn-white3-onlyicon"
                            :iconel="'el-icon-delete'"
                            tooltip="Xóa người nộp thuế"
                            :handleClick="openPopup"
                          >
                            <template v-slot:icon></template>
                          </btn>
                          <span class="del-title" @click="clearSelection"
                            >Bỏ chọn {{ select.length }} người phụ thuộc</span
                          >
                        </el-col>
                      </el-row>
                      <el-row class="table" v-if="getLengthData > 0">
                        <tbl
                          ref="dependent-detail"
                          :listColumn="listColumn"
                          :dataTable="dataTable"
                          :haveCheck="haveCheck"
                          :havePagination="false"
                          :tooltip="true"
                          :chipArray="chipArray"
                          :listaction="listaction"
                          :loading="false"
                          :isStatus="false"
                          @handleSelectionChange="getRowsSelection"
                          @row-action="getRowAction"
                        ></tbl>
                      </el-row>
                    </el-col>
                  </el-row>
                </div>
              </div>
            </div>
            <!-- / Tab 2 -->
          </div>
        </div>
      </template>
      <template v-slot:footer>
        <!-- button employeeTab-->
        <btn
          name="Đóng"
          type="btn-primary"
          class="ml-10 btn-footer"
          :handleClick="resetForm"
          v-if="!changeButton"
        ></btn>
        <btn
          name="Lưu"
          type="btn-primary"
          class="ml-10 btn-footer none-boder"
          :handleClick="handleFinish"
          v-if="changeButton"
        ></btn>
      </template>
    </add-dialog>
    <addDependentDialog
      :title="titleDialog"
      :formMode="formMode"
      :DependentSelected="getDependent"
      :visiableDL="isDialog"
      :declarationID="dependentDetail.DeclarationDetailID"
      :EmployeeName="dependentDetail.FullName"
      @closeDialog="isDialog = $event"
      @success="handleSuccess"
    />
    <!-- / Dialog thêm hồ sơ người phụ thuộc -->
  </div>
</template>
<script>
import dialogel from "@/components/dialog";
import Button from "@/components/buttons";
import tbl from "@/components/tables";
import actionTable from "@/enums/actionTable.js";
import declaration from "@/api/etax/declaration.js";
import addDependentDialog from "@/views/dependent-register/dependent-register-detail/components/add-dependent-dialog";
import apiPosition from "@/api/etax/position.js";
import relation from "@/enums/relation.js";
export default {
  // xóa name ="dialog" do gây lỗi [Vue warn]: Do not use built-in or reserved HTML elements as component id: dialog
  components: {
    "add-dialog": dialogel,
    btn: Button,
    tbl: tbl,
    addDependentDialog,
  },
  props: {
    visiableDL: {
      type: Boolean,
      default: true,
    },
    dependentDetail: {
      type: Object,
      default: null,
    },
    // handleClickProp: {
    //   type: Function,
    //   default: () => {},
    // },
  },

  data() {
    return {
      PositionNameList: [],
      dialogid: "dependent-detail-dialog",
      title: "CHI TIẾT NGƯỜI NỘP THUẾ",
      titleDialog: "THÊM MỚI NGƯỜI PHỤ THUỘC",
      isDialog: false,
      listColumn: [
        {
          label: "Họ và tên ",
          propertyName: "DependentName",
          width: "",
          minWidth: "136",
          align: "left",
          type: "",
          prop: "DependentName",
          sortable: true,
        },
        {
          label: "Mã số thuế",
          propertyName: "DependentTaxNo",
          width: "",
          minWidth: "90",
          align: "left",
          type: "",
          prop: "CitizenIdentityNo",
          sortable: true,
        },
        {
          label: "Ngày sinh",
          propertyName: "DateOfBirth",
          width: "",
          minWidth: "82",
          align: "left",
          type: "date",
          prop: "TaxIssueDate",
          sortable: true,
        },
        {
          label: "Quan hệ với người nộp thuế",
          propertyName: "RelationID",
          width: "",
          minWidth: "",
          align: "left",
          type: "",
          prop: "RelationID",
          sortable: true,
        },
        {
          label: "Loại phụ thuộc",
          propertyName: "DependentType",
          width: "",
          minWidth: "",
          align: "left",
          type: "chip",
          prop: "DependentType",
          sortable: true,
        },
        {
          label: "Ngày bắt đầu",
          propertyName: "StartDependentDate",
          width: "",
          minWidth: "100",
          align: "center",
          type: "month",
          prop: "StartDependentDate",
          sortable: true,
        },
        {
          label: "Ngày kết thúc",
          propertyName: "FinishDependentDate",
          width: "",
          minWidth: "100",
          align: "center",
          type: "month",
          prop: "FinishDependentDate",
          sortable: true,
        },
      ],
      dataTable: [],
      haveCheck: true,
      isCheckRow: false,
      formMode: 1,
      dataEdit: "",
      listaction: [],
      active: true,
      changeButton: false,
      DependentSelected: {},
      listSelected: [],
      listDelete: [],
      select: [],
      numberRecordDeleted: 0, //số bản ghi xóa thành công
      modeBody: "TaxPayer",
      declarationDetailID: "",
      titleListTable: "Danh sách người phụ thuộc",
      chipArray: "DEPENDENT_TYPE",
    };
  },
  computed: {
    getLengthData() {
      return this.dataTable.length;
    },
    getDependent() {
      return this.DependentSelected;
    },
    getPositionName() {
      return this.findInArray(
        this.PositionNameList,
        this.dependentDetail.PositionID,
        "PositionID",
        "PositionName"
      );
    },
    getOrganizationUnitName() {
      return this.dependentDetail.OrganizationUnitName;
    },
  },
  created() {
    this.listaction = actionTable.DEPENDENT_REGISTER;
    this.getPosition();
  },

  watch: {
    visiableDL: {
      async handler(val) {
        if (val) {
          await this.getdataDialog();
        } else {
          this.$emit("success", true);
          this.active = true;
        }
      },
    },
    active: {
      async handler(val) {
        if (val) {
          await this.getdataDialog();
          this.modeBody = "TaxPayer";
        } else {
          this.modeBody = "Dependent";
        }
      },
      deep: true,
    },
  },

  methods: {
    /**
     * Tìm kiếm
     * created by tcduong 25.9.2020
     * @param{array là mảng object cần tìm kiếm}
     * @param{value là giá trị cần so sánh}
     * @param{filed là trường cần so sánh với giá trị truyền vào}
     * @param{name là đầu ra mong muốn}
     */
    findInArray(array, value, filed, name) {
      for (var i in array) {
        if (array[i][filed] == value) {
          return array[i][name];
        }
      }
    },
    /**
     * Lấy danh sách position từ servie
     * created by tcduong 25.9.2020
     */
    async getPosition() {
      var res = await apiPosition.getPosition();
      if (!res.data.Data || !res.data.Success || res.data.MISACode != 200)
        return [];
      else this.PositionNameList = [...res.data.Data];
    },
    /**
     * Hàm gọi service lấy danh sách chi tiết người phụ thuộc theo id người nộp thuế
     * Created by NQTrung created date 19/09/2020
     */
    async getdataDialog() {
      var res = await declaration.getDeclarationDetailDependent(
        this.dependentDetail.DeclarationDetailID
      );
      if (res.data.Success) {
        this.mapdataDialog(res.data.Data);
        // this.$store.commit("taxPeriod/setData", res.data.Data);
      } else {
        this.dataTable = [];
        //this.$store.commit("taxPeriod/setData", this.dataTable);
      }
    },
    //Mapping dữ liệu vào bảng
    mapdataDialog(val) {
      var datas = [];
      val.forEach((item) => {
        var data = {};
        data.DeclarationDetailDependentID =
          item["DeclarationDetailDependentID"];
        data.DependentName = item["DependentName"];
        data.CitizenIdentityNo = item["CitizenIdentityNo"];
        data.DependentTaxNo = item["DependentTaxNo"];
        data.DateOfBirth = item["DateOfBirth"];
        data.RelationID = relation.findInArray(item["RelationID"]);
        data.StartDependentDate = item["StartDependentDate"];
        data.FinishDependentDate = item["FinishDependentDate"];
        data.DeclarationDetailDependentID =
          item["DeclarationDetailDependentID"];
        data.DependentType = item["DependentType"];
        datas.push(data);
      });
      this.dataTable = [...datas];
    },

    resetForm() {
      this.getClose();
    },

    getClose() {
      this.changeButton = false;
      this.$emit("closeDialog", false);
    },

    setAddMode() {
      this.formMode = 1;
      this.openAddDialog();
    },
    //Mở Dialog Thêm/ sửa người phụ thuộc
    openAddDialog() {
      if (this.formMode == 1) {
        this.titleDialog = "THÊM MỚI NGƯỜI PHỤ THUỘC";
        this.isDialog = true;
      } else {
        this.titleDialog = "CHI TIẾT NGƯỜI PHỤ THUỘC";
        this.isDialog = true;
      }
    },

    //Tab người nộp thuế
    getTaxPayerMode() {
      this.active = true;
      this.modeBody = "TaxPayer";
    },
    //Tab người phụ thuộc
    getDependentMode() {
      this.active = false;
      this.modeBody = "Dependent";
    },
    clearSelection() {
      this.$refs["dependent-detail"].clearSelection();
    },

    /**
     * Gọi service thông người phụ thuộc để bind lên dialog
     * created by NQTrung 26/09/2020
     */
    async getEditDependent(id) {
      var res = await declaration.getDeclarationDetailDependentByID(id);
      if (res.data.Success && res.data.MISACode == 200) {
        this.DependentSelected = res.data.Data;
      } else {
        this.$_Notification.error(
          "Thất bại",
          " Lấy danh sách người phụ thuộc thất bại.Vui lòng kiểm tra lại."
        );
      }
    },

    /**
     * Sự kiện khi thao tác vào 1 hàng
     * Created by NQTrung 25/9/2020
     * */
    async getRowAction(val) {
      if (val.value == 1) {
        this.listDelete = [];
        this.listDelete.push(val.row.DeclarationDetailDependentID);
        this.select = [];
        this.select.push(val.row);
        this.openPopup();
      } else {
        this.formMode = 0;
        await this.getEditDependent(val.row.DeclarationDetailDependentID);
        this.openAddDialog();
      }
    },
    // Hiển thị popup xác nhận xóa
    //Sửa content cảnh báo xóa
    openPopup() {
      var title = "Xóa người phụ thuộc ";
      var contentPopup = "";
      if (this.select.length == 1) {
        contentPopup = `Bạn có chắc muốn xóa người phụ thuộc <b>${this.select[0].DependentName}</b> khỏi hồ sơ đăng ký người phụ thuộc không? `;
      } else {
        contentPopup =
          "Danh sách người phụ thuộc được chọn sẽ bị xóa khỏi hệ thống. Bạn có chắc chắn muốn xóa không?";
      }
      var buttons = [
        {
          text: "Có",
          class: "btn-delete",
          callback: this.deleteSelectedRow,
        },
        {
          text: "Không",
          class: "btn-grey",
          callback: () => {},
        },
      ];
      this.$_Popup.delete(title, contentPopup, buttons);
    },

    /**
     * Xóa người phụ thuộc
     * created by NQTrung 22/9/2020
     */
    async deleteSelectedRow() {
      this.numberRecordDeleted = 0;
      var res = await declaration.deleteDeclarationDetailDependent(
        this.listDelete
      );
      if (res.data.Success && res.data.MISACode == 200) {
        this.getdataDialog();
        this.$_Notification.success(
          "Thông báo",
          "Xóa người phụ thuộc thành công"
        );
        this.isCheckRow = false;
        this.changeButton = true;
      } else {
        this.$_Notification.error("Thông báo", "Xóa người phụ thuộc thất bại");
      }
    },
    /**
     * Hàm bắt sự kiện khi click vào nút check box
     *
     */
    getRowsSelection(rows) {
      this.select = rows;
      if (this.select.length >= 1) {
        var listDp = rows.map(function (item) {
          return item.DeclarationDetailDependentID;
        });
        this.listDelete = [...listDp];
        this.isCheckRow = true;
      } else {
        this.isCheckRow = false;
      }
    },

    /**
     * Hàm xử lý sự kiện sau khi thêm/ sửa người phụ thuộc thành công
     * Created by : NQKHAI(17/11/2020)
     */
    handleSuccess() {
      this.getdataDialog();
      this.changeButton = true;
    },

    /**
     * Hàm xử lý sự kiện click nút hoàn thành trong dialog
     * Created By : NQKHAI(19/11/2020)
     */
    handleFinish() {
      var buttons = [
        {
          text: "Đóng",
          class: "btn-primary",
          callback: () => {},
        },
      ];

      if (this.getLengthData < 1) {
        this.$_Popup.warn(
          "Cảnh báo",
          "Vui lòng thêm mới ít nhất 1 người phụ thuộc trước khi hoàn thành",
          buttons
        );
      } else {
        this.resetForm();
      }
    },
  },
};
</script>

<style lang="scss">
@import "@/styles/typo.scss";
@import "@/styles/variables.scss";
@import "@/styles/variables.scss";
@import "@/styles/typo.scss";
#dependent-detail-dialog {
  border-radius: 5px;
  width: calc(100% - 48px);
  height: calc(100% - 25px);
  margin: 25px 24px 0px 24px;

  .el-dialog__header {
    padding: 0;
    z-index: 99999;
  }

  .el-dialog__body {
    height: calc(100% - 62px);
    padding: 0 24px;

    .company-container {
      height: 100%;

      .add-employee-tab {
        height: 67px;
        border-bottom: 1px solid $background-grey;
        display: flex;
        margin: 0 -24px 0 -24px;
        padding: 0 24px;

        .title {
          font-size: 15px;
          font-family: $_fontbold;
          align-self: center;
        }
      }

      .add-employee-main {
        height: calc(100% - 67px);
        position: relative;

        .add-dependent {
          display: flex;
          margin-left: -24px;
          margin-right: -24px;
          height: 100%;

          .employee-info {
            width: 25%;
            margin-left: 24px;
            margin-top: 24px;

            .detail-info {
              margin-bottom: 20px;
            }
          }

          .main-dependent-register {
            background-color: $background-grey;
            width: 75%;
            justify-content: center;
            display: flex;
            position: relative;

            .img-dependent {
              display: grid;
              top: calc(50% - 25px);
              transform: translateY(-50%);
              position: absolute;

              .btn-add-dependent {
                position: relative;
                justify-content: center;
                display: flex;

                button {
                  width: 188px;
                  border-color: $white;

                  span {
                    color: $hover-primary;
                  }
                }
              }
            }

            .container-table-tab {
              height: 100%;
              width: 100%;

              .table {
                margin: 0 24px;
              }

              .header-list-table {
                padding-top: 24px;
                margin-right: 24px;
                display: flex;
                align-items: center;
                padding-bottom: 16px;

                .title-list-table {
                  margin-left: 24px;
                  color: $hover-primary;
                  font-family: $_fontbold;
                }
              }

              .btn-white-icon span {
                color: $hover-primary;
                font-family: $_fontmedium;
              }

              .del {
                margin: 24px 0 16px 24px;
                
                .button-container {
                  float: left !important;
                }
              }
            }
          }
        }
      }
    }
  }

  .el-select .el-input__inner::placeholder {
    color: $dark !important;
  }
  .el-select-tree .el-input__inner::placeholder {
    color: $dark !important;
  }

  #RelationID {
    margin-top: -3px;
  }
  .label-title {
    margin-bottom: 0 !important;
    font-family: $_fontregular;
    font-weight: normal;
    color: #000000;
    line-height: 22px;
    padding-top: 15px;
    font-size: 13px;
  }
  .del-title {
    position: relative;
    top: 10px;
    left: 10px;
    cursor: pointer;
    color: #243585;
  }
  .header-dialog {
    padding-bottom: 24px;
    margin-right: 24px;
  }
  .istableTab {
    background-color: $background-grey;
    padding-top: 0px !important;
  }
  .float-left {
    float: left;
  }
  #btn-tab {
    span {
      font-weight: bold;
      margin-left: -24px !important;
    }
  }
  #two-step-progress {
    justify-content: center;
  }
  .title-name {
    font-size: 15px !important;
    color: #6f6f6f;
  }
  .e-name {
    font-size: 15px !important;
    color: $dark;
    font-weight: bold;
  }
  .el-form-item {
    margin-bottom: 0px !important;
  }
  .el-form-item__label {
    margin: 0 !important;
  }
  .el-form-item__label {
    margin-bottom: 0 !important;
    font-family: "GoogleSansRegular";
    font-weight: normal;
    color: $dark;
    line-height: 22px;
    padding-top: 15px;
    font-size: 13px;
  }
  .container-tableTab {
    width: 100%;
  }
  .w-100 {
    padding-bottom: 0px !important;
  }
  .middle-date {
    padding-left: 15px;
  }
  .el-form-item.is-required:not(.is-no-asterisk) > .el-form-item__label:before,
  .el-form-item.is-required:not(.is-no-asterisk)
    .el-form-item__label-wrap
    > .el-form-item__label:before {
    right: -13px;
  }
  .btn-expand {
    line-height: 51px;
    button {
      background-color: transparent;
      border: none;
    }
  }

  .company-container {
    .Gettingstartted-title {
      height: 70px;
      text-align: center;
      .company-title {
        font-size: 20px;
        font-weight: bold;
        padding-top: 30px;
      }
    }
    .add-employee-tab {
      .tab-bar {
        height: 40px;
        position: absolute;
        top: 14px;
        z-index: 1;
      }
      .progress .step-progress.complete-progress .progress-number {
        border: 2px solid #03dc2e !important;
        background-color: #ffffff !important;
      }
      .progress .step-progress.complete-progress .tick {
        border-bottom: 2px solid #03dc2e !important;
        border-right: 2px solid #03dc2e !important;
      }
    }
    .add-employee-main {
      .contract-tax .el-form-item__label {
        font-size: 13px !important;
        font-weight: bold !important;
      }
      .el-form {
        overflow: initial;
        margin-top: 4px;
        margin-left: 0px;
        margin-right: 0px;
        position: absolute;
        left: 50%;
        width: 30.6%;
        //  height: calc(100% - 50px);
        transform: translateX(-50%);
      }
    }
  }
  .Add-Emplouee-footer {
    height: 70px;
    //background-color: #F4F4F4;
    position: fixed;
    bottom: 0px;
    width: 100%;
    padding-bottom: 24px;
    .button-footer {
      background-color: $white;
      padding-right: 24px;
      border-top: 1px solid #ccc;
      width: 792px;
      height: 70px;
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      .btn-footer {
        padding: 16px 0px;
        .btn-grey {
          margin-left: 24px;
          border: none !important;
        }
        .btn-primary {
          margin-left: 10px;
        }
        .el-button.is-disabled {
          background-color: #e8eef5 !important;
        }
      }
    }
  }
  .input-number .el-input__inner {
    text-align: left;
  }
  .container-tableTab {
    .infor-depentdent-person {
      height: 71px;
      padding-top: 22px;
      .pb-10 {
        padding-bottom: 10px;
      }
      .color-label {
        color: #454545;
      }
      .color-infor {
        color: $dark;
      }
    }
    .table {
      background-color: $background-grey;
      margin-left: -10px;
      margin-right: -10px;
      padding-top: 16px !important;
    }
  }
  .el-dialog__footer {
    padding: 10px 24px 10px 24px !important;
    background-color: $hover-grey !important;
    .none-boder .btn-seconds {
      border: none !important;
    }
  }

  .el-table thead tr:first-child .cell {
    word-break: break-word;
    white-space: unset;
  }
  .el-button.is-disabled:hover {
    color: #c0c4cc !important;
    cursor: not-allowed;
    background-image: none;
    background-color: #ffffff !important;
    border-color: #ebeef5;
  }
  .tab2-dependent-register {
    padding-top: 5px;
  }
  .label-title2 {
    padding-top: 16px;
  }
  .label-title3 {
    margin-top: -4px;
  }
  .btn-add-dependent {
    button {
      border-color: $white;
    }
  }
  .detail-info-text {
    color: $medium-dark-grey;
  }
  .detail-info-value {
    font-family: $_fontmedium;
  }
  .el-form-item__content .el-radio {
    span {
      font-size: 13px !important;
      color: $hover-dark !important;
    }
  }
}
</style>