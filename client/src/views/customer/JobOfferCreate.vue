<template>
  <div name="CustomerJobOfferCreate">
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group label="Název:">
          <b-form-input
            v-model="formData.title"
            type="text"
            placeholder="Název"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Odměna:">
          <b-form-input
            v-model="formData.wage"
            type="number"
            placeholder="Odměna"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Počet míst:">
          <b-form-input
            v-model="formData.spaces"
            type="number"
            placeholder="Počet míst"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Začátek:">
          <b-form-input
            v-model="formData.start.date"
            type="date"
            placeholder="Datum"
          ></b-form-input>
          <b-form-input
            v-model="formData.start.time"
            type="time"
            placeholder="Čas"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Konec:">
          <b-form-input
            v-model="formData.end.date"
            type="date"
            placeholder="Datum"
          ></b-form-input>
          <b-form-input
            v-model="formData.end.time"
            type="time"
            placeholder="Čas"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Země:">
          <b-form-input
            v-model="formData.address.country"
            type="text"
            placeholder="Země"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Město:">
          <b-form-input
            v-model="formData.address.city"
            type="text"
            placeholder="Město"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Ulice:">
          <b-form-input
            v-model="formData.address.street"
            type="text"
            placeholder="Ulice"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Číslo:">
          <b-form-input
            v-model="formData.address.number"
            type="text"
            placeholder="Číslo"
          ></b-form-input>
        </b-form-group>

        <b-form-group label="Podrobný popis:">
          <b-form-textarea
            v-model="formData.description"
            placeholder="Podrobný popis"
            rows="5"
          ></b-form-textarea>
        </b-form-group>

        <b-button type="submit" v-on:click="createOffer" variant="primary"
          >Vytvořit</b-button
        >
      </b-form>
    </b-card>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";
import mixinService from "../../helpers/mixinService";
import errorBox from "../../helpers/errorBox";
import PageHeader from "../../components/PageHeader";

export default {
  name: "CustomerJobOfferCreate",
  components: {
    PageHeader,
  },
  data() {
    return {
      formData: {
        title: null,
        description: null,
        wage: null,
        spaces: null,
        start: {
          date: null,
          time: null,
        },
        end: {
          date: null,
          time: null,
        },
        address: {
          country: null,
          city: null,
          street: null,
          number: null,
        },
      },
    };
  },
  methods: {
    createOffer() {
/*       var body = {
        title: this.title,
        description: this.description,
        wage: this.wage,
        spaces: this.spaces,
        start: mixinService.dateToIsoString(this.startDate, this.startTime),
        end: mixinService.dateToIsoString(this.endDate, this.endTime),
      }; */

      this.formData.start = mixinService.dateToIsoString(this.formData.start.date, this.formData.start.time);
      this.formData.end = mixinService.dateToIsoString(this.formData.end.date, this.formData.end.time);

      apiSevice
        .post("/job-offers", this.formData)
        .then(() => {
          router.push({ name: "CustomerJobOffers" });
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isCustomerLogged"]),
  },
  mounted() {
    if (!this.isCustomerLogged) {
      router.push({ name: "Login" });
    }
  },
};
</script>

<style>
</style>
