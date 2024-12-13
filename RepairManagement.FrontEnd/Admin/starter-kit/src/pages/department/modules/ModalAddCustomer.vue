<script setup>
import { CustomerApi } from "@/apis/customer/customerApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VForm } from 'vuetify/components/VForm';
const props = defineProps({
  customerData: {
    type: Object,
    required: false,
    default: () => ({
      id: 0,
      hoVaTen: "",
      soDienThoai: "",
      email: "",
      diaChi: "",
      createTime: null,
    }),
  },
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const refVForm = ref()
const loading = ref(false);
const createCustomer = ref({
  hoVaTen: '',
  email: '',
  soDienThoai: '',
  diaChi: ''
});
const emit = defineEmits(["submit", "update:isDialogVisible"]);
const customerData = ref(structuredClone(toRaw(props.customerData)));

watch(props, () => {
  createCustomer.value = structuredClone(toRaw(createCustomer.value));
});

const onFormSubmit = () => {

  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
     onClickButtonSubmit();
  })
};

const onFormReset = () => {
  createCustomer.value = structuredClone(toRaw(createCustomer.value));
  emit("update:isDialogVisible", false);
};

const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};

const onClickButtonSubmit = async () => {
  try{
    loading.value = false;
    const result = await CustomerApi.createKhachHang(createCustomer.value);
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
        <VCardTitle class="text-h5 mb-3"> Thêm khách hàng </VCardTitle>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
            <!-- 👉 First Name -->
            <VCol cols="12">
              <AppTextField v-model="createCustomer.hoVaTen" label="Họ và tên" />
            </VCol>

            <!-- 👉 Last Name -->
            <VCol cols="12">
              <AppTextField v-model="createCustomer.soDienThoai" label="Số điện thoại" />
            </VCol>
            <VCol cols="12">
              <AppTextField v-model="createCustomer.email" label="Email" />
            </VCol>
            <VCol cols="12">
              <AppTextField v-model="createCustomer.diaChi" label="Địa chỉ" />
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
