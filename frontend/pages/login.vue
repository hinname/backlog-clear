<script setup lang="ts">
import LoginForm from "~/components/auth/LoginForm.vue";
</script>

<template>
  <Header />
  <main class="flex flex-col items-center justify-center mt-38">
    <section class="w-1/5">
      <fieldset class="w-full flex flex-col items-center">
        <legend class="text-4xl font-bold text-emerald-500 m-auto">Login</legend>
        <p class="text-2xl text-gray-500">Welcome back!</p>
        <LoginForm />
      </fieldset>
    </section>
  </main>
</template>