<template>
  <div class="StudentRegister">
    <b-card no-body class="mx-auto text-center" style="width: 380px">
      <b-card-body>
        <b-card-title>Registrace</b-card-title>
        <b-form @submit.prevent>
          <b-form-group id="input-group-1">
            <b-form-input id="input-1" required placeholder="Email" v-model="email"></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-2">
            <b-form-input
              id="input-2"
              type="password"
              required
              placeholder="Heslo"
              v-model="password"
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-3">
            <b-form-input id="input-3" required placeholder="Jméno" v-model="firstName"></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-4">
            <b-form-input id="input-4" required placeholder="Příjmení" v-model="lastName"></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-5">
            <b-form-datepicker id="input-4" placeholder="Datum narození" v-model="dateOfBirth"></b-form-datepicker>
          </b-form-group>

          

          <b-alert show variant="danger" v-if="!!this.errorMsg">{{this.errorMsg}}</b-alert>
          <b-button
            block
            type="submit"
            variant="primary"
            class="mb-2"
            v-on:click="register"
          >Registrovat</b-button>
        </b-form>
      </b-card-body>
    </b-card>
  </div>
</template>

<script>
// import { mapState, mapMutations, mapActions } from "vuex";
import apiService from "../../helpers/apiService";

export default {
  name: "StudentRegister",
  components: {},
  data() {
    return {
      email: null,
      password: null,
      firstName: null,
      lastName: null,
      dateOfBirth: null,
      errorMsg: null,
    };
  },
  computed: {},
  methods: {
    register() {
      var body = {
        email: this.email,
        password: this.password,
        firstName: this.firstName,
        lastName: this.lastName,
        dateOfBirth: this.dateOfBirth
      };
      this.errorMsg = null;
      apiService
        .post("/student", body)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
};
</script>

<style>
</style>

