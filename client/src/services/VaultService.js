import { api } from './AxiosService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
// import { profileService } from './ProfileService'

class VaultService {
  async GetByProfile(id) {
    try {
      const res = await api.get('profile/' + id + '/vaults')
      logger.log(res.data)
      AppState.vaults = res.data
    } catch (e) {
      logger.log(e)
    }
  }

  async GetSearchVaults(id) {
    try {
      const res = await api.get('profile/' + id + '/vaults')
      logger.log(res.data)
      AppState.searchVaults = res.data
    } catch (e) {
      logger.log(e)
    }
  }

  async GetOne(id) {
    try {
      // await profileService.getProfile()
      const res = await api.get('api/vaults/' + id)
      logger.log(res.data)
      AppState.activeVault = res.data
    } catch (e) {
      logger.log(e)
    }
  }

  async Create(vaultData) {
    try {
      const res = await api.post('api/vaults', vaultData)
      logger.log(res.data)
      this.GetByProfile(AppState.profile.id)
    } catch (e) {
      logger.log(e)
    }
  }

  async Delete(id) {
    try {
      await api.delete('api/vaults/' + id)
      this.GetByProfile(AppState.profile.id)
      logger.log('deleted')
    } catch (e) {
      logger.log(e)
    }
  }
}

export const vaultService = new VaultService()
