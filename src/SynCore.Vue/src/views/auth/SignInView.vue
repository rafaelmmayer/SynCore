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
    <label>
      Email
      <input v-model="userCredentials.email">
    </label>
    <label>
      <div style="display: flex">
        <span style="flex-grow: 1">Senha</span>
        <RouterLink to="/auth/email-password-reset">Esqueceu a senha?</RouterLink>
      </div>
      <input v-model="userCredentials.password">
    </label>
    <button type="submit">
      Login
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