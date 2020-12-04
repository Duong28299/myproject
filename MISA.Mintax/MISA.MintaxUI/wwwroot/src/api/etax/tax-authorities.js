import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);
export default {
  /**
   *  Lấy thông tin cơ quan thuế theo tên
   * @param {tên cơ quan} name
   * created by bvbao(25/11/2020)
   */
  async getTaxAuthoritiesByName(name) {
    var res = await service.get(`TaxAuthorities/getByName?name=${name}`);
    return res;
  },
};
