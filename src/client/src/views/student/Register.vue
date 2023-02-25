<template>
  <div>
    <PageHeader v-bind:title="'Registrace'"></PageHeader>

    <b-card>
      <b-form @submit.prevent>
        <b-row>
          <b-col>
            <b-form-group label="Email">
              <b-form-input v-model="formData.email"></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Heslo">
              <b-form-input
                type="password"
                v-model="formData.password"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row>
          <b-col>
            <b-form-group label="Jméno">
              <b-form-input
                v-model="formData.firstName"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Příjmení">
              <b-form-input required v-model="formData.lastName"></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row>
          <b-col>
            <b-form-group label="Země">
              <b-form-input
                v-model="formData.address.country"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Město">
              <b-form-input
                v-model="formData.address.city"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row>
          <b-col>
            <b-form-group label="Ulice">
              <b-form-input
                v-model="formData.address.street"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Číslo">
              <b-form-input
                v-model="formData.address.number"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>

        <b-form-group label="Datum narození:">
          <b-row>
            <b-col>
              <b-form-input
                v-model="formData.dateOfBirth"
                type="date"
              ></b-form-input>
            </b-col>
          </b-row>
        </b-form-group>

        <b-button
          block
          type="submit"
          variant="primary"
          class="mt-3"
          v-on:click="register"
          >Registrovat</b-button
        >
      </b-form>
    </b-card>
  </div>
</template>

<script>
import apiService from "../../helpers/apiService";
import PageHeader from "../../components/PageHeader";

export default {
  name: "StudentRegister",
  components: {
    PageHeader,
  },
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
          number: null,
        },
        dateOfBirth: null,
      }
    };
  },
  computed: {},
  methods: {
    // register
    register() {
        apiService
          .post("/students", this.formData)
          .then(() => {
            this.$router.push({ name: "Login" });
          })
          .catch(() => {});
    }
  },
};
</script>

<style scoped>
input[type="date"]:before {
  color: lightgray;
  content: attr(placeholder);
}

input[type="date"].full:before {
  color: black;
  content: "" !important;
}
</style>

