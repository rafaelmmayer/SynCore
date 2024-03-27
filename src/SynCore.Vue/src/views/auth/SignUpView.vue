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
  confirmPassword : '',
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
    <VTextField label="Nome" variant="outlined" v-model="userInputs.name" density="compact"/>
    <VTextField label="Sobrenome" variant="outlined" v-model="userInputs.lastName" density="compact"/>
    <VTextField label="E-mail" variant="outlined" v-model="userInputs.email" type="email" density="compact"/>
    <VTextField label="CPF" variant="outlined" v-model="userInputs.cpf" density="compact"/>
    <div style="grid-column: span 2">
      <VTextField label="Faculdade" variant="outlined" v-model="userInputs.collegeName" density="compact"/>
    </div>
    <VTextField label="Senha" variant="outlined" v-model="userInputs.password" type="password" density="compact"/>
    <VTextField label="Confirme a senha" variant="outlined" v-model="userInputs.confirmPassword" type="password" density="compact"/>
    <v-btn type="submit" elevation="0" color="primary">
      Cadastrar
    </v-btn>
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