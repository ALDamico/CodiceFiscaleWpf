<template>
    <div class="container">
        <h3>Risultati della convalida</h3>
        <div class="row">
            <div class="col-3">
                <strong>Atteso</strong>
            </div>
            <div class="col-9 text-left"><p>{{validationResult.expectedFiscalCode}}</p></div>
        </div>
        <div class="row">
            <div class="col-3"><strong>Fornito</strong></div>
            <div class="col-9 text-left"><p>{{validationResult.providedFiscalCode}}</p></div>
        </div>
        <b-table hover :fields="fields" :items="validationResult.validationMessages" bordered>
            <template v-slot:cell(isValid)="data">
                <span v-if="data.item.isValid" class="green-pill">

                </span>
                <span v-else class="red-pill">

                </span>
            </template>
        </b-table>
    </div>
</template>

<script>
    export default {
        name: "ValidationResult",
        props: {
            validationResult: {
                type: Object,
                default: () => ({
                    expectedFiscalCode: "",
                    providedFiscalCode: "",
                    person: {

                    },
                    validationMessages: []
                })
            }
        },
        data() {
            return {
                fields: [
                    {
                        key: 'isValid',
                        label: 'Valido'
                    },
                    {
                        key: 'message',
                        label: 'Messaggio'
                    }
                ],
                tableColumns: [{
                    message: {
                        key: 'message',
                        sortable: false,
                        headerTitle: "Messaggio",
                        label: "Messaggio"
                    }
                }, {
                    valid: {
                        key: 'isValid',
                        label: 'Valido'
                    }
                }]
            }
        }
    }
</script>

<style scoped>
    .red-pill {
        height: 16px;
        width: 16px;
        border-radius: 50%;
        display: inline-block;
        background: rgb(255,0,0);
        background: linear-gradient(144deg, rgba(255,0,0,1) 0%, rgba(255,255,255,1) 93%);
    }

    .green-pill {
        height: 16px;
        width: 16px;
        border-radius: 50%;
        display: inline-block;
        background: rgb(0,191,47);
        background: linear-gradient(144deg, rgba(0,191,47,1) 0%, rgba(255,255,255,1) 93%);
    }
</style>
