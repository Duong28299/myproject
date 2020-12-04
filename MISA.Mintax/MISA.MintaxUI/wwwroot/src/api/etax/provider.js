import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);
export default{
    async getProvider(data){
        var res = await service.get(`/Provider?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}`);
        return res;
    },

    async postProvider(data){
        var res = await service.post("/Provider",data);
        return res;
    },
    async getProviderGroup(){
        var res = await service.get("/ProviderGroup");
        return res;
    },
    async getProviderByID(id){
        var res = await service.get(`/Provider/${id}`);
        return res;
    },
    async putProvider(data){
        var res = await service.put("/Provider",data);
        return res;
    },
    async deleteProvider(data){
        var res = await service.delete("/Provider",{data:data});
        return res;
    }
}
