<script setup lang="ts">
import {ref, watch} from "vue";
import {useAuthUser} from "@/composables/authUser";
import {useRouter} from "vue-router";
import axios, {AxiosError} from "axios";

const authUser = useAuthUser()
const router = useRouter()

const email = ref('')
const password = ref('')
const errors = ref<{
  errorCode?: string,
  errorMessage: string,
}[]>([])

const isFormValid = ref(false);
const rulesInputs = {
  required: (value: string) => !!value || 'Campo obrigatório',
}

watch([email, password], () => {
  errors.value = []
})

async function handleSubmit() {
  errors.value = []

  if (!isFormValid.value) {
    return
  }

  try {
    await authUser.SignIn({ email: email.value, password: password.value })
    await router.push('/')
  } catch (error) {
    if(error instanceof AxiosError) {
      if (error.response) {
        if(error.response.status === 422){
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
  <div style="height: 100vh; display: grid; place-content: center">
    <div style="width: 400px">
      <h1 style="font-size: 24px; text-align: center">Login no SynCore</h1>
      <div v-if="errors" class="mt-4 mb-6">
        <div class="error-container" v-for="error in errors" :key="error.errorMessage">
          {{error.errorMessage}}
        </div>
      </div>
      <VForm v-model="isFormValid" @submit.prevent="handleSubmit">
        <VTextField
          label="E-mail"
          variant="outlined"
          v-model="email"
          @on="() => errors = []"
          type="email"
          density="compact"
          :rules="[rulesInputs.required]"
        />
        <VTextField
          label="Senha"
          variant="outlined"
          v-model="password"
          type="password"
          density="compact"
          :rules="[rulesInputs.required]"
        />
        <div style="display: flex; justify-content: space-between" class="mt-2">
          <router-link to="/auth/email-password-reset">Esqueceu a senha?</router-link>
          <router-link to="/auth/sign-up">Crie uma conta</router-link>
        </div>
        <v-btn type="submit" elevation="0" color="primary">
          Login
        </v-btn>
      </VForm>

    </div>
  </div>
</template>

<style scoped>
form {
  gap: 8px;
  display: flex;
  flex-direction: column;
}
a {
  text-decoration: none;
  color: rgb(9, 105, 218);
}
.error-container {
  padding-inline: 16px;
  padding-top: 8px;
  padding-bottom: 8px;
  background: rgba(255, 129, 130, 0.4);
  border-radius: 4px;
}
</style>