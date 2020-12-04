import axios from "axios";


export const api = function(baseURL) {
  return axios.create({
    baseURL,
    validateStatus: function(status) {
      return status >= 100 && status <= 500;
    },
  });
};
