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
  <header class="py-4 px-4 bg-amber-300 flex justify-between items-center">
    <h1 class="text-4xl font-bold">Backlog Clear</h1>
    <nav class="mx-10 flex-col">
      <ul class="flex justify-between items-center">
        <li class="mx-2 p-2">
          <a href="#">Login</a>
        </li>
        <li class="mx-2 p-2 border-2 border-gray-950 rounded">
          <a href="#" class="font-bold">Cadastre-se</a>
        </li>
      </ul>
    </nav>
  </header>
  <main>
    <div class="bg-blue-400 flex justify-between items-center">
      <section class="px-16">
        <h1 class="text-4xl">Utilize nossa plataforma para limpar o seu Backlog!</h1>
        <h3 class="text-xl">XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX</h3>
        <button class="bg-green-200 p-4 rounded my-5">Cadastre-se</button>
      </section>
      <div class="max-w-[45%]">
        <img src="../assets/images/home_image.jpg" alt="Imagem de uma pessoa limpando um quadro de tarefas">
      </div>
    </div>
    <div class="my-10">
      <h2 class="text-center text-3xl">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum a laoreet odio.</h2>
    </div>
  </main>
</template>