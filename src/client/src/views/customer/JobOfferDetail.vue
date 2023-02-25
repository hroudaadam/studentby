<template>
  <div>
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="secondary" :to="{ name: 'CustomerJobOffers' }" size="sm"
        >Zpět</b-button
      >
    </PageHeader>
    <div v-if="!!jobOffer">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobOffer"></JobInfo>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import JobInfo from "../../components/JobInfo";

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
  name: "CustomerJobOfferDetail",
  props: {
    jobOfferId: String,
  },
  components: {
    PageHeader,
    JobInfo,
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
    // delete job offer
    deleteJobOffer() {
      apiService
        .del("/job-offers/" + this.jobOfferId)
        .then(() => {
          router.push({ name: "CustomerJobOffers" });
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isCustomerLogged"]),
  },
  mounted() {
    if (!this.isCustomerLogged) {
      router.push({ name: "Login" });
    } else {
      this.getJobOffer();
    }
  },
};
</script>

<style>
</style>

