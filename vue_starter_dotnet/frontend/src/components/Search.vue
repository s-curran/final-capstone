<template>
  <div class="search">
    <!-- <input type="text" placeholder="Enter your city..." v-model="city" /> -->
    <br />
    <div class="box">
      <label>Location:</label>
      <input type="text" id="text" v-model="address" @blur="getLatLong" placeholder="City, State"/>
    </div>
    <button
    class="search"
      type="button"
      @click="getLocation"
      v-bind:class="{locationAccessed: lat}"
    >Or Get Current Location</button>
    <br />
    <div class="box">
      <label>Distance:</label>

      <select v-model="radius">
        <option value="1609.34">1 mile</option>
        <option value="3218.69">2 miles</option>
        <option value="8046.72">5 miles</option>
        <option value="16093.4">10 miles</option>
      </select>
    </div>

    <div class="box">
      <label>Type:</label>
      <select v-model="type">
        <option value="tourist_attraction">Tourist Attraction</option>
        <option value="restaurant">Restaurant</option>
        <option value="stadium">Sports</option>
        <option value="park">Park</option>
        <option value="museum">Museum</option>
      </select>
    </div>
    <div class="box">
    <label id="opennow">Only open now</label>
    <input type="checkbox" v-model="opennow" />
    </div>
    <br />
    <button class="search" type="submit" @click="findLandmarks">Submit</button>
    <!-- <ul>
      <li v-for="result in results" v-bind:key="result.id">{{result.name}}</li>
    </ul>-->
  </div>
</template>

<script>
export default {
  name: "Search",
  data() {
    return {
      address: "",
      lat: "",
      long: "",
      // lat: "41.505550",
      // long: "-81.691498",
      radius: "",
      type: "",
      opennow: false,
      key: "AIzaSyANWIg-qW05HeNmXG2Yh1Fd7w8I9w4WXto",
      results: [],
      city: ""
    };
  },
  methods: {
    findLandmarks() {
      let url = `${process.env.VUE_APP_REMOTE_API}/search/places?location=${this.lat},${this.long}&radius=${this.radius}&type=${this.type}&opennow=${this.opennow}`;

      fetch(url)
        .then(response => {
          if (response.ok) {
            response.text().then(txt => {
              // console.log(txt)
              this.results = JSON.parse(txt).results;
              this.$emit("results", this.results);
            });
          } else {
            console.log(response.status);
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    getLatLong() {
      let url = `${process.env.VUE_APP_REMOTE_API}/search/address?address=${this.address}`;

      fetch(url)
        .then(response => {
          if (response.ok) {
            response.json().then(json => {
              this.lat = json.results[0].geometry.location.lat;
              this.long = json.results[0].geometry.location.lng;
            });
          } else {
            alert("Enter a different address");
          }
        })
        .catch(err => console.error(err));
    },

    getLocation() {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(this.showPosition);
      } else {
        alert("Geolocation is not supported by this browser");
      }
    },
    showPosition(position) {
      this.lat = position.coords.latitude;
      this.long = position.coords.longitude;
    }
  }
};
</script>

<style>
button.search{
    width: 300px;

}
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
button:focus {
  color: #03dbfc;
  border-color: #03dbfc;
}
input#text {
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
  width: 200px;
}
.locationAccessed {
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
input[checkbox] {
  width: 10px;
}
#opennow {
  width: 200px;
}

.box {
  margin: 0 auto 0 auto;
  display: flex;
  width: 300px;
  justify-content: space-between;
  align-items: center;
}

select {
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