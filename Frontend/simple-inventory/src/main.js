import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { Quasar } from 'quasar'
import quasarUserOptions from './quasar-user-options'
import  money from 'v-money3'

createApp(App).use(money).use(Quasar, quasarUserOptions).use(router).mount('#app')
