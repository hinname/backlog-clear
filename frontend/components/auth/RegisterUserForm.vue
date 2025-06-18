<script setup lang="ts">
import {reactive} from "vue";
import type {RegisterUserForm} from "@/types/app/RegisterUserForm";

const formData = reactive<RegisterUserForm>({
  username: '',
  email: '',
  password: '',
  confirmPassword: ''
});

const handleSubmit = (event: Event): void => {
  event.preventDefault();
}

</script>

<template>
  <form @submit="handleSubmit" class="flex flex-col gap-4 mt-4 w-full">
    <div class="flex flex-col gap-2">
      <label for="username" class="font-bold text-emerald-500">Username</label>
      <input
          type="text" id="username" name="username"
          class="border border-emerald-500 rounded-md p-2"
          v-model="formData.username"
      />
    </div>
    <div class="flex flex-col gap-2">
      <label for="email" class="font-bold text-emerald-500">Email</label>
      <input
          type="email" id="email" name="email"
          class="border border-emerald-500 rounded-md p-2"
          v-model="formData.email"
      />
    </div>
    <div class="flex flex-col gap-2">
      <label for="password" class="font-bold text-emerald-500">Password</label>
      <input
          type="password" id="password" name="password"
          class="border border-emerald-500 rounded-md p-2"
          v-model="formData.password"
      />
    </div>
    <div class="flex flex-col gap-2">
      <label for="confirmPassword" class="font-bold text-emerald-500">Confirm Password</label>
      <input
          type="password" id="confirmPassword" name="confirmPassword"
          class="border border-emerald-500 rounded-md p-2"
          v-model="formData.confirmPassword"
      />
    </div>
    <button
        type="submit"
        class="font-bold bg-emerald-500 text-black py-2 px-4 border rounded-md border-emerald-500 mt-2
                  hover:bg-emerald-700 hover:border-emerald-700 hover:duration-150 cursor-pointer">Sign Up</button>
  </form>
</template>