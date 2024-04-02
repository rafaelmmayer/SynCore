import { createRouter, createWebHistory } from 'vue-router'

import LoggedLayout from "@/layouts/LoggedLayout.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../views/HomeView.vue'),
      meta: {
        layout: LoggedLayout
      }
    },
    {
      path: '/class-schedule',
      name: 'class-schedule',
      component: () => import('../views/ClassScheduleView.vue'),
      meta: {
        layout: LoggedLayout
      }
    },
    {
      path: '/chat',
      name: 'chat',
      component: () => import('../views/ChatView.vue'),
      meta: {
        layout: LoggedLayout
      }
    },
    {
      path: '/auth/sign-in',
      name: 'sign-in',
      component: () => import('../views/auth/SignInView.vue'),
    },
    {
      path: '/auth/sign-up',
      name: 'sign-up',
      component: () => import('../views/auth/SignUpView.vue'),
    },
    {
      path: '/auth/email-password-reset',
      name: 'email-password-reset',
      component: () => import('../views/auth/EmailPasswordResetView.vue'),
    },
    {
      path: '/auth/password-reset',
      name: 'password-reset',
      component: () => import('../views/auth/PasswordResetView.vue'),
    }
  ]
})

export default router
