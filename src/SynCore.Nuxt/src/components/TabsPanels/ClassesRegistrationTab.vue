<script setup lang="ts">
import AddClassSidebar from "~/components/Sidebars/AddClassSidebar.vue";
import {useClassesStore} from "~/pinia/classesStore";

const classesStore = useClassesStore()

const isSideBarVisible = ref(false)

onMounted(async () => {
  await classesStore.loadClasses()
})
</script>

<template>
  <div class="flex flex-column gap-3 h-full">
    <div>
      <Button size="small" icon="pi pi-plus" label="Adicionar" @click="() => isSideBarVisible = true" />
    </div>
    <DataTable
        :value="classesStore.classes"
        class="flex-grow-1 h-1rem"
        scrollable
        scroll-height="flex"
        size="small"
    >
      <Column field="name" header="Disciplina" ></Column>
      <Column field="total" header="Aulas"></Column>
      <Column field="absences" header="Freq"></Column>
      <Column header="% Freq">
        <template #body="slotProps">
          {{ ((slotProps.data.total !== 0 ? slotProps.data.absences / slotProps.data.total : 0) * 100).toFixed(1) }}
        </template>
      </Column>
      <Column>
        <template #body="slotProps">
          <Button
              class="p-1"
              label="Editar"
              severity="contrast"
              outlined
              size="small"
          />
        </template>
      </Column>
    </DataTable>
  </div>

  <AddClassSidebar v-model="isSideBarVisible" />
</template>

<style>

</style>