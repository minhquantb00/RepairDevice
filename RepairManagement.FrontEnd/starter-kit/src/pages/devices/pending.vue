<script setup>
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

const checkoutCartDataLocal = ref(props.checkoutData)

const removeItem = item => {
  checkoutCartDataLocal.value.cartItems = checkoutCartDataLocal.value.cartItems.filter(i => i.id !== item.id)
  console.log(checkoutCartDataLocal.value.cartItems)
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
</script>

<template>
  <VRow v-if="checkoutCartDataLocal">
    <VCol
      cols="12"
      md="8"
    >

      <h6 class="text-h6 my-4">
        Các thiết bị của bạn ({{ checkoutCartDataLocal.cartItems.length }} thiết bị)
      </h6>

      <!-- 👉 Cart items -->
      <div class="border rounded">
        <template
          v-for="(item, index) in checkoutCartDataLocal.cartItems"
          :key="item.name"
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
                :src="item.image"
              />
            </div>

            <div
              class="d-flex w-100"
              :class="$vuetify.display.width <= 700 ? 'flex-column' : 'flex-row'"
            >
              <div>
                <h6 class="text-base font-weight-regular mb-4">
                  {{ item.name }}
                </h6>
                <div class="d-flex align-center text-no-wrap gap-2 text-base">
                  <span class="text-disabled">Phân công nhân viên:</span>
                  <span class="text-primary">{{ item.seller }}</span>
                  <VChip
                    :color="item.inStock ? 'success' : 'error'"
                    label
                  >
                    <span class="text-xs font-weight-medium">
                      {{ item.inStock ? 'In Stock' : 'Out of Stock' }}
                    </span>
                  </VChip>
                </div>

              </div>

              <VSpacer />
            </div>
          </div>
        </template>
      </div>

      <!-- 👉 Add more from wishlist -->
      <div class="d-flex align-center justify-space-between border rounded py-2 px-5 text-base mt-4">
        <a href="#">Xem tất cả</a>
        <VIcon
          icon="tabler-chevron-right"
          class="flip-in-rtl"
        />
      </div>
    </VCol>

    <VCol
      cols="12"
      md="4"
      style="margin-top: 53px"
    >
      <VCard
        flat
        variant="outlined"
      >

        <VDivider />

        <!-- 👉 Price details -->
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

      <VBtn
        block
        class="mt-4"
        @click="nextStep"
      >
        Bước kế tiếp
      </VBtn>
    </VCol>
  </VRow>
</template>

<style lang="scss" scoped>
.checkout-item-remove-btn {
  position: absolute;
  inset-block-start: 10px;
  inset-inline-end: 10px;
}
</style>
