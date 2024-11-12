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

        <div class="col-6">
            <q-input v-model="product.id" label="Id" :readonly="true" />
            <q-input v-model="product.name" label="Name" :readonly="!editMode" />
        </div>
        <div class="col-6">

            <q-field v-model="product.cost" label="Cost">
                <template v-slot:control="{ id, floatingLabel, modelValue, emitValue }">
                    <money :id="id" class="q-field__input" :model-value="modelValue"
                        @update:model-value="emitValue" v-bind="moneyFormatForDirective"  />
                </template>
            </q-field>

            <q-select map-options emit-value v-model="product.costMethod" label="Cost Method" :options="costMethodOptions" />
        </div>
    </div>

    <Modal v-show="deleteModal" @close="deleteModal = !deleteModal" @confirm="onDelete">
        <h2>Delete</h2>
        <p>Do you want to delete {{ product.name }} ?</p>
    </Modal>
</template>

<script>
import Modal from '@/components/Modal.vue';
import { productUrl } from '@/global/Urls';
import { Money3Component } from 'v-money3'

export default {
    name: "ProductDetailView",
    components: {
        Modal,
        money: Money3Component
    },
    data() {
        return {
            editMode: true,
            deleteModal: false,
            isNew: true,
            product: {
                id: 0,
                name: "",
                costMethod: 0,
                cost: 0
            },
            moneyFormatForDirective: {
                decimal: '.',
                thousands: ',',
                prefix: '$ ',
                masked: false,
                precision: 2,
            },
            costMethodOptions: [
                {
                    label: "None",
                    value: 0
                },
                {
                    label: "AVCO",
                    value: 1
                },
                {
                    label: "FIFO",
                    value: 2
                }
            ]
        }
    },
    mounted() {
        if (this.$route.params.id) {
            fetch(productUrl + this.$route.params.id)
                .then(res => res.json())
                .then(data => this.product = data)

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
                fetch(productUrl, {
                    headers: _header,
                    method: "POST",
                    body: JSON.stringify(this.product)
                })
                    .then(res => {
                        if (res.status == 200)
                            this.$router.push({ name: "ProductListView" })
                    })
            }
            else {
                fetch(productUrl, {
                    headers: _header,
                    method: "PUT",
                    body: JSON.stringify(this.product)
                })
                    .then(res => {
                        if (res.status == 200)
                            this.$router.push({ name: "ProductListView" })
                    })
            }
        },

        onDelete() {
            let _header = {
                'Content-Type': "application/json;charset=utf-8"
            }
            fetch(productUrl + this.product.id, {
                headers: _header,
                method: "DELETE"
            })
                .then(res => {
                    if (res.status == 200)
                        this.$router.push({ name: "ProductListView" })
                })
        }
    }
}
</script>

<style></style>