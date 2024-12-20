<script setup>
import { DeviceApi } from "@/apis/device/deviceApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import { VForm } from "vuetify/components/VForm";
import "vue3-toastify/dist/index.css";
const props = defineProps({
  nhanVienData: {
    type: Object,
    required: false,
    default: () => ({
      id: 0,
    }),
  },
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const keyword = ref("");
const refVForm = ref();
const listNhanVien = ref([]);
const getAllUser = async () => {
  const result = await DeviceApi.getAllUser(keyword.value);
  listNhanVien.value = result.data;
};
const loading = ref(false);
const selectedFile = ref(null);
const isPasswordVisible = ref(false);
const businessExecute = ref({
  email: "",
  hoVaTen: "",
  matKhau: "",
  soDienThoai: "",
});
const emit = defineEmits(["submit", "update:isDialogVisible"]);
watch(props, () => {
  businessExecute.value = structuredClone(toRaw(businessExecute.value));
});

const onFormSubmit = () => {
  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid) onClickButtonSubmit();
  });
};
const resetFormData = () => {
  businessExecute.value = structuredClone(toRaw(businessExecute.value));
  refVForm.value?.reset(); // Nếu bạn sử dụng VForm với validate, có thể reset form
};
const onFormReset = () => {
  // createContract.value = structuredClone(toRaw(createContract.value));
  resetFormData();
  emit("update:isDialogVisible", false);
};

const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};

const onClickButtonSubmit = async () => {
  try {
    loading.value = false;
    const result = await DeviceApi.createNhanVien(businessExecute.value);
    if (result.status === 200) {
      loading.value = true;
      toast(result.message, {
        type: "success",
        transition: "flip",
        autoClose: 2000,
        theme: "dark",
        dangerouslyHTMLString: true,
      });
      emit("update:isDialogVisible", false);
      emit("submit");
      await getAllUser();
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

onMounted(() => {
  resetFormData();
});
</script>

<template>
  <VDialog
    :width="$vuetify.display.smAndDown ? 'auto' : 677"
    :model-value="props.isDialogVisible"
    @update:model-value="dialogModelValueUpdate"
  >
    <!-- Dialog close btn -->
    <DialogCloseBtn @click="dialogModelValueUpdate(false)" />

    <VCard class="pa-sm-8 pa-5">
      <VCardItem class="text-center">
        <VCardTitle class="text-h5 mb-3"> Tạo thông tin nhân viên </VCardTitle>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
            <!-- 👉 First Name -->
            <VCol cols="12">
              <AppTextField v-model="businessExecute.email" label="Email" />
            </VCol>
            <VCol cols="12">
              <AppTextField
                v-model="businessExecute.matKhau"
                label="Mật khẩu"
                :loading="loading"
                :type="isPasswordVisible ? 'text' : 'password'"
                :append-inner-icon="isPasswordVisible ? 'tabler-eye-off' : 'tabler-eye'"
                :rules="[requiredValidator]"
                @click:append-inner="isPasswordVisible = !isPasswordVisible"
              />
            </VCol>
            <VCol cols="12">
              <AppTextField v-model="businessExecute.hoVaTen" label="Họ và tên" />
            </VCol>
            <VCol cols="12">
              <AppTextField v-model="businessExecute.soDienThoai" label="Số điện thoại" />
            </VCol>

            <!-- 👉 Submit and Cancel -->
            <VCol cols="12" class="d-flex flex-wrap justify-center gap-4">
              <VBtn type="submit"> Xác nhận </VBtn>

              <VBtn color="secondary" variant="tonal" @click="onFormReset"> Thoát </VBtn>
            </VCol>
          </VRow>
        </VForm>
      </VCardText>
    </VCard>
  </VDialog>
</template>
