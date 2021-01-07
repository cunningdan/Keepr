<template>
  <div class="vaultKeepComponent col-12 text-center">
    <h1 class="">
      Welcome to Vault {{ vault.name }} <br>
      {{ vault.description }} <br>
      Keeps: {{ vaultKeeps.length }}
    </h1>
    <div class="card-columns">
      <span class="" v-for="vaultKeep in vaultKeeps" :key="vaultKeep.id">
        <div class="card-body">
          <img class="img-fluid rcorners" :src="vaultKeep.img" alt="">
          <!-- <div class="card" :style="{ 'background-image': 'url(' + vaultKeep.img + ')'}"> -->
          <div class="card-title" data-toggle="modal" :data-target="'vaultKeep' + vaultKeep.keepId">
            {{ vaultKeep.name }}
          </div>
          <div class="card-text">
            <p>{{ vaultKeep.description }}</p>
            <img :src="vaultKeep.creator.picture" alt="">
          </div>
          <button class="btn btn-danger" @click.prevent="Delete(vaultKeep)">
            Delete
          </button>
          <!-- </div> -->
        </div>
        <div class="modal fade" :id="'vaultKeep' + vaultKeep.keepId" role="dialog">
          <div class="modal-dialog modal-lg">
            <div class="modal-content row">
              <img :src="vaultKeep.img" alt="" class="img-fluid col-6">
              <div class="col-6 text-info">
                Views: {{ vaultKeep.views }}
                Keeps: {{ vaultKeep.keeps }}
                Shares: {{ vaultKeep.shares }}
              </div>
              <div class="modal-header">
                <h3>
                  {{ vaultKeep.name }}
                </h3>
                <p>{{ vaultKeep.description }}</p>
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
                          @click.prevent="GetVaultsByProfile"
                  >
                    Dropdown button
                  </button>
                  <div class="dropdown-menu">
                    <div class="dropdown-item" v-for="vaults in userVaults" :key="vaults.id" aria-labelledby="dropdownMenuButton" @click.prevent="CreateVaultKeep(vault.id)">
                      <span class="">{{ vaults.name }}</span>
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
      </span>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { vaultKeepService } from '../services/VaultKeepService'
import { useRoute } from 'vue-router'
import swal from 'sweetalert'
import { vaultService } from '../services/VaultService'
// import { vaultService } from '../services/VaultService'
export default {
  name: 'VaultKeepComponent',
  setup() {
    const route = useRoute()
    onMounted(() => {
      vaultKeepService.GetKeeps(route.params.id)
      vaultService.GetSearchVaults(AppState.profile.id)
    })
    return {
      vault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.keeps),
      userVaults: computed(() => AppState.searchVaults),
      profile: computed(() => AppState.profile),
      Delete(vaultKeep) {
        if (vaultKeep) {
          swal({
            title: 'Are you sure you want to Remove?',
            icon: 'warning',
            buttons: true,
            dangerMode: true
          }).then((willDelete) => {
            if (willDelete) {
              vaultKeepService.Delete(vaultKeep.vaultKeepId, route.params.id)
              swal('Keep has been removed', {
                icon: 'success'
              })
            } else {
              swal('Keep is still in Vault')
            }
          })
        }
      },
      GetVaultsByProfile() {
        vaultService.GetSearchVaults(AppState.profile.id)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.img-fluid {background-size: cover;}
.rcorners {
  border-radius: 25px;
  padding: 20px;
}
.card-columns { columns: 4;
}
</style>
