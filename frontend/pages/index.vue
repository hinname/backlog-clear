<script setup lang="ts">
useHead({
  titleTemplate: '%s | Backlog Clear',
  meta: [
    {
      name: 'description',
      content: 'Backlog Clear'
    }
  ]
})
</script>

<template>
  <header class="flex items-center justify-between p-4">
    <h1 class="font-extrabold text-4xl text-emerald-500">Backlog Clear</h1>
    <nav>
      <ul class="flex flex-wrap gap-6 mr-2">
        <li>
          <nuxt-link to="/login" class="font-bold text-emerald-500 py-2 px-4 border rounded-md border-emerald-500
                   hover:bg-emerald-500 hover:text-black hover:duration-300">Login</nuxt-link>
        </li>
        <li>
          <nuxt-link to="/signup" class="font-bold text-emerald-500 py-2 px-4 border rounded-md border-emerald-500
                  hover:bg-emerald-500 hover:text-black hover:duration-300">Sign Up</nuxt-link>
        </li>
      </ul>
    </nav>
  </header>
  <main>
    <section class="flex flex-col items-center justify-center h-screen">
      <h2 class="text-5xl font-bold text-emerald-500">Welcome to Backlog Clear</h2>
      <p class="text-2xl text-gray-500">A simple way to manage the games you play</p>
      <nuxt-link to="/signup" class="font-bold text-emerald-500 py-2 px-4 border rounded-md border-emerald-500 mt-2 text-xl
                  hover:bg-emerald-500 hover:text-black hover:duration-300">SIGN UP NOW</nuxt-link>
    </section>
  </main>
</template>