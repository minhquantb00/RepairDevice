<script setup>
import {computed} from "vue"
import {
  avatarText,
  kFormatter,
} from '@core/utils/formatters'

const props = defineProps({
  deviceData: {
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
              :src="deviceData.imageUrl"
            />
          </VAvatar>

          <!-- 👉 User fullName -->
          <h6 class="text-h4 mt-4">
            {{deviceData.tenThietBi}}
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
                  Tên thiết bị:
                  <span class="text-body-1">
                    {{deviceData.tenThietBi}}
                  </span>
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Giá:
                  <span class="text-body-1">{{formatCurrency(deviceData.gia)}}</span>
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Khách hàng: {{deviceData.dataResponseKhachHang != null ? deviceData.dataResponseKhachHang.hoVaTen : ""}}
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Mô tả: {{deviceData.moTa}}
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Trạng thái:
                  <span class="text-body-1">
                    {{computedDeviceStatus}}
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
