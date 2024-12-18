<script setup>
import { useUserListStore } from "@/views/pages/customer-view/UseUserListStore";
import PhanCongNhanVienBioPanel from "@/views/pages/phancongnhanvien-view/PhanCongNhanVienBioPanel.vue";
import PhanCongNhanVienTabSecurity from "@/views/pages/phancongnhanvien-view/PhanCongNhanVienTabSecurity.vue";
import { CustomerApi } from "@/apis/customer/customerApi";
import { onMounted } from "vue";
import {DeviceApi} from "@/apis/device/deviceApi"
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
const props = defineProps({
  dataId: Number,
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const route = useRoute();
const deviceData = ref();
const userTab = ref(null);
const isLoading = ref(false);
const phanCongCongViecData = ref({})
const emit = defineEmits(["submit", "update:isDialogVisible"]);
const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};
const tabs = [
  {
    icon: "tabler-device-desktop-check",
    title: "Thông tin linh kiện sửa chữa",
  },
];

const getPhanCongCongViecById = async () => {
  const result = await DeviceApi.getPhanCongCongViecById(props.dataId);
  console.log(result.data);
  phanCongCongViecData.value = result.data;
}
watch(
  () => props.isDialogVisible,
  async (newValue) => {
    if (newValue) {
      await getPhanCongCongViecById();
    }
  }
);
onMounted(async () => {
  await getPhanCongCongViecById();
})
</script>

<template>
  <VDialog
    :width="$vuetify.display.smAndDown ? 'auto' : 1200"
    height="2000px"
    :model-value="props.isDialogVisible"
    @update:model-value="dialogModelValueUpdate"
  >
    <DialogCloseBtn @click="dialogModelValueUpdate(false)" />
    <VCard>
      <VRow style="padding: 20px 40px">
        <VCol cols="9" md="5" lg="4">
          <PhanCongNhanVienBioPanel :phanCongCongViecData="phanCongCongViecData" />
        </VCol>

        <VCol cols="9" md="7" lg="8">
          <VTabs v-model="userTab" class="v-tabs-pill">
            <VTab v-for="tab in tabs" :key="tab.icon">
              <VIcon :size="18" :icon="tab.icon" class="me-1" />
              <span>{{ tab.title }}</span>
            </VTab>
          </VTabs>

          <VWindow v-model="userTab" class="mt-6 disable-tab-transition" :touch="false">

            <VWindowItem>
              <PhanCongNhanVienTabSecurity  :phanCongCongViecData="phanCongCongViecData"/>
            </VWindowItem>

          </VWindow>
        </VCol>
      </VRow>
    </VCard>
  </VDialog>
</template>
