<template>
  <div>
    <PageHeader v-bind:title="'Detail nabídky'">
      <b-button variant="secondary" :to="{name: 'StudentJobOffers'}" size="sm">Zpět</b-button>
    </PageHeader>
    <div v-if="!!this.jobOffer">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobOffer"></JobInfo>
          <div v-if="isStudentLogged">
              <hr/>
              <b-button v-on:click="createJobApplication()" variant="primary" >Přihlásit</b-button>
          </div>
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

export default {
  name: "StudentJobOfferDetail",
  props: {
    jobOfferId: String
  },
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
        .get("/job-offers/" + this.jobOfferId)
        .then((response) => {
          this.jobOffer = response;
        })
        .catch(() => {});
    },
    // create job application
    createJobApplication() {
      var body = { jobOfferId: this.jobOfferId };
      apiService
        .post("/job-applications", body)
        .then(() => {
          router.push({ name: "StudentJobApplications" });
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isStudentLogged", "isStudentInactLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged && !this.isStudentInactLogged) {
      router.push({name: 'Login'});
    } else {
      this.getJobOffer();
    }
  },
};
</script>

<style>
</style>

