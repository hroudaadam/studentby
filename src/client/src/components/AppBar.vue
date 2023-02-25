<template>
  <div>
    <b-navbar toggleable="md" type="dark" variant="primary" fixed="top">
      <b-navbar-brand :to="{ name: 'Home' }">
        Studentby
        <!-- <span v-if="isStudentLogged" class="role-desc">student</span>
        <span v-if="isStudentInactLogged" class="role-desc">student - neaktivní</span>
        <span v-if="isOperatorLogged" class="role-desc">operátor</span>
        <span v-if="isCustomerLogged" class="role-desc">firma</span> -->
      </b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item v-if="isStudentLogged || isStudentInactLogged" :to="{ name: 'StudentJobOffers' }"
            >Nabídky</b-nav-item
          >
          <b-nav-item
            v-if="isStudentLogged"
            :to="{ name: 'StudentJobApplications' }"
            >Přihlášky</b-nav-item
          >

          <b-nav-item
            v-if="isCustomerLogged"
            :to="{ name: 'CustomerJobOffers' }"
            >Nabídky</b-nav-item
          >

          <b-nav-item
            v-if="isOperatorLogged"
            :to="{ name: 'OperatorJobOffers' }"
            >Nabídky</b-nav-item
          >
          <b-nav-item
            v-if="isOperatorLogged"
            :to="{ name: 'OperatorJobApplications' }"
            >Přihlášky</b-nav-item
          >
          <b-nav-item v-if="isOperatorLogged" :to="{ name: 'OperatorGroups' }"
            >Skupiny</b-nav-item
          >
          <b-nav-item v-if="isOperatorLogged" :to="{ name: 'OperatorStudents' }"
            >Studenti</b-nav-item
          >
        </b-navbar-nav>

        <b-navbar-nav class="ml-auto">
          <b-nav-item v-if="!isUserLogged" :to="{ name: 'Login' }">
            Přihlásit</b-nav-item
          >

          <b-nav-item-dropdown v-if="isUserLogged" right>
            <template v-slot:button-content>
              <b-icon icon="person-fill"></b-icon>
            </template>
            <b-dropdown-item
              v-on:click="openProfile()"
              v-if="isUserLogged && !isOperatorLogged"
              >Profil</b-dropdown-item
            >
            <b-dropdown-item v-on:click="logoutStore()"
              >Odhlásit</b-dropdown-item
            >
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "AppBar",
  components: {},
  computed: {
    ...mapGetters([
      "isUserLogged",
      "isStudentLogged",
      "isCustomerLogged",
      "isOperatorLogged",
      "isStudentInactLogged",
    ]),
  },
  methods: {
    ...mapActions(["logoutStore"]),
    openProfile() {
      if (this.isStudentLogged || this.isStudentInactLogged) {
        this.$router.push({ name: "StudentProfile" });
      } else if (this.isCustomerLogged)
        this.$router.push({ name: "CustomerProfile" });
    },
  },
};
</script>

<style scoped>
.role-desc {
  position: absolute;
  left: 20px;
  top: 38px;
  font-size: 0.5em;
  color: #c8d6f3;
}
</style>