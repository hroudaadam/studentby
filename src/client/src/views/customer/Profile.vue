<template>
  <div>
    <PageHeader v-bind:title="'Profil'">
    </PageHeader>
    <div v-if="!!customer">
      <b-card no-body>
        <b-card-body>
          <CustomerInfo v-bind:customer="customer"></CustomerInfo>
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import CustomerInfo from "../../components/CustomerInfo";

import apiService from "../../helpers/apiService";
import router from "../../router";
import { mapGetters } from "vuex";

export default {
  name: "CustomerProfile",
  components: {
    PageHeader,
    CustomerInfo
  },
  data() {
    return {
      customer: null,
    };
  },
  methods: {
    getProfile() {
      this.customer = null;
      apiService
        .get("/employers/me")
        .then((response) => {
          this.customer = response;
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isCustomerLogged"]),
  },
  mounted() {
    if (!this.isCustomerLogged) {
      router.push({ name: "Login" });
    } else {
      this.getProfile();
    }
  },
};
</script>

