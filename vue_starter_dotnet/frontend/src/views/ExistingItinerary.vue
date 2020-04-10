<template>
  <div>
    <!-- get the list from backend for this user - loop through and display landmark names in order -->
    <h1>{{itinerary.tourName}}</h1>
    <p>{{itinerary.dateOfTour}}</p>
    <div v-for="landmark in itinerary.landmarks" v-bind:key="landmark.landmarkId">
        <p>{{landmark.landmarkName}}</p>
    </div>
    <!-- need button for edit itinerary and delete itinerary -->
  </div>
</template>

<script>
export default {
  name: 'ExistingItinerary',
  components: {},
  created(){
      this.getItinerary(this.$route.params.id);
  },
  data() {
    return {
      
      itinerary: {
          itineraryId: '',
          userId: '',
          dateOfTour: '',
          tourName: '',
          startPoint: '',
          landmarks: [
              {
                  landmarkId: '',
                  landmarkName: '',
                  landmarkAddress: ''
              }
          ]
      }
    };
  },
  methods: {
    getItinerary(itineraryid) {      
      fetch(`${process.env.VUE_APP_REMOTE_API}/itinerary/display?itineraryId=${itineraryid}`, {
        method: 'GET'
      })
        .then((response) => {
          if (response.ok) {
            return response.json();
          } else {
              console.log('error fetching itinerary')
          }
        })
        .then((json) => {
            this.itinerary = json;
          }
        )
        .catch((err) => console.error(err));
    }
  },
};
</script>

<style>

</style>