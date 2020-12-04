import { api } from "../base";

const service = api(process.env.VUE_APP_BASE_API);

export default {
  /**
   * Lấy danh sách thông tin loại thuế
   * @param {obj chứa thông tin filter} obj
   * created by vdthang 22.09.2020
   */
  async getTaxInfo(TemType) {
    var res = await service.get(`TaxInfo/filter?temporaryTaxID=${TemType}`);
    return res.data;
  },

    
  /**
   * Lấy danh sách thông tin loại thuế
   * @param {loại thuế} taxType 
   * @param {ngày tạo} date 
   * created by NVANH 04.11.2020
   */
  async getTaxInfoByType(taxType, date) {
    var res = await service.get(`TaxInfo/filter?taxType=${taxType}&date=${date}`);
    return res.data;
  },
};
