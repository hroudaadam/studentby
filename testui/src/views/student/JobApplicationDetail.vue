<template>
  <div class="StudentJobApplicationDetail">
    <PageHeader v-bind:title="'Přihlášky'">
      <b-button variant="primary" :to="{name: 'StudentJobApplications'}">Zpět</b-button>
    </PageHeader>
    <div v-if="!!this.jobApplication">
      <b-card no-body>
        <b-card-body>
          <JobInfo v-bind:job="jobApplication"></JobInfo>
        </b-card-body>

        <b-card-body>
          <b-button v-on:click="cancelJobApplication">Zrušit</b-button>
        </b-card-body>
      </b-card>

      <b-alert show variant="danger" v-if="errorMsg" v-html="errorMsg"></b-alert>
    </div>
  </div>
</template>

<script>
import PageHeader from '../../components/PageHeader';
import JobInfo from '../../components/JobInfo';

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";
import mixinService from "../../helpers/mixinService";

export default {
  name: "StudentJobApplicationDetail",
  props: ["id"],
  components: {
    PageHeader,
    JobInfo
  },
  data() {
    return {
      jobApplication: null,
      errorMsg: null,
    };
  },
  methods: {
    getOfferDetail() {
      this.jobApplication = null;
      this.errorMsg = null;
      apiService
        .get("/student/job-applications/" + this.id.toString())
        .then((response) => {
          this.jobApplication = response;
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },    
    cancelJobApplication() {
      this.errorMsg = null;
      apiService
        .del("/student/job-applications/" + this.id.toString())
        .then(() => {
          router.push({name: 'StudentJobApplications'});
        })
        .catch((error) => {
          this.errorMsg = error.message;
        });
    },
  },
  computed: {
    ...mapGetters("authentication", ["isStudentLogged"]),
    start: function () {
      return mixinService.dateToString(this.jobApplication.start);
    },
    end: function () {
      return mixinService.dateToString(this.jobApplication.end);
    },
  },
  mounted() {
    if (!this.isStudentLogged) {
      router.push("/login");
    } else {
      this.getOfferDetail();
    }
  },
};
</script>

<style>
</style>

