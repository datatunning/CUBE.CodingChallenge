import { Component } from '@angular/core';
import { interval } from 'rxjs';
import { Alert } from 'src/app/models/Alert.model';
import { AlertTypes } from 'src/app/models/AlertTypes.enum';
import { AlertService } from 'src/app/services/Alert.service';
import { HealthClientService } from 'src/app/services/HealthClient.service';

@Component({
  selector: 'cube-root',
  templateUrl: './app-root.component.html',
  styleUrls: ['./app-root.component.scss']
})
export class AppRootComponent {
  title = 'CUBE Coding Challenge';
  last: Alert;

  // Allow to use the enum value along with associated enum string in the template.
  alertTypeNames: string[] = Object.values(
    AlertTypes
  ).filter((key) => isNaN(+key));
  // Allow to use the enum items in template for comparison with enum values.
  alertType = AlertTypes;

  constructor(private healthClient: HealthClientService, public alertService: AlertService) {
    interval(30000).subscribe(() => {
      let alertPayload: Alert = null;

      this.healthClient.check().subscribe(
        (healthStatus) => {
          console.log(healthStatus);
          if (healthStatus.toLowerCase() !== 'healthy') {
            alertPayload = {
              title: 'API Status',
              type: AlertTypes.error,
              message: 'Api is down',
              timestamp: new Date()
            };
            alert(this.alertType.error === this.alertTypeNames[alertPayload.type]);
            this.alertService.push(alertPayload);
          }
        },
        (error) => {
          alertPayload = {
            title: 'API Status',
            type: AlertTypes.error,
            message: error.message,
            timestamp: new Date()
          };
          this.alertService.push(alertPayload);
        });
      });
  }
}
