import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);

export default {

    /**
     * Service thực hiện lấy dữ liệu chi tiết tờ khai trong hồ sơ đăng ký mã số thuế
     * @param {dữ liệu gửi lên service bao gồm ID tờ khai, pageindex, page size, isfirst} data 
     * created by vdthang 21.11.2020
     */
    async getDeclarationDetailTaxRegister(data) {
        var url = `/DeclarationDetail/TaxRegister/Paging?declarationID=${data.declarationID}&pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&isFirst=${data.isFirst}`;
        var res = await service.get(url);
        return res;
    }
}