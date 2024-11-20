import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IAppState } from 'src/app/shared/app-state/app-state';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  public appState$: Observable<IAppState>;

  constructor(private appStateService: AppStateService) {
      this.appState$ = this.appStateService.getAppState();
   }
   
  ngOnInit(): void {
  }

}
// If we need an IAppState object to do something with it's data and then show new results on the html
// you make a  public appState: IAppState; and a public sub: Subscription; 
// and implement OnDestroy to get the ngOnDestroy(): void where you do sub.unsubscribe();