<script setup lang="ts">
import {onMounted, ref} from "vue";
import {useAuthUser} from "@/composables/authUser";
import {useRouter} from "vue-router";

const authUser = useAuthUser()
const router = useRouter()

async function handleSignOut() {
  try {
    await authUser.SignOut()
    await router.push('/auth/sign-in')
  } catch (error) {
    console.error(error)
  }
}

onMounted(async () => {
  await authUser.Authenticate()
  if (!authUser.isAuthenticate.value) {
    await router.push('/auth/sign-in')
  }
})
</script>

<template>
  <div v-if="authUser.isAuthenticate">
    <nav style="display: flex; gap: 8px">
      <RouterLink to="/">Home</RouterLink>
      <RouterLink to="/class-schedule">Presença</RouterLink>
      <RouterLink to="/chat">Chat</RouterLink>
      <RouterLink to="/auth/sign-in">Sign In</RouterLink>
      <button @click.prevent="handleSignOut">Sign Out</button>
    </nav>
    <slot/>
  </div>
</template>

<style scoped>

</style>