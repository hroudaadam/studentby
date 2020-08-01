<template>
  <div class="app">
    <Appbar></Appbar>
    <router-view></router-view>
  </div>
</template>

<script>
import Appbar from "./components/layout/Appbar";

export default {
  name: "App",

  components: {
    Appbar,
  },

  data: () => ({}),
};
</script>
