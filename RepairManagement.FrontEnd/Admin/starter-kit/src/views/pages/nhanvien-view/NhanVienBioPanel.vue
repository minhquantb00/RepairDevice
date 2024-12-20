<script setup>
import {computed} from "vue"
import {
  avatarText,
  kFormatter,
} from '@core/utils/formatters'

const props = defineProps({
  nhanVienData: {
    type: Object,
    required: true,
  },
})
const formatCurrency = (value) => {
  return new Intl.NumberFormat("vi-VN", { style: "currency", currency: "VND" }).format(
    value
  );
};
const computedDeviceStatus = computed(() => {
  if (props.deviceData.status === "DangHoatDong") {
    return "Đang hoạt động";
  } else if (props.deviceData.status === "DangHong") {
    return "Đang hỏng";
  } else {
    return "Đã sửa xong";
  }
});
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
            {{nhanVienData.hoVaTen}}
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
                    {{nhanVienData.hoVaTen}}
                  </span>
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Email:
                  <span class="text-body-1">{{nhanVienData.email}}</span>
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Số điện thoại: {{nhanVienData.soDienThoai}}
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Địa chỉ: {{nhanVienData.diaChi}}
                </h6>
              </VListItemTitle>
            </VListItem>


          </VList>
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
