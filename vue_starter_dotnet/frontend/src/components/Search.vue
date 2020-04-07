<template>
    <div class="search">
        <input type="text" placeholder='Enter your city...' v-model='city' /><br/>

        <!-- <button type='submit' @click='getLocation'>Search Near You</button> -->

        <label>Distance: </label>
        <select v-model="radius">
            <option value='1609.34'>1 mile</option>
            <option value='3218.69'>2 miles</option>
            <option value='8046.72'>5 miles</option>
            <option value='16093.4'>10 miles</option>
        </select><br/>
        <label>Type: </label>
        <select v-model="type">
            <option value='tourist_attraction'>Tourist Atrraction</option>
            <option value='restaurant'>Restaurant</option>
            <option value='stadium'>Sports</option>
            <option value='zoo'>Zoo</option>
            <option value='museum'>Museum</option>
        </select><br/>
        <label>Only open now </label>
        <input type='checkbox' v-model='opennow' value='true' /><br/>
        <button type='submit' @click="findLandmarks">Submit</button>
        <ul>
            <li v-for="result in results" v-bind:key="result.id">{{result.name}}</li>
        </ul>
    </div>
</template>

<script>
export default {
    name: 'Search',
    data() {
        return {
            //lat: '',
            //long: '',
            lat: '41.505550',
            long: '-81.691498',
            radius: '',
            type: '',
            opennow: '',
            key: 'AIzaSyANWIg-qW05HeNmXG2Yh1Fd7w8I9w4WXto',
            results: [],
            city: ''
        }
    },
    methods: {
        findLandmarks() {
            let url = `https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=${this.lat},${this.long}&radius=${this.radius}&type=${this.type}&key=${this.key}`;
            if (this.opennow) {
                url += '&opennow=true'
            }
            fetch(url, {
                mode: 'no-cors'
            })
                .then( response => {
                    if (response.ok) {
                    response.json()
                    .then( json => {
                        this.results = json;
                    })
                    } else {
                        console.log(response.status);
                    }
                }).catch(err => {console.log(err)});
              
        },
        getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(this.showPosition);
            } else {
                alert('Geolocation is not supported by this browser')
            }
        },
        showPosition(position) {
            this.lat = position.coords.latitute;
            this.long = position.coords.longitude;
        }
    }
}
</script>

<style>

</style>