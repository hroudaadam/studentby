<template>
    <div>
        <PageHeader v-bind:title="'Skupiny'">
            <b-button variant="secondary" size="sm" :to="{ name: 'OperatorGroups' }">Zpět</b-button>
        </PageHeader>
        <div v-if="!!group">
            <b-card no-body>
                <b-card-body>
                    <b-card-title>{{ group.name }}</b-card-title>
                    <hr />
                    <div class="mb-2" style="font-size: 1.2rem">Uživatelé</div>
                    <div class="my-2">
                        <b-list-group v-if="customers && customers.length > 0">
                            <b-list-group-item
                                v-bind:key="customer.id"
                                v-for="customer in customers"
                                class="d-flex justify-content-between align-items-center"
                            >
                                {{ customer.email }}
                            </b-list-group-item>
                        </b-list-group>
                        <EmptyList v-else></EmptyList>
                    </div>
                    <hr />
                    <b-button
                        variant="primary"
                        :to="{
                            name: 'OperatorCustomerCreate',
                            params: { groupId: groupId, groupName: group.name },
                        }"
                        >Nový uživatel</b-button
                    >
                </b-card-body>
            </b-card>
        </div>
    </div>
</template>

<script>
import PageHeader from "../../components/PageHeader";
import EmptyList from "../../components/EmptyList";

import { mapGetters } from "vuex";
import router from "../../router";
import apiService from "../../helpers/apiService";

export default {
    name: "OperatorGroupDetail",
    props: {
        groupId: String,
    },
    components: {
        PageHeader,
        EmptyList,
    },
    data() {
        return {
            group: null,
            customers: null,
        };
    },
    methods: {
        // get group
        getGroup() {
            this.group = null;
            this.customers = null;

            apiService
                .get("/groups/" + this.groupId)
                .then((response) => {
                    this.group = response;
                })
                .catch(() => {});

            apiService
                .get("/employers?groupId=" + this.groupId)
                .then((response) => {
                    this.customers = response;
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
            this.getGroup();
        }
    },
};
</script>

<style></style>
