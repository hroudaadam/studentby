<template>
  <div>
    <PageHeader v-bind:title="'Skupiny'"></PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group label="Název:">
          <b-form-input
            v-model="formData.name"
            type="text"
            placeholder="Název"
          ></b-form-input>
        </b-form-group>
        <b-button type="submit" v-on:click="createGroup" variant="primary"
          >Vytvořit</b-button
        >
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
  name: "OperatorGroupCreate",
  components: {
    PageHeader,
  },
  data() {
    return {
      formData: {
        name: null,
      },
    };
  },
  methods: {
    // create group
    createGroup() {
      apiSevice
        .post("/groups", this.formData)
        .then(() => {
          router.push({ name: "OperatorGroups" });
        })
        .catch(() => {});
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
