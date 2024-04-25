<script setup lang="ts">

import type {Class} from "~/types";

const { getAllClasses } = useApiClient()

const classes = ref<Class[]>([])
const isSideBarVisible = ref(false)

onMounted(async () => {
  try {
    const res = await getAllClasses()
    classes.value = res.data
  } catch (error) {
    console.log(error)
  }
})
</script>

<template>
  <div class="flex flex-column gap-3 h-full">
    <div>
      <Button size="small" icon="pi pi-plus" label="Adicionar" @click="() => isSideBarVisible = true" />
    </div>
    <DataTable :value="classes" size="small" class="flex-grow-1 h-1rem" scrollable scroll-height="flex">
      <Column field="name" header="Disciplina"></Column>
      <Column field="total" header="Aulas"></Column>
      <Column field="absences" header="Freq"></Column>
      <Column header="% Freq">
        <template #body="slotProps">
          {{ (slotProps.data.total !== 0 ? slotProps.data.absences / slotProps.data.total : 0).toFixed(2) }}
        </template>
      </Column>
      <Column >
        <template #body="slotProps">
          {{ (slotProps.data.total !== 0 ? slotProps.data.absences / slotProps.data.total : 0).toFixed(2) }}
        </template>
      </Column>
    </DataTable>
  </div>

  <Sidebar v-model:visible="isSideBarVisible" header="Left Sidebar" position="right" >
    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
  </Sidebar>
</template>

<style scoped>

</style>