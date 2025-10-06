import { Observable } from "rxjs";
import { SearchResult } from "../interfaces/searchResult";
import { SharedNotebook } from "../interfaces/sharedNotebook-interface";
import { Comment } from "../interfaces/comment-interface";
import { ApiService } from "./api-service";
import { Injectable } from "@angular/core";
import { Collection } from "../interfaces/collection-interface";
import { NotebookCollection } from "../interfaces/notebook-collection";
import { CollectionResponse } from "../interfaces/collectionResponse";

@Injectable({
    providedIn: 'root'
})
export class CollectionService {
    private endpoint = 'Collection';

    constructor(private apiService: ApiService) {

    }

    getAll(params: any = {}): Observable<SearchResult<Collection>> {
        return this.apiService.get<SearchResult<Collection>>(this.endpoint, params);
    }
    getById(id: number): Observable<Collection> {
        return this.apiService.getById<Collection>(this.endpoint, id);
    }
    create(collection: any): Observable<Collection> {
        return this.apiService.post<Collection>(this.endpoint, collection);
    }
    update(id: number, collection: any): Observable<Collection> {
        return this.apiService.put<Collection>(this.endpoint, id, collection);
    }
    delete(id: number): Observable<Collection> {
        return this.apiService.delete<Collection>(this.endpoint, id);
    }
    getCollectionResponse(userId:number):Observable<CollectionResponse[]>{
        return this.apiService.get<CollectionResponse[]>(this.endpoint + '/getCollectionResponse/' + userId);
    }
    
    addToCollection(request:any):Observable<NotebookCollection>{
        return this.apiService.post<NotebookCollection>(this.endpoint + '/addToCollection', request);
    }

    removeFromCollection(request:any):Observable<NotebookCollection>{
        return this.apiService.deleteBody<NotebookCollection>(this.endpoint + '/removeFromCollection', request);
    }
    
    
}