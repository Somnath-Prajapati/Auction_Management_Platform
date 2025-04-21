import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAssetComponent } from './manage-asset.component';

describe('ManageAssetComponent', () => {
  let component: ManageAssetComponent;
  let fixture: ComponentFixture<ManageAssetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageAssetComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageAssetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
