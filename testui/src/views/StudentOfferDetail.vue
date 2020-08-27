<template>
  <div class="StudentOfferDetail">
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <div v-if="!!this.offerDetail">
      <b-card no-body>
        <b-card-body>
          <b-card-title>{{offerDetail.title}}</b-card-title>
          <!-- <b-card-sub-title class="mb-2">{{offerDetailTimeString}}</b-card-sub-title> -->
          <b-card-sub-title class="mb-2">{{start + ' - ' + end}}</b-card-sub-title>

          <b-card-text>
            <b>Základní informace</b>
            <br />
            <ul>
              <li>Odměna: {{offerDetail.wage}} Kč</li>
              <li>Volná místa: {{offerDetail.freeSpaces}} z {{offerDetail.spaces}}</li>
              <li>Zřizovatel: {{offerDetail.companyName}}</li>
            </ul>
          </b-card-text>

          <b-card-text>
            <b>Podrobnosti</b>
            <br />
            {{offerDetail.description}}
          </b-card-text>
        </b-card-body>

        <b-card-body>
          <b-button v-on:click="createJobApplication()" variant="primary">Přihlásit se</b-button>
        </b-card-body>
      </b-card>

      <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import PageHeader from '../components/PageHeader';
import router from "../router";
import apiService from "../helpers/apiService";
import mixinService from "../helpers/mixinService";

export default {
  name: "StudentOfferDetail",
  props: ["id"],
  components: {
    PageHeader
  },
  data() {
    return {
      offerDetail: null,
      errorMsg: null,
    };
  },
  methods: {
    getOfferDetail() {
      this.offerDetail = null;
      this.errorMsg = null;
      apiService
        .get("/student/job-offers/" + this.id.toString())
        .then((response) => {
          this.offerDetail = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
    createJobApplication() {
      this.errorMsg = null;
      var body = { jobofferid: this.id };

      apiService
        .post("/student/job-applications", body)
        .then((response) => {
          console.log(response);
          router.push({ name: "StudentJobApplications" });
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
    start: function () {
      return mixinService.dateToString(this.offerDetail.start);
    },
    end: function () {
      return mixinService.dateToString(this.offerDetail.end);
    },
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getOfferDetail();
    }
  },
};
</script>

<style>
</style>

