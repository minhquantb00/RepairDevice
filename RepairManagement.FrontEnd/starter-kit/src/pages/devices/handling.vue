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
const checkoutCartDataLocal = ref(props.checkoutData)
const emit = defineEmits([
  'update:currentStep',
  'update:checkout-data',
])

const checkoutAddressDataLocal = ref(props.checkoutData)

const deliveryOptions = [
  {
    icon: { icon: 'tabler-user' },
    title: 'Standard',
    desc: 'Get your product in 1 Week.',
    value: 'free',
  },
  {
    icon: { icon: 'tabler-crown' },
    title: 'Express',
    desc: 'Get your product in 3-4 days.',
    value: 'express',
  },
  {
    icon: { icon: 'tabler-rocket' },
    title: 'Overnight',
    desc: 'Get your product in 1 day.',
    value: 'overnight',
  },
]

const resolveAddressBadgeColor = {
  home: 'primary',
  office: 'success',
}

const resolveDeliveryBadgeData = {
  free: {
    color: 'success',
    price: 'Free',
  },
  express: {
    color: 'secondary',
    price: 10,
  },
  overnight: {
    color: 'secondary',
    price: 15,
  },
}

const totalPriceWithDeliveryCharges = computed(() => {
  checkoutAddressDataLocal.value.deliveryCharges = 0
  if (checkoutAddressDataLocal.value.deliverySpeed !== 'free')
    checkoutAddressDataLocal.value.deliveryCharges = resolveDeliveryBadgeData[checkoutAddressDataLocal.value.deliverySpeed].price

  return checkoutAddressDataLocal.value.orderAmount + checkoutAddressDataLocal.value.deliveryCharges
})

const updateAddressData = () => {
  emit('update:checkout-data', checkoutAddressDataLocal.value)
}

const removeItem = item => {
  checkoutCartDataLocal.value.cartItems = checkoutCartDataLocal.value.cartItems.filter(i => i.id !== item.id)
  console.log(checkoutCartDataLocal.value.cartItems)
}

const nextStep = () => {
  updateAddressData()
  emit('update:currentStep', props.currentStep ? props.currentStep + 1 : 1)
}

watch(() => props.currentStep, updateAddressData)
</script>

<template>
  <VRow>
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
    >
      <VCard
        flat
        variant="outlined"
      >
        <!-- 👉 Delivery estimate date -->
        <VCardText>
          <h6 class="text-base font-weight-medium mb-5">
            Chi phí sửa chữa thiết bị thực tế
          </h6>

          <VList class="card-list">
            <VListItem
              v-for="product in checkoutAddressDataLocal.cartItems"
              :key="product.name"
            >
              <template #prepend>
                <VImg
                  width="60"
                  :src="product.image"
                  class="me-2"
                />
              </template>

              <VListItemSubtitle>{{ product.name }}</VListItemSubtitle>
              <span class="font-weight-medium text-sm">
                {{ product.estimatedDelivery }}
              </span>
            </VListItem>
          </VList>
        </VCardText>

        <VDivider />

        <!-- 👉 Price details -->
        <VCardText>
          <!-- <h6 class="text-base font-weight-medium mb-3">
            Chi phí linh kiện sửa chữa dự kiến
          </h6> -->

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
