<template>
  <div>
    <label>Itinenary Name</label>
    <input type="text" v-model="itineraryName" />
<label>Itinenary Date</label>
    <input type="date" v-model="itineraryDate" />
<label>Starting Address</label>
    <input type="text" v-model="itineraryLocation" />
    
  </div>
</template>

<script>
export default {
    name: "CreateItin",
  data() {
    return {
      itineraryDate: "",
      itineraryName: "",
      itineraryLocation: ""
    };
  }
};
</script>

<style>
</style>