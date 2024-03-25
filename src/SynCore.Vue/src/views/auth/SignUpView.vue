<script setup lang="ts">
import {ref} from "vue";
import {useAuthUser} from "@/composables/authUser";
import {useRouter} from "vue-router";
import {AxiosError} from "axios";

const userInputs = ref({
  name: '',
  lastName: '',
  email: '',
  cpf: '',
  collegeName: '',
  password : '',
})
const errors = ref<{
  errorCode?: string,
  errorMessage: string,
}[]>([])

const authUser = useAuthUser()
const router = useRouter()

async function handleSignUp() {
  try {
    await authUser.SignUp({...userInputs.value})
    await router.push('/auth/sign-in')
  } catch (error) {
    if(error instanceof AxiosError) {
      if (error.response) {
        if(error.response.status === 403){
          errors.value = error.response.data.error
        }
        else if (error.response.status === 409){
          errors.value = [
            { errorMessage: error.response.data }
          ]
        }
        else {
          errors.value = [
            { errorMessage: 'erro não mapeado' }
          ]
        }
      }
    }
    console.error(error)
  }
}
</script>

<template>
  <h1>Cadastro</h1>
  <RouterLink to="/auth/sign-in">Login</RouterLink>
  <form style="margin-top: 16px" @submit.prevent="handleSignUp">
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
  <div v-if="errors">
    <p style="color: red" v-for="error in errors" :key="error.errorMessage">
      {{error.errorMessage}}
    </p>
  </div>
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
  gap: 4px;
  flex-direction: column;
}
button {
  grid-column: span 2;
}
</style>