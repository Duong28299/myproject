import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);
export default {

  /**
   * Thêm thông tin dại lý thuế
   * @param {object chứa thông tin của thông tin đại lý thuế} obj 
   * created by tcduong 05.09.2020
   */
  async insertTaxAgent(obj) {
    const res = await service.post("TaxAgents", obj);
    return res;
  }
}