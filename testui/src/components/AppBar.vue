<template>
  <div>
    <b-navbar toggleable="md" type="dark" variant="primary">
      <b-navbar-brand to="/">Studentby</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
      <b-collapse id="nav-collapse" is-nav>

        <b-navbar-nav class="ml-auto">  
          <b-nav-item v-if="isStudentLogged" :to="{name: 'StudentOffers'}">Brigády</b-nav-item>
          <b-nav-item v-if="isStudentLogged" :to="{name: 'StudentApplications'}">Přihlášky</b-nav-item>

          <b-nav-item v-if="isEmployeeLogged" >Brigády</b-nav-item>
          <b-nav-item v-if="isEmployeeLogged">Přihlášky</b-nav-item>

          <b-nav-item v-if="isOperatorLogged">Portál operátora</b-nav-item>  
          
          <b-nav-item v-if="isUserLoggedIn">Účet</b-nav-item>
          <b-nav-item v-if="!isUserLoggedIn" :to="{name: 'Login'}" >Přihlásit</b-nav-item>
          <b-nav-item v-if="isUserLoggedIn" v-on:click="logout">Odhlásit</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Appbar",
  components: {
  },
  computed: {
    ...mapGetters("authentication", [
      "isUserLoggedIn",
      "isStudentLogged",
      "isEmployeeLogged",
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