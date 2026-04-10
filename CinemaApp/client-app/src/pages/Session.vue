<script lang="ts" setup>
    import { ref } from 'vue';
    import SeatSelect from '@/components/SeatSelect.vue';

    const session = {
        movieTitle: "Интерстеллар",
        dateAndTime: new Date(2026,3,25, 13,0),
        price: 400,
    };

    const formattedDate = session.dateAndTime.toLocaleString('ru-RU');

    let selectedSeat: { row: number, seat: number } | null = null;

    function onSeatChanged(value: { row: number, seat: number } | null): void {
        selectedSeat = value;
        btnDisabled.value = false;
    }

    const btnDisabled = ref(true);
</script>

<template>
    <v-container class="fill-height d-flex flex-column justify-center" max-width="1200">
        <v-row>
            <v-col cols="4">
                <div class="text-display-large mb-8 mt-8 text-center">{{ session.movieTitle }}</div>
                <div class="text-headline-small mb-8 mt-8 text-center">{{ formattedDate }}</div>
                <SeatSelect @update:selectedSeat="onSeatChanged"/>
            </v-col>
            <v-col cols="8">
                <div class="text-display-large mb-8 mt-8 text-center">Оплата</div>
                <div class="d-flex justify-center">
                    <v-btn class="mt-16 pa-8" :disabled="btnDisabled">
                        <div class="text-headline-small text-center">Оплатить {{ session.price }} Р</div>
                    </v-btn>
                </div>
            </v-col>
        </v-row>
    </v-container>
</template>
