import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);
const service_amis = api(process.env.VUE_APP_AMISCONNECTOR_API);
const service_general = api(process.env.VUE_APP_BASE_API_GENERAL);

export default {
  /**
   * Lấy thông tin đơn vị theo Id
   * @param {id của thông tin đơn vị} val
   * created by vdthang 22.09.2020
   */
  getOrganizationById(val) {
    var res = service.get(`Organizations/OrganizationID/${val}`);
    return res;
  },

  /**
   * Lấy thông tin đơn vị qua Id trên AMIS
   * @param {id của đơn vị trên amis} val 
   * created by vdthang 22.09.2020
   */
  getOrganizationByOrganizationAmisID(val) {
    var res = service.get(
      `Organizations/OrganizationAmisID/${val}`
    );
    return res;
  },

  /**
   * Thêm mới đơn vị con
   * object Org @param {*} obj
   * created by nnphong 21/8/2020
   */
  async insertOrganization(obj) {
    const res = await service.post(`Organizations`, obj);
    return res;
  },

  /**
   * Lấy đơn vị chủ quản bên AMIS
   * @param {id của thông tin đơn vị} id
   * @param {organizationId của đơn vị} organizationId
   * @param {sestion của đơn vị} ivsid
   * Created By: TVPhi 21/08/2020
   */
  async getOrganizationUnit(id) {
    var res = await service_amis.get(
      `OrganizationUnit/GetOrganizationUnit?organizationUnitID=${id}`
    );
    return res;
  },

  /**
   * Lấy thông tinlist organization (đơn vị con và đơn vị chủ quản )  bên Amis
   * @param {sestion của đơn vị} val
   * Created By: TVPhi 21/08/2020
   */
  async getListOrganizationUnits() {
    var res = await service_amis.get(`OrganizationUnit/OrganizationUnits`);
    return res;
  },

  /**
   * Lấy các tổ chức có cùng cơ cấu tổ chức
   * @param {id cơ cấu tổ chức của bố} id
   * Create By: TvPhi 26/08/2020
   */
  async getListOrganization() {
    var res = await service.get(`Organizations/TenantID`);
    return res;
  },

  /**
   * Lấy thông tin organization bên Amis bằng TaxCode
   * @param {id của thông tin đơn vị} id
   * @param {Mã số thuế} taxCode
   * @param {sestion của đơn vị} ivsid
   *  Created By: TVPhi 21/08/2020
   */
  async getOrganizationByTaxCode(taxCode, status) {
    // var res = await service_amis.get(`OrganizationUnit/GetOrganizationByTaxcode?organizationUnitID=${id}&Taxcode=${taxCode}`);
    var res = "";
    if (taxCode == null || taxCode == "")
      res = await service_amis.get(
        `OrganizationUnit/GetOrganizationByTaxcode?status=${status}`
      );
    if (taxCode != null && taxCode != "")
      res = await service_amis.get(
        `OrganizationUnit/GetOrganizationByTaxcode?taxcode=${taxCode}&status=${status}`
      );
    return res;
  },

  /**
   * Hàm check trùng mã số thuế
   * @param {mã số thuế} taxCode 
   * created by vdthang 22.09.2020
   */
  async checkDuplicateTaxCode(taxCode) {
    var res = await service.get(`Organizations/taxCode?taxCode=${taxCode}`);
    return res;
  },

  /**
   * Cập nhật đơn vị chủ quản
   * @param {Obj chứa thông tin organization} obj
   * createdby nhuy 01.09.2020
   */
  async updateOrganization(obj) {
    const res = await service.put("/Organizations", obj);
    return res;
  },

  /**
   * Lấy danh sách chi nhánh (hạch toán phụ thuộc) của đơn vị chủ quản
   * @param {ID của đơn vị} organizationID
   * Create By: NQKHAI(24/11/2020)
   */
  async getOrganizationDependent(organizationID) {
    var res = await service.get(`Organizations/Dependent?organizationID=${organizationID}`);
    return res;
  },

  /**
   * Lấy thông tin đơn vị gợi ý theo Mã số thuế
   * MISA-ORG
   * Created by : NQKHAI(25/11/2020)
   * @param {Mã số thuế đơn vị} taxCode 
   */
  async getOrganizationSuggest(taxCode){
    var res = await service_general.get(`/OrganizationInfo/Suggest?taxCode=${taxCode}`)
    return res;
  }
};
