import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api-service';
import { User } from '../interfaces/user-interface';
import { SearchResult } from '../interfaces/searchResult';
import { Notebook } from '../interfaces/notebook-interface';
import { Page } from '../interfaces/page-interface';
@Injectable({
    providedIn: 'root'
})
export class PageService {
    private endpoint = 'Page';

    constructor(private apiService: ApiService) {

    }
    getAll(params:any={}): Observable<SearchResult<Page>> {
        return this.apiService.get<SearchResult<Page>>(this.endpoint,params);
    }
    getById(id: number): Observable<Page> {
        return this.apiService.getById<Page>(this.endpoint, id);
    }
    create(page: any): Observable<Page> {
        return this.apiService.post<Page>(this.endpoint, page);
    }
    update(id: number, page: any): Observable<Page> {
        return this.apiService.put<Page>(this.endpoint, id, page);
    }
    delete(id: number): Observable<Page> {
        return this.apiService.delete<Page>(this.endpoint, id);
    }
    getAllPages(notebookId:number): Observable<Page[]> {
        return this.apiService.get<Page[]>(this.endpoint + '/getAllPages'+{notebookId});
    }
}