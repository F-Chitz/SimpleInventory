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
            <q-input v-model="purchase.id" label="Id" :readonly="true" />
        </div>
        <div class="col-6">
            <q-input v-model="purchase.people.name" label="Vendor" :readonly="!editMode" />
        </div>

        <div>
            <q-table title="Items" :rows="purchase.items" :columns="cols" row-key="name" binary-state-sort>            >
                <template v-slot:body="props">
                    <q-tr :props="props">
                        <q-td key="productName" :props="props">
                            {{ props.row.productName }}
                        </q-td>
                        <q-td key="quantity" :props="props">
                            {{ props.row.quantity }}
                            <q-popup-edit v-model="props.row.quantity" v-slot="scope">
                                <q-input v-model="scope.value" type="number" dense autofocus counter @keyup.enter="scope.set" />
                            </q-popup-edit>
                        </q-td>
                        <q-td key="unitPrice" :props="props">
                            {{ props.row.unitPrice}}
                            <q-popup-edit v-model="props.row.unitPrice" v-slot="scope">
                                <q-input v-model="scope.value" type="number" dense autofocus counter @keyup.enter="scope.set" />
                            </q-popup-edit>
                        </q-td>
                        <q-td key="subTotal" :props="props">
                            {{ props.row.unitPrice * props.row.quantity }}
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
</template>

<script>
import { productUrl } from '@/global/Urls';

export default {
    name: "PurchaseDetailView",
    data() {
        return {
            purchase: {
                id: 0,
                people: {
                    id: 0,
                    name: ''
                },
                items: [/*{
                    product: {
                        id: 0,
                        name: ''
                    },
                    productName: 'a',
                    quantity: 0,
                    unitPrice: 0
                }*/]
            },
            cols: [
                { name: 'productName', align: 'left', label: 'Product', field: 'productName', sortable: false },
                { name: 'quantity', align: 'left', label: 'Quantity', field: 'quantity', sortable: false },
                { name: 'unitPrice', align: 'left', label: 'Unit Price', field: 'unitPrice', format: (val) => `$ ${val}`, sortable: false },
                { name: 'subTotal', align: 'right', label: 'Sub Total', field: 'subTotal', format: (val) => `$ ${val}`, sortable: false },
            ],
            products: [],
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
    mounted() {
        fetch(productUrl + "getactives")
            .then(res => res.json())
            .then(data => data.forEach(element => {
                this.products.push(element)
            }))

    },
    methods: {
        addItem(item) {
            let tItem = {
                product: item,
                productName: item.name,
                quantity: 0,
                unitPrice: 0
            }
            this.purchase.items.push(tItem)
            console.log(JSON.stringify(this.purchase))

            this._item.product = null

        }
    }
}
</script>

<style></style>