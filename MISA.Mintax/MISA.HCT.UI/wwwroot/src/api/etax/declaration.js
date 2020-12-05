import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);

export default {
  /**
   * Hàm lấy thông tin chi tiết tờ kê khai thuế
   * @param {id của object chứa thông tin của bảng tính thuế} id
   * @param {id của headers} organizationID
   * created by nhdoanh (13/7/2020)
   */
  async getDeclarationTemporaryTaxID(id) {
    var url = `/Declaration/filter?temporaryTaxID=${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Hàm lấy thông tin chi tiết tờ kê khai thuế
   * @param {id của object chứa thông tin của tờ khai thuế} id
   * @param {id của headers} organizationID
   * created by nhdoanh (20/7/2020)
   */
  async getDeclarationID(id) {
    var url = `/Declaration/${id}`;
    var res = await service.get(url);
    return res;
  },
  /**
   * Hàm lấy thông tin chi tiết tờ kê khai thuế
   * @param {filter chứa id của tờ khai}
   * created by nqhiep (24/9/2020)
   */
  async getViewDeclaration(filter) {
    var url = `/Declaration/filter?declarationID=${filter}`;
    var res = await service.get(url);
    return res;
  },

  async getViewDeclarationPaging(declarationID, filter) {
    var url = `/Declaration/${declarationID}/Paging?isGetDeclaration=${filter.isGetDeclaration}&hasTaxNo=${filter.hasTaxNo}&pageIndex=${filter.pageIndex}&pageSize=${filter.pageSize}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Gọi service lấy danh sách tờ khai
   * @param {Danh sách lọc danh sách tờ khai} filter
   */
  async getDeclaration(filter) {
    var url = `/Declaration/Paging?keyword=${filter.keyword}&month=${filter.month}&year=${filter.year}&pageIndex=${filter.pageIndex}&pageSize=${filter.pageSize}&declarationStatus=${filter.declarationStatus}&groupMode=${filter.groupMode}&groupType=${filter.groupType}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Hàm build tờ khai để ký nộp
   * @param {Thông tin tờ khai} declaration
   * Created by: CVCuong created date: 27/8/2020
   */
  async buildDeclaration(declaration) {
    var url = `/Declaration/Creator`;
    var res = await service.post(url, declaration);
    return res;
  },

  /**
   * Thực hiện xóa dữ liệu tờ khai
   * @param {data chứa mảng id các bảng tạm tính}
   * created by tcduong 16.9.2020
   */
  async deleteDeclaration(data) {
    return await service.delete("/Declaration", {
      data: data,
    });
  },

  /**
   * Hàm thêm tờ khai
   * @param {Thông tin tờ khai} declaration
   * Created by: NVANH (15.09.2020)
   */
  async postDeclaration(declaration) {
    var url = "/Declaration";
    var res = await service.post(url, declaration);
    return res;
  },

  /**
   * Lấy danh sách chi tiết hồ sơ đăng ký MST
   * @param filter obj chứa các trường lọc
   * created by ndhuy 16.09.2020
   */
  async getDeclarationDetail(filter) {
    var obj = {
      declarationID: filter.declarationID,
      keyword: filter.keyword,
      pageSize: filter.pageSize,
      pageIndex: filter.pageIndex,
      organizationUnit: filter.organizationUnit,
    };
    var url = `/DeclarationDetail/Paging`;
    var res = await service.post(url, obj);
    return res;
  },

  /**
   * Hàm thêm hồ sơ chi tiết
   * @param {Thông tin tờ khai chi tiết} data
   * Created by: LTTUAN (19.09.2020)
   */
  async postDeclarationDetail(data) {
    var url = "/DeclarationDetail";
    var res = await service.post(url, data);
    return res;
  },

  /**
   * Xóa chi tiết tờ khai(DeclarationDetail)
   * @param {data chứa mảng id các bảng tạm tính}
   * created by tcduong 16.9.2020
   */
  async deleteDeclarationDetail(data) {
    return await service.delete("/DeclarationDetail", {
      data: data,
    });
  },
  /**
   * Service xem preview thông báo Tổng cục Thuế
   * Created by: NVHUNG created date: 16/9/2020
   */
  getPreviewNotification(taxCode, submissionCode) {
    var url = `/Previews?taxCode=${taxCode}&submissionCode=${submissionCode}`;
    return process.env.VUE_APP_BASE_API_MTAX + `/MTax${url}`;
  },

  /**
   * Lấy thông tin chi tiết 1 hồ sơ đăng ký MST
   * @param DeclarationDetailID mã chi tiết tờ khai
   * createdby lttuan 19.09.2020
   */
  async getDeclarationDetailByDeclarationDetailID(declarationDetailID) {
    var url = `/DeclarationDetail/${declarationDetailID}`;
    var res = await service.get(url);
    return res;
  },

  /** Service lấy danh sách chi tiết người phụ thuộc
   * Created By NQTrung created date 18/09/2020
   */
  async getDeclarationDetailDependent(id) {
    var url = `/DeclarationDetailDependent/filter?declarationDetailID=${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Service Xóa danh sách người phụ thuộc
   * @param {data} data chứa mảng DeclarationDetailDependentID
   * Created by NQTrung created date 21/09/2020
   */
  async deleteDeclarationDetailDependent(data) {
    return await service.delete("/DeclarationDetailDependent", {
      data: data,
    });
  },

  /**
   * service cập nhật tờ khai
   * @param {data} data obj tờ khai cập nhật
   * createdby ndhuy 22.09.2020
   */
  async updateDeclaration(data) {
    var url = "/Declaration";
    var res = await service.put(url, data);
    return res;
  },

  /**
   * service cập nhật tờ khai
   * @param {data} data obj tờ khai cập nhật
   * createdby lttuan 22.09.2020
   */
  async putDeclarationDetail(data) {
    var url = "/DeclarationDetail";
    var res = await service.put(url, data);
    return res;
  },

  /** Service Thêm người phụ thuộc
   * @param {Object người phụ thuộc} data
   * Created by: TCDUONG created date 22.09.2020
   */
  async postDeclarationDetailDependent(data) {
    return await service.post("/DeclarationDetailDependent", data);
  },

  /**
   * Service chỉnh sửa thông tin người phụ thuộc
   * @param {id} id của người phụ thuộc
   * Created by NQTrung  created date 25/09/2020
   */
  async getDeclarationDetailDependentByID(id) {
    var url = `/DeclarationDetailDependent/${id}`;
    var res = await service.get(url);
    return res;
  },

  /**
   *  Service chỉnh sửa thông tin người phụ thuộc
   * @param {data} data Obj thông tin người phụ thuộc
   * createed by NQTrung created date 26/09/2020
   */
  async putDeclarationDetailDependent(data) {
    var url = "/DeclarationDetailDependent";
    var res = await service.put(url, data);
    return res;
  },

  /**
   * Filter depentdent
   * @param {thông tin cần filter} filter
   */
  async getDepentdent(filter) {
    var url = `/Dependents/filter?taxNo=${filter.taxNo}&citizenIdentityNo=${filter.citizenIdentityNo}&birthCertificateNumber=${filter.birthCertificateNumber}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Filter declarationDetal
   * @param {thông tin cần filter} filter
   * created by tcduong 30.9.2020
   */
  async getFilterDeclarationDetail(filter) {
    var url = `DeclarationDetail/filter?declarationID=${filter.declarationID}&citizenIdentityNo=${filter.citizenIdentityNo}&taxNo=${filter.taxNo}&employeeCode=${filter.employeeCode}`;
    var res = await service.get(url);
    return res;
  },
  /** Service lấy chi tiết người nộp thuế
   * Created By tcduong created date 18/09/2020
   */
  async getDeclarationDetailById(id) {
    var url = `/DeclarationDetail/${id}`;
    var res = await service.get(url);
    return res;
  },

  /** Service lấy chi tiết cho phụ lục 1bk-2bk tờ khai quyết toán thuế
   * @param {filter} filter obj chứa các thông tin keyword, SettlementID, pageIndex, pageSize
   * created by nqhiep 27/10/2020
   */
  async getDeclarationSettlementDetail(filter) {
    var url = `/DeclarationSettlementDetail/Paging?SettlementID=${filter.SettlementID}&DeclarationSettlementDetailType=${filter.DeclarationSettlementDetailType}&keyword=${filter.keyword}&pageIndex=${filter.pageIndex}&pageSize=${filter.pageSize}`;
    var res = await service.get(url);
    return res;
  },
};
