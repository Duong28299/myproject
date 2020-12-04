<template>
  <provider-dialog
    :titleDialog="title"
      :id="dialogid"
      :dialogFormVisible="visiableDL"
      :closeonclickmodal="false"
      @close-dialog="getClose()"
      xl="9,10"
      lg="9,10"
      md="9,10"
      sm="4,3"
      xs="7,8"
  >
    <template v-slot:body>
        <el-radio
        class="radio-align"
        :label="0"
        v-model="formPersonal"
        >Tổ chức</el-radio
        >
        <el-radio
        class="radio-align"
        :label="1"
        v-model="formPersonal"
        >Cá nhân</el-radio
        >
        <div v-if="formPersonal==0"
>
            <el-form
        ref="FormProviderOrganization"
        :model="formProviverOrganization"
        label-width="120px"
        label-position="left"
        >
            <el-row>
                <el-col :lg="24" :md="24" :sm="24">
                    <h2>THÔNG TIN CƠ BẢN</h2>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Mã nhà cung cấp"
                                prop="ProviderCode"
                                :rules="[
                                {
                                    required: true,
                                    message:
                                    'Mã nhà cung cấp không được bỏ trống',
                                    trigger: ['change','blur'],
                                },
                                ]"
                                :show-message="validateFormProviverOrganization.ProviderCode"
                            >
                                <field-input
                                id="ProviderCode"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                @hover="validateFormProviverOrganization.ProviderCode = $event"
                                :valueInput="formProviverOrganization.ProviderCode"
                                @value="formProviverOrganization.ProviderCode = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col> 
                        <el-col :lg="12" :md="12" :sm="24" class="provider-name">
                        <el-form-item
                            label="Tên nhà CC"
                            prop="ProviderName"
                            :rules="[
                            {
                                required: true,
                                message:
                                'Tên nhà cung cấp không được bỏ trống',
                                trigger: ['change','blur'],
                            },
                            ]"
                            :show-message="validateFormProviverOrganization.ProviderName"
                        >
                            <field-input
                            id="ProviderName"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            @hover="validateFormProviverOrganization.ProviderName = $event"
                            :valueInput="formProviverOrganization.ProviderName"
                            @value="formProviverOrganization.ProviderName = $event"
                            ></field-input>
                        </el-form-item>
                        </el-col>                     
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row>
                        <el-col :lg="24" :md="24" :sm="24">
                            <el-form-item
                                label="Địa chỉ"
                                prop="ProviderAddress"
                            >
                                <field-input
                                id="ProviderAddress"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverOrganization.ProviderAddress"
                                @value="formProviverOrganization.ProviderAddress = $event"
                                ></field-input>
                                
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Mã số thuế"
                                prop="ProviderTaxNo"
                            >
                                <field-input
                                id="ProviderTaxNo"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverOrganization.ProviderTaxNo"
                                @value="formProviverOrganization.ProviderTaxNo = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Điện thoại"
                            prop="ProviderPhone"
                        >
                            <field-input
                            id="ProviderPhone"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            :valueInput="formProviverOrganization.ProviderPhone"
                            @value="formProviverOrganization.ProviderPhone = $event"
                            ></field-input>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Nhóm nhà CC"
                                prop="ProviderGroupID"
                            >
                                <provider-select
                                    id="ProviderGroupID"
                                    type="label-top"
                                    label="ProviderGroupName"
                                    field="ProviderGroupID"
                                    :defaultString="formProviverOrganization.ProviderGroupID"
                                    :filterable="true"
                                    :list="ProviderGroups"
                                    @value="formProviverOrganization.ProviderGroupID = $event"
                                >                                    
                                </provider-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Số nợ tối đa"
                            prop="Debt"
                        >
                            <input-number
                            id="Debt"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            :valueInput="formProviverOrganization.Debt"
                            @value="formProviverOrganization.Debt = $event"
                            ></input-number>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Hạn nợ (ngày)"
                                prop="DebtTerm"
                            >
                                <input-number
                                id="DebtTerm"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverOrganization.DebtTerm"
                                @value="formProviverOrganization.DebtTerm = $event"
                                ></input-number>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Ngân hàng"
                            prop="BankName"
                        >
                            <field-input
                            id="BankName"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            :valueInput="formProviverOrganization.BankName"
                            @value="formProviverOrganization.BankName = $event"
                            ></field-input>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Số tài khoản"
                                prop="AccountNumber"
                            >
                                <field-input
                                id="AccountNumber"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverOrganization.AccountNumber"
                                @value="formProviverOrganization.AccountNumber = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col> 
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Chi nhánh"
                            prop="BankBranchName"
                        >
                            <field-input
                            id="BankBranchName"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            :valueInput="formProviverOrganization.BankBranchName"
                            @value="formProviverOrganization.BankBranchName = $event"
                            ></field-input>
                        </el-form-item>
                        </el-col>                     
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <h2>THÔNG TIN NGƯỜI LIÊN HỆ</h2>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            
                            <el-form-item
                                label="Họ và tên"
                                prop="ContactName"
                            >
                                <el-row>
                                    <el-col :lg="8" :md="8" :sm="8">
                                        <provider-select
                                        id="Sex"
                                        type="label-top"
                                        label="label"
                                        field="value"
                                        :defaultNumber="sex"
                                        :list="Sex"
                                        @value="sex = $event"
                                    ></provider-select>
                                    </el-col>
                                    <el-col :lg="16" :md="16" :sm="24">
                                        <field-input
                                        id="ContactName"
                                        :width="'100%'"
                                        type="field-input"
                                        placeholder=""
                                        :valueInput="formProviverOrganization.ContactName"
                                        @value="formProviverOrganization.ContactName = $event"
                                        ></field-input>
                                    </el-col>
                                </el-row>                            
                            </el-form-item>
                        </el-col> 
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Email"
                            prop="ContactEmail"
                        >
                            <field-input
                            id="ContactEmail"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            :valueInput="formProviverOrganization.ContactEmail"
                            @value="formProviverOrganization.ContactEmail = $event"
                            ></field-input>
                        </el-form-item>
                        </el-col>                     
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="24" :sm="24">
                            <el-form-item
                                label="Điện thoại"
                                prop="ContactPhone"
                            >
                                <field-input
                                id="ContactPhone"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverOrganization.ContactPhone"
                                @value="formProviverOrganization.ContactPhone = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="24" :sm="24">
                            <el-form-item
                                label="Chức danh"
                                prop="ContactTitle"
                            >
                                <field-input
                                id="ContactTitle"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverOrganization.ContactTitle"
                                @value="formProviverOrganization.ContactTitle = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row>
                        <el-col :lg="24" :md="24" :sm="24">
                            <el-form-item
                                label="Địa chỉ"
                                prop="ContactAddress"
                            >
                                <field-input
                                id="ContactAddress"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverOrganization.ContactAddress"
                                @value="formProviverOrganization.ContactAddress = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24" v-if="modeEdit==true">
                    <el-checkbox v-model="formProviverOrganization.Follow">Ngừng theo dõi</el-checkbox>
                </el-col>
            </el-row>
        </el-form></div>      
        <div         v-if="formPersonal==1"
