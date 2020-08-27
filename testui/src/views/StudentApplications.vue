<template>
  <div class="StudentApplications">
    <PageHeader v-bind:title="'Přihlášky'"></PageHeader>
    <div v-if="!!jobApplications && jobApplications.length > 0">
      <ItemList v-bind:items="this.jobApplications">
        <template v-slot:itemSlot="item">
          <StudentApplicationItem
            v-bind:application="item"
            v-on:cancel="cancelJobApplication($event)"
          ></StudentApplicationItem>
        </template>
      </ItemList>
    </div>
    <div v-else>Nemáte žádné přihlášky</div>
    <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import StudentApplicationItem from "../components/StudentApplicationItem";
import ItemList from "../components/ItemList";
import PageHeader from "../components/PageHeader";
import router from "../router";
import apiSevice from "../helpers/apiService";

export default {
  name: "StudentApplications",
  components: {
    StudentApplicationItem,
    ItemList,
    PageHeader,
  },
  data() {
    return {
      jobApplications: null,
      errorMsg: null,
    };
  },
  methods: {

    getStudentApplications() {
      this.errorMsg = null;
      this.jobApplications = null;
      apiSevice
        .get("/student/job-applications")
        .then((response) => {
          this.jobApplications = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },

    cancelJobApplication(applicationId) {
      this.errorMsg = null;
      apiSevice
        .deleteMehod("/student/job-applications/" + applicationId.toString())
        .then((response) => {
          console.log(response);
          this.getStudentApplications();
          
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
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

