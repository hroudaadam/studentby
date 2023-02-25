<template>
  <div>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Email:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{ customer.email }}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Pozice:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{ customer.jobTitle }}</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="3" lg="2">
        <label>Skupina:</label>
      </b-col>
      <b-col cols="9" lg="10">
        <p>{{ customer.group.name }}</p>
      </b-col>
    </b-row>
  </div>
</template>

<script>

export default {
  name: "CustomerInfo",
  props: {
    customer: Object,
  },
  computed: {
  },
};
</script>