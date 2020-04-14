<template>
  <div>
    <button type="button" @click="addToItin">Add to Itinerary</button>
  </div>
</template>

<script>
import auth from "../auth";

export default {
  name: "Add",
  props: {
 
      LandmarkId: String,
      LandmarkName: String,
      LandmarkAddress: String,
      ItineraryId: Number,
       
  },
  computed: {
    item: function() {
      return {
        LandmarkId: this.LandmarkId,
        LandmarkName: this.LandmarkName,
        LandmarkAddress: this.LandmarkAddress,
        ItineraryId: this.ItineraryId
      };
    }
  },
  methods: {
    addToItin() {
      let url = `${process.env.VUE_APP_REMOTE_API}/itinerary/add`;

      fetch(url, {
        method: "POST",
        headers: new Headers({
          Authorization: "Bearer " + auth.getToken(),
          Accept: "application/json",
          "Content-Type": "application/json"
        }),
        body: JSON.stringify(this.item)
      })
        .then(response => {
          if (response.ok) {
            alert("Landmark has been added to itinerary");
          } else {
            console.log("Could not add landmark");
          }
        })
        .catch(err => console.error(err));
    }
  }
};
</script>

<style>
</style>