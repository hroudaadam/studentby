<template>
  <div class="OperatorStudentDetail">
    <PageHeader v-bind:title="'Studenti'">
      <b-button variant="primary" :to="{ name: 'OperatorStudents' }"
        >Zpět</b-button
      >
    </PageHeader>
    <div v-if="!!this.student">
      <b-card no-body>
        <b-card-body>
            <div class="mb-2"><h5>Údaje</h5></div>
            <StudentInfo v-bind:student="student"></StudentInfo>

          <hr/>
          <b-button v-if="student.activated" variant="danger" v-on:click="editStudent(false)" size="sm">Deaktivovat</b-button>
          <b-button v-else variant="success" v-on:click="editStudent(true)" size="sm">Aktivovat</b-button>

        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import StudentInfo from '../../components/StudentInfo';

import apiService from "../../helpers/apiService";
import router from "../../router";
import { mapGetters, mapState } from "vuex";

export default {
  name: "OperatorStudentDetail",
  components: {
    PageHeader,
    StudentInfo
  },
  props: {
    studentId: Number,
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
        .get("/students/" + this.studentId.toString())
        .then((response) => {
          this.student = response;
        })
        .catch((error) => {
          console.error(error.message);
        });
    },
    // edit student
    editStudent(activate) {
      var role = activate ? this.userRoles.student : this.userRoles.studentInact;
      var body = {
        studentId: this.studentId,
        role: role
      };
      apiService
        .put("/students/" + this.studentId.toString(), body)
        .then(() => {
          this.getStudent();
        })
        .catch((error) => {
          console.error(error.message);
        });
    },
  },
  computed: {
    ...mapGetters(["isOperatorLogged"]),
    ...mapState(["userRoles"]),
  },
  mounted() {
    if (!this.isOperatorLogged) {
      router.push({name: 'Login'});
    } else {
      this.getStudent();
    }
  },
};
</script>