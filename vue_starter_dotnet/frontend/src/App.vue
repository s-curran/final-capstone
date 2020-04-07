<template>
  <div id="app">
    <div id="nav">
    <!-- <img src="./views/pic1.png" /> -->
    <h1 id="pageTitle">City Tours</h1>

      <div v-if="user">
        
        <div>
          <button v-on:click="logout">Logout</button>
        </div>
      </div>
      <div v-else>
          <button><router-link id="link" :to="{name: 'login'}">Login</router-link></button>
          <div></div>
          <button><router-link id="link" :to="{name: 'register'}">Register</router-link></button>

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
  background: url(./views/pic10.jpg);
}
#link{
  text-decoration: none;
}
button{
  margin-left: 10px;
  background-color: white;
  text-align: right;
  Font: Verdana;
  font-size: 20px;
  border-radius: 8px;
  margin:10px;
}
#pageTitle{
  font-family: 'Lobster', cursive;
  font-size: 55px;
  color: #515458 ;
  text-align: center;
}
</style>