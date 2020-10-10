<template>
  <div class="OperatorJobApplications">
    <PageHeader v-bind:title="'Přihlášky'"></PageHeader>
    <div v-if="!!jobApplications && jobApplications.length > 0">
      <div class="mt-2">
        <b-list-group >
          <JobListItem
            v-bind:key="jobApplication.jobApplicationId" 
            v-for="jobApplication in jobApplications"
            v-bind:job="jobApplication.jobOffer"
            v-bind:onClickLink="{name: 'OperatorJobApplicationDetail', params: {jobApplicationId: jobApplication.jobApplicationId}}"
          >
            <JobApplicationState v-bind:jobApplicationState="jobApplication.state"></JobApplicationState>
          </JobListItem>
        </b-list-group>
      </div>
    </div>
    <div v-else>Nemáte žádné přihlášky</div>
  </div>
</template>

<script>
import JobListItem from "../../components/JobListItem";
import PageHeader from "../../components/PageHeader";
import JobApplicationState from '../../components/JobApplicationState';

import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "OperatorJobApplications",
  components: {
    JobListItem,
    PageHeader,
    JobApplicationState
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
        .catch((error) => {
          console.error(error.message);
        });
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({name: 'Login'});
    } else {
      this.getJobApplications();
    }
  },
};
</script>
<style>
</style>

