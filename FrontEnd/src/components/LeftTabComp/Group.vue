<script setup>
import { switchTab } from '@/scripts/UIcontroller';
const props = defineProps({
    tab:Object,
    active: Boolean,
    icon: String,
})
</script>

<template>
    <li :class="['rounded p-1 flex items-center hover:bg-[#EAEAEA] cursor-pointer', active ? 'bg-[#EAEAEA]': '']"
        @click="switchTab(tab)">
        <span v-if="active" class="w-[1.3%] h-4 bg-[#005FB8] rounded -ml-1"></span>
        <i :class="['fas ml-3', icon]"></i>
        
        <p class="pl-5">{{ tab.title }}</p>
        <span v-if="!(tab.count == 0 || tab.count == null)" 
        class="rounded-[50%] w-4 h-4 ml-auto bg-[#E6E6E6] text-center">
            <p class="relative text-xs">{{ tab.count }}</p>
        </span>
    </li>     
</template>