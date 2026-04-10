<script lang="ts" setup>
    // TODO: $route.params.id
    import { ref, computed } from 'vue';

    const selectedDate = ref(new Date());

    const movie = {
        id: 1,
        title: "Интерстеллар",
        posterUrl: "https://m.media-amazon.com/images/M/MV5BYzdjMDAxZGItMjI2My00ODA1LTlkNzItOWFjMDU5ZDJlYWY3XkEyXkFqcGc@._V1_SX300.jpg",
        releaseDate: "07 Nov 2014",
        genres: "Adventure, Drama, Sci-Fi",
        ageRating: "PG-13",
        description: "Earth's future has been riddled by disasters, famines, and droughts. There is only one way to ensure mankind's survival: Interstellar travel. A newly discovered wormhole in the far reaches of our solar system allows a team of astronauts to go where no man has gone before, a planet that may have the right environment to sustain human life."
    };

    const sessions = [
        {
            id: 1,
            dateAndTime: new Date(2026,3,25, 13,0),
            price: 400,
        },
        {
            id: 2,
            dateAndTime: new Date(2026,3,26, 13,0),
            price: 400,
        },
        {
            id: 3,
            dateAndTime: new Date(2026,3,27, 13,0),
            price: 400,
        },
        {
            id: 4,
            dateAndTime: new Date(2026,3,27, 15,0),
            price: 400,
        },
        {
            id: 5,
            dateAndTime: new Date(2026,3,27, 17,0),
            price: 400,
        },
    ]

    function equalDates(date1: Date, date2: Date) {
        return date1.getFullYear() === date2.getFullYear() &&
               date1.getMonth() === date2.getMonth() &&
               date1.getDate() === date2.getDate();
    }

    const filteredSessions = computed(() => {
        if (!selectedDate.value) return [];
        return sessions.filter(session => equalDates(session.dateAndTime, selectedDate.value));
    });

    const formattedDate = computed(() => {
        if (!selectedDate.value) return '';
        return selectedDate.value.toLocaleDateString('ru-RU', {
            day: 'numeric',
            month: 'long',
            year: 'numeric'
        });
    });

    function hasSessions(date: string) {
        const dateObj = new Date(date);
        return sessions.some(session => equalDates(session.dateAndTime, dateObj));
    }
</script>

<template>
    <v-container>
        <div class="text-display-large mb-8 mt-8 text-center">{{ movie.title }}</div>
        <v-row>
            <v-col cols="4">
                <v-img rounded="lg" :src="movie.posterUrl"></v-img>
            </v-col>
            <v-col cols="4">
                <v-table>
                    <tbody>
                        <tr>
                            <td>Дата выхода</td>
                            <td>{{ movie.releaseDate }}</td>
                        </tr>
                        <tr>
                            <td>Жанры</td>
                            <td>{{ movie.genres }}</td>
                        </tr>
                        <tr>
                            <td>Возрастное ограничение</td>
                            <td>{{ movie.ageRating }}</td>
                        </tr>
                    </tbody>
                </v-table>
                <div class="text-body-large mb-8 mt-8 text-justify">{{ movie.description }}</div>
            </v-col>
            <v-col cols="4">
                <v-date-picker
                    v-model="selectedDate"
                    control-variant="modal"
                    show-adjacent-months
                    landscape
                    locale="ru"
                    :events="hasSessions"
                    event-color="green lighten-1"
                />
                <div class="text-title-large mb-8 mt-8 text-justify">Сеансы на {{ formattedDate }}</div>
                <v-container class="d-flex flex-column justify-center">
                    <v-row>
                        <v-card
                            v-for="session in filteredSessions"
                            class="py-4 pa-4"
                            color="surface-variant"
                            rounded="lg"
                            variant="tonal"
                            href="https://www.youtube.com/watch?v=dQw4w9WgXcQ&pp=ygUXbmV2ZXIgZ29ubmEgZ2l2ZSB5b3UgdXA%3D"
                            rel="noopener noreferrer"
                            target="_self"
                        >
                            <v-col>
                                {{ session.dateAndTime.toLocaleTimeString('ru-RU', { hour: '2-digit', minute: '2-digit' }) }}
                            </v-col>
                            <v-col>
                                {{ session.price }} Р
                            </v-col>
                        </v-card>
                    </v-row>
                </v-container>
                <div
                    class="text-body-large mb-8 text-justify"
                    v-if="filteredSessions.length === 0"
                >
                    Нет сеансов на выбранную дату
                </div>
            </v-col>
        </v-row>
    </v-container>
</template>
