<template>
  <v-layout row>
    <gmap-map
      :center="{lat: currentLocation.lat, lng: currentLocation.lng}"
      :zoom="14"
      style="width: 100%; height: 100%;"
    >
      <gmap-marker
        :position="{lat: currentLocation.lat, lng: currentLocation.lng}"
        :clickable="true"
        :draggable="false"
      ></gmap-marker>

      <!-- All -->
      <gmap-marker
        :key="index"
        v-for="(c, index) in allCollection"
        :position="{lat: c.Location.Latitude, lng: c.Location.Longitude}"
        :clickable="true"
        :draggable="false"
        :icon=getMarkerIcon(c.Category)
        @click="showInfo(c.Title, c.Description)"
        v-if="show(c.Category)"
      ></gmap-marker>
    </gmap-map>

    <v-dialog :persistent="true" v-model="infoDialog" max-width="500px">
        <v-card>
          <v-card-title>
            <span>{{title}}</span>
            <v-spacer></v-spacer>
            </v-card-title>
              {{description}}
            <v-card-actions>
            <v-btn color="primary" flat @click.stop="closeDialog">Close</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
  </v-layout>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'Map',
  methods: {
    show: function (category) {
      if (category === 'air') {
        return this.air
      } else if (category === 'traffic') {
        return this.traffic
      } else if (category === 'safety') {
        return this.safety
      } else if (category === 'crime') {
        return this.crime
      }
    },
    getMarkerIcon: function (category) {
      if (category) {
        var icon = {
          scaledSize: {
            width: 32, height: 32
          }
        }

        if (category === 'air') {
          icon.url = 'https://www.shareicon.net/download/2015/09/14/101077_cloud_512x512.png'
          icon.strokeColor = 'yellow'
        } else if (category === 'traffic') {
          icon.url = 'https://www.shareicon.net/data/128x128/2017/06/01/886655_signal_512x512.png'
        } else if (category === 'safety') {
          icon.url = 'https://cdn3.iconfinder.com/data/icons/creative-and-idea/500/Idea-thinking-think-concept_3-512.png'
        } else if (category === 'crime') {
          icon.url = 'https://www.shareicon.net/data/256x256/2016/08/18/809119_man_512x512.png'
        }

        return icon
      }
    },
    geolocation: function () {
      navigator.geolocation.getCurrentPosition((location) => {
        this.currentLocation.lng = location.coords.longitude
        this.currentLocation.lat = location.coords.latitude

        // All
        this.getEvents('all', '?latitude=' + this.currentLocation.lat + '&longitude=' + this.currentLocation.lng + '&distance=20')
      })
    },
    testClick: function () {
      this.$snotify.info('Example body content', {
        timeout: 4000,
        showProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true
      })
    },
    closeDialog: function () {
      this.infoDialog = false
    },
    showInfo: function (title, description) {
      this.description = description
      this.title = title
      this.infoDialog = true
      /* this.$snotify.info(description, title, {
        timeout: 4000,
        showProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true
      }) */
    },
    getEvents: function (category, parameters) {
      return this.axios.get('events/' + parameters).then(response => {
        if (category === 'all') {
          this.allCollection = response.data.EventSearchDocuments
        }
      })
      .catch(error => {
        console.log('Error objekt graph:', error)
      })
    }
  },
  watch: {
    infoDialog: function (val, oldVal) {
      console.log('ändrat', val)
    }
  },
  mounted: function () {
    this.geolocation()
  },
  created: function () {
    // this.geolocation()
  },
  data () {
    return {
      currentLocation: {
        lat: 0, lng: 0
      },
      infoDialog: false,
      title: '',
      description: '',
      searchAddressInput: '',
      testIcon: {
        url: 'https://d30y9cdsu7xlg0.cloudfront.net/png/29715-200.png',
        scaledSize: {
          width: 32, height: 32
        }
      },
      allCollection: []
    }
  },
  computed: {
    ...mapGetters([
      'traffic',
      'air',
      'crime',
      'safety'
    ])
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
