<script setup lang="ts">
import {onMounted, ref} from "vue";
import {useAuthUser} from "@/composables/authUser";
import {useRouter} from "vue-router";
import NavbarButton from "@/components/Navbar/NavbarButton.vue";

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
  <div class="h-screen d-flex flex-column" v-if="authUser.isAuthenticate">
    <div
        class="d-flex justify-lg-space-between align-center pa-6"
        style="height: 50px; border-bottom: 1px solid #E4E5E7;"
    >
      <h1 class="text-sm-h5">Syncore</h1>
      <v-avatar color="surface-variant" size="small">RM</v-avatar>
    </div>
    <div class="d-flex flex-grow-1">
      <nav
          class="d-flex flex-column ga-2 py-2"
          style="width: 160px; border-right: 1px solid #E4E5E7;"
      >
        <NavbarButton to="/" icon="mdi-home" text="Home"/>
        <NavbarButton to="/class-schedule" icon="mdi-format-list-bulleted-square" text="Presença"/>
        <NavbarButton to="/chat" icon="mdi-message" text="Chat"/>
        <div class="flex-grow-1 d-flex align-end justify-center">
          <button class="w-100 d-flex justify-center ga-2" @click.prevent="handleSignOut">
            <v-icon
              color="#BCBCBC"
              icon="mdi-logout"/>
            Sair
          </button>
        </div>
      </nav>
      <div
          class="w-100"
          style="background-color: #f4f5f7"
      >
        <slot/>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>