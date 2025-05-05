import { Routes } from '@angular/router';
import { DashboardComponent } from './component/dashboard/dashboard.component';

import { ManageUserComponent } from './component/manage-user/manage-user.component';
import { SettingsComponent } from './component/settings/settings.component';
import { AddUserComponent } from './component/add-user/add-user.component';
import { AddAuctionComponent } from './component/add-auction/add-auction.component';
import { AddAssetComponent } from './component/add-asset/add-asset.component';
import { HomeComponent } from './component/home/home.component';
import { UpdateUserComponent } from './component/update-user/update-user.component';
import { DetailsUserComponent } from './component/details-user/details-user.component';
import { LandingPageComponent } from './component/landing-page/landing-page.component';
import { StartPageComponent } from './component/start-page/start-page.component';


export const routes: Routes = [
    { path: '', component: StartPageComponent },
    {path:'dashboard', component:DashboardComponent},
    {path:'users', component:ManageUserComponent},
    {path:'updateUser', component:UpdateUserComponent},
    {path:'settings', component:SettingsComponent},
    {path:'newUser', component:AddUserComponent},
    {path:'newAuction', component:AddAuctionComponent},
    {path:'detailsUser', component:DetailsUserComponent},
    {path:'newAsset', component:AddAssetComponent},]},

];
