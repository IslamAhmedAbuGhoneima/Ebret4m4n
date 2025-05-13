import { Component } from '@angular/core';

@Component({
  selector: 'app-payment-cancel',
  imports: [],
  templateUrl: './payment-cancel.component.html',
  styleUrl: './payment-cancel.component.css',
})
export class PaymentCancelComponent {
  paymentPage() {
    const url =
      'https://checkout.stripe.com/c/pay/cs_test_a1DFEK5544uVAve5uprxXAUsjAzlIYgDDw8nZfwHwePrasuzX6APhs78C3#fidkdWxOYHwnPyd1blpxYHZxWjA0V0BgVVBBMH8xPGc2NjIwSXA1YHRzT1VRan01QzBkSWNnRnNpQjFfNlZnPFB2f0luYk1tS2EybURdf2hLMHBHRzBUXWJ2bTFONExdYW9fdUxtTWZiQ2k9NTVvXDVDb3NcSycpJ2N3amhWYHdzYHcnP3F3cGApJ2lkfGpwcVF8dWAnPyd2bGtiaWBabHFgaCcpJ2BrZGdpYFVpZGZgbWppYWB3dic%2FcXdwYHgl';
    window.open(url, '_self');
  }
}
