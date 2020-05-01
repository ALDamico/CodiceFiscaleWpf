<template>
    <b-navbar toggleable="lg" type="dark" variant="primary">
        <b-navbar-brand to="/">CodFiscale.Online</b-navbar-brand>

        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

        <b-collapse id="nav-collapse" is-nav>


            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
                    <b-nav-item to="/">Calcola</b-nav-item>
                <b-nav-item to="/validate">Convalida</b-nav-item>
                    <b-nav-item to="/about">Chi siamo</b-nav-item>
                    <b-nav-item to="/myFiscalCodes">Archivio</b-nav-item>
            </b-navbar-nav>
        </b-collapse>
    </b-navbar>
</template>

<script>
    export default {
        name: "Navbar"
    }
</script>

<style scoped>
    .navbar {
        background-color: forestgreen !important;
    }

    .navbar-brand {
        color: white;
    }
    .navbar-brand:hover {
        color:white;
    }

    .fa-light {
        color: white;
    }

    .fa-light:hover {
        color: black;
    }

    .navbar:hover>.fa-light {
        color:black;
    }
</style>
