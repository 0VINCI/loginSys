import axios, { AxiosResponse } from "axios";

export const api = {
    address: 'https://localhost:7132/auth'
};

const instance = axios.create({
    baseURL: api.address,
    headers: {
        'Accept': "*/*",
        "Content-Type": "application/json",
    },
    withCredentials: true 
});

export const get = async function <T>(request: string): Promise<AxiosResponse<T>> {
    return await instance.get<T>(request);
};

export const post = async function <T>(request: string, params: any = null): Promise<AxiosResponse<T>> {
    return await instance.post<T>(request, params);
};
