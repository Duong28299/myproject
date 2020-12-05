import { api } from "../base";

const service = api(process.env.VUE_APP_BASE_API);

export default {
  /**
   * Thực hiện gọi service lấy dữ liệu bảng chi tiết tạm tính
   * @param {object} data object chứa params để gọi service
   * created by N.T. Dũng 13.07.2020
   */
  async getTemporaryDetailData(data) {
    var url = `TemporaryTaxDetail/Paging?temporaryTaxID=${data.temporaryTaxID}&pageSize=${data.pageSize}&keyword=${data.keyword}&pageIndex=${data.pageIndex}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện xóa dữ liệu người lao động trong bảng chi tiết tạm tính
   * @param {data chứa mảng id các bảng tạm tính}
   * created by cvcuong 15.7.2020
   */
  async deleteTemporaryTaxDetail(data) {
    return await service.delete("/TemporaryTaxDetail", {
      data: data,
    });
  },

  /**
   *  Thực hiện lấy dữ liệu của bảng tạm tính thông qua id
   *  @param {int} id ID bảng tạm tính
   *  @param {string} organizationId ID tổ chức
   */
  async getTemporaryDetailDataByID(id) {
    var url = `TemporaryTaxDetail/${id}`;
    var res = await service.get(url);
    return res;
  },
};
