import { createRouter, createWebHistory } from 'vue-router'
import PeopleListView from '@/views/People/PeopleListView.vue'
import PeopleDetailView from '@/views/People/PeopleDetailView.vue'
import ProductListVIew from '@/views/Product/ProductListVIew.vue'
import PurchaseListView from '@/views/Purchase/PurchaseListView.vue'
import SaleListView from '@/views/Sale/SaleListView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: ProductListVIew,
  },
  {
    path: '/Peoples',
    name: 'PeopleListView',
    component: PeopleListView
  },
  
  {
    path: '/Peoples/:id',
    name: 'PeopleDetailView',
    component: PeopleDetailView
  },
  {
    path: '/Peoples/new',
    name: 'PeopleNewView',
    component: PeopleDetailView
  },
  {
    path: '/Products',
    name: 'ProductListView',
    component: ProductListVIew
  },
  {
    path: '/Purchases',
    name: 'PurchaseListView',
    component: PurchaseListView
  },
  {
    path: '/Sales',
    name: 'SaleListView',
    component: SaleListView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
