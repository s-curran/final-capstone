<template>
  <div class="display">
    <!-- get the list from backend for this user - loop through and display landmark names in order -->

    <h1>{{itinerary.tourName}}</h1>
    <p>{{getDate(itinerary.dateOfTour)}}</p>
    <p id="start">Starting address: {{itinerary.startPoint}}</p>
    <div class="OneLine" v-for="landmark in itinerary.landmarks" v-bind:key="landmark.landmarkId">
    <router-link :to="{name:'detail', params:{id:landmark.landmarkId}}"> 
    <ul>
    <li class="OneLine">{{landmark.landmarkName}}</li> 
    </ul> </router-link> 
    <button v-if="editshow === true" class="OneLine" v-on:click="handleEvent(landmark.landmarkId)">Delete </button>
    <br/>

    <!-- <Search></Search> -->
    <!-- <add :LandmarkId="landmark.place_id" :ItineraryId="itineraryId"></add> -->

    <!-- need button for edit itinerary and delete itinerary -->
  </div>
      <router-link :to="{ name: 'directions', params: { id: itinerary.itineraryId }}"><button>Get Optimized Route Directions</button></router-link>
    <div>
      <button v-on:click.prevent="editshow = true">Delete Landmarks from Itinerary</button>
      <!-- TODO: need this router to keep the itinerary and select it for user -->
      <router-link :to="{name:'home', params:{id:itineraryId}}">
      <button>Add Landmarks to Itinerary</button></router-link>
    </div>
  </div>
  
</template>

<script>
import auth from '../auth'

// import Vue from 'vue';


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
      this.selectedItinerary();
  },
  data() {
    return {
      editshow: false,
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
    getDate(datetime) {
      let date = new Date(datetime).toJSON().slice(0,10).replace(/-/g,'/')
      return date
    },
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
          this.getItinerary(this.$route.params.id);

          } else {
            console.log("Could not delete landmark");
          }
        })
        .catch(err => console.error(err));
    },
      handleEvent(id) {
    this.landmarkid = id;
    this.deleteLandmark();
    this.itinerary.dateOfTour = '';
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
    reloadPage(){
    window.location.reload()
  }
};
</script>

<style scoped>
#start{
  display: inline-block;
  width: 50%;
  margin: 0 25% 0 25%;
  text-align: center;
  padding: 8px;
  border-bottom: solid;
  border-width: 1px;
  margin-bottom: 5%;
  text-transform: uppercase;

}
h1{
  display: inline-block;
  width: 50%;
  margin: 0 25% 0 25%;
  text-align: center;
  padding: 8px;
  border-bottom: solid;
  border-width: 1px;
  text-transform: uppercase;
}
ul {
  justify-content: center;
  display: grid;
  font-size: 18px;
  margin-bottom: 0px;
}

.display{
  font-family: Verdana, Geneva, Tahoma, sans-serif;
    text-align: center;

}

.OneLine{
  display: inline;
}

</style>