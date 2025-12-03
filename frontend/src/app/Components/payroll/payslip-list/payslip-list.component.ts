import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PayslipService } from '../../../Services/payslip.service';
import { PayslipDto } from '../../../models/payslip';
import { ToastService } from '../../../Services/toast.service';

@Component({
  selector: 'app-payslip-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './payslip-list.component.html',
  styleUrls: ['./payslip-list.component.css'],
})
export class PayslipListComponent implements OnInit {
  private service = inject(PayslipService);
  private toastService = inject(ToastService);

  payslips: PayslipDto[] = [];
  isLoading = false;
  isGenerating = false;
  error: string | null = null;

  // Filter/Generate state
  selectedMonth: number;
  selectedYear: number;
  searchTerm: string = '';

  get filteredPayslips() {
    if (!this.searchTerm) return this.payslips;
    return this.payslips.filter(p =>
      p.employeeName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  months = [
    { value: 1, name: 'January' },
    { value: 2, name: 'February' },
    { value: 3, name: 'March' },
    { value: 4, name: 'April' },
    { value: 5, name: 'May' },
    { value: 6, name: 'June' },
    { value: 7, name: 'July' },
    { value: 8, name: 'August' },
    { value: 9, name: 'September' },
    { value: 10, name: 'October' },
    { value: 11, name: 'November' },
    { value: 12, name: 'December' },
  ];
  years: number[] = [];

  // Modal state
  showGenerateModal = false;
  showDetailsModal = false;
  selectedPayslip: PayslipDto | null = null;

  constructor() {
    const today = new Date();
    this.selectedMonth = today.getMonth() + 1;
    this.selectedYear = today.getFullYear();

    // Populate years (current year - 5 to current year + 1)
    for (let i = this.selectedYear - 5; i <= this.selectedYear + 1; i++) {
      this.years.push(i);
    }
  }

  ngOnInit() {
    this.loadPayslips();
  }

  loadPayslips() {
    this.isLoading = true;
    this.service.getMonthly(this.selectedMonth, this.selectedYear).subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.payslips = res.data;
        } else {
          this.error = res.errorMessage;
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'Failed to load payslips';
        this.isLoading = false;
      },
    });
  }

  openGenerateModal() {
    this.showGenerateModal = true;
  }

  closeGenerateModal() {
    this.showGenerateModal = false;
  }

  generatePayslips() {
    this.isGenerating = true;
    this.service.generateMonthly(this.selectedMonth, this.selectedYear).subscribe({
      next: (res) => {
        if (!res.hasError) {
          this.toastService.success('Payslips generated successfully');
          this.payslips = res.data || [];
          this.closeGenerateModal();
        } else {
          this.toastService.error(res.errorMessage || 'Failed to generate payslips');
        }
        this.isGenerating = false;
      },
      error: () => {
        this.toastService.error('An error occurred during generation');
        this.isGenerating = false;
      },
    });
  }

  openDetailsModal(payslip: PayslipDto) {
    this.selectedPayslip = payslip;
    this.showDetailsModal = true;
  }

  closeDetailsModal() {
    this.showDetailsModal = false;
    this.selectedPayslip = null;
  }

  downloadPdf(id: number) {
    this.service.downloadPdf(id).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = `Payslip_${id}.pdf`;
        link.click();
        window.URL.revokeObjectURL(url);
      },
      error: () => {
        this.toastService.error('Failed to download PDF');
      }
    });
  }

  regeneratePayslip(payslip: PayslipDto) {
    if (confirm(`Are you sure you want to regenerate the payslip for ${payslip.employeeName}?`)) {
      this.service.regenerate(payslip.employeeId, payslip.month, payslip.year).subscribe({
        next: (res) => {
          if (!res.hasError && res.data) {
            this.toastService.success('Payslip regenerated successfully');
            this.loadPayslips(); // Reload the list
          } else {
            this.toastService.error(res.errorMessage || 'Failed to regenerate payslip');
          }
        },
        error: () => {
          this.toastService.error('An error occurred while regenerating payslip');
        }
      });
    }
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
    }).format(amount);
  }
}
