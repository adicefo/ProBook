import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedNotebookComponent } from './shared-notebook.component';

describe('SharedNotebookComponent', () => {
  let component: SharedNotebookComponent;
  let fixture: ComponentFixture<SharedNotebookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharedNotebookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SharedNotebookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
