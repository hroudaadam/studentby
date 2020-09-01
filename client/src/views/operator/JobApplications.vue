<template>
  <div class="OperatorJobApplications">
    <PageHeader v-bind:title="'Přihlášky'"></PageHeader>
    <div v-if="!!jobApplications && jobApplications.length > 0">
      <div class="mt-2">
        <b-list-group >
          <JobListItem
            v-bind:key="jobApplication.id" 
            v-for="jobApplication in jobApplications"
            v-bind:job="jobApplication"
            v-bind:onClickLink="{name: 'OperatorJobApplicationDetail', params: {id: jobApplication.id}}"
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
  name: "OperatorJobApplications",
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
        .get("/operator/job-applications")
        .then((response) => {
          this.jobApplications = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push("/login");
    } else {
      this.getApplications();
    }
  },
};
</script>
<style>
</style>

