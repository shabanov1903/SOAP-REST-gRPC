<template>
  <LibraryComponent/>
</template>

<script>
import LibraryComponent from './components/LibraryComponent.vue'

export default {
  name: 'App',
  components: {
    LibraryComponent
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
