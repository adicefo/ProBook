import { Page } from "./page-interface";
import { User } from "./user-interface";

export interface Comment{
    id?:number;
    content?:string;
    createdAt?:Date;
    viewed?:boolean;
    pageId?:number;
    userid?:number;
    page?:Page;
    user?:User;
}