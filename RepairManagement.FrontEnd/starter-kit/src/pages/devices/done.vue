<script setup>
import { ref } from "vue";
import { DeviceApi } from "@/apis/device/deviceApi";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
const props = defineProps({
  currentStep: {
    type: Number,
    required: false,
  },
  checkoutData: {
    type: null,
    required: true,
  },
});
const userInfo = JSON.parse(localStorage.getItem("userInfo"));
const loading = ref(false);
const dataLinhKien = ref({});
const businessExecuteBill = ref({
  createChiTietHoaDons: [],
});
const businessExecuteBillDetail = ref({
  thietBiSuaChuaId: null,
});
const businessExecuteVnPay = ref({
  billID: null,
});
const emit = defineEmits(["update:currentStep", "update:checkout-data"]);
const listDevice = ref([]);
const getDataLinhKien = async () => {
  const result = await DeviceApi.getDataLinhKienByNguoiDung(userInfo.Id);
dataLinhKien.value = result.data;
}
const getAllListDevice = async () => {
  const result = await DeviceApi.getPhanCongCongViecDaHoanThanh();
  listDevice.value = result.data;
};
const createBill = async () => {
  loading.value = true;
  if (!userInfo) {
    toast("Bạn cần phải đăng nhập trước", {
      type: "error",
      transition: "flip",
      theme: "dark",
      autoClose: 1500,
      dangerouslyHTMLString: true,
    });
    router.push("/login");
    return;
  }
  if (listDevice.value == null || listDevice == undefined) {
    toast("Cần có thiết bị cần thanh toán", {
      type: "error",
      transition: "flip",
      theme: "dark",
      autoClose: 1500,
      dangerouslyHTMLString: true,
    });
    return;
  }
  console.log(listDevice.value);
  listDevice.value.forEach((element) => {
    console.log(element);
    businessExecuteBillDetail.value.thietBiSuaChuaId =
      element.dataResponseThietBiSuaChua.id;
    businessExecuteBill.value.createChiTietHoaDons.push(businessExecuteBillDetail.value);
  });
  const result = await DeviceApi.createHoaDon(businessExecuteBill.value);

  if (result.status === 200) {
    toast("Tạo hóa đơn thành công", {
      type: "success",
      transition: "flip",
      autoClose: 1500,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    console.log(result);
    const bill = result.data;
    console.log(bill.id);

    if (bill.billStatus == "PAID") {
      toast("Hóa đơn đã được thanh toán trước đó", {
        type: "error",
        transition: "flip",
        theme: "dark",
        autoClose: 1500,
        dangerouslyHTMLString: true,
      });
    }
    businessExecuteVnPay.value.billID = bill.id;
    console.log(businessExecuteVnPay.value.billID);
    const dataUrl = await DeviceApi.createVnPayUrl(bill.id);
    const url = dataUrl.data;

    if (dataUrl && url) {
      window.location.href = url;
    } else {
      alert("Không nhận được link thanh toán. Vui lòng thử lại.");
    }
  } else {
    toast(result.message, {
      type: "error",
      transition: "flip",
      theme: "dark",
      autoClose: 1500,
      dangerouslyHTMLString: true,
    });
  }
  loading.value = false;
};
const checkoutCartDataLocal = ref(props.checkoutData);

const removeItem = (item) => {
  listDevice = listDevice.filter((i) => i.id !== item.id);
};

//  cart total
const totalCost = computed(() => {
  return (checkoutCartDataLocal.value.orderAmount = checkoutCartDataLocal.value.cartItems.reduce(
    (acc, item) => {
      return acc + item.price * item.quantity;
    },
    0
  ));
});

const updateCartData = () => {
  emit("update:checkout-data", checkoutCartDataLocal.value);
};
const formatCurrency = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value);
}
const nextStep = () => {
  updateCartData();
  emit("update:currentStep", props.currentStep ? props.currentStep + 1 : 1);
};
const printInvoice = () => {
  const printContent = document.querySelector("#printSection");
  if (!printContent) {
    console.error("Không tìm thấy nội dung để in");
    return;
  }

  const printWindow = window.open("", "_blank");
  if (!printWindow) {
    console.error("Không thể mở cửa sổ in");
    return;
  }

  // printWindow.document.open();
  printWindow.document.write(`
    <html>
      <head>
        <title>Hóa đơn</title>
        <style>
          body {
            font-family: Arial, sans-serif;
            margin: 20px;
          }
          .text-primary {
            color: #007bff;
          }
          .text-high-emphasis {
            font-weight: bold;
          }
          .no-print {
            display: none !important;
          }
        </style>
      </head>
      <body>
        ${printContent.innerHTML}
      </body>
    </html>
  `);
  printWindow.document.close();

  printWindow.onload = () => {
    printWindow.focus();
    printWindow.print();
    setTimeout(() => {
      printWindow.close();
    }, 1000); // Đóng cửa sổ sau 1 giây
  };
};


