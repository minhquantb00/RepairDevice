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
import PhanCongDetail from "./PhanCong-detail.vue";

const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const router = useRouter();
const props = defineProps({
  thietBiSuaChuaData: {
    type: Object,
    required: true,
  },
});
const instance = getCurrentInstance();
const isThietBiSuaChuaDetailDialogVisible = ref(false);
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
    key: "tenThietBiSuaChua",
    sortable: false,
  },
  {
    title: "Mô tả lỗi",
    key: "moTaLoi",
  },
  {
    title: "Ghi chú",
    key: "ghiChuCuaKhachHang",
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

const getAllThietBiSuaChua = async () => {
  const result = await DeviceApi.getAllThietBiSuaChua(
    businessExecuteFilterDeviceOfCustomer.value
  );
  isLoading.value = true;
  listDevice.value = result.data;
  invoices.value = result.data;
  console.log(invoices.value);
  totalInvoices.value = result.data.length;
};
watch(
  () => options.value.page,
  async (newPage) => {
    pageNumber.value = newPage; // Cập nhật số trang hiện tại
    await getAllThietBiSuaChua(); // Gọi lại API để lấy dữ liệu trang mới
  }
);
const paginatedData = computed(() => {
  const start = (options.value.page - 1) * options.value.itemsPerPage;
  const end = start + options.value.itemsPerPage;
  return invoices.value.slice(start, end);
});
const totalPages = computed(() => {
  const valueInvoice = Number(invoices.value.length);
  const valueItemPerPage = Number(options.value.itemsPerPage);
  console.log(valueInvoice);
  console.log(valueItemPerPage);
  return Math.ceil((valueInvoice * 1.0) / valueItemPerPage);
});

const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
};

const refreshData = async () => {
  await getAllThietBiSuaChua();
};

const openDialogDeviceFixDetail = (id) => {
  isThietBiSuaChuaDetailDialogVisible.value = true;
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
    await getAllThietBiSuaChua(); // Gọi lại API để lấy dữ liệu trang mới
  }
);
watch(
  () => businessExecuteFilterDeviceOfCustomer.keyword,
  debounce(async () => {
    await getAllThietBiSuaChua(); // Gọi API để lấy dữ liệu tìm kiếm
  }, 300) // Sử dụng debounce để giảm số lần gọi API
);
watchEffect(async () => {
  await getAllThietBiSuaChua();
});

onMounted(async () => {
  await getAllThietBiSuaChua();
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

        <template #item.tenThietBiSuaChua="{ item }">
          {{ item.raw.tenThietBiSuaChua }}
        </template>

        <template #item.moTaLoi="{ item }">
          {{ item.raw.moTaLoi }}
        </template>
        <template #item.thoiGianNhanSua="{ item }">
          {{ item.thoiGianNhanSua}}
        </template>
        <template #item.ghiChuCuaKhachHang="{ item }">
          {{ item.raw.ghiChuCuaKhachHang }}
        </template>
        <template #item.status="{ item }">
          {{ item.raw.status }}
        </template>

        <!-- Actions -->
        <template #item.actions="{ item }">
          <IconBtn>
            <VIcon
              icon="tabler-eye-spark"
              @click="openDialogDeviceFixDetail(item.raw.id)"
            />
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
    <PhanCongDetail
      v-model:isDialogVisible="isThietBiSuaChuaDetailDialogVisible"
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
