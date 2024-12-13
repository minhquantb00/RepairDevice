<script setup>
import avatar1 from "@images/avatars/avatar-1.png";
import avatar2 from "@images/avatars/avatar-2.png";
import avatar3 from "@images/avatars/avatar-3.png";
import avatar4 from "@images/avatars/avatar-4.png";
import eCommerce2 from "@images/eCommerce/2.png";
import pages1 from "@images/pages/1.png";
import pages2 from "@images/pages/2.png";
import pages3 from "@images/pages/3.png";
import pages5 from "@images/pages/5.jpg";
import pages6 from "@images/pages/6.jpg";
import { useRouter, RouterLink } from "vue-router";
import { ref, onMounted } from "vue";
import {DeviceApi} from "@/apis/device/deviceApi"
import ModalAddDeviceType from "./modules/ModalAddDeviceType.vue"
const avatars = [avatar1, avatar2, avatar3, avatar4];

const props = defineProps({
  deviceTypeData: {
    type: Object,
    required: true,
  },
});
const isDeviceTypeAddDialogVisible = ref(false);
const isDeviceTypeDetailDialogVisible = ref(false);
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const currentPage = ref(1);

const router = useRouter();
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});
currentPage.value = options.value.page;
const loading = ref(false);
const pageSize = ref(10);
const pageNumber = ref(1);
const truncateText = (text, maxLength) => {
  return text && text.length > maxLength ? text.slice(0, maxLength) + "..." : text;
};
const listDeviceType = ref([]);
const isCardDetailsVisible = ref(false);

const getAllDeviceType = async () => {
  const result = await DeviceApi.getAllLoaiThietBis();
  listDeviceType.value = result.data;
  invoices.value = result.data;
  totalInvoices.value = result.data.length;
}
watch(() => options.value.page, async (newPage) => {
  pageNumber.value = newPage; // Cập nhật số trang hiện tại
  await getAllDeviceType(); // Gọi lại API để lấy dữ liệu trang mới
});


const paginatedData = computed(() => {
  const start = (options.value.page - 1) * options.value.itemsPerPage;
  const end = start + options.value.itemsPerPage;
  return invoices.value.slice(start, end);
});
const totalPages = computed(() => {
  const valueInvoice  = Number(invoices.value);
  const valueItemPerPage = Number(options.value.itemsPerPage);
  return Math.ceil((valueInvoice * 1.0) / valueItemPerPage);
});
onMounted(async () => {
  await getAllDeviceType();
})
</script>

<template>
  <div>
    <div style="margin: 40px 0" class="text-right">
      <VBtn @click="isDeviceTypeAddDialogVisible = true">Thêm loại thiết bị</VBtn>
    </div>

    <VRow>
      <VCol cols="4" v-for="item in listDeviceType" :key="item.id">
        <VCard>
          <VImg :src="item.imageUrl" height="200px" width="100%" />


          <VCardText class="position-relative">
            <div class="d-flex justify-space-between flex-wrap pt-2">
              <div class="me-2 mb-2">
                <VCardTitle class="pa-0">
                  {{item.name}}
                </VCardTitle>
              </div>
            </div>
          </VCardText>
        </VCard>
      </VCol>
    </VRow>
    <div>
      <VPagination
        class="mt-4"
        v-model="options.page"
        :length="totalPages"
        :total-visible="totalPages"
        rounded="circle"
      >
        <template #prev="slotProps">
          <VBtn variant="tonal" color="default" v-bind="slotProps" :icon="false">
            Previous
          </VBtn>
        </template>
s
        <template #next="slotProps">
          <VBtn variant="tonal" color="default" v-bind="slotProps" :icon="false">
            Next
          </VBtn>
        </template>
      </VPagination>
    </div>
    <ModalAddDeviceType
      v-model:isDialogVisible="isDeviceTypeAddDialogVisible"
      :deviceTypeData="props.deviceTypeData"
    />
  </div>
</template>

<style lang="scss" scoped>
.avatar-center {
  position: absolute;
  border: 3px solid rgb(var(--v-theme-surface));
  inset-block-start: -2rem;
  inset-inline-start: 1rem;
}

// membership pricing
.member-pricing-bg {
  position: relative;
  background-color: rgba(var(--v-theme-on-surface), var(--v-hover-opacity));
}

.membership-pricing {
  sup {
    inset-block-start: 9px;
  }
}

.v-btn {
  transform: none;
}
</style>
