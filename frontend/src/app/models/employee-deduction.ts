export enum RecurrenceType {
    OneTime = 1,
    Period = 2,
    Annual = 3,
    Permanent = 4,
}

export interface EmployeeDeductionDto {
    employeeId: number;
    deductionId: number;
    employeeName?: string;
    deductionName?: string;
    amount?: number;
    isPercentage?: boolean;
    effectiveDate?: string;
    recurrence: RecurrenceType;
    startDate?: string;
    endDate?: string;
}

export interface CreateEmployeeDeductionDto {
    employeeId: number;
    deductionId: number;
    amount?: number;
    isPercentage?: boolean;
    recurrence: RecurrenceType;
    startDate?: string;
    endDate?: string;
}

export interface UpdateEmployeeDeductionDto {
    amount?: number;
    isPercentage?: boolean;
    recurrence: RecurrenceType;
    startDate?: string;
    endDate?: string;
}
