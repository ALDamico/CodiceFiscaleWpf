<template>
    <div>
        <loading :active.sync="isLoading" :can-cancel="false" is-full-page></loading>
        <b-alert :variant="this.error.errorSeverity" :show="errorOccurred" dismissible fade>{{error.errorMessage}}
        </b-alert>
        <b-form class="container align-content-center">
            <b-modal variant="danger" id="modalConfirmReset" title="Conferma" @ok="resetForm">
                Sei sicuro di voler reimpostare il modulo?<br>
                Tutti i cambiamenti andranno persi!
            </b-modal>
            <div class="card">
                <div class="card-header">
                    <div v-if="isValidation">Convalida</div>
                    <div v-else>Informazioni personali</div>
                </div>
                <div class="card-body">
                    <div v-if="isValidation" class="row p-3">
                        <div class="col-3">
                            <label for="fiscalCodeInput" class="col-form-label">Codice Fiscale</label>
                        </div>
                        <div class="col-9">
                            <b-input class="mx-2" v-model="currentFiscalCode.fiscalCode" id="fiscalCodeInput"></b-input>
                        </div>
                    </div>
                    <div class="row p-3">
                        <div class="col-3">
                            <label for="personNameInput" class="col-form-label">Nome</label>
                        </div>
                        <div class="col-9">
                            <b-input class="mx-2" type="text" v-model="currentPerson.name"
                                     id="personNameInput"
                                     placeholder="Nome"></b-input>
                        </div>
                    </div>
                    <div class="row p-3">
                        <div class="col-3">
                            <label for="personSurnameInput" class="col-form-label">Cognome</label>
                        </div>
                        <div class="col-9">
                            <b-input class="mx-2" type="text" v-model="currentPerson.Surname"
                                     placeholder="Cognome"
                                     id="personSurnameInput"></b-input>
                        </div>
                    </div>
                    <div class="row p-3">
                        <div class="col-3">
                            <label for="personBirthDateInput" class="col-form-label">Data di nascita</label>
                        </div>
                        <div class="col-9">
                            <date-picker class="mx-2"
                                         v-model="currentPerson.BirthDate"
                                         placeholder="Data di nascita"
                                         bootstrap-styling :language="it" typeable format="dd/MM/yyyy"></date-picker>
                          <!--  <b-form-datepicker class="mx-2"
                                               value-as-date
                                               v-model="currentPerson.BirthDate"
                                               placeholder="Data di nascita"
                                               id="personBirthDateInput"></b-form-datepicker>-->
                        </div>
                    </div>
                    <div class="row p-3">
                        <div class="col-3">
                            <label for="personGenderInput" class="col-form-label">Sesso</label>
                        </div>
                        <div class="col-9">
                            <b-form-select :options="allowedGenderValues"
                                           v-model="selectedGender"
                                           class="mx-2 custom-select"
                                           @change="changeGender" id="personGenderInput"></b-form-select>
                        </div>
                    </div>
                    <div class="row p-3">
                        <div class="col-3">
                            <label for="personBirthPlaceInput" class="col-form-label">Luogo di nascita</label>
                            <font-awesome-icon v-show="loading"
                                               icon="spinner"
                                               transform="rotate"
                                               class="fa-spin"/>
                        </div>
                        <div class="col-9">
                            <search-dropdown class="mx-2 fc-dropdown"
                                             block
                                             variant="light"
                                             v-model="currentPerson.BirthPlaceId"
                                             :options="placesList"
                                             optionLabel="name"
                                             :filter="true"
                                             :showClear="true"
                                             :valid-on="currentPerson.BirthDate"
                                             @change="currentPerson.BirthPlaceId = $event"
                                             text="Luogo di nascita"
                            >
                                <template v-slot:search-icon>
                                    <b-input-group-append is-text>
                                        <font-awesome-icon icon="search"></font-awesome-icon>
                                    </b-input-group-append>
                                </template>
                            </search-dropdown>
                        </div>

                    </div>
                    <div class="row p-3 float-right">
                        <div v-if="isValidation">
                            <b-btn variant="success" class="mx-5 button-rotate" @click="validateFiscalCode"
                                   type="button" :disabled="isCalculateButtonDisabled">
                                <font-awesome-icon icon="check-circle"
                                                   class="button-animated-icon-check"
                                                   :disabled="isCalculateButtonDisabled"></font-awesome-icon>
                                Convalida
                            </b-btn>
                        </div>
                        <div v-else>
                            <b-btn variant="success" class="mx-5 button-rotate" @click="calculateFiscalCode"
                                   type="button" :disabled="isCalculateButtonDisabled">
                                <font-awesome-icon icon="check-circle"
                                                   class="button-animated-icon-check"
                                                   :disabled="isCalculateButtonDisabled"></font-awesome-icon>
                                Calcola
                            </b-btn>
                        </div>
                        <b-btn variant="danger" class="mx-5" @click="confirmReset" type="button">
                            <font-awesome-icon class="button-animated-icon-undo"
                                               icon="undo"></font-awesome-icon>
                            Reimposta
                        </b-btn>
                    </div>
                </div>
            </div>
        </b-form>
    </div>
