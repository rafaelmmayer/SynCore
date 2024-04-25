export default defineNuxtRouteMiddleware((to, from) => {
    const authUser = useAuthUser()

    if (!authUser.isAuthenticated) {

        let route = '/auth/signin'

        if (from.path !== '/' && from.path !== '/posts') {
            route += `?redirectTo=${btoa(from.fullPath)}`
        }

        return navigateTo(route);
    }

    if (to.path === '/'){
        return navigateTo('/posts')
    }
})