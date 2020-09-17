<template>
  <div name="EmployeeOffers">
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="primary" :to="{name: 'CustomerJobOfferCreate'}">Nový</b-button>
    </PageHeader>
    <div class="mt-2">
      <b-list-group>
        <JobListItem
          v-bind:key="jobOffer.jobOfferId"
          v-for="jobOffer in jobOffers"
          v-bind:job="jobOffer"
          v-bind:onClickLink="{ name: 'CustomerJobOfferDetail', params: {id: jobOffer.jobOfferId}}"
        ></JobListItem>
      </b-list-group>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import PageHeader from "../../components/PageHeader";
import JobListItem from "../../components/JobListItem";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "EmployeeOffers",
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
    getAllOffers() {
      this.jobOffers = null;
      this.errorMsg = null;

      apiSevice
        .get("/job-offers")
        .then((response) => {
          this.jobOffers = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isCustomerLogged"]),
  },
  mounted() {
    if (!this.isCustomerLogged) {
      router.push({ name: "Login" });
    } else {
      this.getAllOffers();
    }
  },
};
</script>

<style>
</style>
