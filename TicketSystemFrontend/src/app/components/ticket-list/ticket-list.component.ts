import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TicketService } from '../../services/ticket.service';
import { AuthService } from '../../services/auth.service';
import { TicketListDto } from '../../models/ticket.models';

@Component({
  selector: 'app-ticket-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {
  tickets: TicketListDto[] = [];
  loading: boolean = false;
  errorMessage: string = '';
  currentUser$ = this.authService.currentUser$;

  constructor(
    private ticketService: TicketService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.loading = true;
    this.errorMessage = '';
    
    this.ticketService.getAllTickets().subscribe({
      next: (tickets) => {
        this.tickets = tickets;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load tickets';
        this.loading = false;
        console.error('Error loading tickets:', error);
      }
    });
  }

  getCardColor(index: number): string {
    const colors = ['card-red', 'card-blue', 'card-green'];
    return colors[index % colors.length];
  }

  onCreateTicket(): void {
    this.router.navigate(['/tickets/create']);
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
