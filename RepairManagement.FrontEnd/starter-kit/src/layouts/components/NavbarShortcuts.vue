<script setup>
import {RouterLink} from "vue-router"
const shortcuts = [
  {
    icon: "tabler-calendar",
    title: "Calendar",
    subtitle: "Appointments",
    to: { name: "apps-calendar" },
  },
  {
    icon: "tabler-file",
    title: "Invoice App",
    subtitle: "Manage Accounts",
    to: { name: "apps-invoice-list" },
  },
  {
    icon: "tabler-user",
    title: "Users",
    subtitle: "Manage Users",
    to: { name: "apps-user-list" },
  },
  {
    icon: "tabler-layout",
    title: "Dashboard",
    subtitle: "Dashboard Analytics",
    to: { name: "dashboards-analytics" },
  },
  {
    icon: "tabler-settings",
    title: "Settings",
    subtitle: "Account Settings",
    to: {
      name: "pages-account-settings-tab",
      params: { tab: "account" },
    },
  },
  {
    icon: "tabler-help",
    title: "Help Center",
    subtitle: "FAQs & Articles",
    to: { name: "pages-help-center" },
  },
];
</script>

<template>
  <RouterLink :to="{path: '/booking'}" class="d-flex" style="align-items:center">
    <svg xmlns="http://www.w3.org/2000/svg" width="33" height="33" viewBox="0 0 33 33">
      <g fill="none">
        <path
          fill="#08D"
          d="M15.125 27.156c-.026 0-.052 0-.078-.003-.3-.023-.575-.176-.754-.419l-3.781-5.156c-.337-.46-.237-1.104.223-1.441.457-.336 1.104-.237 1.44.223l3.07 4.187 7.058-7.057c.402-.403 1.055-.403 1.458 0s.403 1.056 0 1.459l-7.906 7.906c-.195.193-.458.301-.73.301z"
        ></path>

        <path
          fill="#969696"
          d="M29.219 33H3.78C1.695 33 0 31.305 0 29.219V6.53c0-2.086 1.695-3.78 3.781-3.78H29.22C31.305 2.75 33 4.444 33 6.53V29.22C33 31.305 31.305 33 29.219 33zM3.78 4.813c-.947 0-1.719.77-1.719 1.718V29.22c0 .947.772 1.718 1.72 1.718h25.437c.947 0 1.718-.77 1.718-1.718V6.53c0-.947-.77-1.718-1.718-1.718H3.78z"
        ></path>

        <path
          fill="#969696"
          d="M31.969 13.063H1.03C.462 13.063 0 12.6 0 12.03c0-.569.462-1.03 1.031-1.03H31.97c.569 0 1.031.461 1.031 1.03 0 .57-.462 1.032-1.031 1.032zM7.906 8.25c-.569 0-1.03-.462-1.03-1.031V1.03C6.875.462 7.336 0 7.905 0c.57 0 1.032.462 1.032 1.031V7.22c0 .569-.462 1.031-1.032 1.031zM25.094 8.25c-.57 0-1.032-.462-1.032-1.031V1.03c0-.569.462-1.031 1.032-1.031.569 0 1.03.462 1.03 1.031V7.22c0 .569-.461 1.031-1.03 1.031z"
        ></path>
      </g>
    </svg>
    <div style="margin-left: 8px">
      <div>Đặt lịch</div>
    </div>
  </RouterLink>
</template>
