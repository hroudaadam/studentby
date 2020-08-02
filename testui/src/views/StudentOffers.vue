<template>
  <div class="StudentOffers">
    <div class="mb-4">
      <h2 class="text-center">Brigády</h2>
    </div>
    <div>
      <b-list-group v-bind:key="offer.id" v-for="offer in offers">
        <StudentOfferItem v-bind:offer="offer"></StudentOfferItem>
      </b-list-group>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions, mapGetters } from "vuex";
import StudentOfferItem from "../components/StudentOfferItem";
import router from "../router";

export default {
  name: "StudentOffers",
  components: {
    StudentOfferItem,
  },
  methods: {
    ...mapActions("student", ["getAllOffers"]),
  },
  computed: {
    ...mapState("student", ["offers"]),
    ...mapGetters("authentication", ["isStudentLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getAllOffers();
    }
  },
};
</script>

<style>
</style>

