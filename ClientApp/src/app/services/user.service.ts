import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '../models';

@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }
}
