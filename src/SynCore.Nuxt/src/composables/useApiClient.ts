import axios from "axios";
import type {Class, DayOfWeekSchedule} from "~/types";

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

    return {
        getAllClasses,
        getClassesSchedule,
        addClass,

        getAllPosts,
        addPost,
        deletePost,
        addLike
    }
}