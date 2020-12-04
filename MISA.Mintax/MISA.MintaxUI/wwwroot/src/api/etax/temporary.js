import { api } from "../base";

const service = api(process.env.VUE_APP_BASE_API);

export default {
  /**
   * Thực hiện gọi service lấy dữ liệu bảng tạm tính
   * @param {data chứa thông tin filter} data
   * created by vdthang 06.07.2020
   */
  async getTemporaryData(data) {
    const url = `/TemporaryTax/Paging?keyword=${data.keyword}&month=${data.month}&year=${data.year}&pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&taxType=${data.taxType}`;
    var res = await service.get(url, {
      headers: {
        token:
          "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxZjkxOTc1Ni0yYmFmLTQ3YWQtYjI5OS1kMDdlNDIxY2ViYTYiLCJ1bmlxdWVfbmFtZSI6WyJ0dWFuZ2h1bmdwcm8xMjMiLCJNSVNBLk1pbnRheC5BbWlzLkluZnJhc3RydWN0dXJlLkRhdGEuUmVwb3NpdG9yaWVzLkJhc2VSZXBvc2l0b3J5YDErPEdldERhdGE-ZF9fMTRbTUlTQS5NaW50YXguQW1pcy5BcHBsaWNhdGlvbkNvcmUuRW50aXRpZXMuUm9sZV0iXSwibmJmIjoxNTk4NTEzNjIxLCJleHAiOjE1OTg2MDAwMjEsImlhdCI6MTU5ODUxMzYyMX0.Wc6wOI37iwTr94ApdBdmTHimvaI2-RtonV8TyRv6kSjzOhAMQ9YELadQWh6fnw0LCzLIUMOGzek13hNABted8g",
      },
    });
    return res;
  },

  /**
   * Thực hiện check dữ liệu bảng tính thuế
   * @param {data chứa thông tin bảng tính thuế cần check}
   * created by : NTNgoc
   * created date: 15/7/2020
   */
  async checkTemporaryTax(data) {
    var url = `/TemporaryTax/filter?incomeType=${data.incomeType}&temporaryTaxType=${data.temporaryTaxType}&periodMonthOrQuarter=${data.periodMonthOrQuarter}&year=${data.year}`;

    var res = await service.get(url);
    return res;
  },

  /**
   * Thực hiện thêm mới bảng tính thuế
   * @param {dữ liệu bảng tạm tính} data
   * created by vdthang 22.09.2020
   */
  insertTemporaryTax(data) {
    return service.post("/TemporaryTax", JSON.parse(data));
  },

  /**
   * Thực hiện xóa dữ liệu bảng tạm tính
   * @param {data chứa mảng id các bảng tạm tính}
   * created by cvcuong 14.7.2020
   */
  async deleteTemporaryTax(data) {
    return await service.delete("/TemporaryTax", {
      data: data,
    });
  },

  /**
   *Hàm thực hiện lấy tên kiểu thuế
   * @param {Kiểu Thuế} taxType
   * created by vdthang 22.09.2020
   */
  async getNameTemporaryTax(taxType) {
    const url = `/TemporaryTax/GetName?taxType=${taxType}`;
    return await service.get(url);
  },
};
