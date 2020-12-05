<template>
<div class="new-declaration">
    <el-form ref="form" :model="declaration" label-width="120px" label-position="top">
        <dialogel :titleDialog="title" :dialogFormVisible="dialogFormVisible" @close-dialog="getClose" xl="12,13" lg="12,13" md="11,12" sm="4,3" xs="7,8" :close-on-click-modal="false">
            <template v-slot:body>
                <div class="tab-bar">
                    <button-icon type="btn-tab" class="tab-declaration" :active="tab1" name="Tờ khai" :handleClick="changetab1" />
                    <div class="btn-icon-rightmr">
                        <!-- Tờ khai 05 ko có nút sửa -->
                        <!-- <button-icon
                  style="margin-left: 16px;"
                  :type="'btn-white-icon'"
                  :name ="'Sửa'"
                  :handleClick="Edit"
                >
                <template v-slot:icon>
                  <img class="icon" src="@/icons/Group 18656.png"/>
                </template>
              </button-icon>-->
                        <button-icon style="margin-left: 16px" :type="'multi-button2'" :name="'Tải về'" :dataDropdown="arrayDownload" :handleClick="downloadXML">
                            <template v-slot:icon>
                                <img class="icon" src="@/icons/Component 95 – 1.png" />
                            </template>
                            <!-- <template v-slot:manu>
                    <el-dropdown-item v-for="(item, index) in arrayDownload" :key="index" style="padding: 0px">
                      <el-button class="btn-white2" @click="onClick" >{{ item.name }}</el-button>
                    </el-dropdown-item>
                </template>-->
                        </button-icon>
                        <button-icon v-if="modifiedTime == 0 && isSign == true" style="margin-left: 16px" :type="'btn-primary-icon'" :name="'Ký nộp qua mTax'" :handleClick="sendDeclaration">
                            <template v-slot:icon>
                                <img class="icon" src="@/icons/Group 18580.png" />
                            </template>
                        </button-icon>
                    </div>
                </div>
                <div class="scroll">
                    <el-row :gutter="0" v-if="tab1">
                        <el-col :span="24" style="padding: 0 24px">
                            <el-row :gutter="24">
                                <el-col :span="24">
                                    <h2 style="color: #0e1d61">
                                        Tờ khai khấu trừ thuế thu nhập cá nhân - Mẫu 05/KK-TNCN
                                    </h2>
                                </el-col>
                            </el-row>
                            <el-form-item>
                                <el-row :gutter="24">
                                    <el-col :span="24">
                                        <span>(Áp dụng cho tổ chức, cá nhân trả các khoản thu nhập
                                            tiền lương, tiền công)</span>
                                    </el-col>
                                </el-row>
                                <el-row :gutter="16" style="align-item: center; display: flex">
                                    <el-col :xs="10" :sm="8" :md="5" :lg="5" style="align-items: center; display: flex">
                                        <span>[01] Kì tính thuế {{ periodTax }}</span>
                                    </el-col>
                                    <el-col :xs="10" :sm="8" :md="5" :lg="5" v-if="firstTime" style="align-items: center; display: flex">
                                        <span>| [02] Lần đầu</span>
                                    </el-col>
                                    <el-col :xs="8" :sm="6" :md="3" :lg="3">
                                        <span v-if="modifiedTime == 0">
                                            <el-tag type="info">Chưa nộp</el-tag>
                                        </span>
                                        <span v-if="modifiedTime == 1">
                                            <el-tag type="warning">Đã nộp</el-tag>
                                        </span>
                                        <span v-if="modifiedTime == 2">
                                            <el-tag type>Đã tiếp nhận</el-tag>
                                        </span>
                                        <span v-if="modifiedTime == 3">
                                            <el-tag type="danger">Không chấp nhận</el-tag>
                                        </span>
                                        <span v-if="modifiedTime == 4">
                                            <el-tag type="success">Chấp nhận</el-tag>
                                        </span>
                                    </el-col>
                                </el-row>
                                <el-row :gutter="24">
                                    <el-col :span="24">
                                        <h3>THÔNG TIN CHUNG</h3>
                                    </el-col>
                                </el-row>
                                <el-col :span="24">
                                    <el-row :gutter="24">
                                        <el-col :xs="8" :sm="6" :md="5" :lg="4">
                                            <span>[04] Tên người nộp thuế:</span>
                                        </el-col>
                                        <el-col :xs="8" :sm="8" :md="10" :lg="10">
                                            <span>{{ Organization.OrganizationName }}</span>
                                        </el-col>
                                    </el-row>
                                    <el-row :gutter="24">
                                        <el-col :xs="6" :sm="6" :md="5" :lg="4">
                                            <span>[05] Mã số thuế:</span>
                                        </el-col>
                                        <el-col :xs="8" :sm="8" :md="10" :lg="10">
                                            <span>{{ Organization.OrganizationTaxNo }}</span>
                                        </el-col>
                                    </el-row>
                                    <el-row :gutter="24">
                                        <el-col :xs="10" :sm="8" :md="5" :lg="4">
                                            <span>[12] Tên đại lý thuế(nếu có):</span>
                                        </el-col>
                                        <el-col :xs="8" :sm="8" :md="10" :lg="10">
                                            <span>{{ declarationData.OrganizationName }}</span>
                                        </el-col>
                                    </el-row>
                                    <el-row :gutter="24">
                                        <el-col :xs="8" :sm="6" :md="5" :lg="4">
                                            <span>[13] Mã số thuế đại lý:</span>
                                        </el-col>
                                        <el-col :xs="8" :sm="8" :md="10" :lg="10">
                                            <span>{{ declarationData.OrganizationTaxNo }}</span>
                                        </el-col>
                                    </el-row>
                                </el-col>
                            </el-form-item>
                        </el-col>
                        <el-col style="
                  padding: 24px;
                  margin-top: 24px;
                  background-color: #e5e5e5;
                ">
                            <el-table id="tbl-declaration" :data="tableData" :span-method="objectSpanMethod" :cell-style="setTableStyle" border style="width: 100%; border-radius: 8px">
                                <el-table-column prop="index" label="STT" width="50"></el-table-column>
                                <el-table-column prop="norm" label="Chỉ tiêu"></el-table-column>
                                <el-table-column prop="normCode" label="Mã chỉ tiêu" width="140" align="center"></el-table-column>
                                <el-table-column prop="unit" label="Đơn vị tính" width="140" align="center"></el-table-column>
                                <el-table-column prop="peopleOrMoney" label="Số người/số tiền" width="140" align="center"></el-table-column>
                            </el-table>
                        </el-col>

                        <el-row style="padding: 0 24px">
                            <el-col :span="24">
                                <el-row :gutter="24">
                                    <el-col :span="24">
                                        <h3 class="tittle-2">NHÂN VIÊN ĐẠI LÝ THUẾ</h3>
                                    </el-col>
                                </el-row>
                                <el-form-item>
                                    <el-col :span="12">
                                        <el-col>
                                            <el-col :lg="8" :md="8" :sm="10">
                                                <span>Họ và tên:</span>
                                            </el-col>
                                            <span>{{ Organization.EmployeeName }}</span>
                                        </el-col>
                                        <el-col>
                                            <el-col :lg="8" :md="8" :sm="14">
                                                <span>Chứng chỉ hành nghề số:</span>
                                            </el-col>
                                            <span>{{ Organization.PracticingCertifacateNo }}</span>
                                        </el-col>
                                    </el-col>
                                    <el-col :span="12">
                                        <el-col>
                                            <el-col :lg="4" :md="4" :sm="4" :xs="6">
                                                <span>Người ký:</span>
                                            </el-col>
                                            <span>{{ Organization.SignBy }}</span>
                                        </el-col>
                                        <el-col>
                                            <el-col :lg="4" :md="4" :sm="4" :xs="6">
                                                <span>Ngày ký:</span>
                                            </el-col>
                                            <span>{{ signDate }}</span>
                                        </el-col>
                                    </el-col>
                                </el-form-item>
                            </el-col>
                        </el-row>
                    </el-row>
                </div>
                <el-row :gutter="0" v-if="tab2">
                    <el-col :span="12"></el-col>
                </el-row>
            </template>
            <template v-slot:footer class="dialog-footer">
                <el-row class="border-top">
                    <el-col :span="24">
                        <button-icon name="Đóng" type="btn-primary" class="ml-10" :handleClick="handleClick" @AddAndOut="onSubmit('form')"></button-icon>
                    </el-col>
                </el-row>
            </template>
        </dialogel>
    </el-form>
