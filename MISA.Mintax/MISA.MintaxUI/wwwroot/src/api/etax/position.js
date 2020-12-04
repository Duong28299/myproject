import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);

export default {
/**
 * Gọi service lấy danh sách vị trí
 * created by tcduong 25.9.2020
 * @param {Danh sách lọc danh sách nhân viên} filter
 */
async getPosition() {
  var url="/Position";
  var res = await service.get(url);
  return res;
},

};