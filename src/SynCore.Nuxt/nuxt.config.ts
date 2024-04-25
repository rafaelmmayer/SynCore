import {resolve} from "pathe";

export default defineNuxtConfig({
  devtools: { enabled: true },
  ssr: false,
  srcDir: 'src',
  vite: {
    server: {
      proxy: {
        '/api': 'http://localhost:5003'
      }
    }
  },
  modules: ['nuxt-primevue'],
  primevue: {
    cssLayerOrder: 'reset,primevue'
  },
  css: [
      './assets/main.css',
      'primevue/resources/themes/aura-light-green/theme.css',
      'primeflex/primeflex.css',
      'primeflex/themes/primeone-light.css',
      'primeicons/primeicons.css'
  ]
})