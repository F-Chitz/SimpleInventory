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
            <q-input v-model="sale.id" label="Id" :readonly="true" />
        </div>
        <div class="col-6">
            <q-select v-model="sale.people" map-options label="Vendor" :options="peoplesOptions" option-label="name"
                option-value="id" :readonly="!editMode">

                <template v-slot:no-option>
                    <q-item>
                        <q-item-section class="text-grey">
                            No results
                        </q-item-section>
                    </q-item>
                </template>
            </q-select>
        </div>

        <div class="col-12">
            <q-table title="Items" :rows="sale.items" :columns="cols" row-key="name" binary-state-sort>
                <template v-slot:body="props">
                    <q-tr :props="props" v-if="props.row.isActive">
                        <q-td key="productName" :props="props">
                            {{ props.row.productName }}
                        </q-td>
                        <q-td key="quantity" :props="props">
                            {{ props.row.quantity }}
                            <q-popup-edit v-model="props.row.quantity" v-slot="scope">
                                <q-input v-model="scope.value" type="number" dense autofocus counter
                                    @keyup.enter="scope.set" />
                            </q-popup-edit>
                        </q-td>
                        <q-td key="unitPrice" :props="props">
                            $ {{ props.row.unitPrice }}
                            <q-popup-edit v-model="props.row.unitPrice" v-slot="scope">
                                <q-input v-model="scope.value" type="number" dense autofocus counter
                                    @keyup.enter="scope.set" />
                            </q-popup-edit>
                        </q-td>
                        <q-td key="subTotal" :props="props">
                            $ {{ (props.row.unitPrice * props.row.quantity).toFixed(2) }}
                        </q-td>
                        <q-td key="actions" :props="props">
                            <q-btn flat icon="delete" @click="deleteItem(props.row)" />
                        </q-td>
                    </q-tr>

                </template>
                <template v-slot:bottom-row> <q-tr>
                        <q-td> <q-select v-model="_item.product" use-input hide-selected fill-input map-options
                                input-debounce="0" :options="products" option-label="name" option-value="id"
                                @update:model-value="addItem" hint="Add a product">
                                <template v-slot:no-option>
                                    <q-item>
                                        <q-item-section class="text-grey">
                                            No results
                                        </q-item-section>
                                    </q-item>
                                </template>
                            </q-select>
                        </q-td>
                    </q-tr></template>
            </q-table>
        </div>
    </div>
    <Modal v-show="deleteModal" @close="deleteModal = !deleteModal" @confirm="onDelete">
        <h2>Delete</h2>
        <p>Do you want to delete?</p>
    </Modal>
</template>

<script>
import Modal from '@/components/Modal.vue';
import { productUrl, saleUrl, peopleUrl } from '@/global/Urls';

export default {
    name: "SaleDetailView",
    components: {
        Modal
    },
    data(){
        return{
            editMode: true,
            deleteModal: false,
            isNew: true,
            sale:{
                id:0,
                people:{
                    id:0,
                    name: ''
                },
                items:[]
            },
            cols:[
                { name: 'productName', align: 'left', label: 'Product', field: 'productName', sortable: false },
                { name: 'quantity', align: 'left', label: 'Quantity', field: 'quantity', sortable: false },
                { name: 'unitPrice', align: 'left', label: 'Unit Price', field: 'unitPrice', sortable: false },
                { name: 'subTotal', align: 'right', label: 'Sub Total', field: 'subTotal', sortable: false },
                { name: 'actions', align: 'center', label: '', field: 'actions', sortable: false },
            ],
            products: [],
            peoplesOptions:[],
            _item: {
                product: [
                    {
                        product: {
                            id: 0,
                            name: ''
                        },
                        quantity: 0,
                        unitPrice: 0
                    }

                ]
            }
        }
    },
    mounted(){
        fetch(productUrl + "getactives")
            .then(res => res.json())
            .then(data => data.forEach(element => {
                this.products.push(element)
            }))
        fetch(peopleUrl + "getactives")
            .then(res => res.json())
            .then(data => data.forEach(element => {
                this.peoplesOptions.push(element)
            }))

        if(this.$route.params.id){
            fetch(saleUrl + this.$route.params.id)
                .then(res => res.json())
                .then(data => {
                    data.items.forEach(e => e.productName = e.product.name)
                    this.sale = data
                })
            this.isNew = false
            this.editMode = false
        }
    },
    methods:{
        addItem(item){
            let tItem ={
                isActive: true,
                product: item,
                productName: item.name,
                quantity: 0,
                unitPrice: 0
            }
            if(!this.sale.items.find((value, index, array) => {return (value.productName == tItem.productName && value.isActive)})){
                this.sale.items.push(tItem)
            }
            this._item.product = null
        },

        deleteItem(row){
            this.sale.items.filter(
                (value, index, array) => {
                    if(value.productName == row.productName)
                        value.isActive = false
                }
            )
        },
        onSave(){
            let _header = {
                'Content-Type': "application/json;charset=utf-8"
            }
            if (this.isNew){
                fetch( saleUrl, {
                    headers: _header,
                    method: "POST",
                    body: JSON.stringify(this.sale)
                })
                .then(res =>{
                    if (res.status == 200)
                        this.$router.push({name: "SaleListView"})
                })
            }
            else{
                fetch( saleUrl, {
                    headers: _header,
                    method: "PUT",
                    body: JSON.stringify(this.sale)
                })
                .then(res =>{
                    if (res.status == 200)
                        this.$router.push({name: "SaleListView"})
                })
            }
        },

        onDelete(){
            let _header = {
                'Content-Type': "application/json;charset=utf-8"
            }
            fetch(saleUrl + this.sale.id,{
                headers: _header,
                method: "DELETE"
            })
                .then(res => {
                    if (res.status == 200)
                        this.$router.push({name: "SaleListView"})
                })
        }
    }

}
</script>

<style></style>