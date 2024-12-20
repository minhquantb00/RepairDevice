<script setup>
import { paginationMeta } from "@/@fake-db/utils";
import { DeviceApi } from "@/apis/device/deviceApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";
import {useRouter} from "vue-router"
import ModalAddNhanVien from "./modules/ModalAddNhanVien.vue"
import NhanVienDetail from "./nhanvien-detail.vue"
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const router = useRouter();
const props = defineProps({
  nhanVienData: {
    type: Object,
    required: true,
  },
});
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
const nhanVienId = ref();
const isModalAddNhanVienDialog = ref(false);
const isModalDetailDialog = ref(false);
currentPage.value = options.value.page;
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
    title: "Create time",
    key: "createTime",
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
];

const filterNhanVien = ref({
  keyword: ''
});

const getAllUser = async () => {
  const result = await DeviceApi.getAllUser(filterNhanVien.value.keyword);
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
const openDialogNhanVienDetail = (id) => {
  isModalDetailDialog.value = true;
  nhanVienId.value = id
}
const refreshData = async () => {
  await getAllUser();
};



watchEffect(async () => {
  await getAllUser();
});

onMounted(async () => {
  await getAllUser();
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
          <!-- 👉 Create invoice -->
          <VBtn
            prepend-icon="tabler-plus"
            variant="elevated"
            class="me-4"
            @click="isModalAddNhanVienDialog = true"
          >
            Tạo nhân viên
          </VBtn>
        </div>

        <VSpacer />

        <div class="d-flex align-center flex-wrap gap-4">
          <!-- 👉 Search  -->
          <div class="invoice-list-filter">
            <AppTextField
              v-model="filterNhanVien.keyword"
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
          {{ item.raw.diaChi == null ? "Hà Nội" : item.raw.diaChi }}
        </template>

        <template #item.createTime="{item}">
          {{formatDate(item.raw.createTime)}}
        </template>


        <!-- Actions -->
        <template #item.actions="{ item }">
        <IconBtn>
            <VIcon
              icon="tabler-eye-spark"
              @click="openDialogNhanVienDetail(item.raw.id)"
            />
          </IconBtn>

          <IconBtn>
            <VIcon icon="tabler-trash" />
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
    <ModalAddNhanVien
v-model:isDialogVisible="isModalAddNhanVienDialog"
      :nhanVienData="props.nhanVienData"
    />

    <NhanVienDetail
      v-model:isDialogVisible="isModalDetailDialog"
      :dataId="nhanVienId"
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
