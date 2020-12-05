import { api } from "../base/base-amis-connector";
// import Axios from "axios";
const service = api(process.env.VUE_APP_BASE_AMISCONNECTOR_API);

export default {
  /**
   * Lấy thông tin user,tenant và phiên làm việc theo sid
   * @param {sid} val
   */
  async checkUser(val) {
    var res = await service.get(`/User/CheckUser`, undefined, {
      headers: {
        "iv-sid": val,
      },
    });
    return res;
  },
  
  // async getSessionID(val) {
  //   var res = await service.get("AMIS/Session", {
  //     headers: {
  //       "iv-sid": val,
  //     },
  //   });
  //   return res;
  // },

  // /**
  //  * Lấy thông tin sid
  //  * @param {sid} val
  //  */
  async getSessionID(val) {
    var res = await service({
      method: "GET",
      url: "AMIS/Sesion",
      headers: {
        "iv-sid": val,
      },
    });
    return res;
  },
  async getSessionIDtest(val) {
    var res = await service.get("/AMIS/Session?sid=" + val);
    return res;
  },
};
