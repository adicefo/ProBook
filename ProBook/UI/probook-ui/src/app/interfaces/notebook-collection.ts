import { Collection } from "./collection-interface";
import { Notebook } from "./notebook-interface";


export interface NotebookCollection{
    id?:number;
    notebookId?:number;
    collectionId?:number;
    notebook?:Notebook;
    collection?:Collection;
    createdAt?:Date;
}