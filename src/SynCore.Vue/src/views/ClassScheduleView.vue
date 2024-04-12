<script setup lang="ts">

import {onMounted, ref} from "vue";
import axios from "axios";
import type {Class, ErrorResponse} from "@/models";
import AddClassDialog from "@/components/Dialogs/AddClassDialog.vue";

const tab = ref()
const classes = ref<Class[]>([])

function handleCreateClass(c: Class) {
  classes.value.push(c)
}

onMounted(() => {
  axios.get<Class[]>('/api/classes', { withCredentials: true })
    .then(res => {
      console.log(res.data)
      classes.value = res.data
    })
    .catch(err => {
      if (err.data?.error){
        const error = err.data.error as ErrorResponse[]
        console.error(error)
      } else {
        console.log('erro não mapeado', err)
      }
    })
})
</script>

<template>
  <div class="h-100 d-flex flex-column">
    <v-tabs
        v-model="tab"
        bg-color="primary"
    >
      <v-tab value="one">Cadastro</v-tab>
      <v-tab value="two">Grade</v-tab>
      <v-tab value="three">Visão geral</v-tab>
    </v-tabs>

    <v-card-text class="flex-grow-1">
      <v-window v-model="tab">
        <v-window-item value="one">
          <div class="mb-4 d-flex ga-4">
            <div class="text-md-h5">Disciplinas</div>
            <AddClassDialog @create-class="handleCreateClass"/>
          </div>
          <v-table striped density="compact" hover>
            <thead>
            <tr>
              <th class="text-left">
                Disciplina
              </th>
              <th class="text-left">
                Aulas
              </th>
              <th class="text-left">
                Freq
              </th>
              <th class="text-left">
                % Freq
              </th>
            </tr>
            </thead>
            <tbody>
            <tr
                v-for="item in classes"
                :key="item.name"
            >
              <td>{{ item.name }}</td>
              <td>{{ item.total }}</td>
              <td>{{ item.absences }}</td>
              <td>{{ (item.total !== 0 ? item.absences / item.total : 0).toFixed(2) }}</td>
            </tr>
            </tbody>
          </v-table>
        </v-window-item>

        <v-window-item value="two">
          Two
        </v-window-item>

        <v-window-item value="three">
          <div v-for="c in classes" :key="c.id">
            {{ c.name }}
          </div>
        </v-window-item>
      </v-window>
    </v-card-text>
  </div>


</template>

<style scoped>

</style>