import { api } from "../base";

const service = api(process.env.VUE_APP_BASE_API);

export default {
  /**
   * Lấy danh sách đại lý thuế theo mã đại lý thuế
   * @param {obj chứa thông tin filter} obj
   * createdby: lttuan 01-09-2020
   */
  async getTaxAgentById(TaxAgentId) {
    var res = await service.get(`TaxAgents/${TaxAgentId}`);
    return res.data;
  },

  /**
 * Lấy danh sách đại lý thuế theo mã đơn vị
 * @param {obj chứa thông tin filter} obj
 * createdby: lttuan 04-09-2020
 */
  async getTaxAgentByOrganizationID(organizationID) {
    var res = await service.get(`TaxAgents/filter?organizationID=${organizationID}`);
    return res.data;
  },

  /**
   * Thực hiện chỉnh sửa bảng tính thuế
   * createdby: lttuan 01-09-2020
   * @param {*} data
   */
  async putTaxAgent(data) {
    return await service.put("/TaxAgents", data);
  },
};
