<template>
  <div>
    <PageHeader v-bind:title="'Nabídky'"></PageHeader>
    <b-card>
      <b-form @submit.prevent>
        <b-form-group label="Název:">
          <b-form-input v-model="formData.title" type="text"></b-form-input>
        </b-form-group>

        <b-row>
          <b-col>
            <b-form-group label="Hodinová sazba:">
              <b-form-input
                v-model="formData.wage"
                type="number"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Počet míst:">
              <b-form-input
                v-model="formData.spaces"
                type="number"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>

        <b-form-group label="Začátek:">
          <b-row>
            <b-col>
              <b-form-input
                v-model="formData.start.date"
                type="date"
              ></b-form-input>
            </b-col>
            <b-col>
              <b-form-input
                v-model="formData.start.time"
                type="time"
              ></b-form-input>
            </b-col>
          </b-row>
        </b-form-group>

        <b-form-group label="Konec:">
          <b-row>
            <b-col>
              <b-form-input
                v-model="formData.end.date"
                type="date"
              ></b-form-input>
            </b-col>
            <b-col>
              <b-form-input
                v-model="formData.end.time"
                type="time"
              ></b-form-input>
            </b-col>
          </b-row>
        </b-form-group>

        <b-row>
          <b-col>
            <b-form-group label="Země:">
              <b-form-input
                v-model="formData.address.country"
                type="text"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Město:">
              <b-form-input
                v-model="formData.address.city"
                type="text"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row>
          <b-col>
            <b-form-group label="Ulice:">
              <b-form-input
                v-model="formData.address.street"
                type="text"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Číslo:">
              <b-form-input
                v-model="formData.address.number"
                type="text"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>

        <b-form-group label="Podrobný popis:">
          <b-form-textarea
            v-model="formData.description"
            rows="3"
          ></b-form-textarea>
        </b-form-group>

        <b-button type="submit" v-on:click="createJobOffer()" variant="primary"
          >Vytvořit</b-button
        >
      </b-form>
    </b-card>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";

import { mapGetters, mapState } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";
import mixinService from "../../helpers/mixinService";

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
    // create job offer
    createJobOffer() {
      // date format
      this.formData.start = mixinService.getDateIsoString(
        this.formData.start.date,
        this.formData.start.time
      );
      this.formData.end = mixinService.getDateIsoString(
        this.formData.end.date,
        this.formData.end.time
      );

      apiSevice
        .post("/job-offers", this.formData)
        .then(() => {
          router.push({ name: "CustomerJobOffers" });
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isCustomerLogged"]),
    ...mapState(["countries"])

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