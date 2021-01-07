<template>
  <div class="keepComponent">
    <h6 class="card-body" @click.prevent="GetOne">
      <div class="card-title">
        {{ keepProp.name }}
      </div>
      <img class="img-fluid rcorners" :src="keepProp.img" alt="" data-toggle="modal" :data-target="'#keep' + keepProp.id">
      <!-- <div class="card card-body" data-toggle="modal" :data-target="'#keep' + keepProp.id" :style="{'background-image': 'url('+ keepProp.img + ')'}"> -->
      <!-- :style="{ 'background-image': 'url(' + keepProp.img + ')'}" -->
      <div class="card-text">
        <img :src="keepProp.creator.picture" alt="" class="profile-img" @click.stop="SendToProfile">
      </div>
      <button @click.stop="Delete" class="btn btn-danger" v-if="$route.path == '/profile'">
        Delete
      </button>
      <div class="modal fade" :id="'keep' + keepProp.id" role="dialog">
        <div class="modal-dialog modal-lg">
          <div class="modal-content row">
            <img :src="keepProp.img" alt="" class="img-fluid col-6">
            <div class="col-6 text-info">
              Views: {{ keepProp.views }}
              Keeps: {{ keepProp.keeps }}
              Shares: {{ keepProp.shares }}
            </div>
            <div class="modal-header">
              <h3>
                {{ keepProp.name }}
              </h3>
              <p>{{ keepProp.description }}</p>
            </div>
            <div class="modal-body">
            </div>
            <div class="modal-footer">
              <div class="profile" @click="SendToProfile">
                {{ keepProp.creator.name }}
                <img :src="keepProp.creator.picture" alt="">
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
                  <div class="dropdown-item" v-for="vault in vaults" :key="vault.id" aria-labelledby="dropdownMenuButton" @click.prevent="CreateVaultKeep(vault.id)">
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
    </h6>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { keepService } from '../services/KeepService'
import { vaultKeepService } from '../services/VaultKeepService'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'
import { profileService } from '../services/ProfileService'
import swal from 'sweetalert'
export default {
  name: 'KeepComponent',
  props: ['keepProp'],
  setup(props) {
    const state = reactive({
      newVaultKeep: {}
    })
    return {
      state,
      Delete() {
        if (props.keepProp.creator.id) {
          swal({
            text: 'Are you sure you want to delete this Keep',
            icon: 'warning',
            buttons: true,
            dangerMode: true
          }).then((willDelete) => {
            if (willDelete) {
              keepService.Delete(props.keepProp.id)
              keepService.GetByProfile(AppState.profile.id)
              swal('Keep has been deleted', {
                icon: 'success'
              })
            } else {
              swal('Keep has been preserved')
            }
          })
        }
      },
      GetOne() {
        keepService.GetOne(props.keepProp.id)
      },
      GetVaultsByProfile() {
        vaultService.GetByProfile(AppState.profile.id)
      },
      CreateVaultKeep(vaultId) {
        state.newVaultKeep.vaultId = vaultId
        state.newVaultKeep.keepId = props.keepProp.id
        vaultKeepService.Create(state.newVaultKeep)
      },
      SendToProfile() {
        profileService.getByName(props.keepProp.creator.name)
      },
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile)
    }
  },
  components: { }
}
// data-masonry="{'columnwidth': 350, 'itemselector': '.grid-item'}"
</script>

<style lang="scss" >
.modal-backdrop {
  z-index: -1000;
}
.card-body {page-break-inside: avoid;}
.profile {align-self: flex-start !important; }
.model-lg {max-width: 100%; max-height: 100%;}
.keeps {height: 50vh;}
.card {height: 20vh;}
.profile-img {height: 30px; width: 30px;}
.rcorners {
  border-radius: 25px;
  padding: 20px;
}
</style>
