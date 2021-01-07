import { initialize } from '@bcwdev/auth0provider-client'
// import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { audience, clientId, domain } from '../AuthConfig'
import router from '../router'
import { setBearer } from './AxiosService'
import { profileService } from './ProfileService'
// import { vaultService } from './VaultService'

export const AuthService = initialize({
  domain,
  clientId,
  audience,
  onRedirectCallback: appState => {
    router.push(
      appState && appState.targetUrl
        ? appState.targetUrl
        : window.location.pathname
    )
  }
})

AuthService.on(AuthService.AUTH_EVENTS.AUTHENTICATED, async function() {
  setBearer(AuthService.bearer)
  await profileService.getProfile()
  AppState.user = AuthService.user
  // console.log(router)
  // if (router.currentRoute === 'vault/:id') {
  // vaultService.GetOne(useRouter.)
  //   console.log('here')
  // }
  // NOTE if there is something you want to do once the user is authenticated, place that here
})
