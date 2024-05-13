<script setup lang="ts">
import {daysOfWeek, hours, minutes} from "~/lib";
import type {ClassTime} from "~/types";
import {useClassesStore} from "~/pinia/classesStore";

const isSideBarVisible = defineModel<boolean>()

const classStore = useClassesStore()

const classForm = ref({
  name: '',
  total: '0',
  absences: '0'
})
const times = ref<ClassTime[]>([])
const timeForm = ref<{
  dayOfWeek: number
  startHour?: string
  startMinute?: string
  endHour?: string
  endMinute?: string
}>({
  dayOfWeek: 1,
  startHour: '19',
  startMinute: '20',
  endHour: '20',
  endMinute: '50',
})

function handleAddTime() {
  if (
      !timeForm.value.startHour
      || !timeForm.value.startMinute
      || !timeForm.value.endHour
      || !timeForm.value.endMinute
  ) {
    return
  }

  times.value.push({
    dayOfWeek: timeForm.value.dayOfWeek,
    startHour: timeForm.value.startHour,
    startMinute: timeForm.value.startMinute,
    endHour: timeForm.value.endHour,
    endMinute: timeForm.value.endMinute,
  })
}

function handleRemoveTime(t: ClassTime) {
  const i = times.value.indexOf(t)
  times.value.splice(i, 1)
}

function handleAddClass() {
  if (
      !classForm.value.name
      || !classForm.value.total
      || !classForm.value.absences
      || times.value.length === 0
  ) {
    return
  }

  classStore.addClass({
    name: classForm.value.name,
    total: parseInt(classForm.value.total),
    absences: parseInt(classForm.value.absences),
    isActive: true,
    times: times.value,
  })
}
</script>

<template>
  <Sidebar style="width: 500px" v-model:visible="isSideBarVisible" header="Adicionar Disciplina" position="right" class="p-0">
    <form class="flex flex-column h-full" @submit.prevent="handleAddClass">
      <div style="display: grid; grid-template-columns: 1fr 1fr" class="gap-3">
        <div style="grid-column: span 2" class="flex flex-column gap-2">
          <label for="add-class-name">Nome</label>
          <InputText id="add-class-name" v-model="classForm.name" size="small" />
        </div>
        <div class="flex flex-column gap-2">
          <label for="add-class-total">Aulas</label>
          <InputText id="add-class-total" v-model="classForm.total" type="number" size="small"/>
        </div>
        <div class="flex flex-column gap-2">
          <label for="add-class-absences">Frequência</label>
          <InputText id="add-class-absences" v-model="classForm.absences" type="number" size="small"/>
        </div>
        <div style="grid-column: span 2" class="flex flex-column gap-3">
          <div class="mt-2 flex align-items-center gap-2">
            <div>Horários</div>
            <Button icon="pi pi-plus" text rounded size="small" :pt="{ root: 'custom-small-button' }" @click="handleAddTime"/>
          </div>
          <Dropdown v-model="timeForm.dayOfWeek" :options="daysOfWeek" option-label="text" option-value="value" placeholder="Dia da Semana" />
          <div style="display: grid; grid-template-columns: 1fr 1fr 30px 1fr 1fr" class="gap-2">
            <Dropdown v-model="timeForm.startHour" :options="hours()" placeholder="Hora" />
            <Dropdown v-model="timeForm.startMinute" :options="minutes()" placeholder="Min" />
            <div style="display: grid; place-self: center">Até</div>
            <Dropdown v-model="timeForm.endHour" :options="hours()" placeholder="Hora" />
            <Dropdown v-model="timeForm.endMinute" :options="minutes()" placeholder="Min" />
          </div>
        </div>
        <div style="grid-column: span 2" class="mt-2 flex flex-column gap-3">
          <Card v-for="t in times" class="border-1 border-gray-300 shadow-none " :pt="{ body: 'custom-card-body' }">
            <template #content>
              <div class="flex align-items-center justify-content-between">
                <div>Horário: {{t.startHour}}h{{t.startHour}} - {{t.endHour}}h{{t.endMinute}}</div>
                <Button
                    icon="pi pi-times"
                    text
                    rounded
                    size="small"
                    severity="danger"
                    :pt="{ root: 'custom-small-button' }"
                    @click="() => handleRemoveTime(t)"
                />
              </div>
            </template>
          </Card>
        </div>
      </div>
      <div style="align-items: end;" class="flex-grow-1 flex">
        <Button
            size="small"
            label="Adicionar"
            class="w-full"
            type="submit"
        />
      </div>
    </form>
  </Sidebar>
</template>