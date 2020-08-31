<template>
  <div class="Login">
    <b-card no-body class="mx-auto text-center" style="width: 380px">
      <b-card-body>
        <b-card-title>Přihlášení</b-card-title>
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

          <b-alert show variant="danger" v-if="!!this.errorMsg">{{this.errorMsg}}</b-alert>
          <b-button
            block
            type="submit"
            variant="primary"
            class="mb-2"
            v-on:click="this.login"
          >Přihlásit</b-button>
          <b-button block variant class="mb-2" :to="{name: 'StudentRegister'}">Vytvořit nový účet</b-button>

          <b-link :to="{name: 'Home'}">Zapomněli jste heslo?</b-link>
        </b-form>
        
      </b-card-body>
    </b-card>
  </div>
</template>

<script>
import { mapMutations } from "vuex";
import apiSevice from "../helpers/apiService";
import router from "../router";

export default {
  name: "Login",
  components: {},
  data() {
    return {
      email: null,
      password: null,
      errorMsg: null,
    };
  },
  computed: {},
  methods: {
    ...mapMutations("authentication", ["setAccessToken", "setUserRole"]),
    login() {
      this.errorMsg = null;
      var body = {
        email: this.email,
        password: this.password,
      };
      apiSevice
        .post("/login", body)
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

