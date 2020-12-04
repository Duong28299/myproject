<template>
  <div id="header" class="d-flex">
      <div class="contain-user">
        

        <!--lựa chọn đơn vị làm việc-->
        <img class="img-arrowdown" src="@/icons/arrow down.png">
        <img class="img-avatar" src="@/assets/images/avatar.jpg">
        <div class="tittle-2 tittle-header ">{{getOrganization}}</div>
      </div>
  </div>
</template>
<script>
import apiGetOrganization from "@/api/etax/organization.js";
export default {
  components:{
  },

  props:{
    tittlename:{
      type: String,
      default: "CÔNG TY CỔ PHẦN MISA"
    },
    currentMission:{
      type:String,
      default:""
    }
  },
  
  data() {
    return {
      oranizationName:"",
      lstOrganization:[],
      curentOrgamization:"",
      organizeName:"",
      currentPath:""
    };
  },

  methods: {
    /**
     * Lấy đơn vị theo tenant code
     * nvnguyen (04/11/2020)
     */
    async getListOrganization() {
      this.lstOrganization = [];
      var res = await apiGetOrganization.getListOrganization();
      if (res.data && res.data.Success) {
        if (res.data.Data == null || res.data.Data.length == 0) {
          this.$router.push("/getting-started/owner");
        } else {
          this.mapData(res.data.Data);
        }
      } else {
        this.lstOrganization = [];
      }
    },

    /**
     * map thông tin đơn vị
     * nvnguyen (04/11/2020)
     */
    mapData(data) {
      var result = [];
      data.forEach((item) => {
        var obj = {};
        obj.IsParent = item["IsParent"];
        obj.TenantID = item["TenantID"];
        obj.OrganizationID = item["OrganizationID"];
        obj.OrganizationName = item["OrganizationName"];
        obj.OrganizationUnit = item["OrganizationUnit"];
        result.push(obj);
      });
      this.lstOrganization = result;
      this.$store.dispatch("organization/addOrganizationList", this.lstOrganization);
    },
  },

  watch: {
     getOrganize(){
      return this.lstOrganize = this.$store.getters.organizationList;
    }
  },

  computed:{
    getTitle(){
      return this.$route.meta.title
    },
    getOrganization(){
      return this.oranizationName;
    },
  },

  created(){
    this.oranizationName = sessionStorage.getItem("oranizationName");
    this.getListOrganization();
    this.lstOrganize = this.$store.getters.organizationList;
  },
};
</script>
<style lang="scss" scoped>
@import "~@/styles/variables.scss";
#img-back {
  margin: 2px 10px 0px 4px;
  border: 1px solid $grey;
  padding: 0 2px;
  border-radius: 5px;
}
</style>
