import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotebookPreviewComponent } from './notebook-preview.component';

describe('NotebookPreviewComponent', () => {
  let component: NotebookPreviewComponent;
  let fixture: ComponentFixture<NotebookPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NotebookPreviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NotebookPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
