<script setup lang="ts">

import {useApiClient} from "@/composables/http";
import {ref, watch} from "vue";

const { execute, resetErrors, error, isError, isSuccess } = useApiClient<void>()

const email = ref('')
const isFormValid = ref(false)

watch(email, () => {
  resetErrors()
})

const rulesInputs = {
  required: (value: string) => !!value || 'Campo obrigatório',
}

function handleSubmit() {
  if (!isFormValid.value) {
    return
  }
  execute({
    url: '/api/auth/email-password-reset?email=' + email.value,
    method: "POST",
  })
}
</script>

<template>
  <main>
    <div v-if="isSuccess">
      E-mail enviado
    </div>
    <div v-else>
      <div v-if="isError && error" class="error-container">
        {{error[0].errorMessage}}
      </div>
      <VForm v-model="isFormValid" @submit.prevent="handleSubmit">
        <label for="resetEmail">
          Digite o endereço de e-mail da sua conta da que iremos enviar um link de redefinição de senha.
        </label>
        <VTextField
            variant="outlined"
            id="resetEmail"
            name="resetEmail"
            type="email"
            density="compact"
            v-model="email"
            :rules="[rulesInputs.required]"
        />
        <VBtn type="submit" elevation="0" color="primary">
          Enviar
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
}
form {
  width: 500px;
  gap: 8px;
  display: grid;
}
label {
  margin-top: 8px;
}
</style>