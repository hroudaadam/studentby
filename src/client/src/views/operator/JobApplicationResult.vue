<template>
  <div>
    <PageHeader v-bind:title="'Výsledek'">
      <b-button
        variant="secondary"
        size="sm"
        :to="{
          name: 'OperatorJobOfferDetail',
          params: { jobOfferId: jobOfferId },
        }"
        >Zpět</b-button
      >
    </PageHeader>
    <div v-if="!!this.jobApplication">
      <b-card no-body>
        <b-card-body>
          <div class="mb-2"><h5>Údaje</h5></div>
          <StudentInfo v-bind:student="jobApplication.student"></StudentInfo>
          <hr />
          <b-button
            variant="primary"
            class="mr-2"
            size="sm"
            v-on:click="editJobApplication(true)"
            >Účast</b-button
          >
          <b-button
            variant="secondary"
            size="sm"
            v-on:click="editJobApplication(false)"
            >Absence</b-button
          >
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import StudentInfo from "../../components/StudentInfo";

import { mapGetters, mapState } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
  name: "OperatorJobApplicationResult",
  props: {
    jobApplicationId: String,
    jobOfferId: String,
  },
  components: {
    PageHeader,
    StudentInfo,
  },
  data() {
    return {
      jobApplication: null,
    };
  },
  methods: {
    // get job application
    getJobApplication() {
      this.jobApplication = null;
      apiService
        .get(
          "/job-applications/" + this.jobApplicationId
        )
        .then((response) => {
          this.jobApplication = response;
        })
        .catch(() => {});
    },
    // edit job application
    editJobApplication(attended) {
      let state = attended
        ? this.jobApplicationStates.attended
        : this.jobApplicationStates.absent;
        let body = {
        id: this.jobApplicationId,
        state: state,
      };
      apiService
        .put("/job-applications/" + this.jobApplicationId, body)
        .then(() => {
          router.push({
            name: "OperatorJobOfferDetail",
            params: { jobOfferId: this.jobOfferId },
          });
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
    ...mapState(["jobApplicationStates"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getJobApplication();
    }
  },
};
</script>

<style>
</style>

