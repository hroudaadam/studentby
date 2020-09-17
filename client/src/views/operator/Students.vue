<template>
  <div class="OperatorStudents">
    <PageHeader v-bind:title="'Studenti'"></PageHeader>
    <div v-if="!!groups && groups.length > 0">
      <b-list-group>
        <b-list-group-item
          v-bind:key="student.studentId"
          v-for="student in students"
          :to="{name: 'OperatorStudentDetail', params: {id: student.studentId}}"
          class="flex-column align-items-start"
        >
          <div class="d-flex w-100 justify-content-between">
            <h5>{{student.firstName}}</h5>
          </div>
        </b-list-group-item>
      </b-list-group>
    </div>
  </div>
</template>

<script>
import PageHeader from "@/components/PageHeader";
export default {
  data() {
    return {
      students: null
    }
  },
  methods: {
    getStudents() {
      
    }
  },
  mounted () {
    if (!this.isOperatorLogged) {
      router.push({name: 'Login'});
    } else {
      this.getStudents();
    }
  },
};
</script>

<style scoped>
</style>