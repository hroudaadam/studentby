<template>
  <b-list-group-item :to="onClickLink" class="flex-column align-items-start mb-2">
    <div class="d-flex w-100 justify-content-between">
      <h5 class="mb-1">{{job.title}}</h5>
      <small v-if="!!job.wage">{{job.wage}} Kč/h</small>
      <div v-if="!!job.state">
        <b-badge pill class="p-2" v-if="job.state === 'Pending'" variant="warning">Nevyřízeno</b-badge>
        <b-badge pill class="p-2" v-if="job.state === 'Approved'" variant="success">Potvrzeno</b-badge>
        <b-badge pill class="p-2" v-if="job.state === 'Denied'" variant="danger">Zamítnuto</b-badge>
      </div>
    </div>

    <p class="mb-1">{{start}} - {{end}}</p>

    <small>město</small>
  </b-list-group-item>
</template>

<script>
import mixinService from "../helpers/mixinService";

export default {
  name: "JobListItem",
  components: {},
  props: ["job", "onClickLink"],
  computed: {
    start: function () {
      return mixinService.dateToString(this.job.start);
    },
    end: function () {
      return mixinService.dateToString(this.job.end);
    },
  },
};
</script>

<style scoped>
</style>

