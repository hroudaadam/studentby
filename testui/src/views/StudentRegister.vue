<template>
  <div>
    <b-container>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Email address:" label-for="input-1">
          <b-form-input
            id="input-1"
            required
            placeholder="Email"
            v-model="registerEmail"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2" label="Password:" label-for="input-2">
          <b-form-input
            id="input-2"
            type="password"
            required
            placeholder="Password"
            v-model="registerPassword"
          ></b-form-input>
        </b-form-group>

        <b-alert show variant="danger" v-if="registerError" v-html="registerError"></b-alert>
        <b-alert show variant="success" v-if="registerResponse" v-html="registerResponse"></b-alert>

        <b-button type="submit" variant="primary" v-on:click="register">Registrovat</b-button>
      </b-form>
    </b-container>
  </div>
</template>

<script>
// import { mapState, mapMutations, mapActions } from "vuex";
import apiService from '../helpers/apiService';

export default {
  name: "StudentRegister",
  components: {

  },
  data() {
      return {
          registerEmail: null,
          registerPassword: null,
          registerError: null,
          registerResponse: null
      }
  },
  computed: {
    //...mapState("authentication", ["loginEmail", "loginPassword", "loginError"]),
  },
  methods: {
    //...mapMutations("authentication", ["setLoginEmail", "setLoginPassword"]),
    //...mapActions("authentication", ["login"])
    register() {
        this.registerError = null;
        this.registerResponse = null;
        apiService.get('/test')
        .then((response) => {
            this.registerResponse = response;
        })
        .catch((error) => {
            this.registerError = error.message;
        })
    }
  },
};
</script>

<style>
</style>

