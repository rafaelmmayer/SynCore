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

    return {
        getAllClasses,
        getClassesSchedule,
        addClass
    }
}