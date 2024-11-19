import { createRouter, createWebHistory } from 'vue-router'
import PeopleListView from '@/views/People/PeopleListView.vue'
import PeopleDetailView from '@/views/People/PeopleDetailView.vue'
import ProductListVIew from '@/views/Product/ProductListVIew.vue'
import PurchaseListView from '@/views/Purchase/PurchaseListView.vue'
import SaleListView from '@/views/Sale/SaleListView.vue'
import ProductDetailView from '@/views/Product/ProductDetailView.vue'
import PurchaseDetailView from '@/views/Purchase/PurchaseDetailView.vue'
import SaleDetailView from '@/views/Sale/SaleDetailView.vue'

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
    path: '/Products/:id',
    name: 'ProductDetailView',
    component: ProductDetailView
  },
  {
    path: '/Products/new',
    name: 'ProductNewView',
    component: ProductDetailView
  },
  {
    path: '/Purchases',
    name: 'PurchaseListView',
    component: PurchaseListView
  },
  {
    path: '/Purchases/:id',
    name: 'PurchaseDetailView',
    component: PurchaseDetailView
  },
  {
    path: '/Purchases/new',
    name: 'PurchaseNewView',
    component: PurchaseDetailView
  },
  {
    path: '/Sales',
    name: 'SaleListView',
    component: SaleListView
  },
  {
    path: '/Sales/:id',
    name: 'SaleDetailView',
    component: SaleDetailView
  },
  {
    path: '/Sales/new',
    name: 'SaleNewView',
    component: SaleDetailView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
