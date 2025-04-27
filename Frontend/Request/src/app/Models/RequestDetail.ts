// export interface RequestDetail {
//     requestId: number;
//     requestNumber: string;
//     userId: number;
//     username: string;
//     mobileNumber: string;
//     email: string;
//     requestTypeId: number;
//     assetId: number;
//     transactionId?: number;
//     requestDateTime: string;
//     requestStatusId: number;
//     customerNote?: string;
//     adminNote?: string;
//     createdByAdmin: boolean;
//     createdOn: string;
//     updatedOn: string;
//   }
  
export interface RequestDetail {
    requestId?: number;           // optional when creating
    requestNumber: string;
    userId: number;
    username: string;
    mobileNumber: string;
    email: string;
    requestTypeId: number;
    assetId: number;
    transactionId?: number;
    requestDateTime: string;      // ISO string
    requestStatusId: number;
    customerNote?: string;
    adminNote?: string;
    createdByAdmin: boolean;
  }
  