<script setup lang="ts">

import {useApiClient} from "@/composables/http";
import {ref} from "vue";

const { data, error, isSuccess, isError, execute } = useApiClient<void>()

const email = ref('')

function handleSubmit() {
  execute({
    url: '/api/auth/email-password-reset?email=' + email.value,
    method: "POST",
  })
}
</script>

<template>
<form @submit.prevent="handleSubmit">
  <label for="resetEmail">
    Digite o endereço de e-mail da sua conta da que iremos enviar um link de redefinição de senha.
  </label>
  <VTextField variant="outlined" id="resetEmail" name="resetEmail" type="email" density="compact" v-model="email"/>
  <v-btn type="submit" elevation="0" color="primary">
    Enviar
  </v-btn>
</form>
  <div>{{isError}}</div>
  <div>{{isSuccess}}</div>
  <div>{{data}}</div>
  <div>{{error}}</div>
</template>

<style scoped>
form {
  width: 500px;
  gap: 8px;
  display: grid;
}
label {
  display: flex;
  gap: 4px;
  flex-direction: column;
}
</style>