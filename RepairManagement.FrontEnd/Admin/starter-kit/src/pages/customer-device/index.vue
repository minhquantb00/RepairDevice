<script setup>
import { paginationMeta } from "@/@fake-db/utils";
import ConfirmDeleteDialog from "@/components/ConfirmDeleteDialog.vue";
import ModalAddCustomer from "@/pages/customer/modules/ModalAddCustomer.vue";
import ModalUpdateCustomer from "@/pages/customer/modules/ModalUpdateCustomer.vue";
import { CustomerApi } from "@/apis/customer/customerApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";
import { useRouter } from "vue-router";
import { DeviceApi } from "@/apis/device/deviceApi";
import DeviceDetail from "./device-detail.vue"

const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const router = useRouter();
const props = defineProps({
  customerData: {
    type: Object,
    required: true,
  },
});
const instance = getCurrentInstance();
const isDeviceDetailDialogVisible = ref(false);
const listDevice = ref([]);
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});
const businessExecuteFilterDeviceOfCustomer = ref({
  keyword: "",
});
const isLoading = ref(false);
const currentPage = ref(1);
const dataId = ref();
const customerId = ref();
const pageSize = ref(10);
const pageNumber = ref(1);
const formatCurrency = (value) => {
  return new Intl.NumberFormat("vi-VN", { style: "currency", currency: "VND" }).format(
    value
  );
};
const viewDetail = (id) => {
  router.push({
    path: "/customer-detail",
  });
};
currentPage.value = options.value.page;

// 👉 headers
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Tên thiết bị",
    key: "tenThietBi",
    sortable: false,
  },
  {
    title: "Giá",
    key: "gia",
  },
  {
    title: "Khách hàng",
    key: "dataResponseKhachHang",
  },
  {
    title: "Mô tả",
    key: "moTa",
  },
  {
    title: "Trạng thái",
    key: "status",
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
];

const filterCustomer = ref({
  keyword: "",
});

const getAllThietBiOfCustomer = async () => {
  const result = await DeviceApi.getAllThietBiOfCustomer(
    businessExecuteFilterDeviceOfCustomer.value
  );
  isLoading.value = true;
  listDevice.value = result.data;
    invoices.value = result.data;
    totalInvoices.value = result.data.length;
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

const refreshData = async () => {
  await getAllThietBiOfCustomer();
};

const openDialogDeviceDetail = (id) => {
  isDeviceDetailDialogVisible.value = true;
  dataId.value = id;
};
const debounce = (func, delay) => {
  let timer;
  return function (...args) {
    clearTimeout(timer);
    timer = setTimeout(() => func.apply(this, args), delay);
  };
};

watch(
  () => options.value.page,
  async (newPage) => {
    pageNumber.value = newPage; // Cập nhật số trang hiện tại
    await getAllDevice(); // Gọi lại API để lấy dữ liệu trang mới
  }
);
watch(
  () => businessExecuteFilterDeviceOfCustomer.keyword,
  debounce(async () => {
    await getAllThietBiOfCustomer(); // Gọi API để lấy dữ liệu tìm kiếm
  }, 300) // Sử dụng debounce để giảm số lần gọi API
);
watchEffect(async () => {
  await getAllThietBiOfCustomer();
});

onMounted(async () => {
  await getAllThietBiOfCustomer();
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

        <div class="d-flex align-center flex-wrap" style="width: 200px">
          <!-- 👉 Search  -->
          <div class="invoice-list-filter" style="width: 100%">
            <AppTextField
              v-model="businessExecuteFilterDeviceOfCustomer.keyword"
              placeholder="Tìm kiếm..."
              density="compact"
            />
          </div>
        </div>
      </VCardText>

      <VDivider />

      <!-- SECTION Datatable -->
      <VDataTableServer
        v-model="selectedRows"
        v-model:items-per-page="options.itemsPerPage"
        v-model:page="options.page"
        :loading="loading"
        :headers="headers"
        :items="paginatedData"
        class="text-no-wrap"
        @update:options="options = $event"
      >
        <!-- Trending Header -->
        <template #item.stt="{ index }">
          {{ (options.page - 1) * options.itemsPerPage + index + 1 }}
        </template>

        <template #item.tenThietBi="{ item }">
          {{ item.raw.tenThietBi }}
        </template>

        <template #item.gia="{ item }">
          {{ formatCurrency(item.raw.gia) }}
        </template>
        <template #item.dataResponseKhachHang="{ item }">
          {{
            item.raw.dataResponseKhachHang != null &&
            item.raw.dataResponseKhachHang != undefined
              ? item.raw.dataResponseKhachHang.hoVaTen
              : ""
          }}
        </template>
        <template #item.moTa="{ item }">
          {{ item.raw.moTa }}
        </template>
        <template #item.status="{ item }">
          {{ item.raw.status }}
        </template>

        <!-- Actions -->
        <template #item.actions="{ item }">
          <IconBtn >
            <VIcon icon="tabler-eye-spark" @click="openDialogDeviceDetail(item.raw.id)"/>
          </IconBtn>
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
                <VBtn variant="tonal" color="default" v-bind="slotProps" :icon="false">
                  Previous
                </VBtn>
              </template>

              <template #next="slotProps">
                <VBtn variant="tonal" color="default" v-bind="slotProps" :icon="false">
                  Next
                </VBtn>
              </template>
            </VPagination>
          </div>
        </template>
      </VDataTableServer>
      <!-- !SECTION -->
    </VCard>
    <DeviceDetail
      v-model:isDialogVisible="isDeviceDetailDialogVisible"
      :dataId="dataId"
    />
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
