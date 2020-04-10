<template>
  <div id="app">
    <div id="nav">
    <!-- <img src="./views/pic1.png" /> -->
    <h1 id="pageTitle">City Tours</h1>

      <div v-if="user">
        
        <div>
          <button v-on:click="logout">Logout</button>
          <div>
          <button v-on:click="viewItinerary">View Saved Itineraries</button>
          </div>
        </div>
      </div>
      <div v-else>
          <button><router-link id="link" :to="{name: 'login'}">Login</router-link></button>
          <div></div>
          <button><router-link id="link" :to="{name: 'register'}">Register</router-link></button>
          <!-- <button> <router-link id="link" :to="{mane: 'detail'}">Detail</router-link></button> Should not be on this page-->
      </div>
      <button><router-link id="link" to="/">Home</router-link></Button>
    </div>

    <router-view />
  </div>
</template>

<script>
import auth from "@/auth.js";

export default {
  data() {
    return {
      user: null,
    };
  },
  methods: {
    logout() {
      auth.logout();
      this.$router.push('/login');
      this.user = null;
    },
    viewItinerary() {
      this.$router.push('/itineraries');
    },

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
<style scoped>
#nav{
  background-image: url(./views/pic13.jpg);
  background-repeat:no-repeat;
  background-position: center;
  background-size: cover;
  background-color: #A4C2ED;
}
#link{
  text-decoration: none;
}
button{
  background-color: white;
  text-align: right;
  Font: Verdana;
  font-size: 12px;
  border-radius: 8px;
  margin:10px;
}
#pageTitle{
  font-family: 'Lobster', cursive;
  font-size: 55px;
  color: white ;
  text-align: center;
}
</style>