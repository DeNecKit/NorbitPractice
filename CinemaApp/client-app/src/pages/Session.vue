<script lang="ts" setup>
    import { ref, onMounted } from 'vue';
    import { useRoute, useRouter } from 'vue-router';
    import SeatSelect from '@/components/SeatSelect.vue';

    const route = useRoute();
    const router = useRouter();

    const loading = ref(true);
    const error = ref<{ status: boolean, msg?: string }>({ status: false });
    let movieId: number;
    let session: {
        id: number,
        movieTitle: string,
        dateAndTime: Date,
        price: number,
    };

    let formattedDate: string;

    let selectedSeat: { row: number, seat: number } | null = null;

    function onSeatChanged(value: { row: number, seat: number } | null): void {
        selectedSeat = value;
        btnDisabled.value = false;
    }

    const btnDisabled = ref(true);

    onMounted(async () => {
        try {
            let id: number;
            let movieTitle: string;
            let dateAndTime: Date;
            let price: number;

            {
                const res = await fetch(`https://localhost:7297/api/sessions/${route.params.id}`);
                const data = await res.json();
                if (!res.ok) throw new Error(`${data.status}: ${data.title}`);
                id = data.id;
                movieId = data.movieId;
                dateAndTime = new Date(data.dateAndTime);
                price = data.price;
            }
            {
                const res = await fetch(`https://localhost:7297/api/movies/${movieId}`);
                const data = await res.json();
                if (!res.ok) throw new Error(`${data.status}: ${data.title}`);
                movieTitle = data.title;
            }

            session = {
                id: id,
                movieTitle: movieTitle,
                dateAndTime: dateAndTime,
                price: price
            };
            formattedDate = dateAndTime.toLocaleString('ru-RU');
        } catch (err) {
            error.value = { status: true, msg: (err as Error).message };
        } finally {
            loading.value = false;
        }
    });

    async function buyTicket() {
        const publicId = crypto.randomUUID();

        try {
            const res = await fetch(
                'https://localhost:7297/api/tickets',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        publicId: publicId,
                        sessionId: session.id,
                        row: selectedSeat!.row,
                        seat: selectedSeat!.seat,
                    }),
                }
            );
            const data = await res.json();
            if (!res.ok) throw new Error(`${data.status}: ${data.title}`);
        } catch (err) {
            error.value = { status: true, msg: (err as Error).message };
        } finally {
            loading.value = false;
        }

        router.push(`/tickets/${publicId}`);
    }
</script>

<template>
    <v-app-bar class="pa-4">
          <v-row align="center" no-gutters>
            <v-col cols="2" class="d-flex justify-start">
                <v-btn v-if="!loading && !error.status" :to="'/movies/' + movieId">
                    <div class="text-headline-small text-center">Назад</div>
                </v-btn>
            </v-col>
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
            <v-row>
                <v-col cols="4">
                    <div class="text-display-large mb-8 mt-8 text-center">{{ session.movieTitle }}</div>
                    <div class="text-headline-small mb-8 mt-8 text-center">{{ formattedDate }}</div>
                    <SeatSelect @update:selectedSeat="onSeatChanged"/>
                </v-col>
                <v-col cols="8">
                    <div class="text-display-large mb-8 mt-8 text-center">Оплата</div>
                    <div class="d-flex justify-center">
                        <v-btn
                            class="mt-16 pa-8"
                            :disabled="btnDisabled"
                            @click="buyTicket"
                        >
                            <div class="text-headline-small text-center">Оплатить {{ session.price }} Р</div>
                        </v-btn>
                    </div>
                </v-col>
            </v-row>
        </div>
    </v-container>
</template>
