<template>
    <div class="backdrop" @click.self="closeModal">
        <div class="modal">
            <slot></slot>
            <div class="action">
                <q-btn color="primary" label="Yes" @click="confirmModal"/>
                <q-btn color="white" text-color="black" label="No" @click="closeModal"/>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    methods: {
        closeModal() {
            this.$emit('close')
        },
        confirmModal(){
            this.$emit('confirm')
        }
    }
}
</script>

<style>
.modal {
    width: 400px;
    padding: 20px;
    margin: 100px auto;
    background: white;
    border-radius: 10px;
}

.backdrop {
    top: 0;
    position: fixed;
    background: rgba(0, 0, 0, 0.5);
    width: 100%;
    height: 100%;
}
</style>