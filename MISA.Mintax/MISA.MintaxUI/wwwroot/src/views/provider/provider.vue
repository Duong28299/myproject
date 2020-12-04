<template>
  <div class="provider" style="margin-top: 24px;">
      <el-row class="toolbar-provider">
          <div class="group-btn-delete">             
              <m-btn
              type="btn-primary-icon"
              style="margin-right: 16px"
              name="Nạp"
              :disabled="false"
              :handleClick="getData"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/ico-refresh.png" />
              </template>
            </m-btn> 
            <m-btn
              type="btn-primary-icon"
              style="margin-right: 16px"
              name="Xóa"
              :disabled="disableButtonDel"
              :handleClick="openPopupWarning"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group 18660@2x.png" />
              </template>
            </m-btn>
            <m-btn
              type="btn-primary-icon"
              style="margin-right: 16px"
              name="Sửa"
              :disabled="disableButton"
              :handleClick="openEditDialog"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group 18656@2x.png" />
              </template>
            </m-btn>
            <m-btn
              type="btn-primary-icon"
              style="margin-right: 16px"
              name="Nhân bản"
              :disabled="disableButton"
              :handleClick="openDuplicateDialog"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group 19244.png" />
              </template>
            </m-btn>
            <m-btn
              type="btn-primary-icon"
              style="margin-right: 16px"
              name="Thêm mới"
              :disabled="false"
              :handleClick="openAddDialog"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group 17812.png" />
              </template>
            </m-btn>              
          </div>
        </el-row>
    <el-row class="data-table">
      <div v-if="loading" v-loading="loading"></div>
      <tbl-provider
        v-if="!loading && !isEmpty"
        ref="provider"
        :listColumn="listColumn"
        :dataTable="dataTable"
        @handleSelectionChange="getRowsSelection"
        :loading="false"
        :havePagination="true"
        :haveAction="false"
        :haveCheck="true"
        :pageSizes="pageSizes"
        :layoutPaging="layoutPaging"
        :total="total"
        @pagination="pagination"
        typepage="tờ khai"
      ></tbl-provider>
    </el-row>
    <provider-dialog
    :visiableDL="openDialog"
    @closeDialog="openDialog = $event"
    @success="reload=$event"
    :providerID="providerID"
    :modeEdit="modeEdit"
    :modeDuplicate="modeDuplicate"
    @edit="modeEdit=$event"
    @duplicate ="modeDuplicate = $event"
    />
  </div>
</template>

