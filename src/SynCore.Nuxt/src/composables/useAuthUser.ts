import axios from "axios";
import {User, type Claim} from "~/types";

interface SignInParams {
    email: string,
    password: string
}

let user: User | undefined = undefined
let isAuthenticated = false

export function useAuthUser() {
    async function authenticate() {
        const res = await axios.get<Claim[]>('/api/auth/me', { withCredentials: true })

        if (res.data && res.data.length > 0) {
            user = new User(res.data)
            isAuthenticated = true
            console.log(user)
        }
    }

    async function signIn(params: SignInParams) {
        const body = new URLSearchParams();
        body.append('email', params.email);
        body.append('password', params.password);
        await axios.post("/api/auth/sign-in", body)
        await authenticate()
    }

    async function signOut() {
        await axios.post("/api/auth/sign-out")

        user = undefined
        isAuthenticated = false
    }

    return {
        user,
        isAuthenticated,
        authenticate,
        signIn,
        signOut
    }
}