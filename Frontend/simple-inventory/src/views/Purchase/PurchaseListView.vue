<template>
    <div class="q-pa-md">
    <q-btn color="primary" text-color="white" label="New" @click="createPurchase" class="q-mb-xs"/>
        
    <q-table
      title="Purchases"
      :rows="rows"
      :columns="cols"
      row-key="name"
      @row-click="onRowClick"
    />
  </div>
</template>

<script>
import { purchaseUrl } from '@/global/Urls';
export default {
    name: 'PurchaseListView',
    components: {
    },
    data() {
        return {
            cols: [
                { name: 'peopleName', align: 'left', label: 'Vendor', field: 'peopleName', sortable: true },
                { name: 'total', align: 'left', label: 'Total', field: 'total', format: (val) => `$ ${val}`, sortable: false },
            ],
            rows: []
        }
    },
    mounted() {
        fetch(purchaseUrl + 'getactives')
            .then(res => res.json())
            .then(data => {
                data.forEach(element => {
                    let row = { id: element.id,
                                peopleName: element.people.name,
                                total: 0 
                    }
                    element.items.forEach(item =>{
                        if(item.isActive)
                            row.total += item.quantity * item.unitPrice
                    })
                    this.rows.push(row)

                });

            })
            .catch(e => { })
    },
    methods: {
        onRowClick (evt, row) {
            this.$router.push({name:'PurchaseDetailView', params:{id: row.id}})
        },
        createPurchase(){
            this.$router.push({name:'PurchaseNewView'})
        }
    }

}
</script>

<style></style>