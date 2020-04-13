<template>
  <div></div>
</template>

<script>
import auth from "../auth";

export default {
  name: "Directions",
  data() {
      return {
          itinerary: []
      }
  },
  methods: {
    getDirections(itineraryid) {
      fetch(
        `${process.env.VUE_APP_REMOTE_API}/directions?itineraryId=${itineraryid}`,
        {
          method: "GET",
          headers: new Headers({
            Authorization: "Bearer " + auth.getToken()
          })
        }
      )
        .then(response => {
          if (response.ok) {
            return response.json();
          } else {
            console.log("error fetching itinerary");
          }
        })
        .then(json => {
          this.itinerary = json;
        })
        .catch(err => console.error(err));
    },
    
  },
  created() {
      this.getDirections(this.$route.id)
  }
};
</script>

<style>
</style>