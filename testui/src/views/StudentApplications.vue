<template>
  <div class="StudentApplications">   
    <div v-if="anyApplications">
      <ItemList v-bind:title="'Přihlášky'" v-bind:items="this.applications">
        <template v-slot:itemSlot="item">
            <StudentApplicationItem v-bind:application="item"></StudentApplicationItem>
        </template>
    </ItemList>
    </div>
    <div v-else>
      Nemáte žádné přihlášky
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters, mapActions } from "vuex";
import StudentApplicationItem from "../components/StudentApplicationItem";
import ItemList from '../components/ItemList';
import router from "../router";

export default {
  name: "StudentApplications",
  components: {
    StudentApplicationItem,
    ItemList
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

