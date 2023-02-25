<template>
    <div class="app">
        <AppBar></AppBar>
        <b-container style="padding-top: 80px" class="mb-3">
            <router-view></router-view>
        </b-container>
    </div>
</template>

<script>
import AppBar from "./components/AppBar";
import { mapState } from "vuex";

export default {
    name: "App",
    components: {
        AppBar,
    },
    data: () => ({}),
    computed: {
        ...mapState(["loading", "errorMsg"]),
    },
    watch: {
        errorMsg: function(errorMsg) {
            let title = "Chyba";
            let detail = errorMsg.message;

            this.$bvToast.toast(detail, {
                title: title,
                variant: "danger",
                solid: true,
            });
        },
    },
};
</script>
