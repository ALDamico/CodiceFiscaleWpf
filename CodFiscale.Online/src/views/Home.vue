<template>
  <div class="home p-5">
    <PersonForm></PersonForm>
  </div>
</template>

<script>
// @ is an alias to /src
import PersonForm from '@/components/PersonForm.vue'

export default {
  name: 'Home',
  components: {
    PersonForm
  }
}
</script>
