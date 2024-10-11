<script setup>
import Profile from './LeftTabComp/Profile.vue';
import Group from './LeftTabComp/Group.vue';
import { addGroup, getGroups, groups } from '@/scripts/groups';
import { onBeforeMount, ref, watch } from 'vue';
import { getBaseTaskCount } from '@/scripts/tasks';
import { user } from '@/scripts/user';
import { activeTab, groupsLoaded, tasksChanged } from '@/scripts/UIcontroller';


const tabs = ref([{title: 'My Day'}, {title: 'Planned'}, {title: 'Completed'}]);
const icons = ['fa-sun', 'fa-calendar-alt', 'fa-check-circle', 'fa-tasks', 'fa-bars'];
onBeforeMount(async () => {
    const base = await getBaseTaskCount()
    for (let i = 0; i < 3; i++)
    {
        tabs.value[i].count = base[i]
        tabs.value[i].base = true
    }
    await getGroups();
    for (let i in groups.value)
    {
        tabs.value.push({
            title:groups.value[i].name,
            count:groups.value[i].taskCount,
            id:groups.value[i].id,
            base:false
        })
    }
    activeTab.value = tabs.value[0]
    groupsLoaded.value = true
});
const AddGroup = async () => {
    const group = await addGroup('Untitled')
    tabs.value.push({
            title:group.name,
            count:group.taskCount,
            id:group.id,
            base:false
        })
    groupsLoaded.value = false;
}

watch(() => tasksChanged.value, async (newValue) =>
{
    if (!newValue)
        return;

    const base = await getBaseTaskCount()
    for (let i = 0; i < 3; i++)
        tabs.value[i].count = base[i];

    await getGroups()
    for (let i in groups.value)
    {
        let num = Number(i) + 3;
        tabs.value[num].title = groups.value[i].name;    
        tabs.value[num].count = groups.value[i].taskCount;    
        tabs.value[num].id = groups.value[i].id;    
    }
    groupsLoaded.value = true
    tasksChanged.value = false
});
</script>
<template>
<div class="flex flex-col flex-[1]">
    <!-- Profile -->
     <Profile :user-name="user.fullName" :email="user.login"/>
    <!-- Search Form -->
    <div>
        <form action="" class="m-4">
            <input type="text" placeholder="Search"
                class="rounded-md pl-2 p-0.5
                     border-[#E5E5E5] border-2 border-b-[#868686] 
                       focus-within:outline-none focus-within:border-b-[#0067C0]">
        </form>
    </div>
    <!-- Groups -->
    <div class="h-[70%] overflow-y-auto scroll">
        <ul class="pl-1 space-y-1">
            <template v-for="tab, id in tabs" :key="id">
                <Group  :tab="tab" :icon="icons[id > 3 ? 4 : id]"
                        :active="id==tabs.indexOf(activeTab)"/>

                <hr v-if="id==3 && tabs.length > 4" class="h-0.5 my-8 bg-[#BEBEBE]">
            </template>
        </ul>
    </div>
    
    <!-- Settings -->
    <div class="mt-auto flex space-x-1">
        <button class="hover:bg-[#EAEAEA] rounded
                        p-1 flex-[5] text-left pl-[]"
                @click="AddGroup()">
            <span class="text-2xl mr-4 ml-2">+</span>New list
        </button>
    </div>
</div>
</template>