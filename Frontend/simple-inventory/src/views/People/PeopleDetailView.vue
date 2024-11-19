<template>
    <div class="row justify-end">
        <div class=" q-gutter-md q-pa-xs ">
                <q-btn color="white" text-color="black" label="Edit" icon="edit" @click="editMode = !editMode"
                    v-show="!isNew" />
                <q-btn color="red" text-color="black" label="Delete" icon="delete" @click="deleteModal = !deleteModal"
                    v-show="!isNew" />
                <q-btn color="primary" text-color="white" label="Save" icon="save" @click="onSave" />
            </div>
    </div>
    <div class="q-pa-md q-col-gutter-md row justify-around">
        <div class="col">
            <q-input v-model="people.name" label="Name" :readonly="!editMode" />
            <q-input v-model="people.document" label="Document" :readonly="!editMode" />
        </div>
    </div>

    <Modal v-show="deleteModal" @close="deleteModal = !deleteModal" @confirm="onDelete">
        <h2>Delete</h2>
        <p>Do you want to delete {{ people.name }} ?</p>
    </Modal>
</template>
<script>
import Modal from '@/components/Modal.vue';
import { peopleUrl } from '@/global/Urls';

export default {
    components: {
        Modal
    },
    data() {
        return {
            people: {
                id: 0,
                name: '',
                document: ''
            },
            editMode: true,
            deleteModal: false,
            isNew: true,
        }
    },
    mounted() {
        if (this.$route.params.id) {
            fetch(peopleUrl + this.$route.params.id)
                .then(res => res.json())
                .then(data => this.people = data)
            this.isNew = false
            this.editMode = false
        }
    },
    methods: {
        onSave() {
            let _header = {
                'Content-Type': "application/json;charset=utf-8"
            }
            if (this.isNew) {
                fetch(peopleUrl,{
                    headers : _header,
                    method: "POST",
                    body: JSON.stringify(this.people)
                })
                .then(res => {
                    if(res.status == 200){
                this.$router.push({name: "PeopleListView"})
                        
                    }
                })

            }
            else {
                fetch(peopleUrl,{
                    headers : _header,
                    method: "PUT",
                    body: JSON.stringify(this.people)
                })
                .then(res => {
                    if(res.status == 200){
                this.$router.push({name: "PeopleListView"})
                        
                    }
                })
            }
        },
        onDelete() {
            let _header = {
                'Content-Type': "application/json;charset=utf-8"
            }
            fetch(peopleUrl + this.people.id,{
                    headers : _header,
                    method: "DELETE",
                })
                .then(res => {
                    if(res.status == 200){
                this.$router.push({name: "PeopleListView"})
                        
                    }
                })
        }
    }
}
</script>
<style></style>