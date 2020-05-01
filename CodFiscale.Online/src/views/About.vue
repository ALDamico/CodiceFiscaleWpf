<template>
    <div class="about-box">
        <h1>Informazioni su CodFiscale.Online</h1>
        <p>CodFiscale.Online è il sistema per il calcolo del codice fiscale sviluppato da <a
                href="https://www.lucianodamico.info">Andrea Luciano Damico</a>, uno sviluppatore software che vive e
            lavora in provincia di Catania.
        </p>
        <h2>
            Quali tecnologie vengono usate in CodFiscale.Online?
        </h2>
        <p>Il <strong>frontend</strong> di CodFiscale.Online è scritto in Vue.js, con un misto di JavaScript puro e di
            TypeScript, soprattutto per le classi model.</p>
        <p>Il <strong>backend</strong> di CodFiscale.Online è scritto in C#. La persistenza dei dati è gestita da un
            database SQL Server e l'ORM utilizzato per gestirla è Entity Framework Core.</p>
        <p>L'<strong>applicazione desktop</strong> utilizza lo stesso codice del backend della versione web e ne
            riutilizza gran parte del codice. Nella versione desktop la persistenza dei dati è affidata a un database
            embedded SQLite, gestito tramite Entity Framework Core.</p>
        <p>Tutto il codice sorgente di CodFiscale.Online e della corrispettiva applicazione desktop è disponibile sotto
            licenza MIT su GitHub. Se ti interessi di programmazione o vuoi approcciarti questo mondo per la prima volta
            sei libero di clonare i repository e studiarne il codice. E se noti qualcosa che si può migliorare, i contributi
            al codice sono i benvenuti.</p>
    </div>

</template>
<style scoped>
    .about-box {
        margin-left: 10%;
        margin-right: 10%;

    }
    .about-box > p {
        text-align: justify;
    }
</style>
