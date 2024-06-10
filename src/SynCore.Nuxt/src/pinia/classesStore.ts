import {defineStore} from "pinia";
import type {Class, DayOfWeekSchedule} from "~/types";

export const useClassesStore = defineStore('classes', () => {
    const api = useApiClient()

    const classes = ref<Class[]>([])
    const classesSchedule = ref<DayOfWeekSchedule[]>([])

    async function loadClasses() {
        try {
            const res = await api.getAllClasses()
            classes.value = res.data
        } catch (error) {
            console.log(error)
        }
    }

    async function loadClassesSchedule() {
        try {
            const res = await api.getClassesSchedule()
            classesSchedule.value = res.data
        } catch (error) {
            console.log(error)
        }
    }

    async function addClass(c: Class) {
        await api.addClass(c)
        classes.value.push(c)
        await loadClassesSchedule()
    }

    function removeClass(c: Class) {
        classes.value.splice(classes.value.indexOf(c), 1)
    }

    return {
        classes,
        classesSchedule,

        loadClasses,
        loadClassesSchedule,
        addClass,
        removeClass
    }
})