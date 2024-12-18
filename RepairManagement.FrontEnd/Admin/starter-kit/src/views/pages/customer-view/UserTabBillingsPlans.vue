<script setup>
import americanExpress from '@images/icons/payments/american-express.png'
import mastercard from '@images/icons/payments/mastercard.png'
import visa from '@images/icons/payments/visa.png'
import {BookingApi} from "@/apis/booking/bookingApi"
import {ref, onMounted} from "vue";
import {useRouter, useRoute} from "vue-router"
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";
import { paginationMeta } from "@/@fake-db/utils";
const props = defineProps({
  userData: {
    type: Object,
    required: true,
  },
})
const router = useRouter();
const route = useRoute();
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const instance = getCurrentInstance();
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});

const isLoading = ref(false);
const currentPage = ref(1);
const dataId = ref();


currentPage.value = options.value.page;

// 👉 headers
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Họ và tên",
    key: "hoVaTen",
    sortable: false,
  },
  {
    title: "Số điện thoại",
    key: "soDienThoai",
  },
  {
    title: "Email",
    key: "email",
  },
  {
    title: "Địa chỉ",
    key: "diaChi",
  },
  {
    title: "Thời gian đặt",
    key: "thoiGianDat",
  },
  {
    title: "Mô tả",
    key: "moTa"
  },
  {
    title: "Tên thiết bị",
    key: "tenThietBi"
  },
];


const getAllBooking = async () => {
  const result = await BookingApi.getAllBookings(props.userData.id);
  invoices.value = result.data;
  totalInvoices.value = result.length;
};

const paginatedData = computed(() => {
  const start = (options.value.page - 1) * options.value.itemsPerPage;
  const end = start + options.value.itemsPerPage;
  return invoices.value.slice(start, end);
});

const totalPages = computed(() => {
  return Math.ceil((invoices.value.length * 1.0) / options.value.itemsPerPage);
});

const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
};





watchEffect(async () => {
  await getAllBooking();
});

onMounted(async () => {
  await getAllBooking();
});
</script>

<template>
  <div>
    <VCard v-if="invoices" id="invoice-list">
      <VCardText class="d-flex align-center flex-wrap gap-4">
        <div class="me-3 d-flex gap-3">
          <AppSelect
            :model-value="options.itemsPerPage"
            :items="[
              { value: 5, title: '5' },
              { value: 10, title: '10' },
              { value: 25, title: '25' },
              { value: 50, title: '50' },
              { value: 100, title: '100' },
              { value: -1, title: 'All' },
            ]"
            style="width: 6.25rem"
            @update:model-value="options.itemsPerPage = parseInt($event, 10)"
          />
        </div>

        <VSpacer />
      </VCardText>

      <VDivider />

      <!-- SECTION Datatable -->
      <VDataTableServer
        v-model="selectedRows"
        v-model:items-per-page="options.itemsPerPage"
        v-model:page="options.page"
        :loading="isLoading"
        :headers="headers"
        :items="paginatedData"
        class="text-no-wrap"
        @update:options="options = $event"
      >
        <!-- Trending Header -->
        <template #item.stt="{ index }">
          {{ (options.page - 1) * options.itemsPerPage + index + 1 }}
        </template>

        <template #item.hoVaTen="{ item }">
          {{ item.raw.hoVaTen }}
        </template>

        <template #item.soDienThoai="{ item }">
          {{ item.raw.soDienThoai }}
        </template>
        <template #item.email="{ item }">
          {{ item.raw.email }}
        </template>
        <template #item.diaChi="{ item }">
          {{ item.raw.diaChi }}
        </template>
        <template #item.thoiGianDat="{ item }">
          {{ formatDate(item.raw.thoiGianDat) }}
        </template>
<template #item.moTa="{ item }">
          {{ item.raw.moTa }}
        </template>

        <template #item.tenThietBi="{ item }">
          {{ item.raw.tenThietBi }}
        </template>


        <!-- pagination -->

        <template #bottom>
          <VDivider />
          <div
            class="d-flex align-center justify-sm-space-between justify-center flex-wrap gap-3 pa-5 pt-3"
          >
            <p class="text-sm text-disabled mb-0">
              {{ paginationMeta(options, totalInvoices) }}
            </p>

            <VPagination
              v-model="options.page"
              :length="totalPages"
              :total-visible="$vuetify.display.xs ? 1 : totalPages"
              rounded="circle"
            >
              <template #prev="slotProps">
                <VBtn
                  variant="tonal"
                  color="default"
                  v-bind="slotProps"
                  :icon="false"
                >
                  Previous
                </VBtn>
              </template>

              <template #next="slotProps">
                <VBtn
                  variant="tonal"
                  color="default"
                  v-bind="slotProps"
                  :icon="false"
                >
                  Next
                </VBtn>
              </template>
            </VPagination>
          </div>
        </template>
      </VDataTableServer>
      <!-- !SECTION -->
    </VCard>
  </div>
</template>

<style lang="scss">
#invoice-list {
  .invoice-list-actions {
    inline-size: 8rem;
  }

  .invoice-list-filter {
    inline-size: 12rem;
  }
}
</style>
