import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MessageTemplatesComponent } from './message-templates.component';

describe('MessageTemplatesComponent', () => {
  let component: MessageTemplatesComponent;
  let fixture: ComponentFixture<MessageTemplatesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MessageTemplatesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MessageTemplatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
