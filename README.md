# 🚀 Crack Technology: Full-Stack Dev Setup & Execution

This repository contains the source code and orchestration logic for the **Crack Technology** professional project. It demonstrates a high-performance architecture using **.NET 10**, **React (Vite)**, **MongoDB**, and **Kubernetes**.

---

## 🛠 1. Prerequisites (Developer Tools)

To maintain the "Three-Way Path" (Local, Docker, K8s), you need specific tools installed based on your Operating System.

### **🍎 macOS (Using Homebrew)**
Open your terminal and run:
```bash
# System Utilities
brew install wget git gnupg coreutils gettext

# Runtimes & Frameworks
brew install dotnet-sdk node@20

# Container & K8s Orchestration
brew install docker helm kubectl k9s

# Task Runner (The Brain of the project)
brew install go-task/tap/go-task

# Force link envsubst (Required for K8s templates)
brew link --force gettext

### Open PowerShell as Administrator and run:**

PowerShell
# System Utilities & Shells
choco install git wget openssl powershell-core

# Runtimes & Frameworks
choco install dotnet-sdk nodejs-lts

# Container & K8s Orchestration
choco install docker-desktop kubernetes-helm kubernetes-cli k9s envsubst

# Task Runner
choco install go-task

⚙️ 2. Initial Configuration
Clone the Repository:

Bash
    git clone <your-repo-url>
    cd ProductStoreReactNetMongo
    ```

2.  **Environment Setup:**
    Ensure the file `infra/Taskfile.env` exists. This file is the **Single Source of Truth** for all environments. Check that the paths in `Taskfile.yml` match your local directory structure.

---

## 🏃 3. Running the Application

We use `go-task` (the `task` command) to automate the three different ways to run this application.

### **Mode 1: Local Development (Native)**
*Best for: Writing code, UI changes, and debugging.*
1.  **Start Database:** `task infra:up`
2.  **Start Full Stack:** `task run:local`
    *   **WebApp:** `http://localhost:80`
    *   **API Swagger:** `http://localhost:5000/swagger`

### **Mode 2: Docker Mode (Containerized)**
*Best for: Testing production-ready containers and networking.*
1.  **Run Full Stack:** `task run:docker`
    *   **WebApp:** `http://localhost`

### **Mode 3: Kubernetes Mode (Orchestrated)**
*Best for: Testing Helm charts and cluster behavior.*
1.  **Deploy to K8s:** `task run:k8s`
    *   *Note:* This task builds Docker images and generates `values.dev.yaml` files automatically from templates.
2.  **Monitor:** Run `k9s` to manage your pods and services.

---

## 📊 4. Useful Automation Commands

| Command | Action |
| :--- | :--- |
| `task --list` | List all available developer tasks |
| `task infra:up` | Spin up MongoDB only |
| `task gen:k8s-values` | Manually update K8s values from `.env` |
| `task stop:all` | Stop and remove all Docker and Helm deployments |
| `kubectl get all` | View status of all K8s resources |

---

## 🏗 Project Architecture Note
This project utilizes a **Single Source of Truth** configuration. By editing `infra/Taskfile.env`, your changes propagate automatically to:
1.  The **Local** .NET Environment Variables.
2.  The **Docker Compose** environment mapping.
3.  The **Helm Chart** `values.dev.yaml` templates via `envsubst`.

This ensures consistency whether you are running a single service or an entire cluster.