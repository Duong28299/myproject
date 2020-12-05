import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);
export default {
  /**
   * Gọi service lấy danh sách người phụ thuộc
   * @param {Danh sách lọc danh sách tờ khai} filter
   * Createdby: tcduong 24.10.2020
   */
  async getDeclarationSettlementDetailDependent(filter) {
    var url = `/DeclarationSettlementDetailDependent/Paging?keyword=${filter.keyword}&declarationsettlementID=${filter.declarationsettlementID}&pageIndex=${filter.pageIndex}&pageSize=${filter.pageSize}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Gọi service đọc dữ liệu từ file excel phụ lục 02-BK
   * @param {*} overrideData 
   * @param {*} declarationSettelementID 
   * created by vdthang 16.11.2020
   */
  postImportExcelFile(overrideData, declarationSettelementID) {
    return (
      process.env.VUE_APP_BASE_API_IMPORT +
      `/declarationsettlementdetails/reader?overrideData=${overrideData}&declarationSettelementID=${declarationSettelementID}`
    );
  },
};
