<template>
  <div>
    <!-- get the list from backend for this user - loop through and display landmark names in order -->
    <div>
        <ul>
            <li v-for="itin in itineraries" v-bind:key="itin.itineraryId" >
            <router-link :to="{ name: 'itinerary', params: { id: itin.itineraryId }}">{{itin.tourName}}</router-link>
            </li>
      </ul>
    </div>
  </div>
</template>

<script>
import auth from '../auth'

export default {
  name: 'ItineraryList',
  components: {},
  created(){
      this.getItineraries();
  },
  data() {
    return {
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
    }
  },
};
</script>

<style>

</style>