<template>
  <div>
    <PageHeader v-bind:title="'Skupiny'">
      <b-button variant="primary" :to="{ name: 'OperatorGroupCreate' }"
        >Vytvořit</b-button
      >
    </PageHeader>
    <b-list-group v-if="!!groups && groups.length > 0">
      <b-list-group-item
        v-bind:key="group.id"
        v-for="group in groups"
        :to="{
          name: 'OperatorGroupDetail',
          params: { groupId: group.id },
        }"
        class="flex-column align-items-start"
      >
        <div class="d-flex w-100 justify-content-between">
          <h5>{{ group.name }}</h5>
        </div>
      </b-list-group-item>
    </b-list-group>
    <EmptyList v-else></EmptyList>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import EmptyList from "../../components/EmptyList";

import { mapGetters } from "vuex";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "OperatorGroups",
  components: {
    PageHeader,
    EmptyList
  },
  data() {
    return {
      groups: null,
    };
  },
  methods: {
    // get groups
    getGroups() {
      this.groups = null;
      apiSevice
        .get("/groups")
        .then((response) => {
          this.groups = response;
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getGroups();
    }
  },
};
</script>
<style>
</style>

