<script setup>
import authV1BottomShape from '@images/svg/auth-v1-bottom-shape.svg?raw'
import authV1TopShape from '@images/svg/auth-v1-top-shape.svg?raw'
import { VNodeRenderer } from '@layouts/components/VNodeRenderer'
import { themeConfig } from '@themeConfig'
import {AuthApi} from "@/apis/auth/authApi"
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import {useRouter, RouterLink} from "vue-router"

const router = useRouter()
const time = ref();
const refVForm = ref()
const loading = ref(false)
const disabled = ref(false)
const businessExecute = ref({
  maXacNah: '',
  matKhauMoi: '',
  xacNhanMatKhauMoi: ''
})
const resetPassword = async () => {
  loading.value = false;
  const result = await AuthApi.confirmCreateNewPassword(businessExecute.value);
  if(result.status === 200){
    toast("Thay đổi mật khẩu thành công", {
      type: "success",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
  time.value = setTimeout(() =>{
    router.push('/login')
  }, 1500)
  }
  else{
    toast("Đã xảy ra lỗi", {
      type: "error",
      transition: "flip",
      theme: "dark",
      "autoClose": 2000,
      dangerouslyHTMLString: true,
    });
  loading.value = false
  }
}

const isPasswordVisible = ref(false)
const isConfirmPasswordVisible = ref(false)

const onSubmit = () => {
  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
      resetPassword()
  })
}
</script>

<template>
  <div class="auth-wrapper d-flex align-center justify-center pa-4">
    <div class="position-relative my-sm-16">
      <!-- <VNodeRenderer
        :nodes="h('div', { innerHTML: authV1TopShape })"
        class="text-primary auth-v1-top-shape d-none d-sm-block"
      />

      <VNodeRenderer
        :nodes="h('div', { innerHTML: authV1BottomShape })"
        class="text-primary auth-v1-bottom-shape d-none d-sm-block"
      /> -->

      <!-- 👉 Auth Card -->
      <VCard
        class="auth-card pa-4"
        max-width="448"
      >
        <VCardItem class="justify-center">
          <template #prepend>
            <div class="d-flex">
              <VNodeRenderer :nodes="themeConfig.app.logo" class="mb-6" />
            </div>
          </template>

          <VCardTitle class="font-weight-bold text-capitalize text-h5 py-1">
            RepairDevice
          </VCardTitle>
        </VCardItem>

        <VCardText class="pt-2">
          <h5 class="text-h5 mb-1">
            Tạo mật khẩu mới 🔒
          </h5>
        </VCardText>

        <VCardText>
          <VForm ref="refVForm" @submit.prevent="onSubmit">
            <VRow>
            <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.maXacNhan"
                  autofocus
                  label="Mã xác nhận"
                />
              </VCol>
              <!-- password -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.matKhauMoi"
                  autofocus
                  label="Mật khẩu mới"
                  :type="isPasswordVisible ? 'text' : 'password'"
                  :append-inner-icon="isPasswordVisible ? 'tabler-eye-off' : 'tabler-eye'"
                  @click:append-inner="isPasswordVisible = !isPasswordVisible"
                />
              </VCol>

              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.xacNhanMatKhauMoi"
                  autofocus
                  label="Xác nhận mật khẩu mới"
                  :type="isPasswordVisible ? 'text' : 'password'"
                  :append-inner-icon="isPasswordVisible ? 'tabler-eye-off' : 'tabler-eye'"
                  @click:append-inner="isPasswordVisible = !isPasswordVisible"
                />
              </VCol>


              <!-- reset password -->
              <VCol cols="12">
                <VBtn
                  block
                  type="submit"
                >
                  Xác nhận
                </VBtn>
              </VCol>

              <!-- back to login -->
              <VCol cols="12">
                <RouterLink
                  class="d-flex align-center justify-center"
                  :to="{ path: '/login' }"
                >
                  <VIcon
                    icon="tabler-chevron-left"
                    class="flip-in-rtl"
                  />
                  <span>Trở về đăng nhập</span>
                </RouterLink>
              </VCol>
            </VRow>
          </VForm>
        </VCardText>
      </VCard>
    </div>
  </div>
</template>

<style lang="scss">
@use "@core/scss/template/pages/page-auth.scss";
</style>

<route lang="yaml">
meta:
  layout: blank
</route>
