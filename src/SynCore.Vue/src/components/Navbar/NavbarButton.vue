<script setup lang="ts">
defineProps<{
  to: string,
  text: string,
  icon: string
}>()
</script>

<template>
  <RouterLink
    :to="to"
    class="px-3 py-2 mx-2 text-decoration-none rounded rounded-md d-flex align-items-center"
    active-class="active-class-nav-btn"
    style="border: 1px solid transparent;"
    v-slot="{ isActive }"
  >
    <v-icon
      color="blue-darken-2"
      :icon="icon"/>
    <span
      :class="{ 'font-weight-bold': isActive }"
      class="text-black ml-1 font-weight-medium"
    >
      {{ text }}
    </span>
  </RouterLink>
</template>

<style scoped>
  .active-class-nav-btn {
    background-color: #f4f5f7;
    border-color: #E4E5E7 !important;
  }
</style>