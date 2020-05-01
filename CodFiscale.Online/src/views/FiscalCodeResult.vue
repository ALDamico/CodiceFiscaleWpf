<template>
    <div>
        <fiscal-code-component :fiscal-code="this.$route.params.fullFiscalCode"></fiscal-code-component>
    </div>
</template>

<script>
    import FiscalCodeComponent from "@/components/FiscalCodeComponent";

    export default {
        name: "FiscalCode",
        components: {FiscalCodeComponent},
        props: ['fiscalCode']
    }
</script>

<style scoped>
    fiscal-code-component {
        position:center;
        top: 50%;
        left: 50%;
    }
</style>
