<template>
  <div name="OperatorGroupCreate">
    <PageHeader v-bind:title="'Skupiny'"></PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Název:" label-for="input-1">
          <b-form-input id="input-1" v-model="name" type="text" placeholder="Název"></b-form-input>
        </b-form-group>
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
import errorBox from "../../helpers/errorBox";

export default {
  name: "OperatorGroupCreate",
  components: {
    PageHeader
  },
  data() {
    return {    
      formData: {
        name: null,
      }
    };
  },
  methods: {
    // create group
    createGroup() {
      apiSevice
        .post("/groups", this.formData)
        .then(() => {
          router.push({name: 'OperatorGroups'})
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
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
