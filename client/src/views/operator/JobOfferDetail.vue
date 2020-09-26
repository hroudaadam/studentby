<template>
  <div class="OperatorJobOfferDetail">
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="primary" :to="{name: 'OperatorJobOffers'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!jobOffer">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobOffer"></JobInfo>
          <b-card-text v-if="!!jobOffer.jobApplications && jobOffer.jobApplications.length > 0">
            <b>Studenti</b>
            <div class="mt-2">
              <b-list-group v-bind:key="jobApplication.jobApplicationId" v-for="jobApplication in jobOffer.jobApplications">
                <b-list-group-item class="d-flex justify-content-between align-items-center" :to="{name: 'OperatorJobApplicationResult', params: {jobApplicationId: jobApplication.jobApplicationId, jobOfferId: jobOfferId}}">
                  {{jobApplication.student.firstName}} {{jobApplication.student.lastName}}
                </b-list-group-item>
              </b-list-group>
            </div>
          </b-card-text>
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
  name: "OperatorJobOfferDetail",
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
    }
  },
  computed: {
    ...mapGetters("authentication", ["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getOfferDetail();
    }
  },
};
</script>

<style>
</style>

