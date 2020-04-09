<template>
  <div>
    <!-- get the list from backend for this user - loop through and display landmark names in order -->
    <h1>{{itinerary.tourName}}</h1>
    <p>{{itinerary.dateOfTour}}</p>
    <div v-for="landmark in itinerary.itineraryOrder" v-bind:key="landmark.landmarkId">
        <p>{{landmark.landmarkName}}</p>
    </div>
    <!-- need button for edit itinerary and delete itinerary -->
  </div>
</template>

<script>
export default {
  name: 'existingItinerary',
  components: {},
  created(){
    //   this.getItinerary(this.$route.params.id);
  },
  data() {
    return {
      
      itinerary: {
          itineraryId: 100,
          userId: 2,
          dateOfTour: new Date(),
          tourName: 'Tour1',
          startPoint: '32, 200',
          itineraryOrder: [
              {
                  landmarkId: 1,
                  landmarkName: 'string',
                  landmarkAddress: '123 main'
              },
                            {
                  landmarkId: 2,
                  landmarkName: 'string1',
                  landmarkAddress: '123 fever'
              },
                            {
                  landmarkId: 3,
                  landmarkName: 'string2',
                  landmarkAddress: '123 bear'
              }
          ]
      }
    };
  },
  methods: {
    getItinerary(id) {
      fetch(`${process.env.VUE_APP_REMOTE_API}/(controller name/method name/${id})`, {
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
    },
  },
};
</script>

<style>

</style>