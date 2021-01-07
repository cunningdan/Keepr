import { api } from './AxiosService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'

class KeepService {
  async GetAll() {
    try {
      const res = await api.get('api/keeps')
      logger.log(res.data)
      AppState.keeps = res.data
    } catch (err) {
      logger.log(err)
    }
  }

  async GetOne(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      logger.log(res.data)
      AppState.activeKeeps = res.data
    } catch (err) {
      logger.log(err)
    }
  }

  async GetByProfile(id) {
    try {
      const res = await api.get('profile/' + id + '/keeps')
      logger.log(res.data)
      AppState.keeps = res.data
    } catch (e) {
      logger.log(e)
    }
  }

  async Create(keepData) {
    try {
      const res = await api.post('api/keeps', keepData)
      logger.log(res.data)
      await this.GetByProfile(AppState.profile.id)
    } catch (e) {
      logger.log(e)
    }
  }

  async Delete(id) {
    try {
      await api.delete('api/keeps/' + id)
      await this.GetByProfile(AppState.profile.id)
    } catch (e) {
      logger.log(e)
    }
  }
}

export const keepService = new KeepService()
