<template>
  <div class="PageHeader">
    <div class="mb-4">
      <div class="d-flex w-100 justify-content-between">
        <h2 class="mb-1">{{this.title}}</h2>
        <div>
          <slot></slot>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "PageHeader",
  props: ["title"],
};
</script>