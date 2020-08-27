<template>
  <b-list-group-item class="flex-column align-items-start mb-2">
    <div class="d-flex w-100 justify-content-between">
      <h5 class="mb-1">{{application.title}}</h5>
      <b-badge pill class="p-2" v-if="application.state === 'Pending'" variant="warning">Nevyřízeno</b-badge>
      <b-badge pill class="p-2" v-if="application.state === 'Approved'" variant="success">Potvrzeno</b-badge>
      <b-badge pill class="p-2" v-if="application.state === 'Denied'" variant="danger">Zamítnuto</b-badge>
    </div>

    <p class="mb-1">Text1</p>

    <div class="d-flex w-100 justify-content-between">
      <small>Text2</small>
      <b-button size="sm" variant="secondary" class="mb-2 pr-2 pl-2" v-on:click="$emit('cancel', application.id)">
        Zrušit
    </b-button>
    </div>
  </b-list-group-item>
</template>

<script>

export default {
  name: "StudentApplicationItem",
  components: {},
  props: ["application"],
  computed: {
  },
  methods: {
  }
};
</script>

<style scoped>
</style>

