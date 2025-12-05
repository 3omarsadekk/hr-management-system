import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutService } from '../../Services/layout.service';
import { AuthService } from '../../Services/auth.service';
import { NotificationService } from '../../Services/notification.service';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { Notification } from '../../models/notification';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  private layoutService = inject(LayoutService);
  private authService = inject(AuthService);
  private router = inject(Router);
  notificationService = inject(NotificationService);

  private pollingSubscription?: Subscription;
  isNotificationDropdownOpen = false;

  // Expose signals to template
  unreadCount = this.notificationService.unreadCount;
  notifications = this.notificationService.notifications;
  isLoading = this.notificationService.isLoading;

  // Reactive username
  userName: string = 'User';

  ngOnInit(): void {
    // Subscribe to username changes
    this.authService.userName$.subscribe((name) => {
      this.userName = name || 'User';
    });

    // Only load notifications if user is authenticated
    if (this.authService.getToken()) {
      // Load notifications on init
      this.notificationService.loadNotifications();

      // Start polling for new notifications every 30 seconds
      this.pollingSubscription = this.notificationService.startPolling(30000).subscribe();
    }
  }

  ngOnDestroy(): void {
    this.pollingSubscription?.unsubscribe();
  }

  toggleSidebar() {
    this.layoutService.toggleSidebar();
  }

  goTocheckOut() {
    this.router.navigate(['/pages/checkOut']);
  }

  goToCheckIn() {
    this.router.navigate(['/pages/checkIn']);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/pages/login']);
  }

  toggleNotificationDropdown(event: Event): void {
    event.stopPropagation();

    // Only allow if authenticated
    if (!this.authService.getToken()) {
      this.router.navigate(['/pages/login']);
      return;
    }

    this.isNotificationDropdownOpen = !this.isNotificationDropdownOpen;

    if (this.isNotificationDropdownOpen) {
      // Refresh notifications when opening dropdown
      this.notificationService.loadNotifications();
    }
  }

  closeNotificationDropdown(): void {
    this.isNotificationDropdownOpen = false;
  }

  markAsRead(notification: Notification, event: Event): void {
    event.stopPropagation();
    if (!notification.isRead) {
      this.notificationService.markAsRead(notification.id).subscribe();
    }

    // Navigate to action URL if present
    if (notification.actionUrl) {
      this.router.navigateByUrl(notification.actionUrl);
      this.closeNotificationDropdown();
    }
  }

  markAllAsRead(event: Event): void {
    event.stopPropagation();
    this.notificationService.markAllAsRead().subscribe();
  }

  deleteNotification(notification: Notification, event: Event): void {
    event.stopPropagation();
    this.notificationService.deleteNotification(notification.id).subscribe();
  }

  viewAllNotifications(): void {
    this.closeNotificationDropdown();
    this.router.navigate(['/pages/notifications']);
  }

  getNotificationIcon(type: string): string {
    return this.notificationService.getNotificationIcon(type);
  }

  getNotificationColorClass(type: string): string {
    return this.notificationService.getNotificationColorClass(type);
  }

  getTimeAgo(date: Date | string): string {
    return this.notificationService.getTimeAgo(date);
  }

  // Get recent notifications (limit to 5 for dropdown)
  get recentNotifications(): Notification[] {
    return this.notifications().slice(0, 5);
  }
}
