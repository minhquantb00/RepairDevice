import { setupLayouts } from 'virtual:generated-layouts'
import { createRouter, createWebHistory } from 'vue-router'
import routes from '~pages'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      redirect: () => ({
        path: "home",
      }),
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@/pages/login.vue')
    },
    {
      path: '/register',
      name: 'register',
      component: () => import('@/pages/authentication/register.vue')
    },
    {
      path: '/forgot-password',
      name: 'forgot-password',
      component: () => import('@/pages/authentication/forgot-password.vue'),
    },
    {
      path: '/reset-password',
      name: 'reset-password',
      component: () => import('@/pages/authentication/reset-password.vue'),
    },
    ...setupLayouts(routes),
  ],
})

export default router
