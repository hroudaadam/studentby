<template>
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
            <li>Odměna: {{offerDetail.wage}} €</li>
            <li>Volná místa: {{offerDetail.spaces}}</li>
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
        <b-button v-on:click="applyForJobOffer(id)" variant="primary">Přihlásit se</b-button>
      </b-card-body>
    </b-card>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import router from "../router";
import apiService from "../helpers/apiService";
import mixinService from "../helpers/mixinService";

export default {
  name: "StudentOfferDetail",
  props: ["id"],
  components: {},
  data() {
    return {
      offerDetail: null,
      errorMsg: null,
    };
  },
  methods: {
    ...mapActions("student", ["applyForJobOffer"]),
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
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
    start: function () {
      return mixinService.dateToString(this.offerDetail.start)
    },
    end: function () {
      return mixinService.dateToString(this.offerDetail.end)
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

