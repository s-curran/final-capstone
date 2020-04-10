<template>
  <div>
    <label>Itinenary Name</label>
    <input type="text" v-model="itinerary.itineraryName" />
    <label>Itinenary Date</label>
    <input type="date" v-model="itinerary.itineraryDate" />
    <label>Starting Address</label>
    <input type="text" v-model="itinerary.itineraryLocation" />
    <button type="button" @click="saveItin">Create</button>
  </div>
</template>

<script>
import auth from "../auth"
export default {
  name: "CreateItin",
  data() {
    return {
        itinerary: {
      itineraryDate: "",
      itineraryName: "",
      itineraryLocation: ""
        }
    };
  },
  methods: {
      saveItin(){
    let url = `${process.env.VUE_APP_REMOTE_API}/itinerary/create`
    fetch(url, {
        method: "POST",
          headers: new Headers({
            Authorization: "Bearer " + auth.getToken(),
            "Accept": "application/json",
            "Content-Type": "application/json"
        }),
    body: JSON.stringify(this.itinerary)
      })
      .then(response => {
          if (response.ok){
              response.json().then(json =>{
                  console.log(json)
                  alert("Your itinerary has been created!!")
              })
          }
          else {
              console.log("Itinerary failed to save!!")
          }
      })
      .catch(err => {
          console.error(err)
      })
  }
}
}
</script>

<style>
</style>