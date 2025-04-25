export interface AuctionSettings {
  globalIncrementalTimeInMinutes: number;
}
 
export interface FinanceSettings {
  vatPercent: number;
  creditCardFee: number;
  debitCardFee: number;
  adminFees: number;
  auctionFees: number;
  buyerCommission: number;
}
 
export interface DirectSaleSettings {
  cartItemsLimit: number;
  cartTimerInMinutes: number;
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
  twitterLink: string;
  instagramLink: string;
  facebookLink: string;
  linkedInLink: string;
  youTubeLink: string;
  appStoreLink: string;
  googlePlayLink: string;
}
export interface AllSettings {
  auctionSettings: AuctionSettings;
  financeSettings: FinanceSettings;
  directSaleSettings: DirectSaleSettings;
  staticPages: StaticPagesSettings;
  footerLinks: FooterLinksSettings;
}
 