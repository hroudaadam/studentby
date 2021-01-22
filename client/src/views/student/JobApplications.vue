<template>
  <div class="StudentJobApplications">
    <PageHeader v-bind:title="'Přihlášky'"></PageHeader>
    <div v-if="!!jobApplications && jobApplications.length > 0">
      <b-list-group>
        <JobListItem
          v-bind:key="jobApplication.jobApplicationId"
          v-for="jobApplication in jobApplications"
          v-bind:job="jobApplication.jobOffer"
          v-bind:jobApplicationState="jobApplication.state"
          v-bind:onClickLink="{
            name: 'StudentJobApplicationDetail',
            params: { jobApplicationId: jobApplication.jobApplicationId },
          }"
        >
          <JobApplicationState v-bind:jobApplicationState="jobApplication.state"></JobApplicationState>
        </JobListItem>
      </b-list-group>
    </div>
    <div v-else>Nemáte žádné přihlášky</div>
  </div>
</template>

<script>
import JobApplicationState from '../../components/JobApplicationState';
import PageHeader from "../../components/PageHeader";
import JobListItem from "../../components/JobListItem";

import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "StudentJobApplications",
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
        .get("/job-applications/student-view")
        .then((response) => {
          this.jobApplications = response;
        });
    },
  },
  computed: {
    ...mapGetters(["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push({ name: "Login" });
    } else {
      this.getJobApplications();
    }
  },
};
</script>
<style>
</style>

