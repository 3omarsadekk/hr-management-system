export interface DeductionDto {
    id: number;
    name: string;
    amount: number;
    isPercentage: boolean;
}

export interface CreateDeductionDto {
    name: string;
    amount: number;
    isPercentage: boolean;
}

export interface UpdateDeductionDto {
    name: string;
    amount: number;
    isPercentage: boolean;
}
