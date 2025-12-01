import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../../Services/notification.service';
import {
  Notification,
  NotificationCategory,
  NotificationPriority,
} from '../../models/notification';

@Component({
  selector: 'app-notification-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css'],
})
export class NotificationListComponent implements OnInit {
  private router = inject(Router);
  notificationService = inject(NotificationService);

  // Filter state
  filterStatus = signal<'all' | 'unread' | 'read'>('all');
  filterCategory = signal<string>('all');
  filterPriority = signal<string>('all');
  searchQuery = signal<string>('');

  // Sort state
  sortBy = signal<'date' | 'priority'>('date');
  sortOrder = signal<'asc' | 'desc'>('desc');

  // Loading and error states
  isLoading = this.notificationService.isLoading;
  notifications = this.notificationService.notifications;
  unreadCount = this.notificationService.unreadCount;

  // Categories and priorities for filters
  categories = Object.values(NotificationCategory);
  priorities = Object.values(NotificationPriority);

  // Filtered and sorted notifications
  filteredNotifications = computed(() => {
    let result = [...this.notifications()];

    // Filter by status
    if (this.filterStatus() === 'unread') {
      result = result.filter((n) => !n.isRead);
    } else if (this.filterStatus() === 'read') {
      result = result.filter((n) => n.isRead);
    }

    // Filter by category
    if (this.filterCategory() !== 'all') {
      result = result.filter((n) => n.category === this.filterCategory());
    }

    // Filter by priority
    if (this.filterPriority() !== 'all') {
      result = result.filter((n) => n.priority === this.filterPriority());
    }

    // Filter by search query
    const query = this.searchQuery().toLowerCase().trim();
    if (query) {
      result = result.filter(
        (n) => n.title.toLowerCase().includes(query) || n.message.toLowerCase().includes(query)
      );
    }

    // Sort
    result.sort((a, b) => {
      if (this.sortBy() === 'date') {
        const dateA = new Date(a.createdAt).getTime();
        const dateB = new Date(b.createdAt).getTime();
        return this.sortOrder() === 'desc' ? dateB - dateA : dateA - dateB;
      } else {
        const priorityOrder = { Urgent: 4, High: 3, Normal: 2, Low: 1 };
        const priorityA = priorityOrder[a.priority as keyof typeof priorityOrder] || 0;
        const priorityB = priorityOrder[b.priority as keyof typeof priorityOrder] || 0;
        return this.sortOrder() === 'desc' ? priorityB - priorityA : priorityA - priorityB;
      }
    });

    return result;
  });

  ngOnInit(): void {
    this.notificationService.loadNotifications();
  }

  // Actions
  markAsRead(notification: Notification): void {
    if (!notification.isRead) {
      this.notificationService.markAsRead(notification.id).subscribe();
    }
    if (notification.actionUrl) {
      this.router.navigateByUrl(notification.actionUrl);
    }
  }

  markAllAsRead(): void {
    this.notificationService.markAllAsRead().subscribe();
  }

  deleteNotification(notification: Notification, event: Event): void {
    event.stopPropagation();
    this.notificationService.deleteNotification(notification.id).subscribe();
  }

  refreshNotifications(): void {
    this.notificationService.loadNotifications();
  }

  // Filter handlers
  setFilterStatus(status: 'all' | 'unread' | 'read'): void {
    this.filterStatus.set(status);
  }

  setFilterCategory(category: string): void {
    this.filterCategory.set(category);
  }

  setFilterPriority(priority: string): void {
    this.filterPriority.set(priority);
  }

  setSearchQuery(query: string): void {
    this.searchQuery.set(query);
  }

  // Sort handlers
  setSortBy(sortBy: 'date' | 'priority'): void {
    if (this.sortBy() === sortBy) {
      // Toggle order if same sort field
      this.sortOrder.set(this.sortOrder() === 'desc' ? 'asc' : 'desc');
    } else {
      this.sortBy.set(sortBy);
      this.sortOrder.set('desc');
    }
  }

  clearFilters(): void {
    this.filterStatus.set('all');
    this.filterCategory.set('all');
    this.filterPriority.set('all');
    this.searchQuery.set('');
  }

  // Helpers
  getNotificationIcon(type: string): string {
    return this.notificationService.getNotificationIcon(type);
  }

  getNotificationColorClass(type: string): string {
    return this.notificationService.getNotificationColorClass(type);
  }

  getPriorityBadgeClass(priority: string): string {
    return this.notificationService.getPriorityBadgeClass(priority);
  }

  getTimeAgo(date: Date | string): string {
    return this.notificationService.getTimeAgo(date);
  }

  formatDate(date: Date | string): string {
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  getCategoryDisplayName(category: string): string {
    // Convert camelCase to Title Case with spaces
    return category.replace(/([A-Z])/g, ' $1').trim();
  }
}