><el-form
        ref="FormProviderPersonal"
        :model="formProviverPersonal"
        label-width="120px"
        label-position="left"
        >
            <el-row>
                <el-col :lg="24" :md="24" :sm="24">
                    <h2>THÔNG TIN CƠ BẢN</h2>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Mã nhà cung cấp"
                                prop="ProviderCode"
                                :rules="[
                                {
                                    required: true,
                                    message:
                                    'Mã nhà cung cấp không được bỏ trống',
                                    trigger: ['change','blur'],
                                },
                                ]"
                                :show-message="validateFormProviverPersonal.ProviderCode"
                            >
                                <field-input
                                id="ProviderCode"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                @hover="validateFormProviverPersonal.ProviderCode = $event"
                                :valueInput="formProviverPersonal.ProviderCode"
                                @value="formProviverPersonal.ProviderCode = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col> 
                        <el-col :lg="12" :md="12" :sm="24" class="provider-name">
                            <el-form-item
                                label="Họ và tên"
                                prop="ProviderName"
                                :rules="[
                                {
                                    required: true,
                                    message:
                                    'Tên nhà cung cấp không được bỏ trống',
                                    trigger: ['change','blur'],
                                },
                                ]"
                            :show-message="validateFormProviverPersonal.ProviderName"
                            >
                                <!-- <el-row>
                                    <el-col :lg="8" :md="8" :sm="8">
                                        <provider-select
                                        id="Sex"
                                        type="label-top"
                                        label="label"
                                        field="value"
                                        :defaultNumber="sex"
                                        :list="Sex"
                                        @value="sex = $event"
                                    ></provider-select>
                                    </el-col>
                                    <el-col :lg="16" :md="16" :sm="24">
                                        
                                    </el-col>
                                </el-row>                             -->
                                <field-input
                                        id="ProviderName"
                                        :width="'100%'"
                                        type="field-input"
                                        placeholder=""
                                        @hover="validateFormProviverPersonal.ProviderName = $event"
                                        :valueInput="formProviverPersonal.ProviderName"
                                        @value="formProviverPersonal.ProviderName = $event"
                                        ></field-input>
                            </el-form-item>
                        </el-col>                     
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row>
                        <el-col :lg="24" :md="24" :sm="24">
                            <el-form-item
                                label="Địa chỉ"
                                prop="ProviderAddress"
                            >
                                <field-input
                                id="ProviderAddress"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverPersonal.ProviderAddress"
                                @value="formProviverPersonal.ProviderAddress = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Email"
                                prop="ProviderEmail"
                            >
                                <field-input
                                id="ProviderEmail"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverPersonal.ProviderEmail"
                                @value="formProviverPersonal.ProviderEmail = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Điện thoại"
                            prop="ProviderPhone"
                        >
                            <field-input
                            id="ProviderPhone"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            :valueInput="formProviverPersonal.ProviderPhone"
                            @value="formProviverPersonal.ProviderPhone = $event"
                            ></field-input>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Số CMND"
                                prop="CitizenIdentityNo"
                            >
                                <field-input
                                id="CitizenIdentityNo"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverPersonal.CitizenIdentityNo"
                                @value="formProviverPersonal.CitizenIdentityNo = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Ngày cấp"
                            prop="CitizenIdentityDate"
                        >
                            <datetime2
                            id="CitizenIdentityDate"
                            :width="'100%'"
                            type="date"
                            placeholder="__/__/____"
                            :value="formProviverPersonal.CitizenIdentityDate"
                            @getValue="formProviverPersonal.CitizenIdentityDate = $event"
                            ></datetime2>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Nơi cấp CMND"
                                prop="CitizenIdentityPlace"
                            >
                                <field-input
                                id="CitizenIdentityPlace"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverPersonal.CitizenIdentityPlace"
                                @value="formProviverPersonal.CitizenIdentityPlace = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Nhóm nhà CC"
                            prop="ProviderPhone"
                        >
                            <provider-select
                                    id="ProviderGroupID"
                                    type="label-top"
                                    label="ProviderGroupName"
                                    field="ProviderGroupID"
                                    :defaultString="formProviverPersonal.ProviderGroupID"
                                    :filterable="true"
                                    :list="ProviderGroups"
                                    @value="formProviverPersonal.ProviderGroupID = $event"
                                >                                    
                                </provider-select>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Số nợ tối đa"
                                prop="Debt"
                            >
                                <input-number
                                id="Debt"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverPersonal.Debt"
                                @value="formProviverPersonal.Debt = $event"
                                ></input-number>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Hạn nợ (ngày)"
                            prop="DebtTerm"
                        >
                            <input-number
                                id="DebtTerm"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverPersonal.DebtTerm"
                                @value="formProviverPersonal.DebtTerm = $event"
                                ></input-number>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                            <el-form-item
                                label="Số tài khoản"
                                prop="AccountNumber"
                            >
                                <field-input
                                id="AccountNumber"
                                :width="'100%'"
                                type="field-input"
                                placeholder=""
                                :valueInput="formProviverPersonal.AccountNumber"
                                @value="formProviverPersonal.AccountNumber = $event"
                                ></field-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Ngân hàng"
                            prop="BankName"
                        >
                            <field-input
                            id="BankName"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            :valueInput="formProviverPersonal.BankName"
                            @value="formProviverPersonal.BankName = $event"
                            ></field-input>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24">
                    <el-row :gutter="16">
                        <el-col :lg="12" :md="12" :sm="24">
                        <el-form-item
                            label="Chi nhánh"
                            prop="BankBranchName"
                        >
                            <field-input
                            id="BankBranchName"
                            :width="'100%'"
                            type="field-input"
                            placeholder=""
                            :valueInput="formProviverPersonal.BankBranchName"
                            @value="formProviverPersonal.BankBranchName = $event"
                            ></field-input>
                        </el-form-item>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :lg="24" :md="24" :sm="24" v-if="modeEdit==true">
                    <el-checkbox v-model="formProviverPersonal.Follow">Ngừng theo dõi</el-checkbox>
                </el-col>
            </el-row>
            
        </el-form></div>
        
    </template>
    <template v-slot:footer>

        <!-- Button -->
        <m-btn
              type="btn-grey-icon"
              style="margin-right: 16px"
              name="Hủy"
              :handleClick="getClose"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group_19919.png" />
              </template>
        </m-btn> 
        <m-btn
              type="btn-primary-icon"
              style="margin-right: 16px !important"
              name="Lưu"
              :handleClick="addProvider"
            >
              <template v-slot:icon>
                <img class="icon" src="@/icons/Group 19244.png" />
              </template>
            </m-btn> 
        
        <!-- Button -->
      </template>
  </provider-dialog>
