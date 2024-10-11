<script setup>
  import LeftTab from './components/LeftTab.vue';
  import CenterTab from './components/CenterTab.vue';
  import RightTab from './components/RightTab.vue';
  import Login_Register from './components/Login_Register.vue';
  import { logined, shownTask } from './scripts/UIcontroller';
  import { getCookieByName } from './scripts/helpers';

</script>
<template>
  <template v-if="logined">
    <LeftTab/>  
    <CenterTab/>
    <RightTab v-if="shownTask" :task="shownTask"/>
  </template>
  <Login_Register v-else/>
</template>
