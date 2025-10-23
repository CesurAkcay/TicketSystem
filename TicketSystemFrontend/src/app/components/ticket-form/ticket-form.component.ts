import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TicketService } from '../../services/ticket.service';
import { AuthService } from '../../services/auth.service';
import { TicketPostDto, TicketStatus, TicketPriority } from '../../models/ticket.models';

@Component({
  selector: 'app-ticket-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ticket-form.component.html',
  styleUrls: ['./ticket-form.component.css']
})
export class TicketFormComponent implements OnInit {
  code: string = '';
  status: number = TicketStatus.Open;
  priority: number = TicketPriority.Medium;
  errorMessage: string = '';
  successMessage: string = '';
  loading: boolean = false;
  isEditMode: boolean = false;

  TicketStatus = TicketStatus;
  TicketPriority = TicketPriority;

  statusOptions = [
    { value: TicketStatus.Open, label: 'Open' },
    { value: TicketStatus.InProgress, label: 'In Progress' },
    { value: TicketStatus.Closed, label: 'Closed' }
  ];

  priorityOptions = [
    { value: TicketPriority.Low, label: 'Low' },
    { value: TicketPriority.Medium, label: 'Medium' },
    { value: TicketPriority.High, label: 'High' }
  ];

  constructor(
    private ticketService: TicketService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Check if we're in edit mode based on route params
    const ticketId = this.route.snapshot.paramMap.get('id');
    if (ticketId) {
      this.isEditMode = true;
      // Load ticket data if needed
    }
  }

  onSubmit(): void {
    if (!this.code) {
      this.errorMessage = 'Ticket code is required';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const currentUser = this.authService.getCurrentUser();
    if (!currentUser) {
      this.errorMessage = 'User not authenticated';
      this.loading = false;
      return;
    }

    const ticket: TicketPostDto = {
      code: this.code,
      ticketOwnerId: 'current-user-id', // This should come from authenticated user
      status: this.status,
      priority: this.priority
    };

    const operation = this.isEditMode 
      ? this.ticketService.updateTicket(ticket)
      : this.ticketService.addTicket(ticket);

    operation.subscribe({
      next: (response) => {
        this.successMessage = this.isEditMode 
          ? 'Ticket updated successfully!'
          : 'Ticket created successfully!';
        this.loading = false;
        setTimeout(() => {
          this.router.navigate(['/tickets']);
        }, 1500);
      },
      error: (error) => {
        this.errorMessage = error.error || 'Failed to save ticket';
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/tickets']);
  }
}
