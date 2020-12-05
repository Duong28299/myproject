import axios from "axios";
import Cookie from "js-cookie";
// import { getToken ,clearToken} from "@/utils/token.js";
import { getToken } from "@/utils/token.js";

export const api = function(baseURL) {
  /**
   * Tạo Instance axios
   */
  var instanceAxios = axios.create({
    baseURL,
    // mode: "no-cors",
    validateStatus: function(status) {
      return status >= 100 && status <= 500;
    },
  });
  /**
   * Hàm Interceptor Request setup config trước khi gửi đi
   * Created by NVBINH 26.08.2020
   */
  instanceAxios.interceptors.request.use(
    function(config) {
      // Do something before request is sent
      // Nếu có Token được lưu trong Localstorage thì sẽ gắn và gửi đi
      config.headers = {
        // ...config.headers,
        // Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxZjkxOTc1Ni0yYmFmLTQ3YWQtYjI5OS1kMDdlNDIxY2ViYTYiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwianRpIjoiNzA0MjgwNzctMmZhZC00MTRmLThlYTEtNDQ2YjEyMDRhOGM2IiwiaWF0IjoiMTAvMDYvMjAyMCAxMDo1OToyMCIsInNlc3Npb25faWQiOiJkMzU3YThkMzljMTk0MTBhYTNhYmFkYjI1OWViODRkZSIsInRlbmFudF9pZCI6ImQzOTAwM2U5LTc0OTctNGI3MS1hYmMzLTNiNTlhM2QwYjlmYiIsInRlbmFudF9jb2RlIjoidGVzdDAxIiwiZnVsbG5hbWUiOiJIxrBuZyBOZ3V54buFbiBUdeG6pW4iLCJuYmYiOjE2MDE5ODE5NjAsImV4cCI6MTYwMjA1NzEwMywiaXNzIjoiaHR0cDovL21pbnRheGF1dGgubWlzYS5jb20udm4iLCJhdWQiOiJodHRwOi8vbWludGF4YXV0aC5taXNhLmNvbS52biJ9.ZYB9-X03m0_L3ooll9tNQJZqRDxyHrVYQH3fl5-3Xt_HFkUxmklJwQCLhWJ-n8Tzt8rWpjf00TptbApG2JJ5YpYX-b7JYTS2UuLYMcz7vs9fv8yChF0PsbDngqJ-ngrOHbQthvoW7b5n_0uqdRX7GE5kRIpa8n7eNnFJE-vLxCvgZ2bH86k5JFqqawpVaFMKOYQN0KFaPtcw0G0FMMFgyHnxv9F1OKsKJ64GHnWIrfsWPtEQqzi8WRn1I3vckl1k0ARV9HS3p_fPX9WeHmusXtp8IqSmwHMjoSSr31hC_T_xS2y35yL49ykbrj5oEi16Zi8H5TeEIMN8KXsSw9MgEw
        // Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZmU5NTdkNy05NjQ2LTQ2ZTItODEwNi03MWFkZGYxODZiNGQiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwianRpIjoiYjIyZTA4ZjEtZjg0ZS00MGIxLTg2YTEtMDZiMWRjZTYzNjc1IiwiaWF0IjoiMTEvMTEvMjAyMCAwODozNzoxMyIsInNlc3Npb25faWQiOiI1YmNiMWEzY2U0YTI0MTAxODY2NGY4YjljMzdjMjdmYiIsInRlbmFudF9pZCI6IjM0MjRlZmZjLWIwNWQtNGQ3Ny04YTQwLTAxNmM4MGJhZjJjZiIsInRlbmFudF9jb2RlIjoiOTlYQzdMVDkiLCJmdWxsbmFtZSI6IsSQw6BvIFRo4buLIExpbmggSMawxqFuZyIsIm5iZiI6MTYwNTA4MzgzMywiZXhwIjoxNjA1MTcwMjI1LCJpc3MiOiJodHRwOi8vbWludGF4YXV0aC5taXNhLmNvbS52biIsImF1ZCI6Imh0dHA6Ly9taW50YXhhdXRoLm1pc2EuY29tLnZuIn0.iVvg2-N2sP1hKxDL_Ph9nrwNYvh3Q4awqwek7BtuR_7NuzrMtjUo4jfa3F-XG4bqoHBtWBTgXe_eKod32PIr2kbSfanwunGC-jKTTHTDyKDcg50Hlpfjbwv5FRb3DORs1YqdqPyU5AP5C8vZwM0jr4uQcYy3HgeHERm8VsO1veS-uzzX2vvpBu4y4EUJMDtN3T13uW95X2VH22P6WgLVuOLz3EMF_fG4MDimbYMabL5jZXvBzp9qudYjAGBFP5OiXxru8HOwwScExWDwsVcsXVD92p5nv7RvmeOeN0UOhh144gE17MFphGYpTphpyzpTqIquqa_ylriF5-XeNFeI9g
        // Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZmU5NTdkNy05NjQ2LTQ2ZTItODEwNi03MWFkZGYxODZiNGQiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwianRpIjoiOTAzZDU3MWMtZjFjNi00ZjU3LWFjMTctNjUzMDM3ZmI4YWQ1IiwiaWF0IjoiMTEvMTgvMjAyMCAwMzoyOTozOSIsInNlc3Npb25faWQiOiJjZDc5NWJmYWUyMDQ0YzM0ODBkZDg0ODc1OWVlMjNkOSIsInRlbmFudF9pZCI6IjM0MjRlZmZjLWIwNWQtNGQ3Ny04YTQwLTAxNmM4MGJhZjJjZiIsInRlbmFudF9jb2RlIjoiOTlYQzdMVDkiLCJmdWxsbmFtZSI6IsSQw6BvIFRo4buLIExpbmggSMawxqFuZyIsIm5iZiI6MTYwNTY3MDE3OSwiZXhwIjoxNjA1NzU2NDI2LCJpc3MiOiJodHRwOi8vbWludGF4YXV0aC5taXNhLmNvbS52biIsImF1ZCI6Imh0dHA6Ly9taW50YXhhdXRoLm1pc2EuY29tLnZuIn0.fLPZ7yRS8gjgdWLXPLBA9ElhkvW1akOBFp2rzbFc2MtEtPklmkr0PlC7VWA5COYdmQGRof2C0O5BAM2OwZZ7n0S5PU04Pi3vmpR2zQIYvqDPJsPhC3tky1DVvYiF7JchtcaRS9IOilIAY9k2vx_SvitM-jgJxA_MmQIOMqg3qyPWdGTMOZyXvNeYskKrhLnxN3XuImL38Z7dec9bnLv4dtLWgETEOLgVfZnSD7nxwJZK6R5OsKlNC6vLVYhUkqn9Y6K1-kROtjAGVOKak-7MLy_z4AsD6hx3Cn7nHVmV1FK1wAyg97WVcKqFkyCchaG5CzJs54yH4YPUWAnE-tjLnA
        Authorization:
          getToken() != null
            ? getToken() : "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxZjkxOTc1Ni0yYmFmLTQ3YWQtYjI5OS1kMDdlNDIxY2ViYTYiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwianRpIjoiNzA0MjgwNzctMmZhZC00MTRmLThlYTEtNDQ2YjEyMDRhOGM2IiwiaWF0IjoiMTAvMDYvMjAyMCAxMDo1OToyMCIsInNlc3Npb25faWQiOiJkMzU3YThkMzljMTk0MTBhYTNhYmFkYjI1OWViODRkZSIsInRlbmFudF9pZCI6ImQzOTAwM2U5LTc0OTctNGI3MS1hYmMzLTNiNTlhM2QwYjlmYiIsInRlbmFudF9jb2RlIjoidGVzdDAxIiwiZnVsbG5hbWUiOiJIxrBuZyBOZ3V54buFbiBUdeG6pW4iLCJuYmYiOjE2MDE5ODE5NjAsImV4cCI6MTYwMjA1NzEwMywiaXNzIjoiaHR0cDovL21pbnRheGF1dGgubWlzYS5jb20udm4iLCJhdWQiOiJodHRwOi8vbWludGF4YXV0aC5taXNhLmNvbS52biJ9.ZYB9-X03m0_L3ooll9tNQJZqRDxyHrVYQH3fl5-3Xt_HFkUxmklJwQCLhWJ-n8Tzt8rWpjf00TptbApG2JJ5YpYX-b7JYTS2UuLYMcz7vs9fv8yChF0PsbDngqJ-ngrOHbQthvoW7b5n_0uqdRX7GE5kRIpa8n7eNnFJE-vLxCvgZ2bH86k5JFqqawpVaFMKOYQN0KFaPtcw0G0FMMFgyHnxv9F1OKsKJ64GHnWIrfsWPtEQqzi8WRn1I3vckl1k0ARV9HS3p_fPX9WeHmusXtp8IqSmwHMjoSSr31hC_T_xS2y35yL49ykbrj5oEi16Zi8H5TeEIMN8KXsSw9MgEw",
        OrganizationID: Cookie.get("OrganizationID")
          ? Cookie.get("OrganizationID")
          : "",
      };
      return config;
    },
    function(error) {
      // Do something with request error
      return Promise.reject(error);
    }
  );
  /**
   * Hàm Interceptor Response xử lý response trước khi trả về cho nơi gọi
   * Created by NVBINH 26.08.2020
   */
  // instanceAxios.interceptors.response.use(
  //   function(response) {
  //     // Any status code that lie within the range of 2xx cause this function to trigger
  //     // Do something with response data
  //     if (response.status == 401) {
  //       clearToken();
  //       window.location.href = process.env.VUE_APP_AMIS_URL;
  //     } else if (response.status == 403) {
  //       return response;
  //     } else {
  //       return response;
  //     }
  //   },
  //   function(error) {
  //     // Any status codes that falls outside the range of 2xx cause this function to trigger
  //     // Do something with response error
  //     return Promise.reject(error);
  //   }
  // );
  return instanceAxios;
};
