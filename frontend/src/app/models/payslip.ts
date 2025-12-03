export interface PayslipDto {
    id: number;
    employeeId: number;
    employeeName: string;
    basicSalary: number;
    totalAllowances: number;
    totalDeductions: number;
    netSalary: number;
    month: number;
    year: number;
    generatedDate: string;
    generatedAt?: string; // Alternative field name
    status?: string;
    allowances?: PayslipItemDto[];
    deductions?: PayslipItemDto[];
}

export interface PayslipItemDto {
    id: number;
    name: string;
    amount: number;
    isPercentage: boolean;
}

export interface GeneratePayslipRequest {
    month: number;
    year: number;
}
