import { api } from "../base";

const service = api(process.env.VUE_APP_BASE_API_GENERAL);

export default {
  /**
   * lấy danh sách locations từ API MISA.Location
   * @param {int} kind loại
   * @param {string} parentID định danh
   * created by: N.T. Dũng 28.07.2020
   */
  async getLocation(kind, parentID) {
    var res = await service.get(`Location?kind=${kind}&parentID=${parentID}`);
    return res.data.Data;
  },
};
