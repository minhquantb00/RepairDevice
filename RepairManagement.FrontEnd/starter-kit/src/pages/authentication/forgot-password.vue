<script setup>
import authV1BottomShape from '@images/svg/auth-v1-bottom-shape.svg?raw'
import authV1TopShape from '@images/svg/auth-v1-top-shape.svg?raw'
import { VNodeRenderer } from '@layouts/components/VNodeRenderer'
import { requiredValidator } from "@core/utils/validators";
import { themeConfig } from '@themeConfig'
import {useRouter, RouterLink} from "vue-router"
import {AuthApi} from "@/apis/auth/authApi";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
const router = useRouter()
const time = ref();
const refVForm = ref()
const loading = ref(false)
const disabled = ref(false)

const businessExecute = ref({ email: '' });

const forgotPassword = async () => {
  loading.value = false;
  const result = await AuthApi.forgotPassword(businessExecute.value);
  if(result.status === 200){
    toast("Chúng tôi đã gửi mã xác nhận email! Vui lòng kiểm tra email của bạn", {
      type: "success",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
  time.value = setTimeout(() =>{
    router.push('/reset-password')
  }, 1500)
  }
  else{
    toast("Đã xảy ra lỗi!", {
      type: "error",
      transition: "flip",
      theme: "dark",
      "autoClose": 2000,
      dangerouslyHTMLString: true,
    });
  loading.value = false
  }
}
const onSubmit = () => {
  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
      forgotPassword()
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

      <!-- 👉 Auth card -->
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
        </VCardItem>

        <VCardText class="pt-2">
          <h5 class="text-h5 mb-1">
            Quên mật khẩu? 🔒
          </h5>
          <p class="mb-0">
            Nhập email của bạn để lấy mã xác nhận
          </p>
        </VCardText>

        <VCardText>
          <VForm ref="refVForm" @submit.prevent="onSubmit">
            <VRow>
              <!-- email -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.email"
                  autofocus
                  label="Email"
                  type="email"
                  :rules="[requiredValidator]"
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
