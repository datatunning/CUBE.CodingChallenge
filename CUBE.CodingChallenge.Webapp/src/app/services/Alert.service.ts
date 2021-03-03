import { Injectable } from '@angular/core';
import { merge, Observable, Subject } from 'rxjs';
import { map, scan } from 'rxjs/operators';
import { Alert } from '../models/Alert.model';

enum ActionType {
  push = 'push',
  pop = 'pop'
}

interface Action {
  type: ActionType;
  payload?: Alert;
}

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  alerts$: Observable<Alert[]>;
  count: number;

  private pushSource = new Subject<Alert>();
  private popSource = new Subject<void>();

  constructor() {
    const push$ = this.pushSource.asObservable()
      .pipe(map((payload: Alert) => ({ type: ActionType.push, payload } as Action)));

    const pop$ = this.popSource.asObservable()
      .pipe(map(() => ({ type: ActionType.pop } as Action)));

    this.alerts$ = merge(push$, pop$)
      .pipe(
        scan((acc: Alert[], { payload, type }) => {
          if (type === ActionType.pop) {
            acc = acc.slice(0, -1);
          }
          if (type === ActionType.push) {
            acc = [...acc, payload];
            this.count = acc.length;
          }
          return acc;
        },
        [])
      );
  }

  push(alert: Alert) {
    this.pushSource.next(alert);
  }

  pop() {
    this.popSource.next();
  }
}
