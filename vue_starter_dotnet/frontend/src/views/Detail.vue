<template>
    <div class="detail">
       <h2> Site Details </h2>
      <h2>{{landmark.name}}</h2>
      <h2>{{landmark.name}}</h2>
      <h2>{{landmark.name}}</h2>
      <h2>Total number of ratings to date: {{landmark.user_ratings_total}}</h2>
      <h2>Address: {{landmark.vicinity}}</h2>
      <h2>Composite Rating (out of 5): {{landmark.rating}}</h2>
      <select-itin @selected="handleEvent"></select-itin>
        <add :LandmarkId="landmark.place_id" :ItineraryId="itineraryId"></add>
    </div>
</template>

<script>
import Add from '@/components/AddToItinerary.vue'
import SelectItin from '@/components/SelectItinerary.vue'

export default {
name: "Detail",
components: {
  Add,
  SelectItin
},
data() {
   return {
     itineraryId: '',
     landmark : {}
    }
},
methods: {
getLandmark(id) {
      fetch(`${process.env.VUE_APP_REMOTE_API}/search/place?placeid=${id}`, {
        method: 'GET'
      })
        .then((response) => {
          if (response.ok) {
            return response.json();
          } else {
              console.log('error fetching place')
          }
        })
        .then((json) => {
            this.landmark = json.result;
          }
        )
        .catch((err) => console.error(err));
    },
  handleEvent(selected) {
    this.itineraryId = selected;
  }
},

  


created () {
    this.getLandmark(this.$route.params.id);
}
}
</script>

<style>

</style>