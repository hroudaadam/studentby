<template>
  <div name="EmployeeCreateOffer">
    <b-card>
      <b-form @submit.prevent>
        <b-form-group id="input-group-1" label="Název:" label-for="input-1">
          <b-form-input
            id="input-1"
            :value="newOffer.title"
            @input="setNewTitle"
            type="text"
            placeholder="Název"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Odměna:" label-for="input-1">
          <b-form-input
            id="input-1"
            :value="newOffer.wage"
            @input="setNewWage"
            type="number"
            placeholder="Odměna"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Počet míst:" label-for="input-1">
          <b-form-input
            id="input-1"
            :value="newOffer.spaces"
            @input="setNewSpaces"
            type="number"
            placeholder="Počet míst"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Začátek:" label-for="input-1">
          <b-form-input
            id="input-1"
            :value="newOffer.start.date"
            @input="setNewStartDate"
            type="date"
            placeholder="Datum"
          ></b-form-input>
          <b-form-input
            id="input-1"
            :value="newOffer.start.time"
            @input="setNewStartTime"
            type="time"
            placeholder="Čas"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Konec:" label-for="input-1">
          <b-form-input
            id="input-1"
            :value="newOffer.end.date"
            @input="setNewEndDate"
            type="date"
            placeholder="Datum"
          ></b-form-input>
          <b-form-input
            id="input-1"
            :value="newOffer.end.time"
            @input="setNewEndTime"
            type="time"
            placeholder="Čas"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2" label="Podrobný popis:" label-for="input-2">
          <b-form-textarea
            id="input-2"
            :value="newOffer.description"
            @input="setNewDescription"
            placeholder="Podrobný popis"
            rows="5"
          ></b-form-textarea>
        </b-form-group>

        <b-button type="submit" v-on:click="createOffer" variant="primary">Vytvořit</b-button>
      </b-form>
    </b-card>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations, mapActions } from "vuex";
import router from "../router";

export default {
  name: "EmployeeCreateOffer",
  components: {},
  methods: {
    ...mapActions("employee", ["createOffer"]),
    ...mapMutations("employee", [
      "setNewTitle",
      "setNewDescription",
      "setNewWage",
      "setNewSpaces",
      "setNewStartDate",
      "setNewStartTime",
      "setNewEndDate",
      "setNewEndTime",
    ])
  },
  computed: {
    ...mapState("employee", ["newOffer"]),
    ...mapGetters("authentication", ["isEmployeeLogged"]),
  },
  mounted() {
    if (!this.isEmployeeLogged) {
      router.push("/login");
    }
  }
};
</script>

<style>
</style>
