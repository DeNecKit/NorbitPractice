<script lang="ts" setup>
    import { ref, onMounted } from 'vue';
    import { useRoute } from 'vue-router';

    const ROOT = import.meta.env.VITE_API_BASE_URL;

    const route = useRoute();

    const loading = ref(true);
    const error = ref<{ status: boolean, msg?: string }>({ status: false });

    let ticket: {
        publicId: string,
        movieTitle: string,
        dateAndTime: Date,
        row: number,
        seat: number,
        price: number,
    };

    onMounted(async () => {
        try {
            const res = await fetch(`${ROOT}/api/tickets/${route.params.id}`);
            const data = await res.json();
            if (!res.ok) throw new Error(`${data.status}: ${data.title}`);
            ticket = {
                publicId: data.publicId,
                movieTitle: data.movieTitle,
                dateAndTime: new Date(data.dateAndTime),
                row: data.row,
                seat: data.seat,
                price: data.price,
            };
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
                    <div class="text-headline-small text-center">Вернуться на главную</div>
                </v-btn>
            </v-col>
            <v-col cols="8" class="d-flex justify-center">
                <div class="text-display-medium text-center">Кинотеатр «Cinema»</div>
            </v-col>
            <v-col cols="2"/>
        </v-row>
    </v-app-bar>
    <v-container class="fill-height d-flex flex-column justify-center">
        <v-card>
            <div v-if="loading" class="text-display-large mb-8 mt-8 text-center">Загрузка...</div>
            <div v-else-if="error.status" class="text-display-large mb-8 mt-8 text-center">Ошибка загрузки: {{ error.msg }}</div>
            <v-container v-else>
                <div class="text-display-medium mb-8 mt-8 text-center">Билет</div>
                <div class="text-headline-small mb-4 mt-4 text-center">
                    Фильм: {{ ticket.movieTitle }}
                </div>
                <div class="text-headline-small mb-4 mt-4 text-center">
                    Дата и время сеанса: {{ new Date(ticket.dateAndTime).toLocaleString() }}
                </div>
                <div class="text-headline-small mb-4 mt-4 text-center">
                    Ряд {{ ticket.row }}, место {{ ticket.seat }}
                </div>
                <div class="text-headline-small mb-4 mt-4 text-center">
                    Цена билета: {{ ticket.price }} Р
                </div>
                <div class="text-headline-small mb-4 mt-4 text-center">
                    Номер билета: {{ ticket.publicId }}
                </div>
            </v-container>
        </v-card>
    </v-container>
</template>
