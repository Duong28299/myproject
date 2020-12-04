import organizationService from "@/api/etax/organization";
import Cookie from "js-cookie";
const state = {
  organization: {},
  organizationList: [],
  organizationId: Cookie.get('orgId') ? Cookie.get('orgId') : '',
};

const mutations = {
  ADD_ERROR_LOG: (state, log) => {
    state.logs.push(log);
  },
  CLEAR_ERROR_LOG: (state) => {
    state.logs.splice(0);
  },
  SET_ORGID: (state, id) => {
    state.organizationId = id;
  },
  ADD_ORGANIZATION: (state, list) => {
    state.organizationList = list;
  },
};
const actions = {
  async getOrganizationById({ state }, organizationId) {
    var res = await organizationService.getOrganizationById(organizationId);
    if (res.data.Success == true) {
      return (state.organization = res.data.Data);
    }
  },
  addOrganizationList({ commit }, list) {
    commit('ADD_ORGANIZATION', list);
  },
};


export default {
  namespaced: true,
  state,
  mutations,
  actions,
};
