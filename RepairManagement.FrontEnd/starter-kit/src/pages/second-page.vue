<script setup>
import handling from '@/pages/devices/handling.vue'
import pending from '@/pages/devices/pending.vue'
import delivering from '@/pages/devices/delivering.vue'
import done from '@/pages/devices/done.vue'
import googleHome from '@images/pages/google-home.png'
import iphone11 from '@images/pages/iphone-11.png'

const checkoutSteps = [
  {
    title: 'Chờ xử lý',
    icon: 'custom-cart',
  },
  {
    title: 'Đang xử lý',
    icon: 'custom-address',
  },
  {
    title: 'Đã hoàn thành',
    icon: 'custom-payment',
  },
  {
    title: 'Đang giao',
    icon: 'custom-trending',
  },
]

const checkoutData = ref({
  cartItems: [
    {
      id: 1,
      name: 'Acer Aspire 7 - Ryzen 5 5500U',
      seller: 'Nguyễn Văn A',
      inStock: true,
      rating: 4,
      price: 299,
      discountPrice: 359,
      image: 'https://laptopmedia.com/wp-content/uploads/2021/01/2-51-e1619534462869.jpg',
      quantity: 1,
      estimatedDelivery: '18th Nov 2021',
    },
    {
      id: 2,
      name: 'HP 205 Pro G8 AiO',
      seller: 'Nguyễn Văn B',
      inStock: true,
      rating: 4,
      price: 899,
      discountPrice: 999,
      image: 'https://genk.mediacdn.vn/139269124445442048/2022/3/18/anh-3-16475931415151542185711.jpg',
      quantity: 1,
      estimatedDelivery: '20th Nov 2021',
    },
  ],
  promoCode: '',
  orderAmount: 1198,
  deliveryAddress: 'home',
  deliverySpeed: 'free',
  deliveryCharges: 0,
  addresses: [
    {
      title: 'John Doe (Default)',
      desc: '4135 Parkway Street, Los Angeles, CA, 90017',
      subtitle: '1234567890',
      value: 'home',
    },
    {
      title: 'ACME Inc.',
      desc: '87 Hoffman Avenue, New York, NY, 10016',
      subtitle: '1234567890',
      value: 'office',
    },
  ],
})

const currentStep = ref(0)
</script>

<template>
  <VCard>
    <VCardText>
      <!-- 👉 Stepper -->
      <AppStepper
        v-model:current-step="currentStep"
        class="checkout-stepper"
        :items="checkoutSteps"
        :direction="$vuetify.display.smAndUp ? 'horizontal' : 'vertical'"
      />
    </VCardText>

    <VDivider />

    <VCardText>
      <!-- 👉 stepper content -->
      <VWindow
        v-model="currentStep"
        class="disable-tab-transition"
      >
        <VWindowItem>
          <pending
            v-model:current-step="currentStep"
            v-model:checkout-data="checkoutData"
          />
        </VWindowItem>

        <VWindowItem>
          <handling
            v-model:current-step="currentStep"
            v-model:checkout-data="checkoutData"
          />
        </VWindowItem>

        <VWindowItem>
          <done
            v-model:current-step="currentStep"
            v-model:checkout-data="checkoutData"
          />
        </VWindowItem>

        <VWindowItem>
          <delivering v-model:checkout-data="checkoutData" />
        </VWindowItem>
      </VWindow>
    </VCardText>
  </VCard>
</template>

<style lang="scss">
.checkout-stepper {
  .stepper-icon-step {
    .step-wrapper + svg {
      margin-inline: 3.5rem !important;
    }
  }
}
</style>
