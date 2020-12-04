<template>
<div class="tax-settlement">

    <!-- LƯU Ý KHÔNG ĐƯỢC ĐỔI DÒNG CODE TITLE DƯỚI ĐÂY
    MỌI THẮC MẮC LIÊN HỆ VDTHANG -->
    <!-- Page title -->
    <el-row class="page-header">
        <el-col :span="24">
            <div class="page-title d-flex">
                <h1 style="height:40px;line-height: 40px;"> {{ getTitle }}</h1>
                <m-btn type="btn-white2-onlyicon-big" tooltip="Làm mới" positionTooltip="left" :handleClick="refreshTable">
                    <template v-slot:icon>
                        <img class="icon" src="@/icons/AddNew.svg" style="width: 20px;height: 20px;" />
                    </template>
                </m-btn>
            </div>
        </el-col>
    </el-row>

    <!-- Filter- Bar -->
    <el-row class="filter-bar" justify="space-between" v-if="!isEmpty && !loading">
        <el-col :span="20">
            <el-row :gutter="16" v-if="!isCheckRow">
                <el-col :span="6">
                    <field-input type="input-icon" classicon="icon-search" :placeholder="'Tìm kiếm theo tên tờ khai quyết toán'" @value="filterBar.keyword=$event" :valueInput="filterBar.keyword" :clearable="true"></field-input>
                </el-col>
                <el-col :span="4">
                    <input-dropdown selectID="declaration-type" :list="listDeclarationType" type="select-border-color" label="name" field="value" :placeholder="'Loại tờ khai'" @value="filterBar.declarationType=($event=='' ? null : $event)" :defaultNumber="filterBar.declarationType" :clearable="true"></input-dropdown>
                </el-col>
                <el-col :span="4">
                    <input-dropdown selectID="status" :list="listStatus" type="select-border-color" label="name" field="value" :placeholder="'Trạng thái'" @value="filterBar.declarationStatus=($event=='' ? null : $event)" :defaultNumber="filterBar.declarationStatus" :clearable="true"></input-dropdown>
                </el-col>
            </el-row>
            <el-row :gutter="16" v-if="isCheckRow">
                <div class="group-btn-delete">
                    <m-btn type="btn-white3-onlyicon" tooltip="Kí nộp" style="margin-right:16px" :disabled="disabledDeleteAndSign" :handleClick="sendDeclaration">
                        <template v-slot:icon>
                            <img class="icon" src="@/icons/Group 18667@2x.png" />
                        </template>
                    </m-btn>
                    <m-btn type="btn-white3-onlyicon" style="margin-right:16px" tooltip="Tải xuống">
                        <template v-slot:icon>
                            <img class="icon" src="@/icons/Component 95 – 1.png" />
                        </template>
                    </m-btn>
                    <m-btn type="btn-white3-onlyicon" :iconel="'el-icon-delete'" tooltip="Xóa" :handleClick="openPopup" :disabled="disabledDeleteAndSign">
                        <template v-slot:icon></template>
                    </m-btn>
                    <div style="margin-left:16px" class="deselect-all" @click="clearSelection">Bỏ chọn {{select.length}} tờ khai</div>
                </div>
            </el-row>
        </el-col>
        <el-col :span="4" v-if="!isCheckRow">
            <m-btn type="btn-primary-icon" name="Tạo mới hồ sơ" :handleClick="openDialog">
                <template v-slot:icon>
                    <img class="icon" src="@/icons/Group 17812.png" />
                </template>
            </m-btn>
        </el-col>
    </el-row>
    <!-- / Filter - Bar -->

    <!--Data Table  -->
    <el-row class="data-table">
        <div v-if="loading" v-loading="loading"></div>
        <tbl-tax-settlement v-if="!isEmpty && !loading" ref="tax-settlement" :listColumn="listColumn" :dataTable="getDataTable" :haveCheck="haveCheck" :pageSizes="pageSizes" :havePagination="true" :total="numberOfRecord" :layoutPaging="layoutPaging" :chipArray="chipArray" :tooltip="true" :listaction="listaction" :destiArrStatus="destiArrStatus" destiStatus="DeclarationStatus" typepage="tờ khai" @pagination="pagination" @selection="getSelection" @handleSelectionChange="getRowsSelection" @handleSelectAll="handleSelectAll" @handleRowDBLClick="handleRowDBLClick" :loading="false" @row-action="getRowAction"></tbl-tax-settlement>
    </el-row>
    <!-- Data Table -->

    <!--IF DATA Empty -->
    <div class="temporary-mode-btn" v-if="numberOfRecord == 0 && !isEmpty && !loading">
        <img src="@/assets/images/Group 19459.png" alt="not have data" style="position: relative;" />
        <span>Không tìm thấy kết quả phù hợp</span>
    </div>

    <div class="main-empty" v-if="isEmpty">
        <img src="@/assets/images/Group 19790.png" />
        <m-btn type="btn-primary-icon" name="Tạo mới tờ khai quyết toán" :handleClick="openDialog">
            <template v-slot:icon>
                <img class="icon" src="@/icons/Group 17812.png" />
            </template>
        </m-btn>
    </div>

    <!--/IF DATA Empty -->

    <!-- Dialog thêm tờ khai -->
    <AddDeclarationSettlement :visiableDL="visiableAdd" @reload="loadTable=$event" @closeDialog="visiableAdd=$event" />
    <!-- // dialog thêm tờ khai -->
