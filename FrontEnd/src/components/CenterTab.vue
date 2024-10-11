<script setup>
import Task from './CenterTabComp/Task.vue';
import { groups, renameGroup } from '@/scripts/groups';
import { onMounted, provide, ref, watch } from 'vue';
import { addTask, removeTask } from '@/scripts/tasks';
import { getPlannedTasks, getCompletedTasks, getMyDayTasks, getTasks } from '@/scripts/tasks';
import { groupsLoaded, activeTab, tasksChanged } from '@/scripts/UIcontroller';

const tasks = ref([]);
const inputValue = ref('')
const groupInputValue = ref('')

watch([() => activeTab.value, ()=> tasksChanged.value], async ([active, tasksChange]) => {
    groupInputValue.value = (activeTab.value.title)
    if (activeTab.value.base)
    {
        switch(activeTab.value.title)
        {
            case 'My Day': tasks.value = await getMyDayTasks(); break;
            case 'Planned': tasks.value = await getPlannedTasks(); break;
            case 'Completed': tasks.value =await getCompletedTasks(); break;
        }
    }
    else
    {

        tasks.value = await getTasks(activeTab.value.id)
    }
    tasks.value.sort((a, b) => new Date(a.createdOn) - new Date(b.createdOn))
})
const add = async () => {
    const groupId = activeTab.value.id ? activeTab.value.id : groups.value[0].id
    const myDay = groupId == groups.value[0].id && activeTab.value.title == "My Day";
    tasks.value.push(await addTask(inputValue.value, groupId, myDay ))
    inputValue.value= '';
    tasksChanged.value = true
}

const RemoveTask = async (id) => {
    tasks.value = tasks.value.filter(t => t.id != id);
    await removeTask(id)
    tasksChanged.value = true
}
const RenameGroup = () => {
    renameGroup(activeTab.value.id, groupInputValue.value)
    tasksChanged.value = true
}
provide('removeTask', RemoveTask)
</script>
<template>
    <div class="bg-cover bg-no-repeat flex-[3] flex flex-col bg-center
                bg-[url('src/assets/images/skyScraper.png')]">
        <div class="flex pl-10 pt-5">
            <span v-if="activeTab" class="text-white">
                <h1 v-if="activeTab.base" class="text-3xl">{{ activeTab.title }}</h1>
                <form v-else @submit.prevent="RenameGroup">
                    <input class="text-3xl bg-transparent focus-within:outline-none" v-model="groupInputValue">
                </form>
                <p class="text-md">Sep 25</p>
            </span>
            <span v-else class="text-white">
                <h1 class="text-3xl">Loading</h1>
                <p class="text-md">Sep 25</p>
            </span>
        </div>
        <!-- Tasks  -->
        <div v-if="true" class="mt-6 space-y-1 overflow-y-auto h-[70%] scroll">
            <Task v-for="t, id in tasks" :task="t" :ident="id"/>
        </div>
        <!-- Helper -->
        <div v-else>
            <div class="relative bg-slate-800 w-[30%] rounded-2xl m-auto mt-[25%] pl-3 pr-3 p-4">
                <span class="text-white text-center">
                    <h1 class="text-2xl">Wondering where your tasks are?</h1>
                    <p class="text-xs w-[45%] m-auto">Any tasks you didn't complete in My Day last time show up in the suggestions pane.</p>
                </span>
            </div>
        </div>
        <!-- Adding new task -->
        <div class="mt-auto">
            <form @submit.prevent="add" class="mx-5 mb-4">
                <input type="text" placeholder="Add task" v-model="inputValue"
                class="rounded-md pl-8 pr-[30%] py-2 w-[100%] placeholder:text-lg text-lg
                     border-[#E5E5E5] border-2 border-b-[#868686]  
                       focus-within:outline-none"/>   
            </form>
        </div>
    </div>
</template>