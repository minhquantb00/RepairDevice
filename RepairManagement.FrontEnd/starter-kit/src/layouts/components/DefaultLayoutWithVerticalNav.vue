<script setup>
import navItems from '@/navigation/vertical'
import { useThemeConfig } from '@core/composable/useThemeConfig'

// Components
import Footer from '@/layouts/components/Footer.vue'
import NavBarNotifications from '@/layouts/components/NavBarNotifications.vue'
import NavbarShortcuts from '@/layouts/components/NavbarShortcuts.vue'
import NavbarThemeSwitcher from '@/layouts/components/NavbarThemeSwitcher.vue'
import NavSearchBar from '@/layouts/components/NavSearchBar.vue'
import UserProfile from '@/layouts/components/UserProfile.vue'

// @layouts plugin
import { VerticalNavLayout } from '@layouts'

const { appRouteTransition, isLessThanOverlayNavBreakpoint } = useThemeConfig()
const { width: windowWidth } = useWindowSize()
</script>

<template>
  <VerticalNavLayout :nav-items="navItems">
    <!-- 👉 navbar -->
    <template #navbar="{ toggleVerticalOverlayNavActive }">
      <div class="d-flex h-100 align-center">
        <IconBtn
          v-if="isLessThanOverlayNavBreakpoint(windowWidth)"
          id="vertical-nav-toggle-btn"
          class="ms-n3"
          @click="toggleVerticalOverlayNavActive(true)"
        >
          <VIcon
            size="26"
            icon="tabler-menu-2"
          />
        </IconBtn>

        <NavSearchBar class="ms-lg-n3" />

        <VSpacer />
        <NavbarShortcuts class="me-1 short-cut" style="margin-right: 20px"/>
        <div class="hotline d-flex" style="align-items: center; margin-right: 20px; margin-left: 20px">

						<div style="margin-right: 8px">
            <svg xmlns="http://www.w3.org/2000/svg" width="29" height="29" viewBox="0 0 29 29">

					    	<g fill="#e71b1b">

						        <path d="M21.867 19.265c-.94-.928-2.113-.928-3.047 0-.712.706-1.424 1.412-2.125 2.13-.191.198-.353.24-.586.108-.461-.251-.952-.455-1.395-.73-2.065-1.299-3.794-2.968-5.326-4.848-.76-.933-1.437-1.933-1.91-3.058-.095-.227-.078-.377.108-.563.712-.688 1.407-1.394 2.107-2.1.975-.982.975-2.131-.006-3.119-.557-.562-1.113-1.113-1.67-1.675-.575-.575-1.143-1.155-1.724-1.724-.94-.916-2.113-.916-3.046.006-.718.706-1.407 1.43-2.137 2.125C.434 6.457.093 7.24.02 8.157c-.113 1.49.252 2.897.767 4.267 1.053 2.837 2.657 5.357 4.602 7.667 2.628 3.124 5.764 5.596 9.433 7.38 1.652.801 3.364 1.418 5.225 1.52 1.28.071 2.394-.252 3.286-1.251.61-.683 1.299-1.305 1.945-1.958.958-.97.964-2.142.012-3.1-1.137-1.143-2.28-2.28-3.423-3.417zM20.714 15L23 14.601c-.36-2.145-1.35-4.088-2.855-5.633C18.553 7.342 16.54 6.316 14.322 6L14 8.348c1.716.247 3.277 1.038 4.51 2.298 1.164 1.19 1.926 2.696 2.204 4.354zM24.49 4.699C21.982 2.126 18.81.5 15.306 0L15 2.24c3.026.436 5.77 1.843 7.936 4.06 2.055 2.107 3.403 4.77 3.892 7.7L29 13.62c-.571-3.395-2.131-6.475-4.51-8.921z"></path>

						    </g>

						</svg>
            </div>

						<div>
            <div>Hotline miễn phí</div>

						<div class="phone_number"><a title="hotline" href="tel:1800 6024">1800 6024</a></div></div>



					</div>
        <NavbarThemeSwitcher class="me-1" />

        <NavBarNotifications class="me-2" />
        <UserProfile />
      </div>
    </template>

    <!-- 👉 Pages -->
    <RouterView v-slot="{ Component }">
      <Transition
        :name="appRouteTransition"
        mode="out-in"
      >
        <Component :is="Component" />
      </Transition>
    </RouterView>

    <!-- 👉 Footer -->
    <template #footer>
      <Footer />
    </template>

    <!-- 👉 Customizer -->
    <TheCustomizer />
  </VerticalNavLayout>
</template>
<style scoped>
#menu-fixed-bar .hotline {
    width: calc(100% - 165px);
    float: right;
    position: relative;
    padding-left: 36px;
    box-sizing: border-box;
    text-transform: uppercase;
    font-size: 11px;
}
.short-cut:hover{
  cursor: pointer;
}
.hotline:hover{
  cursor: pointer;
}
</style>
