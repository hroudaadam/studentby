<template>
  <div class="OperatorGroupDetail">
    <PageHeader v-bind:title="'Skupiny'">
      <b-button variant="primary" :to="{ name: 'OperatorGroups' }"
        >Zpět</b-button
      >
    </PageHeader>
    <div v-if="!!group">
      <b-card no-body>
        <b-card-body>
          <b-card-title>{{ group.name }}</b-card-title>

          <div class="my-2">
            <b-list-group>
              <b-list-group-item
                v-bind:key="customer.id"
                v-for="customer in group.customers"
                class="d-flex justify-content-between align-items-center"
              >
                {{ customer.firstName }} {{ customer.lastName }}
              </b-list-group-item>
            </b-list-group>
          </div>

          <b-button
            variant="primary"
            :to="{
              name: 'OperatorCustomerCreate',
              params: { groupId: groupId, groupName: group.name },
            }"
            >Nový uživatel</b-button
          >
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
  name: "OperatorGroupDetail",
  props: {
    groupId: Number,
  },
  components: {
    PageHeader,
  },
  data() {
    return {
      group: null,
    };
  },
  methods: {
    // get group
    getGroup() {
      apiService
        .get("/groups/" + this.groupId.toString())
        .then((response) => {
          this.group = response;
        })
        .catch((error) => {
          console.error(error.message);
        });
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getGroup();
    }
  },
};
</script>

<style>
</style>

