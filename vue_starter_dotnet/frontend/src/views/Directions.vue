<template>
  <div>
      <div v-for="leg in trip" v-bind:key="leg.end_address">
          <h3>{{leg.end_address}}</h3>
          <h4>{{leg.duration.text}} - {{leg.distance.text}}</h4>
          <ul>
              <li v-for="step in leg.steps" v-bind:key="step.index" v-html="replaceText(step.html_instructions, step.distance.text)"></li>
          </ul>
      </div>
  </div>
</template>

<script>
import auth from "../auth";

export default {
  name: "Directions",
  data() {
      return {
          trip: []
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

<style>
</style>