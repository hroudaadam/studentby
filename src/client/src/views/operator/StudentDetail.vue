<template>
  <div>
    <PageHeader v-bind:title="'Studenti'">
      <b-button variant="secondary" size="sm" :to="{ name: 'OperatorStudents' }"
        >Zpět</b-button
      >
    </PageHeader>
    <div v-if="!!this.student">
      <b-card no-body>
        <b-card-body>
          <div class="mb-2"><h5>Údaje</h5></div>
          <StudentInfo v-bind:student="student"></StudentInfo>

          <hr />
          <b-button
            v-if="student.activated"
            variant="secondary"
            v-on:click="editStudent(false)"
            >Deaktivovat</b-button
          >
          <b-button
            v-else
            variant="primary"
            v-on:click="editStudent(true)"
            >Aktivovat</b-button
          >
        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import StudentInfo from "../../components/StudentInfo";

import apiService from "../../helpers/apiService";
import router from "../../router";
import { mapGetters, mapState } from "vuex";

export default {
  name: "OperatorStudentDetail",
  components: {
    PageHeader,
    StudentInfo,
  },
  props: {
    studentId: String,
  },
  data() {
    return {
      student: null,
    };
  },
  methods: {
    // get student
    getStudent() {
      this.student = null;
      apiService
        .get("/students/" + this.studentId)
        .then((response) => {
          this.student = response;
        })
        .catch(() => {});
    },
    // edit student
    editStudent(activate) {
      let body = {
        id: this.studentId,
        activated: activate,
      };
      apiService
        .put("/students/" + this.studentId, body)
        .then(() => {
          this.getStudent();
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
    ...mapState(["userRoles"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({ name: "Login" });
    } else {
      this.getStudent();
    }
  },
};
</script>