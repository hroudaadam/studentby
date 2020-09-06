<template>
  <div class="StudentJobOffers">
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <div class="mt-2">
      <b-list-group>
        <JobListItem
          v-bind:key="jobOffer.id"
          v-for="jobOffer in jobOffers"
          v-bind:job="jobOffer"
          v-bind:onClickLink="{ name: 'StudentJobOfferDetail', params: {jobOfferId: jobOffer.jobOfferId}}"
        ></JobListItem>
      </b-list-group>
    </div>
    <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
  </div>
</template>

<script>
import apiService from "../../helpers/apiService";
import { mapGetters } from "vuex";
import JobListItem from "../../components/JobListItem";
import PageHeader from "../../components/PageHeader";
import router from "../../router";

export default {
  name: "StudentJobOffers",
  components: {
    JobListItem,
    PageHeader,
  },
  data() {
    return {
      jobOffers: null,
      errorMsg: null,
    };
  },
  methods: {
    getOffers() {
      this.errorMsg = null;
      this.jobOffers = null;
      apiService
        .get("/student/job-offers")
        .then((response) => {
          this.jobOffers = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getOffers();
    }
  },
};
</script>

<style>
</style>

