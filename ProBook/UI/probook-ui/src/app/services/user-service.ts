import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api-service';
import { User } from '../interfaces/user-interface';
import { SearchResult } from '../interfaces/searchResult';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private endpoint = 'User';

    constructor(private apiService: ApiService) {

    }
    getAllUsers(params:any={}): Observable<SearchResult<User>> {
        return this.apiService.get<SearchResult<User>>(this.endpoint,params);
    }
    getUserById(id: number): Observable<User> {
        return this.apiService.getById<User>(this.endpoint, id);
    }
    createUser(user: User): Observable<User> {
        return this.apiService.post<User>(this.endpoint, user);
    }
    updateUser(id: number, user: User): Observable<User> {
        return this.apiService.put<User>(this.endpoint, id, user);
    }
    deleteUser(id: number): Observable<User> {
        return this.apiService.delete<User>(this.endpoint, id);
    }
    getCurrentUser(): Observable<User> {
        return this.apiService.get<User>(this.endpoint + '/getCurrentUser');
    }
    updatePassword(id:number,body:any):Observable<User>{
        return this.apiService.post<User>(this.endpoint + '/updatePassword/'+id,body);
    }
}