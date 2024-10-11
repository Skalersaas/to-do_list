import { ref } from "vue";
const logined = ref(false)
const activeTab = ref();
const groupsLoaded = ref(false);
const tasksChanged = ref(false);
const shownTask = ref();
const showTask = (task) => {
    console.log(task)
    if (shownTask.value && shownTask.value == task)
        shownTask.value = null;
    else
        shownTask.value = task;

}
const switchTab = (tab) => {
    activeTab.value = tab;
}
export {logined, activeTab, groupsLoaded, tasksChanged, shownTask, switchTab, showTask}