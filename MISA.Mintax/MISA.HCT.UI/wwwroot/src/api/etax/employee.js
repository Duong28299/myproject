import { api } from "../base";
const service = api(process.env.VUE_APP_BASE_API);
import employeeService from "./general"
export default {
  /**
   * Gọi service lấy danh sách nhân viên
   * @param {Danh sách lọc danh sách nhân viên} filter
   */
  async getEmployee(filter) {
    var url = `/Employee/filter?employeeID=${filter.employeeID}&citizenIdentityNo=${filter.citizenIdentityNo}&taxNo=${filter.taxNo}&employeeCode=${filter.employeeCode}`;
    var res = await service.get(url);
    return res;
  },

  /**
   * Gọi service lấy phân trang
   */
  async getEmployeePaging(filter){
    var res = employeeService.getPaging(filter, "Employee", true);
    return res;
  },
  /**
   * Lấy thông tin lịch sử theo id phân trang
   * created by nvbinh2
   * @param {bộ lọc phân trang} filter 
   */
  async getEmployeeHistoryPaging(filter){
    // Gọi service phân trang chung
    var res = employeeService.getPaging(filter, "EmployeeHistory",true);
    return res;
  } ,
  /**
   * Hàm lấy thông tin người nộp thuế theo EmployeeID
   * @param {thông tin người nộp thuế} obj
   * created by nvnguyen 26/11/2020
   */
  async getTaxPayerByID(id){
    var url =`Employee/${id}`
    var res = await service.get(url);
    return res;
  },

  /**
   * hàm xóa người nộp thuế theo id
   * @param {id nhân viên cần xóa tỏng bảng người nộp thuế} arrayID 
   * created by nvnguyen 27/11/2020
   */
  async deleteTaxPayerByID(arrayID) {
    return await service.delete("Employee",{
      data:arrayID,
    });
  },
};
