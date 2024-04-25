import axios from "axios";
import type {Class} from "~/types";

export function useApiClient() {
    async function getAllClasses() {
        return await axios.get<Class[]>('/api/classes', { withCredentials: true })
    }

    return {
        getAllClasses,
    }
}