import { User } from "./user-interface";

export interface Collection{
    id?:number;
    name?:string;
    description?:string;
    createdAt?:Date;
    userId?:number;
    user?:User;
}