<template>
  <div class="OperatorJobOfferDetail">
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="primary" :to="{ name: 'OperatorJobOffers' }"
        >Zpět</b-button
      >
    </PageHeader>
    <div v-if="!!jobOffer">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobOffer"></JobInfo>
          <b-card-text
            v-if="
              !!jobOffer.jobApplications && jobOffer.jobApplications.length > 0
            "
          >
            <div class="mb-2"><h5>Studenti</h5></div>
            <b-list-group>
              <b-list-group-item
                v-bind:key="jobApplication.jobApplicationId"
                v-for="jobApplication in jobOffer.jobApplications"
                class="d-flex justify-content-between align-items-center"
                :to="{
                  name: 'OperatorJobApplicationResult',
                  params: {
                    jobApplicationId: jobApplication.jobApplicationId,
                    jobOfferId: jobOfferId,
                  },
                }"
              >
                <div class="text-wrap">
                  {{ jobApplication.student.firstName }}
                  {{ jobApplication.student.lastName }}
                </div>
                <JobApplicationState
                  v-bind:jobApplicationState="jobApplication.state"
                  v-bind:attendance="true"
                ></JobApplicationState>
              </b-list-group-item>
            </b-list-group>
          </b-card-text>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import JobInfo from "../../components/JobInfo";
import JobApplicationState from "../../components/JobApplicationState";

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
  name: "OperatorJobOfferDetail",
  props: {
    jobOfferId: Number,
  },
  components: {
    PageHeader,
    JobInfo,
    JobApplicationState,
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
        .get("/job-offers/" + this.jobOfferId.toString())
        .then((response) => {
          this.jobOffer = response;
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
      this.getJobOffer();
    }
  },
};
</script>

<style>
</style>

