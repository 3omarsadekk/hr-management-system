import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { AllowanceDto, CreateAllowanceDto, UpdateAllowanceDto } from '../models/allowance';

@Injectable({
    providedIn: 'root',
})
export class AllowanceService {
    private http = inject(HttpClient);
    private apiUrl = 'http://localhost:5093/api/Allowance';

    getAll(): Observable<ApiResponse<AllowanceDto[]>> {
        return this.http.get<ApiResponse<AllowanceDto[]>>(this.apiUrl);
    }

    getById(id: number): Observable<ApiResponse<AllowanceDto>> {
        return this.http.get<ApiResponse<AllowanceDto>>(`${this.apiUrl}/${id}`);
    }

    create(allowance: CreateAllowanceDto): Observable<ApiResponse<AllowanceDto>> {
        return this.http.post<ApiResponse<AllowanceDto>>(this.apiUrl, allowance);
    }

    update(id: number, allowance: UpdateAllowanceDto): Observable<ApiResponse<void>> {
        return this.http.put<ApiResponse<void>>(`${this.apiUrl}/${id}`, allowance);
    }

    delete(id: number): Observable<ApiResponse<void>> {
        return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${id}`);
    }
}
