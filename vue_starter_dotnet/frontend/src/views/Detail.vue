<template>
    <div id="detail">
      <h1 class="itemDetails">
        <a target="_blank" :href="landmark.website">{{landmark.name}}</a></h1>
      <h3 class="itemDetails">{{landmark.vicinity}}</h3>
      <h3 class="itemDetails">{{landmark.international_phone_number}}</h3>
      <!-- <h3 class="itemDetails">{{landmark.website}}</h3> -->
      <h3 class="itemDetails">Rating: {{correctRating}} out of 5 ({{correctNumRatings}} ratings)</h3>
      <!-- <button v-on:click="addRating">Add Rating</button> -->
<button v-if="ratingshow === false" v-on:click="ratingshow = true">Submit a new Rating</button>

      <div v-if="ratingshow === true">
<p>
<label for="rating">Your Rating:</label>
        <select id="rating" v-model.number="inputedAverageRating">
          <option>5</option>
          <option>4</option>
          <option>3</option>
          <option>2</option>
          <option>1</option>
        </select>
      </p>
        <button type="submit" value="Submit" v-on:click="addRating">Submit Rating</button>  
        <button class="cancel" v-if="ratingshow === true" v-on:click="ratingshow = false">Cancel</button>  
      </div>
      <div>
      <button v-if="!addshow && user" v-on:click="addshow = true">Add this landmark to an Existing Itinerary</button>
      </div>
      <div v-if="user && addshow">
        <div>
      <select-itin @selected="handleEvent"></select-itin>
        <add @landmark-added="addshow = false" :LandmarkId="landmark.place_id" :LandmarkName="landmark.name" :LandmarkAddress="landmark.vicinity" :ItineraryId="itineraryId"></add>
        <button class="cancel" v-if="addshow" v-on:click="addshow = false">Cancel</button>  
        </div>
      </div>
    </div>
</template>

<script>
import Add from '@/components/AddToItinerary.vue'
import SelectItin from '@/components/SelectItinerary.vue'
import auth from "../auth";

export default {
name: "Detail",
components: {
  Add,
  SelectItin
},
data() {
   return {
     ratingshow: false,
     addshow: false,
     itineraryId: '',
     landmark : {},
     cRating: {},
     apiRating: '',
     user: null,
     inputedAverageRating: 5,
    //  correctRating: '',
    }
},
computed:{


      correctRating: function() {
       let correctRating;
       if(this.cRating.averageRating != null)
        {
          correctRating = this.cRating.averageRating; 
        }
        else{
          correctRating = this.landmark.rating;
        }
     return correctRating;
      
},
      correctNumRatings: function() {
       let correctNumRatings;
       if(this.cRating.numberOfRatings != null)
        {
          correctNumRatings = this.cRating.numberOfRatings;
        }
        else{
          correctNumRatings = this.landmark.user_ratings_total;
        }
     return correctNumRatings;
      },

       newRatingInput: function() {
      return {
        landmarkVM: {
            LandmarkId: this.landmark.place_id,
            LandmarkName: this.landmark.name,
            LandmarkAddress: this.landmark.vicinity,
        },
        newAverageRating: (((this.inputedAverageRating + (this.correctRating * this.correctNumRatings))/(this.correctNumRatings + 1)).toFixed(1)),
        newNumberOfRatings: this.correctNumRatings + 1,
      };
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
        // .then((landmark) =>{
        //     this.getCRating(landmark);
        // })
        .catch((err) => console.error(err));
    },
  handleEvent(selected) {
    this.itineraryId = selected;
    // alert("Location added");
  },
  getCRating(id) {
            let url = `${process.env.VUE_APP_REMOTE_API}/itinerary/getRating?placeId=${id}`
 
            fetch(url, {
                method: "GET",          
                headers: new Headers({
            Authorization: "Bearer " + auth.getToken(),
            "Accept": "application/json",
            "Content-Type": "application/json"
        }),
            })
            .then(response => {
                if(response.ok) {
                    response.json()
                    .then(json => {
                        this.cRating = json;
                    })
                } else {
                    console.log('Could not get itineraries')
                }
            })
            .catch(err => console.error(err))
        },
          addRating() {
            let url = `${process.env.VUE_APP_REMOTE_API}/itinerary/createRating`
 
            fetch(url, {
                method: "POST",          
                headers: new Headers({
            Authorization: "Bearer " + auth.getToken(),
            "Accept": "application/json",
            "Content-Type": "application/json"
        }),
                body: JSON.stringify(this.newRatingInput)

            })
            .then(response => {
                if (response.ok) {
                alert("Rating has been posted!");
                this.getLandmark(this.$route.params.id);
                this.getCRating(this.$route.params.id);
                this.user = auth.getUser();
                this.ratingshow = false;
                } else {
                console.log("Could not add rating");
                }
            })
            .catch(err => console.error(err))
        },

    finalAddRating(landmark){
    this.apiRating = landmark.rating;
  },
  displayCorrectRating(apiRating, cRating){
      if(cRating.AverageRating != null)
      {
        this.correctRating = cRating.AverageRating;
      }
      else
      {
        this.correctRating = apiRating;
      }
  
          console.log(this.cRating.AverageRating);
    console.log(this.apiRating);
    console.log(this.correctRatingRating);
  }

},

  


created () {
    this.getLandmark(this.$route.params.id);
    this.getCRating(this.$route.params.id);
    this.user = auth.getUser();
    // this.getApiRating(this.landmark);
    // this.displayCorrectRating(this.apiRating, this.cRating);
},

mounted () {
    // this.getLandmark(this.$route.params.id);
    // this.getCRating();
    // this.getApiRating(this.landmark);
    // this.displayCorrectRating(this.apiRating, this.cRating);
}
}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Lobster&family=Pacifico&display=swap');
.cancel{
  color: red;
  border-color: red;
}

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
