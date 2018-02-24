// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import Vuex from 'vuex'
import store from './store/store'

import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'

import axios from 'axios'
import VueAxios from 'vue-axios'
import App from './App'
import router from './router'
import LineChart from './components/graphs/LineChart.js'
import ReactiveExample from './components/graphs/ReactiveExample.js'
import GridLoader from 'vue-spinner/src/GridLoader.vue'
import LoadingScreen from './components/loading/LoadingScreen.vue'
import Snotify from 'vue-snotify'
// You also need to import the styles. If you're using webpack's css-loader, you can do so here:
import 'vue-snotify/styles/material.css' // or dark.css or simple.css

// Map
import * as VueGoogleMaps from 'vue2-google-maps'

// Webcam
import VueNewWebcam from './components/vue-new-webcam/VueNewWebcam.js'

// Calendar
import 'vue-event-calendar/dist/style.css' // ^1.1.10, CSS has been extracted as one file, so you can easily update it.
import vueEventCalendar from 'vue-event-calendar'
Vue.use(vueEventCalendar, { locale: 'sv', weekStartOn: 1 })

// Vuex
Vue.use(Vuex)

// SignalR
// var signalR = require('./signalr-client.min.js')
var signalR = require('@aspnet/signalr-client/dist/browser/signalr-client-1.0.0-alpha2-final.min.js')
Vue.prototype.$signalR = signalR

Vue.prototype.$VueWebcam = VueNewWebcam

Vue.use(VueGoogleMaps, {
  load: {
    key: process.env.GOOGLE_MAPS_API_KEY,
    libraries: 'places,drawing,visualization' // This is required if you use the Autocomplete plugin
    // OR: libraries: 'places,drawing'
    // OR: libraries: 'places,drawing,visualization'
    // (as you require)
  }
})

Vue.use(Vuetify)

Vue.config.productionTip = false

Vue.use(Snotify)
Vue.use(VueAxios, axios)

Vue.axios.defaults.baseURL = process.env.BACKEND_URL // 'http://localhost:5000/api/'

// Components
Vue.component('line-chart', LineChart)
Vue.component('reactive-example', ReactiveExample)
Vue.component('grid-loader', GridLoader)
Vue.component('loading-screen', LoadingScreen)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: { App }
})
