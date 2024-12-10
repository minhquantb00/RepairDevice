<script setup>
const firstName = ref("");
const lastName = ref("");
const country = ref();
const birthDate = ref("");
const phoneNo = ref();

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
const time = ref(null)
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
</script>

<template>
  <VCard flat>
    <VCardText>
      <VWindow class="disable-tab-transition">
        <VWindowItem value="personal-info">
          <VForm class="mt-2">
            <VRow>
              <VCol md="6" cols="12">
                <AppTextField v-model="firstName" label="Họ và tên" />
              </VCol>
              <VCol cols="12" md="6">
                <AppTextField v-model="phoneNo" type="number" label="Số điện thoại" />
              </VCol>

              <VCol cols="12" md="6">
                <AppSelect v-model="country" :items="countryList" label="Địa chỉ" />
              </VCol>

              <VCol cols="12" md="6">
                <AppSelect
                  v-model="languages"
                  :items="languageList"
                  multiple
                  chips
                  clearable
                  label="Chọn dịch vụ"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppDateTimePicker
                  v-model="time"
                  label="Thời gian sửa"
                  placeholder="HH:MM"
                  :config="{ enableTime: true, noCalendar: true, dateFormat: 'H:i' }"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppDateTimePicker
                  v-model="birthDate"
                  label="Ngày sửa"
                  placeholder="YYYY-MM-DD"
                />
              </VCol>
              <VCol cols="12" md="6">
                <AppSelect
                  v-model="languages"
                  :items="languageList"
                  multiple
                  chips
                  clearable
                  label="Chọn chi nhánh"
                />
              </VCol>
              <VCol md="6" cols="12">
                <AppTextField v-model="lastName" label="Mô tả lỗi" />
              </VCol>
            </VRow>
          </VForm>
        </VWindowItem>
      </VWindow>
    </VCardText>

    <VDivider />

    <VCardText class="d-flex gap-4">
      <VBtn>Đặt lịch</VBtn>
    </VCardText>
  </VCard>
</template>
