<script setup>
import avatar1 from '@images/avatars/avatar-1.png'
import {DeviceApi} from "@/apis/device/deviceApi"
import {ref, onMounted, computed} from "vue"
import {useRouter} from "vue-router"
const userInfo = JSON.parse(localStorage.getItem('userInfo'));
const router = useRouter();
const user = ref({})
const getUserById = async () => {
  const result = userInfo !== null ? await DeviceApi.getUserById(userInfo.Id) : {};
  user.value = result.data;
}

const logOut = () => {
  localStorage.removeItem('userInfo');
  localStorage.removeItem('accessToken')
  localStorage.removeItem('refreshToken');
  router.push({path: '/login'})

}


onMounted(async () => {
  await getUserById();
})
</script>

<template>
  <VBadge
    dot
    location="bottom right"
    offset-x="3"
    offset-y="3"
    bordered
    color="success"
  >
    <VAvatar
      class="cursor-pointer"
      color="primary"
      variant="tonal"
    >
      <VImg :src="avatar1" />

      <!-- SECTION Menu -->
      <VMenu
        activator="parent"
        width="230"
        location="bottom end"
        offset="14px"
      >
        <VList>
          <!-- 👉 User Avatar & Name -->
          <VListItem>
            <template #prepend>
              <VListItemAction start>
                <VBadge
                  dot
                  location="bottom right"
                  offset-x="3"
                  offset-y="3"
                  color="success"
                >
                  <VAvatar
                    color="primary"
                    variant="tonal"
                  >
                    <VImg :src="avatar1" />
                  </VAvatar>
                </VBadge>
              </VListItemAction>
            </template>

            <VListItemTitle class="font-weight-semibold">
              {{user.hoVaTen}}
            </VListItemTitle>
            <VListItemSubtitle>{{user.email}}</VListItemSubtitle>
          </VListItem>

          <VDivider class="my-2" />

          <!-- 👉 Profile -->
          <VListItem link>
            <template #prepend>
              <VIcon
                class="me-2"
                icon="tabler-user"
                size="22"
              />
            </template>

            <VListItemTitle>Trang cá nhân</VListItemTitle>
          </VListItem>


          <!-- Divider -->
          <VDivider class="my-2" />

          <!-- 👉 Logout -->
          <VListItem @click="logOut">
            <template #prepend>
              <VIcon
                class="me-2"
                icon="tabler-logout"
                size="22"
              />
            </template>

            <VListItemTitle>Đăng xuất</VListItemTitle>
          </VListItem>
        </VList>
      </VMenu>
      <!-- !SECTION -->
    </VAvatar>
  </VBadge>
</template>
