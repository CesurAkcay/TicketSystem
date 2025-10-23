export interface TicketPostDto {
  code: string;
  createdAt?: string;
  ticketOwnerId: string;
  status?: number;
  priority?: number;
}

export interface TicketListDto {
  code: string;
  createdAtStr: string;
  customerFullName: string;
  statusStr: string;
  priorityStr: string;
}

export enum TicketStatus {
  Open = 0,
  InProgress = 1,
  Closed = 2
}

export enum TicketPriority {
  Low = 0,
  Medium = 1,
  High = 2
}
