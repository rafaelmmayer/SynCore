<script setup lang="ts">

</script>

<template>
<form>
  <label>
    Digite o endereço de e-mail da sua conta da que iremos enviar um link de redefinição de senha.
    <input type="email" class="input" name="email">
  </label>
  <button type="submit">
    Enviar
  </button>
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