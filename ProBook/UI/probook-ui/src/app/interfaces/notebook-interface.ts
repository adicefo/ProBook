import{User} from './user-interface';
export interface Notebook{
    id?:number;
    name?:string;
    imageUrl?:string;
    description?:string;
    createdAt?:Date;
    userId?:number;
    user?:User;
}