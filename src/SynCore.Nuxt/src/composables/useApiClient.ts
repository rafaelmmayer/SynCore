import axios from "axios";
import type {Class, DayOfWeekSchedule} from "~/types";

export function useApiClient() {
    async function getAllClasses() {
        return await axios.get<Class[]>('/api/classes')
    }

    async function getClassesSchedule() {
        return await axios.get<DayOfWeekSchedule[]>('/api/classes/schedule')
    }

    return {
        getAllClasses,
        getClassesSchedule
    }
}