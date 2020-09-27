<template>
  <div class="StudentInfo">
    <b-row>
      <b-col cols="3" lg="1">
        <label>Email:</label>
      </b-col>
      <b-col cols="9" lg="11">
        <p>{{ student.email }}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="1">
        <label>Jméno:</label>
      </b-col>
      <b-col cols="9" lg="11">
        <p>{{ student.firstName }} {{ student.lastName }}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="1">
        <label>Adresa:</label>
      </b-col>
      <b-col cols="9" lg="11">
        <p>{{ address }}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="1">
        <label>Narozen:</label>
      </b-col>
      <b-col cols="9" lg="11">
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
      return mixinService.addressToString(this.student.address);
    },
    dateOfBirth: function () {
      return mixinService.dateOfBirthToString(this.student.dateOfBirth);
    },
  },
};
</script>