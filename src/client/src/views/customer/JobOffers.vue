<template>
  <div>
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="primary" :to="{name: 'CustomerJobOfferCreate'}">Přidat</b-button>
    </PageHeader>
    <div class="mt-2">
      <b-list-group v-if="jobOffers && jobOffers.length > 0">
        <JobListItem
          v-bind:key="jobOffer.id"
          v-for="jobOffer in jobOffers"
          v-bind:job="jobOffer"
          v-bind:onClickLink="{ name: 'CustomerJobOfferDetail', params: {jobOfferId: jobOffer.id}}"
        ></JobListItem>
      </b-list-group>
      <EmptyList v-else></EmptyList>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import JobListItem from "../../components/JobListItem";
import EmptyList from "../../components/EmptyList";

import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "CustomerJobOffers",
  components: {
    JobListItem,
    PageHeader,
    EmptyList
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
        .get("/job-offers?page=1&pageSize=1000")
        .then((response) => {
          this.jobOffers = response.data;
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
      this.getJobOffers();
    }
  },
};
</script>

<style>
</style>
