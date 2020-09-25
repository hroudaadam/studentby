<template>
  <div class="CustomerJobOfferDetail">
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="primary" :to="{name: 'CustomerJobOffers'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!jobOffer">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobOffer"></JobInfo>
          <b-card-text v-if="!!jobOffer.students && jobOffer.students.length > 0">
            <b>Přijaté přihlášky</b>
            <div class="mt-2">
              <b-list-group v-bind:key="student.id" v-for="student in jobOffer.students">
                <b-list-group-item class="d-flex justify-content-between align-items-center">
                  {{student.firstName}} {{student.lastName}}
                  <b-badge pill class="p-2" variant="success">Potvrzeno</b-badge>
                </b-list-group-item>
              </b-list-group>
            </div>
          </b-card-text>
        </b-card-body>
        <b-card-body>
          <b-button v-on:click="deleteJobOffer">Smazat</b-button>
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
  props: ["id"],
  components: {
    PageHeader,
    JobInfo,
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
        .get("/job-offers/" + this.id.toString())
        .then((response) => {
          this.jobOffer = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
          
        });
    },
    deleteJobOffer() {
      apiService
        .del("/job-offers/" + this.id.toString())
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

