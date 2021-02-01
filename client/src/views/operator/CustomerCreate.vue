<template>
  <div>
    <PageHeader v-bind:title="'Uživatelé'">
      <b-button variant="primary" :to="{name: 'OperatorGroupDetail', params: {groupId: this.groupId}}"
        >Zpět</b-button
      >
    </PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Email:" label-for="input-1">
          <b-form-input id="input-1" v-model="formData.email" type="text" placeholder="Email"></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-1" label="Skupina:" label-for="input-1">
          <b-form-input readonly id="input-1" v-model="groupName" type="text" placeholder="Skupina "></b-form-input>
        </b-form-group>
        <b-button type="submit" v-on:click="createCustomer" variant="primary">Vytvořit</b-button>
      </b-form>
    </b-card>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";

import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "OperatorCustomerCreate",
  components: {
    PageHeader
  },
  data() {
    return {
      formData: {
        email: null,
        firstName: null,
        lastName: null,
        groupId: this.groupId
      }      
    };
  },
  props: {
    groupId: Number,
    groupName: String
  },
  methods: {
    // create customer
    createCustomer() {
      apiSevice
        .post("/customers", this.formData)
        .then(() => {
          router.push({name: 'OperatorGroupDetail', params: {groupId: this.groupId}})
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
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
