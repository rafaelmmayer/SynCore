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
      <Column header="Disciplina" >
        <template #body="slotsProps">
          <div v-if="slotsProps.data.edit">
            <InputText type="text" v-model="slotsProps.data.name" />
          </div>
          <div v-else>
            {{ slotsProps.data.name }}
          </div>
        </template>
      </Column>
      <Column header="Aulas">
        <template #body="slotsProps">
          <div v-if="slotsProps.data.edit">
            <InputText type="text" v-model="slotsProps.data.total" />
          </div>
          <div v-else>
            {{ slotsProps.data.total }}
          </div>
        </template>
      </Column>
      <Column header="Freq">
        <template #body="slotsProps">
          <div v-if="slotsProps.data.edit">
            <InputText type="text" v-model="slotsProps.data.absences" />
          </div>
          <div v-else>
            {{ slotsProps.data.absences }}
          </div>
        </template>
      </Column>
      <Column header="% Freq">
        <template #body="slotsProps">
          <div v-if="!slotsProps.data.edit">
            {{ ((slotsProps.data.total !== 0 ? slotsProps.data.absences / slotsProps.data.total : 0) * 100).toFixed(1) }}
          </div>
        </template>
      </Column>
      <Column>
        <template #body="slotProps">
          <div class="flex gap-2">
            <Button
                class="p-1"
                :label=" slotProps.data.edit ? 'Salvar' : 'Editar' "
                severity="contrast"
                outlined
                size="small"
                @click="() => slotProps.data.edit = !slotProps.data.edit"
            />
            <Button
                class="p-1"
                label="Deletar"
                severity="danger"
                outlined
                size="small"
                @click="() => classesStore.removeClass(slotProps.data)"
            />
          </div>
        </template>
      </Column>
    </DataTable>
  </div>

  <AddClassSidebar v-model="isSideBarVisible" />
</template>

<style>

</style>