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
  deviceData: {
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
const listThietBiSuaChua = ref([]);
const currentPage = ref(1);
const businessExecuteFilterDeviceFix = ref({
  keyword: "",
});
const pageSize = ref(10);
const pageNumber = ref(1);
const smsVerificationNumber = ref("+1(968) 819-2547");
const isTwoFactorDialogOpen = ref(false);
const status = ref(["DangHoatDong", "DangHong", "DaSuaXong"]);
const businessExecute = ref({
  khachHangId: null,
  thietBiID: null,
  moTaLoi: '',
  ghiChuCuaKhachHang: ''
});
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});
currentPage.value = options.value.page;
const getAllThietBiSuaChua = async () => {
  const result = await DeviceApi.getAllThietBiSuaChua(businessExecuteFilterDeviceFix.value);
  listThietBiSuaChua.value = result.data;
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
    title: "Tên thiết bị",
    key: "tenThietBiSuaChua",
    sortable: false,
  },
  {
    title: "Giá",
    key: "moTaLoi",
  },
  {
    title: "Thời gian nhận sửa",
    key: "thoiGianNhanSua",
  },
  {
    title: "Thời gian dự kiến",
    key: "thoiGianDuKien",
  },
  {
    title: "Thời gian thực tế",
    key: "thoiGianThucTe",
  },
  {
    title: "Ghi chú",
    key: "ghiChuCuaKhachHang",
  },
  {
    title: "Trạng thái",
    key: "status"
  },
];
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
  return Math.ceil((valueInvoice * 1.0) / valueItemPerPage);
});

const onClickButtonSubmit = async () => {
  try {
    loading.value = false;
    console.log(props.deviceData)
    businessExecute.value.khachHangId = props.deviceData.dataResponseKhachHang.id;
    businessExecute.value.thietBiID = props.deviceData.id;
    const result = await DeviceApi.createThietBiSuaChua(businessExecute.value);
    if (result.status === 200) {
      loading.value = true;
      toast(result.message, {
        type: "success",
        transition: "flip",
        autoClose: 2000,
        theme: "dark",
        dangerouslyHTMLString: true,
      });

      await getAllThietBiSuaChua();
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
  await getAllThietBiSuaChua();
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
                <AppTextField
                  style="color: white"
                  v-model="businessExecute.moTaLoi"
                  label="Mô tả lỗi"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField
                  style="color: white"
                  v-model="businessExecute.ghiChuCuaKhachHang"
                  label="Ghi chú của khách hàng"
                />
              </VCol>

              <VCol cols="12">
                <VBtn type="submit" @click="onClickButtonSubmit"> Tạo thiết bị sửa chữa </VBtn>
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

        <div class="d-flex align-center flex-wrap" style="width: 120px">
          <!-- 👉 Search  -->
          <div class="invoice-list-filter" style="width: 100%">
            <AppTextField
              v-model="businessExecuteFilterDeviceFix.keyword"
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
          {{ formatDate(item.raw.thoiGianNhanSua) }}
        </template>
        <template #item.thoiGianDuKien="{ item }">
          {{ formatDate(item.raw.thoiGianDuKien) }}
        </template>
        <template #item.thoiGianThucTe="{ item }">
          {{ formatDate(item.raw.thoiGianThucTe) }}
        </template>
        <template #item.ghiChuCuaKhachHang="{ item }">
          {{ item.raw.ghiChuCuaKhachHang }}
        </template>
        <template #item.status="{ item }">
          {{ item.raw.status }}
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
