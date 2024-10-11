<script setup>
import { groups } from '@/scripts/groups';
import { inject, onBeforeMount, onUpdated, ref } from 'vue';
import { showTask } from '@/scripts/UIcontroller';
import { tasksChanged } from '@/scripts/UIcontroller';
import { patchTask } from '@/scripts/tasks';
import { getFormattedDay } from '@/scripts/helpers';
const props = defineProps({
    task:Object,
    ident:Number
})
const formattedDue = ref('');
const foundGroup = ref('')
const remove = inject('removeTask')
const update = async () =>{
    await patchTask(props.task);
    tasksChanged.value = true
}
onUpdated(()=>{
    getFormattedDue();
})
onBeforeMount(() => {
    getFormattedDue();
})
const getFormattedDue = () => {
    if (props.task.dueTo != '0001-01-01T00:00:00')
        formattedDue.value = getFormattedDay(props.task.dueTo);
    foundGroup.value = groups.value.find(group => group.id === props.task.groupId).name;
}
</script>


<template>
    <div class="flex bg-[#ffffff] rounded-md mx-6 py-2 task">
        <label class="flex cursor-pointer ml-3">
            <input type="checkbox" class="hidden" :id="ident" v-model="task.isCompleted" v-on:change="update()"/>
            <span class="w-5 h-5 rounded-full border-2 border-blue-500 flex items-center justify-center mr-2 relative">
                <span class="w-3 h-3 bg-blue-500 rounded-full hidden"></span>
            </span>
        </label>

        <a class="cursor-pointer flex-1" @click="showTask(task)">
            <h1 class="text-xl -mt-1">{{ task.title }}</h1>
            <span class="flex">
                <p class="text-xs">{{ foundGroup }}</p>
                <span :class="['rounded-full w-1 h-1 bg-gray-500 m-auto mx-2', formattedDue == '' ? 'hidden': 'block']"></span>
                <p class="text-xs">{{ formattedDue }}</p>
            </span>
        </a>

        <button class="self mb-3 mr-3 hidden" @click="remove(task.id)">X</button>

    </div>
</template>