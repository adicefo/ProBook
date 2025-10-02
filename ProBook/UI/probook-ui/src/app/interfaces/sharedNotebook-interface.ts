import { Notebook } from "./notebook-interface";
import { User } from "./user-interface";

export interface SharedNotebook{
    id?:number;
    sharedDate?:Date;
    isForEdit?:boolean;
    notebookId?:number;
    fromUserId?:number;
    toUserId?:number;
    notebook?:Notebook;
    fromUser?:User;
    toUser?:User;
}