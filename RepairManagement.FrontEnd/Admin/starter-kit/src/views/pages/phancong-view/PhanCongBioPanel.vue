<script setup>
import {computed} from "vue"
import {
  avatarText,
  kFormatter,
} from '@core/utils/formatters'

const props = defineProps({
  thietBiSuaChuaData: {
    type: Object,
    required: true,
  },
})
const formatCurrency = (value) => {
  return new Intl.NumberFormat("vi-VN", { style: "currency", currency: "VND" }).format(
    value
  );
};
const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
};
const computedDeviceStatus = computed(() => {
  if (props.thietBiSuaChuaData.status === "ChuaSua") {
    return "Chưa sửa";
  } else if (props.thietBiSuaChuaData.status === "DangSua") {
    return "Đang sửa";
  } else {
    return "Hoàn thành";
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
            {{thietBiSuaChuaData.tenThietBiSuaChua}}
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
                    {{thietBiSuaChuaData.tenThietBiSuaChua}}
                  </span>
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Mô tả lỗi:
                  <span class="text-body-1">{{thietBiSuaChuaData.moTaLoi}}</span>
                </h6>
              </VListItemTitle>
            </VListItem>


            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Ghi chú của khách: {{thietBiSuaChuaData.ghiChuCuaKhachHang}}
                </h6>
              </VListItemTitle>
            </VListItem>
            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Thời gian nhận sửa: {{formatDate(thietBiSuaChuaData.thoiGianNhanSua)}}
                </h6>
              </VListItemTitle>
            </VListItem>
            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Thời gian dự kiến: {{formatDate(thietBiSuaChuaData.thoiGianDuKien)}}
                </h6>
              </VListItemTitle>
            </VListItem>
            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Thời gian thực tế: {{formatDate(thietBiSuaChuaData.thoiGianThucTe)}}
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
