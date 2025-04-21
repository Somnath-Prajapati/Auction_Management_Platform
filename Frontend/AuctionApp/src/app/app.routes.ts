import { Routes } from '@angular/router';
import { FooterComponent } from './component/footer/footer.component';
import { DashboardComponent } from './component/dashboard/dashboard.component';
import { ManageAssetComponent } from './component/manage-asset/manage-asset.component';
import { ManageAuctionComponent } from './component/manage-auction/manage-auction.component';
import { ManageUserComponent } from './component/manage-user/manage-user.component';
import { LoginComponent } from './component/login/login.component';
import { SettingsComponent } from './component/settings/settings.component';
import { AddUserComponent } from './component/add-user/add-user.component';
import { AddAuctionComponent } from './component/add-auction/add-auction.component';
import { AddAssetComponent } from './component/add-asset/add-asset.component';
import { HomeComponent } from './component/home/home.component';

export const routes: Routes = [
    {path:'', component:LoginComponent},
    {path:'home', component:HomeComponent,children:[ {path:'assets', component:ManageAssetComponent},
    {path:'auctions', component:ManageAuctionComponent},
    {path:'dashboard', component:DashboardComponent},
    {path:'users', component:ManageUserComponent},
    {path:'settings', component:SettingsComponent},
    {path:'newUser', component:AddUserComponent},
    {path:'newAuction', component:AddAuctionComponent},
    {path:'newAsset', component:AddAssetComponent},]},
];
