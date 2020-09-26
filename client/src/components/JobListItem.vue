<template>
  <b-list-group-item :to="onClickLink" class="flex-column align-items-start">
    <div class="d-flex w-100 justify-content-between">
      <h5 class="mb-1">{{job.title}}</h5>      
      <small v-if="!!job.wage">{{job.wage}} Kč/h</small>
      <slot></slot>
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
  props: {
    job: Object,
    onClickLink: Object,
    jobApplicationState: String
  },
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

