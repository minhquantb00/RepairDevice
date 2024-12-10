<script setup>
const props = defineProps({
  services: {
    type: Array,
    required: true,
  },
})

</script>

<template>
  <VRow>
    <VCol
      cols="12"
      lg="10"
      class="mx-auto mb-8"
    >
      <VRow>
        <VCol
          v-for="article in props.services"
          :key="article.id"
          cols="12"
          sm="6"
          md="4"
        >
          <VCard>
            <template #title>
              <span class="text-h5">
                {{ article.title }}
              </span>
            </template>
            <template #prepend>
              <VAvatar
                :icon="article.icon"
                rounded
                :color="article.avatarColor"
                variant="tonal"
              />
            </template>
          </VCard>
        </VCol>
      </VRow>
    </VCol>
  </VRow>
</template>
