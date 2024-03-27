<script setup lang="ts">
import {ref} from "vue";
import {useAuthUser} from "@/composables/authUser";
import {useRouter} from "vue-router";
import {AxiosError} from "axios";

const userCredentials = ref({
  email: '',
  password: ''
})
const errors = ref<{
  errorCode?: string,
  errorMessage: string,
}[]>([])

const authUser = useAuthUser()
const router = useRouter()

async function handleSubmit() {
  try {
    await authUser.SignIn({...userCredentials.value})
    await router.push('/')
  } catch (error) {
    if(error instanceof AxiosError) {
      if (error.response) {
        if(error.response.status === 403){
          errors.value = error.response.data.error
        }
        else if (error.response.status === 404){
          errors.value = [
            { errorMessage: error.response.data }
          ]
        }
      }
    }
    console.error(error)
  }
}
</script>

<template>
  <h1>Login</h1>
  <RouterLink to="/auth/sign-up">Cadastro</RouterLink>
  <form style="margin-top: 16px" @submit.prevent="handleSubmit" >
    <VTextField label="E-mail" variant="outlined" v-model="userCredentials.email" type="email" density="compact"/>
    <VTextField label="Senha" variant="outlined" v-model="userCredentials.password" type="password" density="compact"/>
    <router-link to="/auth/email-password-reset">Esqueceu a senha?</router-link>
    <v-btn type="submit" elevation="0" color="primary">
      Login
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
  gap: 8px;
  display: grid;
  grid-template-columns: 1fr 1fr;
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