</template>

<script>
import dialog from "@/components/dialog";
import fieldinput from "@/components/inputs";
import fieldInputNumber from "@/components/inputs/input-number/index";
import select from "@/components/select";

import Button from "@/components/buttons";
import datetime2 from "@/components/date-time-pickers/date-time-picker-v2";
import APIProvider from "@/api/etax/provider.js";
import { formatDate } from "@/utils/index.js";
export default {
    components:{
        "provider-dialog":dialog,
        "field-input":fieldinput,
        "provider-select":select,
        "input-number":fieldInputNumber,
         "m-btn":Button,
         datetime2
    },
    props:{
        visiableDL:{
            type:Boolean,
            default:false
        },
        providerID:{
            type:String,
            default:null
        },
        modeEdit:{
            type:Boolean,
            default:false
        },
        modeDuplicate:{
            type:Boolean,
            default:false
        }
    },
    data(){
        return{
            mountDone: false,
            title:"Thêm nhà cung cấp",
            dialogid:"provider-dialog",
            sex:1,
            dateNow:"",
            formPersonal:0,
            formProviverOrganization:{
                ProviderCode: "",
                ProviderName:"",
                ProviderAddress:"",
                ProviderTaxNo:"",
                ProviderPhone:"",
                ProviderGroupID:"",
                Debt:0,
                DebtTerm:0,
                BankName:"",
                BankBranchName:"",
                AccountNumber:"",
                ContactName:"Ông ",
                ContactEmail:"",
                ContactPhone:"",
                ContactTitle:"",
                ContactAddress:"",
                Follow:false,
                ProviderType:0
            },
            formProviverPersonal:{
                ProviderCode: "",
                ProviderName:"",
                ProviderAddress:"",
                ProviderEmail:"",
                ProviderPhone:"",
                CitizenIdentityNo:"",
                CitizenIdentityDate:"",
                CitizenIdentityPlace:"",
                ProviderGroupID:"",
                Debt:0,
                DebtTerm:0,
                BankName:"",
                BankBranchName:"",
                AccountNumber:"",
                Follow:false,
                ProviderType:1
            },
            ProviderGroups:[
                {
                    label:"Nhóm ABC",
                    value:1
                },
                {
                    label:"Nhóm DEF",
                    value:2
                }
            ],
            Sex:[
                {
                    label:"Ông",
                    value:1
                },
                {
                    label:"Bà",
                    value:2
                }
            ],
            validateFormProviverOrganization:{
                ProviderCode:false,
                ProviderName:false
            },
            validateFormProviverPersonal:{
                ProviderCode:false,
                ProviderName:false
            }
        }
    },
    methods:{
        getClose(){
            this.$emit("edit",false);
            this.$emit("duplicate",false);
            this.$emit("closeDialog", false);
            
            this.resetForm();
        },
        popUpWarnDuplicateDependent(content) {
            this.buttons = [
                {
                text: "Đóng",
                class: "btn-primary",
                callback: () => {},
                },
            ];
            // gọi dialog
            this.$_Popup.warn(`Cảnh báo`, content, this.buttons);
            },
        async addProvider(){
            var formName="";
            if(this.formPersonal==0)
                formName="FormProviderOrganization";
            if(this.formPersonal==1)
                formName="FormProviderPersonal";
            this.$refs[formName].validate(async (valid, index) => {
                if (index !== undefined) {
                    if (Object.keys(index)[0] != null) {
                        setTimeout(() => {
                        document.getElementById(`${Object.keys(index)[0]}`).focus();
                        }, 200);
                    }
                }
                if(valid){
                    var res
                    
                    if(this.formPersonal==0)
                        if(this.modeEdit==true){
                            this.formProviverOrganization.ProviderID = this.providerID;
                            res = await APIProvider.putProvider(this.formProviverOrganization);
                        }else{
                            res = await APIProvider.postProvider(this.formProviverOrganization);
                        }
                    if(this.formPersonal==1)
                        if(this.modeEdit==true){
                            this.formProviverPersonal.ProviderID = this.providerID;
                            res = await APIProvider.putProvider(this.formProviverPersonal);
                        }else{
                            res = await APIProvider.postProvider(this.formProviverPersonal);
                        }
                    if(!res.data.Success){
                        if(res.data.MISACode == 501){
                            this.popUpWarnDuplicateDependent(`Nhà cung cấp ${res.data.Data.ProviderCode} đã tồn tại. Bạn vui lòng kiểm tra lại.`)
                        }else if(res.data.MISACode == 603){
                            if(this.modeDuplicate==true)
                                this.$_Notification.error("Lỗi","Nhân bản thất bại.");
                            else
                                this.$_Notification.error("Lỗi","Thêm thất bại.");

                        }
                        else if(res.data.MISACode == 605)
                            this.$_Notification.error("Lỗi","Sửa thất bại.");
                    }
                    else
                    {
                        
                        this.$emit("success",true);

                        if(this.modeEdit==true){
                            this.$_Notification.success(
                                "Thành công",
                                "Sửa nhà cung cấp thành công"
                            );
                        }else if(this.modeDuplicate==true){
                            this.$_Notification.success(
                                "Thành công",
                                "Nhân bản nhà cung cấp thành công"
                            );
                        }else{
                            this.$_Notification.success(
                                "Thành công",
                                "Thêm nhà cung cấp thành công"
                            );
                        }
                        
                        this.getClose();  
                    }
                }
                
            });
            },
            async getProviderGroup(){
                var res =await APIProvider.getProviderGroup();
                if(res.data.Success){
                    this.ProviderGroups = res.data.Data;
                }
            },
            async getProviderByID(){
                var res =await APIProvider.getProviderByID(this.providerID);
                if(res.data.Success)
                    this.mapDataToForm(res.data.Data)
            },
            mapDataToForm(val){
                if(val.ProviderType==0){
                    this.formPersonal = 0;
                    if(this.modeEdit==true){
                        this.formProviverOrganization.ProviderCode = val["ProviderCode"]
                    }else{
                        this.formProviverPersonal.ProviderCode = ""
                    }
                    this.formProviverOrganization.ProviderName = val["ProviderName"]
                    this.formProviverOrganization.ProviderAddress = val["ProviderAddress"]
                    this.formProviverOrganization.ProviderTaxNo= val["ProviderTaxNo"]
                    this.formProviverOrganization.ProviderPhone= val["ProviderPhone"]
                    this.formProviverOrganization.ProviderGroupID= val["ProviderGroupID"]
                    this.formProviverOrganization.Debt= val["Debt"]
                    this.formProviverOrganization.DebtTerm= val["DebtTerm"]
                    this.formProviverOrganization.BankName= val["BankName"]
                    this.formProviverOrganization.BankBranchName= val["BankBranchName"]
                    this.formProviverOrganization.AccountNumber= val["AccountNumber"]
                    this.formProviverOrganization.ContactName= val["ContactName"]
                    this.formProviverOrganization.ContactEmail= val["ContactEmail"]
                    this.formProviverOrganization.ContactPhone= val["ContactPhone"]
                    this.formProviverOrganization.ContactTitle= val["ContactTitle"]
                    this.formProviverOrganization.ContactAddress= val["ContactAddress"]
                    this.formProviverOrganization.Follow= val["Follow"] == 1?true:false
                    this.formProviverOrganization.ProviderType= val["ProviderType"]
                }
                if(val.ProviderType==1){
                    this.formPersonal = 1;
                    if(this.modeEdit==true){
                        this.formProviverPersonal.ProviderCode = val["ProviderCode"]
                    }else{
                        this.formProviverPersonal.ProviderCode = ""
                    }
                    this.formProviverPersonal.ProviderName = val["ProviderName"]
                    this.formProviverPersonal.ProviderAddress= val["ProviderAddress"]
                    this.formProviverPersonal.ProviderEmail= val["ProviderEmail"]
                    this.formProviverPersonal.ProviderPhone= val["ProviderPhone"]
                    this.formProviverPersonal.CitizenIdentityNo= val["CitizenIdentityNo"]
                    this.formProviverPersonal.CitizenIdentityDate= formatDate(val["CitizenIdentityDate"],"mm/dd/yyyy") 
                    this.formProviverPersonal.CitizenIdentityPlace= val["CitizenIdentityPlace"]
                    this.formProviverPersonal.ProviderGroupID= val["ProviderGroupID"]
                    this.formProviverPersonal.Debt= val["Debt"]
                    this.formProviverPersonal.DebtTerm= val["DebtTerm"]
                    this.formProviverPersonal.BankName= val["BankName"]
                    this.formProviverPersonal.BankBranchName= val["BankBranchName"]
                    this.formProviverPersonal.AccountNumber= val["AccountNumber"]
                    this.formProviverPersonal.ProviderType= val["ProviderType"]
                    this.formProviverPersonal.Follow= val["Follow"] == 1?true:false
                }
            },
            resetForm(){
                this.formPersonal = 0;
                  this.formProviverOrganization={
                        ProviderCode: "",
                        ProviderName:"",
                        ProviderAddress:"",
                        ProviderTaxNo:"",
                        ProviderPhone:"",
                        ProviderGroupID:"",
                        Debt:0,
                        DebtTerm:0,
                        BankName:"",
                        BankBranchName:"",
                        AccountNumber:"",
                        ContactName:"Ông ",
                        ContactEmail:"",
                        ContactPhone:"",
                        ContactTitle:"",
                        ContactAddress:"",
                        Follow:false,
                        ProviderType:0
                    },
                this.formProviverPersonal={
                    ProviderCode: "",
                    ProviderName:"",
                    ProviderAddress:"",
                    ProviderEmail:"",
                    ProviderPhone:"",
                    CitizenIdentityNo:"",
                    CitizenIdentityDate:"",
                    CitizenIdentityPlace:"",
                    ProviderGroupID:"",
                    Debt:0,
                    DebtTerm:0,
                    BankName:"",
                    BankBranchName:"",
                    AccountNumber:"",
                    Follow:false,
                    ProviderType:1
                }
            }
    },
    watch:{
        modeEdit:{
            handler(val){
                if(val)
                   this.getProviderByID();
            }
        },
        modeDuplicate:{
            handler(val){
                if(val)
                   this.getProviderByID();
            }
        },
        visiableDL(val) {
                console.log(this.formPersonal);
                if(val){
                    this.dateNow = new Date().getDate() + "/"+ (new Date().getMonth()+1) + "/"+ new Date().getFullYear();
                    this.formProviverPersonal.CitizenIdentityDate = formatDate(this.dateNow,"dd/mm/yyyy");
                    if(this.formPersonal==0){
                        this.$nextTick(function() {
                            this.$refs["FormProviderOrganization"].clearValidate();
                        });
                    }
                    else{
                        console.log("ok 1");
                        this.$nextTick(function() {
                            this.$refs["FormProviderPersonal"].clearValidate();
                        });
                    }
                }             
        },
        formPersonal:{
            handler(val) {
                if(val==0){
                    this.$nextTick(function() {
                            this.$refs["FormProviderOrganization"].clearValidate();
                        });
                    
                }
                else{
                    this.$nextTick(function() {
                            this.$refs["FormProviderPersonal"].clearValidate();
                        });
                }
            },
        },
        sex:{
            handler(val){
                console.log(val);
                if(val==1 && this.formPersonal==0)
                    this.formProviverOrganization.ContactName = "Ông "
                if(val==1 && this.formPersonal==0)
                    this.formProviverOrganization.ContactName = "Ông "
                if(val==2 && this.formPersonal==0)
                    this.formProviverOrganization.ContactName = "Bà "
                if(val==2 && this.formPersonal==0)
                    this.formProviverOrganization.ContactName = "Bà "
            }
        },
        "formProviverOrganization.Debt":{
            handler(val){
                this.formProviverOrganization.Debt = val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            
            }
        }
    },
    created(){
        this.getProviderGroup();
    },
    mounted(){
        this.mountDone = true;
    }
}
</script>

<style lang="scss">
#provider-dialog{
    .el-dialog{
        width: 750px !important;
        .el-dialog__body{
            height: 700px;
        }
    }

    .el-form-item{
        margin-bottom: 0px ;
    }
    .el-form-item__label{
        margin-right: 0px;
        margin-bottom: 15px ;
    }
    .provider-name .el-form-item.is-required:not(.is-no-asterisk) > .el-form-item__label:before{
        margin-right: 40px
    }
    #ProviderGroupID{
        .label-input{
            padding-bottom: 0px !important;
        }
    }
}
</style>