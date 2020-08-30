<template>
  <div>
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

        <b-alert show variant="danger" v-if="!!this.errorMsg">{{this.errorMsg}}</b-alert>

        <b-button type="submit" variant="primary" v-on:click="this.login">Přihlásit</b-button>
      </b-form>
    </b-container>
  </div>
</template>

<script>
import { mapMutations } from "vuex";
import apiSevice from '../helpers/apiService';
import router from "../router";

export default {
  name: "Login",
  components: {},
  data() {
    return {
      email: null,
      password: null,
      errorMsg: null
    }
  },
  computed: {
  },
  methods: {
    ...mapMutations("authentication", ["setAccessToken", "setUserRole"]),
    login() {
      this.errorMsg = null;
      var body = {
          email: this.email,
          password: this.password
      }
      apiSevice.post("/login", body)
        .then((response) => {
          this.setAccessToken(response.token);
          this.setUserRole(response.role);
          router.push("/");
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
};
</script>

<style scoped></style>

