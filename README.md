# Datavanced Product Management System

A high-performance, secure, and scalable enterprise Product Management System built using a modern **N-Layer Architecture** with **.NET Core 8 Web API** and **Angular 21**. The system features strict **Token-Based Authorization**, server-side data grids, and optimized CRUD capabilities designed to keep API response times under **300ms**.

---

## 🏛️ Architecture Overview

The application follows clean architecture principles with a strict separation of concerns across four key layers:
*   **API Layer (Presentation):** Exposes RESTful endpoints, handles HTTP requests/responses, and enforces strict JWT Authorization rules to ensure only authenticated users can access product management features.
*   **Service Layer (Business):** Implements business rules (e.g., stock validation, category matching), handles DTO mapping.
*   **Repository Layer (Data Access):** Utilizes EF Core (Code-First) with optimized, non-tracking, parameterized queries to safeguard against SQL Injection.
*   **Shared Kernel (Common):** Houses reusable DTOs, Enums, JWT helpers, and pagination/search request models.

---
## 🚀 Tech Stack

### Backend
*   **Framework:** ASP.NET Core 8 Web API
*   **ORM:** Entity Framework Core 8 (Code-First)
*   **Database:** MS SQL Server
*   **Security:** JWT Authentication & Custom Authorization Filter (Ensures secure endpoint access for logged-in users).
*   **Caching:** Hybrid Strategy (`IMemoryCache` & Redis Distributed Cache) for product lookups.

---

## 📦 Core Product Management Capabilities (CRUD)

The system supports a full **CRUD (Create, Read, Update, Delete)** lifecycle with a strong security layer:

> 🔒 **Security Note:** All CRUD endpoints are strictly protected. Anonymous or unauthenticated requests are blocked instantly with a `401 Unauthorized` or `400 Bad Request` status code. Only successfully logged-in users with a valid JWT token can perform these operations.

1.  **Create (Add Product):** Secure forms featuring client-side and server-side model validation (`[Required]`, bounds checking for price/quantity).
2.  **Read (View Details & List):** Server-side paginated list optimized using `AsNoTracking()` to reduce EF Core tracking overhead. Supports single product deep-view templates.
3.  **Update (Edit Product):** Partial and full entity updates mapped cleanly using AutoMapper, complete with real-time field sync.
4.  **Delete (Remove Product):** Safe deletion process integrated with front-end Bootstrap confirmation modals to prevent accidental loss of data.
