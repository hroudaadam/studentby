<template>
  <div>
    <PageHeader v-bind:title="'Uživatelé'">
      <b-button
        variant="primary"
        :to="{ name: 'OperatorGroupDetail', params: { groupId: this.groupId } }"
        >Zpět</b-button
      >
    </PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group label="Email:" >
          <b-form-input
            v-model="formData.email"
            type="text"
            placeholder="Email"
          ></b-form-input>
        </b-form-group>
        <b-form-group label="Pozice:" >
          <b-form-input
            v-model="formData.jobTitle"
            type="text"
            placeholder="Pozice"
          ></b-form-input>
        </b-form-group>
        <b-form-group label="Skupina:">
          <b-form-input
            readonly
            v-model="groupName"
            type="text"
            placeholder="Skupina "
          ></b-form-input>
        </b-form-group>
        <b-button type="submit" v-on:click="createCustomer" variant="primary"
          >Vytvořit</b-button
        >
      </b-form>
    </b-card>
    <b-modal
      id="modal-secret"
      title="Nastavení hesla"
      ok-only
      v-on:hidden="redirectBack()"
      v-on:close="redirectBack()"
      v-on:ok="redirectBack()"
    >
      <p class="my-4 text-break text-center">
        <router-link
          :to="{ name: 'SetPassword', params: { secret: secret } }"
        >
          Heslo lze nastavit zde
        </router-link>
      </p>
    </b-modal>
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
    PageHeader,
  },
  data() {
    return {
      formData: {
        email: null,
        jobTitle: null,
        groupId: this.groupId,
      },
      secret: null,
    };
  },
  props: {
    groupId: String,
    groupName: String,
  },
  methods: {
    // create customer
    createCustomer() {
      apiSevice
        .post("/employers", this.formData)
        .then((response) => {
          this.secret = response.changePasswordSecret;
          this.$bvModal.show("modal-secret");
        })
        .catch(() => {});
    },
    redirectBack() {
      this.$router.push({
        name: "OperatorGroupDetail",
        params: { groupId: this.groupId },
      });
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    }
  },
};
</script>

<style>
</style>
