<template>
  <div id="app">
    <v-app id="inspire">
    <v-toolbar color="teal" dark fixed app>
      <v-toolbar-title>Mitt Sundsvall</v-toolbar-title>

      <v-spacer></v-spacer>
      
      <v-menu offset-y
              :close-on-content-click="false"
              :nudge-width="200"
              v-model="menu">
        <v-btn icon slot="activator" dark>
          <v-btn icon>
            <v-icon>settings</v-icon>
          </v-btn>
          <v-badge overlap color="red">
          </v-badge>
        </v-btn>
        <v-card>
          <v-list>
            <v-list-tile avatar>
              <v-list-tile-avatar>
                <img src="/static/img/user/dato.jpg" alt="Daniel">
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>Daniel Toivonen</v-list-tile-title>
                <v-list-tile-sub-title>Datahacker</v-list-tile-sub-title>
              </v-list-tile-content>
              
            </v-list-tile>
          </v-list>
          <v-divider></v-divider>
          <v-list>
            <v-list-tile>
              <v-list-tile-action>
                <v-switch v-model="air" color="blue" v-on:change="toggle('air')"></v-switch>
              </v-list-tile-action>
              <v-list-tile-title>Visa luft</v-list-tile-title>
            </v-list-tile>

            <v-list-tile>
              <v-list-tile-action>
                <v-switch v-model="traffic" color="green" v-on:change="toggle('traffic')"></v-switch>
              </v-list-tile-action>
              <v-list-tile-title>Visa trafik</v-list-tile-title>
            </v-list-tile>

            <v-list-tile>
              <v-list-tile-action>
                <v-switch v-model="crime" color="red" v-on:change="toggle('crime')"></v-switch>
              </v-list-tile-action>
              <v-list-tile-title>Visa brott</v-list-tile-title>
            </v-list-tile>

            <v-list-tile>
              <v-list-tile-action>
                <v-switch v-model="safety" color="purple" v-on:change="toggle('safety')"></v-switch>
              </v-list-tile-action>
              <v-list-tile-title>Visa säkerhet</v-list-tile-title>
            </v-list-tile>
          </v-list>
        </v-card>
      </v-menu>
      <v-btn icon @click="reportProblem">
        <v-icon>add_alert</v-icon>
      </v-btn>
    </v-toolbar>
    <v-content>
      <v-container fluid fill-height>
        <v-layout
          justify-center
          align-center
        >
        <router-view/>
        </v-layout>

        <!-- RAPPORTERA -->
        <v-dialog v-model="dialog" max-width="500px">
          <v-card>
            <v-card-title>
              Rapportera problem
            </v-card-title>
            <v-card-text>
              <!-- FLÖDE -->
              <v-stepper v-model="e1">
                <v-stepper-header>
                  <v-stepper-step step="1" :complete="e1 > 1">Kategori</v-stepper-step>
                  <v-divider></v-divider>
                  <v-stepper-step step="2" :complete="e1 > 2">Information</v-stepper-step>
                </v-stepper-header>
                <v-stepper-items>
                  <v-stepper-content step="1">
                    <v-card color="grey lighten-3" class="mb-5" height="200px" style="padding: 10px">
                      
                      <h4>Kategori</h4>

                      <v-btn :color="airSelected ? 'primary' : ''" @click.native="selectCategory('air')">
                        <v-icon>cloud_queue</v-icon>
                      </v-btn>

                      <v-btn :color="trafficSelected ? 'primary' : ''" @click.native="selectCategory('traffic')">
                        <v-icon>directions_car</v-icon>
                      </v-btn>

                       <v-btn :color="safetySelected ? 'primary' : ''" @click.native="selectCategory('safety')">
                        <v-icon>lightbulb_outline</v-icon>
                      </v-btn>
                      <br/>
                      <h4>Allvarlighetsgrad</h4>
                      <v-slider :color="sliderColor" v-model="reportModel.level" step="1" max="5"></v-slider>

                    </v-card>
                    <v-btn :disabled="(!safetySelected && !trafficSelected && !airSelected) || reportModel.level == 0" color="primary" @click.native="e1 = 2">Fortsätt</v-btn>
                    <v-btn flat @click.stop="dialog=false">Avbryt</v-btn>
                  </v-stepper-content>
                  <v-stepper-content step="2">
                    <v-card color="grey lighten-3" class="mb-5" height="200px" style="padding: 10px">
                      <v-text-field
                        label="Titel"
                        v-model="reportModel.title"
                        :counter="20"
                        required
                      ></v-text-field>

                      <v-text-field
                        label="Beskrivning"
                        v-model="reportModel.description"
                        :counter="100"
                        required
                      ></v-text-field>
                    </v-card>
                    <v-btn @click.native="e1 = 1">Tillbaka</v-btn>
                    <v-btn color="primary" @click.native="sendReport" :disabled="reportModel.description === '' || reportModel.title === ''" >Skicka</v-btn>
                    <v-btn flat @click.stop="dialog=false">Avbryt</v-btn>
                  </v-stepper-content>
                </v-stepper-items>
              </v-stepper>
              <!--SLUT FLÖDE -->
            </v-card-text>
          </v-card>
        </v-dialog>

      </v-container>
    </v-content>
    <v-footer color="teal" app>
      <span class="white--text">&copy; 2018 - Masarin Consulting Group AB</span>
    </v-footer>
  </v-app>
    <vue-snotify></vue-snotify>
  </div>
