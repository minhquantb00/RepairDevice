<script setup>
import avatar3 from '@images/avatars/avatar-3.png'
import avatar4 from '@images/avatars/avatar-4.png'
import avatar5 from '@images/avatars/avatar-5.png'
import paypal from '@images/svg/paypal.svg'
import {DeviceApi} from "@/apis/device/deviceApi"
import {AuthApi} from "@/apis/auth/authApi"

const userInfo = JSON.parse(localStorage.getItem('userInfo'))

const notifications = ref([])

const removeNotification = notificationId => {
  notifications.value.forEach((item, index) => {
    if (notificationId === item.id)
      notifications.value.splice(index, 1)
  })
}

const markRead = notificationId => {
  notifications.value.forEach(item => {
    notificationId.forEach(id => {
      if (id === item.id)
        item.isSeen = true
    })
  })
}

const markUnRead = notificationId => {
  notifications.value.forEach(item => {
    notificationId.forEach(id => {
      if (id === item.id)
        item.isSeen = false
    })
  })
}

const handleNotificationClick = notification => {
  if (!notification.isSeen)
    markRead([notification.id])
}

const getAllThongBao = async () => {
  const khachHang = await AuthApi.getKhachHangByUserId(userInfo.Id);
  const result = await DeviceApi.getAllThongBaoByKhachHang(khachHang.data.id);
  notifications.value = result.data
}

onMounted(async () => {
  await getAllThongBao();
})
</script>

<template>
  <Notifications
    :notifications="notifications"
    @remove="removeNotification"
    @read="markRead"
    @unread="markUnRead"
    @click:notification="handleNotificationClick"
  />
</template>
