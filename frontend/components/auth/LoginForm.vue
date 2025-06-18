<script setup lang="ts">
import {ref} from "vue";

const email = ref('');
const password = ref('');

const handleSubmit = (event: Event) => {
  event.preventDefault();

  console.log('Email:', email.value);
  console.log('Password:', password.value);

  email.value = '';
  password.value = '';
};
</script>

<template>
  <form class="flex flex-col gap-4 mt-4 w-full">
    <div class="flex flex-col gap-2">
      <label for="email" class="font-bold text-emerald-500">Email</label>
      <input
          type="email" id="email" name="email"
          class="border border-emerald-500 rounded-md p-2"
          v-model="email" />
    </div>
    <div class="flex flex-col gap-2">
      <label for="password" class="font-bold text-emerald-500">Password</label>
      <input
          type="password" id="password" name="password"
          class="border border-emerald-500 rounded-md p-2"
          v-model="password" />
    </div>
    <button
        type="submit"
        class="font-bold bg-emerald-500 text-black py-2 px-4 border rounded-md border-emerald-500 mt-2
                  hover:bg-emerald-700 hover:border-emerald-700 hover:duration-150 cursor-pointer"
        @click="handleSubmit">Login</button>
  </form>
</template>