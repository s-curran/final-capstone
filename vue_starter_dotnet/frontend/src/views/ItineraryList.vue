<template>
  <div>
    <!-- get the list from backend for this user - loop through and display itineraries -->
    <div>
        <h1 v-for="itin in itineraries" v-bind:key="itin.itineraryId" >
            <router-link :to="{ name: 'itinerary', params: { id: itin.itineraryId }}">{{itin.tourName}}</router-link>
        </h1>
        <div></div>
    <button v-on:click.prevent="selectshow = true">Delete an Itinerary</button>
    <div v-if="selectshow === true">
      <SelectItin @selected="handleEvent"></SelectItin>
      <button v-on:click.prevent="deleteItinerary" >Delete</button>
      <button v-on:click.prevent="selectshow = false">Cancel</button>
    </div>

    <button  v-on:click.prevent="showform = true">Create a New Itinerary</button>
    <div v-if="showform === true">
    <CreateItin ></CreateItin>
    <button v-on:click.prevent="showform = false">Cancel</button>
    </div>


    </div>
  </div>
</template>

<script>
import auth from '../auth'
import CreateItin from '@/components/CreateItinerary.vue'
import SelectItin from '@/components/SelectItinerary.vue'

export default {
  name: 'ItineraryList',
  components: {
    CreateItin,
    SelectItin
  },
  created(){
      this.getItineraries();
  },
  data() {
    return {
      itin: {itineraryId: ''},
      selectshow: false,
      showform: false,
      itineraries: [
           {
          itineraryId: '',
          userId: '',
          dateOfTour: '',
          tourName: '',
          startPoint: ''
      }
      ]
    };
  },
  methods: {
    getItineraries() {      
      fetch(`${process.env.VUE_APP_REMOTE_API}/itinerary/list`, {
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
            this.itineraries = json;
          }
        )
        .catch((err) => console.error(err));
    },
      handleEvent(selected) {
    this.itin.itineraryId = selected;
  },
      deleteItinerary() {   
      let url = `${process.env.VUE_APP_REMOTE_API}/itinerary/delete`;
      fetch(url, {
        method: "DELETE",
        headers: new Headers({
          Authorization: "Bearer " + auth.getToken(),
          Accept: "application/json",
          "Content-Type": "application/json"
        }),
        body: JSON.stringify(this.itin.itineraryId)
      })
        .then(response => {
          if (response.ok) {
            alert("Itinerary has been deleted");
          } else {
            console.log("Could not delete itinerary");
          }
        })
        .catch(err => console.error(err));
   
    },

  },
};
</script>

<style>

</style>