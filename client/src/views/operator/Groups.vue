<template>
  <div class="OperatorGroups">
    <PageHeader v-bind:title="'Skupiny'">
      <b-button variant="primary" :to="{name:'OperatorGroupCreate'}">Vytvořit</b-button>
    </PageHeader>
    <div v-if="!!groups && groups.length > 0">
      <b-list-group>
        <b-list-group-item
          v-bind:key="group.groupId"
          v-for="group in groups"
          :to="{name: 'OperatorGroupDetail', params: {id: group.groupId}}"
          class="flex-column align-items-start"
        >
          <div class="d-flex w-100 justify-content-between">
            <h5>{{group.title}}</h5>
          </div>
        </b-list-group-item>
      </b-list-group>
    </div>
    <div v-else>Není tu nic</div>
    <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import PageHeader from "../../components/PageHeader";
import router from "../../router";
import apiSevice from "../../helpers/apiService";

export default {
  name: "OperatorGroups",
  components: {
    PageHeader,
  },
  data() {
    return {
      groups: null,
      errorMsg: null,
    };
  },
  methods: {
    getGroups() {
      this.errorMsg = null;
      this.groups = null;
      apiSevice
        .get("/operator/groups")
        .then((response) => {
          this.groups = response;
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
      router.push("/login");
    } else {
      this.getGroups();
    }
  },
};
</script>
<style>
</style>

