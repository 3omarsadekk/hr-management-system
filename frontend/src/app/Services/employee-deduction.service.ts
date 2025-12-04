import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import {
    EmployeeDeductionDto,
    CreateEmployeeDeductionDto,
    UpdateEmployeeDeductionDto,
} from '../models/employee-deduction';

@Injectable({
    providedIn: 'root',
})
export class EmployeeDeductionService {
    private http = inject(HttpClient);
    private apiUrl = 'http://localhost:5093/api/EmployeeDeduction';

    getAll(): Observable<ApiResponse<EmployeeDeductionDto[]>> {
        return this.http.get<ApiResponse<EmployeeDeductionDto[]>>(this.apiUrl);
    }

    getByCompositeKey(
        employeeId: number,
        deductionId: number
    ): Observable<ApiResponse<EmployeeDeductionDto>> {
        return this.http.get<ApiResponse<EmployeeDeductionDto>>(
            `${this.apiUrl}/${employeeId}/${deductionId}`
        );
    }

    create(
        employeeDeduction: CreateEmployeeDeductionDto
    ): Observable<ApiResponse<EmployeeDeductionDto>> {
        return this.http.post<ApiResponse<EmployeeDeductionDto>>(this.apiUrl, employeeDeduction);
    }

    update(
        employeeId: number,
        deductionId: number,
        employeeDeduction: UpdateEmployeeDeductionDto
    ): Observable<ApiResponse<void>> {
        return this.http.put<ApiResponse<void>>(
            `${this.apiUrl}/${employeeId}/${deductionId}`,
            employeeDeduction
        );
    }

    delete(employeeId: number, deductionId: number): Observable<ApiResponse<void>> {
        return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${employeeId}/${deductionId}`);
    }

    getByEmployee(employeeId: number): Observable<ApiResponse<EmployeeDeductionDto[]>> {
        return this.http.get<ApiResponse<EmployeeDeductionDto[]>>(
            `${this.apiUrl}/employee/${employeeId}`
        );
    }
}
