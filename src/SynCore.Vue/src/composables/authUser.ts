import {ref} from "vue";
import axios from "axios";
import type {Claim} from "@/models/User";
import {User} from "@/models/User";

interface SignUpArgs {
    name: string,
    lastName: string,
    email: string,
    cpf: string,
    collegeName: string,
    password: string
}

interface SignInArgs {
    email: string,
    password: string
}

const user = ref<User | null>(null)
const isAuthenticate = ref(false)

export function useAuthUser(){

    async function SignIn(args: SignInArgs){
        const params = new URLSearchParams();
        params.append('email', args.email);
        params.append('password', args.password);
        await axios.post('/api/auth/sign-in', params)

        const me = await Me()
        user.value = new User(me)

        isAuthenticate.value = true
    }

    async function Authenticate() {
        const me = await Me()

        if (me?.length <= 0) {
            return
        }

        user.value = new User(me)
        isAuthenticate.value = true
    }

    async function SignOut() {
        await axios.post('/api/auth/sign-out')
        user.value = null
        isAuthenticate.value = false
    }

    async function SignUp(args: SignUpArgs) {
        const params = new URLSearchParams();
        params.append('name', args.name);
        params.append('lastName', args.lastName);
        params.append('email', args.email);
        params.append('cpf', args.cpf);
        params.append('collegeName', args.collegeName);
        params.append('password', args.password);
        await axios.post('/api/auth/sign-up', params)
    }

    async function Me() {
        const res = await axios.get<Claim[]>('/api/auth/me')
        return res.data
    }

    return {
        user,
        isAuthenticate,
        SignIn,
        Authenticate,
        SignOut,
        SignUp
    }
}