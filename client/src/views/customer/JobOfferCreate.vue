<template>
  <div name="CustomerJobOfferCreate">
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Název:" label-for="input-1">
          <b-form-input id="input-1" v-model="title" type="text" placeholder="Název"></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Odměna:" label-for="input-1">
          <b-form-input id="input-1" v-model="wage" type="number" placeholder="Odměna"></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Počet míst:" label-for="input-1">
          <b-form-input id="input-1" v-model="spaces" type="number" placeholder="Počet míst"></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Začátek:" label-for="input-1">
          <b-form-input id="input-1" v-model="startDate" type="date" placeholder="Datum"></b-form-input>
          <b-form-input id="input-1" v-model="startTime" type="time" placeholder="Čas"></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Konec:" label-for="input-1">
          <b-form-input id="input-1" v-model="endDate" type="date" placeholder="Datum"></b-form-input>
          <b-form-input id="input-1" v-model="endTime" type="time" placeholder="Čas"></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2" label="Podrobný popis:" label-for="input-2">
          <b-form-textarea id="input-2" v-model="description" placeholder="Podrobný popis" rows="5"></b-form-textarea>
        </b-form-group>

        <b-button type="submit" v-on:click="createOffer" variant="primary">Vytvořit</b-button>
      </b-form>
    </b-card>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";
import mixinService from '../../helpers/mixinService';
import PageHeader from "../../components/PageHeader";

export default {
  name: "CustomerJobOfferCreate",
  components: {
    PageHeader
  },
  data() {
    return {
      errorMsg: null,
      title: null,
      description: null,
      wage: null,
      spaces: null,
      startDate: null,
      startTime: null,
      endDate: null,
      endTime: null,
    };
  },
  methods: {
    createOffer() {
      this.errorMsg = null;
      var body = {
        title: this.title,
        description: this.description,
        wage: this.wage,
        spaces: this.spaces,
        start: mixinService.dateToIsoString(this.startDate, this.startTime),
        end: mixinService.dateToIsoString(this.endDate, this.endTime),
      };
      apiSevice
        .post("/job-offers", body)
        .then(() => {
          router.push({name:'CustomerJobOffers'});
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isCustomerLogged"]),
  },
  mounted() {
    if (!this.isCustomerLogged) {
      router.push({name: 'Login'});
    }
  },
};
</script>

<style>
</style>
