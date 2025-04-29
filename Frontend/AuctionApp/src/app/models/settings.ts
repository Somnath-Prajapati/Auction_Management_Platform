export interface AuctionSettings {
    globalIncrementalTime: number;
  }
  
  export interface FinanceSettings {
    vat: number;
    creditCardFee: number;
    debitCardFee: number;
    adminFees: number;
    auctionFees: number;
    buyerCommission: number;
  }
  
  export interface DirectSaleSettings {
    cartItemsLimit: number;
    cartItemsTimer: number;
  }
  
  export interface StaticPagesSettings {
    privacyPolicy: string;
    termsAndConditions: string;
    cookiesPolicy: string;
  }
  
  export interface FooterLinksSettings {
    faq: string;
    blog: string;
    status: string;
    twitter: string;
    instagram: string;
    facebook: string;
    linkedin: string;
    youtube: string;
    appstore: string;
    googlePlay: string;
  }
  
  export interface AllSettings {
    auctionSettings: AuctionSettings;
    financeSettings: FinanceSettings;
    directSaleSettings: DirectSaleSettings;
    staticPages: StaticPagesSettings;
    footerLinks: FooterLinksSettings;
  }
  