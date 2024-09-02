import axios from 'axios';
import { RegisterRequest } from '../types/RegisterRequest';
import { post } from './httpClient';

export const login = async (email: string, password: string): Promise<{ data: any, status: number }> => {
    try {
        const response = await post<any>(`/login`, { email, password });
        return { data: response.data, status: response.status };
    } catch (error) {
        return handleError(error);
    }
};

export const register = async (data: RegisterRequest): Promise<{ data: any, status: number }> => {
    try {
        const response = await post<any>(`/register`, data);
        return { data: response.data, status: response.status };
    } catch (error) {
        return handleError(error);
    }
};

export const changePassword = async (email: string, oldPassword: string, newPassword: string): Promise<{ data: any, status: number }> => {
    try {
        const response = await post<any>(`/changePassword`, { email, oldPassword, newPassword });
        return { data: response.data, status: response.status };
    } catch (error) {
        return handleError(error);
    }
};

export const passwordReminder = async (email: string): Promise<{ data: any, status: number }> => {
    try {
        const response = await post<any>(`/passwordReminder`, { email });
        return { data: response.data, status: response.status };
    } catch (error) {
        return handleError(error);
    }
};

export const logout = async (userId: string | null): Promise<{ data: any, status: number }> => {
    try {
        const response = await post<any>('/logout', { userId });
        return { data: response.data, status: response.status };
    } catch (error) {
        return handleError(error);
    }
};

function handleError(error: any): { data: any, status: number } {
    if (axios.isAxiosError(error) && error.response) {
        return { data: error.response.data, status: error.response.status };
    }
    return { data: { error: "Wystąpił nieoczekiwany błąd!" }, status: 500 };
}
