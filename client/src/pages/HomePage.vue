<template>
  <div class="home container-fluid">
    <div class="card-columns mt-3 m-2 text-nowrap">
      <keep-component v-for="keep in keeps" :key="keep" :keep-prop="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import KeepComponent from '../components/KeepComponent'

export default {
  name: 'Home',
  setup() {
    onMounted(() => {
      keepService.GetAll()
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { KeepComponent }
}
</script>

<style scoped lang="scss">
.grid-item { width: 200px; }
.grid-item--width2 { width: 400px; }
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
.card-columns { columns: 4;}

  // @include media-breakpoint-only(lg) {
  //   column-count: 4;
  // }
  // @include media-breakpoint-only(xl) {
  //   column-count: 5;
  // }
  // @include media-breakpoint-only(xs) {
  //   column-count: 2;
  // }
</style>
