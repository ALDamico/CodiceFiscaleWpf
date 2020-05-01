<template>
    <div class="p-5">
    <person-form is-validation></person-form></div>
</template>

<script>
    import PersonForm from "../components/PersonForm";
    export default {
        name: "Validate",
        components: {
            PersonForm
        }
    }
</script>

<style scoped>

</style>
