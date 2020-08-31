<template>
  <div class="JobInfo">
    <div class="d-flex w-100 justify-content-between">
      <b-card-title>{{job.title}}</b-card-title>
      <div v-if="!!job.state">
        <b-badge pill class="p-2" v-if="job.state === 'Pending'" variant="warning">Nevyřízeno</b-badge>
        <b-badge pill class="p-2" v-if="job.state === 'Approved'" variant="success">Potvrzeno</b-badge>
        <b-badge pill class="p-2" v-if="job.state === 'Denied'" variant="danger">Zamítnuto</b-badge>
      </div>
    </div>
    <b-card-sub-title class="mb-2">{{start}} - {{end}}</b-card-sub-title>

    <b-card-text>
      <b class="mb-2">Základní informace</b>
      <p>
        Odměna: {{job.wage}} Kč/h
        <br />
        Volná místa: {{job.freeSpaces}} z {{job.spaces}}
        <br />
      </p>
    </b-card-text>

    <b-card-text>
      <b class="mb-2">Podrobnosti</b>
      <p>{{job.description}}</p>
    </b-card-text>
  </div>
</template>

<script>
import mixinService from "../helpers/mixinService";

export default {
  name: "JobInfo",
  components: {},
  props: ["job"],
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

