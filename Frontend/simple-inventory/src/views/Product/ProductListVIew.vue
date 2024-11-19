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
                { name: 'costMethod', align: 'left', label: 'Cost Method', field: 'costMethod', sortable: false}
            ],
            rows: []
        }
    },
    mounted() {
        fetch(productUrl + 'getactives')
            .then(res => res.json())
            .then(data => {
                data.forEach(element => {
                    switch(element.costMethod){
                        case 0:
                            element.costMethod = 'None'
                            break;
                        case 1:
                            element.costMethod = "AVCO"
                            break;
                        case 2:
                            element.costMethod = "FIFO"
                            break;
                    }

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