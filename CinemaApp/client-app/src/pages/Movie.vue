<script lang="ts" setup>
    import { ref, computed, onMounted } from 'vue';
    import { useRoute } from 'vue-router';

    const ROOT = import.meta.env.VITE_API_BASE_URL;

    const route = useRoute();

    const selectedDate = ref(new Date());

    const loading = ref(true);
    const error = ref<{ status: boolean, msg?: string }>({ status: false });
    let movie: {
        id: number,
        title: string,
        releaseDate: string,
        ageRating: string,
        duration: number,
        description: string,
        posterURL: string,
        genres: string,
    };

    const sessions: {
        id: number,
        price: number,
        dateAndTime: Date,
    }[] = [];

    function equalDates(date1: Date, date2: Date) {
        return date1.getFullYear() === date2.getFullYear() &&
               date1.getMonth() === date2.getMonth() &&
               date1.getDate() === date2.getDate();
    }

    const filteredSessions = computed(() => {
        if (!selectedDate.value) return [];
        return sessions.filter(session => equalDates(session.dateAndTime, selectedDate.value));
    });

    function formatDate(date: Date) {
        if (!date) return '';
        return date.toLocaleDateString('ru-RU', {
            day: 'numeric',
            month: 'long',
            year: 'numeric'
        });
    }

    function hasSessions(date: any) {
        const dateObj = new Date(date);
        return sessions.some(session => equalDates(session.dateAndTime, dateObj));
    }

    onMounted(async () => {
        try {
            {
                const res = await fetch(`${ROOT}/api/movies/${route.params.id}`);
                const data = await res.json();
                if (!res.ok) throw new Error(`${data.status}: ${data.title}`);
                movie = data;
            }
            {
                const res = await fetch(`${ROOT}/api/sessions?movieId=${route.params.id}`);
                const data = await res.json();
                if (!res.ok) throw new Error(`${data.status}: ${data.title}`);
                data.forEach((session: { id: number, price: number, dateAndTime: string }) => {
                    const { id, price, dateAndTime } = session;
                    sessions.push({ id: id, price: price, dateAndTime: new Date(dateAndTime) });
                });
            }
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
            <v-col cols="2" class="d-flex justify-start">
                <v-btn v-slot:prepend v-if="!loading && !error.status" to="/">
                    <div class="text-headline-small text-center">Назад</div>
                </v-btn>
            </v-col>
            <v-col cols="8" class="d-flex justify-center">
                <div class="text-display-medium text-center">Кинотеатр «Cinema»</div>
            </v-col>
            <v-col cols="2"/>
        </v-row>
    </v-app-bar>
    <v-container>
        <div v-if="loading" class="text-display-large mb-8 mt-8 text-center">Загрузка...</div>
        <div v-else-if="error.status" class="text-display-large mb-8 mt-8 text-center">Ошибка загрузки: {{ error.msg }}</div>
        <div v-else>
            <div class="text-display-large mb-8 mt-8 text-center">{{ movie.title }}</div>
            <v-row>
                <v-col cols="4">
                    <v-img rounded="lg" :src="movie.posterURL"></v-img>
                </v-col>
                <v-col cols="4">
                    <v-table>
                        <tbody>
                            <tr>
                                <td>Дата выхода</td>
                                <td>{{ formatDate(new Date(movie.releaseDate)) }}</td>
                            </tr>
                            <tr>
                                <td>Жанры</td>
                                <td>{{ movie.genres }}</td>
                            </tr>
                            <tr>
                                <td>Возрастное ограничение</td>
                                <td>{{ movie.ageRating }}</td>
                            </tr>
                            <tr>
                                <td>Длительность</td>
                                <td>{{ movie.duration }} мин.</td>
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
                        :allowed-dates="hasSessions"
                    />
                    <div class="text-title-large mb-8 mt-8 text-justify">Сеансы на {{ formatDate(selectedDate) }}</div>
                    <v-container class="d-flex flex-column justify-center">
                        <v-row>
                            <v-card
                                v-for="session in filteredSessions"
                                class="py-4 pa-4"
                                color="surface-variant"
                                rounded="lg"
                                variant="tonal"
                                :to="'/sessions/' + session.id"
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
        </div>
    </v-container>
</template>
