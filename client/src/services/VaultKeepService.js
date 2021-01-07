import { AppState } from '../AppState'
import { api } from './AxiosService'
// import { profileService } from '../services/ProfileService'

class VaultKeepService {
  async Create(vaultKeepData) {
    try {
      const res = await api.post('api/vaultkeeps', vaultKeepData)
      console.log(res.data)
      AppState.vaultKeeps.push(res.data)
    } catch (e) {
      console.log(e)
    }
  }

  async GetKeeps(id) {
    try {
      // await profileService.getProfile()
      const res = await api.get('api/vaults/' + id + '/keeps')
      console.log(res.data)
      AppState.keeps = res.data
    } catch (e) {
      console.log(e)
    }
  }

  async GetOne(id) {
    try {
      const res = await api.get('api/vaultkeeps/' + id)
      console.log(res.data)
      AppState.activeVault = res.data
    } catch (e) {
      console.log(e)
    }
  }

  async Delete(id, vaultId) {
    try {
      await api.delete('api/vaultkeeps/' + id)
      this.GetKeeps(vaultId)
    } catch (e) {
      console.log(e)
    }
  }
}

export const vaultKeepService = new VaultKeepService()
