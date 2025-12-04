import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { PayslipDto } from '../models/payslip';

@Injectable({
    providedIn: 'root'
})
export class PayslipService {
    private http = inject(HttpClient);
    private apiUrl = 'http://localhost:5093/api/payslips';

    generateMonthly(month: number, year: number): Observable<ApiResponse<PayslipDto[]>> {
        return this.http.post<ApiResponse<PayslipDto[]>>(`${this.apiUrl}/generate-month?month=${month}&year=${year}`, {});
    }

    getMonthly(month: number, year: number): Observable<ApiResponse<PayslipDto[]>> {
        return this.http.get<ApiResponse<PayslipDto[]>>(`${this.apiUrl}/month?month=${month}&year=${year}`);
    }

    downloadPdf(id: number): Observable<Blob> {
        return this.http.get(`${this.apiUrl}/${id}/export/pdf`, { responseType: 'blob' });
    }

    regenerate(employeeId: number, month: number, year: number): Observable<ApiResponse<PayslipDto>> {
        return this.http.post<ApiResponse<PayslipDto>>(`${this.apiUrl}/${employeeId}/regenerate?month=${month}&year=${year}`, {});
    }
}
