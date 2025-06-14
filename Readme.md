# 🏦 Banking System Project

A modern, modular banking system built with **.NET 9**, **C#**, and **Microservices** architecture. Designed to be scalable, maintainable, and production-ready, with full CI/CD integration using Docker, GitHub, and GitHub Actions.

## 🚀 Features

The system includes the following core modules:

1. ✅ **Add Customers** – Register new clients with secure identity management.
2. 🏦 **Open Accounts** – Create checking/saving accounts tied to customers.
3. 💰 **Deposit Funds** – Support for crediting accounts with proper validations.
4. 💸 **Withdraw Funds** – Secure withdrawals with balance checks and logging.
5. 📄 **View Transactions** – Full transaction history per account.

## 🧱 Tech Stack

| Technology     | Description                                  |
|----------------|----------------------------------------------|
| .NET 9         | Core framework for all microservices         |
| C#             | Backend language                             |
| Microservices  | Modular service-based architecture           |
| Docker         | Containerization of each service             |
| GitHub         | Source code management                       |
| GitHub Actions | Automated CI/CD workflows                    |

## 🐳 Deployment

Each microservice is containerized with Docker. You can spin up the entire system using Docker Compose or Kubernetes (if configured).

```bash
docker-compose up --build
```

>Make sure Docker and .NET 9 SDK are installed on your machine.

🧪 CI/CD

- GitHub Actions automatically:

  - Builds and tests each service on push

  - Lints and verifies PRs

  - Deploys to the configured environment (optional)
  
📁 Project Structure

```bash
/src
  /CustomerService
  /AccountService
  /TransactionService
  ...
/docker
.github/workflows
README.md
```  

>📌 Future Enhancements

- 🛡️ Authentication/authorization (OAuth2 / JWT)  
- 🌐 REST + gRPC hybrid support
- 📊 Admin dashboard for monitoring
- 🔍 ElasticSearch or similar for transaction search

>🤝 Contributing

Feel free to fork, clone, or raise issues. PRs welcome!
