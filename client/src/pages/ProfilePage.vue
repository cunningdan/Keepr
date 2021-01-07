<template>
  <div class="container">
    <div class="row">
      <div class="about text-center">
        <h1>Welcome {{ profile.name }}</h1>
        <img class="rounded" :src="profile.picture" alt="" />
        <p>{{ profile.email }}</p>
        <div class="">
          Vaults: {{ vaults.length }}
          Keeps: {{ keeps.length }}
        </div>
      </div>
    </div>
    <div class="">
      <div class="card-columns">
        <vault-component v-for="vault in vaults" :key="vault" :vault-prop="vault" />
      </div>
      <div class="card-columns">
        <keep-component v-for="keep in keeps" :key="keep" :keep-prop="keep" />
      </div>
    </div>
    <div class="row ">
      <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#createVaultModal">
        Create New Vault
      </button>
      <div id="createVaultModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">
                Vault Creation
              </h4>
            </div>
            <div class="modal-body">
              <form @submit.prevent="createVault">
                <input type="text" v-model="state.newVault.name" placeholder="Name">
                <input type="text" v-model="state.newVault.description" placeholder="Description">
                <div class="btn-group" data-toggle="buttons">
                  <label class="btn btn-primary active">
                    <input type="checkbox" v-model="state.newVault.IsPrivate" checked autocomplete="on"> IsPrivate
                  </label>
                </div>
                <button type="submit">
                  Submit
                </button>
              </form>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-default" data-dismiss="modal">
                Close
              </button>
            </div>
          </div>
        </div>
      </div>
      <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#createKeepModal">
        Create New Keep
      </button>
      <div id="createKeepModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">
                Keep Creation
              </h4>
            </div>
            <div class="modal-body">
              <form @submit.prevent="createKeep">
                <label for="keepName">Name</label>
                <input type="text" id="keepName" v-model="state.newKeep.name" placeholder="Name"> <br />
                <input type="text" v-model="state.newKeep.description" placeholder="Description"><br />
                <input type="text" v-model="state.newKeep.img" placeholder="Img URL">
                <div class="btn-group" data-toggle="buttons">
                </div>
                <button type="submit">
                  Submit
                </button>
              </form>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-default" data-dismiss="modal">
                Close
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'
import { keepService } from '../services/KeepService'
import VaultComponent from '../components/VaultComponent'
import KeepComponent from '../components/KeepComponent'
import { profileService } from '../services/ProfileService'
export default {
  name: 'Profile',
  setup() {
    const state = reactive({
      newVault: {
        IsPrivate: true
      },
      newKeep: {
      }
    })
    onMounted(async() => {
      await profileService.getProfile()
      await vaultService.GetByProfile(AppState.profile.id)
      await keepService.GetByProfile(AppState.profile.id)
    })
    return {
      state,
      createVault() {
        vaultService.Create(state.newVault)
      },
      createKeep() {
        keepService.Create(state.newKeep)
      },
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { VaultComponent, KeepComponent }
}
</script>

<style scoped>
/* .h {height: 30vh;} */
img {
  max-width: 100px;
}
</style>
