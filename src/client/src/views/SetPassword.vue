<template>
    <div>
        <b-row>
            <b-col cols="0" lg="4"></b-col>
            <b-col cols="12" lg="4">
                <b-card no-body class="mx-auto text-center">
                    <b-card-body>
                        <b-card-title>Nastavení hesla</b-card-title>
                        <b-form @submit.prevent>
                            <b-form-group id="input-group-2">
                                <b-form-input id="input-2" type="password" required placeholder="Heslo" v-model="password"></b-form-input>
                            </b-form-group>

                            <b-button block type="submit" variant="primary" class="mb-2" v-on:click="setPassword()">Uložit</b-button>
                        </b-form>
                    </b-card-body>
                </b-card>
            </b-col>
            <b-col cols="0" lg="4"></b-col>
        </b-row>
    </div>
</template>

<script>
import router from "../router";
import apiService from "../helpers/apiService";
import { mapGetters } from "vuex";

export default {
    name: "CustomerProfile",
    components: {},
    data() {
        return {
            password: null,
        };
    },
    props: {
        secret: String,
    },
    methods: {
        setPassword() {
            let body = {
                changePasswordSecret: this.secret,
                password: this.password,
            };
            apiService
                .put("/users/password", body)
                .then(() => {
                    router.push({ name: "Home" });
                })
                .catch(() => {});
        },
    },
    computed: {
        ...mapGetters(["isCustomerLogged"]),
    },
    mounted() {},
};
</script>
