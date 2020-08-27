<template>
  <div name="EmployeeOffers">
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <ItemList v-bind:items="this.jobOffers">
      <template v-slot:itemSlot="item">
        <EmployeeOfferItem v-bind:offer="item"></EmployeeOfferItem>
      </template>
    </ItemList>

    <b-button variant="primary" :to="{name: 'EmployeeCreateOffer'}">Vytvořit nabídku</b-button>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import ItemList from "../components/ItemList";
import PageHeader from "../components/PageHeader";
import EmployeeOfferItem from "../components/EmployeeOfferItem";
import router from "../router";
import apiSevice from "../helpers/apiService";

export default {
  name: "EmployeeOffers",
  components: {
    ItemList,
    EmployeeOfferItem,
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
        .get("/employee/job-offers")
        .then((response) => {
          this.jobOffers = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
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
      this.getAllOffers();
    }
  },
};
</script>

<style>
</style>
