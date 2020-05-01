<template>
    <div>
        <b-modal id="confirmDelete" @ok="clearList">
            <template v-slot:modal-title>Conferma cancellazione
            </template>
            Sei sicuro di voler eliminare tutti gli elementi presenti in questo elenco?
        </b-modal>
        <div class="container-fluid">
            <div v-if="fiscalCodeList.length > 0">
                <div class="scroll-horizontally">
                    <b-table :items="fiscalCodeList"
                             :fields="tableFields"
                             per-page="10"
                             :current-page="currentPage"
                    >
                    </b-table>
                    <b-pagination v-model="currentPage" :total-rows="fiscalCodeList.length"></b-pagination>
                </div>
                <b-button-group>
                    <b-button variant="primary" @click="exportAsCsv">Salva i dati in CSV</b-button>
                    <b-button variant="danger" @click="confirmClear">Cancella tutti i dati locali</b-button>
                </b-button-group>
            </div>
            <div v-else class="center-on-screen">
                <h1>
                    <font-awesome-icon icon="user"></font-awesome-icon>
                </h1>
                <p>Non hai ancora calcolato nessun codice fiscale.</p>
                <p>Vai alla pagina
                    <router-link to="/">Calcola</router-link>
                    .
                </p>
            </div>
        </div>
    </div>
</template>

<script>
    import Papa from 'papaparse';
    export default {
        name: "FiscalCodeListComponent",
        data: function () {
            return {
                tableFields: [
                    {
                        key: "name",
                        label: "Nome",
                        sortable: true
                    },
                    {
                        key: "surname",
                        label: "Cognome",
                        sortable: true
                    },
                    {
                        key: "fiscalCode",
                        label: "Codice fiscale"
                    },
                    {
                        key: "dateOfBirth",
                        label: "Data di nascita",
                        formatter: (value) => {
                            return new Date(value).toLocaleDateString('it-IT', {
                                year: 'numeric',
                                month: 'long',
                                day: 'numeric'
                            })
                        }
                    },
                    {
                        key: "placeOfBirth",
                        label: "Luogo di nascita"
                    }
                ],
                currentPage: 1,
                fiscalCodeList: []
            }
        },
        mounted() {
            const localFiscalCodeCache = JSON.parse(localStorage.getItem('localFiscalCodes'));
            if (localFiscalCodeCache != null) {
                this.fiscalCodeList = localFiscalCodeCache.filter(x => x != null);
            }
        },
        methods: {
            confirmClear() {
                this.$bvModal.show("confirmDelete");
            },
            clearList() {
                this.fiscalCodeList = null;
                localStorage.setItem('localFiscalCodes', null);
                this.$forceUpdate();
            },
            exportAsCsv() {
                if (!this.fiscalCodeList || this.fiscalCodeList.length === 0) {
                    return;
                }
                const config = {
                    quoteChar: '"',
                    escapeChar: '"',
                    delimiter: ';',
                    header: true,
                    newline: "\r\n"
                };
                const output = Papa.unparse(this.fiscalCodeList, config);
                const data = new Blob([output], {type: 'text/csv;charset=utf-8'});
                const url = window.URL.createObjectURL(data);
                const tempLink = document.createElement('a');
                tempLink.href = url;
                tempLink.setAttribute('download', 'estrazione.csv');
                tempLink.click();
                //window.location = output;
            }
        }
    }
</script>

<style scoped>
    b-table {
        position: absolute;
        top: 50%;
        left: 50%;
    }

    .scroll-horizontally {
        overflow-x: auto;
    }

    .center-on-screen {
        position: absolute;
        top:50%;
        left: 50%;
        transform: translate(-50%, -50%);
    }
</style>
