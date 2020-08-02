<template>
  <div class="StudentApplications">
    <div class="mb-4">
      <h2 class="text-center">Přihlášky</h2>
    </div>    
    <div v-if="anyApplications">
      <b-list-group v-bind:key="application.id" v-for="application in applications">
        <StudentApplicationItem v-bind:application="application"></StudentApplicationItem>
      </b-list-group>
    </div>
    <div v-else>
      Nemáte žádné přihlášky
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters, mapActions } from "vuex";
import StudentApplicationItem from "../components/StudentApplicationItem";
import router from "../router";

export default {
  name: "StudentApplications",
  components: {
    StudentApplicationItem,
  },
  methods: {
    ...mapActions("student", ["getStudentApplications"]),
  },
  computed: {
    ...mapState("student", ["applications"]),
    ...mapGetters("authentication", ["isStudentLogged"]),
    ...mapGetters("student", ["anyApplications"])
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getStudentApplications();
    }
  },
};
</script>
<style>
</style>

