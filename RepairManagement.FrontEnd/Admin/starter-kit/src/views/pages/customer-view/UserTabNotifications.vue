<script setup>
import { ThietBiSuaChuaStatus } from "@/enums/enumerate"; // Đảm bảo import enum nếu bạn đang dùng TypeScript
import {DeviceApi} from "@/apis/device/deviceApi"
// Dữ liệu mẫu để minh họa
const props = defineProps({
  userData: {
    type: Object,
    required: true,
  },
})
const repairs = ref([]);

const getStatus = (status) => {
  switch (status) {
    case ThietBiSuaChuaStatus.ChuaSua:
      return "Chưa sửa";
    case ThietBiSuaChuaStatus.DangSua:
      return "Đang sửa";
    case ThietBiSuaChuaStatus.HoanThanh:
      return "Hoàn thành";
    default:
      return "Không xác định";
  }
};

const getAllLichSuSuaChua = async () => {
  const result = await DeviceApi.getAllLichSuSuaChua(props.userData.id);
  repairs.value = result.data;
}

onMounted(async () => {
  await getAllLichSuSuaChua();
})
</script>

<template>
  <VCard class="user-tab-repairs">
    <VCardItem>
      <VCardTitle>Lịch sử sửa chữa</VCardTitle>
      <p class="text-base mt-2 mb-0">
        Thông tin lịch sử sửa chữa của khách hàng tại đây
      </p>
    </VCardItem>
    <VCardText>
      <VTable class="border rounded text-no-wrap">
        <thead>
          <tr>
            <th scope="col">Tên thiết bị</th>
            <th scope="col">Chưa sửa</th>
            <th scope="col">Đang sửa</th>
            <th scope="col">Hoàn thành</th>
          </tr>
        </thead>
        <tbody>
          <tr  v-for="repair in repairs" :key="repair.id">
            <td>{{ repair.tenThietBi }}</td>
            <td>
              <VCheckbox v-model="repair.status" :true-value="ThietBiSuaChuaStatus.ChuaSua" :false-value="null" disabled />
            </td>
            <td>
              <VCheckbox v-model="repair.status" :true-value="ThietBiSuaChuaStatus.DangSua" :false-value="null" disabled />
            </td>
            <td>
              <VCheckbox v-model="repair.status" :true-value="ThietBiSuaChuaStatus.HoanThanh" :false-value="null" disabled />
            </td>
          </tr>
        </tbody>
      </VTable>
    </VCardText>
  </VCard>
</template>
