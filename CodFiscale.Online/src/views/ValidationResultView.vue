<template>
    <validation-result :validation-result="validationResult"></validation-result>
</template>

<script>
    import ValidationResult from "@/components/ValidationResult";
    export default {
        name: "ValidationResultView",
        components: {ValidationResult},
        props: {
            validationResult: {
                type: Object,
                default: () => ({})
            }
        }
    }
</script>

<style scoped>

</style>
