<script setup lang="ts">
const route = useRoute();

</script>

<template>
  <header class="flex items-center justify-between p-4">
    <h1 class="font-extrabold text-4xl text-emerald-500">
      <NuxtLink to="/">Backlog Clear</NuxtLink>
    </h1>
    <nav>
      <ul class="flex flex-wrap gap-6 mr-2">
        <li v-if="route.path == '/' || route.path == '/signup'">
          <NuxtLink to="/login" class="font-bold text-emerald-500 py-2 px-4 border rounded-md border-emerald-500
                   hover:bg-emerald-500 hover:text-black hover:duration-300">Login</NuxtLink>
        </li>
        <li v-if="route.path == '/' || route.path == '/login'">
          <NuxtLink to="/signup" class="font-bold text-emerald-500 py-2 px-4 border rounded-md border-emerald-500
                  hover:bg-emerald-500 hover:text-black hover:duration-300">Sign Up</NuxtLink>
        </li>
      </ul>
    </nav>
  </header>
</template>