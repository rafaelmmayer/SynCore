<script setup lang="ts">

import ClassesRegistrationTab from "~/components/TabsPanels/ClassesRegistrationTab.vue";
</script>

<template>
  <TabView
      class="h-full flex flex-column"
      :pt="{ panelContainer: 'custom-tabview-panels' }"
  >
    <TabPanel header="Cadastro" :pt="{ content: 'custom-tabview-panel' }">
      <ClassesRegistrationTab />
    </TabPanel>
    <TabPanel header="Grade" >
      
    </TabPanel>
  </TabView>
</template>

<style>
.custom-tabview-panels {
  flex-grow: 1;
  height: 1px;
  overflow: auto;
}

.custom-tabview-panel {
  height: 100%;
}
</style>