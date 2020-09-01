<template>
  <div class="StudentJobApplications">
    <PageHeader v-bind:title="'Přihlášky'"></PageHeader>
    <div v-if="!!jobApplications && jobApplications.length > 0">
        <b-list-group>
          <JobListItem
          v-bind:key="jobApplication.id" 
          v-for="jobApplication in jobApplications"
            v-bind:job="jobApplication"
            v-bind:onClickLink="{name: 'StudentJobApplicationDetail', params: {id: jobApplication.id}}"
          ></JobListItem>
        </b-list-group>
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
    getApplications() {
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
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getApplications();
    }
  },
};
</script>
<style>
</style>

