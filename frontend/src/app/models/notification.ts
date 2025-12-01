export interface Notification {
  id: number;
  recipientUserId?: string;
  title: string;
  message: string;
  type: NotificationType;
  category: NotificationCategory;
  isRead: boolean;
  readAt?: Date;
  actionUrl?: string;
  relatedEntityId?: number;
  relatedEntityType?: string;
  priority: NotificationPriority;
  createdAt: Date;
  updatedAt: Date;
}

export enum NotificationType {
  Info = 'Info',
  Success = 'Success',
  Warning = 'Warning',
  Error = 'Error',
}

export enum NotificationCategory {
  LeaveRequest = 'LeaveRequest',
  LeaveApproval = 'LeaveApproval',
  JobApplication = 'JobApplication',
  EmployeeRegistration = 'EmployeeRegistration',
  Interview = 'Interview',
  Payroll = 'Payroll',
  System = 'System',
}

export enum NotificationPriority {
  Low = 'Low',
  Normal = 'Normal',
  High = 'High',
  Urgent = 'Urgent',
}

export interface CreateNotificationDto {
  recipientUserId?: string;
  title: string;
  message: string;
  type?: NotificationType;
  category: NotificationCategory;
  actionUrl?: string;
  relatedEntityId?: number;
  relatedEntityType?: string;
  priority?: NotificationPriority;
}
