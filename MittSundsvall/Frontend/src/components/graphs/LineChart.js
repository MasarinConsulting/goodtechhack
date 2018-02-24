import { Line } from 'vue-chartjs'

export default {
  extends: Line,
  props: ['data', 'options'],
  watch: {
    'data': function (value) {
      this.renderChart(value, this.options)
    }
  },
  data () {
    return {
      gradient: null,
      gradient2: null
    }
  },
  mounted () {
    // this.renderChart(this.data, this.options)
  }
}
