<script setup>
import {
  avatarText,
  kFormatter,
} from '@core/utils/formatters'

const props = defineProps({
  userData: {
    type: Object,
    required: true,
  },
})

const standardPlan = {
  plan: 'Standard',
  price: 99,
  benefits: [
    '10 Users',
    'Up to 10GB storage',
    'Basic Support',
  ],
}


</script>

<template>
  <VRow>
    <!-- SECTION User Details -->
    <VCol cols="12">
      <VCard >
        <VCardText class="text-center pt-15">
          <!-- 👉 Avatar -->
          <VAvatar
            rounded
            :size="100"
            color="primary"
            variant="tonal"
          >
            <VImg
              src="https://tse4.mm.bing.net/th?id=OIP.z3fa8PjEnvzg4bhW61tEOwAAAA&pid=Api&P=0&h=180"
            />
          </VAvatar>

          <!-- 👉 User fullName -->
          <h6 class="text-h4 mt-4">
            {{userData.hoVaTen}}
          </h6>
        </VCardText>

        <VDivider />

        <VCardText>
          <p class="text-sm text-uppercase text-disabled">
            Chi tiết
          </p>

          <!-- 👉 User Details list -->
          <VList class="card-list mt-2">
            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Họ và tên:
                  <span class="text-body-1">
                    {{userData.hoVaTen}}
                  </span>
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Email:
                  <span class="text-body-1">{{userData.email}}</span>
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Số điện thoại: {{userData.soDienThoai}}
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Địa chỉ: {{userData.diaChi}}
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Điểm:
                  <span class="text-body-1">
                    {{userData.diem}}
                  </span>
                </h6>
              </VListItemTitle>
            </VListItem>


          </VList>
        </VCardText>

        <!-- 👉 Edit and Suspend button -->
        <VCardText class="d-flex justify-center">
          <VBtn
            variant="elevated"
            class="me-4"
          >
            Chỉnh sửa
          </VBtn>

        </VCardText>
      </VCard>
    </VCol>
    <!-- !SECTION -->


  </VRow>
</template>

<style lang="scss" scoped>
.card-list {
  --v-card-list-gap: 0.75rem;
}

.text-capitalize {
  text-transform: capitalize !important;
}
</style>
