<template>
  <v-app>
    <div id="app">
      <Appbar></Appbar>
      <router-view></router-view>
    </div>
  </v-app>
</template>

<script>
import Appbar from "./components/Appbar";
export default {
  name: "App",
  components: {
    Appbar
  },

  data: () => ({
    //
  }),
};
</script>
