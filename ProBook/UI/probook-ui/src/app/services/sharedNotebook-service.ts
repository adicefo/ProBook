import { Observable } from "rxjs";
import { SearchResult } from "../interfaces/searchResult";
import { SharedNotebook } from "../interfaces/sharedNotebook-interface";
import { ApiService } from "./api-service";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class SharedNotebookService{
    private endpoint='SharedNotebook';

    constructor(private apiService:ApiService){
        
    }

    getAll(params:any={}):Observable<SearchResult<SharedNotebook>>{
        return this.apiService.get<SearchResult<SharedNotebook>>(this.endpoint,params);
    }
    getById(id:number):Observable<SharedNotebook>{
        return this.apiService.getById<SharedNotebook>(this.endpoint,id);
    }
    create(sharedNotebook:any):Observable<SharedNotebook>{
        return this.apiService.post<SharedNotebook>(this.endpoint,sharedNotebook);
    }
    update(id:number,sharedNotebook:any):Observable<SharedNotebook>{
        return this.apiService.put<SharedNotebook>(this.endpoint,id,sharedNotebook);
    }
    delete(id:number):Observable<SharedNotebook>{
        return this.apiService.delete<SharedNotebook>(this.endpoint,id);
    }
}