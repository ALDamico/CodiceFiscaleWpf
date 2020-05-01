<template>
    <div class="m-0">
        <navbar class="col-12"></navbar>
        <div id="app">


            <router-view/>
        </div>
    </div>
</template>

<style>
  /*  #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
    }*/

    /* Revert icons to their original state */
    button > .button-animated-icon-check, .button-animated-icon-undo {
        transition: 1s linear;
        transform: scale(1);
        transform: rotate(0deg);
    }

    button:hover > .button-animated-icon-check {
        transition: 1s linear;
        transform: scale(1.10);
    }

    button:hover > .button-animated-icon-undo {
        transition: 1s linear;
        transform: rotate(-90deg);
    }
</style>
