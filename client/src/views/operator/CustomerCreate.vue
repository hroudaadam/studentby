<template>
  <div name="OperatorCustomerCreate">
    <PageHeader v-bind:title="'Uživatelé'"></PageHeader>
    <b-card>
    GroupId: {{groupId}}
      <!-- <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Název:" label-for="input-1">
          <b-form-input id="input-1" v-model="title" type="text" placeholder="Název"></b-form-input>
        </b-form-group>
        <b-alert show variant="danger" v-if="!!this.errorMsg">{{this.errorMsg}}</b-alert>
        <b-button type="submit" v-on:click="createGroup" variant="primary">Vytvořit</b-button>
      </b-form> -->
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
      errorMsg: null
    };
  },
  props: {
    groupId: Number
  },
  methods: {
    createCustomer() {
      this.errorMsg = null;
      var body = {
        title: this.title,
      };
      apiSevice
        .post("/operator/customers", body)
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
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push("/login");
    }
  },
};
</script>

<style>
</style>
