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
  </div>
</template>

<script>
import apiService from "../../helpers/apiService";
import { mapGetters } from "vuex";
import JobListItem from "../../components/JobListItem";
import PageHeader from "../../components/PageHeader";
import router from "../../router";
import errorBox from "../../helpers/errorBox";

export default {
  name: "StudentJobOffers",
  components: {
    JobListItem,
    PageHeader,
  },
  data() {
    return {
      jobOffers: null
    };
  },
  methods: {
    // get job offers
    getJobOffers() {
      this.jobOffers = null;
      apiService
        .get("/job-offers")
        .then((response) => {
          this.jobOffers = response;
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
  },
  computed: {
    ...mapGetters(["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push({name: 'Login'});
    } else {
      this.getJobOffers();
    }
  },
};
</script>

<style>
</style>

