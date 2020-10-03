<template>
  <div class="StudentOfferDetail">
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="primary" :to="{name: 'StudentJobOffers'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!this.jobOffer">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobOffer"></JobInfo>
        </b-card-body>

        <b-card-body>
          <b-button v-on:click="createJobApplication()" variant="primary">Přihlásit se</b-button>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from '../../components/PageHeader';
import JobInfo from '../../components/JobInfo';

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";
import mixinService from "../../helpers/mixinService";
import errorBox from "../../helpers/errorBox";

export default {
  name: "StudentJobOfferDetail",
  props: ["jobOfferId"],
  components: {
    PageHeader,
    JobInfo
  },
  data() {
    return {
      jobOffer: null,
    };
  },
  methods: {
    // get job offer
    getJobOffer() {
      this.jobOffer = null;
      apiService
        .get("/job-offers/" + this.jobOfferId.toString())
        .then((response) => {
          this.jobOffer = response;
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
    // create job application
    createJobApplication() {
      var body = { jobOfferId: this.jobOfferId };

      apiService
        .post("/job-applications", body)
        .then(() => {
          router.push({ name: "StudentJobApplications" });
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
  },
  computed: {
    ...mapGetters(["isStudentLogged"]),
    start: function () {
      return mixinService.dateToString(this.jobOffer.start);
    },
    end: function () {
      return mixinService.dateToString(this.jobOffer.end);
    },
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

