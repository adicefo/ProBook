import { Observable } from "rxjs";
import { SearchResult } from "../interfaces/searchResult";
import { SharedNotebook } from "../interfaces/sharedNotebook-interface";
import { ApiService } from "./api-service";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class CommentService{
    private endpoint='Comment';

    constructor(private apiService:ApiService){
        
    }

    getAll(params:any={}):Observable<SearchResult<Comment>>{
        return this.apiService.get<SearchResult<Comment>>(this.endpoint,params);
    }
    getById(id:number):Observable<Comment>{
        return this.apiService.getById<Comment>(this.endpoint,id);
    }
    create(sharedNotebook:any):Observable<Comment>{
        return this.apiService.post<Comment>(this.endpoint,sharedNotebook);
    }
    update(id:number,sharedNotebook:any):Observable<Comment>{
        return this.apiService.put<Comment>(this.endpoint,id,sharedNotebook);
    }
    delete(id:number):Observable<Comment>{
        return this.apiService.delete<Comment>(this.endpoint,id);
    }
    updateViewed(list:number[]):Observable<Comment[]>{
        return this.apiService.post<Comment[]>(this.endpoint + '/updateView',list);
    }
}