</div>
</template>

<script>
import dialogel from "@/components/dialog";
import Button from "@/components/buttons";
import {
    formatMoney,
    formatDate
} from "@/utils/index.js";

import signDeclaration from "@/mixins/signDeclaration"

export default {
    props: {
        // Thông tin của tờ kê khai thuế
        declarationData: {
            type: Object,
            default: null,
        },
        dialogFormVisible: {
            type: Boolean,
            default: false,
        },
        handleClickProp: {
            type: Function,
            default: () => {},
        },
    },
    mixins: [signDeclaration],
    data() {
        return {
            arrayDownload: [{
                    icon: "",
                    name: "Tải Excel",
                    callback: () => {},
                },
                {
                    icon: "",
                    name: "Tải XML",
                    callback: () => {},
                },
                {
                    icon: "",
                    name: "Tải PDF",
                    callback: () => {},
                },
            ],
            declaration: {},
            tab1: true,
            tab2: false,
            loading: false,
            activeName: "first",
            title: "CHI TIẾT TỜ KHAI THUẾ TNCN",
            dataButton: [
                // Thông tin dropdown button
                {
                    name: "Lưu và đóng",
                    nameVal: "AddAndOut",
                },
                {
                    name: "Lưu và thêm mới",
                },
            ],

            // Dữ liệu từ Organization
            Organization: {
                type: Object,
                default: "",
            },

            // Kỳ tính thuế
            periodTax: "",

            /**
             * Số thứ tự tờ khai
             */

            // lần đầu
            firstTime: {
                type: Boolean,
                default: false,
            },
            // Lần bổ sung
            modifiedTime: "",

            // Biến show ký nộp tờ khai
            isSign: true,

            // Ngày ký
            signDate: {
                type: String,
                default: "",
            },

            // Dữ liệu bảng kê khai thuế
            tableData: [{
                    index: "1",
                    norm: "Tổng số người lao động",
                    normCode: "[21]",
                    unit: "Người",
                    peopleOrMoney: 0,
                },
                {
                    index: "",
                    norm: "Trong đó: cá nhân cư trú có hợp đồng lao động",
                    normCode: "[22]",
                    unit: "Người",
                    peopleOrMoney: 0,
                },
                {
                    index: "2",
                    norm: "Tổng số cá nhân đã khấu trừ thuế [23]=[24]+[25]",
                    normCode: "[23]",
                    unit: "Người",
                    peopleOrMoney: 0,
                },
                {
                    index: "2.1",
                    norm: "Cá nhân cư trú",
                    normCode: "[24]",
                    unit: "Người",
                    peopleOrMoney: 0,
                },
                {
                    index: "2.2",
                    norm: "Cá nhân không cư trú",
                    normCode: "[25]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "3",
                    norm: "Tổng thu nhập chịu thuế (TNCT) trả cho cá nhân [26]=[27]+[28]",
                    normCode: "[26]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "3.1",
                    norm: "Cá nhân cư trú",
                    normCode: "[27]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "3.2",
                    norm: "Cá nhân không cư trú",
                    normCode: "[28]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "4",
                    norm: "Tổng TNCN trả cho các nhân thuộc diện khấu trừ thuế [29]=[30]+[31]",
                    normCode: "[29]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "4.1",
                    norm: "Cá nhân cư trú",
                    normCode: "[30]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "4.2",
                    norm: "Cá nhân không cư trú",
                    normCode: "[31]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "5",
                    norm: "Tổng số thuế thu nhập cá nhân đã khấu trừ [32]=[33]+[34]",
                    normCode: "[32]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "5.1",
                    norm: "Cá nhân cư trú",
                    normCode: "[33]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "5.3",
                    norm: "Cá nhân không cư trú",
                    normCode: "[34]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "6",
                    norm: "Tổng TNCN từ tiền phí mua bảo hiểm nhân thọ, bảo hiểm không bắt buộc của doanh nghiệp bảo hiểm không thành lập tại Việt Nam cho người lao động",
                    normCode: "[35]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
                {
                    index: "7",
                    norm: "Tổng số TNCN đã khấu trừ trên tiền phí mua bảo hiểm nhân thọ, bảo hiểm không bắt buộc khác của doanh nghiệp bảo hiểm không thành lập tại Việt Nam cho người lao động",
                    normCode: "[36]",
                    unit: "VNĐ",
                    peopleOrMoney: 0,
                },
            ],
            TaxCode: "0101243150999",
            files: [],
            isNotExpired: false,
        };
    },
    components: {
        dialogel,
        "button-icon": Button,
    },
    methods: {
        /**
         * hàm chuyển sang tab 1
         */
        changetab1() {
            this.tab1 = true;
            this.tab2 = false;
        },
        /**
         * hàm chuyển sang tab 2
         */
        changetab2() {
            this.tab2 = true;
            this.tab1 = false;
        },
        // hàm bắt sự đóng form
        handleClick() {
            this.$emit("close-dialog", false);
        },

        // Hàm đóng dialog
        getClose() {
            this.$emit("close-dialog", false);
        },

        /**
         * Truyền giá trị ứng với từng hàng trong bảng
         * created by tvphi (15/7/2020)
         */
        getDeclaration(val) {
            this.tableData[0].peopleOrMoney =
                val.DeclarationPartials[0].NumberOfEmployee;
            this.tableData[1].peopleOrMoney =
                val.DeclarationPartials[0].NumberOfResidentContract;
            this.tableData[2].peopleOrMoney =
                val.DeclarationPartials[0].NumberOfEmployeeDeductResident +
                val.DeclarationPartials[0].NumberOfEmployeeDeductNonResident;
            this.tableData[3].peopleOrMoney =
                val.DeclarationPartials[0].NumberOfEmployeeDeductResident;
            this.tableData[4].peopleOrMoney =
                val.DeclarationPartials[0].NumberOfEmployeeDeductNonResident;
            this.tableData[5].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalIncomeTaxResident +
                val.DeclarationPartials[0].TotalIncomeTaxNonResident,
                ","
            );
            this.tableData[6].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalIncomeTaxResident,
                ","
            );
            this.tableData[7].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalIncomeTaxNonResident,
                ","
            );
            this.tableData[8].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalIncomeTaxDeductedResident +
                val.DeclarationPartials[0].TotalIncomeTaxDeductedNonResident,
                ","
            );
            this.tableData[9].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalIncomeTaxDeductedResident,
                ","
            );
            this.tableData[10].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalIncomeTaxDeductedNonResident,
                ","
            );
            this.tableData[11].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalTaxResident +
                val.DeclarationPartials[0].TotalTaxNonResident,
                ","
            );
            this.tableData[12].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalTaxResident,
                ","
            );
            this.tableData[13].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalTaxNonResident,
                ","
            );
            this.tableData[14].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalIncomeTaxFromInssurance,
                ","
            );
            this.tableData[15].peopleOrMoney = formatMoney(
                val.DeclarationPartials[0].TotalTaxFromInssurance,
                ","
            );
        },

        /**
         * Hàm tải về tờ khai thuế theo Excel
         */
        edit() {
        },

        /**
         * Hàm tải về tờ khai thuế theo XML
         */
        downloadXML() {
        },

        /**
         * Hàm đóng popup
         */
        closeDialog() {
            this.getClose();
        },

        /**
         * Hàm bắt nút hủy
         */
        exitSubmit() {
            this.buttonDialog = [{
                    text: "Không",
                    class: "btn-grey",
                    callback: () => {},
                },
                {
                    text: "Có",
                    class: "btn-primary",
                    callback: this.closeDialog,
                },
            ];
            this.$_Popup.warn(
                "Cảnh báo",
                "Thông tin vừa nhập liệu sẽ không được lưu lại. Bạn có chắc chắn muốn thoát không?",
                this.buttonDialog
            );
        },

        /**
         * Hàm gộp 2 hàng của cột 1 và hàng 1 trong bảng
         * @param {Chỉ số hàng với 0 ứng với hàng đầu tiên bên dưới hàng chứa tên cột} rowIndex
         * @param {Chỉ số cột với 0 ứng với cột đầu tiên} columnIndex
         * created by tvphi (12/8/2020)
         */
        objectSpanMethod({
            rowIndex,
            columnIndex
        }) {
            if (columnIndex === 0) {
                if (rowIndex === 0) {
                    return {
                        rowspan: 1,
                        colspan: 1,
                    };
                }
                if (rowIndex === 1) {
                    return {
                        rowspan: 1,
                        colspan: 1,
                    };
                }
                if (rowIndex > 1) {
                    return {
                        rowspan: 1,
                        colspan: 1,
                    };
                } else {
                    return {
                        rowspan: 0,
                        colspan: 0,
                    };
                }
            }
        },
        /**
         * Hàm căn chỉnh text cho các ô trong bảng
         * @param {Chỉ số hàng với 0 ứng với hàng đầu tiên bên dưới hàng chứa tên cột} rowIndex
         * @param {Chỉ số cột với 0 ứng với cột đầu tiên} columnIndex
         * created by tvphi (12/8/2020)
         */
        setTableStyle({
            rowIndex,
            columnIndex
        }) {
            let boldRow = [0, 2, 5, 8, 11, 14, 15];

            // Những hàng được tô đậm
            if (columnIndex === 0) {
                return boldRow.indexOf(rowIndex) !== -1 ?
                    "font-weight: bold;text-align: center;background-color: #EBF4FF; line-hight:32px;" :
                    "text-align: center";
            } else if (columnIndex === 1) {
                if (boldRow.indexOf(rowIndex) !== -1)
                    return "font-weight: bold;background-color: #EBF4FF;";
                //if(boldRow.indexOf(rowIndex) !== -1) return 'font-weight: bold;background-color: #EBF4FF;';
            } else if (columnIndex === 2) {
                return boldRow.indexOf(rowIndex) !== -1 ?
                    "font-weight: bold;background-color: #EBF4FF;text-align: center" :
                    "text-align: center";
                //return 'text-align: center; font-weight: bold;background-color: #EBF4FF;';
            } else if (columnIndex === 3) {
                return boldRow.indexOf(rowIndex) !== -1 ?
                    "font-weight: bold;background-color: #EBF4FF;text-align: center" :
                    "text-align: center";
                //return 'text-align: center;background-color: #EBF4FF;';
            } else if (columnIndex === 4) {
                return boldRow.indexOf(rowIndex) !== -1 ?
                    "font-weight: bold; text-align: right;background-color: #EBF4FF; padding-right:16px;" :
                    "text-align: right;padding-right:16px;";
            }
        },
        /**
         * Hàm căn chỉnh độ dài cho các ô trong bảng
         * @param {Chỉ số hàng với 0 ứng với hàng đầu tiên bên dưới hàng chứa tên cột} rowIndex
         * @param {Chỉ số cột với 0 ứng với cột đầu tiên} columnIndex
         * created by tvphi (12/8/2020)
         */
        setRowStyle({
            rowIndex
        }) {
            if (rowIndex == 14 || rowIndex == 15)
                return {
                    height: "70px !important"
                };
            else return "";
        },

        /**
         * Hàm truyền tên cho tờ khai thuế
         * created by tvphi (12/8/2020)
         */
        setTitleForDeclaration() {},

        /**
         * Hàm truyền kỳ tính thuế
         * created by tvphi (12/8/2020)
         */
        setTaxPeriod(taxType, periodYear, periodMonthOrQuarter) {
            var taxTime = periodMonthOrQuarter + "/" + periodYear; // Thời gian tính thuế
            this.periodTax =
                taxType == 1 ?
                " Tháng " + taxTime :
                taxType == 2 ?
                " Quý " + taxTime :
                "";
        },

        /**
         * Hàm truyền sô thứ tự của tờ khai thuế
         * @param {Số thứ tự của tờ khai thuế} numberOfDeclaration
         * created by tvphi (12/8/2020)
         */
        setNumberForDelaration(numberOfDeclaration) {
            if (numberOfDeclaration > 1) {
                this.firstTime = false;
                this.modifiedTime = numberOfDeclaration.toString();
            } else {
                this.firstTime = true;
                this.modifiedTime = "";
            }
        },
        /**
         * Gửi tờ khai
         */
        sendDeclaration() {
            this.dataSign.push(this.declarationData);
            this.registerByMTAX();
        },
        /**
         * Khởi tạo giá trị đối với mixin kí tờ khai
         * created by nvbinh2 11.11.2020
         */
        initDataMixins() {
            this.keyProperty = "DeclarationID";
            this.mode = "Declaration";
            this.nameOfDeclaration = "DeclarationName";
        },

        /**
         * Hàm truyền dữ liệu vào bảng kê khai thuế
         * @param {obj chứa thông tin tờ kê khai thuế} declarationData
         * created by tvphi (12/8/2020)
         */
        setData(declarationData) {
            this.setTaxPeriod(
                declarationData.TaxType,
                declarationData.PeriodYear,
                declarationData.PeriodMonthOrQuarter
            ); // Xác định kỳ tính thuế (theo quý hoặc tháng)
            this.setNumberForDelaration(declarationData.NumberOfDeclaration); // Truyền số thứ tự của tờ khai thuế
            //this.signDate = formatDate(this.declarationData.SignDateOr, "dd/mm/yyyy"); // Chuẩn hóa hiển thị cho ngày ký
        },
        /**
         * Hàm truyền dữ liệu vào organization
         * created by tvphi (12/8/2020)
         */
        setDataOr() {
            this.Organization = this.$store.getters.organization;
            this.signDate = formatDate(this.Organization.SignDate, "dd/mm/yyyy"); // Chuẩn hóa hiển thị cho ngày ký
        },
    },
    watch: {
        /**
         * cập nhật lại dữ liệu vào tờ khai
         * created by tvphi (15/7/2020)
         */
        declarationData: {
            handler(val) {
                this.getDeclaration(val);
            },
        },
    },

    /**
     * Hàm truyền giá trị từ prop về bảng
     * created by tvphi (15/7/2020)
     */
    updated() {
        this.setData(this.declarationData); // Truyền giá trị từ props cho bảng thống kê thue
        this.setDataOr();
    },
    created() {
        this.initDataMixins()
    }
};
</script>

