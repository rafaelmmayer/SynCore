<script setup lang="ts">

import type {Class} from "~/types";

const { getAllClasses } = useApiClient()

const classes = ref<Class[]>([])
const isSideBarVisible = ref(false)
const addClassForm = ref({
  name: '',
  total: '0',
  absences: '0'
})

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
    <DataTable
        :value="classes"
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

  <Sidebar style="width: 600px" v-model:visible="isSideBarVisible" header="Adicionar Disciplina" position="right" class="p-0">
    <form style="display: grid; grid-template-columns: 1fr 1fr" class="gap-3" @submit.prevent>
      <div style="grid-column: span 2" class="flex flex-column gap-2">
        <label for="add-class-name">Nome</label>
        <InputText id="add-class-name" v-model="addClassForm.name" />
      </div>
      <div class="flex flex-column gap-2">
        <label for="add-class-total">Aulas</label>
        <InputText id="add-class-total" v-model="addClassForm.total" type="number"/>
      </div>
      <div class="flex flex-column gap-2">
        <label for="add-class-absences">Frequência</label>
        <InputText id="add-class-absences" v-model="addClassForm.absences" type="number" />
      </div>
    </form>
  </Sidebar>
</template>

<style scoped>
</style>