</div>
</template>

<script>
import Button from "@/components/buttons";
import fieldinput from "@/components/inputs";
import Dropdown from "@/components/select";
import Table from "@/components/tables";
import DeclarationStatus from "@/enums/chipEnum.js";
import actionTable from "@/enums/actionTable.js";
import declarationSettlement from "@/api/etax/declaration-settlement.js";
import DeclarationSettlementEnum from "@/enums/declaration-settlement.js";
import AddDeclarationSettlement from "./components/add-declaration-settlement-dialog";
import Cookies from 'js-cookie';

import signDeclaration from "@/mixins/signDeclaration.js";

export default {
    name: "TaxSettlement",
    components: {
        "m-btn": Button,
        "field-input": fieldinput,
        "input-dropdown": Dropdown,
        "tbl-tax-settlement": Table,
        AddDeclarationSettlement
    },
    mixins: [signDeclaration],

    data() {
        return {
            loadTable: false,
            // hiển thị dialog 
            visiableAdd: false,
            // Disable các nút xóa ký nộp
            disabledDeleteAndSign: false,
            // Lọc danh sách
            filterBar: {
                keyword: "", //tên hồ sơ
                declarationType: null, //loại tờ khai
                declarationStatus: null, //trạng thái
                pageIndex: 1,
                pageSize: 25,
            },
            // danh sách cột
            listColumn: [{
                    label: "Tên tờ khai",
                    prop: "DeclarationSettlementName",
                    propertyName: "DeclarationSettlementName",
                    width: "",
                    minWidth: "300",
                    align: "left",
                    type: "",
                    sortable: true,
                },
                {
                    label: "Năm tổng hợp",
                    propertyName: "SettlementYear",
                    width: "",
                    minWidth: "130",
                    align: "left",
                    type: "",
                    prop: "SettlementYear",
                    sortable: true,
                },
                {
                    label: "Thời gian tổng hợp",
                    propertyName: "SettlementTime",
                    width: "",
                    minWidth: "160",
                    align: "left",
                    type: "",
                    prop: "SettlementTime",
                    sortable: true,
                },
                {
                    label: "Loại tờ khai",
                    propertyName: "DelarationType",
                    width: "",
                    minWidth: "120",
                    align: "left",
                    type: "",
                    prop: "DelarationType",
                    sortable: true,
                },
                {
                    label: "Lần nộp",
                    propertyName: "DeclarationTime",
                    width: "",
                    minWidth: "120",
                    align: "left",
                    type: "",
                    prop: "DeclarationTime",
                    sortable: true,
                },
                {
                    label: "Ngày nộp",
                    propertyName: "SignDate",
                    width: "",
                    minWidth: "120",
                    align: "center",
                    type: "date",
                    prop: "CreatedDate",
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
            ],
            // dữ liệu bảng
            dataTable: [],

            haveCheck: true,
            // số lượng phân trang
            pageSizes: [25, 50, 75, 100],
            // tổng bản ghi
            numberOfRecord: 0,
            layoutPaging: "prev, pager, next, sizes",
            // trạng thái tờ khai theo enum
            chipArray: "TAX_REGISTER",
            // Dữ liệu tờ khai quyết toán của tổ chức có rỗng?
            isEmpty: false,
            // Danh sách đã chọn
            select: [],
            // có cột check hay không
            isCheckRow: false,
            // danh sách các action [xóa]
            listaction: [],
            // danh sách trạng thái [lọc]
            listStatus: [],
            // danh sách loại tờ khai [lọc]
            listDeclarationType: [],
            // danh sách id để xóa
            listIdDelete: [],
            // các mã trạng thái không được xóa
            destiArrStatus: [11, 12, 10, 2],
            // đang load dữ liệu hay không?
            loading: true,

        };
    },
    methods: {
        /**
         * Hàm reset lại bảng 
         * created by: NVANH 15.10.2020
         */
        refreshTable() {
            this.loading = true;
            this.filterBar.keyword = "";
            this.filterBar.declarationType = null;
            this.filterBar.declarationStatus = null;
            (this.filterBar.pageIndex = 1), (this.filterBar.pageSize = 15), this.getData();
        },

        /**
         * Bỏ chọn tất cả các dòng trong bảng
         * createdby nvanh 15.10.2020
         */
        clearSelection() {
            this.$refs["tax-settlement"].clearSelection();
        },

        pagination(val) {
            this.filterBar.pageSize = val.limit;
            this.filterBar.pageIndex = val.page;
            this.getData();
        },

        getSelection() {},

        getDeclaratonTime(startMonth, finishMonth, year) {
            const startM = startMonth < 10 ? `0${startMonth}` : `${startMonth}`;
            const finishM = finishMonth < 10 ? `0${finishMonth}` : `${finishMonth}`;
            return `${startM}/${year} - ${finishM}/${year}`;
        },

        /**
         * Mở dialog thêm mới
         * createdby NVANH 27.10.2020
         */
        openDialog() {
            this.visiableAdd = true;
        },
        /**
         * Xóa tất cả bản ghi được chọn
         * Created by: NVANH 15.10.2020
         */
        async deleteSelectedRow() {
            
            // Xóa 1 tờ khai
            if (this.listIdDelete.length === 1) {
                const deletedDeclaration = this.select[0];
                const res = await declarationSettlement.deleteDeclarationSettlement(this.listIdDelete);
                const checkSuccess = res.data.Data.some(item =>
                    item == DeclarationSettlementEnum.DeleteResult.SuccessfulDeletionDeclaration);
                if (res.data.Success && res.data.MISACode == 200 && checkSuccess) {
                    this.getData(); //load lại data
                    this.$_Notification.success("Thông báo", `Xóa tờ khai ${deletedDeclaration.DeclarationSettlementName} thành công`);
                } else {
                    this.$_Notification.error("Thông báo", `Xóa tờ khai ${deletedDeclaration.DeclarationSettlementName} thất bại`);
                }
            }
            //Xóa nhiều tờ khai
            else {
                var checkProfileStatus = this.select.some((item) => item.DeclarationStatus !== 1);
                // Có tờ khai trạng thái khác "Chưa gửi"
                if (checkProfileStatus) {
                    var res = await declarationSettlement.deleteDeclarationSettlement(this.listIdDelete);
                    if (res.data.Success && res.data.MISACode == 200) {
                        this.getData(); //load lại data
                        // lấy số thành công / thất bại
                        const deleteSuccess = res.data.Data.filter(item =>
                            item == DeclarationSettlementEnum.DeleteResult.SuccessfulDeletionDeclaration).length;
                        const deleteFail = res.data.Data.filter(item =>
                            item != DeclarationSettlementEnum.DeleteResult.SuccessfulDeletionDeclaration).length;

                        const totalrecord = res.data.Data.length;
                        var contentNotifi = `Xóa thành công ${deleteSuccess}/${totalrecord} tờ khai.  
              Xóa không thành công ${deleteFail}/${totalrecord} tờ khai`;
                        this.$_Notification.success("Thông báo", contentNotifi);
                    } else {
                        this.$_Notification.error("Thông báo", "Đã xóa tờ khai thất bại.");
                    }
                }
                // Tất cả tờ khai đều trạng thái chưa gửi 
                else {
                    var respon = await declarationSettlement.deleteDeclarationSettlement(this.listIdDelete);
                    if (respon.data.Success && respon.data.MISACode == 200) {
                        this.getData(); //load lại data
                        const deleteSuccess = respon.data.Data.filter(item =>
                            item == DeclarationSettlementEnum.DeleteResult.SuccessfulDeletionDeclaration).length;
                        this.$_Notification.success("Thông báo", `Xóa thành công ${deleteSuccess} tờ khai`);
                    } else {
                        this.$_Notification.error("Thông báo", "Đã xóa tờ khai thất bại.");
                    }
                }
            }
        },

        /**
         * Hiển thị popup cảnh báo khi xóa dữ liệu
         * created by: NVANH 15.10.2020
         */
        openPopup() {
            var title = "Xóa tờ khai quyết toán thuế";
            var contentPopup = "";
            if (this.select.length == 1) {
                contentPopup = `Bạn có chắc chắn muốn xóa <b>${this.select[0].DeclarationSettlementName}</b> không?`;
            } else {
                contentPopup = "Bạn có chắc chắn muốn xóa toàn bộ tờ khai đã chọn hay không?";
            }
            var buttons = [{
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
         * Hàm bắt sự kiện click vào cột checkbox
         * @param rows mảng các hàng được chọn
         */
        getRowsSelection(rows) {
            
            this.disabledDeleteAndSign = false;
            if (rows.length > 0) {
                var checkProfileStatus = rows.every((item) => item.DeclarationStatus !== 1); //kiểm tra xem tất cả các dòng được chọn có declarationStatus != 1 không?
                if (checkProfileStatus) {
                    this.isCheckRow = true;
                    this.disabledDeleteAndSign = true;
                } else if (!checkProfileStatus) {
                    var listId = rows.map(function (item) {
                        // if (item.DeclarationStatus == 1) {
                        //   return item.DeclarationSettlementID;
                        // }
                        return item.DeclarationSettlementID;
                    });
                    var filterId = listId.filter((item) => item != undefined);
                    this.listIdDelete = [...filterId];
                    this.select = rows;
                    this.isCheckRow = true;
                }
            } else this.isCheckRow = false;
        },

        async getRowAction(val) {
            if (val.value == 1) {
                if (val.row.DeclarationStatus != 1) {
                    return this.$_Notification.error("Thông báo",
                        `Không thể xóa tờ khai có trạng thái ${DeclarationStatus.findInArray(val.row.DeclarationStatus)}`);
                }
                this.listIdDelete = [];
                this.listIdDelete.push(val.row.DeclarationSettlementID);
                this.select = [];
                this.select.push(val.row);
                this.openPopup();
            }
        },
        handleSelectAll() {},

        /**
         * Chuyển sang trang detail khi doubleclick vào 1 dòng
         * @param val object chứa dữ liệu của 1 bản ghi
         */
        handleRowDBLClick(val) {
            if (Cookies.get('isCollapse') != null)
                Cookies.remove('isCollapse')
            Cookies.set('isCollapse', false);

            if (Cookies.get('activeTab') != null)
                Cookies.remove('activeTab')
            Cookies.set('activeTab', "gentab");

            if (sessionStorage.getItem("objectName") !== null || sessionStorage.getItem("declarationStatus")) {
                sessionStorage.clear();
            }
            // tạo sessionStorage
            sessionStorage.setItem("objectName", val.DeclarationSettlementName);
            //set DeclarationStatus để dùng bên trang detail
            sessionStorage.setItem("declarationStatus", val.DeclarationStatus);
            this.$router.push({
                name: "TaxSettlementDetail",
                params: {
                    id: val.DeclarationSettlementID,
                    activeTab:true
                },
            });
        },

        /**
         * thực hiện gọi service đẻ lấy dánh sách hồ sơ đăng ký
         */
        async getData() {
            var res = await declarationSettlement.getDeclarationSettlement(this.filterBar);
            if (res.data.Success && !res.data.Data.IsEmpty) {
                this.mapdata(res.data.Data.SettlementDeclarationLists);
                this.isEmpty = res.data.Data.IsEmpty;
                this.numberOfRecord = res.data.Data.TotalSettlementDeclaration;
                this.loading = false;
            } else {
                this.loading = false;
                this.isEmpty = res.data.Data.IsEmpty;
                this.loading = false;
                this.dataTable = [];
                this.numberOfRecord = res.data.Data.TotalSettlementDeclaration;
            }
        },

        /**
         * map dữ liệu vào table
         * @param val mảng dữ liệu lấy từ service
         */
        mapdata(val) {
            var datas = [];
            val.forEach((item) => {
                var data = {};
                data.DeclarationSettlementID = item["DeclarationSettlementId"];
                data.OrganizationID = item["OrganizationID"];
                data.DeclarationSettlementName = item["DeclarationSettlementName"];
                data.SettlementYear = item["SettlementYear"];
                data.DelarationType = DeclarationSettlementEnum.findInArray(item["DelarationType"]);
                data.DeclarationTime = item["NumberOfDeclaration"];
                data.CreatedDate = item["CreatedDate"] ? item["CreatedDate"] : "";
                data.SignDate = item["SignDate"] ? item["SignDate"] : "";
                data.DeclarationStatus = item["DeclarationStatus"];
                data.SettlementTime = this.getDeclaratonTime(item['StarSettlementMonth'], item['FinishSettlementMonth'], item["SettlementYear"]);
                datas.push(data);
            });
            this.dataTable = [...datas];
            this.loading = false;
        },

        /**
         * Gửi tờ khai thuế
         * created by nvbinh2 11.11.2020
         */
        async sendDeclaration() {
            this.dataSign = this.select;
            this.registerByMTAX();
        },
        /**
         * Khởi tạo giá trị đối với mixin kí tờ khai
         * created by nvbinh2 11.11.2020
         */
        initDataMixins() {
            this.keyProperty = "DeclarationSettlementID";
            this.mode = "DeclarationSettlement";
            this.nameofDeclaration = "DeclarationSettlementName"
            this.callReloadPage = async function(){ await this.getData()};


        }
    },

    computed: {
        checkData() {
            return this.dataTable.length;
        },
        getDataTable() {
            return this.dataTable;
        },
        getTitle() {
            return this.$route.meta.title;
        },
    },
    created() {

        this.initDataMixins();
        this.listStatus = DeclarationStatus.TAX_REGISTER;
        this.listDeclarationType = DeclarationSettlementEnum.DeclarationType;
        this.listaction = actionTable.DECLARATION_SETTLEMENT;
        this.getData();
    },

    watch: {
        loadTable: {
            async handler(val) {
                if (val)
                    await this.getData();
                this.loadTable = false;
            }
        },
        filterBar: {
            async handler() {
                await this.getData();
            },
            deep: true,
        },
    },
};
</script>

<style lang="scss">
@import "@/styles/typo.scss";
@import "@/styles/variables.scss";
@import "@/styles/main-page.scss";

.tax-settlement {
    @include main-page;    
}
</style>
