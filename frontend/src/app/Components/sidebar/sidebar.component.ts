import { Component, inject, HostListener } from '@angular/core';
import { LayoutService } from '../../Services/layout.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../Services/auth.service'; // ✅ NEW

export interface MenuItem {
  title: string;
  icon?: string;
  link?: string;
  group?: boolean;
  children?: MenuItem[];
  allowedRoles?: string[]; // ✅ already here
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {
  private layoutService = inject(LayoutService);
  private router = inject(Router);
  private authService = inject(AuthService); // ✅ NEW

  isSidebarOpen = this.layoutService.isSidebarOpen;

  menuItems: MenuItem[] = [
    { title: 'Dashboard', icon: 'home-outline', link: '/pages/dashboard', allowedRoles: ['Admin', 'HR', 'Manager', 'Employee'] },

    {
      title: 'Employee Management',
      icon: 'people-outline',
      allowedRoles: ['HR'],
      children: [
        { title: 'Employee List', link: '/pages/employees', allowedRoles: ['Admin', 'HR'] },
        { title: 'Add Employee', link: '/pages/employees/create', allowedRoles: ['Admin', 'HR'] },
        { title: 'Resignation Requests', link: '/pages/employees/resignations', allowedRoles: ['Admin', 'HR', 'Manager'] },
      ],
    },
    {
      title: 'Organization',
      icon: 'briefcase-outline',
      allowedRoles: ['HR'],
      children: [
        { title: 'Departments', link: '/pages/departments', allowedRoles: ['Admin', 'HR'] },
        { title: 'Designations', link: '/pages/designations', allowedRoles: ['Admin', 'HR'] },
      ],
    },
    {
      title: 'Attendance',
      icon: 'clock-outline',
      allowedRoles: ['HR', 'Manager'],
      children: [
        { title: 'Check In', link: '/pages/attendance/check', allowedRoles: ['Admin', 'HR', 'Manager', 'Employee'] },
        { title: 'Check Out', link: '/pages/attendance/checkout', allowedRoles: ['Admin', 'HR', 'Manager', 'Employee'] },
        { title: 'Attendance Records', link: '/pages/attendance/records', allowedRoles: ['Admin', 'HR', 'Manager'] },
      ],
    },
    {
      title: 'Leave Management',
      icon: 'calendar-outline',
      allowedRoles: ['HR', 'Manager'],
      children: [
        { title: 'Leave Types', link: '/pages/leave/types', allowedRoles: ['Admin', 'HR'] },
        { title: 'Leave Requests', link: '/pages/leave/requests', allowedRoles: ['Admin', 'HR'] },
        { title: 'My Approvals', link: '/pages/leave/approvals', allowedRoles: ['Manager', 'HR'] },
        { title: 'Leave Balances', link: '/pages/leave/balances', allowedRoles: ['Admin', 'HR'] },
        { title: 'Manage Balances', link: '/pages/leave/manage-balances', allowedRoles: ['Admin', 'HR'] },
      ],
    },
    {
      title: 'Payroll',
      icon: 'credit-card-outline',
      allowedRoles: ['HR'],
      children: [
        { title: 'Payslips', link: '/pages/payroll/payslips', allowedRoles: ['Admin', 'HR'] },
        { title: 'Allowances', link: '/pages/payroll/allowances', allowedRoles: ['Admin', 'HR'] },
        { title: 'Deductions', link: '/pages/payroll/deductions', allowedRoles: ['Admin', 'HR'] },
        { title: 'Employee Allowances', link: '/pages/payroll/employee-allowances', allowedRoles: ['Admin', 'HR'] },
        { title: 'Employee Deductions', link: '/pages/payroll/employee-deductions', allowedRoles: ['Admin', 'HR'] },
      ],
    },
    {
      title: 'Performance',
      icon: 'trending-up-outline',
      allowedRoles: ['HR', 'Manager'],
      children: [
        { title: 'Review Cycles', link: '/pages/performance/cycles', allowedRoles: ['Admin', 'HR'] },
        { title: 'Reviews', link: '/pages/performance/reviews', allowedRoles: ['Admin', 'HR', 'Manager'] },
        { title: 'Goals', link: '/pages/performance/goals', allowedRoles: ['Admin', 'HR', 'Manager', 'Employee'] },
        { title: 'KPIs', link: '/pages/performance/kpis', allowedRoles: ['Admin', 'HR', 'Manager'] },
        { title: 'Competencies', link: '/pages/performance/competencies', allowedRoles: ['Admin', 'HR'] },
        { title: 'Feedback', link: '/pages/performance/feedback', allowedRoles: ['Admin', 'HR', 'Manager', 'Employee'] },
      ],
    },
    {
      title: 'Training',
      icon: 'book-open-outline',
      allowedRoles: ['HR', 'Manager'],
      children: [
        { title: 'Training Courses', link: '/pages/training/courses', allowedRoles: ['Admin', 'HR'] },
        { title: 'Employee Enrollments', link: '/pages/training/enrollments', allowedRoles: ['Admin', 'HR', 'Manager'] },
        { title: 'Training Requests', link: '/pages/training/requests', allowedRoles: ['Admin', 'HR', 'Manager'] },
      ],
    },
    {
      title: 'Recruitment',
      icon: 'person-add-outline',
      allowedRoles: ['HR'],
      children: [
        { title: 'Job Postings', link: '/pages/recruitment/jobs', allowedRoles: ['Admin', 'HR'] },
        { title: 'Candidates', link: '/pages/recruitment/candidates', allowedRoles: ['Admin', 'HR'] },
        { title: 'Job Applications', link: '/pages/recruitment/applications', allowedRoles: ['Admin', 'HR'] },
      ],
    },
    {
      title: 'Reports & Analytics',
      icon: 'bar-chart-outline',
      allowedRoles: ['HR', 'Manager'],
      children: [
        { title: 'Dashboard KPIs', link: '/pages/reports/dashboard', allowedRoles: ['Admin', 'HR', 'Manager'] },
        { title: 'Employee Reports', link: '/pages/reports/employees', allowedRoles: ['Admin', 'HR', 'Manager'] },
        { title: 'Payroll Reports', link: '/pages/reports/payroll', allowedRoles: ['Admin', 'HR'] },
        { title: 'Leave Reports', link: '/pages/reports/leave', allowedRoles: ['Admin', 'HR', 'Manager'] },
        { title: 'Recruitment Reports', link: '/pages/reports/recruitment', allowedRoles: ['Admin', 'HR'] },
      ],
    },
    { title: 'Notifications', icon: 'bell-outline', link: '/pages/notifications', allowedRoles: ['Admin', 'HR', 'Manager', 'Employee'] },
    {
      title: 'Employee Self-Service',
      icon: 'person-outline',
      allowedRoles: ['Employee', 'Manager', 'HR'],
      children: [
        { title: 'My Profile', link: '/pages/ess/profile' },
        { title: 'My Leave Requests', link: '/pages/ess/leave-requests' },
        { title: 'My Resignations', link: '/pages/ess/resignations' },
        { title: 'My Training', link: '/pages/ess/training' },
        { title: 'My Payslips', link: '/pages/ess/payslips' },
        { title: 'My Dashboard', link: '/pages/ess/dashboard' },
        { title: 'Update My Image', link: '/pages/updateEmployeeImage' },
      ],
    },
    {
      title: 'Settings',
      icon: 'settings-2-outline',
      allowedRoles: ['HR', 'Manager', 'Employee'],
      children: [{ title: 'Change Password', link: '/pages/account/change-password' }],
    },
  ];

  expandedItems = new Set<string>();
  private isMobile = false;

  constructor() {
    this.checkScreenSize();
  }

  @HostListener('window:resize')
  onResize() {
    this.checkScreenSize();
  }

  private checkScreenSize() {
    this.isMobile = window.innerWidth < 992;
  }

  onMenuItemClick(item: MenuItem, event: Event) {
    if (item.children) {
      event.preventDefault();
      this.toggleItem(item);
    } else {
      // Close sidebar on mobile when navigating
      this.closeSidebarOnMobile();
    }
  }

  toggleItem(item: MenuItem) {
    if (item.children) {
      // If sidebar is closed, open it first
      if (!this.isSidebarOpen()) {
        this.layoutService.isSidebarOpen.set(true);
      }

      if (this.expandedItems.has(item.title)) {
        this.expandedItems.delete(item.title);
      } else {
        this.expandedItems.add(item.title);
      }
    }
  }

  isExpanded(item: MenuItem): boolean {
    return this.expandedItems.has(item.title);
  }

  closeSidebarOnMobile() {
    if (this.isMobile && this.isSidebarOpen()) {
      this.layoutService.isSidebarOpen.set(false);
    }
  }

  isActiveRoute(link?: string): boolean {
    if (!link) return false;
    return this.router.url === link;
  }

  hasActiveChild(item: MenuItem): boolean {
    if (!item.children) return false;
    return item.children.some((child) => child.link && this.router.url.startsWith(child.link));
  }


  canShow(item: MenuItem): boolean {
    if (!item.allowedRoles || item.allowedRoles.length === 0) {
      return true;
    }
    return this.authService.hasAnyRole(item.allowedRoles);
  }
}
