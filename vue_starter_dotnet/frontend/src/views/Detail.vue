<template>
    <div class="detail">
       <h2> Site Details </h2>
      <h2>{{landmark.name}}</h2>
        
    </div>
</template>

<script>
export default {
name: "Detail",

data() {
   return {
     landmark : {}
      //   landmark:  {
         // "geometry" : {
         //    "location" : {
         //       "lat" : 41.4868227,
         //       "lng" : -81.8466157
         //    },
         //    "viewport" : {
         //       "northeast" : {
         //          "lat" : 41.48788218029149,
         //          "lng" : -81.84514436970849
         //       },
         //       "southwest" : {
         //          "lat" : 41.4851842197085,
         //          "lng" : -81.84784233029151
         //       }
         //    }
         // },
         // "icon" : "https://maps.gstatic.com/mapfiles/place_api/icons/generic_recreational-71.png",
         // "id" : "0c05f873ded69765d418cd0859f2d82ce2e2393e",
         // "name" : "Rocky River Park",
         // "opening_hours" : {
         //    "open_now" : true
         // },
         // "photos" : [
         //    {
         //       "height" : 3024,
         //       "html_attributions" : [
         //          "\u003ca href=\"https://maps.google.com/maps/contrib/106971071873360326318\"\u003eMohammed Qassim\u003c/a\u003e"
         //       ],
         //       "photo_reference" : "CmRaAAAAiyuk3TOM6dCzDGrFz1K3z4CatJL1Z_yNsrKnDhk6rcIuvphdi75lgOggl7X0lHWDaO6qeQN2O0BOaEsW9oeE5oSB6YDMt2FCsXDD2Qd-rd40aiHzamOS28JiKa1vZSdYEhACm6-DtG36_LAzw8GkPzyPGhS_yktSIF6PU72s0pyxvbigYFy64Q",
         //       "width" : 4032
         //    }
         // ],
         // "place_id" : "ChIJe4i8e77yMIgRBiii_wLdhaE",
         // "plus_code" : {
         //    "compound_code" : "F5P3+P9 Rocky River, Ohio, United States",
         //    "global_code" : "86HWF5P3+P9"
         // },
         // "rating" : 4.7,
         // "reference" : "ChIJe4i8e77yMIgRBiii_wLdhaE",
         // "scope" : "GOOGLE",
         // "types" : [ "park", "tourist_attraction", "point_of_interest", "establishment" ],
         // "user_ratings_total" : 529,
         // "vicinity" : "20250 Beach Cliff Boulevard, Rocky River"
      // },

    }
},
methods: {
getLandmark(id) {
      fetch(`${process.env.VUE_APP_REMOTE_API}/search/place?placeid=${id}`, {
        method: 'GET'
      })
        .then((response) => {
          if (response.ok) {
            return response.json();
          } else {
              console.log('error fetching place')
          }
        })
        .then((json) => {
            this.landmark = json.result;
          }
        )
        .catch((err) => console.error(err));
    },
  
},

created () {
    this.getLandmark(this.$route.params.id);
}
}
</script>

<style>

</style>