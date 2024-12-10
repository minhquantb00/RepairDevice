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

const selectedDeliveryAddress = computed(() => {
  return props.checkoutData.addresses.filter(address => {
    return address.value === props.checkoutData.deliveryAddress
  })
})

const resolveDeliveryMethod = computed(() => {
  if (props.checkoutData.deliverySpeed === 'overnight')
    return {
      method: 'Overnight Delivery',
      desc: 'In 1 business day.',
    }
  else if (props.checkoutData.deliverySpeed === 'express')
    return {
      method: 'Express Delivery',
      desc: 'Normally in 3-4 business days',
    }
  else
    return {
      method: 'Standard Delivery',
      desc: 'Normally in 1 Week',
    }
})
</script>

<template>
  <section class="text-base">
    <div class="text-center">
      <h5 class="text-h5 mb-3">
        Thank You! 😇
      </h5>
      <p>
        Đơn hàng <span class="text-primary">#1536548131</span> đang được giao!
      </p>
      <p class="mb-0">
        Chúng tôi đã gửi về email <span class="text-primary">john.doe@example.com</span> để thông báo về đơn hàng cho bạn!
      </p>
      <p>
Cảm ơn bạn đã tin tưởng và sử dụng dịch vụ sửa chữa của chúng tôi. Chúng tôi cam kết mang đến những dịch vụ sửa chữa và bảo trì thiết bị công nghệ tốt nhất, với mục tiêu luôn làm hài lòng bạn trong từng trải nghiệm.</p>
      <div class="d-flex align-center gap-2 justify-center">
        <VIcon
          size="20"
          icon="tabler-clock"
        />
        <span>Ngày giao dự kiến: 25/05/2020</span>
      </div>
    </div>


    <VRow>
      <VCol
        cols="12"
        md="9"
        class="mt-8"
      >
        <!-- 👉 cart items -->
        <div class="border rounded">
          <template
            v-for="(item, index) in props.checkoutData.cartItems"
            :key="item.name"
          >
            <div
              class="d-flex align-start gap-3 pa-5 position-relative flex-column flex-sm-row"
              :class="index ? 'border-t' : ''"
            >
              <div>
                <VImg
                  width="80"
                  :src="item.image"
                />
              </div>

              <div
                class="d-flex w-100 pt-3"
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
                      <span>
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
      </VCol>

      <VCol
        cols="12"
        md="3"
        style="margin-top: 32px"
      >
        <VCard
        flat
        variant="outlined"
      >

        <!-- 👉 Price details -->
        <VCardText>
          <h6 class="text-base font-weight-medium mb-5">
            Chi phí sửa chữa thiết bị thực tế
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
      </VCol>
    </VRow>
  </section>
</template>
