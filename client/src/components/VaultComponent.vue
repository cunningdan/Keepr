<template>
  <div class="VaultComponent">
    <div class="card">
      <div class="card-body">
        {{ vaultProp.name }}
        <router-link :to="{ name: 'Vault', params: { id: vaultProp.id} }">
          <button class="btn btn-dark">
            Enter Vault
          </button>
        </router-link>
        <button class="btn btn-danger" @click.prevent="Delete" v-if="$route.path == '/profile'">
          Delete
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { vaultService } from '../services/VaultService'
import swal from 'sweetalert'
export default {
  name: 'VaultComponent',
  props: ['vaultProp'],
  setup(props) {
    return {
      Delete() {
        if (props.vaultProp.isPrivate === true) {
          swal({
            title: 'Are you sure you want to delete?',
            text: 'this will be unrecoverable',
            icon: 'warning',
            buttons: true,
            dangerMode: true
          }).then((willDelete) => {
            if (willDelete) {
              vaultService.Delete(props.vaultProp.id)
              swal('Vault has been deleted', {
                icon: 'success'
              })
            } else {
              swal('Vault is still intact')
            }
          }
          )
        } else {
          swal('You are not allowed to delete public vaults')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
