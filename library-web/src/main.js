import axios from 'axios'
import { createApp } from 'vue'
import VueAxios from 'vue-axios'
import PrimeVue from 'primevue/config';
import App from './App.vue'

import InputText from 'primevue/inputtext'
import RadioButton from 'primevue/radiobutton';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';

import "@/assets/scss/styles.scss"

const app = createApp(App);

app.use(VueAxios, axios);
app.use(PrimeVue)

app.component('InputText', InputText)
app.component('RadioButton', RadioButton)
app.component('DataTable', DataTable)
app.component('DataTable', DataTable)
app.component('Column', Column);

app.mount('#app')
