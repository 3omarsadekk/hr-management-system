import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeDeductionService } from '../../../Services/employee-deduction.service';
import { EmployeeService } from '../../../Services/employee';
import { DeductionService } from '../../../Services/deduction.service';
import {
    EmployeeDeductionDto,
    CreateEmployeeDeductionDto,
    UpdateEmployeeDeductionDto,
    RecurrenceType,
} from '../../../models/employee-deduction';
import { Employee } from '../../../models/employee';
import { DeductionDto } from '../../../models/deduction';
import { ToastService } from '../../../Services/toast.service';

@Component({
    selector: 'app-employee-deduction-list',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './employee-deduction-list.component.html',
    styleUrls: ['./employee-deduction-list.component.css'],
})
export class EmployeeDeductionListComponent implements OnInit {
    private service = inject(EmployeeDeductionService);
    private employeeService = inject(EmployeeService);
    private deductionService = inject(DeductionService);
    private toastService = inject(ToastService);

    RecurrenceType = RecurrenceType; // Expose enum to template

    employeeDeductions: EmployeeDeductionDto[] = [];
    employees: Employee[] = [];
    deductions: DeductionDto[] = [];
    isLoading = true;
    error: string | null = null;

    // Modal state
    showModal = false;
    showDeleteModal = false;
    showDetailsModal = false;
    isEditing = false;
    isSaving = false;

    // Form data
    createFormData: CreateEmployeeDeductionDto = {
        employeeId: 0,
        deductionId: 0,
        recurrence: RecurrenceType.OneTime,
        startDate: new Date().toISOString().split('T')[0],
    };
    updateFormData: UpdateEmployeeDeductionDto = {
        recurrence: RecurrenceType.OneTime,
        startDate: new Date().toISOString().split('T')[0],
    };

    selectedItem: EmployeeDeductionDto | null = null;
    itemToDelete: EmployeeDeductionDto | null = null;
    itemToView: EmployeeDeductionDto | null = null;

    // Helper for amount input type (fixed/percentage)
    amountType: 'default' | 'fixed' | 'percentage' = 'default';
    searchTerm: string = '';

    get filteredEmployeeDeductions() {
        if (!this.searchTerm) return this.employeeDeductions;
        const term = this.searchTerm.toLowerCase();
        return this.employeeDeductions.filter(item =>
            (item.employeeName || this.getEmployeeName(item.employeeId)).toLowerCase().includes(term) ||
            (item.deductionName || this.getDeductionName(item.deductionId)).toLowerCase().includes(term)
        );
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;
        this.service.getAll().subscribe({
            next: (res) => {
                if (!res.hasError && res.data) {
                    this.employeeDeductions = res.data;
                } else {
                    this.error = res.errorMessage;
                }
                this.isLoading = false;
            },
            error: () => {
                this.error = 'Failed to load employee deductions';
                this.isLoading = false;
            },
        });

        this.employeeService.getEmployees().subscribe((res) => {
            if (!res.hasError && res.data) {
                this.employees = res.data;
            }
        });

        this.deductionService.getAll().subscribe((res) => {
            if (!res.hasError && res.data) {
                this.deductions = res.data;
            }
        });
    }

    openCreateModal() {
        this.isEditing = false;
        this.amountType = 'default';
        this.createFormData = {
            employeeId: 0,
            deductionId: 0,
            recurrence: RecurrenceType.OneTime,
            startDate: new Date().toISOString().split('T')[0],
            amount: undefined,
            isPercentage: undefined
        };
        this.showModal = true;
    }

    openEditModal(item: EmployeeDeductionDto) {
        this.isEditing = true;
        this.selectedItem = item;

        if (item.amount !== undefined && item.amount !== null) {
            this.amountType = item.isPercentage ? 'percentage' : 'fixed';
        } else {
            this.amountType = 'default';
        }

        // Convert string recurrence to enum value for proper selection
        let recurrenceValue: RecurrenceType;
        if (typeof item.recurrence === 'string') {
            recurrenceValue = RecurrenceType[item.recurrence as keyof typeof RecurrenceType];
        } else {
            recurrenceValue = item.recurrence;
        }

        this.updateFormData = {
            recurrence: recurrenceValue,
            startDate: item.startDate ? item.startDate.split('T')[0] : undefined,
            endDate: item.endDate ? item.endDate.split('T')[0] : undefined,
            amount: item.amount,
            isPercentage: item.isPercentage,
        };
        this.showModal = true;
    }

    closeModal() {
        this.showModal = false;
        this.selectedItem = null;
    }

    onRecurrenceChange() {
        // Reset end date only for OneTime and Permanent (not Annual)
        const formData = this.isEditing ? this.updateFormData : this.createFormData;
        if (formData.recurrence === RecurrenceType.OneTime || formData.recurrence === RecurrenceType.Permanent || formData.recurrence === RecurrenceType.Annual) {
            formData.endDate = undefined;
        }
    }

