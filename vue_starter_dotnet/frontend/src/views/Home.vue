<template>
  <div class="home">
    <!-- <search class="search" ></search> -->
    <search class="search" v-on:results="displayResults" v-model="searchResults"></search>
    <br />
    <div v-if="user" class="itin">
      <div v-if="toggle === false" class="button">
        <button type="button" @click="toggleForm">Create Itinerary</button>
      </div>
      <div v-else>
        <create-itin class="create" @itinerary-added="toggleForm"></create-itin>
      </div>
      <p>-OR-</p>
      <select-itin @selected="handleEvent" class="selectItin"></select-itin>
    </div>
    <div v-if="searchResults.length >=1">
      <h3 class="listTitle">Landmarks to visit {{cityName}}:</h3>
      <ul>
        <div v-for="result in searchResults" v-bind:key="result" id="result">
          <router-link
            :to="{ name: 'detail', params: { id: result.place_id }}"
            class="resultText"
          >{{result.name}}</router-link>
          <div v-if="itineraryId" class="addTo">
            <add
              :LandmarkId="result.place_id"
              :LandmarkName="result.name"
              :LandmarkAddress="result.vicinity"
              :ItineraryId="itineraryId"
            ></add>
          </div>
          <!-- <a v-bind:href="`{{result.name}}`">{{result.name}}</a> -->
        </div>
      </ul>
    </div>
    <div v-else>
      <h3 class="needToSearch">Enter a city above to view landmarks!</h3>
    </div>
  </div>
</template>

<script>
import Search from "@/components/Search.vue";
import CreateItin from "@/components/CreateItinerary.vue";
import SelectItin from "@/components/SelectItinerary.vue";
import auth from "../auth";
import Add from "@/components/AddToItinerary.vue";

export default {
  name: "home",
  components: {
    Search,
    CreateItin,
    SelectItin,
    Add
  },
  data() {
    return {
      cityName: "near you",
      itineraryId: "",
      user: null,
      toggle: false,
      searchResults: [
        // { id: "3cf5baf562b322d3bd794748a971263f6af071df", name: "Nautica Queen Cleveland's Dining Cruise Ship"},
        // { id: "6edc2f98f10c2d0137ecc4c135db3760605df971", name: "Shooters On the Water"},
        // { id: "f37878763520ba80ccd8fc4772137754d63c1c86", name: "Lago East Bank"},
        // { id: "7c9160b5e479779b9b1b3ecc6b72f4c3703c30df", name: "Alley Cat Oyster Bar"},
      ]
    };
  },
  methods: {
    displayResults(results) {
      this.searchResults = results;
    },
    handleEvent(selected) {
      this.itineraryId = selected;
    },
    toggleForm() {
      this.toggle = !this.toggle;
    }
  },
  created() {
    this.user = auth.getUser();
  },
  watch: {
    $route: function() {
      this.user = auth.getUser();
    }
  }
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Lobster&family=Pacifico&display=swap");
.pageTitle {
  font-family: "Lobster", cursive;
  font-size: 55px;
}
.listTitle {
  font-family: verdana;
  font-size: 25px;
  text-align: center;
  margin-top: 20px;
  margin-bottom: 20px;
}
p {
  font-family: Verdana, sans-serif;
  text-align: center;
  color: #515458;
}
.search {
  text-align: center;
}
.create {
  text-align: center;
}
.selectItin {
  text-align: center;
}
#result {
  font-family: verdana;
  font-size: 18px;
  text-align: left;
  list-style-type: none;
  margin: 20px auto;
  /* margin-top: 20px;
  margin-bottom: 20px; */
  display: flex;
  max-width: 500px;
  justify-content: space-between;
  align-self: center;
}
#result > a {
  align-self: center;
}

.needToSearch {
  font-family: verdana;
  font-size: 25px;
  text-align: center;
  margin-top: 20px;
  margin-bottom: 20px;
}
.button {
  text-align: center;
  margin: auto;
}
.addTo {
  display: inline;
  
}
.addTo > div {
  display: inline;
}
a {
  display: inline;
  text-decoration: none;
  text-transform: uppercase;
  color: #515458;
}
a:hover {
  color: #03dbfc;
}
</style>
