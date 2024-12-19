import { Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

interface OnDestroyOptional {
  customOnDestroy?(): void;
}

@Injectable()
export abstract class BaseComponent implements OnDestroy {
  protected subscriptions: Subscription[] = [];

  ngOnDestroy() {
    this.unsubscribeAll();
    this.customOnDestroy();
  }

  protected addSubscription(subscription: Subscription) {
    this.subscriptions.push(subscription);
  }

  protected customOnDestroy() { }

  private unsubscribeAll() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }
}
