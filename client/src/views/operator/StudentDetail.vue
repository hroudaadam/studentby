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
          <b-card-text>
            <b class="mb-2">Informace</b>
            <p>
              Jméno: {{ student.firstName }} {{ student.lastName }} <br/>
              Datum narození: {{ dateOfBirth }} <br/>
              Adresa: město <br />
            </p>
          </b-card-text>

          <b-button v-if="student.activated" variant="danger" v-on:click="editStudent(false)">Deaktivovat</b-button>
          <b-button v-else variant="success" v-on:click="editStudent(true)">Aktivovat</b-button>

        </b-card-body>
      </b-card>
    </div>
  </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import errorBox from '../../helpers/errorBox';
import apiService from "../../helpers/apiService";
import mixinService from "../../helpers/mixinService";
import router from "../../router";
import { mapGetters, mapState } from "vuex";

export default {
  name: "OperatorStudentDetail",
  components: {
    PageHeader,
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
    getStudent() {
      this.student = null;

      apiService
        .get("/students/" + this.studentId.toString())
        .then((response) => {
          this.student = response;
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
    editStudent(activate) {
      var role = activate ? this.userRoles.student : this.userRoles.studentUnver;
      var body = {
        studentId: this.studentId,
        role: role
      };

      apiService
        .put("/students/" + this.studentId.toString(), body)
        .then(() => {
          console.log("Aktivováno");
          this.getStudent();
        })
        .catch((error) => {
          errorBox.new(this, error.message);
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isOperatorLogged"]),
    ...mapState(["userRoles"]),

    dateOfBirth: function () {
      return mixinService.dateOfBirthToString(this.student.dateOfBirth);
    }
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