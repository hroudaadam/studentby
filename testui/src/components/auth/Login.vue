<template>
  <div>
    <b-container>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Email address:" label-for="input-1">
          <b-form-input
            id="input-1"
            required
            placeholder="Email"
            :value="loginEmail"
            @input="setLoginEmail"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2" label="Password:" label-for="input-2">
          <b-form-input
            id="input-2"
            type="password"
            required
            placeholder="Password"
            :value="loginPassword"
            @input="setLoginPassword"
          ></b-form-input>
        </b-form-group>

        <b-alert show variant="danger" v-if="!!loginError">{{loginError}}</b-alert>

        <b-button type="submit" variant="primary" v-on:click="login">Přihlásit</b-button>
      </b-form>
    </b-container>
  </div>
</template>

<script>
import { mapState, mapMutations, mapActions } from "vuex";

export default {
  name: "Login",
  components: {},
  computed: {
    ...mapState("authentication", ["loginEmail", "loginPassword", "loginError"]),
  },
  methods: {
    ...mapMutations("authentication", ["setLoginEmail", "setLoginPassword"]),
    ...mapActions("authentication", ["login"])
  },
};
</script>

<style scoped></style>

