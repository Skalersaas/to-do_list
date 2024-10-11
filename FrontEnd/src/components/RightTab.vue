<script setup>
import { toRaw, ref, onUpdated, onMounted, onBeforeMount, watch } from 'vue';
import { showTask } from '@/scripts/UIcontroller';
import { getFormattedDay } from '@/scripts/helpers';
import { patchTask } from '@/scripts/tasks';
import Dropdown from './RightTabComp/Dropdown.vue';
const props = defineProps({
    task:Object,
})
watch(() => props.task, () => {
    due.value = (new Date(props.task.dueTo)).toISOString().split('T')[0];
    console.log(due.value)
})
const due = ref(new Date(props.task.dueTo).toISOString().split('T')[0])
const repeatType = ['None', 'Daily', 'Weekly', 'Monthly', 'Yearly']
const changeRepeat = async(repeat)=>
{
    props.task.repeatType = repeat;
    await update();
}
const addToMyDay = async()=>
{
    props.task.myDay = !props.task.myDay
    await update();
}
const update = async () =>{
    props.task.dueTo = (new Date(due.value)).toISOString()
    await patchTask(props.task);
}
</script>

<template>
    <div class="bg-white flex-1 p-4 flex flex-col">
        <span class="flex">
            <p class="text-center flex-1">{{ task.group }}</p>
            <button class="self mb-3" @click="showTask((task))">X</button>
        </span>
        <div class="border p-3 rounded-md flex">
            <label class="flex cursor-pointer ml-3">
                <input type="checkbox" class="hidden" :id=task.title v-model="props.task.isCompleted" v-on:change="update()"/>
                <span class="w-5 h-5 rounded-full border-2 border-blue-500 flex items-center justify-center mr-2 relative">
                    <span class="w-3 h-3 bg-blue-500 rounded-full hidden"></span>
                </span>
            </label>
            <form v-on:submit.prevent="update()">
                <input type="text" v-model="task.title" class="focus-within:outline-none">
            </form>
        </div>
        <div class="border p-3 rounded-md flex justify-between mt-4 items-baseline cursor-pointer" @click="addToMyDay">
            <p v-if="!task.myDay" class="mr-4">Add to My Day</p>
            <p v-else class="mr-4">Remove from My Day</p>
        </div>
        <div class="border p-3 rounded-md flex justify-between mt-4 items-baseline">
            <p class="mr-4">Due</p>
            <span class="flex gap-1">
                <p>{{ getFormattedDay(props.task.dueTo)}}</p>
                <input type="date" class="focus-within:outline-none max-w-5" v-model="due" v-on:change="update">
            </span>
        </div>
        <div class="border p-3 rounded-md flex justify-between mt-4 items-baseline">
            <p>Repeat</p>
            <Dropdown :options="repeatType" :select="changeRepeat" :field="task.repeatType" tag="repeat"/>
        </div>
        <div class="mt-auto">
            <hr>
            <span class="flex mt-3 text-center">
                <p v-if="!task.isCompleted" class="flex-[9]">Created on {{ getFormattedDay(task.createdOn) }}</p>
                <p v-else class="flex-[9]">Completed on {{ getFormattedDay(task.completedOn) }}</p>
            </span>
        </div>
    </div>
</template>