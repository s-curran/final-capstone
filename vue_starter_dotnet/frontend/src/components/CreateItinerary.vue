<template>
  <div>
    <div class="box">
    <label>Itinenary Name</label>
    <input type="text" v-model="itinerary.TourName" />
    </div>
    <div class="box">
    <label>Itinenary Date</label>
    <input type="date" v-model="itinerary.DateOfTour" />
    </div><div class="box">
    <label>Starting Address</label>
    <input type="text" v-model="itinerary.StartPoint" />
    </div>
   
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
      DateOfTour: "",
      TourName: "",
      StartPoint: ""
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
              response.json()
              .then(json =>{
                  console.log(json)
                this.$emit("itinerary-added")
              })
          }
          else {
              console.log("Itinerary failed to save!!")
          }
      })
      .catch(err => {
          console.error(err)
      })
  },
  
}
}
</script>

<style scoped>
button {
  display: inline-block;
  padding: 0.5em 3em;
  border: 0.16em solid #515458;
  margin: 5px 0.3em 0.3em 0;
  box-sizing: border-box;
  font-size: 14px;
  text-decoration: none;
  text-transform: uppercase;
  font-family: "Verdana", sans-serif;
  font-weight: 400;
  color: #515458;
  text-align: center;
  transition: all 0.15s;
  background-color: white;

}
button:hover {
  color: #03dbfc;
  border-color: #03dbfc;
}
button:active {
  color: #03dbfc;
  border-color: #03dbfc;
}
label {
  text-align: left;
  text-transform: uppercase;
  font-family: "Verdana", sans-serif;
  font-weight: 500;
  color: #515458;
}

.box {
  margin: 0 auto 0 auto;
  display: flex;
  width: 400px;
  justify-content: space-between;
  align-items: center;
}

input {
  display: inline-block;
  padding: 0.5em 1em;
  border: 0.16em solid #515458;
  margin: 5px 0.3em 0.3em 0;
  box-sizing: border-box;
  font-size: 14px;
  text-align-last: right;
  text-decoration: none;
  text-transform: uppercase;
  font-family: "Verdana", sans-serif;
  font-weight: 400;
  color: #515458;
  text-align: center;
}
</style>