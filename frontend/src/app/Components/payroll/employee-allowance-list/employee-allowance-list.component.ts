import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeAllowanceService } from '../../../Services/employee-allowance.service';
import { EmployeeService } from '../../../Services/employee';
import { AllowanceService } from '../../../Services/allowance.service';
import {
    EmployeeAllowanceDto,
    CreateEmployeeAllowanceDto,
    UpdateEmployeeAllowanceDto,
    RecurrenceType,
} from '../../../models/employee-allowance';
import { Employee } from '../../../models/employee';
import { AllowanceDto } from '../../../models/allowance';
import { ToastService } from '../../../Services/toast.service';

@Component({
    selector: 'app-employee-allowance-list',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './employee-allowance-list.component.html',
    styleUrls: ['./employee-allowance-list.component.css'],
})
export class EmployeeAllowanceListComponent implements OnInit {
    private service = inject(EmployeeAllowanceService);
    private employeeService = inject(EmployeeService);
    private allowanceService = inject(AllowanceService);
    private toastService = inject(ToastService);

    RecurrenceType = RecurrenceType; // Expose enum to template

    employeeAllowances: EmployeeAllowanceDto[] = [];
    employees: Employee[] = [];
    allowances: AllowanceDto[] = [];
    isLoading = true;
    error: string | null = null;

    // Modal state
    showModal = false;
    showDeleteModal = false;
    showDetailsModal = false;
    isEditing = false;
    isSaving = false;

    // Form data
    createFormData: CreateEmployeeAllowanceDto = {
        employeeId: 0,
        allowanceId: 0,
        recurrence: RecurrenceType.OneTime,
        startDate: new Date().toISOString().split('T')[0],
    };
    updateFormData: UpdateEmployeeAllowanceDto = {
        recurrence: RecurrenceType.OneTime,
        startDate: new Date().toISOString().split('T')[0],
    };

    selectedItem: EmployeeAllowanceDto | null = null;
    itemToDelete: EmployeeAllowanceDto | null = null;
    itemToView: EmployeeAllowanceDto | null = null;

    // Helper for amount input type (fixed/percentage)
    amountType: 'default' | 'fixed' | 'percentage' = 'default';
    searchTerm: string = '';

    get filteredEmployeeAllowances() {
        if (!this.searchTerm) return this.employeeAllowances;
        const term = this.searchTerm.toLowerCase();
        return this.employeeAllowances.filter(item =>
            (item.employeeName || this.getEmployeeName(item.employeeId)).toLowerCase().includes(term) ||
            (item.allowanceName || this.getAllowanceName(item.allowanceId)).toLowerCase().includes(term)
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
                    this.employeeAllowances = res.data;
                } else {
                    this.error = res.errorMessage;
                }
                this.isLoading = false;
            },
            error: () => {
                this.error = 'Failed to load employee allowances';
                this.isLoading = false;
            },
        });

        this.employeeService.getEmployees().subscribe((res) => {
            if (!res.hasError && res.data) {
                this.employees = res.data;
            }
        });

        this.allowanceService.getAll().subscribe((res) => {
            if (!res.hasError && res.data) {
                this.allowances = res.data;
            }
        });
    }

    openCreateModal() {
        this.isEditing = false;
        this.amountType = 'default';
        this.createFormData = {
            employeeId: 0,
            allowanceId: 0,
            recurrence: RecurrenceType.OneTime,
            startDate: new Date().toISOString().split('T')[0],
            amount: undefined,
            isPercentage: undefined
        };
        this.showModal = true;
    }

    openEditModal(item: EmployeeAllowanceDto) {
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
                    this.selectedItem.allowanceId,
                    this.updateFormData
                )
                .subscribe({
                    next: (res) => {
                        if (res && !res.hasError) {
                            this.toastService.success('Employee allowance updated successfully');
                            this.closeModal();
                            this.loadData();
                        } else if (res && res.hasError) {
                            this.toastService.error(res.errorMessage || 'Failed to update');
                        } else {
                            // Handle null response (API might return 204 No Content)
                            this.toastService.success('Employee allowance updated successfully');
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
            if (!this.createFormData.employeeId || !this.createFormData.allowanceId) {
                this.toastService.error('Employee and Allowance are required');
                this.isSaving = false;
                return;
            }
            this.service.create(this.createFormData).subscribe({
                next: (res) => {
                    if (!res.hasError) {
                        this.toastService.success('Employee allowance assigned successfully');
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

    openDeleteModal(item: EmployeeAllowanceDto) {
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
            .delete(this.itemToDelete.employeeId, this.itemToDelete.allowanceId)
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

    getAllowanceName(id: number): string {
        const all = this.allowances.find((a) => a.id === id);
        return all ? all.name : 'Unknown';
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

    getAllowanceDisplay(allowance: AllowanceDto): string {
        if (allowance.isPercentage) {
            return `${allowance.name} (${allowance.amount}%)`;
        }
        return `${allowance.name} ($${allowance.amount})`;
    }

    openDetailsModal(item: EmployeeAllowanceDto) {
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
