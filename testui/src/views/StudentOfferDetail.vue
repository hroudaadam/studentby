<template>
  <div v-if="!!offerDetail">

    <b-card no-body>
      <b-card-body>
        <b-card-title>{{offerDetail.title}}</b-card-title>
        <b-card-sub-title class="mb-2">{{offerDetailTimeString}}</b-card-sub-title>
        
        <b-card-text>
          <b>Základní informace</b><br/>
          <ul>
            <li>Odměna: {{offerDetail.wage}} €</li>
            <li>Volná místa: {{offerDetail.spaces}}</li>
            <li>Zřizovatel: {{offerDetail.companyName}}</li>
          </ul>          
        </b-card-text>

        <b-card-text>
          <b>Podrobnosti</b><br/>
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
import { mapActions, mapGetters, mapState } from "vuex";
import router from "../router";

export default {
  name: "StudentOfferDetail",
  props: ["id"],
  components: {},
  methods: {
    ...mapActions("student", ["getOfferDetails", "applyForJobOffer"]),
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
    ...mapGetters("student", ["offerDetailTimeString"]),
    ...mapState("student", ["offerDetail"]),
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getOfferDetails(this.id);
    }
  },
};
</script>

<style>
</style>

