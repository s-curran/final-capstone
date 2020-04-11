<template>
    <div id="detail">
      <h1 class="heading"> Site Details </h1>
      <h3 class="itemDetails">Name of Attraction: {{landmark.name}}</h3>
      
      <h3 class="itemDetails">Phone Number: {{landmark.international_phone_number}}</h3>
      <h3 class="itemDetails">Address: {{landmark.vicinity}}</h3>
      <h3 class="itemDetails">Composite Rating (out of 5): {{landmark.rating}}</h3>
      <h3 class="itemDetails">Total number of ratings to date: {{landmark.user_ratings_total}}</h3>
      <h3 class="itemDetails">Website:  {{landmark.website}}</h3>
      
      <select-itin @selected="handleEvent"></select-itin>
        <add :LandmarkId="landmark.place_id" :LandmarkName="landmark.name" :LandmarkAddress="landmark.vicinity" :ItineraryId="itineraryId"></add>
    </div>
</template>

<script>
import Add from '@/components/AddToItinerary.vue'
import SelectItin from '@/components/SelectItinerary.vue'

export default {
name: "Detail",
components: {
  Add,
  SelectItin
},
data() {
   return {
     itineraryId: '',
     landmark : {}
    }
},
methods: {
getLandmark(id) {
      fetch(`${process.env.VUE_APP_REMOTE_API}/search/place?placeid=${id}`, {
        method: 'GET'
      })
        .then((response) => {
          if (response.ok) {
             console.log(response.json);
            return response.json();
           
          } else {
              console.log('error fetching place')
          }
        })
        .then((json) => {
            this.landmark = json.result;
          }
        )
        .catch((err) => console.error(err));
    },
  handleEvent(selected) {
    this.itineraryId = selected;
  }
},

  


created () {
    this.getLandmark(this.$route.params.id);
}
}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Lobster&family=Pacifico&display=swap');
.pageTitle{
  font-family: 'Lobster', cursive;
      font-size: 55px;

}
#detail{
        font-family: verdana;
    text-align: center;
    margin-top: 10px;
    margin-bottom: 10px;
}
.itemDetails{
    font-family: verdana;
    text-align: center;
    margin-top: 10px;
    margin-bottom: 10px;
}
.search{
  text-align: center;
}
.result{
    font-family: verdana;
    font-size: 18px;
    text-align: center;
    list-style-type: none;
    margin-top: 20px;
    margin-bottom: 20px;
}
.needToSearch{
    font-family: verdana;
    font-size: 25px;
    text-align: center;
    margin-top: 20px;
    margin-bottom: 20px;
}
</style>
