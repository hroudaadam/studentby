<template>
  <div class="CustomerJobOfferDetail">
    <PageHeader v-bind:title="'Nabídky'">
      <b-button variant="primary" :to="{name: 'CustomerJobOffers'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!jobOffer">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobOffer"></JobInfo>
        </b-card-body>
        <b-card-body>
          <b>Přijaté přihlášky</b>
          <div class="mt-2">
            <b-list-group
              v-bind:key="jobApplication.id"
              v-for="jobApplication in jobOffer.approvedJobApplications"
            >
              <b-list-group-item class="d-flex justify-content-between align-items-center">
                {{jobApplication.studentFirstName}} {{jobApplication.studentLastName}}
                <b-badge pill class="p-2" variant="success">Potvrzeno</b-badge>
              </b-list-group-item>
            </b-list-group>
          </div>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from '../../components/PageHeader';
import JobInfo from '../../components/JobInfo';

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
  name: "CustomerJobOfferDetail",
  props: ["id"],
  components: {
    PageHeader,
    JobInfo
  },
  data() {
    return {
      jobOffer: null,
      errorMsg: null,
    };
  },
  methods: {
    getOfferDetail() {
      apiService
        .get("/customer/job-offers/" + this.id.toString())
        .then((response) => {
          this.jobOffer = response;
          console.log(response);
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
      router.push("/login");
    } else {
      this.getOfferDetail();
    }
  },
};
</script>

<style>
</style>

