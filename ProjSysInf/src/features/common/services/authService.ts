import { RegisterRequest } from '../types/RegisterRequest';
import { post } from './httpClient';

export const login = async (email: string, password: string): Promise<{ data: any, status: number }> => {
    const response = await post<any>(`/login`, { email, password });
    return { data: response.data, status: response.status };
}

export const register = async (data: RegisterRequest): Promise<{ data: any, status: number }> => {
    const response = await post<any>(`/register`, data);
    return { data: response.data, status: response.status };
}

export const changePassword = async (email: string, oldPassword: string, newPassword: string): Promise<{ data: any, status: number }> => {
    const response = await post<any>(`/changePassword`, { email, oldPassword, newPassword });
    return { data: response.data, status: response.status };
}

export const passwordReminder = async (email: string): Promise<{ data: any, status: number }> => {
    const response = await post<any>(`/passwordReminder`, { email });
    return { data: response.data, status: response.status };
}

export const logout = async (): Promise<{ data: any, status: number }> => {
    const response = await post<any>('/logout');
    return { data: response.data, status: response.status };
}
