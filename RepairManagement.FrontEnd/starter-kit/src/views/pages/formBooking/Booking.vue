<script setup>
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { BookingApi } from "@/apis/booking/bookingApi";
import { ServiceApi } from "@/apis/service/serviceApi";
import { VForm } from "vuetify/components/VForm";

const router = useRouter();
const loading = ref(false);
const listService = ref([]);
const refVForm = ref();
const businessExecute = ref({
  hoVaTen: "",
  soDienThoai: "",
  email: "",
  diaChi: "",
  dichVuId: null,
  thoiGianDat: null,
  moTa: "",
  tenThietBi: "",
});
const getAllServices = async () => {
  const result = await ServiceApi.getAllServices();

  listService.value = result.data;
};

const countryList = [
  "USA",
  "Canada",
  "UK",
  "Denmark",
  "Germany",
  "Iceland",
  "Israel",
  "Mexico",
];

const languageList = [
  "English",
  "German",
  "French",
  "Spanish",
  "Portuguese",
  "Russian",
  "Korean",
];
const time = ref(null);
const username = ref("");
const email = ref("");
const password = ref("");
const cPassword = ref("");
const twitterLink = ref("");
const facebookLink = ref("");
const googlePlusLink = ref("");
const linkedInLink = ref("");
const instagramLink = ref("");
const quoraLink = ref("");
const languages = ref([]);
const isPasswordVisible = ref(false);
const isCPasswordVisible = ref(false);

const datLichSuaChua = async () => {
  loading.value = false;
  const result = await BookingApi.datLichSuaChua(businessExecute.value);
  if (result.status === 200) {
    toast(result.message, {
      type: "success",
      transition: "flip",
      autoClose: 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
  } else {
    toast(result.message, {
      type: "error",
      transition: "flip",
      theme: "dark",
      autoClose: 2000,
      dangerouslyHTMLString: true,
    });
    loading.value = false;
  }
};

const onSubmitForm = () => {
  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid) datLichSuaChua();
  });
};
onMounted(async () => {

  await getAllServices();
  console.log(listService.value)
});
</script>

<template>
  <VCard flat>
    <VCardText>
      <VWindow class="disable-tab-transition">
        <VWindowItem value="personal-info">
          <VForm ref="refVForm" @submit.prevent="onSubmitForm" class="mt-2">
            <VRow>
              <VCol md="6" cols="12">
                <AppTextField v-model="businessExecute.hoVaTen" label="Họ và tên" />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField
                  v-model="businessExecute.soDienThoai"
                  type="number"
                  label="Số điện thoại"
                />
              </VCol>

              <VCol cols="12" md="6">
                <AppTextField
                  v-model="businessExecute.diaChi"
                  type="text"
                  label="Địa chỉ"
                />
              </VCol>

              <VCol cols="12" md="6">
                <AppSelect
                ref="select"
                  v-model="businessExecute.dichVuId"
                  item-title="tenDichVu"
                  item-value="id"
                  :items="listService"
                  clearable
                  label="Chọn dịch vụ"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField v-model="businessExecute.email" type="text" label="Email" />
              </VCol>
              <VCol cols="12" md="6">
                <AppDateTimePicker
                  v-model="businessExecute.thoiGianDat"
                  label="Ngày sửa"
                  placeholder="YYYY-MM-DD"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField
                  v-model="businessExecute.tenThietBi"
                  type="text"
                  label="Tên thiết bị"
                />
              </VCol>
              <VCol md="6" cols="12">
                <AppTextField v-model="businessExecute.moTa" label="Mô tả lỗi" />
              </VCol>
            </VRow>
          </VForm>
        </VWindowItem>
      </VWindow>
    </VCardText>

    <VDivider />

    <VCardText class="d-flex gap-4">
      <VBtn block type="submit" @click="datLichSuaChua">Đặt lịch</VBtn>
    </VCardText>
  </VCard>
</template>
