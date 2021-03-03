import { Time } from '@angular/common';
import { Timestamp } from 'rxjs/internal/operators/timestamp';
import { AlertTypes } from './AlertTypes.enum';

export interface Alert {
  title: string;
  type: AlertTypes;
  message: string;
  timestamp: Date;
}
