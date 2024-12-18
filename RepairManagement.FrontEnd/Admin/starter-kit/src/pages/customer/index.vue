<script setup>
import { paginationMeta } from "@/@fake-db/utils";
import ConfirmDeleteDialog from "@/components/ConfirmDeleteDialog.vue";
import ModalAddCustomer from "@/pages/customer/modules/ModalAddCustomer.vue";
import ModalUpdateCustomer from "@/pages/customer/modules/ModalUpdateCustomer.vue";
import CustomerDetail from "./customer-detail.vue"
import { CustomerApi } from "@/apis/customer/customerApi";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";
import {useRouter} from "vue-router"
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const router = useRouter();
const props = defineProps({
  customerData: {
    type: Object,
    required: true,
  },
});
const instance = getCurrentInstance();
const isCustomerAddDialogVisible = ref(false);
const isCustomerUpdateDialogVisible = ref(false);
const isCustomerDetailDialogVisible = ref(false);
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});

const isLoading = ref(false);
const currentPage = ref(1);
const dataId = ref();
const customerId = ref();

const viewDetail = (id) => {
  router.push({
    path: '/customer-detail'
  })
}
currentPage.value = options.value.page;

// 👉 headers
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Họ và tên",
    key: "hoVaTen",
    sortable: false,
  },
  {
    title: "Số điện thoại",
    key: "soDienThoai",
  },
  {
    title: "Email",
    key: "email",
  },
  {
    title: "Địa chỉ",
    key: "diaChi",
  },
  {
    title: "Create time",
    key: "createTime",
  },
  {
    title: "Điểm",
    key: "diem"
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
];

const filterCustomer = ref({
  keyword: ''
});

const getAllCustomer = async (filter) => {
  const result = await CustomerApi.getAllKhachHang(filterCustomer.value);
  console.log(result)
  invoices.value = result.data;
  totalInvoices.value = result.length;
};

const paginatedData = computed(() => {
  const start = (options.value.page - 1) * options.value.itemsPerPage;
  const end = start + options.value.itemsPerPage;
  return invoices.value.slice(start, end);
});

const totalPages = computed(() => {
  return Math.ceil((invoices.value.length * 1.0) / options.value.itemsPerPage);
});

const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
};

const refreshData = async () => {
  await getAllCustomer(filterCustomer.value);
};

const openDialogUpdateCustomer = (id) => {
  isCustomerUpdateDialogVisible.value = true;
  dataId.value = id
}

const openDialogCustomerDetail = (id) => {
  isCustomerDetailDialogVisible.value = true;
  console.log(id);
  dataId.value = id
}

