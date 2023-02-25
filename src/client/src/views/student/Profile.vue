<template>
  <div>
    <PageHeader v-bind:title="'Profil'">
    </PageHeader>
    <div v-if="!!student">
      <b-card no-body>
        <b-card-body>
          <StudentInfo v-bind:student="student"></StudentInfo>
          <div  class="d-flex justify-content-center badge-size">
            <b-badge v-if="!student.activated" variant="secondary">Účet není aktivován</b-badge>
            <b-badge v-else variant="primary">Účet je aktivován</b-badge>
          </div>
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
import { mapGetters } from "vuex";

export default {
  name: "StudentProfile",
  components: {
    PageHeader,
    StudentInfo
  },
  data() {
    return {
      student: null,
    };
  },
  methods: {
    getProfile() {
      this.student = null;

      apiService
        .get("/students/me")
        .then((response) => {
          this.student = response;
        })
        .catch(() => {});
    },
  },
  computed: {
    ...mapGetters(["isStudentLogged", "isStudentInactLogged"]),
  },
  mounted() {
    if (!this.isStudentLogged && !this.isStudentInactLogged) {
      router.push({ name: "Login" });
    } else {
      this.getProfile();
    }
  },
};
</script>

<style>
.badge-size {
  font-size: 1.2em;
}
</style>

