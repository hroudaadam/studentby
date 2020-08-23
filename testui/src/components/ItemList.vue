<template>
  <div class="ItemList">
    <div class="mt-2">
      <b-list-group v-bind:key="item.id" v-for="item in items">
        <slot name="itemSlot" v-bind="item"></slot>
      </b-list-group>
    </div>
  </div>
</template>

<script>
/* import { mapState, mapActions, mapGetters } from "vuex";
import StudentOfferItem from "../components/StudentOfferItem";
import router from "../router"; */

export default {
  name: "ItemList",
  props: ['items'],
  components: {
  },
  methods: {
    /* ...mapActions("student", ["getAllOffers"]), */
  },
  computed: {
/*     ...mapState("student", ["offers"]),
    ...mapGetters("authentication", ["isStudentLogged"]), */
  },
  mounted() {
/*     if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getAllOffers();
    } */
  },
};
</script>

<style>
</style>

