<script setup lang="ts">
import {onMounted, ref} from "vue";
import axios from "axios";
import type {DayOfWeek} from "@/models";

interface ClassSchedule {
  dayOfWeek: number
  classes: Class[]
}

interface Class {
  name: string
  absences: number
  total: number
  freq: string
  dayOfWeek: number
  hour: number
  minute: number
  time: string
}

const itemsDayOfWeek: DayOfWeek[] = [
  {day: 0, str: 'Domingo',},
  {day: 1, str: 'Segunda',},
  {day: 2, str: 'Terça',},
  {day: 3, str: 'Quarta',},
  {day: 4, str: 'Quinta',},
  {day: 5, str: 'Sexta',},
  {day: 6, str: 'Sábado',},
]

const tab = ref()
const classesSchedules = ref<ClassSchedule[]>()

onMounted(async () => {
  try {
    const res = await axios.get<ClassSchedule[]>('/api/classes/schedule')
    classesSchedules.value = res.data
  } catch (e) {
    console.error(e)
  }
})
</script>

<template>
<div class="h-100 d-flex">
  <v-tabs
      v-model="tab"
      direction="vertical"
      class="h-100"
  >
    <v-tab v-for="d in itemsDayOfWeek" :value="d.day">{{d.str}}</v-tab>
  </v-tabs>

  <div class="flex-grow-1 h-100">
    <v-window v-model="tab">
      <v-window-item v-for="d in itemsDayOfWeek" :value="d.day" class="pa-4">
        {{d.str}}
      </v-window-item>
    </v-window>
  </div>
</div>
</template>

<style scoped>

</style>