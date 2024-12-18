<script setup>
import { useUserListStore } from "@/views/pages/customer-view/UseUserListStore";
import UserBioPanel from "@/views/pages/customer-view/UserBioPanel.vue";
import UserTabAccount from "@/views/pages/customer-view/UserTabAccount.vue";
import UserTabBillingsPlans from "@/views/pages/customer-view/UserTabBillingsPlans.vue";
import UserTabConnections from "@/views/pages/customer-view/UserTabConnections.vue";
import UserTabNotifications from "@/views/pages/customer-view/UserTabNotifications.vue";
import UserTabSecurity from "@/views/pages/customer-view/UserTabSecurity.vue";
import { CustomerApi } from "@/apis/customer/customerApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
const props = defineProps({
  dataId: Number,
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const userListStore = useUserListStore();
const route = useRoute();
const userData = ref();
const userTab = ref(null);
const isLoading = ref(false);
const customer = ref({})
const emit = defineEmits(["submit", "update:isDialogVisible"]);
const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};
const tabs = [
  {
    icon: "tabler-device-desktop-check",
    title: "Thiết bị",
  },
  {
    icon: "tabler-list-check",
    title: "Thông tin đặt lịch",
  },
  {
    icon: "tabler-history",
    title: "Lịch sử sửa chữa",
  },
];

const getCustomerById = async () => {
  const result = await CustomerApi.getKhachHangById(props.dataId);
  console.log(result);
  customer.value = result.data;
}
watch(
  () => props.isDialogVisible,
  async (newValue) => {
    if (newValue) {
      await getCustomerById();
    }
  }
);
onMounted(async () => {
  await getCustomerById();
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
          <UserBioPanel :user-data="customer" />
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
              <UserTabSecurity  :user-data="customer"/>
            </VWindowItem>

            <VWindowItem>
              <UserTabBillingsPlans :user-data="customer"/>
            </VWindowItem>

            <VWindowItem>
              <UserTabNotifications :user-data="customer"/>
            </VWindowItem>

          </VWindow>
        </VCol>
      </VRow>
    </VCard>
  </VDialog>
</template>
