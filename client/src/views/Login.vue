<template>
  <div class="Login">
    <b-row>
      <b-col cols="0" lg="4"></b-col>
      <b-col cols="12" lg="4">
        <b-card no-body class="mx-auto text-center">
          <b-card-body>
            <b-card-title>Přihlášení</b-card-title>
            <b-form @submit.prevent>
              <b-form-group id="input-group-1">
                <b-form-input
                  id="input-1"
                  required
                  placeholder="Email"
                  v-model="formData.email"
                ></b-form-input>
              </b-form-group>

              <b-form-group id="input-group-2">
                <b-form-input
                  id="input-2"
                  type="password"
                  required
                  placeholder="Heslo"
                  v-model="formData.password"
                ></b-form-input>
              </b-form-group>

              <b-button
                block
                type="submit"
                variant="primary"
                class="mb-2"
                v-on:click="login()"
                >Přihlásit</b-button
              >
              <b-button
                block
                variant
                class="mb-2"
                :to="{ name: 'StudentRegister' }"
                >Vytvořit nový účet</b-button
              >

              <b-link :to="{ name: 'Home' }">Zapomněli jste heslo?</b-link>
            </b-form>
          </b-card-body>
        </b-card>
      </b-col>
      <b-col cols="0" lg="4"></b-col>
    </b-row>
  </div>
</template>

<script>
import { mapActions } from "vuex";
import apiSevice from "../helpers/apiService";
// import router from "../router";

export default {
  name: "Login",
  components: {},
  data() {
    return {
      formData: {
        email: null,
        password: null
      }      
    };
  },
  computed: {},
  methods: {
    ...mapActions(["loginStore"]),
    // login
    login() {
      apiSevice
        .post("/login", this.formData)
        .then((response) => {
          // store login
          this.loginStore(response);
        })
        .catch(() => {});
    },
  },
};
</script>
