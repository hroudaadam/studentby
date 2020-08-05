<template>
  <div name="EmployeeOffers">
    <ItemList v-bind:title="'Nabídky'" v-bind:items="this.offers">
        <template v-slot:itemSlot="item">
            <StudentOfferItem v-bind:offer="item"></StudentOfferItem>
        </template>
    </ItemList>

    <b-button variant="primary" :to="{name: 'EmployeeCreateOffer'}">Vytvořit nabídku</b-button>
  </div>
</template>

<script>
import { mapState, mapActions, mapGetters } from "vuex";
import ItemList from "../components/ItemList";
import StudentOfferItem from "../components/StudentOfferItem";
import router from "../router";

export default {
  name: "EmployeeOffers",
  components: {
    ItemList,
    StudentOfferItem,
  },
  methods: {
    ...mapActions("employee", ["getAllOffers"]),
  },
  computed: {
    ...mapState("employee", ["offers"]),
    ...mapGetters("authentication", ["isEmployeeLogged"])
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
