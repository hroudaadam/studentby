<template>
  <div>
    <PageHeader v-bind:title="'Studenti'"></PageHeader>
      <b-list-group v-if="students && students.length > 0">
        <b-list-group-item
          v-bind:key="student.id"
          v-for="student in students"
          :to="{
            name: 'OperatorStudentDetail',
            params: { studentId: student.id },
          }"
          class="flex-column align-items-start"
        >
          {{ student.firstName }} {{ student.lastName }}
        </b-list-group-item>
      </b-list-group>
      <EmptyList v-else></EmptyList>
  </div>
</template>

<script>
import PageHeader from "@/components/PageHeader";
import EmptyList from "../../components/EmptyList";

import { mapGetters } from "vuex";
import router from "@/router/index";
import apiSevice from "../../helpers/apiService";

export default {
  name: "OperatorStudents",
  components: {
    PageHeader,
    EmptyList
  },
  data() {
    return {
      students: null,
      errorMsg: null,
    };
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
  },
  methods: {
    // get students
    getStudents() {
      this.students = null;
      apiSevice
        .get("/students")
        .then((response) => {
          this.students = response;
        })
        .catch(() => {});
    },
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getStudents();
    }
  },
};
</script>

<style scoped>
</style>