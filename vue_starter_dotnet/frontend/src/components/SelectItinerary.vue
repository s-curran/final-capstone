<template>
  <div>
    <select v-model='selected' @focus="getItins" @click='selectItin'>
        <option value="" disabled selected>Select your Itinerary</option>
      <option v-for="option in options" v-bind:key="option.itineraryId" v-bind:value="option.itineraryId">{{option.tourName}}</option>
      <option v-if="showCreate" value="Create Itinerary">Create New Itinerary</option>
    </select>
    <!-- <button type='button' @click='selectItin'>Select</button> -->
  </div>
</template>

<script>
import auth from '../auth'

export default {
    name: 'SelectItin',
    props: {
        show: Boolean
    },
    data() {
        return {
            options: [],
            selected: ''
        }
    },
    computed: {
        showCreate: function() {
            return this.show;
        }
    },
    methods: {
        getItins() {
            let url = `${process.env.VUE_APP_REMOTE_API}/itinerary/list`

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
                        this.options = json;
                    })
                } else {
                    console.log('Could not get itineraries')
                }
            })
            .catch(err => console.error(err))
        },
        selectItin() {
            this.$emit('selected', this.selected)
            if (this.selected === 'Create Itinerary'){
                this.selected = '';
            }
        }
    },
    created() {
        this.getItins();
    }
}
</script>

<style>
</style>