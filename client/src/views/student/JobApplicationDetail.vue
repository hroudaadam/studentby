<template>
  <div class="StudentJobApplicationDetail">
    <PageHeader v-bind:title="'Přihlášky'">
      <b-button variant="primary" :to="{name: 'StudentJobApplications'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!this.jobApplication">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobApplication.jobOffer">
            <JobApplicationState v-bind:jobApplicationState="jobApplication.state"></JobApplicationState>
          </JobInfo>
          <hr/>
          <b-button v-on:click="deleteJobApplication()" size="sm">Zrušit</b-button>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from '../../components/PageHeader';
import JobInfo from '../../components/JobInfo';
import JobApplicationState from '../../components/JobApplicationState';

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
  name: "StudentJobApplicationDetail",
  props: ["jobApplicationId"],
  components: {
    PageHeader,
    JobInfo,
    JobApplicationState
  },
  data() {
    return {
      jobApplication: null,
    };
  },
  methods: {
    // get job offe
    getJobOffer() {
      this.jobApplication = null;
      apiService
        .get("/job-applications/" + this.jobApplicationId.toString() + "/student-view")
        .then((response) => {
          this.jobApplication = response;
        });
    },    
    // delete job application
    deleteJobApplication() {
      apiService
        .del("/job-applications/" + this.jobApplicationId.toString())
        .then(() => {
          router.push({name: 'StudentJobApplications'});
        });
    },
  },
  computed: {
    ...mapGetters(["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push({name: 'Login'});
    } else {
      this.getJobOffer();
    }
  },
};
</script>

<style>
</style>

