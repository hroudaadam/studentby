<template>
  <div v-if="!!offer">
    <b-card no-body>
      <b-card-body>
        <b-card-title>{{offer.title}}</b-card-title>
        <b-card-sub-title class="mb-2">Čas</b-card-sub-title>

        <b-card-text>
          <b class="mb-2">Základní informace</b>
          <p>
            Odměna: {{offer.wage}} € <br>
            Volná místa: {{offer.spaces}} <br>
          </p>
        </b-card-text>

        <b-card-text>
          <b class="mb-2">Podrobnosti</b>
          <p>{{offer.description}}</p>
        </b-card-text>

        <b>Přijaté přihlášky</b>
        <ItemList v-bind:items="this.offer.approvedJobApplications">
          <template v-slot:itemSlot="item">
            <EmployeeApplicationItem v-bind:application="item"></EmployeeApplicationItem>
          </template>
        </ItemList>
      </b-card-body>
    </b-card>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import router from "../router";
import apiService from "../helpers/apiService";
import ItemList from "../components/ItemList";
import EmployeeApplicationItem from "../components/EmployeeApplicationItem";

export default {
  name: "EmployeeOfferDetail",
  props: ["id"],
  components: {
    ItemList,
    EmployeeApplicationItem,
  },
  data() {
    return {
      offer: null,
      getOfferDetailError: null,
    };
  },
  methods: {
    getOfferDetail() {
      apiService
        .get("/employee/job-offers/" + this.id.toString())
        .then((response) => {
          this.offer = response;
          console.log(response);
        })
        .catch((error) => {
          this.getOfferDetailError = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isEmployeeLogged"]),
  },
  mounted() {
    if (!this.isEmployeeLogged) {
      router.push("/login");
    } else {
      this.getOfferDetail();
    }
  },
};
</script>

<style>
</style>