watch(() => props.currentStep, updateCartData);
onMounted(async () => {
  await getAllListDevice();
  console.log(listDevice.value);
  if(userInfo && listDevice.value != null && listDevice.value != undefined && listDevice.value.length != 0){
    await getDataLinhKien();
  }
});
</script>

<template>
  <VRow id="printSection">
    <VCol cols="12" md="8" >
      <h6 class="text-h6 my-4">
        Các thiết bị của bạn ({{ listDevice.length }} thiết bị)
      </h6>

      <!-- 👉 Cart items -->
      <div class="border rounded" v-if="listDevice.length > 0">
        <template v-for="item in listDevice" :key="item.id">
          <div
            class="d-flex align-center gap-3 pa-5 position-relative flex-column flex-sm-row"
            :class="index ? 'border-t' : ''"
          >
            <IconBtn class="checkout-item-remove-btn no-print" @click="removeItem(item)">
              <VIcon size="20" icon="tabler-x" />
            </IconBtn>

            <div>
              <VImg width="140" :src="item.dataResponseThietBiSuaChua.anhThietBi" />
            </div>

            <div
              class="d-flex w-100"
              :class="$vuetify.display.width <= 700 ? 'flex-column' : 'flex-row'"
            >
              <div>
                <h6 class="text-base font-weight-regular mb-4">
                  {{ item.dataResponseThietBiSuaChua.tenThietBiSuaChua }}
                </h6>
                <div class="d-flex align-center text-no-wrap gap-2 text-base">
                  <span class="text-disabled">Phân công nhân viên:</span>
                  <span class="text-primary">{{ item.nhanVien.hoVaTen }}</span>
                </div>
              </div>

              <VSpacer />
            </div>
          </div>
        </template>
      </div>

      <div v-else>Không có thiết bị nào trong mục này</div>
    </VCol>
    <VCol cols="12" md="4" style="margin-top: 53px">
      <VCard flat variant="outlined">
        <!-- 👉 Price details -->
        <VCardText>
          <h6 class="text-base font-weight-medium mb-5">
            Chi phí sửa chữa thiết bị thực tế
          </h6>

          <div>
          <div class="text-high-emphasis" v-for="item in dataLinhKien.dataResponseLinhKiens" :key="item.id">
            <div class="d-flex justify-space-between mb-2">
              <span style="margin-right: 4px">{{item.tenLinhKien}}</span>
              <span>{{formatCurrency(item.giaBan)}}</span>
            </div>
          </div>
          </div>
        </VCardText>

        <VDivider />

        <VCardText class="d-flex justify-space-between py-4">
          <h6 class="text-base font-weight-medium">Tổng tiền</h6>
          <h6 class="text-base font-weight-medium">{{(dataLinhKien.tongTien == 0 || dataLinhKien.tongTien == undefined) ? formatCurrency(0) : formatCurrency(dataLinhKien.tongTien)}}</h6>
        </VCardText>
      </VCard>
      <VBtn
              block
              variant="tonal"
              color="secondary"
              class="mb-2 mt-4 no-print"
              @click="printInvoice"
            >
              In hóa đơn
            </VBtn>
      <VBtn block class="mt-4 no-print" @click="createBill"> Thanh toán </VBtn>
    </VCol>
  </VRow>
</template>

<style scoped>

</style>
