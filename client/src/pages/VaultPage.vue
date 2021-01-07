<template>
  <div class="vault container-fluid">
    <vault-keep-component />
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import VaultKeepComponent from '../components/VaultKeepComponent.vue'
import { vaultService } from '../services/VaultService'
import { useRoute } from 'vue-router'
// import { vaultKeepService } from '../services/VaultKeepService'

export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    onMounted(() => {
      vaultService.GetOne(route.params.id)
      // vaultKeepService.GetKeeps(route.params.id)
    })
    return {
      vault: computed(() => AppState.activeVault)
    }
  },
  components: { VaultKeepComponent }
}
</script>

<style lang="scss" scoped>

</style>
