<script setup>
import { DeviceApi } from "@/apis/device/deviceApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VForm } from "vuetify/components/VForm";
const props = defineProps({
  dataId: Number,
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const refVForm = ref();
const selectedFile = ref(null);
const loading = ref(false);
const listLinhKien = ref([]);
const businessExecute = ref({
  id: null,
  tenLinhKien: "",
  loaiLinhKien: "",
  giaBan: 0,
  moTa: "",
  imageUrl: null,
});
const emit = defineEmits(["submit", "update:isDialogVisible"]);

const getAllLinhKien = async () => {
  const result = await DeviceApi.getAllLinhKien();
  listLinhKien.value = result.data;
};
const onFormSubmit = () => {
  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid) onClickButtonSubmit();
  });
};

const onFormReset = () => {
  businessExecute.value = structuredClone(toRaw(businessExecute.value));
  emit("update:isDialogVisible", false);
};

const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};

const onClickButtonSubmit = async () => {
  try {
    loading.value = false;
    const formData = new FormData();
    formData.append("id", props.dataId);
    formData.append("tenLinhKien", businessExecute.value.tenLinhKien);
    formData.append("loaiLinhkien", businessExecute.value.loaiLinhKien);
    formData.append("giaBan", businessExecute.value.giaBan);
    formData.append("moTa", businessExecute.value.moTa);
    if (selectedFile.value) {
      formData.append("imageUrl", selectedFile.value);
    }
    const result = await DeviceApi.updateLinhKien(formData);
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
      await getAllLinhKien();
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
        <VCardTitle class="text-h5 mb-3"> Cập nhật linh kiện </VCardTitle>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
            <!-- 👉 First Name -->
            <VCol cols="12">
              <AppTextField v-model="businessExecute.tenLinhKien" label="Tên linh kiện" />
            </VCol>

            <!-- 👉 Last Name -->
            <VCol cols="12">
              <AppTextField
                v-model="businessExecute.loaiLinhKien"
                label="Loại linh kiện"
              />
            </VCol>
            <VCol cols="12">
              <AppTextField v-model="businessExecute.giaBan" label="Giá bán" />
            </VCol>
            <VCol cols="12">
              <AppTextField v-model="businessExecute.moTa" label="Mô tả" />
            </VCol>
            <VCol cols="12">
              <VFileInput
                accept="image/*"
                v-model="businessExecute.imageUrl"
                @change="handleFileUpload"
                label="Ảnh loại thiết bị"
              />
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
