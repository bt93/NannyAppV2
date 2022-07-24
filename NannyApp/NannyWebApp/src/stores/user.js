import { defineStore, acceptHMRUpdate } from 'pinia'

export const useUserStore = defineStore({
    id: 'userToken',
    state: () => ({
        token: '',
        refreshToken: ''
    }),
    actions: {
        // Logs out user
        logout() {
            this.$patch({
                token: '',
                refreshToken: ''
            })
        },

        // logs in user
        login(token, refreshToken) {
            this.$patch({
                token,
                refreshToken
            })
        } 
    }
})

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useUserStore, import.meta.hot))
}