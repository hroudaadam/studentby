<template>
  <div class="StudentRegister">
    <b-container>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Email address:" label-for="input-1">
          <b-form-input
            id="input-1"
            required
            placeholder="Email"
            v-model="email"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2" label="Password:" label-for="input-2">
          <b-form-input
            id="input-2"
            type="password"
            required
            placeholder="Password"
            v-model="password"
          ></b-form-input>
        </b-form-group>

        <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
        <b-alert show variant="success" v-if="response" v-html="response"></b-alert>

        <b-button type="submit" variant="primary" v-on:click="register">Registrovat</b-button>
      </b-form>
    </b-container>
  </div>
</template>

<script>
// import { mapState, mapMutations, mapActions } from "vuex";
import apiService from '../../helpers/apiService';

export default {
  name: "StudentRegister",
  components: {

  },
  data() {
      return {
          email: null,
          password: null,
          errorMsg: null,
          response: null
      }
  },
  computed: {
  },
  methods: {
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