<script>
import Button from "@/components/buttons";
import Table from "@/components/tables";
import APIProvider from "@/api/etax/provider.js";
import ProviderDialog from "./components/add-provider.vue"
export default {
    components:{
        "m-btn":Button,
        "tbl-provider":Table,
        "provider-dialog":ProviderDialog
    },
    name:"Provider",
    data(){
        return{
          layoutPaging: "prev, pager, next, sizes",
          pageSizes:[15, 25, 50, 100],
            listColumn:[
                {
                    label: "Mã nhà cung cấp",
                    prop: "ProviderCode",
                    propertyName: "ProviderCode",
                    width: "",
                    minWidth: "200",
                    align: "left",
                    type: "",
                    sortable: true,
                },
                {
                    label: "Tên nhà cung cấp",
                    prop: "ProviderName",
                    propertyName: "ProviderName",
                    width: "",
                    minWidth: "300",
                    align: "left",
                    type: "",
                    sortable: true,
                },
                {
                    label: "Loại NCC",
                    prop: "ProviderType",
                    propertyName: "ProviderType",
                    width: "",
                    minWidth: "100",
                    align: "left",
                    type: "",
                    sortable: true,
                },
                {
                    label: "Nhóm NCC",
                    prop: "ProviderGroupName",
                    propertyName: "ProviderGroupName",
                    width: "",
                    minWidth: "300",
                    align: "left",
                    type: "",
                    sortable: true,
                },
                {
                    label: "Số điện thoại",
                    prop: "ProviderPhone",
                    propertyName: "ProviderPhone",
                    width: "",
                    minWidth: "200",
                    align: "left",
                    type: "",
                    sortable: true,
                },
                {
                    label: "Địa chỉ",
                    prop: "ProviderAddress",
                    propertyName: "ProviderAddress",
                    width: "",
                    minWidth: "300",
                    align: "left",
                    type: "",
                    sortable: true,
                },
                {
                    label: "Trạng thái",
                    prop: "Follow",
                    propertyName: "Follow",
                    width: "",
                    minWidth: "200",
                    align: "left",
                    type: "",
                    sortable: true,
              }
            ],
            dataTable:[
                {
                    ProviderCode:"NCC1",
                    ProviderName:"Trần Cảnh Dương",
                    ProviderType:"Tổ chức",
                    ProviderGroupName:"Nhóm TCD",
                    ProviderPhone:"095737294",
                    ProviderAddress:"Nam Định",
                    Follow:"Đang theo dõi"
                }
            ],
            loading:false,
            isEmpty:false,
            openDialog:false,
            reload:false,
            providerID:"",
            disableButton:true,
            modeEdit:false,
            modeDuplicate:false,
            listProviderID:[],
            disableButtonDel:true,
            rowSelected:[],
            total:0,
            panigation:{
              pageSize:15,
              pageIndex:1
            }
        }
    },
    methods:{
      pagination(val) {
        console.log(val);
        this.panigation.pageSize = val.limit;
        this.panigation.pageIndex = val.page;
      },
        getRowsSelection(val){
            this.listProviderID=[];
            this.rowSelected = val;
            if(val.length>1){
                this.disableButton=true;
                this.disableButtonDel=false;
            }                
            if(val.length==1){
                this.disableButton=false;
                this.disableButtonDel=false;
                this.providerID=val[0].ProviderID;
            }
            if(val.length==0)
            {
                this.disableButton=true;
                this.disableButtonDel=true;
            }
            if(val.length>0){
                val.forEach(item=>{
                this.listProviderID.push(item.ProviderID)
            }) 
            }
                       
        },
       async getData(){
           this.loading = true;
            var res = await APIProvider.getProvider(this.panigation);
            if(res.data.Success){
                this.mapData(res.data.Data.providers)
                this.loading = false;
                this.total = res.data.Data.total;
            }else
                this.loading = true;
          this.reload = false;
          this.disableButtonDel = true;
          this.disableButton=true;
        },
        mapData(val){
            var datas=[];
            val.forEach(item => {
                var data = {};
                data.ProviderID = item["ProviderID"];
                data.ProviderCode = item["ProviderCode"];
                data.ProviderName = item["ProviderName"];
                data.ProviderType = item["ProviderType"] == 0?"Tổ chức":"Cá nhân";
                data.ProviderGroupName = item["ProviderGroupName"];
                data.ProviderPhone = item["ProviderPhone"];
                data.ProviderAddress = item["ProviderAddress"];
                data.Follow = item["Follow"]==1?"Ngừng theo dõi":"Đang theo dõi";
                datas.push(data);
            });
            this.dataTable = [...datas];
        },
        openAddDialog(){
            this.openDialog = true;
        },
        openEditDialog(){
            this.openDialog = true;
            this.modeEdit = true;
            this.modeDuplicate = false;
        },
        openDuplicateDialog(){
            this.openDialog = true;
            this.modeDuplicate = true;
            this.modeEdit = false;
        },
        async deleteProvider(){
            var res =await APIProvider.deleteProvider(this.listProviderID);
            if(res.data.Success){
                this.$_Notification.success("Thành công","Xóa thành công");
                this.getData();
            }else{
                this.$_Notification.error("Lỗi","Xóa thất bại");
            }
        },
         openPopupWarning(){
            var title = "Xóa nhà cung cấp";
            var contentPopup = "";
            if (this.rowSelected.length == 1) {
              contentPopup = `Bạn có chắc chắn muốn xóa nhà cung cấp ${this.rowSelected[0].ProviderName} - (${this.rowSelected[0].ProviderCode}) không?`
            } else {
              contentPopup =
                "Các nhà cung cấp được chọn sẽ bị xóa khỏi hệ thống. Bạn có chắc chắn muốn xóa không?";
            }
            var buttons = [
              {
                text: "Có",
                class: "btn-delete",
                callback: this.deleteProvider,
              },
              {
                text: "Không",
                class: "btn-grey",
                callback: () => {},
              },
            ];
            this.$_Popup.delete(title, contentPopup, buttons);
        }
    },
    created(){
        this.getData();
    },
    watch:{
        reload:{
            handler(val){
                if(val)
                    this.getData();
            }
                
        },
        panigation:{
          handler(){
            this.getData();
          },
          deep:true
        }
    }
}
</script>

<style lang="scss">
.provider{
  .el-table__body-wrapper{
    height: 721px !important;
  }
    .toolbar-provider{
        .button-container button{
        border-radius: 0px !important;
        background: #2B3173;
        border-right: 1px solid;
    }
    display: flex;
    background-color: #2B3173;
    }
    
}
</style>