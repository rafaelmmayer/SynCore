<script setup lang="ts">
import {ref} from "vue";
import {useAuthUser} from "@/composables/authUser";
import {useRouter} from "vue-router";

const userInputs = ref({
  name: '',
  lastName: '',
  email: '',
  cpf: '',
  collegeName: '',
  password : '',
})

const authUser = useAuthUser()
const router = useRouter()

async function handleSignUp() {
  try {
    await authUser.SignUp({...userInputs.value})
    await router.push('/auth/sign-in')
  } catch (error) {
    console.error(error)
  }
}
</script>

<template>
  <h1>Cadastro</h1>
  <form @submit.prevent="handleSignUp">
    <label>
      Nome
      <input v-model="userInputs.name"/>
    </label>
    <label>
      Sobrenome
      <input v-model="userInputs.lastName"/>
    </label>
    <label>
      Email
      <input v-model="userInputs.email"/>
    </label>
    <label>
      CPF
      <input v-model="userInputs.cpf"/>
    </label>
    <label>
      Faculdade
      <input v-model="userInputs.collegeName"/>
    </label>
    <label>
      Senha
      <input v-model="userInputs.password"/>
    </label>
    <button type="submit">
      Cadastrar
    </button>
  </form>
  <RouterLink to="/auth/sign-in">Login</RouterLink>
</template>

<style scoped>
form {
  width: 500px;
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 8px;
}
label {
  display: flex;
  flex-direction: column;
}
button {
  grid-column: span 2;
}
</style>