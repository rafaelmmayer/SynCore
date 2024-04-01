import {ref} from "vue";
import type {AxiosRequestConfig} from "axios";
import axios from "axios";

interface Error {
    errorMessage: string,
}

export function useApiClient<T>(){
    const isSuccess = ref(false)
    const isError = ref(false)

    const data = ref<T>()
    const error = ref<Error[]>()

    const isLoading = ref(false)

    function execute(
        config: AxiosRequestConfig<any>){

        isLoading.value = true

        axios.request<T | Error[]>(config)
            .then(res => {
                data.value = res.data as T

                isSuccess.value = true
                isError.value = false
            })
            .catch(err => {
                error.value = err.response.data as Error[]

                isError.value = true
                isSuccess.value = false
            })
            .finally(() => {
                isLoading.value = false
            })
    }

    return {
        data,
        error,
        isSuccess,
        isError,
        execute
    }
}