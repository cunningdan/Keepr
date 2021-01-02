<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <h3>
    </h3>
    <p>
    </p>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  setup(props) {
    const state = reactive({
      newJob: {}
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
