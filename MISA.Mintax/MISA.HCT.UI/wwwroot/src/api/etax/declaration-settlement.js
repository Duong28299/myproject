import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);

export default {

  /**
   * Gọi service lấy danh sách tờ khai quyết toán thuế
   * @param {Danh sách lọc danh sách tờ khai} filter
   * Createdby: NVANH 14.10.2020
   */
  async getDeclarationSettlement(filter) {
    var url = `/DeclarationSettlement/Paging?keyword=${filter.keyword}&pageIndex=${filter.pageIndex}&pageSize=${filter.pageSize}&declarationType=${filter.declarationType}&declarationStatus=${filter.declarationStatus}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Gọi service lấy chi tiết tờ khai quyết toán thuế
   * @param {ID tờ khai quyết toán thuế} declarationID
   * CreatedBy: NVANH 20.10.2020
   */
  async getDeclarationSettlementByID(declarationID) {
    var url = `/DeclarationSettlement/${declarationID}`;
    var res = await service.get(url);
    return res;
  },

    /**
   * Gọi service thêm tờ khai quyết toán thuế
   * @param {tờ khai quyết toán thuế} declaration
   * CreatedBy: NVANH 28.10.2020
   */
  async postDeclarationSettlement(declarationsettlement) {
    var res = await service.post(`/DeclarationSettlement`, {
      ...declarationsettlement
    });
    return res;
  },

  /**
   * Gọi service sửa tờ khai quyết toán thuế
   * @param {tờ khai quyết toán thuế} declaration
   * CreatedBy: NVANH 20.10.2020
   */
  async putDeclarationSettlement(declarationsettlement) {
    var res = await service.put(`/DeclarationSettlement`, {
      ...declarationsettlement
    });
    return res;
  },

  /**
   * Thực hiện xóa dữ liệu tờ khai quyết toán thuế
   * @param {data chứa mảng id các tờ khai quyết toán thuế}
   * createdby: NVANH 14.10.2020
   */
  async deleteDeclarationSettlement(data) {
    return await service.delete("/DeclarationSettlement", {
      data: data,
    });
  },

  /**
   * Thực hiện lấy danh sách phân trang cho chi tiết phụ lục 05-1BK
   * @param {*} filter 
   * Created by: NQTrung 27/10/2020
   */
  async getDeclarationSettlementDetail(filter) {
    var url = `/DeclarationSettlementDetail/Paging?settlementID=${filter.settlementID}&declarationSettlementDetailType=${filter.declarationSettlementDetailType}&keyword=${filter.keyword}&pageIndex=${filter.pageIndex}&pageSize=${filter.pageSize}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Lấy Thông tin chi tiết tờ khai quyết toán theo id
   * @param {*} id id của tờ khai quyết toán thuế (DeclarationSettlementDetailID)
   * Created by: NQTrung 31/10/2020
   */
  async getDeclarationSettlementDetailById(id) {
    var url = `/DeclarationSettlementDetail/${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Chỉnh sửa chi tiết tờ khai quyết toán thuế 
   * @param {*} data 
   * Created by: NQTrung 31/10/2020
   */
  async putDeclarationSettlementDetail(data){
    var url = "/DeclarationSettlementDetail";
    var res = await service.put(url,data);
    return res;
  }
  


};
