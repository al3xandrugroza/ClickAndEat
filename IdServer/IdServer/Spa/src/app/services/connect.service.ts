import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginRequest} from "../dtos/login.request";
import {Observable} from "rxjs";
import {LoginResponse} from "../dtos/login.response";

@Injectable({
  providedIn: 'root'
})
export class ConnectService {
  private connectUrl = '/connect';

  constructor(private http: HttpClient) {}

  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.connectUrl}/login`, loginRequest);
  }
}
