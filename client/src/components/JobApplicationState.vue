<template>
  <div>
    <div>
      <div v-if="!!attendance">
        <b-badge
          pill
          class="p-2"
          v-if="jobApplicationState === jobApplicationStates.approved"
          variant="warning"
          >Nevyřízeno</b-badge
        >
        <b-badge
          pill
          class="p-2"
          v-if="jobApplicationState === jobApplicationStates.attended"
          variant="success"
          >Účast</b-badge
        >
        <b-badge
          pill
          class="p-2"
          v-if="jobApplicationState === jobApplicationStates.absent"
          variant="danger"
          >Absence</b-badge
        >
      </div>
      <div v-else>
        <b-badge
          pill
          class="p-2"
          v-if="jobApplicationState === jobApplicationStates.pending"
          variant="warning"
          >Nevyřízeno</b-badge
        >
        <b-badge
          pill
          class="p-2"
          v-if="jobApplicationState === jobApplicationStates.approved"
          variant="success"
          >Potvrzeno</b-badge
        >
        <b-badge
          pill
          class="p-2"
          v-if="jobApplicationState === jobApplicationStates.denied"
          variant="danger"
          >Zamítnuto</b-badge
        >
        <b-badge
          pill
          class="p-2"
          v-if="jobApplicationState === jobApplicationStates.attended"
          variant="success"
          >Účast</b-badge
        >
        <b-badge
          pill
          class="p-2"
          v-if="jobApplicationState === jobApplicationStates.absent"
          variant="danger"
          >Absence</b-badge
        >
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "JobApplicationState",
  components: {},
  props: {
    jobApplicationState: String,
    attendance: Boolean,
  },
  data() {
    return {};
  },
  computed: {
    ...mapState(["jobApplicationStates"]),
  },
};
</script>