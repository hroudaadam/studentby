<template>
  <div name="OperatorGroupCreate">
    <PageHeader v-bind:title="'Skupiny'"></PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Název:" label-for="input-1">
          <b-form-input id="input-1" v-model="name" type="text" placeholder="Název"></b-form-input>
        </b-form-group>
        <b-alert show variant="danger" v-if="!!this.errorMsg">{{this.errorMsg}}</b-alert>
        <b-button type="submit" v-on:click="createGroup" variant="primary">Vytvořit</b-button>
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
  name: "OperatorGroupCreate",
  components: {
    PageHeader
  },
  data() {
    return {
      errorMsg: null,
      name: null
    };
  },
  methods: {
    createGroup() {
      this.errorMsg = null;
      var body = {
        name: this.name,
      };
      apiSevice
        .post("/groups", body)
        .then(() => {
          router.push({name: 'OperatorGroups'})
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
      router.push({name: 'Login'});
    }
  },
};
</script>

<style>
</style>
