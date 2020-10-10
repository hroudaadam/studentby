<template>
  <div name="OperatorJobOffers">
    <PageHeader v-bind:title="'Nabídky'">
    </PageHeader>
    <div class="mt-2">
      <b-list-group>
        <JobListItem
          v-bind:key="jobOffer.jobOfferId"
          v-for="jobOffer in jobOffers"
          v-bind:job="jobOffer"
          v-bind:onClickLink="{ name: 'OperatorJobOfferDetail', params: {jobOfferId: jobOffer.jobOfferId}}"
        ></JobListItem>
      </b-list-group>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import JobListItem from "../../components/JobListItem";

import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "OperatorJobOffers",
  components: {
    JobListItem,
    PageHeader,
  },
  data() {
    return {
      jobOffers: null,
    };
  },
  methods: {
    // get job offers
    getJobOffers() {
      this.jobOffers = null;

      apiSevice
        .get("/job-offers")
        .then((response) => {
          this.jobOffers = response;
        })
        .catch((error) => {
          console.error(error.message);
        });
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getJobOffers();
    }
  },
};
</script>

<style>
</style>
