<script setup>
import { useUserListStore } from "@/views/pages/customer-view/UseUserListStore";
import DeviceBioPanel from "@/views/pages/device-view/DeviceBioPanel.vue";
import DeviceTabSecurity from "@/views/pages/device-view/DeviceTabSecurity.vue";
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
const device = ref({})
const emit = defineEmits(["submit", "update:isDialogVisible"]);
const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};
const tabs = [
  {
    icon: "tabler-device-desktop-check",
    title: "Thông tin sửa chữa",
  },
];

const getDeviceById = async () => {
  const result = await DeviceApi.getThietBiById(props.dataId);
  device.value = result.data;
}
watch(
  () => props.isDialogVisible,
  async (newValue) => {
    if (newValue) {
      await getDeviceById();
    }
  }
);
onMounted(async () => {
  await getDeviceById();
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
          <DeviceBioPanel :device-data="device" />
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
              <DeviceTabSecurity  :device-data="device"/>
            </VWindowItem>

          </VWindow>
        </VCol>
      </VRow>
    </VCard>
  </VDialog>
</template>
