<template>
  <div>
    <!-- get the list from backend for this user - loop through and display landmark names in order -->
    <h1>{{itinerary.tourName}}</h1>
    <p>{{itinerary.dateOfTour}}</p>
    <div v-for="landmark in itinerary.landmarks" v-bind:key="landmark.landmarkId">
        <p>{{landmark.landmarkName}}</p> <button v-on:click="handleEvent(landmark.landmarkId)">Delete {{landmark.landmarkName}}from itinerary</button>
    </div>
    <!-- <Search></Search> -->
    <!-- <add :LandmarkId="landmark.place_id" :ItineraryId="itineraryId"></add> -->

    <!-- need button for edit itinerary and delete itinerary -->
  </div>
</template>

<script>
import auth from '../auth'
import Vue from 'vue';

// import Add from '@/components/AddToItinerary.vue'
// import Search from '@/components/Search.vue'



export default {
  name: 'ExistingItinerary',
  components: {
    // Add,
    // Search
  },
    props: {
      LandmarkId: String,
      ItineraryId: Number
  },
   computed: {
    item: function() {
      return {
        LandmarkId: this.landmarkid,
        ItineraryId: this.itinerary.itineraryId,
      };
    }
  },
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
    deleteLandmark() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/itinerary/remove?itineraryid=${this.ItineraryId}&landmarkid=${this.LandmarkId}`, {
         method: "DELETE",
        headers: new Headers({
          Authorization: "Bearer " + auth.getToken(),
          Accept: "application/json",
          "Content-Type": "application/json"
        }),
        body: JSON.stringify(this.item)
      })
        .then(response => {
          if (response.ok) {
            alert("Landmark has been deleted from itinerary");
            Vue.$forceUpdate();
          } else {
            console.log("Could not delete landmark");
          }
        })
        .catch(err => console.error(err));
    },
      handleEvent(id) {
    this.landmarkid = id;
    this.deleteLandmark();
  },
    getItinerary(itineraryid) {      
      fetch(`${process.env.VUE_APP_REMOTE_API}/itinerary/display?itineraryId=${itineraryid}`, {
        method: 'GET',
                headers: new Headers( {
            Authorization: "Bearer " + auth.getToken()
        })

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