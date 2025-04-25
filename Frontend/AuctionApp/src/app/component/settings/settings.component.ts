import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SettingsService } from '../../services/settings.service';
import { AuctionSettings, FinanceSettings, DirectSaleSettings, StaticPagesSettings, FooterLinksSettings } from '../../modals/settings';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
})
export class SettingsComponent implements OnInit {
  settingsForm!: FormGroup;
  viewMode = true; // true = viewing, false = editing
  latestSettings: any = {};
  editMode = false;

  constructor(private fb: FormBuilder, private settingsService: SettingsService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadSettings();
  }

  initForm(): void {
    this.settingsForm = this.fb.group({
      auctionSettings: this.fb.group({    
        globalIncrementalTimeInMinutes: ['']
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
        cartTimerInMinutes: ['']
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
        twitterLink: [''],
        instagramLink: [''],
        facebookLink: [''],
        linkedinLink: [''],
        youTubeLink: [''],
        appStoreLink: [''],
        googlePlayLink: ['']
      })
    });
  }
 

  loadSettings(): void {
    // Load auction settings
    this.settingsService.getAuctionSettings().subscribe((data: AuctionSettings) => {
      this.latestSettings.auctionSettings = data;
      this.settingsForm.get('auctionSettings')?.patchValue(data);
    });

    // Load finance settings
    this.settingsService.getFinanceSettings().subscribe((data: FinanceSettings) => {
      this.latestSettings.financeSettings = data;
      this.settingsForm.get('financeSettings')?.patchValue(data);
    });

    // Load direct sale settings
    this.settingsService.getDirectSaleSettings().subscribe((data: DirectSaleSettings) => {
      this.latestSettings.directSaleSettings = data;
      this.settingsForm.get('directSaleSettings')?.patchValue(data);
    });

    // Load static pages settings
    this.settingsService.getStaticPagesSettings().subscribe((data: StaticPagesSettings) => {
      this.latestSettings.staticPages = data;
      this.settingsForm.get('staticPages')?.patchValue(data);
    });

    // Load footer links settings
    this.settingsService.getFooterLinksSettings().subscribe((data: FooterLinksSettings) => {
      this.latestSettings.footerLinks = data;
      this.settingsForm.get('footerLinks')?.patchValue(data);
    });
  }

  showSettings(): void {
    this.settingsService.getAllSettings().subscribe({
      next: (data) => {
        // Assuming each section is an array and you want the last one in each
        this.latestSettings = {
          auctionSettings: Array.isArray(data.auctionSettings) ? data.auctionSettings[data.auctionSettings.length - 1] : data.auctionSettings,
          financeSettings: Array.isArray(data.financeSettings) ? data.financeSettings[data.financeSettings.length - 1] : data.financeSettings,
          directSaleSettings: Array.isArray(data.directSaleSettings) ? data.directSaleSettings[data.directSaleSettings.length - 1] : data.directSaleSettings,
          staticPages: Array.isArray(data.staticPages) ? data.staticPages[data.staticPages.length - 1] : data.staticPages,
          footerLinks: Array.isArray(data.footerLinks) ? data.footerLinks[data.footerLinks.length - 1] : data.footerLinks,
        };
 
        this.settingsForm.patchValue(this.latestSettings);
        this.viewMode = true;
        console.log(this.latestSettings);
        // console.log('Raw auction settings:', data.auctionSettings);
 
      },
      error: (error: any) => {
        console.error('Failed to load settings:', error);
      }
    });
  }

  editSettings(): void {
    this.viewMode = false;
  }

  onSubmit(): void {
    const formValue = this.settingsForm.value;

    // Update settings only if they are changed
    if (this.settingsForm.dirty) {
      this.settingsService.updateAuctionSettings(formValue.auctionSettings).subscribe();
      this.settingsService.updateFinanceSettings(formValue.financeSettings).subscribe();
      this.settingsService.updateDirectSaleSettings(formValue.directSaleSettings).subscribe();
      this.settingsService.updateStaticPagesSettings(formValue.staticPages).subscribe();
      this.settingsService.updateFooterLinksSettings(formValue.footerLinks).subscribe();
    }

    this.viewMode = true;
  }
}
