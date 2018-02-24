import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
// import * as Cookies from 'js-cookie'

Vue.use(Vuex)

const state = {
  air: true,
  crime: true,
  safety: true,
  traffic: true
}
export default new Vuex.Store({
  state,
  getters: {
    air: state => state.air,
    crime: state => state.crime,
    safety: state => state.safety,
    traffic: state => state.traffic

    // storedNumberMatches(state) {
    //  return matchNumber => {
    //   return state.safelyStoredNumber === matchNumber
    //  }
    // }
    // Shorthand:
    // storedNumberMatches: state => matchNumber => state.safelyStoredNumbers === matchNumber
  },
  plugins: [createPersistedState()],
  mutations: {
    setAir (state, n) {
      state.air = n
    },
    setCrime (state, n) {
      state.crime = n
    },
    setSafety (state, n) {
      state.safety = n
    },
    setTraffic (state, n) {
      state.traffic = n
    }
  }
})
