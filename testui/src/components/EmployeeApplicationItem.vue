<template>
  <!-- <b-list-group-item class="flex-column align-items-start mb-2"> -->
  <b-list-group-item class="d-flex justify-content-between align-items-center">
    {{application.studentFirstName}} {{application.studentLastName}}
    <b-badge pill class="p-2" variant="success">Potvrzeno</b-badge>
  </b-list-group-item>
</template>

<script>
export default {
  name: "EmployeeApplicationItem",
  components: {},
  props: ["application"],
  computed: {},
  methods: {},
};
</script>

<style scoped>
</style>

