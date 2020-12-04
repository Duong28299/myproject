import { api } from "../base";
import {setParamPaging} from "@/utils/paging"
const service = api(process.env.VUE_APP_BASE_API);

export default {
    /**
     * Phân trang chung
     * 
     * @param {Bộ lọc} filter 
     * @param {Thông tin cần truy cập} type 
     * @param {Có tìm kiếm theo organization hay không} isOrganization 
     */
    async getPaging(filter,type,isOrganization){
        var base64Filter = setParamPaging(filter,isOrganization);

        var urlRequest = `general/${type}/Paging?paging=${base64Filter}`;
        var res = await service.get(urlRequest);
        return res;
    }
}