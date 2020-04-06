<template>
    <div class="search">
        <input type="text" placeholder='Enter your city...' v-model='city' />
        <button type='submit' @click="findLandmarks">Submit</button>
    </div>
</template>

<script>
export default {
    name: 'Search',
    data() {
        return {
            
            lat: '41.505550',
            long: '-81.691498',
            radius: '1500',
            key: 'AIzaSyANWIg-qW05HeNmXG2Yh1Fd7w8I9w4WXto',
            results: []
            //city: ''
        }
    },
    methods: {
        findLandmarks() {
            const apiUrl = 'https://maps.googleapis.com/maps/api/place/nearbysearch/json?location='
             + this.lat + ',' + this.long + '&radius=' + this.radius + '&key=' + this.key;
            fetch(apiUrl, {
                Method: 'GET'
            })
                .then( response => {
                    response.json()
                    .then( json => {
                        this.results = json;
                    })
                }).catch(err => {console.log(err)});
              
        }
    }
}
</script>

<style>

</style>