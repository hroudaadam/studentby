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
          :to="{name: 'OperatorGroupDetail', params: {groupId: group.groupId}}"
          class="flex-column align-items-start"
        >
          <div class="d-flex w-100 justify-content-between">
            <h5>{{group.name}}</h5>
          </div>
        </b-list-group-item>
      </b-list-group>
    </div>
    <div v-else>Není tu nic</div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import PageHeader from "../../components/PageHeader";
import router from "../../router";
import apiSevice from "../../helpers/apiService";
import errorBox from "../../helpers/errorBox";

export default {
  name: "OperatorGroups",
  components: {
    PageHeader,
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
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({name: 'Login'});
    } else {
      this.getGroups();
    }
  },
};
</script>
<style>
</style>

