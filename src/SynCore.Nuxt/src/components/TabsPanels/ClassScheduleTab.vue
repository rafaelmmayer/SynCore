<script setup lang="ts">
import type {ClassSchedule} from "~/types";

interface WeekDay {
  label: string,
  value: number,
  isActive: boolean,
  classes?: ClassSchedule[]
}

const { getClassesSchedule } = useApiClient()

const selectedWeekDay = ref<WeekDay>()

const weekDays = ref<WeekDay[]>([
  {
    label: 'Domingo',
    value: 0,
    isActive: false,
  },
  {
    label: 'Segunda',
    value: 1,
    isActive: false,
  },
  {
    label: 'Terça',
    value: 2,
    isActive: false,
  },
  {
    label: 'Quarta',
    value: 3,
    isActive: false,
  },
  {
    label: 'Quinta',
    value: 4,
    isActive: false,
  },
  {
    label: 'Sexta',
    value: 5,
    isActive: false,
  },
  {
    label: 'Sábado',
    value: 6,
    isActive: false,
  }
])

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

onMounted(async () => {
  try {
    const res = await getClassesSchedule()

    weekDays.value.forEach(d => {
      const resWeekDay = res.data.find(r => r.dayOfWeek === d.value)

      if (resWeekDay) {
        d.classes = resWeekDay.classes
      }
    })

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