<script setup lang="ts">
import RegisterUserForm from "~/components/auth/RegisterUserForm.vue";
</script>

<template>
  <Header />
  <main class="flex flex-col items-center justify-center mt-38">
    <section class="w-1/5">
      <fieldset class="w-full flex flex-col items-center">
        <legend class="text-3xl font-bold text-emerald-500 m-auto">Create your account</legend>
        <p class="text-xl text-gray-500">Start cleaning your backlog!</p>
        <RegisterUserForm/>
      </fieldset>
    </section>
  </main>
</template>