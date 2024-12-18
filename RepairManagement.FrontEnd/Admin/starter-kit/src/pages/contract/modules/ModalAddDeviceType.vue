<script setup>
import { DeviceApi } from "@/apis/device/deviceApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import { VForm } from 'vuetify/components/VForm';
import "vue3-toastify/dist/index.css";
const props = defineProps({
  deviceTypeData: {
    type: Object,
    required: false,
    default: () => ({
      id: 0
    }),
  },
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const refVForm = ref()
const listDeviceType = ref([]);
const getAllDeviceType = async () => {
  const result = await DeviceApi.getAllLoaiThietBis();
  listDeviceType.value = result.data;
}
const loading = ref(false);
const selectedFile = ref(null);
const createDeviceType = ref({
  name: '',
  imageUrl: null
});
const emit = defineEmits(["submit", "update:isDialogVisible"]);
const handleFileUpload = (event) => {
  selectedFile.value = event.target.files[0];
};
watch(props, () => {
  createDeviceType.value = structuredClone(toRaw(createDeviceType.value));
});

const onFormSubmit = () => {

  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
     onClickButtonSubmit();
  })
};
const resetFormData = () => {
  createDeviceType.value = structuredClone(toRaw(createDeviceType.value));
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
  try{
    loading.value = false;
    const formData = new FormData();
    formData.append("name", createDeviceType.value.name);
    if (selectedFile.value) {
      formData.append("imageUrl", selectedFile.value);
    }
    const result = await DeviceApi.createLoaiThietBi(formData);
    if(result.status === 200){
      loading.value = true;
      toast(result.message, {
      type: "success",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    emit("update:isDialogVisible", false);
      emit("submit");
    await getAllDeviceType();
    }
    else{
      loading.value = true
      toast(result.message, {
      type: "error",
      transition: "flip",
      theme: "dark",
      "autoClose": 2000,
      dangerouslyHTMLString: true,
    });
    }
    loading.value = false;
  }catch (error) {
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
}

onMounted(() => {
  resetFormData();
})
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
        <VCardTitle class="text-h5 mb-3"> Tạo loại thiết bị </VCardTitle>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
          <VCol cols="12">
              <VLabel style="font-size: 13px; color: black; margin-bottom: 4px"
                >Ảnh loại thiết bị</VLabel
              >
              <VFileInput accept="image/*" v-model="createDeviceType.imageUrl" @change="handleFileUpload" label="Ảnh loại thiết bị" />
            </VCol>

            <!-- 👉 First Name -->
            <VCol cols="4">
              <AppTextField v-model="createDeviceType.name" label="Tên loại thiết bị" />
            </VCol>

            <!-- 👉 Submit and Cancel -->
            <VCol cols="12" class="d-flex flex-wrap justify-center gap-4">
              <VBtn type="submit"> Xác nhận </VBtn>

              <VBtn color="secondary" variant="tonal" @click="onFormReset">
                Thoát
              </VBtn>
            </VCol>
          </VRow>
        </VForm>
      </VCardText>
    </VCard>
  </VDialog>
</template>
