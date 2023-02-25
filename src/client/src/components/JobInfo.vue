<template>
  <div>
    <div class="d-flex w-100 justify-content-between">
      <h5>{{job.title}} [{{job.group.name}}]</h5>
      <slot></slot>
    </div>

    <b-row>
      <b-col cols="3" lg="2">
        <label>Adresa:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{address}}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Termín:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{timerange}}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Hodinová sazba:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{job.wage}} Kč</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Volná místa:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{job.freeSpaces}} z {{job.spaces}}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="12" lg="2">
        <label>Podrobnosti:</label>
      </b-col>
      <b-col cols="12" lg="10">
        <p>{{job.description}}</p>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import mixinService from "../helpers/mixinService";

export default {
  name: "JobInfo",
  components: {},
  props: {
    job: Object
  },
  computed: {
    timerange: function () {
      return mixinService.getJobTimeRangeString(this.job.start, this.job.end);
    },
    address: function () {
      return mixinService.getAddressString(this.job.address);
    },
  },
};
</script>

<style scoped>
</style>

