import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API_MTAX);
const service_build = api(process.env.VUE_APP_BASE_API);
export default {
  /**
   * Hàm check chứng thư số
   * @param {đối tượng chứa mã số thuế và serial chứng thư số} data 
   * created by vdthang 22.09.2020
   */
  async getUsbToken(data) {
    var res = await service.get(
      `/MTax/UsbToken?TaxCode=${data.TaxCode}&CASerialID=${data.CASerialID}`
    );
    return res;
  },

  /**
   * Hàm check License
   * @param {mã số thuế đơn vị} data
   * created by vdthang 22.09.2020
   */
  async licenseChecker(data) {
    var url = `/MTax/License?TaxCode=${data}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Hàm gửi file xml đã ký lên hệ thống MTAX
   * @param {*} data file xml đã ký
   * created by vdthang 22.09.2020
   */
  async mTaxDeclarationSender(data) {
    var res = await service.post("/MTax/MTaxDeclaration", data);
    return res;
  },

  /**
   * Hàm check tờ khai
   * @param {*} data thông tin tờ khai
   * created by vdthang 22.09.2020
   */
  async validateReport(data) {
    var res = await service.post("/MTax/ValidateReport", data);
    return res;
  },

  /**
   * Hàm check có sử dụng TVan hay chưa
   * @param {mã số thuế đơn vị} data
   * created by vdthang 22.09.2020
   */
  async checkUsingTvan(data) {
    var res = await service.get(`/MTax/TVanStatus?TaxCode=${data}`);
    return res;
  },


  async buildDeclaration(mode,declaration,keyProperty ){
    var declarationID = [];
    declaration.forEach(element => {
      declarationID.push(element[keyProperty]);
    });
    var res =await service_build.post(`/${mode}/Creator`, declarationID);
    return res;
  }
};
