<template>
    <div class="q-pa-md">
    <q-btn color="primary" text-color="white" label="New" @click="createProduct" class="q-mb-xs"/>
        
    <q-table
      title="Products"
      :rows="rows"
      :columns="cols"
      row-key="name"
      @row-click="onRowClick"
    />
  </div>
</template>

<script>
import { productUrl } from '@/global/Urls';
export default {
    name: 'ProductListView',
    components: {
    },
    data() {
        return {
            cols: [
                { name: 'names', align: 'left', label: 'Name', field: 'name', sortable: true },
                { name: 'cost', align: 'left', label: 'Cost', field: 'cost', sortable: false, format: (val) =>`$ ${val}` },
            ],
            rows: []
        }
    },
    mounted() {
        fetch(productUrl + 'getactives')
            .then(res => res.json())
            .then(data => {
                data.forEach(element => {
                        this.rows.push(element)
                });
            })
            .catch(e => { })
    },
    methods: {
        onRowClick (evt, row) {
            this.$router.push({name:'ProductDetailView', params:{id: row.id}})
        },
        createProduct(){
            this.$router.push({name:'ProductNewView'})
        }
    }

}
</script>

<style></style>