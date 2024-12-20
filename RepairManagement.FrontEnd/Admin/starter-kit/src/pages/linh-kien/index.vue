<script setup>
import { paginationMeta } from "@/@fake-db/utils";
import ConfirmDeleteDialog from "@/components/ConfirmDeleteDialog.vue";
import { DeviceApi } from "@/apis/device/deviceApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";
import ModalAddLinhKien from "./modules/ModalAddLinhKien.vue"
import ModalUpdateLinhKien from "./modules/ModalUpdateLinhKien.vue"
import ModalXuatNhapKho from "./modules/ModalXuatNhapKho.vue"
import {useRouter} from "vue-router"
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const router = useRouter();
const props = defineProps({
  linhKienData: {
    type: Object,
    required: true,
  },
});
const instance = getCurrentInstance();
const isModalAddLinhKienDialog = ref(false);
const isModalUpdateLinhKienDialog = ref(false);
const isModalExportLinhKienDialog = ref(false);
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
const linhKienId = ref();
currentPage.value = options.value.page;

// 👉 headers
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Tên linh kiện",
    key: "tenLinhKien",
    sortable: false,
  },
  {
    title: "Loại linh kiện",
    key: "loaiLinhKien",
  },
  {
    title: "Giá bán",
    key: "giaBan",
  },
  {
    title: "Số lượng",
    key: "soLuong",
  },

  {
    title: "Mô tả",
    key: "moTa",
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
];

const getAllLinhKien = async (filter) => {
  const result = await DeviceApi.getAllLinhKien();
  invoices.value = result.data;
  totalInvoices.value = result.data.length;
};
const openUpdateLinhKienDialog = (id) => {
  isModalUpdateLinhKienDialog.value = true;
  dataId.value = id;
}

const openExportLinhKienDialog = (id) => {
  isModalExportLinhKienDialog.value = true;
  dataId.value = id;
}
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
const formatCurrency = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value);
}
const refreshData = async () => {
  await getAllLinhKien();
};

const onConfirmed = async () => {
  try{
    const result = await DeviceApi.xoaLinhKien(linhKienId.value);
    if(result.status === 200){
      toast(result.message, {
      type: "success",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    await getAllLinhKien();
    }
    else{
      toast("Xoá thất bại", {
      type: "error",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    }
  }catch(error){
    toast(error, {
      type: "error",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
  }
}

const onclickDeleteItem = (id) => {
  linhKienId.value = id;
  instance?.refs.deleteDialog.show();
}

watchEffect(async () => {
  await getAllLinhKien();
});

onMounted(async () => {
  await getAllLinhKien();
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
            @click="isModalAddLinhKienDialog = true"
          >
            Tạo linh kiện
          </VBtn>
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

        <template #item.tenLinhKien="{ item }">
          {{ item.raw.tenLinhKien }}
        </template>

        <template #item.loaiLinhKien="{ item }">
          {{ item.raw.loaiLinhKien }}
        </template>
        <template #item.giaBan="{ item }">
          {{ formatCurrency(item.raw.giaBan) }}
        </template>
        <template #item.soLuong="{ item }">
          {{ item.raw.soLuong }}
        </template>
        <template #item.moTa="{ item }">
          {{ item.raw.moTa }}
        </template>


        <!-- Actions -->
        <template #item.actions="{ item }">
        <IconBtn @click="openExportLinhKienDialog(item.raw.id)">
            <VIcon icon="tabler-table-export" />
          </IconBtn>
          <IconBtn @click="openUpdateLinhKienDialog(item.raw.id)">
            <VIcon icon="tabler-settings-check" />
          </IconBtn>

          <IconBtn @click="onclickDeleteItem(item.raw.id)">
            <VIcon icon="tabler-trash" />
          </IconBtn>

          <!-- <IconBtn @click="openDialogCustomerDetail(item.raw.id)">
            <VIcon icon="tabler-eye-spark" />
          </IconBtn> -->
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

    <ModalAddLinhKien
      v-model:isDialogVisible="isModalAddLinhKienDialog"
      :linhKienData="props.linhKienData"
    />
    <ModalXuatNhapKho
      v-model:isDialogVisible="isModalExportLinhKienDialog"
      :dataId="dataId"
    />
    <ModalUpdateLinhKien
      v-model:isDialogVisible="isModalUpdateLinhKienDialog"
      :dataId="dataId"
    />

    <ConfirmDeleteDialog
      @onConfirmed="onConfirmed"
      title="Xác nhận"
      content="Bạn có muốn xóa không"
      ref="deleteDialog"
    ></ConfirmDeleteDialog>
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
