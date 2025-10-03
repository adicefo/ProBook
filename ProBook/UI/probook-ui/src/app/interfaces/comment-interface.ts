import { Page } from "./page-interface";
import { SharedNotebook } from "./sharedNotebook-interface";
import { User } from "./user-interface";

export interface Comment{
    id?:number;
    content?:string;
    createdAt?:Date;
    viewed?:boolean;
    pageId?:number;
    userid?:number;
    sharedNotebookId?:number;
    page?:Page;
    user?:User;
    sharedNotebook?:SharedNotebook;
}