<style lang="scss">
@import "@/styles/variables.scss";

.btn-icon-rightmr {
    position: absolute;
    right: 24px;
    top: 9px;

    button {
        height: 40px !important;
    }
}

.mb-16 {
    margin-bottom: 16px;
}

.mb-24 {
    margin-bottom: 24px;
}

.scroll {
    height: 550px;
    overflow-y: auto;
    overflow-x: hidden;
}

.hideSidebar {
    .new-declaration {
        .el-loading-parent--relative {
            position: absolute !important;
            top: -62px;
            left: -70px;
            height: 100vh;
            width: 100vw;
        }
    }
}

.new-declaration {
    .el-loading-parent--relative {
        position: absolute !important;
        top: -62px;
        left: -217px;
        height: 100vh;
        width: 100vw;
    }

    .tab-bar {
        position: relative;
        height: 58px;
        width: 100%;
        border-bottom: 1px solid $bg-color-3;

        .button-container {
            float: left;

            .tabbar {
                margin: 18px 0 0 24px;
            }
        }

        .btn-tab-active {
            border-bottom-color: $red !important;
        }

        #btn-tab {
            border-bottom: 1px solid $bg-color-3;
            height: 50px !important;
            margin-top: 8px;

            span {
                font-size: 15px !important;
            }
        }
    }

    .el-dialog__title {
        font-size: 20px;
        font-family: $_fontbold;
    }

    .el-dialog__wrapper .el-dialog {
        min-height: 698px;
        overflow: auto;
    }

    .el-dialog__body {
        height: calc(100% - 60px - 48px);
        padding: 0px;
    }
}

#tbl-declaration.el-table {
    .cell {
        white-space: normal !important;
        word-break: break-word;
        padding: 0 10px 0 10px !important;
    }
}
</style>
