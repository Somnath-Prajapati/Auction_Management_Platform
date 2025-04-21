import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAuctionComponent } from './manage-auction.component';

describe('ManageAuctionComponent', () => {
  let component: ManageAuctionComponent;
  let fixture: ComponentFixture<ManageAuctionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageAuctionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageAuctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
