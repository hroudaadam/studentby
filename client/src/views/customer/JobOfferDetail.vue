<template>
  <div class="CustomerJobOfferDetail">
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="primary" :to="{name: 'CustomerJobOffers'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!jobOffer">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobOffer"></JobInfo>
          <hr/>
          <b-button v-on:click="deleteJobOffer" size="sm">Smazat</b-button>
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
import errorBox from '../../helpers/errorBox';

export default {
  name: "CustomerJobOfferDetail",
  props: {
    jobOfferId: Number
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
    getOfferDetail() {
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
    deleteJobOffer() {
      apiService
        .del("/job-offers/" + this.jobOfferId.toString())
        .then(() => {
          router.push({name: 'CustomerJobOffers'});
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    }
  },
  computed: {
    ...mapGetters("authentication", ["isCustomerLogged"]),
  },
  mounted() {
    if (!this.isCustomerLogged) {
      router.push({ name: "Login" });
    } else {
      this.getOfferDetail();
    }
  },
};
</script>

<style>
</style>

