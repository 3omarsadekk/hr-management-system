import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap , BehaviorSubject} from 'rxjs';
import { ApiResponse } from '../models/api-response';

interface LoginResponseData {
  userId: string;
  email: string;
  fullName: string;
  employeeId: number;
  roles: string[];
  token: string;
  tokenExpiration: string;
}


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);


  private apiUrl = 'https://localhost:7005/api/Account';
  private TOKEN_KEY = 'auth_token';
  private ROLES_KEY = 'auth_roles';
  private USER_ID_KEY = 'auth_user_id';
  private EMP_ID_KEY = 'auth_employee_id';
  // BehaviorSubject for reactive username
  private userNameSubject = new BehaviorSubject<string | null>(
    localStorage.getItem('userName')
  );
  public userName$ = this.userNameSubject.asObservable();

  constructor(private http: HttpClient) { }


  // ========= API Calls =========

  login(payload: {
    email: string;
    password: string;
    rememberMe: boolean;
  }): Observable<ApiResponse<LoginResponseData>> {
    return this.http
      .post<ApiResponse<LoginResponseData>>(`${this.apiUrl}/login`, payload)
      .pipe(
        tap((res) => {
          if (!res.hasError && res.data) {
            this.saveAuthData(res.data);
          }
        })
      );
  }

  // Register new user/employee
  register(payload: any): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.apiUrl}/register`, payload);
  }

  // ========= Save / Clear Auth =========

  private saveAuthData(data: LoginResponseData) {
    this.saveToken(data.token);
    this.saveRoles(data.roles);
    this.saveUserId(data.userId);
    this.saveEmployeeId(data.employeeId);

  changePassword(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/change-password`, data);
  }

  saveToken(token: string) {
    localStorage.setItem('jwtToken', token);
  }

  saveToken(token: string) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  saveUserName(name: string) {
    localStorage.setItem('userName', name);
    // Emit the new username to all subscribers
    this.userNameSubject.next(name);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  saveRoles(roles: string[]) {
    localStorage.setItem(this.ROLES_KEY, JSON.stringify(roles || []));
  }

  getRoles(): string[] {
    const raw = localStorage.getItem(this.ROLES_KEY);
    if (!raw) return [];
    try {
      return JSON.parse(raw) as string[];
    } catch {
      return [];
    }
  }

  saveUserId(userId: string) {
    localStorage.setItem(this.USER_ID_KEY, userId);
  }

  getUserId(): string | null {
    return localStorage.getItem(this.USER_ID_KEY);
  }

  saveEmployeeId(empId: number) {
    localStorage.setItem(this.EMP_ID_KEY, empId.toString());
  }

  getEmployeeId(): number | null {
    const raw = localStorage.getItem(this.EMP_ID_KEY);
    return raw ? Number(raw) : null;
  }

  // ========= Helpers: isLoggedIn / roles =========

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  hasRole(role: string): boolean {
    const roles = this.getRoles();
    return roles.map((r) => r.toLowerCase()).includes(role.toLowerCase());
  }

  hasAnyRole(requiredRoles: string[] | undefined | null): boolean {
    if (!requiredRoles || requiredRoles.length === 0) return true;
    const userRoles = this.getRoles().map((r) => r.toLowerCase());
    return requiredRoles.some((r) => userRoles.includes(r.toLowerCase()));
  }

  getUserName(): string | null {
    return localStorage.getItem('userName');
  }

  logout() {

    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.ROLES_KEY);
    localStorage.removeItem(this.USER_ID_KEY);
    localStorage.removeItem(this.EMP_ID_KEY);
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    // Clear the username in the subject
    this.userNameSubject.next(null);
    this.router.navigate(['/pages/login']);
  }
}
