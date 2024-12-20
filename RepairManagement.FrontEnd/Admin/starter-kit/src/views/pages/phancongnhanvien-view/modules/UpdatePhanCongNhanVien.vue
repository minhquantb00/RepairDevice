<script setup>
import { CustomerApi } from "@/apis/customer/customerApi";
import { DeviceApi } from "@/apis/device/deviceApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VForm } from 'vuetify/components/VForm';
const props = defineProps({
  phanCongCongViecData: {
    type: Object,
    required: false,
    default: () => ({
    }),
  },
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const status = ref(["ChuaSua", "DangSua", "HoanThanh"])
const refVForm = ref()
const loading = ref(false);
const businessExecute = ref({
  id: null,
  ghiChu: '',
  status: null,
});
const emit = defineEmits(["submit", "update:isDialogVisible"]);
const phanCongCongViecData = ref(structuredClone(toRaw(props.phanCongCongViecData)));

watch(props, () => {
  businessExecute.value = structuredClone(toRaw(businessExecute.value));
});

const onFormSubmit = () => {

  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
     onClickButtonSubmit();
  })
};

const onFormReset = () => {
  businessExecute.value = structuredClone(toRaw(businessExecute.value));
  emit("update:isDialogVisible", false);
};

const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};

const onClickButtonSubmit = async () => {
  try{
    loading.value = false;
    const formData = new FormData();
    formData.append("id", props.phanCongCongViecData.id);
    formData.append("ghiChu", businessExecute.value.ghiChu);
    formData.append("status", businessExecute.value.status)
    const result = await DeviceApi.updatePhanCongCongViec(formData);
    console.log(result);
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
        <VCardTitle class="text-h5 mb-3"> Cập nhật thông tin phân công </VCardTitle>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
            <!-- 👉 First Name -->
            <VCol cols="12">
              <AppTextField v-model="businessExecute.ghiChu" label="Ghi chú" />
            </VCol>

            <!-- 👉 Last Name -->
            <VCol cols="12">
              <AppSelect
                  ref="select"
                  v-model="businessExecute.status"
                  :items="status"
                  clearable
                  label="Chọn trạng thái"
                />
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
