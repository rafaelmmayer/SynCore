<script setup lang="ts">

import type {Class, Time} from "@/models";
import {ref} from "vue";
import type {VTextField} from "vuetify/components";

interface DayOfWeek {
  day: number;
  str: string;
}

const emit = defineEmits<{
  (e: 'createClass', c: Class): void
}>()

const name = ref<string>()
const hour = ref<number>()
const minute = ref<number>()
const times = ref<Time[]>([])
const selectDayOfWeek = ref<DayOfWeek>()
const isTimeFormValid = ref<boolean>()
const isEmptyTimesMessageVisible = ref<boolean>()

const classNameInput = ref<VTextField>()

const itemsDayOfWeek: DayOfWeek[] = [
  {day: 0, str: 'Domingo',},
  {day: 1, str: 'Segunda',},
  {day: 2, str: 'Terça',},
  {day: 3, str: 'Quarta',},
  {day: 4, str: 'Quinta',},
  {day: 5, str: 'Sexta',},
  {day: 6, str: 'Sábado',},
]

function getDayOfWeekStr(day: number) {
  return itemsDayOfWeek.find((c) => c.day === day)?.str ?? ''
}

const inputsRules = {
  required: (value: string) => !!value || 'Campo obrigatório',
  min: (value: number) => value > 0 || 'Deve ser maior a 0',
  maxHour: (value: number) => value <= 24 || 'Deve ser menor ou igual a 24',
  maxMinutes: (value: number) => value <= 60 || 'Deve ser menor ou igual a 60',
}

function handleCreateClass() {
  if (!classNameInput.value?.isValid) {
    return
  }

  if (times.value?.length === 0) {
    isEmptyTimesMessageVisible.value = true
    return;
  }

  const c: Class = {
    name: name.value!,
    isActive: true,
    total: 0,
    absences: 0,
    times: times.value,
  }

  emit('createClass', c)

  clearData()
}

function handleAddTime() {
  setTimeout(() => {
    if (!isTimeFormValid.value) {
      return;
    }

    times.value.push({dayOfWeek: selectDayOfWeek.value!.day, hour: hour.value!, minute: minute.value!})

    isEmptyTimesMessageVisible.value = false

    clearTimeInputs()

  }, 10)
}

function handleDeleteTime(t: Time) {
  times.value.splice(times.value.indexOf(t), 1)
}

function clearTimeInputs() {
  selectDayOfWeek.value = undefined
  hour.value = undefined
  minute.value = undefined
}

function clearData() {
  setTimeout(() => {
    name.value = undefined
    times.value = []
    clearTimeInputs()
  }, 500)
}
</script>

<template>
  <v-dialog
      fullscreen
      transition="dialog-right-transition"
  >
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
          v-bind="activatorProps"
          prepend-icon="mdi-plus"
          elevation="0"
          color="primary"
          density="comfortable"
      >
        Adicionar
      </v-btn>
    </template>
    <template v-slot:default="{ isActive }">
      <div class="d-flex justify-end h-100">
        <div class="bg-white d-flex flex-column" style="width: 500px">
          <div class="d-flex align-center ma-3">
            <v-btn density="compact" icon="mdi-arrow-collapse-right" elevation="0"
                   @click="isActive.value = false; clearData()"></v-btn>
            <div class="flex-grow-1 text-center" style="font-size: 20px">Criar disciplina</div>
          </div>
          <div class="pa-2 flex-grow-1">
            <v-text-field
                ref="classNameInput"
                density="compact"
                variant="outlined"
                label="Nome"
                :rules="[inputsRules.required]"
                v-model="name"
            />
            <div class="mt-4">
              <v-form v-model="isTimeFormValid" @submit.prevent="handleAddTime" class="d-flex flex-column ga-3">
                <div class="d-flex align-center ga-3">
                  <div>Horários</div>
                  <v-btn
                      density="compact"
                      icon="mdi-plus"
                      elevation="0"
                      type="submit"
                  />
                  <div v-if="isEmptyTimesMessageVisible" style="color: #dc362e">Por favor, preencha pelo menos um horário</div>
                </div>
                <v-select
                    v-model="selectDayOfWeek"
                    :items="itemsDayOfWeek"
                    item-title="str"
                    item-value="day"
                    label="Dia da semana"
                    return-object
                    single-line
                    density="compact"
                    variant="outlined"
                    :rules="[inputsRules.required]"
                />
                <div class="d-flex ga-3">
                  <v-text-field
                      density="compact"
                      variant="outlined"
                      label="Hora"
                      type="number"
                      hide-spin-buttons
                      :rules="[inputsRules.required, inputsRules.min, inputsRules.maxHour]"
                      v-model="hour"
                  />
                  <v-text-field
                      density="compact"
                      variant="outlined"
                      label="Minutos"
                      type="number"
                      hide-spin-buttons
                      :rules="[inputsRules.required, inputsRules.min, inputsRules.maxMinutes]"
                      v-model="minute"
                  />
                </div>
              </v-form>
            </div>
            <div class="d-flex flex-column ga-2 mt-2">
              <div v-for="time in times" style="background: #f4f5f7"
                   class="py-2 px-4 d-flex align-center justify-space-between">
                <div>
                  <div>{{ getDayOfWeekStr(time.dayOfWeek) }}</div>
                  <div>{{ time.hour }}h{{ time.minute }}</div>
                </div>
                <div>
                  <v-btn
                      density="compact"
                      icon="mdi-close"
                      elevation="0"
                      color="transparent"
                      @click="() => handleDeleteTime(time)"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="pa-2 d-flex justify-center">
            <v-btn
                elevation="0"
                color="primary"
                density="comfortable"
                @click="handleCreateClass"
            >
              Criar
            </v-btn>
          </div>
        </div>
      </div>
    </template>
  </v-dialog>
</template>

<style scoped>

</style>