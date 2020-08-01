<template>
  <div>
    <b-navbar toggleable="md" type="dark" variant="primary">
      <b-navbar-brand to="/">Studentby</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav class="ml-auto">          
          <b-nav-item v-if="isLoggedIn" to="/student">Portál studenta</b-nav-item>
          <b-nav-item v-if="false" to="/login">Portál firmy</b-nav-item>
          <b-nav-item v-if="false" to="/login">Portál operátora</b-nav-item>          
          <b-nav-item v-if="isLoggedIn">Účet</b-nav-item>

          <b-nav-item v-if="!isLoggedIn" to="/login">Přihlásit</b-nav-item>
          <b-nav-item v-if="isLoggedIn" v-on:click="logout">Odhlásit</b-nav-item>
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
    ...mapGetters("authentication", ["isLoggedIn"])
  },
  methods: {
    ...mapActions("authentication", ["logout"])
  }
};
</script>

<style scoped>
</style>