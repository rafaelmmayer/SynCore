<script setup lang="ts">

import {ref, watch} from "vue";
import {useRoute} from "vue-router";
import {useApiClient} from "@/composables/http";

const route = useRoute()
const { isError, error, isSuccess, execute, resetErrors } = useApiClient<void>()

const password = ref('')
const confirmPassword = ref('')
const isFormValid = ref(false)

const rulesInputs = {
  required: (value: string) => !!value || 'Campo obrigatório',
  confirmPassword: (value: string) => {
    if (password.value === value) {
      return true
    }

    return 'Senhas devem ser iguais'
  }
}

watch([password, confirmPassword], () => {
  resetErrors()
})

function handleSubmit() {
  if (!isFormValid.value) {
    return
  }

  const params = new URLSearchParams()
  params.append('token', route.query.token?.toString() ?? '');
  params.append('password', password.value);

  execute({
    url: '/api/auth/password-reset',
    method: 'POST',
    data: params
  })
}
</script>

<template>
<main>
  <div v-if="isSuccess">
    Senha trocada
  </div>
  <div v-else>
    <div v-if="isError && error" class="error-container">
      <p v-for="err in error" :key="err.errorMessage">
        {{err.errorMessage}}
      </p>
    </div>
    <VForm v-model="isFormValid" @submit.prevent="handleSubmit">
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
      <VBtn type="submit" elevation="0" color="primary">
        Cadastrar
      </VBtn>
    </VForm>
  </div>
</main>
</template>

<style scoped>
main {
  height: 100vh;
  display: grid;
  place-content: center;
}
.error-container {
  padding-inline: 16px;
  padding-top: 8px;
  padding-bottom: 8px;
  background: rgba(255, 129, 130, 0.4);
  border-radius: 4px;
  margin-bottom: 20px;
}
form {
  width: 400px;
  display: grid;
  gap: 8px;
}
</style>