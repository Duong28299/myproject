import { api } from "../base/index";

const service = api(process.env.VUE_APP_BASE_API_IMPORT);
const serviceEtax = api(process.env.VUE_APP_BASE_API);

export default {
  /**
   * Service updload file excel
   * @param {Các tham số truyền lên} options
   * created by nvbinh2 30.10.2020
   * Modified by: NQKHAI(31/10/2020)
   *
   */
  async postUpdoadExcelFile(url, data) {
    var res = await service.post(url, data,
      {
        headers:{
          'Content-Type':'multipart/form-data'
        }
    });
    return res;
  },
  /**
   * Lấy đường dẫn request
   * @param {kiểu request: "reader" - upload file lên server , "import"} mode
   * @param {*} options
   * created by nvbinh2 30.10.2020
   */
  getURLExcel(mode, options) {
    var url =
      process.env.VUE_APP_BASE_API_IMPORT +
      "/declarationsettlementdetails/" +
      mode;
    switch (mode) {
      case "reader":
        return (
          url +
          `?declarationSettelementID=${options.declarationSettelementID}&overrideData=${options.overrideData}` +
          (options.cancellationToken
            ? `&cancellationToken=${options.cancellationToken}`
            : "")
        );
      case "import":
        return (
          url +
          `?importKey=${options.importKey}&overrideData=${options.overrideData}` +
          (options.cancellationToken
            ? `&cancellationToken=${options.cancellationToken}`
            : "")
        );
    }
  },

  /**
   * Hàm import file excel
   * Created By : NQKHAI(31/10/2020)
   * @param {} url 
   */
  async importExcelFile(url) {
    var res = await service.post(url);
    return res;
  },

  /**
   * Lấy đường dẫn request
   * @param {*} mode Kiểu request: settlementdetail05_2bk / settlementdetail05_2bk_error
   * @param {*} options {thuộc tính chứa errorKey}
   * created by bvbao (2/11/2020)
   */
  getURLDownloadExcel(mode, options) {
    var url =
      process.env.VUE_APP_BASE_API_IMPORT +
      "/imports/download";
    switch (mode) {
      case "settlementdetail05_2bk_error":
        return (
          url + "/error" +
          `?errorKey=${options.errorKey}&fileCode=${mode}`
        );
      case "settlementdetail05_2bk":
        return (
          url +
          `/${mode}`
        );
    }
  },

  /**
   * Hàm download file excel
   * @param {*} url đường dẫn tới api download
   * created by bvbao(2/11/2020)
   */
  async downloadExcelFile(url) {
    var res = await service({
      method: 'get',
      url: url,
      responseType: 'blob'
  });
    return res;
  },

  /**
   * Gọi service thêm người nộp thuế
   * @param {người nộp thuế} declaration
   * CreatedBy: NVANH 20.10.2020
   */
  async postDeclarationSettlementDetail(employee) {
    var res = await serviceEtax.post(`/DeclarationSettlementDetail`, {
      ...employee
    });
    return res;
  },

    /**
   * Gọi service cập nhật người nộp thuế
   * @param {người nộp thuế} declaration
   * CreatedBy: NVANH 20.10.2020
   */
  async putDeclarationSettlementDetail(employee) {
    var res = await serviceEtax.put(`/DeclarationSettlementDetail`, {
      ...employee
    });
    return res;
  },

  /**
   * Lấy danh sách người nộp thuế theo EmployeeCode
   * @param {người nộp thuế} declaration
   * CreatedBy: NVANH 10.11.2020
   */
  async getDeclarationSettlementDetailByEmployeeCode(filter) {
    var res = await serviceEtax.get(`DeclarationSettlementDetail/Filter?employeeCode=${filter.EmployeeCode}&declarationType=${filter.DelcarationType}`);
    return res;
  },

  /**
   * Xóa người nộp thuế theo ID
   * @param {người nộp thuế} declaration
   * CreatedBy: TCDUONG 10.11.2020
   */
  async deleteDeclarationSettlementDetailByID(data) {
    var res = await serviceEtax.delete(`/DeclarationSettlementDetail`,{
      data: data
    });
    return res;
  },
};
