<template>
  <div class="OperatorJobApplicationDetail">
    <PageHeader v-bind:title="'Přihlášky'">
      <b-button variant="primary" :to="{name: 'OperatorJobApplications'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!this.jobApplication">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobApplication.jobOffer">
            <JobApplicationState v-bind:jobApplicationState="jobApplication.state"></JobApplicationState>
          </JobInfo>
          <b-card-text>
            <b class="mb-2">Student</b>
            <p>
              Jméno: {{jobApplication.student.firstName}} {{jobApplication.student.lastName}}
              <br />Datum narození: {{dateOfBirth}}
              <br />Adresa: město
              <br />
            </p>
          </b-card-text>

          <b-button class="mr-2" variant="success" v-on:click="editJobApplication(true)">Přijmout</b-button>
          <b-button variant="danger" v-on:click="editJobApplication(false)">Odmítnout</b-button>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import JobInfo from "../../components/JobInfo";
import JobApplicationState from '../../components/JobApplicationState';

import { mapGetters, mapState } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";
import mixinService from "../../helpers/mixinService";
import errorBox from '../../helpers/errorBox';

export default {
  name: "OperatorJobApplicationDetail",
  props: {
    jobApplicationId: Number  
  },
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
    getOfferDetail() {
      this.jobApplication = null;
      apiService
        .get("/job-applications/" + this.jobApplicationId.toString())
        .then((response) => {
          this.jobApplication = response;
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
    editJobApplication(approve) {
      var state = approve ? this.jobApplicationStates.approved : this.jobApplicationStates.denied;
      var body = {
        jobApplicationId: this.jobApplicationId,
        state: state
      };
      apiService
        .put("/job-applications/" + this.jobApplicationId.toString(), body)
        .then(() => {
          router.push({name: 'OperatorJobApplications'});
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isOperatorLogged"]),
    ...mapState(["jobApplicationStates"]),
    start: function () {
      return mixinService.dateToString(this.jobApplication.joboffer.start);
    },
    end: function () {
      return mixinService.dateToString(this.jobApplication.joboffer.end);
    },
    dateOfBirth: function () {
      return mixinService.dateOfBirthToString(this.jobApplication.student.dateOfBirth);
    }
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({name: 'Login'});
    } else {
      this.getOfferDetail();
    }
  },
};
</script>

<style>
</style>