</template>

<script>
export default {
  name: 'app',
  watch: {
    reportModel: function (val, oldVal) {
      console.log('ändrat', val)
      if (val.level === 0) {
        this.sliderColor = 'primary'
      }
      if (val.level === 1) {
        this.sliderColor = 'warning'
      }
      if (val.level === 2) {
        this.sliderColor = 'warning'
      }
      if (val.level > 2) {
        this.sliderColor = 'error'
      }
    }
  },
  methods: {
    sendReport: function () {
      console.log('skickar...')
      console.log('model', this.reportModel)
      this.axios.post('events/', this.reportModel)
      .then(response => {
        console.log('skickat', response)
      })
      .catch(e => {
        console.log('ERROR', e)
      })
      // Resets
      this.dialog = false
      this.e1 = 1
      this.reportModel = {
        title: '',
        description: '',
        level: 0,
        category: '',
        location: {},
        userId: 1
      }
    },
    selectCategory: function (category) {
      if (category === 'air') {
        this.airSelected = true
        this.safetySelected = false
        this.trafficSelected = false
        this.reportModel.category = 'air'
      } else if (category === 'traffic') {
        this.airSelected = false
        this.safetySelected = false
        this.trafficSelected = true
        this.reportModel.category = 'traffic'
      } else if (category === 'safety') {
        this.airSelected = false
        this.safetySelected = true
        this.trafficSelected = false
        this.reportModel.category = 'safety'
      }
      navigator.geolocation.getCurrentPosition((location) => {
        this.reportModel.location = {
          Latitude: location.coords.latitude,
          Longitude: location.coords.longitude
        }
      })
    },
    reportProblem: function () {
      this.dialog = true
    },
    toggle: function (category) {
      if (category === 'air') {
        this.$store.commit('setAir', !this.$store.state.air)
      } else if (category === 'traffic') {
        this.$store.commit('setTraffic', !this.$store.state.traffic)
      } else if (category === 'safety') {
        this.$store.commit('setSafety', !this.$store.state.safety)
      } else if (category === 'crime') {
        this.$store.commit('setCrime', !this.$store.state.crime)
      }
    }
  },
  created: function () {
    navigator.geolocation.getCurrentPosition((location) => {
      // alert('Hittad gps' + location.coords.longitude)
    })
    this.air = this.$store.state.air
    this.crime = this.$store.state.crime
    this.safety = this.$store.state.safety
    this.traffic = this.$store.state.traffic
  },
  data: () => ({
    sliderColor: '',
    air: false,
    crime: false,
    safety: false,
    traffic: false,
    drawer: false,
    fav: true,
    menu: false,
    message: false,
    dialog: false,
    e1: 0,
    airSelected: false,
    safetySelected: false,
    trafficSelected: false,
    reportModel: {
      title: '',
      description: '',
      level: '',
      category: '',
      location: {},
      userId: 1
    }
  }),
  props: {
    source: 'String'
  }
}
</script>

<style>
html {
  overflow-y: auto;
}
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
