import {Routes} from '@angular/router';
import { MembersComponent } from './members/members.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { InfoComponent } from './info/info.component';

export const appRoutes: Routes = [
  // each route is an object
  { path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MembersComponent},
      { path: 'lists', component: ListsComponent},
      { path: 'messages', component: MessagesComponent},
      { path: 'info', component: InfoComponent}
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'}, // matches the full path of the url to redirect
];
