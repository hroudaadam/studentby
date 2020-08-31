<template>
  <div class="app">
    <AppBar></AppBar>
    <b-container class="mt-4">
      <router-view></router-view>
    </b-container>
    
  </div>
</template>

<script>
import AppBar from "./components/AppBar";

export default {
  name: "App",

  components: {
    AppBar,
  },

  data: () => ({}),
};
</script>
