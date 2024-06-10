import axios from "axios";
import type {ChatMessage, Class, DayOfWeekSchedule, SecondaryUser} from "~/types";
import {v4 as uuidv4} from 'uuid';

const users: SecondaryUser[] = [
    {
        id: uuidv4(),
        name: "Willian Mayrink"
    },
    {
        id: uuidv4(),
        name: "Luan Bregunce"
    },
    {
        id: uuidv4(),
        name: "João Valente"
    },
    {
        id: uuidv4(),
        name: "Victor Emanuel"
    },
    {
        id: uuidv4(),
        name: "Bruno Alecio"
    }
]

const message: ChatMessage[] = []

function getUser(name: string) {
    return users.find((user) => user.name === name);
}

export function useApiClient() {
    async function getAllClasses() {
        return await axios.get<Class[]>('/api/classes')
    }

    async function getClassesSchedule() {
        return await axios.get<DayOfWeekSchedule[]>('/api/classes/schedule')
    }

    async function addClass(c: Class) {
        return await axios.post('/api/classes', c)
    }

    async function getAllPosts() {
        return await axios.get('/api/posts')
    }

    async function addPost(p: { content: string }) {
        return await axios.post('/api/posts', p)
    }

    async function deletePost(id: string) {
        return {}
    }

    async function addLike(id: string)  {
        return {
            id: '',
        }
    }

    async function getAllUsers() {
        return users
    }

    async function getChatMessages(toId: string)
    {
        const authUser = useAuthUser()

        return [...message, ...[{
                id: uuidv4(),
                to: {
                    id: authUser.user!.id!,
                    name: authUser.user!.name!
                },
                owner: {
                    id: getUser('João Valente')!.id,
                    name: getUser('João Valente')!.name,
                },
                text: 'Olá. tudo bem com você?'
            }]]
            .map(m => {
                console.log(m)
                return m
            })
            .filter(m => {
                const owner = m.owner.id == authUser.user!.id || m.owner.id == toId
                const to = m.to.id == authUser.user!.id || m.to.id == toId

                return owner || to
            })
    }

    async function addChatMessage(m: ChatMessage) {
        message.push(m)
    }

    return {
        getAllClasses,
        getClassesSchedule,
        addClass,

        getAllPosts,
        addPost,
        deletePost,
        addLike,

        getAllUsers,

        getChatMessages,
        addChatMessage
    }
}