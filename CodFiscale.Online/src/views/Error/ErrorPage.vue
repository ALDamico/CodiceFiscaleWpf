<template>
    <div class="container">
        <h1>Oh no!</h1>
        <p>A quanto pare si è verificato un errore...</p>
        <div v-if="errorMessage">
            <h3>Dettagli</h3>
            <p>{{errorMessage}}</p>
        </div>
        <p>Torna alla <router-link to="/">pagina iniziale</router-link></p>
    </div>
</template>

<script>
    export default {
        name: "ErrorPage",
        props: {
            errorMessage: {
                type: String,
                default: ""
            }
        }
    }
</script>

<style scoped>

</style>
