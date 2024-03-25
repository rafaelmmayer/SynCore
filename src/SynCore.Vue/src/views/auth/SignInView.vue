<script setup lang="ts">
import {ref} from "vue";
import {useAuthUser} from "@/composables/authUser";
import {useRouter} from "vue-router";

const userCredentials = ref({
  email: '',
  password: ''
})

const authUser = useAuthUser()
const router = useRouter()

async function handleSubmit() {
  try {
    await authUser.SignIn({...userCredentials.value})
    await router.push('/')
  } catch (error) {
    console.error(error)
  }
}
</script>

<template>
  <h1>Login</h1>
  <form @submit.prevent="handleSubmit">
    <label>
      Email
      <input v-model="userCredentials.email">
    </label>
    <label>
      Senha
      <input v-model="userCredentials.password">
    </label>
    <button type="submit">
      Login
    </button>
  </form>
  <RouterLink to="/auth/sign-up">Cadastro</RouterLink>
</template>

<style scoped>

</style>