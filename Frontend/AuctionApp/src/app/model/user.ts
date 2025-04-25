// user.model.ts

export interface Role {
    roleId: number;
    roleName: string;
  }
  
  export interface Status {
    statusId: number;
    statusName: string;
  }
  export interface Country {
    countryId: number;
    countryName: string;
    phoneCode: string;
    minLength: number;
    maxLength: number;
    tblUsers?: (null)[] | null;
  }
  
  export interface User {
    name: string;
    mobileNumber: string;
    email: string;
    companyName: string;
    companyNumber: string;
    statusId: number;
    chatEnabled: boolean;
    roleId: number;
    personalIdNumber: string;
    gender: string;
    personalIdExpiryDate: string;
    countryId: number;
    profileImage: File | null;
    personalIdImage: File | null;
  }
  
  export interface UserView {
    userId:number; 
    uid: number;
    name: string;
    email: string;
    mobileNumber: string;
    companyName: string;
    roleId: number;
    statusId: number;
    gender: string;
  }
  