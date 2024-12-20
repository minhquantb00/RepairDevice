<script setup>
import { VDataTable } from "vuetify/labs/VDataTable";
import { DeviceApi } from "@/apis/device/deviceApi";
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import { toast } from "vue3-toastify";
import { VForm } from "vuetify/components/VForm";
import { VDataTableServer } from "vuetify/labs/VDataTable";
import { paginationMeta } from "@/@fake-db/utils";
import "vue3-toastify/dist/index.css";
const props = defineProps({
  nhanVienData: {
    type: Object,
    required: true,
  },
})
const router = useRouter();
const loading = ref(false);
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const listHieuSuat = ref([]);
const currentPage = ref(1);
const pageSize = ref(10);
const pageNumber = ref(1);
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});
currentPage.value = options.value.page;
const getAllHieuSuat = async () => {
  console.log(props.nhanVienData.id);
  const result = await DeviceApi.getAllHieuSuat(props.nhanVienData.id);
  listHieuSuat.value = result.data;
  invoices.value = result.data;
  totalInvoices.value = result.data.length;
};
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Họ và tên",
    key: "dataResponseUser",
    sortable: false,
  },
  {
    title: "Số thiết bị đã sửa",
    key: "soThietBiDaSua",
  },
  {
    title: "Tổng thời gian xử lý",
    key: "tongThoiGianXuLy",
  },
];
watch(
  () => options.value.page,
  async (newPage) => {
    pageNumber.value = newPage; // Cập nhật số trang hiện tại
    await getAllHieuSuat(); // Gọi lại API để lấy dữ liệu trang mới
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
  return Math.ceil((valueInvoice * 1.0) / valueItemPerPage);
});

watch(
  () => props.nhanVienData,
  async (newVal) => {
    if (newVal) {
      await getAllHieuSuat();
    }
  },
  { immediate: true }
);
onMounted(async () => {
  await getAllHieuSuat();
});
</script>

<template>
  <VRow>

    <VCol cols="12">
      <!-- 👉 Recent devices -->
      <VCard title="Hiệu suất nhân viên">
        <VDivider />
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

        <template #item.dataResponseUser="{ item }">
          {{ item.raw.dataResponseUser.hoVaTen }}
        </template>

        <template #item.soThietBiDaSua="{ item }">
          {{ item.raw.soThietBiDaSua }}
        </template>
        <template #item.tongThoiGianXuLy="{ item }">
          {{ item.raw.tongThoiGianXuLy }}
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
      </VCard>
    </VCol>
  </VRow>

  <!-- 👉 Enable One Time Password Dialog -->
  <TwoFactorAuthDialog
    v-model:isDialogVisible="isTwoFactorDialogOpen"
    :sms-code="smsVerificationNumber"
  />
</template>
