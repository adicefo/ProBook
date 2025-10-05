import { Notebook } from "./notebook-interface";
import { User } from "./user-interface";

export interface CollectionResponse{
    id?:number;
    name?:string;
    description?:string;
    createdAt?:Date;
    userId?:number;
    user?:User;
    notebooks?:Notebook[];
}