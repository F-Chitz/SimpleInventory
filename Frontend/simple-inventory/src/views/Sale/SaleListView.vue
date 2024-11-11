<template>
    <div class="q-pa-md">
    <q-btn color="primary" text-color="white" label="New" @click="createSale" class="q-mb-xs"/>
        
    <q-table
      title="Sales"
      :rows="rows"
      :columns="cols"
      row-key="name"
      @row-click="onRowClick"
    />
  </div>
</template>

<script>
import { saleUrl } from '@/global/Urls';
export default {
    name: 'SaleListView',
    components: {
    },
    data() {
        return {
            cols: [
                { name: 'peopleName', align: 'left', label: 'Customer', field: 'peopleName', sortable: true },
                { name: 'total', align: 'left', label: 'Total', field: 'total', sortable: false },
            ],
            rows: []
        }
    },
    mounted() {
        fetch(saleUrl + 'getactives')
            .then(res => res.json())
            .then(data => {
                data.forEach(element => {
                    let row = { id: element.id,
                                peopleName: element.people.name,
                                total: 0 
                    }
                    element.items.forEach(item =>{
                        row.total += item.quantity * item.unitPrice
                    })
                    this.rows.push(row)
                console.log(this.rows)
                console.log(data)

                });

            })
            .catch(e => { })
    },
    methods: {
        onRowClick (evt, row) {
            this.$router.push({name:'SaleDetailView', params:{id: row.id}})
        },
        createPurchase(){
            this.$router.push({name:'SaleNewView'})
        }
    }

}
</script>

<style></style>