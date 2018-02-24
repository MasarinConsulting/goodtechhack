'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  BACKEND_URL: '"__fillme__"',
  GOOGLE_MAPS_API_KEY: '"__fillme__"'
})
