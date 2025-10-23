# Ticket System Frontend - Setup Guide

## Overview

This Angular frontend provides a complete user interface for the Ticket System with authentication, ticket management, and a modern card-based design.

## Features

### Authentication
- Customer and Admin login/registration
- JWT token-based authentication
- Automatic token injection via HTTP interceptor
- Route guards for protected pages

### Ticket Management
- View all tickets in colorful card layout
- Create new tickets
- Update existing tickets  
- Delete tickets
- Status and priority management

### UI Design
- Responsive design matching the template
- Three gradient color themes (red, blue, green) for ticket cards
- Clean and intuitive forms
- Mobile-friendly layout

## Installation

1. **Install dependencies:**
```bash
npm install
```

2. **Configure API URLs:**

Edit these files to point to your backend:
- `src/app/services/auth.service.ts` (line 18)
- `src/app/services/ticket.service.ts` (line 10)

Change `http://localhost:5000` to your actual backend URL.

## Running the Application

### Development Mode

```bash
ng serve
```

Then navigate to `http://localhost:4200/`

### Production Build

```bash
ng build --configuration production
```

Build artifacts will be in the `dist/` directory.

## Usage Guide

### For Customers

1. **Sign Up:**
   - Go to `/signup`
   - Select "Customer Signup"
   - Enter your full name, email, and password
   - Click "Sign Up"

2. **Login:**
   - Go to `/login`
   - Select "Customer Login"
   - Enter your credentials
   - You'll be redirected to `/tickets`

3. **Manage Tickets:**
   - View your tickets on the dashboard
   - Click "+ New Ticket" to create a ticket
   - Fill in ticket code, status, and priority
   - Click "Create Ticket"

### For Admins

1. **Sign Up:**
   - Go to `/signup`
   - Select "Admin Signup"
   - Enter your details
   - Click "Sign Up"

2. **Login:**
   - Go to `/login`
   - Select "Admin Login"
   - Enter your credentials
   - You'll be redirected to `/admin/tickets`

3. **View All Tickets:**
   - Admins can see tickets from all customers
   - View ticket details in card format

## Application Routes

| Route | Description | Auth Required | Role Required |
|-------|-------------|---------------|---------------|
| `/login` | Login page | No | - |
| `/signup` | Registration page | No | - |
| `/tickets` | Ticket list | Yes | Any |
| `/tickets/create` | Create new ticket | Yes | Any |
| `/tickets/edit/:id` | Edit ticket | Yes | Any |
| `/admin/tickets` | Admin view all tickets | Yes | Admin |

## Project Structure

```
src/app/
├── components/
│   ├── login/                  # Login component
│   │   ├── login.component.ts
│   │   ├── login.component.html
│   │   └── login.component.css
│   ├── signup/                 # Signup component
│   │   ├── signup.component.ts
│   │   ├── signup.component.html
│   │   └── signup.component.css
│   ├── ticket-list/            # Ticket list with cards
│   │   ├── ticket-list.component.ts
│   │   ├── ticket-list.component.html
│   │   └── ticket-list.component.css
│   └── ticket-form/            # Create/edit ticket form
│       ├── ticket-form.component.ts
│       ├── ticket-form.component.html
│       └── ticket-form.component.css
├── services/
│   ├── auth.service.ts         # Authentication logic
│   └── ticket.service.ts       # Ticket CRUD operations
├── models/
│   ├── auth.models.ts          # Auth DTOs and interfaces
│   └── ticket.models.ts        # Ticket DTOs and enums
├── guards/
│   └── auth.guard.ts           # Route protection
├── interceptors/
│   └── auth.interceptor.ts     # JWT token injection
└── app.routes.ts               # Route configuration
```

## Backend API Endpoints

The frontend expects these endpoints from the backend:

### Authentication Endpoints
```
POST /api/Auth/loginCustomer
POST /api/Auth/registerCustomer
POST /api/Auth/loginAdminUser
POST /api/Auth/registerAdminUser
```

### Ticket Endpoints
```
GET  /api/Ticket/getall    (Admin only)
POST /api/Ticket/add
POST /api/Ticket/update
POST /api/Ticket/delete
```

## Technologies

- **Angular 19** - Latest Angular framework
- **TypeScript** - Type-safe JavaScript
- **RxJS** - Reactive programming
- **Standalone Components** - Modern Angular architecture
- **CSS3** - Custom styling with gradients

## Troubleshooting

### CORS Errors

If you see CORS errors in the browser console:
- Ensure your backend has CORS enabled
- Add `http://localhost:4200` to allowed origins in backend

### Token Issues

If authentication isn't working:
- Check browser DevTools > Application > Local Storage
- Verify token is stored
- Check Network tab for Authorization header

### Build Errors

If you encounter build errors:
```bash
# Clear cache and reinstall
rm -rf node_modules package-lock.json
npm install

# Clear Angular cache
ng cache clean
```

## Development Tips

- Use browser DevTools to inspect network requests
- Check console for error messages
- Use Angular DevTools extension for debugging
- Hot reload is enabled in development mode

## Next Steps

1. Start the backend API server
2. Run `ng serve` for the frontend
3. Navigate to `http://localhost:4200`
4. Sign up as a customer or admin
5. Start managing tickets!

## Support

For issues or questions:
- Check the browser console for errors
- Verify backend API is running
- Ensure API URLs are configured correctly
