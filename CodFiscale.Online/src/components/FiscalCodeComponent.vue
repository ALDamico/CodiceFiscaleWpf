<template>
    <div class="container">
        <div class="card">
            <div class="card-header">Codice Fiscale</div>
            <div class="card-body">
                <b-input-group>
                    <b-input v-model="currentFiscalCode.fiscalCode" disabled/>
                    <b-input-group-append>
                        <b-button variant="success" @click="copyToClipboard">
                            <font-awesome-icon icon="copy" id="btnCopy"></font-awesome-icon>
                        </b-button>
                    </b-input-group-append>
                </b-input-group>


                <b-tooltip target="btnCopy">Clicca per copiare negli appunti</b-tooltip>
                <b-toast>Codice fiscale copiato negli appunti!</b-toast>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "FiscalCodeComponent",
        props: ['fiscalCode'],
        data: function () {
            return {
                currentFiscalCode: ""
            }
        },
        methods: {
            copyToClipboard() {
                navigator.clipboard.writeText(this.currentFiscalCode).then(() => this.$bvToast.toast("Codice fiscale copiato negli appunti!",  {
                    variant: "success",
                    toaster: 'b-toaster-bottom-center',
                    autoHideDelay: 5000
                }));
            }
        },
        mounted() {
            if (typeof this.$route.params.fiscalCode !== 'undefined') {
                this.currentFiscalCode = this.$route.params.fiscalCode;
            }
            else {
                this.$router.replace({
                    name: 'Errore',
                    params: {
                        errorMessage: "L'oggetto Codice Fiscale non era definito."
                    }
                });
            }

        }
    }
</script>

<style scoped>

</style>
