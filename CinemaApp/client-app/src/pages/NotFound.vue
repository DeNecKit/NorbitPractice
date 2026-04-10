<!-- NotFound.vue -->
<template>
    <v-container class="fill-height d-flex flex-column justify-center" max-width="1200">
        <div class="text-display-large text-center ma-8">404 - Страница не найдена</div>
        <v-card
            to="/"
            color="surface-variant"
            rounded="lg"
            variant="tonal"
            rel="noopener noreferrer"
            target="_self"
        >
            <div class="text-title-large text-center ma-8">Вернуться на главную</div>
            <!-- <router-link to="/">Вернуться на главную</router-link> -->
        </v-card>
    </v-container>
</template>
