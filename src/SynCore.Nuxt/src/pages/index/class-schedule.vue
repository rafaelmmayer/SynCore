<script setup lang="ts">

import ClassesRegistrationTab from "~/components/TabsPanels/ClassesRegistrationTab.vue";
import ClassScheduleTab from "~/components/TabsPanels/ClassScheduleTab.vue";

</script>

<template>
  <TabView
      class="h-full flex flex-column"
      :pt="{ panelContainer: 'custom-tabview-panels' }"
  >
    <TabPanel header="Cadastro" :pt="{ content: 'custom-tabview-panel' }">
      <ClassesRegistrationTab />
    </TabPanel>
    <TabPanel header="Grade" :pt="{ content: 'custom-tabview-panel' }">
      <ClassScheduleTab />
    </TabPanel>
  </TabView>
</template>

<style>

</style>