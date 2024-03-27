<script setup lang="ts">

</script>

<template>
<form @submit.prevent>
  <label for="resetEmail">
    Digite o endereço de e-mail da sua conta da que iremos enviar um link de redefinição de senha.
  </label>
  <VTextField variant="outlined" id="resetEmail" name="resetEmail" type="email" density="compact"/>
  <v-btn type="submit" elevation="0" color="primary">
    Enviar
  </v-btn>
</form>
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