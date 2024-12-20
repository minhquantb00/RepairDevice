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
import UpdatePhanCongNhanVien from "./modules/UpdatePhanCongNhanVien.vue";
import ConfirmDeleteDialog from "@/components/ConfirmDeleteDialog.vue";
const props = defineProps({
  phanCongCongViecData: {
    type: Object,
    required: true,
    default: () => ({}),
  },
});
const router = useRouter();
const instance = getCurrentInstance();
const isNewPasswordVisible = ref(false);
const isConfirmPasswordVisible = ref(false);
const loading = ref(false);
const selectedFile = ref(null);
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const listLinhKienSuaChua = ref([]);
const isOpenUpdatePhanCongVisible = ref(false);
const listLinhKien = ref([]);
const currentPage = ref(1);
const pageSize = ref(10);
const dataId = ref();
const pageNumber = ref(1);
const linhKienSuaChuaId = ref();
const businessExecute = ref({
  linhKienId: null,
  thietBiSuaChuaId: null,
  soLuongDung: 0,
});
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});
const keyword = ref("");
currentPage.value = options.value.page;
const getAllLinhKienSuaChua = async () => {
  try {
    loading.value = true;
    const result = await DeviceApi.getAllLinhKienSuaChua(
      props.phanCongCongViecData.dataResponseThietBiSuaChua.id
    );
    listLinhKienSuaChua.value = result.data;
    invoices.value = result.data;
  } catch (error) {
    console.error(error);
  } finally {
    loading.value = false;
  }
};

const getAllLinhKien = async () => {
  try {
    loading.value = true;
    const result = await DeviceApi.getAllLinhKien();
    listLinhKien.value = result.data;
  } catch (error) {
    console.error(error);
  } finally {
    loading.value = false;
  }
};
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Tên linh kiện",
    key: "dataResponseLinhKien",
    sortable: false,
  },
  {
    title: "Số lượng dùng",
    key: "soLuongDung",
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
];

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

const onClickButtonSubmit = async () => {
  try {
    loading.value = false;
    console.log(props.phanCongCongViecData);
    businessExecute.value.thietBiSuaChuaId =
      props.phanCongCongViecData.dataResponseThietBiSuaChua.id;
    const result = await DeviceApi.createLinhKienSuaChua(businessExecute.value);
    if (result.status === 200) {
      loading.value = true;
      toast(result.message, {
        type: "success",
        transition: "flip",
        autoClose: 2000,
        theme: "dark",
        dangerouslyHTMLString: true,
      });

      await getAllLinhKienSuaChua();
    } else {
      loading.value = true;
      toast(result.message, {
        type: "error",
        transition: "flip",
        theme: "dark",
        autoClose: 2000,
        dangerouslyHTMLString: true,
      });
    }
    loading.value = false;
  } catch (error) {
    console.error(error);
    toast("An error occurred. Please try again.", {
      type: "error",
      transition: "flip",
      theme: "dark",
      autoClose: 2000,
      dangerouslyHTMLString: true,
    });
  } finally {
    loading.value = false;
  }
};
const onConfirmed = async () => {
  try {
    const result = await DeviceApi.xoaLinhKienSuaChua(linhKienSuaChuaId.value);
    console.log(result.data);
    if (result.status === 200) {
      toast(result.message, {
        type: "success",
        transition: "flip",
        autoClose: 2000,
        theme: "dark",
        dangerouslyHTMLString: true,
      });
      await getAllLinhKienSuaChua();
    } else {
      toast("Xoá thất bại", {
        type: "error",
        transition: "flip",
        autoClose: 2000,
        theme: "dark",
        dangerouslyHTMLString: true,
      });
    }
  } catch (error) {
    toast(error, {
      type: "error",
      transition: "flip",
      autoClose: 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
  }
};
const onclickDeleteItem = (id) => {
  console.log('object');
  console.log('delete: ', id);
  linhKienSuaChuaId.value = id;
  instance?.refs.deleteDialog.show();
};
const formatCurrency = (value) => {
  return new Intl.NumberFormat("vi-VN", { style: "currency", currency: "VND" }).format(
    value
  );
};
const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
};

watch(
  () => props.phanCongCongViecData,
  async (newVal) => {
    if (newVal) {
      businessExecute.value.thietBiSuaChuaId = newVal.thietBiSuaChuaId;
      await getAllLinhKienSuaChua();
      await getAllLinhKien();
    }
  },
  { immediate: true }
);

onMounted(async () => {
  await getAllLinhKienSuaChua();
  await getAllLinhKien();
});
</script>

<template>
  <VRow>
    <VCol cols="12">
      <!-- 👉 Change password -->
      <VCard title="Thêm thông tin linh kiện cần sửa chữa">
        <VCardText>
          <VForm @submit.prevent="onClickButtonSubmit">
            <VRow>
              <VCol cols="12" md="6">
                <AppSelect
                  ref="select"
                  v-model="businessExecute.linhKienId"
                  item-title="tenLinhKien"
                  item-value="id"
                  :items="listLinhKien"
                  clearable
                  label="Chọn linh kiện"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField
                  style="color: white"
                  v-model="businessExecute.soLuongDung"
                  label="Số lượng"
                />
              </VCol>

              <VCol cols="12">
                <VBtn type="submit"> Tạo thông tin </VBtn>
              </VCol>
            </VRow>
          </VForm>
        </VCardText>
      </VCard>
    </VCol>

    <VCol cols="12">
      <!-- 👉 Recent devices -->
      <VCard title="Danh sách linh kiện sửa chữa thiết bị">
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

          <template #item.dataResponseLinhKien="{ item }">
            {{ item.raw.dataResponseLinhKien.tenLinhKien }}
          </template>

          <template #item.soLuongDung="{ item }">
            {{ item.raw.soLuongDung }}
          </template>

          <!-- Actions -->
          <template #item.actions="{ item }">

            <IconBtn @click="onclickDeleteItem(item.raw.id)">
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
      </VCard>
    </VCol>
  <ConfirmDeleteDialog
    @onConfirmed="onConfirmed"
    title="Xác nhận"
    content="Bạn có muốn xóa không"
    ref="deleteDialog"
  ></ConfirmDeleteDialog>
  </VRow>

  <!-- 👉 Enable One Time Password Dialog -->

</template>
