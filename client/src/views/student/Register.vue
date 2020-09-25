<template>
  <div class="StudentRegister">
    <b-card no-body class="mx-auto text-center" style="width: 380px">
      <b-card-body>
        <b-card-title>Registrace</b-card-title>
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

          <b-form-group>
            <b-form-input
              required
              placeholder="Jméno"
              v-model="formData.firstName"
            ></b-form-input>
          </b-form-group>

          <b-form-group>
            <b-form-input
              required
              placeholder="Příjmení"
              v-model="formData.lastName"
            ></b-form-input>
          </b-form-group>

          <b-form-group>
            <b-form-input
              required
              placeholder="Země"
              v-model="formData.address.country"
            ></b-form-input>
          </b-form-group>

          <b-form-group>
            <b-form-input
              required
              placeholder="Město"
              v-model="formData.address.city"
            ></b-form-input>
          </b-form-group>

          <b-form-group>
            <b-form-input
              required
              placeholder="Ulice"
              v-model="formData.address.street"
            ></b-form-input>
          </b-form-group>

          <b-form-group>
            <b-form-input
              required
              placeholder="Číslo"
              v-model="formData.address.number"
            ></b-form-input>
          </b-form-group>

          <b-form-group>
            <b-form-datepicker
              placeholder="Datum narození"
              v-model="formData.dateOfBirth"
            ></b-form-datepicker>
          </b-form-group>

          <b-alert show variant="danger" v-if="!!this.errorMsg">{{
            this.errorMsg
          }}</b-alert>
          <b-button
            block
            type="submit"
            variant="primary"
            class="mb-2"
            v-on:click="register"
            >Registrovat</b-button
          >
        </b-form>
      </b-card-body>
    </b-card>
  </div>
</template>

<script>
// import { mapState, mapMutations, mapActions } from "vuex";
import apiService from "@/helpers/apiService";
import router from "@/router/index";

export default {
  name: "StudentRegister",
  components: {},
  data() {
    return {
      formData: {
        email: null,
        password: null,
        firstName: null,
        lastName: null,
        address: {
          country: null,
          city: null,
          street: null,
          number: null
        },
        dateOfBirth: null,
      },
      errorMsg: null,
    };
  },
  computed: {},
  methods: {
    register() {
      this.errorMsg = null;
      apiService
        .post("/students", this.formData)
        .then(() => {
          router.push({ name: "Login" });
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

