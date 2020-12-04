import { api } from "../base";

const service = api(process.env.VUE_APP_BASE_API);

export default {
  /**
   * Thực hiện gọi service lấy dữ liệu quốc gia
   */
  async getNationalites() {
    const url = "/CategoryDictionary/Nationalites";
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu tỉnh/thành phố
   */
  async getProvincials() {
    const url = "/CategoryDictionary/Province";
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu quận/huyện
   */
  async getDistricts() {
    const url = "/CategoryDictionary/Districts";
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu quận/huyện theo tỉnh/thành phố
   */
  async getDistrictsByDistrictsID(id) {
    const url = `/CategoryDictionary/Districts/${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu quận/huyện theo tỉnh/thành phố
   */ async getDistrictsByProvinceID(id) {
    const url = `/CategoryDictionary/Districts/${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu xã
   */
  async getWard() {
    const url = "/CategoryDictionary/Wards";
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu xã theo quận/huyện
   */
  async getWardByDistrictsID(id) {
    const url = `/CategoryDictionary/Wards/${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu ngân hàng
   */
  async getBanks() {
    const url = "/CategoryDictionary/Banks";
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu cơ quan thuế cấp cục
   */
  async getTaxAuthorities() {
    const url = "/CategoryDictionary/TaxAuthorities";
    var res = await service.get(url);
    return res;
  },

  /**
   * lấy cơ quan thuế quản lí theo cơ quan thuế cấp cục
   */
  async getListTaxAuthoritiesParent(id) {
    const url = `/CategoryDictionary/TaxAuthorities/${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu DBOPtion
   */
  async getDBOPtion() {
    const url = "/CategoryDictionary/DBOPtion";
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu DBOPtion
   */
  async getDBOPtionByType(id) {
    const url = `/CategoryDictionary/DBOPtion/${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu PlaceOfIssues
   */
  async getPlaceOfIssues() {
    const url = "/CategoryDictionary/PlaceOfIssues";
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu PlaceOfIssues
   */
  async getPlaceOfIssuesByIssueTypeCode(id) {
    const url = `/CategoryDictionary/PlaceOfIssues/${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Hàm gọi service lấy nơi cấp theo NationalID
   * created by nvnguyen 07/11/2020
   */
  async getPlaceOfIssuesByNationalID(id){
    const url = `/CategoryDictionary/PlaceOfIssues/NationalID/${id}`;
    var res = await service.get(url);
    return res;
  }, 

  /**
   * Thực hiện gọi service lấy dữ liệu position
   * createdby ntdong
   **/
  async getPosition() {
    const url = `/Position`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện gọi service lấy dữ liệu bộ phận phòng ban
   * createdby ndhuy 02.10.2020
   * @param {*} getType 
   */
  async getOrganizationUnit(style,type){
    const url = `/OrganizationUnits?style=${style}&type=${type}`;
    var res = await service.get(url);
    return res;
  }
};
