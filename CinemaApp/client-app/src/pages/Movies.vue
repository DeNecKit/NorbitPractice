<script lang="ts" setup>
    import { ref, onMounted } from 'vue';

    const ROOT = import.meta.env.VITE_API_BASE_URL;

    const loading = ref(true);
    const error = ref<{ status: boolean, msg?: string }>({ status: false });
    let movies: {
        id: number,
        title: string,
        posterURL: string,
    }[] = [];

    onMounted(async () => {
        try {
            const res = await fetch(`${ROOT}/api/movies`);
            const data = await res.json();
            if (!res.ok) throw new Error(`${data.status}: ${data.title}`);
            movies = data;
        } catch (err) {
            error.value = { status: true, msg: (err as Error).message };
        } finally {
            loading.value = false;
        }
    });
</script>

<template>
    <v-app-bar class="pa-4">
        <v-row align="center" no-gutters>
            <v-col cols="2"/>
            <v-col cols="8" class="d-flex justify-center">
                <div class="text-display-medium text-center">Кинотеатр «Cinema»</div>
            </v-col>
            <v-col cols="2"/>
        </v-row>
    </v-app-bar>
    <v-container class="fill-height d-flex flex-column justify-center" max-width="1200">
        <div v-if="loading" class="text-display-large mb-8 mt-8 text-center">Загрузка...</div>
        <div v-else-if="error.status" class="text-display-large mb-8 mt-8 text-center">Ошибка загрузки: {{ error.msg }}</div>
        <div v-else>
            <div class="text-display-large mb-8 mt-8 text-center">Каталог фильмов</div>
            <v-row>
                <v-col v-for="movie in movies" :key="movie.id" cols="3">
                    <v-card
                        class="py-4 pa-4"
                        color="surface-variant"
                        rounded="lg"
                        variant="tonal"
                        :to="'/movies/' + movie.id"
                        rel="noopener noreferrer"
                        target="_self"
                    >
                        <div class="text-headline-small py-4 text-center">{{movie.title}}</div>
                        <v-img class="mt-4" position="bottom" :src="movie.posterURL"/>
                    </v-card>
                </v-col>
            </v-row>
        </div>
    </v-container>
</template>
