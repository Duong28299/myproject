import { api } from "../base/base-auth";

const service = api(process.env.VUE_APP_AUTH_API);

export default {
  async getToken(sid) {
    var res = await service.get("/token", {
      headers: {
        sid: sid,
      },
    });
    return res;
  },
};
