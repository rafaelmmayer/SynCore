<script setup lang="ts">
import {ref, watch} from "vue";
import {useAuthUser} from "@/composables/authUser";
import {useRouter} from "vue-router";
import axios, {AxiosError} from "axios";

const authUser = useAuthUser()
const router = useRouter()

const name = ref('')
const lastName = ref('')
const email = ref('')
const cpf = ref('')
const collegeName = ref('')
const password = ref('')
const confirmPassword = ref('')
const errors = ref<{
  errorCode?: string,
  errorMessage: string,
}[]>([])
const errorMessageEmail = ref('')
const errorMessageCpf = ref('')

const isFormValid = ref(false);
const rulesInputs = {
  required: (value: string) => !!value || 'Campo obrigatório',
  confirmPassword: (value: string) => {
    if (password.value === value) {
      return true
    }

    return 'Senhas devem ser iguais'
  }
}

watch(email, () => {
  errorMessageEmail.value = ''
})
watch([name, lastName, email, cpf, collegeName, password, confirmPassword], () => {
  errors.value = []
})

async function handleOnFocusOutEmail() {
  if (email.value.length <= 0) {
    return
  }

  try {
    const res = await axios.get(
      '/api/auth/email-exists', { params: { email: email.value } }
    )
    if (res.data) {
      errorMessageEmail.value = "E-mail já cadastrado"
    }
    else{
      errorMessageEmail.value = ''
    }
  } catch (error) {
    if (error instanceof AxiosError) {
      if (error.response) {
        if (error.response.status === 422) {
          console.log(error.response.data)
          errorMessageEmail.value = error.response.data.error[0].errorMessage
        }
      }
    }
  }
}

async function handleOnFocusOutCpf() {
  if (cpf.value.length <= 0) {
    return
  }

  try {
    const res = await axios.get(
        '/api/auth/cpf-exists', { params: { cpf: cpf.value } }
    )
    if (res.data) {
      errorMessageCpf.value = "Cpf já cadastrado"
    }
    else{
      errorMessageCpf.value = ''
    }
  } catch (error) {
    if (error instanceof AxiosError) {
      if (error.response) {
        if (error.response.status === 422) {
          console.log(error.response.data)
          errorMessageCpf.value = error.response.data.error[0].errorMessage
        }
      }
    }
  }
}

async function handleSignUp() {
  if (!isFormValid) {
    return
  }

  try {
    await authUser.SignUp({
      name: name.value,
      lastName: lastName.value,
      email: email.value,
      cpf: cpf.value,
      collegeName: collegeName.value,
      password: password.value,
    })
    await router.push('/auth/sign-in')
  } catch (error) {
    if(error instanceof AxiosError) {
      if (error.response) {
        if(error.response.status === 422){
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
  <div style="height: 100vh; display: grid; place-content: center">
    <div style="width: 600px">
      <h1 style="font-size: 24px; text-align: center">Crie sua conta no SynCore</h1>
      <div v-if="errors" class="mt-4 mb-6">
        <div class="error-container" v-for="error in errors" :key="error.errorMessage">
          {{error.errorMessage}}
        </div>
      </div>
      <VForm v-model="isFormValid" style="margin-top: 16px" @submit.prevent="handleSignUp">
        <VTextField
          label="Nome"
          variant="outlined"
          v-model="name"
          density="compact"
          :rules="[rulesInputs.required]"
        />
        <VTextField
          label="Sobrenome"
          variant="outlined"
          v-model="lastName"
          density="compact"
          :rules="[rulesInputs.required]"
        />
        <VTextField
          label="E-mail"
          variant="outlined"
          v-model="email"
          @focusout="handleOnFocusOutEmail"
          :error-messages="errorMessageEmail"
          type="email"
          density="compact"
          :rules="[rulesInputs.required]"
        />
        <VTextField
          label="CPF"
          variant="outlined"
          v-model="cpf"
          @focusout="handleOnFocusOutCpf"
          :error-messages="errorMessageCpf"
          density="compact"
          :rules="[rulesInputs.required]"
        />
        <div style="grid-column: span 2">
          <VTextField
            label="Faculdade"
            variant="outlined"
            v-model="collegeName"
            density="compact"
            :rules="[rulesInputs.required]"
          />
        </div>
        <VTextField
          label="Senha"
          variant="outlined"
          v-model="password"
          type="password"
          density="compact"
          :rules="[rulesInputs.required]"
        />
        <VTextField
          label="Confirme a senha"
          variant="outlined"
          v-model="confirmPassword"
          type="password"
          density="compact"
          :rules="[rulesInputs.required, rulesInputs.confirmPassword]"
        />
        <div class="mt-2">
          <RouterLink to="/auth/sign-in">Ja possui uma conta?</RouterLink>
        </div>
        <v-btn type="submit" elevation="0" color="primary">
          Cadastrar
        </v-btn>
      </VForm>
    </div>
  </div>
</template>

<style scoped>
form {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 8px;
}
a {
  text-decoration: none;
  color: rgb(9, 105, 218);
}
button {
  grid-column: span 2;
}
.error-container {
  padding-inline: 16px;
  padding-top: 8px;
  padding-bottom: 8px;
  background: rgba(255, 129, 130, 0.4);
  border-radius: 4px;
}
</style>