    onAmountTypeChange() {
        const formData = this.isEditing ? this.updateFormData : this.createFormData;
        if (this.amountType === 'default') {
            formData.amount = undefined;
            formData.isPercentage = undefined;
        } else if (this.amountType === 'fixed') {
            formData.isPercentage = false;
            if (!formData.amount) formData.amount = 0;
        } else if (this.amountType === 'percentage') {
            formData.isPercentage = true;
            if (!formData.amount) formData.amount = 0;
        }
    }

    save() {
        const formData = this.isEditing ? this.updateFormData : this.createFormData;

        // Validation
        if (formData.recurrence === RecurrenceType.Period && !formData.endDate) {
            this.toastService.error('End Date is required for Period recurrence');
            return;
        }

        if (this.amountType !== 'default' && (formData.amount === undefined || formData.amount === null)) {
            this.toastService.error('Amount is required when not using default');
            return;
        }

        this.isSaving = true;
        if (this.isEditing && this.selectedItem) {
            this.service
                .update(
                    this.selectedItem.employeeId,
                    this.selectedItem.deductionId,
                    this.updateFormData
                )
                .subscribe({
                    next: (res) => {
                        if (res && !res.hasError) {
                            this.toastService.success('Employee deduction updated successfully');
                            this.closeModal();
                            this.loadData();
                        } else if (res && res.hasError) {
                            this.toastService.error(res.errorMessage || 'Failed to update');
                        } else {
                            // Handle null response (API might return 204 No Content)
                            this.toastService.success('Employee deduction updated successfully');
                            this.closeModal();
                            this.loadData();
                        }
                        this.isSaving = false;
                    },
                    error: () => {
                        this.toastService.error('An error occurred');
                        this.isSaving = false;
                    },
                });
        } else {
            if (!this.createFormData.employeeId || !this.createFormData.deductionId) {
                this.toastService.error('Employee and Deduction are required');
                this.isSaving = false;
                return;
            }
            this.service.create(this.createFormData).subscribe({
                next: (res) => {
                    if (!res.hasError) {
                        this.toastService.success('Employee deduction assigned successfully');
                        this.closeModal();
                        this.loadData();
                    } else {
                        this.toastService.error(res.errorMessage || 'Failed to assign');
                    }
                    this.isSaving = false;
                },
                error: () => {
                    this.toastService.error('An error occurred');
                    this.isSaving = false;
                },
            });
        }
    }

    openDeleteModal(item: EmployeeDeductionDto) {
        this.itemToDelete = item;
        this.showDeleteModal = true;
    }

    closeDeleteModal() {
        this.showDeleteModal = false;
        this.itemToDelete = null;
    }

    confirmDelete() {
        if (!this.itemToDelete) return;

        this.isSaving = true;
        this.service
            .delete(this.itemToDelete.employeeId, this.itemToDelete.deductionId)
            .subscribe({
                next: (res) => {
                    if (res && !res.hasError) {
                        this.toastService.success('Removed successfully');
                        this.closeDeleteModal();
                        this.loadData();
                    } else if (res && res.hasError) {
                        this.toastService.error(res.errorMessage || 'Failed to remove');
                    } else {
                        // Handle null response (API might return 204 No Content)
                        this.toastService.success('Removed successfully');
                        this.closeDeleteModal();
                        this.loadData();
                    }
                    this.isSaving = false;
                },
                error: () => {
                    this.toastService.error('An error occurred');
                    this.isSaving = false;
                },
            });
    }

    getEmployeeName(id: number): string {
        const emp = this.employees.find((e) => e.id === id);
        return emp ? `${emp.firstName} ${emp.lastName}` : 'Unknown';
    }

    getDeductionName(id: number): string {
        const ded = this.deductions.find((d) => d.id === id);
        return ded ? ded.name : 'Unknown';
    }

    formatDate(dateString?: string): string {
        if (!dateString) return 'N/A';
        return new Date(dateString).toLocaleDateString();
    }

    getRecurrenceLabel(type: RecurrenceType | number | string): string {
        // Handle string enum values from API
        if (typeof type === 'string') {
            switch (type) {
                case 'OneTime': return 'One Time';
                case 'Period': return 'Period';
                case 'Annual': return 'Annual';
                case 'Permanent': return 'Permanent';
                default: return 'Unknown';
            }
        }

        // Handle numeric enum values
        const typeNum = typeof type === 'number' ? type : Number(type);
        switch (typeNum) {
            case 1:
            case RecurrenceType.OneTime: return 'One Time';
            case 2:
            case RecurrenceType.Period: return 'Period';
            case 3:
            case RecurrenceType.Annual: return 'Annual';
            case 4:
            case RecurrenceType.Permanent: return 'Permanent';
            default: return 'Unknown';
        }
    }

    getDeductionDisplay(deduction: DeductionDto): string {
        if (deduction.isPercentage) {
            return `${deduction.name} (${deduction.amount}%)`;
        }
        return `${deduction.name} ($${deduction.amount})`;
    }

    openDetailsModal(item: EmployeeDeductionDto) {
        this.itemToView = item;
        this.showDetailsModal = true;
    }

    closeDetailsModal() {
        this.showDetailsModal = false;
        this.itemToView = null;
    }

    formatCurrency(amount: number | undefined | null): string {
        if (amount === undefined || amount === null) return '$0.00';
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD',
        }).format(amount);
    }
}
