export interface CreateRequest {
    requestNumber: string;
    userId: number;
    username: string;
    mobileNumber: string;
    email: string;
    requestTypeId: number;
    assetId: number;
    transactionId?: number;
    requestDateTime: string;
    requestStatusId: number;
    customerNote?: string;
    adminNote?: string;
    createdByAdmin: boolean;
  }
  