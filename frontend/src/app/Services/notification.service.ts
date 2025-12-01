import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, interval, switchMap, startWith } from 'rxjs';
import { Notification, CreateNotificationDto } from '../models/notification';
import { ApiResponse } from '../models/api-response';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Notification';

  // Reactive state for notifications
  private _notifications = signal<Notification[]>([]);
  private _unreadCount = signal<number>(0);
  private _isLoading = signal<boolean>(false);

  // Public readonly signals
  notifications = this._notifications.asReadonly();
  unreadCount = this._unreadCount.asReadonly();
  isLoading = this._isLoading.asReadonly();

  // Computed for unread notifications
  unreadNotifications = computed(() => this._notifications().filter((n) => !n.isRead));

  // Get all notifications for current user
  getMyNotifications(): Observable<ApiResponse<Notification[]>> {
    return this.http.get<ApiResponse<Notification[]>>(`${this.apiUrl}/my`).pipe(
      tap((response) => {
        if (!response.hasError && response.data) {
          this._notifications.set(response.data);
        }
      })
    );
  }

  // Get unread notifications for current user
  getMyUnreadNotifications(): Observable<ApiResponse<Notification[]>> {
    return this.http.get<ApiResponse<Notification[]>>(`${this.apiUrl}/my/unread`);
  }

  // Get unread count for current user
  getMyUnreadCount(): Observable<ApiResponse<number>> {
    return this.http.get<ApiResponse<number>>(`${this.apiUrl}/my/unread-count`).pipe(
      tap((response) => {
        if (!response.hasError && response.data !== null) {
          this._unreadCount.set(response.data);
        }
      })
    );
  }

  // Get notification by ID
  getNotificationById(id: number): Observable<ApiResponse<Notification>> {
    return this.http.get<ApiResponse<Notification>>(`${this.apiUrl}/${id}`);
  }

  // Create a new notification
  createNotification(notification: CreateNotificationDto): Observable<ApiResponse<Notification>> {
    return this.http.post<ApiResponse<Notification>>(this.apiUrl, notification);
  }

  // Mark notification as read
  markAsRead(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/read`, {}).pipe(
      tap(() => {
        // Update local state
        this._notifications.update((notifications) =>
          notifications.map((n) => (n.id === id ? { ...n, isRead: true, readAt: new Date() } : n))
        );
        this._unreadCount.update((count) => Math.max(0, count - 1));
      })
    );
  }

  // Mark all notifications as read
  markAllAsRead(): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/my/read-all`, {}).pipe(
      tap(() => {
        // Update local state
        this._notifications.update((notifications) =>
          notifications.map((n) => ({ ...n, isRead: true, readAt: new Date() }))
        );
        this._unreadCount.set(0);
      })
    );
  }

  // Delete notification
  deleteNotification(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const notification = this._notifications().find((n) => n.id === id);
        this._notifications.update((notifications) => notifications.filter((n) => n.id !== id));
        if (notification && !notification.isRead) {
          this._unreadCount.update((count) => Math.max(0, count - 1));
        }
      })
    );
  }

  // Load notifications (for initial load)
  loadNotifications(): void {
    this._isLoading.set(true);
    this.getMyNotifications().subscribe({
      next: () => this._isLoading.set(false),
      error: () => this._isLoading.set(false),
    });
    this.getMyUnreadCount().subscribe();
  }

  // Start polling for notifications (optional - for real-time updates)
  startPolling(intervalMs: number = 30000): Observable<ApiResponse<number>> {
    return interval(intervalMs).pipe(
      startWith(0),
      switchMap(() => this.getMyUnreadCount())
    );
  }

  // Get notification icon based on type
  getNotificationIcon(type: string): string {
    switch (type) {
      case 'Success':
        return 'checkmark-circle-2-outline';
      case 'Warning':
        return 'alert-triangle-outline';
      case 'Error':
        return 'close-circle-outline';
      case 'Info':
      default:
        return 'info-outline';
    }
  }

  // Get notification color class based on type
  getNotificationColorClass(type: string): string {
    switch (type) {
      case 'Success':
        return 'text-success';
      case 'Warning':
        return 'text-warning';
      case 'Error':
        return 'text-danger';
      case 'Info':
      default:
        return 'text-info';
    }
  }

  // Get priority badge class
  getPriorityBadgeClass(priority: string): string {
    switch (priority) {
      case 'Urgent':
        return 'bg-danger';
      case 'High':
        return 'bg-warning';
      case 'Normal':
        return 'bg-primary';
      case 'Low':
      default:
        return 'bg-secondary';
    }
  }

  // Format time ago
  getTimeAgo(date: Date | string): string {
    const now = new Date();
    const notificationDate = new Date(date);
    const diffInSeconds = Math.floor((now.getTime() - notificationDate.getTime()) / 1000);

    if (diffInSeconds < 60) {
      return 'Just now';
    } else if (diffInSeconds < 3600) {
      const minutes = Math.floor(diffInSeconds / 60);
      return `${minutes} minute${minutes > 1 ? 's' : ''} ago`;
    } else if (diffInSeconds < 86400) {
      const hours = Math.floor(diffInSeconds / 3600);
      return `${hours} hour${hours > 1 ? 's' : ''} ago`;
    } else if (diffInSeconds < 604800) {
      const days = Math.floor(diffInSeconds / 86400);
      return `${days} day${days > 1 ? 's' : ''} ago`;
    } else {
      return notificationDate.toLocaleDateString();
    }
  }
}
