<template>
  <div>
    <v-app-bar color="blue" dense dark>

      <v-btn outlined to="/">
        <v-toolbar-title>Studentby</v-toolbar-title>
      </v-btn>

      <v-spacer></v-spacer>

      <v-btn text to="/about">Přihlásit</v-btn>
    </v-app-bar>
  </div>
</template>

<script>
export default {
  name: "Appbar",
};
</script>

<style>
</style>