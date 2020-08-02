<template>
      <b-list-group-item class="flex-column align-items-start mb-2">
        
        <div class="d-flex w-100 justify-content-between">
          <h5 class="mb-1">{{application.title}}</h5>
          <b-badge pill class="p-2" v-if="application.state === 'Pending'" variant="warning">Nevyřízeno</b-badge>
          <b-badge pill class="p-2" v-if="application.state === 'Approved'" variant="success">Potvrzeno</b-badge>
          <b-badge pill class="p-2" v-if="application.state === 'Denied'" variant="danger">Zamítnuto</b-badge>
        </div>

        <p
          class="mb-1"
        >Text</p>

        <small>Text</small>
        
      </b-list-group-item>
</template>

<script>
export default {
  name: "StudentApplicationItem",
  components: {},
  props: ["application"]
};
</script>

<style scoped>
</style>

