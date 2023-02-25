<template>
  <div>
    <PageHeader v-bind:title="'Přihlášky'"></PageHeader>
    <div class="mt-2">
      <b-list-group v-if="!!jobApplications && jobApplications.length > 0">
        <JobListItem
          v-bind:key="jobApplication.id"
          v-for="jobApplication in jobApplications"
          v-bind:job="jobApplication.jobOffer"
          v-bind:onClickLink="{
            name: 'OperatorJobApplicationDetail',
            params: { jobApplicationId: jobApplication.id },
          }"
        >
          <JobApplicationState
            v-bind:jobApplicationState="jobApplication.state"
          ></JobApplicationState>
        </JobListItem>
      </b-list-group>
      <EmptyList v-else></EmptyList>
    </div>
  </div>
</template>

<script>
import JobListItem from "../../components/JobListItem";
import PageHeader from "../../components/PageHeader";
import JobApplicationState from "../../components/JobApplicationState";
import EmptyList from "../../components/EmptyList";

import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "OperatorJobApplications",
  components: {
    JobListItem,
    PageHeader,
    JobApplicationState,
    EmptyList
  },
  data() {
    return {
      jobApplications: null,
    };
  },
  methods: {
    // get job applications
    getJobApplications() {
      this.jobApplications = null;
      apiSevice
        .get("/job-applications")
        .then((response) => {
          this.jobApplications = response;
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getJobApplications();
    }
  },
};
</script>
<style>
</style>

