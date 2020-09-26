<template>
  <div class="OperatorJobApplicationResult">
    <PageHeader v-bind:title="'Výsledek'">
      <b-button
        variant="primary"
        :to="{
          name: 'OperatorJobOfferDetail',
          params: { jobOfferId: jobOfferId },
        }"
        >Zpět</b-button
      >
    </PageHeader>
    <div v-if="!!this.jobApplication">
      <b-card no-body>
        <b-card-body>
          <b-card-text>
            <b class="mb-2">Student</b>
            <p>
              Jméno: {{ jobApplication.student.firstName }}
              {{ jobApplication.student.lastName }} <br />Datum narození:
              {{ dateOfBirth }} <br />Adresa: město
              <br />
            </p>
            <b-button variant="success" class="mr-2" size="sm" v-on:click="editJobApplication(true)">Účast</b-button>
            <b-button variant="danger" size="sm" v-on:click="editJobApplication(false)">Absence</b-button>
          </b-card-text>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";

import { mapGetters, mapState } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";
import mixinService from "../../helpers/mixinService";
import errorBox from "../../helpers/errorBox";

export default {
  name: "OperatorJobApplicationResult",
  props: {
    jobApplicationId: Number,
    jobOfferId: Number,
  },
  components: {
    PageHeader,
  },
  data() {
    return {
      jobApplication: null,
    };
  },
  methods: {
    getJobApplication() {
      this.jobApplication = null;

      apiService
        .get(
          "/job-applications/" + this.jobApplicationId.toString() + "/result"
        )
        .then((response) => {
          this.jobApplication = response;
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
    editJobApplication(attended) {
      var state = attended
        ? this.jobApplicationStates.attended
        : this.jobApplicationStates.absent;
      var body = {
        jobApplicationId: this.jobApplicationId,
        state: state,
      };
      console.log(body);

      apiService
        .put("/job-applications/" + this.jobApplicationId.toString(), body)
        .then(() => {
          router.push({
            name: "OperatorJobOfferDetail",
            params: { jobOfferId: this.jobOfferId },
          });
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isOperatorLogged"]),
    ...mapState(["jobApplicationStates"]),
    dateOfBirth: function () {
      return mixinService.dateOfBirthToString(
        this.jobApplication.student.dateOfBirth
      );
    },
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getJobApplication();
    }
  },
};
</script>

<style>
</style>

