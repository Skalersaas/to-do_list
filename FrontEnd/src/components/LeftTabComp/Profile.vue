<script setup>
defineProps({
    userName:String,
    email:String
})
function userInitials(userName){
    const spl = userName.split(' ')
    return spl[1][0] + spl[0][0];
}
</script>
<template>
    <div class="flex ml-4 mt-4">
        <span class="bg-[#603cba] rounded-full w-12 h-12   
                     text-white text-center ">
            <p class="relative top-[25%]">{{ userInitials(userName) }}</p>
        </span>
        <span class="ml-2 relative mt-1">
            <p class="font-bold text-[0.8rem]">{{ userName }}</p>
            <p class="text-gray-500 text-xs">{{ email }}</p>
        </span>
    </div>
</template>