export interface AllowanceDto {
    id: number;
    name: string;
    amount: number;
    isPercentage: boolean;
}

export interface CreateAllowanceDto {
    name: string;
    amount: number;
    isPercentage: boolean;
}

export interface UpdateAllowanceDto {
    name: string;
    amount: number;
    isPercentage: boolean;
}
