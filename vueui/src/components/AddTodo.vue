<template>
  <div>
    <form v-on:submit="addTodo">
        <input type="text" name="title" v-model="title" placeholder="Add Todo...">
        <input type="submit" value="Ok">
    </form>
  </div>
</template>

<script>
export default {
  name: "AddTodo",
  data() {
      return { title: ''}
  },
  props: [],
  methods: {
      addTodo(e) {
          e.preventDefault();
          const newTodo = {
              id: Math.floor(Math.random() * 1000),
              title: this.title,
              completed: false 
          };
          this.$emit('add-todo', newTodo);

          this.title = '';
      }
  },
  components: {},
};
</script>

<style scoped>

</style>