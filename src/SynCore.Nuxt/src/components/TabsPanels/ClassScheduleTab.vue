<script setup lang="ts">
import type {ClassSchedule} from "~/types";
import {daysOfWeek} from "~/lib";
import {useClassesStore} from "~/pinia/classesStore";

interface WeekDay {
  label: string,
  value: number,
  isActive: boolean,
  classes?: ClassSchedule[]
}

const classesStore = useClassesStore()

const selectedWeekDay = ref<WeekDay>()
const weekDays = ref<WeekDay[]>(
    daysOfWeek.map(d => {
      return {
        label: d.text,
        value: d.value,
        isActive: false,
      }
    })
)

classesStore.$subscribe(() => {
  loadWeekDays()
})

function handleClickWeekDayButton(value: number) {
  weekDays.value
      .filter(b => b.value !== value)
      .map(b => b.isActive = false)

  const weekDay = weekDays.value
      .find(b => b.value === value)

  if (weekDay) {
    weekDay.isActive = true
    selectedWeekDay.value = weekDay
  }
}

function loadWeekDays() {
    weekDays.value.forEach(d => {
      const resWeekDay = classesStore.classesSchedule.find(r => r.dayOfWeek === d.value)

      if (resWeekDay) {
        d.classes = resWeekDay.classes
      }
    })
}

onMounted(async () => {
  try {
    await classesStore.loadClassesSchedule()

    loadWeekDays()

    const dayOfWeekNow = new Date().getDay()
    selectedWeekDay.value = weekDays.value.find(d => d.value === dayOfWeekNow)
    if (selectedWeekDay.value){
      selectedWeekDay.value.isActive = true
    }
  } catch (e) {
    console.error(e)
  }
})
</script>

<template>
  <div class="h-full flex flex-column gap-4">
    <div class="flex gap-3">
      <Button
          v-for="day in weekDays"
          :label="day.label"
          class="p-1"
          :class="{ 'text-gray-500': !day.isActive, 'active:bg-gray-200': !day.isActive, 'hover:bg-gray-100': !day.isActive }"
          text
          @click="() => handleClickWeekDayButton(day.value)"
      />
    </div>
    <div class="flex-grow-1 h-1rem overflow-auto flex flex-column gap-3 ">
      <Card v-for="c in selectedWeekDay?.classes" class="border-1 border-gray-300 shadow-none mr-2">
        <template #title>{{ c.name }}</template>
        <template #content>
          <div class="flex gap-3">
            <div>Freq % {{ c.freq.replace(",", ".") }}</div>
            <div>Horário: {{ c.startTime }} - {{ c.endTime }}</div>
          </div>
        </template>
      </Card>
    </div>
  </div>
</template>

<style scoped>

</style>