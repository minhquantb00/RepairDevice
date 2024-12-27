<script setup>
import FullCalendar from '@fullcalendar/vue3'
import {
  blankEvent,
  useCalendar,
} from '@/views/pages/customer-booking/useCalendar'
import { useCalendarStore } from '@/views/pages/customer-booking/useCalendarStore'
import { useResponsiveLeftSidebar } from '@core/composable/useResponsiveSidebar'
import {AuthApi} from "@/apis/auth/authApi"
import { BookingApi } from "@/apis/booking/bookingApi"
import { ref, onMounted, reactive, watchEffect } from "vue";

// Components
import CalendarEventHandler from '@/views/pages/customer-booking/CalendarEventHandler.vue'

const store = useCalendarStore()
const userInfo = JSON.parse(localStorage.getItem('userInfo'));
const event = ref(structuredClone(blankEvent))
const isEventHandlerSidebarActive = ref(false)
const listBooking = ref([]);

const bookedDates = ref([]);

const getAllBookings = async () => {
  try {
    const khachHang = await AuthApi.getKhachHangByUserId(userInfo.Id);
    const result = await BookingApi.getAllBookings(khachHang.data.id);
    listBooking.value = result.data;
      listBooking.value.forEach(element => {
        if (element?.thoiGianDat) {
          const formattedDate = new Date(element.thoiGianDat).toISOString().split('T')[0];
          if (!bookedDates.value.includes(formattedDate)) {
            bookedDates.value.push(formattedDate);
          }
        }
      });
  } catch (error) {
    console.error('Error fetching bookings:', error);
  }
};

watch(isEventHandlerSidebarActive, val => {
  if (!val)
    event.value = structuredClone(blankEvent)
})

const { isLeftSidebarOpen } = useResponsiveLeftSidebar()
const { refCalendar, calendarOptions, addEvent, updateEvent, removeEvent, jumpToDate } = useCalendar(event, isEventHandlerSidebarActive, isLeftSidebarOpen)

const customizedCalendarOptions = ref({
  ...calendarOptions,
  dayCellDidMount: async (info) => {
    await getAllBookings();
    const formattedDate = info.date.toISOString().split('T')[0];
    const listArray = Object.values(bookedDates.value)
    if (listArray.includes(formattedDate)) {
      info.el.style.fontWeight = 'bold';
      info.el.style.backgroundColor = '#e0f7fa';
    }
  },
});

onMounted(async () => {
  await getAllBookings();
})
</script>



<template>
  <div>
    <VCard>
      <VLayout style="z-index: 0;">
        <VMain>
          <VCard flat>
            <VCardTitle>Thông tin đặt lịch</VCardTitle>
            <FullCalendar
              ref="refCalendar"
              :options="customizedCalendarOptions"
            />

          </VCard>
          <div class="calendar-legend">
              <div class="legend-item">
                <span class="legend-color" style="background-color: #e0f7fa;"></span>
                <span style="font-weight: bold">Đã đặt lịch</span>
              </div>
            </div>
        </VMain>

      </VLayout>
    </VCard>
  </div>
</template>

<style lang="scss">
@use "@core/scss/template/libs/full-calendar";

.calendars-checkbox {
  .v-label {
    color: rgba(var(--v-theme-on-surface), var(--v-high-emphasis-opacity));
    opacity: var(--v-high-emphasis-opacity);
  }
}

.calendar-add-event-drawer {
  &.v-navigation-drawer:not(.v-navigation-drawer--temporary) {
    border-end-start-radius: 0.375rem;
    border-start-start-radius: 0.375rem;
  }
}

.calendar-date-picker {
  display: none;

  +.flatpickr-input {
    +.flatpickr-calendar.inline {
      border: none;
      box-shadow: none;

      .flatpickr-months {
        border-block-end: none;
      }
    }
  }

  & ~ .flatpickr-calendar .flatpickr-weekdays {
    margin-block: 0 4px;
  }
}

.calendar-legend {
  margin-top: 2rem;
  margin-left: 2rem;
  margin-bottom: 1rem;
  display: flex;
  align-items: center;

  .legend-item {
    display: flex;
    align-items: center;
    margin-right: 1rem;

    .legend-color {
      width: 40px;
      height: 40px;
      display: inline-block;
      margin-right: 0.5rem;
    }
  }
}
</style>

<style lang="scss" scoped>
.v-layout {
  overflow: visible !important;

  .v-card {
    overflow: visible;
  }
}
</style>
