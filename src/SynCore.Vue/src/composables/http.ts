import {ref} from "vue";
import type {AxiosRequestConfig} from "axios";
import axios from "axios";
import type { ErrorResponse } from "@/models";

export function useApiClient<T>(){
    const isSuccess = ref(false)
    const isError = ref(false)

    const data = ref<T>()
    const error = ref<ErrorResponse[]>()

    const isLoading = ref(false)

    function execute(
        config: AxiosRequestConfig<any>){

        isLoading.value = true

        axios.request<T | ErrorResponse[]>(config)
            .then(res => {
                data.value = res.data as T

                isSuccess.value = true
                isError.value = false
            })
            .catch(err => {
                error.value = err.response.data.error as ErrorResponse[]

                isError.value = true
                isSuccess.value = false
            })
            .finally(() => {
                isLoading.value = false
            })
    }

    function resetErrors() {
        isError.value = false
        error.value = []
    }

    return {
        data,
        error,
        isSuccess,
        isError,
        isLoading,
        execute,
        resetErrors
    }
}