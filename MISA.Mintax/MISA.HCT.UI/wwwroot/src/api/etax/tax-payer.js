import { api } from "../base";

const service = api(process.env.VUE_APP_BASE_API);
// const service1 = api(process.env.VUE_APP_BASE_API_TEST);

export default {
  /**
   * Lấy danh sách thông tin người nộp thuế
   * @param {object chứa các param để gọi service} obj 
   * created by vdthang 22.09.2020
   * modified by nvnguyen 19/11/2020 (thay đổi param truyền vào)
   */
  async getTaxPayer(obj) {
    var url =`Employee/filter?keyword=${obj.keyword}&organizationUnitID=${obj.organizationID}&taxNo=${obj.TaxNo}&pageIndex=${obj.pageIndex}&pageSize=${obj.pageSize}`
    var res = await service.get(
      url
    );
    return res.data;
  },

  /**
   * Hàm thêm chi tiết bảng tạm tính
   * @param {thông tin chi tiết bảng tạm tính} obj 
   * created by vdthang 22.09.2020
   */
  async createTaxPayer(obj) {
    const res = await service.post(`/TemporaryTaxDetail`, obj);
    return res;
  },

  /**
   * Call service cập nhật thông tin người nộp thuế trong chi tiết bảng tạm tính
   * @param {object chứa thông tin đã sửa đổi của người nộp thuế} obj 
   * created by: N.T. Dũng 17.07.2020
   */
  async editTaxPayer(obj) {
    const res = await service.put("/TemporaryTaxDetail", obj);
    return res;
  },
};
