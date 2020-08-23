<template>
      <b-list-group-item :to="{ name: 'StudentOfferDetail', params: {id: offer.id}}" class="flex-column align-items-start mb-2">
        
        <div class="d-flex w-100 justify-content-between">
          <h5 class="mb-1">{{offer.title}}</h5>
          <small>3 days ago</small>
          
        </div>

        <p
          class="mb-1"
        ></p>

        <small>{{offer.wage}} €</small>
        
      </b-list-group-item>
</template>

<script>
export default {
  name: "StudentOfferItem",
  components: {
    
  },
  props: ['offer']
};
</script>

<style scoped>
</style>

