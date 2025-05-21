# 🧠 N5Challenge - Permission System

A full-stack application to manage employee permissions, built with:

- 🟦 .NET 8 (API with CQRS & Clean Architecture)
- ⚛️ React (Vite + TypeScript)
- 🐳 Docker + Docker Compose
- 📦 Kafka (Confluent)
- 🔍 Elasticsearch
- 🗃 SQL Server

---

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js (v18+)](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Git

---

## 📦 Clone the repository

```bash
git clone https://github.com/jgarciadev12/N5Challenge.git
cd N5Challenge
```

---

## 🐳 Run Everything with Docker

This will spin up the entire environment:

- 🟦 Backend API (.NET)
- ⚛️ Frontend App (React)
- 🗃 SQL Server
- 📦 Kafka & Zookeeper
- 🔍 Elasticsearch
- 📊 Kafka UI

### ✅ Command

```bash
docker-compose up --build
```

### 🌐 Access:

- **Frontend:** http://localhost:5173  
- **API Swagger:** http://localhost:5000/swagger  
- **Kafka UI:** http://localhost:8081  
- **Elasticsearch:** http://localhost:9200  

> Note: make sure port 5000, 5173, 8081, and 9200 are free before running

---

## ⚙️ Run locally without Docker

### Backend (.NET)

```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

> Swagger: https://localhost:5001/swagger

### Frontend (React)

```bash
cd frontend
npm install
npm run dev
```

---

## 🧪 Tests

### Unit & Integration Tests

```bash
cd PermissionSystem.Tests
dotnet test
```

---

## 📁 Project Structure

```
N5Challenge/
│
├── backend/
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   ├── Persistence/
│   ├── API/
│
├── frontend/
│   └── src/components/
│
├── docker-compose.yml
└── README.md
```

---

## 📬 Contact

Created by [@jgarciadev12](https://github.com/jgarciadev12)
