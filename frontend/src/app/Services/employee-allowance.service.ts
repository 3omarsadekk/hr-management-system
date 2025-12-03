import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import {
    EmployeeAllowanceDto,
    CreateEmployeeAllowanceDto,
    UpdateEmployeeAllowanceDto,
} from '../models/employee-allowance';

@Injectable({
    providedIn: 'root',
})
export class EmployeeAllowanceService {
    private http = inject(HttpClient);
    private apiUrl = 'http://localhost:5093/api/EmployeeAllowance';

    getAll(): Observable<ApiResponse<EmployeeAllowanceDto[]>> {
        return this.http.get<ApiResponse<EmployeeAllowanceDto[]>>(this.apiUrl);
    }

    getByCompositeKey(
        employeeId: number,
        allowanceId: number
    ): Observable<ApiResponse<EmployeeAllowanceDto>> {
        return this.http.get<ApiResponse<EmployeeAllowanceDto>>(
            `${this.apiUrl}/${employeeId}/${allowanceId}`
        );
    }

    create(
        employeeAllowance: CreateEmployeeAllowanceDto
    ): Observable<ApiResponse<EmployeeAllowanceDto>> {
        return this.http.post<ApiResponse<EmployeeAllowanceDto>>(this.apiUrl, employeeAllowance);
    }

    update(
        employeeId: number,
        allowanceId: number,
        employeeAllowance: UpdateEmployeeAllowanceDto
    ): Observable<ApiResponse<void>> {
        return this.http.put<ApiResponse<void>>(
            `${this.apiUrl}/${employeeId}/${allowanceId}`,
            employeeAllowance
        );
    }

    delete(employeeId: number, allowanceId: number): Observable<ApiResponse<void>> {
        return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${employeeId}/${allowanceId}`);
    }

    getByEmployee(employeeId: number): Observable<ApiResponse<EmployeeAllowanceDto[]>> {
        return this.http.get<ApiResponse<EmployeeAllowanceDto[]>>(
            `${this.apiUrl}/employee/${employeeId}`
        );
    }
}
