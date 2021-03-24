/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Footer } from './footer.component';

describe('FooterComponent', () => {
  let component: Footer;
  let fixture: ComponentFixture<Footer>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ Footer ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Footer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
