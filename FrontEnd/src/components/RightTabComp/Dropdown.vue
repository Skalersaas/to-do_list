<script setup>
import { onMounted } from 'vue';
const props = defineProps({
    select:Function,
    field: Number,
    options: Array,
    tag:String
})

onMounted(()=> {
    const dropdownButton = document.getElementById(props.tag);
    const dropdownMenu = document.getElementById(props.tag + 'menu');

    dropdownButton.addEventListener('click', () => {
    dropdownMenu.classList.toggle('hidden');
    });

    // Optional: Hide the dropdown when clicking outside
    document.addEventListener('click', (e) => {
    if (!dropdownButton.contains(e.target) && !dropdownMenu.contains(e.target)) {
        dropdownMenu.classList.add('hidden');
    }})
})
</script>
<template>
    <div class="relative inline-block text-left">
        <button :id="tag" class="inline-flex justify-center w-full rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none">
            {{ options[field]}}
            <svg class="-mr-1 ml-2 h-5 w-5" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
            <path fill-rule="evenodd" d="M5.23 7.21a.75.75 0 011.06-.02l3.72 3.58a.75.75 0 001.06 0l3.72-3.58a.75.75 0 111.06 1.06l-4.5 4.32a.75.75 0 01-1.06 0l-4.5-4.32a.75.75 0 01-.02-1.06z" clip-rule="evenodd" />
            </svg>
        </button>

        <!-- Dropdown Menu -->
        <div :id="tag + 'menu'" class="hidden origin-top-right absolute mt-2 w-[100%] rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none z-10">
            <div class="py-1" role="menu" aria-orientation="vertical" aria-labelledby="options-menu">
                <a v-for="option, id in options" :key="id"
                    @click="select(id)" class="cursor-pointer block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900" role="menuitem">{{ option }}</a>
            </div>
        </div>
    </div>
</template>