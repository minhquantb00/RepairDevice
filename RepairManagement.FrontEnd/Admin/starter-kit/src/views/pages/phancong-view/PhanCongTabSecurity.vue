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
  thietBiSuaChuaData: {
    type: Object,
    required: true,
  },
})
const router = useRouter();
const isNewPasswordVisible = ref(false);
const isConfirmPasswordVisible = ref(false);
const loading = ref(false);
const selectedFile = ref(null);
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const listPhanCong = ref([]);
const users = ref([])
const currentPage = ref(1);
const pageSize = ref(10);
const pageNumber = ref(1);
const businessExecute = ref({
  nguoiDungId: null,
  thietBiSuaChuaId: null,
  ghiChu: ''
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
const getAllPhanCong = async () => {
  console.log(props.thietBiSuaChuaData.id);
  const result = await DeviceApi.getAllPhanCong(props.thietBiSuaChuaData.id);
  listPhanCong.value = result.data;
  invoices.value = result.data;
  totalInvoices.value = result.data.length;
};
const getAllUser = async () => {
  const result = await DeviceApi.getAllUser(keyword.value);
  users.value = result.data;
}
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Tên thiết bị",
    key: "dataResponseThietBiSuaChua",
    sortable: false,
  },
  {
    title: "Nhân viên",
    key: "nhanVien",
  },
  {
    title: "Thời gian phân công",
    key: "thoiGianPhanCong",
  },
  {
    title: "Thời gian hoàn thành",
    key: "thoiGianHoanThanh",
  },
  {
    title: "Ghi chú",
    key: "ghiChu",
  },
  {
    title: "Trạng thái",
    key: "status"
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
];
watch(
  () => options.value.page,
  async (newPage) => {
    pageNumber.value = newPage; // Cập nhật số trang hiện tại
    await getAllPhanCong(); // Gọi lại API để lấy dữ liệu trang mới
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

const onClickButtonSubmit = async () => {
  try {
    loading.value = false;
    businessExecute.value.thietBiSuaChuaId = props.thietBiSuaChuaData.id;
    const result = await DeviceApi.createPhanCongCongViec(businessExecute.value);
    if (result.status === 200) {
      loading.value = true;
      toast(result.message, {
        type: "success",
        transition: "flip",
        autoClose: 2000,
        theme: "dark",
        dangerouslyHTMLString: true,
      });

      await getAllPhanCong();
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

onMounted(async () => {
  await getAllUser();
  await getAllPhanCong();

});
</script>

<template>
  <VRow>
    <VCol cols="12">
      <!-- 👉 Change password -->
      <VCard title="Thêm thông tin thiết bị">
        <VCardText>
          <VForm @submit.prevent="() => {}">
            <VRow>
              <VCol cols="12" md="6">
                <AppSelect
                  ref="select"
                  v-model="businessExecute.nguoiDungId"
                  item-title="hoVaTen"
                  item-value="id"
                  :items="users"
                  clearable
                  label="Chọn người dùng"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField
                  style="color: white"
                  v-model="businessExecute.ghiChu"
                  label="Ghi chú của khách hàng"
                />
              </VCol>

              <VCol cols="12">
                <VBtn type="submit" @click="onClickButtonSubmit"> Phân công công việc </VBtn>
              </VCol>
            </VRow>
          </VForm>
        </VCardText>
      </VCard>
    </VCol>

    <VCol cols="12">
      <!-- 👉 Recent devices -->
      <VCard title="Danh sách thiết bị sửa chữa">
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

        <template #item.dataResponseThietBiSuaChua="{ item }">
          {{ item.raw.dataResponseThietBiSuaChua.tenThietBiSuaChua }}
        </template>

        <template #item.nhanVien="{ item }">
          {{ item.raw.nhanVien.hoVaTen }}
        </template>
        <template #item.thoiGianPhanCong="{ item }">
          {{ formatDate(item.raw.thoiGianPhanCong) }}
        </template>
        <template #item.thoiGianHoanThanh="{ item }">
          {{ formatDate(item.raw.thoiGianHoanThanh) }}
        </template>
        <template #item.ghiChu="{ item }">
          {{ item.raw.ghiChu }}
        </template>
        <template #item.status="{ item }">
          {{ item.raw.status }}
        </template>


        <!-- Actions -->
        <template #item.actions="{ item }">
          <IconBtn >
            <VIcon icon="tabler-settings-check" />
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
      </VCard>
    </VCol>
  </VRow>

  <!-- 👉 Enable One Time Password Dialog -->
  <TwoFactorAuthDialog
    v-model:isDialogVisible="isTwoFactorDialogOpen"
    :sms-code="smsVerificationNumber"
  />
</template>
