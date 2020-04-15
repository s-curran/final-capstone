<template>
  <div>
    <h2 id="start">{{ this.trip[0].start_address}}</h2>
    <div v-for="leg in trip" v-bind:key="leg.end_address">
      <h4>{{leg.duration.text}} - {{leg.distance.text}}</h4>
      <ul>
        <li
          v-for="step in leg.steps"
          v-bind:key="step.index"
          v-html="replaceText(step.html_instructions, step.distance.text)"
        ></li>
      </ul>
      <!-- name of establishment? -->
      <!-- <h3>{{getLandmark(trip.geocoded_waypoints[(trip.routes[0].waypoint_order[leg.index])+1].place_id)}}</h3> -->
    <div v-for="landmark in landmarks" v-bind:key="landmark.landmarkId">
    <h3 v-if="landmark.landmarkAddress.substring(0,4) === leg.end_address.substring(0,4)">{{landmark.landmarkName}}</h3>
    </div>
      <h2>{{leg.end_address}}</h2>
    </div>
  </div>
</template>

<script>
import auth from "../auth";

export default {
  name: "Directions",
  data() {
    return {
      trip: [],
      geo: [],
      landmarks: []
    };
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
            console.log("error fetching trip");
          }
        })
        .then(json => {
          this.trip = json.routes[0].legs;
          
        })
        .catch(err => console.error(err));
    },
    replaceText(string, distance) {
      return `${string} - ${distance}`;
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
            this.landmarks = json.landmarks;
          }
        )
        .catch((err) => console.error(err));
    }
  },
  created() {
    this.getDirections(this.$route.params.id);
    this.getItinerary(this.$route.params.id);
  }
};
</script>

<style scoped>
div {
  text-align: center;
  font-family: verdana;
}
ul {
  max-inline-size: 50%;
  justify-content: center;
  text-align: left;
  display: grid;
  list-style-type: square;
  margin-bottom: 15px;
  font-size: 18px;
  
}
li {
  margin-bottom: 10px;
}
h2{
  font-size: 20px;
}
#start{
  background: lightgrey;
  font-size: 20px;

}
h3 {
  background: #515458;
  color: #03dbfc;
  padding: 10px;
  font-size: 24px;
  text-transform: uppercase;
  text-align: center;
}
</style>