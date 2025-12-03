export enum RecurrenceType {
    OneTime = 1,
    Period = 2,
    Annual = 3,
    Permanent = 4,
}

export interface EmployeeAllowanceDto {
    employeeId: number;
    allowanceId: number;
    employeeName?: string;
    allowanceName?: string;
    amount?: number;
    isPercentage?: boolean;
    effectiveDate?: string; // Kept for backward compatibility if needed, but new fields are primary
    recurrence: RecurrenceType;
    startDate?: string;
    endDate?: string;
}

export interface CreateEmployeeAllowanceDto {
    employeeId: number;
    allowanceId: number;
    amount?: number;
    isPercentage?: boolean;
    recurrence: RecurrenceType;
    startDate?: string;
    endDate?: string;
}

export interface UpdateEmployeeAllowanceDto {
    amount?: number;
    isPercentage?: boolean;
    recurrence: RecurrenceType;
    startDate?: string;
    endDate?: string;
}
