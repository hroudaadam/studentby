<template>
    <div>
        <PageHeader v-bind:title="'Detail přihlášky'">
            <b-button variant="secondary" size="sm" :to="{ name: 'OperatorJobApplications' }">Zpět</b-button>
        </PageHeader>
        <div v-if="!!this.jobApplication">
            <b-card no-body>
                <b-card-body>
                    <JobInfo v-bind:job="jobApplication.jobOffer">
                        <JobApplicationState v-bind:jobApplicationState="jobApplication.state"></JobApplicationState>
                    </JobInfo>

                    <hr />
                    <div class="mb-2"><h5>Student</h5></div>

                    <StudentInfo v-bind:student="jobApplication.student"></StudentInfo>
                    <hr />

                    <b-button class="mr-2" variant="primary" v-on:click="editJobApplication(true)" size="sm">Přijmout</b-button>
                    <b-button variant="secondary" v-on:click="editJobApplication(false)" size="sm">Odmítnout</b-button>
                </b-card-body>
            </b-card>
        </div>
    </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import JobInfo from "../../components/JobInfo";
import JobApplicationState from "../../components/JobApplicationState";
import StudentInfo from "../../components/StudentInfo";

import { mapGetters, mapState } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
    name: "OperatorJobApplicationDetail",
    props: {
        jobApplicationId: String,
    },
    components: {
        PageHeader,
        JobInfo,
        JobApplicationState,
        StudentInfo,
    },
    data() {
        return {
            jobApplication: null,
        };
    },
    methods: {
        // get job application
        getJobApplication() {
            this.jobApplication = null;
            apiService
                .get("/job-applications/" + this.jobApplicationId)
                .then((response) => {
                    this.jobApplication = response;
                })
                .catch(() => {});
        },
        // edit job application
        editJobApplication(approve) {
            let state = approve ? this.jobApplicationStates.approved : this.jobApplicationStates.denied;
            let body = {
                id: this.jobApplicationId,
                state: state,
            };
            apiService
                .put("/job-applications/" + this.jobApplicationId, body)
                .then(() => {
                    router.push({ name: "OperatorJobApplications" });
                })
                .catch(() => {});
        },
    },
    computed: {
        ...mapGetters(["isOperatorLogged"]),
        ...mapState(["jobApplicationStates"]),
    },
    mounted() {
        if (!this.isOperatorLogged) {
            router.push({ name: "Login" });
        } else {
            this.getJobApplication();
        }
    },
};
</script>

<style></style>
