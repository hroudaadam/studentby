<template>
  <div name="OperatorCustomerCreate">
    <PageHeader v-bind:title="'Uživatelé'"></PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Email:" label-for="input-1">
          <b-form-input id="input-1" v-model="email" type="text" placeholder="Email"></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-1" label="Jméno:" label-for="input-1">
          <b-form-input id="input-1" v-model="firstName" type="text" placeholder="Jméno"></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-1" label="Příjmení:" label-for="input-1">
          <b-form-input id="input-1" v-model="lastName" type="text" placeholder="Příjmení"></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-1" label="Skupina:" label-for="input-1">
          <b-form-input readonly id="input-1" v-model="groupId" type="text" placeholder="Skupina "></b-form-input>
        </b-form-group>
        <b-alert show variant="danger" v-if="!!this.errorMsg">{{this.errorMsg}}</b-alert>
        <b-button type="submit" v-on:click="createCustomer" variant="primary">Vytvořit</b-button>
      </b-form>
    </b-card>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";
import PageHeader from "../../components/PageHeader";

export default {
  name: "OperatorCustomerCreate",
  components: {
    PageHeader
  },
  data() {
    return {
      errorMsg: null,
      email: null,
      firstName: null,
      lastName: null,
    };
  },
  props: {
    groupId: Number
  },
  methods: {
    createCustomer() {
      this.errorMsg = null;
      /* var body = {
        email: this.email,
        firstName: this.firstName,
        lastName: this.lastName,
        groupId: this.groupId
      }; */
      apiSevice
        .post("/customers", this.body)
        .then(() => {
          router.push({name: 'OperatorGroupDetail', params: {id: this.groupId}})
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isOperatorLogged"]),
    body: function () {
      return {
        email: this.email,
        firstName: this.firstName,
        lastName: this.lastName,
        groupId: this.groupId
      }
    }
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({name: 'Login'});
    }
  },
};
</script>

<style>
</style>
