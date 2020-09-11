<template>
  <div class="OperatorGroupDetail">
    <PageHeader v-bind:title="'Skupiny'">
      <b-button variant="primary" :to="{name: 'OperatorGroups'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!group">
      <b-card no-body>
        <b-card-body>
          <b-card-title>{{group.name
            }}</b-card-title>
          <div class="mt-2">
            <b-list-group
              v-bind:key="customer.id"
              v-for="customer in group.customers"
            >
              <b-list-group-item class="d-flex justify-content-between align-items-center">
                {{customer.firstName}} {{customer.lastName}}
              </b-list-group-item>
            </b-list-group>
          </div>
        </b-card-body>
        <b-card-body>
          <b-button variant="primary" :to="{name: 'OperatorCustomerCreate', params:{groupId: id}}">Nový uživatel</b-button>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from '../../components/PageHeader';

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
  name: "OperatorGroupDetail",
  props: ["id"],
  components: {
    PageHeader
  },
  data() {
    return {
      group: null,
      errorMsg: null,
    };
  },
  methods: {
    getGroup() {
      apiService
        .get("/groups/" + this.id.toString())
        .then((response) => {
          this.group = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({name: 'Login'});
    } else {
      this.getGroup();
    }
  },
};
</script>

<style>
</style>

