<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated class="glossy">
    <q-toolbar>

      <q-toolbar-title @click="goToHome">
        Simple Inventory
      </q-toolbar-title>
      <q-btn flat color="" label="Peoples" @click="goToPeople"/>
      <q-btn flat color="" label="Products" @click="goToProduct"/>
      <q-btn flat color="" label="Purchases" @click="goToPurchase"/>
      <q-btn flat color="" label="Sales" @click="goToSale"/>

    </q-toolbar>
    </q-header>


    <q-page-container>
      <RouterView></RouterView>
    </q-page-container>
  </q-layout>
</template>

<script>

export default {
  name: 'LayoutDefault',
  data(){
     return{
    }
  },
  components: {
  },
  methods:{
    goToHome(){
      this.$router.push({name: 'home'});
    },
    goToPeople(){
      this.$router.push({name: 'PeopleListView'});
    },
    goToProduct(){
      this.$router.push({name: 'ProductListView'});
    },
    goToPurchase(){
      this.$router.push({name: 'PurchaseListView'});
    },
    goToSale(){
      this.$router.push({name: 'SaleListView'});
    },
    
  }
}
</script>
