<template>
  <div class="searchProfile container-fluid">
    <div class="">
      <div class="row">
        <div class="col-3">
          <img :src="profile.picture" alt="">
        </div>
        <div class="col-5">
          <h1>
            {{ profile.name }}
          </h1>
          <p>{{ profile.email }}</p>
        </div>
      </div>
      <h3 class="text-center">
        Vaults
      </h3>
      <div class="card-columns">
        <div class="" v-for="vault in vaults" :key="vault.id">
          <div class="card" v-if="vault.isPrivate != true">
            <div class="card-title text-center">
              {{ vault.name }}
            </div>
          </div>
        </div>
      </div>
      <h3 class="text-center">
        Keeps
      </h3>
      <p class="text-center">
        Keep Count: {{ keeps.length }}
      </p>
      <div class="card-columns">
        <div class="" v-for="keep in keeps" :key="keep.id">
          <div class="card-body img-fluid" :style="{ 'background-image': 'url(' + keep.img + ')'}" data-toggle="modal" :data-target="'#keep' + keep.id">
            <div class="card-title">
              {{ keep.name }}
            </div>
            <div class="card-text">
              <img class="profile-img" :src="keep.creator.picture" alt="">
            </div>
          </div>
          <div class="modal fade" :id="'keep' + keep.id" role="dialog">
            <div class="modal-dialog modal-lg">
              <div class="modal-content row">
                <img :src="keep.img" alt="" class="img-fluid col-6">
                <div class="col-6 text-info">
                  Views: {{ keep.views }}
                  Keeps: {{ keep.keeps }}
                  Shares: {{ keep.shares }}
                </div>
                <div class="modal-header">
                  <h3>
                    {{ keep.name }}
                  </h3>
                  <p>{{ keep.description }}</p>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                  <div class="profile" @click="SendToProfile">
                    {{ profile.name }}
                    <img :src="profile.picture" alt="">
                  </div>
                  <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle"
                            type="button"
                            id="dropdownMenuButton"
                            data-toggle="dropdown"
                            aria-haspopup="true"
                            aria-expanded="false"
                    >
                      Dropdown button
                    </button>
                    <div class="dropdown-menu">
                      <div class="dropdown-item" v-for="vault in userVaults" :key="vault.id" aria-labelledby="dropdownMenuButton" @click.prevent="CreateVaultKeep(vault.id, keep.id)">
                        <span class="">{{ vault.name }}</span>
                      </div>
                    </div>
                  </div>
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">
                    Close
                  </button>
                </div>
              </div>
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
import { profileService } from '../services/ProfileService'
import { useRoute } from 'vue-router'
import { vaultService } from '../services/VaultService'
import { keepService } from '../services/KeepService'
import { vaultKeepService } from '../services/VaultKeepService'
export default {
  name: 'SearchProfile',
  setup() {
    const state = reactive({
      newVaultKeep: {

      }
    })
    const route = useRoute()
    onMounted(async() => {
      profileService.getByName(route.params.query)
      vaultService.GetSearchVaults(AppState.searchProfile.id)
      await profileService.getProfile()
      await vaultService.GetByProfile(AppState.profile.id)
      await keepService.GetByProfile(AppState.searchProfile.id)
    })
    return {
      profile: computed(() => AppState.searchProfile),
      userProfile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.searchVaults),
      userVaults: computed(() => AppState.vaults),
      CreateVaultKeep(vaultId, keepId) {
        state.newVaultKeep.keepId = keepId
        state.newVaultKeep.vaultId = vaultId
        vaultKeepService.Create(state.newVaultKeep)
      },
      SendToProfile() {
        profileService.getByName(this.keep.creator.id)
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.card-columns { columns: 4;}
.profile-img {height: 30px; width: 30px;}

</style>
