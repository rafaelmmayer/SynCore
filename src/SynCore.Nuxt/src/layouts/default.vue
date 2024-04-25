<script setup lang="ts">
const { signOut } = useAuthUser()

function handleSignOut() {
  signOut()
      .then(() => {
        navigateTo('/auth/signin')
      })
      .catch((err) => {
        console.error(err)
      })
}
</script>

<template>
  <div class="flex flex-column h-screen">
    <div style="height: 50px" class="flex justify-content-between align-items-center px-5 border-bottom-1 border-gray-200">
      <span class="select-none">Syncore</span>
      <span class="select-none">Syncore</span>
    </div>
    <div class="flex-grow-1 flex">
      <nav style="width: 145px" class="py-2 flex flex-column">
        <ul class="flex-grow-1">
          <li>
            <NuxtLink to="/">
              <Button link label="Home" icon="pi pi-home" icon-class="mr-3"/>
            </NuxtLink>
          </li>
          <li>
            <NuxtLink to="/class-schedule">
              <Button link label="Presença" icon="pi pi-list" icon-class="mr-3"/>
            </NuxtLink>
          </li>
          <li>
            <NuxtLink to="/chat">
              <Button link label="Chat" icon="pi pi-comment" icon-class="mr-3"/>
            </NuxtLink>
          </li>
        </ul>
        <div class="mb-2 mx-3">
          <Button
              label="Sair"
              icon="pi pi-sign-out"
              icon-class="mr-3"
              severity="secondary"
              class="w-full"
              @click="handleSignOut" />
        </div>
      </nav>
      <div class="flex-grow-1 bg-gray-100">
        <slot/>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>