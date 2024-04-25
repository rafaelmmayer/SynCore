<script setup lang="ts">
definePageMeta({
  layout: "public",
})

const {isAuthenticated, signIn} = useAuthUser()

const formLogin = ref({
  email: 'rafa.mayer67@gmail.com',
  password: 'mayeeR09'
})

function handleLoginSubmit() {
  signIn({ email: formLogin.value.email, password: formLogin.value.password })
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
      <form style="width: 400px" class="flex flex-column gap-3" @submit.prevent="handleLoginSubmit">
        <div class="flex flex-column gap-2">
          <label for="email">E-mail</label>
          <InputText id="email" v-model="formLogin.email" />
        </div>
        <div class="flex flex-column gap-2">
          <label for="password">Senha</label>
          <InputText id="password" v-model="formLogin.password" type="password" />
        </div>
        <Button label="Entrar" type="submit"/>
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>