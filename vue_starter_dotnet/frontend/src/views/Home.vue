<template>
  <div class="home">
    <h1>Home</h1>
    <search></search>
    <p>You must be authenticated to see this</p>
  </div>
</template>

<script>
import Search from '@/components/Search.vue'

export default {
  name: 'home',
  components: {
    Search
  }
}
</script>
