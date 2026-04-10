<script lang="ts" setup>
    const movies = [
        {
            id: 1,
            title: "Интерстеллар",
            posterUrl: "https://m.media-amazon.com/images/M/MV5BYzdjMDAxZGItMjI2My00ODA1LTlkNzItOWFjMDU5ZDJlYWY3XkEyXkFqcGc@._V1_SX300.jpg"
        },
        {
            id: 2,
            title: "Титаник",
            posterUrl: "https://m.media-amazon.com/images/M/MV5BYzYyN2FiZmUtYWYzMy00MzViLWJkZTMtOGY1ZjgzNWMwN2YxXkEyXkFqcGc@._V1_SX300.jpg"
        },
        {
            id: 3,
            title: "Человек-паук: Новый день",
            posterUrl: "https://m.media-amazon.com/images/M/MV5BMTJhZGE3NmYtYTg0Ny00MWUzLWE0MmUtYTZjYTg1ZjVlMWRkXkEyXkFqcGc@._V1_SX300.jpg"
        },
        {
            id: 4,
            title: "Мегамозг",
            posterUrl: "https://m.media-amazon.com/images/M/MV5BMTAzMzI0NTMzNDBeQTJeQWpwZ15BbWU3MDM3NTAyOTM@._V1_SX300.jpg"
        }
    ];
</script>

<template>
    <v-container class="fill-height d-flex flex-column justify-center" max-width="1200">
        <div>
            <div class="text-display-large mb-8 mt-8 text-center">Каталог фильмов</div>
            <v-row>
                <v-col v-for="movie in movies" :key="movie.id" cols="3">
                    <v-card
                        class="py-4 pa-4"
                        color="surface-variant"
                        rounded="lg"
                        variant="tonal"
                        href="https://www.youtube.com/watch?v=dQw4w9WgXcQ&pp=ygUXbmV2ZXIgZ29ubmEgZ2l2ZSB5b3UgdXA%3D"
                        rel="noopener noreferrer"
                        target="_self"
                    >
                        <div class="text-headline-small py-4 text-center">{{movie.title}}</div>
                        <v-img class="mt-4" position="bottom" :src="movie.posterUrl"/>
                    </v-card>
                </v-col>
            </v-row>
        </div>
    </v-container>
</template>
