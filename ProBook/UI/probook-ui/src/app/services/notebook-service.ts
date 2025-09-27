import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api-service';
import { User } from '../interfaces/user-interface';
import { SearchResult } from '../interfaces/searchResult';
import { Notebook } from '../interfaces/notebook-interface';

@Injectable({
    providedIn: 'root'
})
export class NotebookService {
    private endpoint = 'Notebook';

    constructor(private apiService: ApiService) {

    }
    getAll(params: any = {}): Observable<SearchResult<Notebook>> {
        return this.apiService.get<SearchResult<Notebook>>(this.endpoint, params);
    }
    getNotebookById(id: number): Observable<Notebook> {
        return this.apiService.getById<Notebook>(this.endpoint, id);
    }
    createNotebook(notebook: any): Observable<Notebook> {
        return this.apiService.post<Notebook>(this.endpoint, notebook);
    }
    updateNotebook(id: number, notebook: any): Observable<Notebook> {
        return this.apiService.put<Notebook>(this.endpoint, id, notebook);
    }
    deleteNotebook(id: number): Observable<Notebook> {
        return this.apiService.delete<Notebook>(this.endpoint, id);
    }
    getAllNotebooks(userId: number): Observable<Notebook[]> {
        return this.apiService.get<Notebook[]>(this.endpoint + '/getAllNotebooks/' +userId);
    }
}