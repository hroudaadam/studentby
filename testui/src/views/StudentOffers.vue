<template>
  <div class="StudentOffers">
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <ItemList v-bind:items="this.offers">
      <template v-slot:itemSlot="item">
        <StudentOfferItem v-bind:offer="item"></StudentOfferItem>
      </template>
    </ItemList>
    <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
  </div>
</template>

<script>
import apiService from "../helpers/apiService";
import { mapGetters } from "vuex";
import StudentOfferItem from "../components/StudentOfferItem";
import PageHeader from '../components/PageHeader';
import ItemList from "../components/ItemList";
import router from "../router";

export default {
  name: "StudentOffers",
  components: {
    StudentOfferItem,
    ItemList,
    PageHeader,
  },
  data() {
    return {
      offers: null,
      errorMsg: null
    };
  },
  methods: {
    getAllOffers() {
      this.errorMsg = null;
      this.offers = null;
      apiService
        .get("/student/job-offers")
        .then((response) => {
          this.offers = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
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

