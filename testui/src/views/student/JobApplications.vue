<template>
  <div class="StudentJobApplications">
    <PageHeader v-bind:title="'Přihlášky'"></PageHeader>
    <div v-if="!!jobApplications && jobApplications.length > 0">
      <div class="mt-2">
        <b-list-group v-bind:key="jobApplication.id" v-for="jobApplication in jobApplications">
          <JobListItem
            v-bind:job="jobApplication"
            v-bind:onClickLink="{name: 'StudentJobApplicationDetail', params: {id: jobApplication.id}}"
          ></JobListItem>
        </b-list-group>
      </div>
    </div>
    <div v-else>Nemáte žádné přihlášky</div>
    <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import JobListItem from "../../components/JobListItem";
import PageHeader from "../../components/PageHeader";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "StudentJobApplications",
  components: {
    JobListItem,
    PageHeader,
  },
  data() {
    return {
      jobApplications: null,
      errorMsg: null,
    };
  },
  methods: {
    getStudentApplications() {
      this.errorMsg = null;
      this.jobApplications = null;
      apiSevice
        .get("/student/job-applications")
        .then((response) => {
          this.jobApplications = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },

    cancelJobApplication(applicationId) {
      this.errorMsg = null;
      apiSevice
        .deleteMehod("/student/job-applications/" + applicationId.toString())
        .then((response) => {
          console.log(response);
          this.getStudentApplications();
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getStudentApplications();
    }
  },
};
</script>
<style>
</style>

