<script lang="ts" setup>
    import { ref } from 'vue';

    const rowsCount = 5;
    const seatsPerRow = 8;

    const occupiedSeats = ref([
        { row: 2, seat: 3 },
        { row: 2, seat: 4 },
        { row: 4, seat: 7 },
    ]);

    const selectedSeat = ref<{ row: number, seat: number } | null>(null);

    const emit = defineEmits<{
        (e: 'update:selectedSeat',
         value: { row: number, seat: number } | null): void
    }>();

    function isOccupied(row: number, seat: number): boolean {
        return occupiedSeats.value.some(s => s.row === row && s.seat === seat);
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
</script>

<template>
    <v-container>
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
                    class="seat-btn"
                >
                    {{ seat }}
                </v-btn>
                </div>
            </div>
            </v-col>
        </v-row>
    </v-container>
</template>

<style scoped>
    .seat-btn {
        min-width: 40px;
        transition: all 0.2s;
    }
    .seat-btn:not(:disabled):hover {
        transform: scale(1.05);
    }
</style>
