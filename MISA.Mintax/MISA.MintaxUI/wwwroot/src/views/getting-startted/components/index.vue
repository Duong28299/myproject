<template>
    <div>
        <Header/>
        <div class="logo">
            <img class="img-menu" src="@/assets/images/Union 28@2x.png"/>
            <img class="img-logo" src="@/assets/images/Group 17877@2x.png"/>
        </div>
        <div class="company">
            <div class="company-title">
                <h1>
                    Danh sách đơn vị bạn quản lý
                </h1>
            </div>
        
            <div class="btn-company-mode">
                <button-icon type="btn-primary-icon" name="Thêm mới đơn vị" :handleClick="()=> $router.push('unit-getting-startted')">
                <template v-slot:icon>
                    <img class="icon" src="@/icons/Group 17812.png" />
                </template>
                </button-icon>
            </div>
        </div>
        <div class="company-body scroll">
            <div class="test">
                <div class="company-body-header">
                    <div class="company-icon-main">
                        <img class="icon-main" src="@/icons/Group 18663.png"/>
                    </div>
                    <div class="company-child-content">
                        <h3>Công ty cổ phần MISA</h3>
                        <div>Mã số thuế: 3434343</div>
                        <div>Cơ cấu tổ chức: Tổng công ty/công ty</div>
                    </div>
                </div>
            
                <div class="company-body-child" @dblclick="dblClick">
                    <div class="company-child-content">
                        <h3>Văn Phòng MISA Hồ Chí Minh</h3>
                        <div>Trực thuộc đơn vị: Công ty cổ phần MISA</div>
                        <div>Mã số thuế: 3434343</div>
                        <div>Cơ cấu tổ chức: Tổng công ty/công ty</div>
                    </div>
                    
                </div>
                <div class="company-body-child">
                    <div class="company-child-content">
                        <h3>Văn Phòng MISA Hồ Chí Minh</h3>
                        <div>Trực thuộc đơn vị: Công ty cổ phần MISA</div>
                        <div>Mã số thuế: 3434343</div>
                        <div>Cơ cấu tổ chức: Tổng công ty/công ty</div>
                    </div>
                </div>
                <div class="company-body-child">
                    <div class="company-child-content">
                        <h3>Văn Phòng MISA Hồ Chí Minh</h3>
                        <div>Trực thuộc đơn vị: Công ty cổ phần MISA</div>
                        <div>Mã số thuế: 3434343</div>
                        <div>Cơ cấu tổ chức: Tổng công ty/công ty</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import header from "@/layout/components/header.vue";
import Button from "@/components/buttons";
import apiGetOrganizationUnit from  "@/api/etax/organization.js"

export default {
  name: "GettingStartted2",
  components:{
      "Header":header,
      "button-icon": Button,
      
  },
  props:{

  },
  data(){
      return{
        isbutton: false,
        tab1: true,
        tab2: false,
      }
  },
  methods:{
      dblClick(){
        this.$router.push("tax-period");
        },



    async getListOrganizationUnits(){
        var res = apiGetOrganizationUnit.getListOrganizationUnits("1b902914b6db47408b5b4c39452e9fec");
        if(typeof res.data.Data != "undefined" && res.data.Success){
            this.mapDataEdit(res.data.Data);
        }
        else{
            this.formData = [];
        }
        
    },

    /**
     * Map dữ liệu trả về thông tin đơn vị chủ quản
     * Created by: TVPhi 21/08/2020
     */

    mapDataEdit(data){
        var result = [];
        data.forEach((item)=>{
            var obj = {};
            // Dữ liệu phía thông tin chung
            obj.OrganizationName= item["OrganizationUnitName"];
            obj.OrganizationTaxNo = item["BusinessRegistrationCode"]; 
            obj.TaxPlaceName = item["Address"]; // Địa chỉ
            obj.OrganizationUnitTypeName = item["OrganizationUnitTypeName"];// Cơ cấu tổ chức
            obj.TaxCode = item["TaxCode"]; // Mã số thuế
            obj.OrganizationUnitTypeID = item["OrganizationUnitTypeID"]; //Kiểm tra xem có phải trực thuộc đơn vị
            obj.OrganizationUnitCode = item["OrganizationUnitCode"];

            result.push(obj);
        });
        
        this.formData = result[0];
    },


  },
  created(){
    this.getListOrganizationUnits();
  },
};
</script>
<style lang="scss" scoped>
@import "@/styles/variables.scss";
.logo {
    display: flex;
    width: 202px;
    height: 60px;
    align-items: center;
    box-shadow: -2px 1px 4px rgba(0, 0, 0, 0.16);
    position: absolute;
    z-index: 100;
    top: 0px;
    height: 60px;
    left: 0px;
    background-color: $white;
    .img-menu {
        width: 16px;
        height: 16px;
        margin-left: 20px;
        margin-right: 20px;
    }
    .img-logo {
        height: 40px;
    }
}
.company{
    align-items: center;
	justify-content: center;
    position: absolute;
    left: 0px;
    right: 0px;
    bottom: 0px;
    top: 60px;
    height: 60px;
    display: flex;
    width: 100%;
    .btn-company-mode{
        position: absolute;
        padding-top:10px ;
        right: 120px;
    }  
}
.scroll{
    height:550px ;
    overflow:auto;  
}
.company-body{
    margin-top: 16px;
    align-items: center;
    justify-content: center;
    //display: flex;
    position: absolute;
    top: 120px;
    height: 700px;
    width: 100%;

    .test{
        width: 550px;
        height: 700px;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%,-50%);

        .company-body-header{
            cursor: pointer;
            margin-top: 16px;
            height: 110px;
            width: 552px;
            border: 1px solid #ccc;
            border-radius: 4px ;

            .company-icon-main{
                float: right;
                margin-right: 10px;
            }

            .company-child-content{
                margin: 14px 0px 17px 14px;
                div{
                    margin-bottom: 10px;
                }
                h3{
                    margin-bottom: 24px;
                }
            }
        }
        
        .company-body-child{
            margin-top: 16px;
            height: 137px;
            width: 552px;
            border: 1px solid #ccc;
            border-radius: 4px ;
            .company-icon-main{
                float: right;
                margin-right: 10px;
            }
            .company-child-content{
                margin: 14px 0px 17px 14px;
                div{
                    margin-bottom: 10px;
                }
                h3{
                    margin-bottom: 24px;
                }
            }
        }
    }
}


</style>