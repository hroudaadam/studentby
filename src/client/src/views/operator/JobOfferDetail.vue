<template>
    <div>
        <PageHeader v-bind:title="'Nabídky'">
            <b-button variant="secondary" size="sm" :to="{ name: 'OperatorJobOffers' }">Zpět</b-button>
        </PageHeader>
        <div v-if="!!jobOffer">
            <b-card no-body>
                <b-card-body>
                    <JobInfo v-bind:job="jobOffer"></JobInfo>
                    <b-card-text>
                        <div class="mb-2"><h5>Studenti</h5></div>
                        <b-list-group v-if="jobApplications && jobApplications.length > 0">
                            <b-list-group-item
                                v-bind:key="jobApplication.id"
                                v-for="jobApplication in jobApplications"
                                class="d-flex justify-content-between align-items-center"
                                :to="{
                                    name: 'OperatorJobApplicationResult',
                                    params: {
                                        jobApplicationId: jobApplication.id,
                                        jobOfferId: jobOfferId,
                                    },
                                }"
                            >
                                <div class="text-wrap">
                                    {{ jobApplication.student.firstName }}
                                    {{ jobApplication.student.lastName }}
                                </div>
                                <JobApplicationState v-bind:jobApplicationState="jobApplication.state" v-bind:attendance="true"></JobApplicationState>
                            </b-list-group-item>
                        </b-list-group>
                        <EmptyList v-else></EmptyList>
                    </b-card-text>
                </b-card-body>
            </b-card>
        </div>
    </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import JobInfo from "../../components/JobInfo";
import JobApplicationState from "../../components/JobApplicationState";
import EmptyList from "../../components/EmptyList";

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
    name: "OperatorJobOfferDetail",
    props: {
        jobOfferId: String,
    },
    components: {
        PageHeader,
        JobInfo,
        JobApplicationState,
        EmptyList,
    },
    data() {
        return {
            jobOffer: null,
            jobApplications: null
        };
    },
    methods: {
        // get job offer
        getJobOffer() {
            this.jobOffer = null;
            apiService
                .get("/job-offers/" + this.jobOfferId)
                .then((response) => {
                    this.jobOffer = response;
                })
                .catch(() => {});
        },
        // get job applications
        getJobApplications() {
            this.jobApplications = null;
            apiService
                .get("/job-applications?jobOfferId=" + this.jobOfferId)
                .then((response) => {
                    this.jobApplications = response;
                })
                .catch(() => {});
        },
    },
    computed: {
        ...mapGetters(["isOperatorLogged"]),
    },
    mounted() {
        if (!this.isOperatorLogged) {
            router.push({ name: "Login" });
        } else {
            this.getJobOffer();
            this.getJobApplications();
        }
    },
};
</script>

<style></style>