const onConfirmed = async () => {
  try{
    const result = await CustomerApi.deleteKhachHang(customerId.value);
    if(result.status === 200){
      toast(result.message, {
      type: "success",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    await getAllCustomer(filterCustomer.value);
    }
    else{
      toast("Xoá thất bại", {
      type: "error",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    }
  }catch(error){
    toast(error, {
      type: "error",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
  }
}

const onclickDeleteItem = (id) => {
  customerId.value = id;
  instance?.refs.deleteDialog.show();
}

watchEffect(async () => {
  await getAllCustomer(filterCustomer.value);
});

onMounted(async () => {
  await getAllCustomer(filterCustomer.value);
});
</script>

<template>
  <div>
    <VCard v-if="invoices" id="invoice-list">
      <VCardText class="d-flex align-center flex-wrap gap-4">
        <div class="me-3 d-flex gap-3">
          <AppSelect
            :model-value="options.itemsPerPage"
            :items="[
              { value: 5, title: '5' },
              { value: 10, title: '10' },
              { value: 25, title: '25' },
              { value: 50, title: '50' },
              { value: 100, title: '100' },
              { value: -1, title: 'All' },
            ]"
            style="width: 6.25rem"
            @update:model-value="options.itemsPerPage = parseInt($event, 10)"
          />
          <!-- 👉 Create invoice -->
          <VBtn
            prepend-icon="tabler-plus"
            variant="elevated"
            class="me-4"
            @click="isCustomerAddDialogVisible = true"
          >
            Tạo khách hàng
          </VBtn>
        </div>

        <VSpacer />

        <div class="d-flex align-center flex-wrap gap-4">
          <!-- 👉 Search  -->
          <div class="invoice-list-filter">
            <AppTextField
              v-model="filterCustomer.keyword"
              placeholder="Tìm kiếm..."
              density="compact"
            />
          </div>

        </div>
      </VCardText>

      <VDivider />

      <!-- SECTION Datatable -->
      <VDataTableServer
        v-model="selectedRows"
        v-model:items-per-page="options.itemsPerPage"
        v-model:page="options.page"
        :loading="isLoading"
        :headers="headers"
        :items="paginatedData"
        class="text-no-wrap"
        @update:options="options = $event"
      >
        <!-- Trending Header -->
        <template #item.stt="{ index }">
          {{ (options.page - 1) * options.itemsPerPage + index + 1 }}
        </template>

        <template #item.hoVaTen="{ item }">
          {{ item.raw.hoVaTen }}
        </template>

        <template #item.soDienThoai="{ item }">
          {{ item.raw.soDienThoai }}
        </template>
        <template #item.email="{ item }">
          {{ item.raw.email }}
        </template>
        <template #item.diaChi="{ item }">
          {{ item.raw.diaChi }}
        </template>
        <template #item.createTime="{ item }">
          {{ formatDate(item.raw.createTime) }}
        </template>


        <!-- Actions -->
        <template #item.actions="{ item }">
          <IconBtn @click="openDialogUpdateCustomer(item.raw.id)">
            <VIcon icon="tabler-settings-check" />
          </IconBtn>

          <IconBtn @click="onclickDeleteItem(item.raw.id)">
            <VIcon icon="tabler-trash" />
          </IconBtn>

          <IconBtn @click="openDialogCustomerDetail(item.raw.id)">
            <VIcon icon="tabler-eye-spark" />
          </IconBtn>
        </template>

        <!-- pagination -->

        <template #bottom>
          <VDivider />
          <div
            class="d-flex align-center justify-sm-space-between justify-center flex-wrap gap-3 pa-5 pt-3"
          >
            <p class="text-sm text-disabled mb-0">
              {{ paginationMeta(options, totalInvoices) }}
            </p>

            <VPagination
              v-model="options.page"
              :length="totalPages"
              :total-visible="$vuetify.display.xs ? 1 : totalPages"
              rounded="circle"
            >
              <template #prev="slotProps">
                <VBtn
                  variant="tonal"
                  color="default"
                  v-bind="slotProps"
                  :icon="false"
                >
                  Previous
                </VBtn>
              </template>

              <template #next="slotProps">
                <VBtn
                  variant="tonal"
                  color="default"
                  v-bind="slotProps"
                  :icon="false"
                >
                  Next
                </VBtn>
              </template>
            </VPagination>
          </div>
        </template>
      </VDataTableServer>
      <!-- !SECTION -->
    </VCard>
    <ModalAddCustomer
      v-model:isDialogVisible="isCustomerAddDialogVisible"
      :customerData="props.customerData"
      @submit="refreshData"
    />
    <ModalUpdateCustomer
      v-model:isDialogVisible="isCustomerUpdateDialogVisible"
      :dataId="dataId"
      @submit="refreshData"
    />
    <CustomerDetail
      v-model:isDialogVisible="isCustomerDetailDialogVisible"
      :dataId="dataId"
    />
    <ConfirmDeleteDialog
      @onConfirmed="onConfirmed"
      title="Xác nhận"
      content="Bạn có muốn xóa không"
      ref="deleteDialog"
    ></ConfirmDeleteDialog>
  </div>
</template>

<style lang="scss">
#invoice-list {
  .invoice-list-actions {
    inline-size: 8rem;
  }

  .invoice-list-filter {
    inline-size: 12rem;
  }
}
</style>
