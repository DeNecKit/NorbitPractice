<script lang="ts" setup>
    import { ref, onMounted } from 'vue';
    import { useRoute } from 'vue-router';

    const route = useRoute();

    const loading = ref(true);
    const error = ref<{ status: boolean, msg?: string }>({ status: false });
    let occupiedSeats: { row: number, seat: number }[];

    const rowsCount = 5;
    const seatsPerRow = 8;

    const selectedSeat = ref<{ row: number, seat: number } | null>(null);

    const emit = defineEmits<{
        (e: 'update:selectedSeat',
         value: { row: number, seat: number } | null): void
    }>();

    function isOccupied(row: number, seat: number): boolean {
        return occupiedSeats.some(s => s.row === row && s.seat === seat);
    }

    function isSelected(row: number, seat: number): boolean {
        if (selectedSeat.value === null) return false;
        return selectedSeat.value.row === row && selectedSeat.value.seat === seat;
    }

    function toggleSeat(row: number, seat: number) {
        if (isOccupied(row, seat)) return;
        selectedSeat.value = { row: row, seat: seat };
        emit('update:selectedSeat', selectedSeat.value);
    }

    function getSeatColor(row: number, seat: number): string {
        if (isOccupied(row, seat)) return 'grey';
        if (isSelected(row, seat)) return 'primary';
        return 'secondary';
    }

    onMounted(async () => {
        try {
            const res = await fetch(`https://localhost:7297/api/tickets?sessionId=${route.params.id}`);
            const data = await res.json();
            if (!res.ok) throw new Error(`${data.status}: ${data.title}`);
            occupiedSeats = data;
        } catch (err) {
            error.value = { status: true, msg: (err as Error).message };
        } finally {
            loading.value = false;
        }
    });
</script>

<template>
    <div v-if="loading" class="text-headline-small mb-8 mt-8 text-center">Загрузка...</div>
    <div v-else-if="error.status" class="text-headline-small mb-8 mt-8 text-center">Ошибка загрузки: {{ error.msg }}</div>
    <v-container v-else>
        <v-row justify="center">
            <v-col cols="auto">
            <div class="text-h6 text-center mb-4">Экран</div>
            <v-divider class="mb-6"/>
            <div v-for="row in rowsCount" :key="row" class="d-flex justify-center mb-2">
                <span class="mr-3 font-weight-bold" style="width: 30px;">{{ row }}</span>
                <div v-for="seat in seatsPerRow" :key="seat" class="mx-1">
                <v-btn
                    :color="getSeatColor(row, seat)"
                    :disabled="isOccupied(row, seat)"
                    variant="tonal"
                    size="small"
                    @click="toggleSeat(row, seat)"
                    style="min-width: 40px;"
                >
                    {{ seat }}
                </v-btn>
                </div>
            </div>
            <div v-if="selectedSeat !== null" class="text-headline-small mb-8 mt-8 text-center">
                Ряд {{ selectedSeat.row }}, Место {{ selectedSeat.seat }}
            </div>
            <div v-else class="text-headline-small mb-8 mt-8 text-center">Выберите место</div>
            </v-col>
        </v-row>
    </v-container>
</template>
