<script setup lang="ts">
definePageMeta({
  layout: "public",
})

const {isAuthenticated, signIn} = useAuthUser()

const signUpLogin = ref({
  name: 'Rafael',
  lastname: 'Mayer',
  email: 'rafa.mayer67@gmail.com',
  cpf: '44480073833',
  college: 'PUC Campinas',
  password: 'mayeeR09',
  confirmPassword: 'mayeeR09',
  preferences: [] as string[]
})

function addPreference(preference: string) {
  const exist = signUpLogin.value.preferences
      .find(p => p === preference)

  if (!exist){
    signUpLogin.value.preferences.push(preference)
  }
}

async function handleLoginSubmit() {
  signIn({ email: signUpLogin.value.email, password: signUpLogin.value.password })
      .then(() => {
        navigateTo('/')
      })
      .catch((err) => {
        console.error(err)
      })
}

onMounted(() => {
  if (isAuthenticated) {
    navigateTo('/posts')
  }
})
</script>

<template>
  <div class="h-screen flex align-items-center justify-content-center">
    <div class="border-1 border-gray-200 border-round p-5">
      <form style="width: 600px; display: grid; grid-template-columns: 1fr 1fr" class="gap-3" @submit.prevent="handleLoginSubmit">
        <div class="flex flex-column gap-2">
          <label for="name">Nome</label>
          <InputText id="name" v-model="signUpLogin.name" />
        </div>
        <div class="flex flex-column gap-2">
          <label for="lastname">Sobrenome</label>
          <InputText id="lastname" v-model="signUpLogin.lastname" />
        </div>
        <div class="flex flex-column gap-2">
          <label for="email">E-mail</label>
          <InputText id="email" v-model="signUpLogin.email" />
        </div>
        <div class="flex flex-column gap-2">
          <label for="cpf">CPF</label>
          <InputText id="cpf" v-model="signUpLogin.cpf" />
        </div>
        <div style="grid-column: 1 / 3" class="flex flex-column gap-2">
          <label for="college">Faculdade</label>
          <InputText id="college" v-model="signUpLogin.college" />
        </div>
        <div class="flex flex-column gap-2">
          <label for="password">Senha</label>
          <InputText id="password" v-model="signUpLogin.password" type="password" />
        </div>
        <div class="flex flex-column gap-2">
          <label for="confirmPassword">Confirme a senha</label>
          <InputText id="confirmPassword" v-model="signUpLogin.confirmPassword" type="password" />
        </div>
        <div style="grid-column: 1 / 3" class="p-fluid flex flex-column gap-3">
          <label for="preferences">Preferências</label>
          <div class="flex align-items-center gap-2">
            <Chip label="Emprego" class="cursor-pointer" @click="addPreference('Emprego')" />
            <Chip label="Festas" class="cursor-pointer" @click="addPreference('Festas')" />
            <Chip label="Eventos" class="cursor-pointer" @click="addPreference('Eventos')" />
          </div>
          <Chips id="preferences" v-model="signUpLogin.preferences" onkeydown="return false" />
        </div>
        <Button style="grid-column: 1 / 3" label="Cadastrar" type="submit"/>
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>