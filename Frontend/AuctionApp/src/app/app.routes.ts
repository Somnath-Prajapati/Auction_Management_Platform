import { Routes } from '@angular/router';
import { DashboardComponent } from './component/dashboard/dashboard.component';
import { ManageAssetComponent } from './component/manage-asset/manage-asset.component';
import { ManageAuctionComponent } from './component/manage-auction/manage-auction.component';
import { ManageUserComponent } from './component/manage-user/manage-user.component';
import { LoginComponent } from './component/login/login.component';
import { SettingsComponent } from './component/settings/settings.component';
import { AddUserComponent } from './component/add-user/add-user.component';
import { AddAuctionComponent } from './component/add-auction/add-auction.component';
import { AddAssetComponent } from './component/add-asset/add-asset.component';
import { LandingPageComponent } from './component/landing-page/landing-page.component';
import { StartPageComponent } from './component/start-page/start-page.component';


export const routes: Routes = [
    { path: '', component: StartPageComponent },
    {path:'dashboard', component:DashboardComponent},
    {path:'assets', component:ManageAssetComponent},
    { path: 'landing-page', component: LandingPageComponent },
    {path:'auctions', component:ManageAuctionComponent},
    { path: 'users', component: AddUserComponent },
    {path:'login', component:LoginComponent},
    {path:'settings', component:SettingsComponent},
    {path:'newUser', component:AddUserComponent},
    {path:'newAuction', component:AddAuctionComponent},
    {path:'newAsset', component:AddAssetComponent},

];
