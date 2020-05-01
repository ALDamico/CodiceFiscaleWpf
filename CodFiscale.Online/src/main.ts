import Vue from 'vue';
import {BootstrapVue, BootstrapVueIcons} from "bootstrap-vue";

import App from './App.vue'
import router from './router'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import {FontAwesomeIcon} from '@fortawesome/vue-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core'
import {
  faCheckCircle,
  faUndo,
  faSpinner,
  faCreditCard,
  faCopy,
  faSearch,
  faExclamation,
    faUser
} from '@fortawesome/free-solid-svg-icons';

import Navbar from "@/components/Navbar.vue";
import 'vue-loading-overlay/dist/vue-loading.css';
library.add(faCheckCircle, faUndo, faSpinner, faCreditCard, faCopy, faSearch, faExclamation, faUser);

Vue.use(BootstrapVue);
Vue.use(BootstrapVueIcons);
Vue.component('navbar', Navbar);
Vue.component('font-awesome-icon', FontAwesomeIcon);

Vue.config.productionTip = false;

new Vue({
  router,
  render: h => h(App)
}).$mount('#app');
