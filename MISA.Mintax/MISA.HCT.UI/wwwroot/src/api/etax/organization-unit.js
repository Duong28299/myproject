import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);
const organizationUrl = "OrganizationUnits";
export default {
  /**
   * Lấy danh sách đơn vị
   */
  async getOrganizationUnit(style, type = 0) {
    return await service.get(organizationUrl + `?style=${style}&type=${type}`);
  },

  async installOrganizationUnit() {
    return await service.post(organizationUrl + "/install");
  },
};
