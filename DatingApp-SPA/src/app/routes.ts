import {Routes} from '@angular/router';
import { MemberListComponent } from './member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { HomeComponent } from './home/home.component';

export const appRoutes: Routes = [
  // each route is an object
  { path: 'home', component: HomeComponent},
  { path: 'member-list', component: MemberListComponent},
  { path: 'lists', component: ListsComponent},
  { path: 'messages', component: MessagesComponent},
  { path: '**', redirectTo: 'home', pathMatch: 'full'}, // matches the full path of the url to redirect
];
