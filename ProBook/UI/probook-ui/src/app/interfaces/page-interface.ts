
import { Notebook } from "./notebook-interface";
export interface Page{
    id?:number;
    title?:string;
    content?:string;
    imageUrl?:string;
    createdAt?:Date;
    notebookId?:number;
    notebook?:Notebook;
}
