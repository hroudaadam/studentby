<template>
  <div>
  <b-card
    :title=offerDetail.title
    tag="article"
    class="mb-2"
  >
    <b-card-text>
      {{offerDetail.description}}
    </b-card-text>

    <b-button href="#" variant="primary">Přihlásit se</b-button>
  </b-card>
</div>
</template>

<script>
import { mapActions, mapGetters, mapState } from "vuex";
import router from '../router';

export default {
  name: "StudentOfferDetail",
  props: ['id'],
  components: {
  },
  methods: {
       ...mapActions("student", ["getOfferDetails"])
  },
  computed: {
      ...mapGetters("authentication", ["isStudentLogged"]),
      ...mapState("student", ["offerDetail"])
  },
  mounted() {
      if (!this.isStudentLogged) {
        router.push('/login');
      }
      else {
        this.getOfferDetails(this.id);
      } 
      
  }
};
</script>

<style>
</style>

