import { AppState } from '../AppState'
import router from '../router'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile() {
    try {
      const res = await api.get('/profile')
      AppState.profile = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getByName(query) {
    try {
      const res = await api.get('api/profiles/' + query)
      logger.log(res.data)
      AppState.searchProfile = res.data
      router.push({ name: 'SearchProfile', params: { query } })
    } catch (e) {
      logger.log(e)
    }
  }
}

export const profileService = new ProfileService()
