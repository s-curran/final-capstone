<template>
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
      <div class="alert alert-danger" role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>
      <div class="alert alert-success" role="alert" v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div id="username">
      <label for="username" class="sr-only">Username</label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      </div>
      <div id="password">
      <label for="password" class="sr-only">Password</label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      </div>
      <div id="btn"><button type="submit">Sign in</button></div>
      <div id="register">
      <router-link :to="{ name: 'register' }">Need an account? Click here to create an account.</router-link>
      </div>
    </form>
  </div>
</template>

<script>
import auth from '../auth';

export default {
  name: 'login',
  components: {},
  data() {
    return {
      user: {
        username: '',
        password: '',
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/login`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.user),
      })
        .then((response) => {
          if (response.ok) {
            return response.text();
          } else {
            this.invalidCredentials = true;
          }
        })
        .then((token) => {
          if (token != undefined) {
            if (token.includes('"')) {
              token = token.replace(/"/g, '');
            }
            auth.saveToken(token);
            this.$router.push('/');
          }
        })
        .catch((err) => console.error(err));
    },
  },
};
</script>

<style scoped>
#register{
  text-align: center;
  margin: 10px;
}
#username{
  text-align: center;
  margin: 10px;
}
#password{
  text-align: center;
  margin: 10px;
  margin-bottom: 30px;
}
h1 {
  background:#515458;
  color: #03dbfc;
  padding:10px;
  font-size:24px;
  text-transform: uppercase;
  text-align: center;
  margin-bottom: 0px;
  border-top-left-radius: 10px;
  border-top-right-radius: 10px;
}
.sr-only{  
  width:450px;
  background: #fff;
  margin: 50px auto;
  font: Verdana;
  font-size: 15px;
  border-radius: 10px;
}
input {
  border-bottom:1px solid #f2f2f2;
  padding:10px 10px;

}
#btn{
  text-align: center;
}
button{
  background-color: #03dbfc;
  Font: Verdana;
  font-size: 15px;
  border-radius: 8px;
  margin:10px;
}
button:hover{
box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19);

}
</style>
