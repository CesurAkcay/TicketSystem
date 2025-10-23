import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketListDto, TicketPostDto } from '../models/ticket.models';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private apiUrl = 'http://localhost:5000/api/Ticket'; // Update with your API URL

  constructor(private http: HttpClient) {}

  // Get all tickets (Admin only)
  getAllTickets(): Observable<TicketListDto[]> {
    return this.http.get<TicketListDto[]>(`${this.apiUrl}/getall`);
  }

  // Add new ticket
  addTicket(ticket: TicketPostDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/add`, ticket);
  }

  // Update ticket
  updateTicket(ticket: TicketPostDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/update`, ticket);
  }

  // Delete ticket
  deleteTicket(ticket: TicketPostDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/delete`, ticket);
  }
}
