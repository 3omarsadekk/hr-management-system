export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  contactNumber?: string;
  dateOfBirth?: string;
  gender?: string;
  hireDate?: string;
  eFF_Start?: string;
  eFF_End?: string;
  deptId: number;
  designationId: number;
  basicSalary: number;
  address?: string;
  applicationUserId?: string;
  faceEmbedding?: number[];
  // These might need to be fetched separately or joined in backend if needed for display
  departmentName?: string;
  designationName?: string;
  status?: string;
  avatar?: string;
}

export interface CreateEmployeeDto {
  firstName: string;
  lastName: string;
  deptId: number;
  designationId: number;
  dateOfBirth: string;
  gender?: string;
  hireDate: string;
  eFF_Start?: string;
  eFF_End?: string;
  email: string;
  contactNumber?: string;
  address?: string;
  basicSalary: number;
  applicationUserId?: string;
}

export interface UpdateEmployeeDto {
  firstName: string;
  lastName: string;
  deptId: number;
  designationId: number;
  dateOfBirth: string;
  gender?: string;
  hireDate: string;
  eFF_Start?: string;
  eFF_End?: string;
  email: string;
  contactNumber?: string;
  address?: string;
  basicSalary: number;
  applicationUserId?: string;
}

export interface RegisterEmployeeDto {
  email: string;
  password: string;
  confirmPassword: string;
  phoneNumber?: string;
  firstName: string;
  lastName: string;
  dateOfBirth?: string;
  gender?: string;
  hireDate: string;
  eFF_Start?: string;
  eFF_End?: string;
  contactNumber?: string;
  address?: string;
  basicSalary: number;
  deptId: number;
  designationId: number;
  roles?: string[];
}

export interface RegisterEmployeeResponseDto {
  userId: string;
  employeeId: number;
  email: string;
  fullName: string;
  roles: string[];
  token?: string;
  tokenExpiration?: string;
}

export interface EmployeeSummary {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  position?: string;
}
