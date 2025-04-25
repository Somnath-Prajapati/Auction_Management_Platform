import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SettingsService } from '../../services/settings.service';
import { AllSettings } from '../../models/settings';

@Component({
  selector: '../app/component/settings',
  templateUrl: '',
  styleUrls: ['./settings.component.css']
})
export class SettingsFormComponent implements OnInit {
  settingsForm: FormGroup;

  constructor(private fb: FormBuilder, private settingsService: SettingsService) {}

  ngOnInit(): void {
    this.settingsForm = this.fb.group({
      auctionSettings: this.fb.group({
        globalIncrementalTime: ['']
      }),
      financeSettings: this.fb.group({
        vat: [''],
        creditCardFee: [''],
        debitCardFee: [''],
        adminFees: [''],
        auctionFees: [''],
        buyerCommission: ['']
      }),
      directSaleSettings: this.fb.group({
        cartItemsLimit: [''],
        cartItemsTimer: ['']
      }),
      staticPages: this.fb.group({
        privacyPolicy: [''],
        termsAndConditions: [''],
        cookiesPolicy: ['']
      }),
      footerLinks: this.fb.group({
        faq: [''],
        blog: [''],
        status: [''],
        twitter: [''],
        instagram: [''],
        facebook: [''],
        linkedin: [''],
        youtube: [''],
        appstore: [''],
        googlePlay: ['']
      })
    });
  }

  onSubmit(): void {
    const formValue: AllSettings = this.settingsForm.value;

    this.settingsService.updateAuctionSettings(formValue.auctionSettings).subscribe();
    this.settingsService.updateFinanceSettings(formValue.financeSettings).subscribe();
    this.settingsService.updateDirectSaleSettings(formValue.directSaleSettings).subscribe();
    this.settingsService.updateStaticPagesSettings(formValue.staticPages).subscribe();
    this.settingsService.updateFooterLinksSettings(formValue.footerLinks).subscribe();
  }
}
