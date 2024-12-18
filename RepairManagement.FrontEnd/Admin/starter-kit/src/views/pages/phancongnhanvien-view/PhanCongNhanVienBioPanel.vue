<script setup>
import {computed, ref, onMounted} from "vue"
import {
  avatarText,
  kFormatter,
} from '@core/utils/formatters'

const props = defineProps({
  phanCongCongViecData: {
    type: Object,
    required: true,
    default: () => ({}),
  },
})

const thietBi = ref({});
const data = ref({});
const nv = ref({})

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
onMounted( () => {
  thietBi.value = props.phanCongCongViecData;
  data.value = thietBi.value.dataResponseThietBiSuaChua;
  nv.value = thietBi.value.nhanVien;
})
</script>

<template>
  <VRow>
    <VCol cols="12">
      <VCard >
        <VCardText class="text-center pt-15">
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
          <h6 class="text-h4 mt-4">
            {{data.tenThietBiSuaChua}}
          </h6>
        </VCardText>

        <VDivider />

        <VCardText>
          <p class="text-sm text-uppercase text-disabled">
            Chi tiết
          </p>
          <VList class="card-list mt-2">
            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Tên thiết bị:
                  <span class="text-body-1">
                    {{data.tenThietBiSuaChua}}
                  </span>
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Tên nhân viên:
                  <span class="text-body-1">{{nv.hoVaTen}}</span>
                </h6>
              </VListItemTitle>
            </VListItem>


            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Ghi chú: {{phanCongCongViecData.ghiChu}}
                </h6>
              </VListItemTitle>
            </VListItem>
            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Thời gian phân công: {{formatDate(phanCongCongViecData.thoiGianPhanCong)}}
                </h6>
              </VListItemTitle>
            </VListItem>
            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Thời gian hoàn thành: {{formatDate(phanCongCongViecData.thoiGianHoanThanh)}}
                </h6>
              </VListItemTitle>
            </VListItem>

            <VListItem>
              <VListItemTitle>
                <h6 class="text-h6">
                  Trạng thái:
                  <span class="text-body-1">
                    {{phanCongCongViecData.status}}
                  </span>
                </h6>
              </VListItemTitle>
            </VListItem>
          </VList>
        </VCardText>
      </VCard>
    </VCol>
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
