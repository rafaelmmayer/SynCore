export default defineNuxtPlugin(async (nuxtApp) => {
    const {authenticate} = useAuthUser()
    await authenticate()
})