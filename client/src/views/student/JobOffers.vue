<template>
  <div>
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <div class="mt-2">
      <b-list-group v-if="jobOffers && jobOffers.length > 0">
        <JobListItem
          v-bind:key="jobOffer.id"
          v-for="jobOffer in jobOffers"
          v-bind:job="jobOffer"
          v-bind:onClickLink="{
            name: 'StudentJobOfferDetail',
            params: { jobOfferId: jobOffer.jobOfferId },
          }"
        ></JobListItem>
      </b-list-group>
      <EmptyList v-else></EmptyList>
    </div>
  </div>
</template>

<script>
import JobListItem from "../../components/JobListItem";
import PageHeader from "../../components/PageHeader";
import EmptyList from "../../components/EmptyList";

import apiService from "../../helpers/apiService";
import { mapGetters } from "vuex";
import router from "../../router";

export default {
  name: "StudentJobOffers",
  components: {
    JobListItem,
    PageHeader,
    EmptyList,
  },
  data() {
    return {
      jobOffers: [],
    };
  },
  methods: {
    // get job offers
    getJobOffers() {
      this.jobOffers = [];
      apiService
        .get("/job-offers")
        .then((response) => {
          this.jobOffers = response;
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push({ name: "Login" });
    } else {
      this.getJobOffers();
    }
  },
};
</script>

<style>
</style>

