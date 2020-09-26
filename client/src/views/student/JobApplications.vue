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
import { mapGetters } from "vuex";
import JobListItem from "../../components/JobListItem";
import PageHeader from "../../components/PageHeader";
import router from "../../router";
import apiSevice from "../../helpers/apiService";
import errorBox from "../../helpers/errorBox";
import JobApplicationState from '../../components/JobApplicationState';

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
    getApplications() {
      this.jobApplications = null;
      apiSevice
        .get("/job-applications")
        .then((response) => {
          this.jobApplications = response;
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push({ name: "Login" });
    } else {
      this.getApplications();
    }
  },
};
</script>
<style>
</style>

