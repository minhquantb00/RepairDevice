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
  userData: {
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
const listDevice = ref([]);
const currentPage = ref(1);
const businessExecuteFilterDevice = ref({
  tenThietBi: "",
  loaiThietBiId: null,
  khachHangId: null
});
const pageSize = ref(10);
const pageNumber = ref(1);
const smsVerificationNumber = ref("+1(968) 819-2547");
const isTwoFactorDialogOpen = ref(false);
const status = ref(["DangHoatDong", "DangHong", "DaSuaXong"]);
const businessExecute = ref({
  tenThietBi: "",
  imageUrl: null,
  loaiThietBiId: null,
  gia: 0,
  moTa: "",
  status: null,
  khachHangId: null
});
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});
currentPage.value = options.value.page;
const handleFileUpload = (event) => {
  selectedFile.value = event.target.files[0];
};
const listDeviceType = ref([]);
const getAllDeviceType = async () => {
  const result = await DeviceApi.getAllLoaiThietBis();
  listDeviceType.value = result.data;
};
const getAllDevice = async () => {
  businessExecuteFilterDevice.value.khachHangId = props.userData.id
  const result = await DeviceApi.getAllThietBis(businessExecuteFilterDevice.value);
  listDevice.value = result.data;
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
    await getAllDevice(); // Gọi lại API để lấy dữ liệu trang mới
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
const formatCurrency = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value);
}

const onClickButtonSubmit = async () => {
  try {
    loading.value = false;
    const formData = new FormData();
    formData.append("tenThietBi", businessExecute.value.tenThietBi);
    formData.append("gia", businessExecute.value.gia);
    formData.append("moTa", businessExecute.value.moTa);
    formData.append("loaiThietBiId", businessExecute.value.loaiThietBiId);
    formData.append("status", businessExecute.value.status);
    formData.append("khachHangId", props.userData.id || null);

    if (selectedFile.value) {
      formData.append("imageUrl", selectedFile.value);
    }
    const result = await DeviceApi.createThietBi(formData);
    if (result.status === 200) {
      loading.value = true;
      toast(result.message, {
        type: "success",
        transition: "flip",
        autoClose: 2000,
        theme: "dark",
        dangerouslyHTMLString: true,
      });

      await getAllDevice(businessExecuteFilterDevice.value);
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

onMounted(async () => {
  await getAllDeviceType();
  await getAllDevice();
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
                  v-model="businessExecute.tenThietBi"
                  label="Tên thiết bị"
                />
              </VCol>
              <VCol cols="12" md="6">
                <VLabel
                  style="
                    font-size: 13px;
                    color: rgba(
                      var(--v-theme-on-background),
                      var(--v-high-emphasis-opacity)
                    ) !important;
                    margin-bottom: 4px;
                  "
                  >Ảnh thiết bị</VLabel
                >
                <VFileInput
                  accept="image/*"
                  v-model="businessExecute.imageUrl"
                  @change="handleFileUpload"
                  label="Ảnh thiết bị"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField
                  style="color: white"
                  v-model="businessExecute.moTa"
                  label="Mô tả"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField
                  style="color: white"
                  v-model="businessExecute.gia"
                  type="number"
                  label="Giá"
                />
              </VCol>

              <VCol cols="12" md="6">
                <AppSelect
                  ref="select"
                  v-model="businessExecute.loaiThietBiId"
                  item-title="name"
                  item-value="id"
                  :items="listDeviceType"
                  clearable
                  label="Chọn loại thiết bị"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppSelect
                  ref="select"
                  v-model="businessExecute.status"
                  :items="status"
                  clearable
                  label="Chọn trạng thái"
                />
              </VCol>

              <VCol cols="12">
                <VBtn type="submit" @click="onClickButtonSubmit"> Tạo thiết bị </VBtn>
              </VCol>
            </VRow>
          </VForm>
        </VCardText>
      </VCard>
    </VCol>

    <VCol cols="12">
      <!-- 👉 Recent devices -->
      <VCard title="Danh sách thiết bị">
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
              v-model="businessExecuteFilterDevice.tenThietBi"
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
          {{ item.raw.dataResponseKhachHang != null && item.raw.dataResponseKhachHang != undefined ? item.raw.dataResponseKhachHang.hoVaTen : "" }}
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
