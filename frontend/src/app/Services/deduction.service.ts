import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { DeductionDto, CreateDeductionDto, UpdateDeductionDto } from '../models/deduction';

@Injectable({
    providedIn: 'root',
})
export class DeductionService {
    private http = inject(HttpClient);
    private apiUrl = 'http://localhost:5093/api/Deduction';

    getAll(): Observable<ApiResponse<DeductionDto[]>> {
        return this.http.get<ApiResponse<DeductionDto[]>>(this.apiUrl);
    }

    getById(id: number): Observable<ApiResponse<DeductionDto>> {
        return this.http.get<ApiResponse<DeductionDto>>(`${this.apiUrl}/${id}`);
    }

    create(deduction: CreateDeductionDto): Observable<ApiResponse<DeductionDto>> {
        return this.http.post<ApiResponse<DeductionDto>>(this.apiUrl, deduction);
    }

    update(id: number, deduction: UpdateDeductionDto): Observable<ApiResponse<void>> {
        return this.http.put<ApiResponse<void>>(`${this.apiUrl}/${id}`, deduction);
    }

    delete(id: number): Observable<ApiResponse<void>> {
        return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${id}`);
    }
}
