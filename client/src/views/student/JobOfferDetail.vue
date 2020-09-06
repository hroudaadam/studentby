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

      <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
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
      errorMsg: null,
    };
  },
  methods: {
    getOfferDetail() {
      this.jobOffer = null;
      this.errorMsg = null;
      apiService
        .get("/student/job-offers/" + this.jobOfferId.toString())
        .then((response) => {
          this.jobOffer = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
    createJobApplication() {
      this.errorMsg = null;
      var body = { jobOfferId: this.jobOfferId };

      apiService
        .post("/student/job-applications", body)
        .then((response) => {
          console.log(response);
          router.push({ name: "StudentJobApplications" });
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
    start: function () {
      return mixinService.dateToString(this.jobOffer.start);
    },
    end: function () {
      return mixinService.dateToString(this.jobOffer.end);
    },
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getOfferDetail();
    }
  },
};
</script>

<style>
</style>

