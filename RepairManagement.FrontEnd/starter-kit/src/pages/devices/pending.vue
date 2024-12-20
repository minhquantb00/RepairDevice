<script setup>
import {ref} from "vue"
import {DeviceApi} from "@/apis/device/deviceApi"
const props = defineProps({
  currentStep: {
    type: Number,
    required: false,
  },
  checkoutData: {
    type: null,
    required: true,
  },
})

const emit = defineEmits([
  'update:currentStep',
  'update:checkout-data',
])
const listDevice = ref([]);

const getAllListDevice = async () => {
  const result =await DeviceApi.getPhanCongCongViecChoXuLy();
  listDevice.value = result.data
}

const checkoutCartDataLocal = ref(props.checkoutData)

const removeItem = item => {
  listDevice = listDevice.filter(i => i.id !== item.id)
}

//  cart total
const totalCost = computed(() => {
  return checkoutCartDataLocal.value.orderAmount = checkoutCartDataLocal.value.cartItems.reduce((acc, item) => {
    return acc + item.price * item.quantity
  }, 0)
})

const updateCartData = () => {
  emit('update:checkout-data', checkoutCartDataLocal.value)
}

const nextStep = () => {
  updateCartData()
  emit('update:currentStep', props.currentStep ? props.currentStep + 1 : 1)
}

watch(() => props.currentStep, updateCartData)
onMounted(async () => {
  await getAllListDevice();
})
</script>

<template>
  <VRow >
    <VCol
      cols="12"
      md="8"
    >

      <h6 class="text-h6 my-4">
        Các thiết bị của bạn ({{ listDevice.length }} thiết bị)
      </h6>

      <!-- 👉 Cart items -->
      <div class="border rounded" v-if="listDevice.length > 0">
        <template
          v-for="(item) in listDevice"
          :key="item.id"
        >
          <div
            class="d-flex align-center gap-3 pa-5 position-relative flex-column flex-sm-row"
            :class="index ? 'border-t' : ''"
          >
            <IconBtn
              class="checkout-item-remove-btn"
              @click="removeItem(item)"
            >
              <VIcon
                size="20"
                icon="tabler-x"
              />
            </IconBtn>

            <div>
              <VImg
                width="140"
                :src="item.dataResponseThietBiSuaChua.anhThietBi"
              />
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

      <div v-else>Không có thiết bị nào trong mục này </div>

    </VCol>

    <!-- <VCol
      cols="12"
      md="4"
      style="margin-top: 53px"
    >
      <VCard
        flat
        variant="outlined"
      >

        <VDivider />

        <VCardText>
          <h6 class="text-base font-weight-medium mb-3">
            Chi phí linh kiện sửa chữa dự kiến
          </h6>

          <div class="text-high-emphasis">
            <div class="d-flex justify-space-between mb-2">
              <span>Thanh Ram 4gb</span>
              <span>120.000đ</span>
            </div>

            <div class="d-flex justify-space-between mb-2">
              <span>SSD</span>
              <span>120.000đ</span>
            </div>

            <div class="d-flex justify-space-between">
              <span>Vận chuyển</span>

              <div>
                <span class="text-decoration-line-through text-disabled me-2">50.000đ</span>
                <VChip
                  label
                  color="success"
                >
                  Free
                </VChip>
              </div>
            </div>
          </div>
        </VCardText>

        <VDivider />

        <VCardText class="d-flex justify-space-between py-4">
          <h6 class="text-base font-weight-medium">
            Tổng tiền
          </h6>
          <h6 class="text-base font-weight-medium">
            240.000đ
          </h6>
        </VCardText>
      </VCard>

    </VCol> -->
  </VRow>
</template>

<style lang="scss" scoped>
.checkout-item-remove-btn {
  position: absolute;
  inset-block-start: 10px;
  inset-inline-end: 10px;
}
</style>
