<template>
  <div class="home">
    <h1 class="pageTitle">City Tours</h1>
    <!-- <search class="search" ></search> -->
    <search class="search" v-on:results="displayResults" v-model="searchResults"></search>

    <div v-if="searchResults.length >=1">
        <h3 class="listTitle">Landmarks to visit in {{cityName}}:</h3>
      <ul>
        <li v-for="result in searchResults" v-bind:key="result" class="result">
            {{result.name}}
        </li>
      </ul>
      </div>
      <div v-else>
        <h3 class="needToSearch">Enter a city above to view landmarks!</h3>
     </div>
  </div>
</template>

<script>
import Search from '@/components/Search.vue'

export default {
  name: 'home',
  components: {
    Search
  },
  data(){
    return{
    cityName: "Cleveland",

    searchResults: [
    // { id: "3cf5baf562b322d3bd794748a971263f6af071df", name: "Nautica Queen Cleveland's Dining Cruise Ship"},
    // { id: "6edc2f98f10c2d0137ecc4c135db3760605df971", name: "Shooters On the Water"},
    // { id: "f37878763520ba80ccd8fc4772137754d63c1c86", name: "Lago East Bank"},
    // { id: "7c9160b5e479779b9b1b3ecc6b72f4c3703c30df", name: "Alley Cat Oyster Bar"},
  ]
    };
  },
  methods: {
    displayResults(results)
    {
      this.searchResults = results;
    }
  }

}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Lobster&family=Pacifico&display=swap');
.pageTitle{
  font-family: 'Lobster', cursive;
      font-size: 55px;

}
.listTitle{
    font-family: verdana;
    font-size: 25px;
    text-align: center;
    margin-top: 20px;
    margin-bottom: 20px;
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
