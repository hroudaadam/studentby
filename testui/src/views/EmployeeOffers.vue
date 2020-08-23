<template>
  <div name="EmployeeOffers">
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <ItemList v-bind:items="this.offers">
      <template v-slot:itemSlot="item">
        <EmployeeOfferItem v-bind:offer="item"></EmployeeOfferItem>
      </template>
    </ItemList>

    <b-button variant="primary" :to="{name: 'EmployeeCreateOffer'}">Vytvořit nabídku</b-button>
  </div>
</template>

<script>
import { mapState, mapActions, mapGetters } from "vuex";
import ItemList from "../components/ItemList";
import PageHeader from "../components/PageHeader";
import EmployeeOfferItem from "../components/EmployeeOfferItem";
import router from "../router";

export default {
  name: "EmployeeOffers",
  components: {
    ItemList,
    EmployeeOfferItem,
    PageHeader,
  },
  methods: {
    ...mapActions("employee", ["getAllOffers"]),
  },
  computed: {
    ...mapState("employee", ["offers"]),
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
