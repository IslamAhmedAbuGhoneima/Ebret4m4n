import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AddAdmin } from '../../interfaces/AddAdmin';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class GlobalService {
  constructor(protected http: HttpClient) {}


}
