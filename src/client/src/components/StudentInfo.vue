<template>
  <div>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Email:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{ student.email }}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Jméno:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{ student.firstName }} {{ student.lastName }}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Adresa:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{ address }}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Narozen:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{ dateOfBirth }}</p>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import mixinService from "../helpers/mixinService";

export default {
  name: "StudentInfo",
  props: {
    student: Object,
  },
  computed: {
    address: function () {
      return mixinService.getAddressString(this.student.address);
    },
    dateOfBirth: function () {
      return mixinService.getDateOfBirthString(this.student.dateOfBirth);
    },
  },
};
</script>