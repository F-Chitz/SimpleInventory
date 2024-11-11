<template>
    <div class="q-pa-md">
    <q-btn color="primary" text-color="white" label="New" @click="createPeople" class="q-mb-xs"/>
        
    <q-table
      title="Peoples"
      :rows="rows"
      :columns="cols"
      row-key="name"
      @row-click="onRowClick"
    />
  </div>
</template>

<script>
import { peopleUrl } from '@/global/Urls';
export default {
    name: 'PeopleListView',
    components: {
    },
    data() {
        return {
            cols: [
                { name: 'names', align: 'left', label: 'Name', field: 'name', sortable: true },
                { name: 'documents', align: 'left', label: 'Document', field: 'document', sortable: true }
            ],
            rows: []
        }
    },
    mounted() {
        fetch(peopleUrl + 'getactives')
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
            this.$router.push({name:'PeopleDetailView', params:{id: row.id}})
        },
        createPeople(){
            this.$router.push({name:'PeopleNewView'})
        }
    }

}
</script>

<style></style>