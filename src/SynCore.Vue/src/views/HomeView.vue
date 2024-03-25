<script setup lang="ts">
import {useAuthUser} from "@/composables/authUser";

const authUser = useAuthUser()

</script>

<template>
  <h1>Posts</h1>
  <p>Id: {{authUser.user.value?.id}}</p>
  <p>Nome: {{authUser.user.value?.name}}</p>
  <p>E-mail: {{authUser.user.value?.email}}</p>
  <p>Faculdade: {{authUser.user.value?.college}}</p>
</template>
