<template>
  <div class="OperatorStudents">
    <PageHeader v-bind:title="'Studenti'"></PageHeader>
    <div v-if="!!students && students.length > 0">
      <b-list-group>
        <b-list-group-item
          v-bind:key="student.studentId"
          v-for="student in students"
          :to="{
            name: 'OperatorStudentDetail',
            params: { studentId: student.studentId },
          }"
          class="flex-column align-items-start"
        >
            {{ student.firstName }} {{ student.lastName }}
        </b-list-group-item>
      </b-list-group>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import PageHeader from "@/components/PageHeader";
import router from "@/router/index";
import apiSevice from "../../helpers/apiService";
import errorBox from "../../helpers/errorBox";

export default {
  name: "OperatorStudents",
  components: {
    PageHeader,
  },
  data() {
    return {
      students: null,
      errorMsg: null
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
        .catch((error) => {
          errorBox.new(this, error.message);
        });
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