</template>

<script>
    import {Person} from "@/models/Person";
    import ax from 'axios';
    import {CodFiscaleError} from "@/models/CodFiscaleError.ts";
    import SearchDropdown from "@/components/SearchDropdown";
    import {Gender} from '@/models/Gender.ts'
    import moment from 'moment';
    import Loading from 'vue-loading-overlay';
    import DatePicker from 'vuejs-datepicker';
    import {it} from 'vuejs-datepicker/dist/locale';

    export default {
        name: "PersonForm",
        components: {
            SearchDropdown,
            Loading,
            DatePicker
        },
        computed: {
            isCalculateButtonDisabled() {
                if (!this.currentPerson.name ||
                    !this.currentPerson.Surname ||
                    !this.currentPerson.BirthDate ||
                    this.currentPerson.BirthPlaceId === 0 ||
                    this.currentPerson.Gender === Gender.Unspecified

                ) {
                    return true;
                }
                return false;
            }
        },
        data: function () {
            return {
                it: it,
                currentPerson: new Person(),
                allowedGenderValues: ['Maschile', 'Femminile'],
                selectedGender: null,
                retrievedPlaces: [],
                selectedPlace: null,
                errorOccurred: false,
                loading: false,
                error: new CodFiscaleError(),
                currentFiscalCode: {},
                placesList: [],
                isLoading: false
            }
        },
        props: {
            isValidation: {
                type: Boolean,
                default: false
            }
        },
        methods: {
            changeGender: function (selectedValue) {
                this.selectedGender = selectedValue;
                this.currentPerson.Gender = selectedValue;
            },
            confirmReset() {
                this.$bvModal.show("modalConfirmReset");
            },
            resetForm() {
                this.currentPerson = new Person();
                this.selectedGender = null;
            },
            validateFiscalCode() {
                const request = {
                    name: this.currentPerson.name,
                    surname: this.currentPerson.surname,
                    birthDate: this.currentPerson.BirthDate,
                    birthPlaceId: this.currentPerson.BirthPlaceId.id,
                    gender: this.currentPerson.Gender.valueOf(),
                    fiscalCode: this.currentFiscalCode.fiscalCode
                }
                ax.post("fiscalCode/validate", request, {
                    baseURL: "https://localhost:5001",
                    body: request
                }).then((res) => {
                    if (res.status === 200) {
                        this.$router.push({
                            name: 'validationResult',
                            params: {validationResult: res.data}
                        })
                    }
                });
            },
            calculateFiscalCode() {
                this.isLoading = true;
                const p = {
                    name: this.currentPerson.name,
                    surname: this.currentPerson.Surname,
                    birthDate: moment(this.currentPerson.BirthDate).format('YYYY-MM-DD'),
                    birthPlaceId: this.currentPerson.BirthPlaceId.id,
                    gender: this.currentPerson.Gender.valueOf()
                };
                const formData = new FormData();
                formData.set('request', JSON.stringify(p));

                ax.post("fiscalCode/calculate", formData, {
                    baseURL: "https://localhost:5001",
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded',
                        'Access-Control-Allow-Origin': '*'
                    }
                })
                    .then(response => {
                        if (response.data.result === "success") {
                            this.currentFiscalCode = response.data.fiscalCode;
                            this.saveFiscalCode(this.currentFiscalCode);
                            this.$router.push({
                                name: 'fiscalCode',
                                params: {fiscalCode: this.currentFiscalCode}
                            });
                        } else {
                            const error = new CodFiscaleError("Si sono verificati degli errori");
                            this.errorMessage = error.ErrorMessage;
                            this.errorOccurred = true;
                        }
                        this.isLoading = false;
                    })
                    .catch(err => {
                        this.$bvToast.toast("Si è verificato un errore nella comunicazione col server!", {
                            variant: "danger",
                            toaster: 'b-toaster-bottom-center',
                            autoHideDelay: 5000
                        })
                        this.isLoading = false;
                    });
            },
            saveFiscalCode(currentFiscalCode) {
                let localFiscalCodes = JSON.parse(localStorage.getItem('localFiscalCodes'));
                if (!localFiscalCodes) {
                    localFiscalCodes = [];
                }
                localFiscalCodes.push(currentFiscalCode);
                localStorage.setItem('localFiscalCodes', JSON.stringify(localFiscalCodes));
            }
        }
    }
</script>

<style scoped>
    .fc-dropdown {
        width: 100%;
    }


</style>
