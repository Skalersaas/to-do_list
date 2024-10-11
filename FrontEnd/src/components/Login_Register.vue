<script setup>
    import { login, register, restart } from '@/scripts/user';
    import { ref } from 'vue';
    import { logined } from '@/scripts/UIcontroller';

    // restart();
    const active = ref(1)
    const email = ref('');
    const fullName = ref('');
    const password = ref('');
    const rPassword = ref('');
    const error = ref(false)
    const reg = async () => {
        if (password.value == rPassword.value)
        {
            await register(email.value, fullName.value, password.value);
            active.value = 1;
        }
        else error.value = true
    }
    const log = async () => {
        if (await login(email.value, password.value) == 'unreal')
            error.value = true
        else
            logined.value = true
    }
</script>
<template>
    <div class="m-auto">
        <div class="flex h-fit space-x-3">
            <button :class="['font-bold text-[2rem] px-4', active == 1 ? 'text-[#467469] border-b-2 border-b-[#467469]': 'text-[#C8D5D2] ']"
                    @click="active = 1; error = false; email = ''; password= '';fullName=''">
                    Login
            </button>
            <button :class="['font-bold text-[2rem] px-4', active == 2 ? 'text-[#467469] border-b-2 border-b-[#467469]': 'text-[#C8D5D2] ']"
                    @click="active = 2; error = false; email = ''; password= '';fullName=''">
                Sign Up
            </button>
        </div>
        <form v-if="active == 1"
                v-on:submit.prevent="log()" class="flex flex-col space-y-4 mt-10">
            <input type="email" placeholder="Email" v-model="email" required
                   class="pl-10 py-1 rounded-full border border-gray-200" >
            <input type="password" placeholder="Password" v-model="password" required
                   class="pl-10 py-1 rounded-full border border-gray-200" >

            <p v-if="error" class="text-red-600">Wrong email/password</p>
            
            <input type="submit" value="Login" class="text-white text-xl bg-[#467469] rounded-full py-2">
        </form>
        <form v-else
                v-on:submit.prevent="reg()" class="flex flex-col space-y-4 mt-10">
            <input type="email" placeholder="Email" v-model="email" required
                class="pl-10 py-1 rounded-full border border-gray-200">
            <input type="text" placeholder="Full Name" v-model="fullName" required
                class="pl-10 py-1 rounded-full border border-gray-200">
            <input type="password" placeholder="Password" v-model="password" required  
                class="pl-10 py-1 rounded-full border border-gray-200">
            <input type="password" placeholder="Repeat Password" v-model="rPassword" required
                class="pl-10 py-1 rounded-full border border-gray-200">

            <p v-if="error" class="text-red-600">Passwords must be equal</p>
            <input type="submit" value="Create Account" class="text-white text-xl bg-[#467469] rounded-full py-2">
        </form>
    </div>
</template>