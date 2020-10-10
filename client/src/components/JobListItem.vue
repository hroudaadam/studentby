<template>
  <b-list-group-item :to="onClickLink" class="flex-column align-items-start">
    <div class="d-flex w-100 justify-content-between">
      <h5 class="mb-1">{{job.title}}</h5>      
      <slot></slot>
    </div>

    <p class="mb-1">{{timerange}}</p>

    <div class="d-flex w-100 justify-content-between">
      <p class="mb-1">adresa</p>    
      <small v-if="!!job.wage">{{job.wage}} Kč/h</small>
    </div>
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
    timerange: function () {
      return mixinService.getJobTimeRangeString(this.job.start, this.job.end);
    }
  },
};
</script>

<style scoped>
</style>

