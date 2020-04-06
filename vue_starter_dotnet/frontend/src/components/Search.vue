<template>
    <div class="search">
        <input type="text" placeholder='Enter your city...' v-model='city' />
    </div>
</template>

<script>
export default {
    data() {
        return {
            city: ''
        }
    }
}
</script>

<style>

</style>