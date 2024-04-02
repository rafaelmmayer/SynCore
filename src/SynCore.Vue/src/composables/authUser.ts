import {ref} from "vue";
import axios, { AxiosError } from "axios";
import type {Claim, ErrorResponse} from "@/models";
import {User} from "@/models";

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

    function createError(err: any) {
        if(err instanceof AxiosError) {
            if (err.response){
                if (err.response.data.error) {
                    return err.response?.data.error
                }
                else{
                    return [{
                        errorMessage: 'erro não mapeado'
                    }]
                }
            }
        }
        else {
            return [{
                errorMessage: 'erro não mapeado'
            }]
        }
    }

    async function SignIn(args: SignInArgs) : Promise<void | ErrorResponse[]> {
        const params = new URLSearchParams();
        params.append('email', args.email);
        params.append('password', args.password);

        try {
            await axios.post('/api/auth/sign-in', params)
        } catch (err) {
            return createError(err)
        }

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

    async function SignOut() : Promise<void | ErrorResponse[]> {
        try {
            await axios.post('/api/auth/sign-out')
        } catch (err) {
            return createError(err)
        }

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

        try {
            await axios.post('/api/auth/sign-up', params)
        } catch (err) {
            return createError(err)
        }
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