<template>
  <div class="app">
    <AppBar></AppBar>
    <b-overlay :show="loading" rounded="sm" no-wrap> </b-overlay>
    <b-container style="padding-top: 80px">
      <router-view></router-view>
    </b-container>
  </div>
</template>

<script>
import AppBar from "./components/AppBar";
import { mapState } from "vuex";

export default {
  name: "App",
  components: {
    AppBar,
  },
  data: () => ({}),
  computed: {
    ...mapState(["loading", "errorMsg"]),
  },
  watch: {
    errorMsg: function () {
      this.createErrorAlert(this.errorMsg);
    },
  },
  methods: {
    createErrorAlert(errorMsg) {
      this.$bvToast.toast(errorMsg, {
        title: "Chyba",
        variant: "danger",
        solid: true,
      });
    },
  },
};
</script>
