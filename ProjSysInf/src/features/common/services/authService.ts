import { get, post } from './httpClient';

export const login = async (username: string, password: string): Promise<any> => {
    const response = await post<any>(`/login`, {username, password})
return response.data; 
}

export const register = async (username: string, password: string): Promise<any> => {
    const response = await post<any>(`/register`, {username, password})
return response.data; 
}

export const reminder = async (username: string): Promise<any> => {
    const response = await post<any>(`/reminder`, username)
return response.data; 
}

export const logout = async (): Promise<any> => {
    const response = await get<any>('/logout');
    return response.data;
}
