<template>
  <div class="AppBar">
    <b-navbar toggleable="md" type="dark" variant="primary">
      <b-navbar-brand to="/">Studentby</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>  
          <b-nav-item v-if="isStudentLogged" :to="{name: 'StudentJobOffers'}">Brigády</b-nav-item>
          <b-nav-item v-if="isStudentLogged" :to="{name: 'StudentJobApplications'}">Přihlášky</b-nav-item>

          <b-nav-item v-if="isCustomerLogged" :to="{name: 'CustomerJobOffers'}" >Nabídky</b-nav-item>

          <b-nav-item v-if="isOperatorLogged" :to="{name: 'OperatorJobApplications'}" >Přihlášky</b-nav-item>
          <b-nav-item v-if="isOperatorLogged" :to="{name: 'OperatorGroups'}" >Skupiny</b-nav-item>


        </b-navbar-nav>

        <b-navbar-nav class="ml-auto">  
          
          
          <b-nav-item v-if="isUserLogged">Účet</b-nav-item>
          <b-nav-item v-if="!isUserLogged" :to="{name: 'Login'}" >Přihlásit</b-nav-item>
          <b-nav-item v-if="isUserLogged" v-on:click="logout">Odhlásit</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "AppBar",
  components: {
  },
  computed: {
    ...mapGetters("authentication", [
      "isUserLogged",
      "isStudentLogged",
      "isCustomerLogged",
      "isOperatorLogged",
      ])
  },
  methods: {
    ...mapActions("authentication", ["logout"])
  }
};
</script>

<style scoped>
</style>