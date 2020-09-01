<template>
  <div class="OperatorGroups">
    <PageHeader v-bind:title="'Skupiny'">
      <b-button variant="primary">Vytvořit</b-button>
    </PageHeader>
    <div v-if="!!groups && groups.length > 0">
      <b-list-group>
        <b-list-group-item :to="onClickLink" class="flex-column align-items-start mb-2">
          <div class="d-flex w-100 justify-content-between">
            <h5 class="mb-1">{{group.title}}</h5>
          </div>
          <p class="mb-1">Text</p>
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

