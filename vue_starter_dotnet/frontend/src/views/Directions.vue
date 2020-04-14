<template>
  <div>
    <h3>{{ this.trip[0].start_address}}</h3>
      <div v-for="leg in trip" v-bind:key="leg.end_address">
          <h4>{{leg.duration.text}} - {{leg.distance.text}}</h4>
          <ul>
              <li v-for="step in leg.steps" v-bind:key="step.index" v-html="replaceText(step.html_instructions, step.distance.text)"></li>
          </ul>
          <!-- name of establishment? -->
        <h3>{{leg.end_address}}</h3>
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
            console.log("error fetching trip");
          }
        })
        .then(json => {
          this.trip = json.routes[0].legs;
        })
        .catch(err => console.error(err));
    },
    replaceText(string, distance) {
        return `${string} - ${distance}`
    },
  },
  created() {
      this.getDirections(this.$route.params.id)
  }
};
</script>

<style scoped>
div{
  text-align: center;
  font-family: verdana;

}
ul{
justify-content: center;
text-align: left;
display: grid;
list-style-type: square;
margin-bottom: 15px;
font-size: 18px;
}
li{
  margin-bottom: 10px;
}
h3{
  background:#515458;
  color: #03dbfc;
  padding:10px;
  font-size:24px;
  text-transform: uppercase;
  text-align: center;
}
</style>