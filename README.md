# 🛠️ 1. System Setup

## 🍎 macOS (Homebrew)

### System Utilities
brew install wget git gnupg coreutils gettext

### Runtimes & Frameworks
brew install dotnet-sdk node@20

### Container & Kubernetes Tools
brew install docker helm kubectl k9s

### Task Runner (Project Automation)
brew install go-task/tap/go-task

### Link envsubst (Required for K8s templates)
brew link --force gettext


## 🪟 Windows (Chocolatey)

👉 Open PowerShell as Administrator

### System Utilities & Shells
choco install git wget openssl powershell-core

### Runtimes & Frameworks
choco install dotnet-sdk nodejs-lts

### Container & Kubernetes Tools
choco install docker-desktop kubernetes-helm kubernetes-cli k9s envsubst

### Task Runner
choco install go-task


# ⚙️ 2. Initial Configuration

## Clone the Repository
git clone <your-repo-url>
cd ProductStoreReactNetMongo

## Environment Setup
- Ensure the file exists: infra/Taskfile.env
- This file is the Single Source of Truth
- Verify paths in Taskfile.yml


# 🏃 3. Running the Application

We use `task` (go-task) to run the app in different modes.

## 🧑‍💻 Mode 1: Local Development (Native)
Best for: Coding, debugging, UI work

Steps:
task infra:up
task run:local

Access:
- WebApp → http://localhost:80
- API Swagger → http://localhost:5000/swagger


## 🐳 Mode 2: Docker Mode (Containerized)
Best for: Production-like testing

task run:docker

Access:
- WebApp → http://localhost


## ☸️ Mode 3: Kubernetes Mode (Orchestrated)
Best for: Helm + cluster testing

task run:k8s

Notes:
- Builds Docker images automatically
- Generates values.dev.yaml from templates

Monitor:
k9s


# 📊 4. Useful Commands

task --list            → List all tasks
task infra:up          → Start MongoDB only
task gen:k8s-values    → Generate K8s values from .env
task stop:all          → Stop all Docker & Helm deployments
kubectl get all        → View Kubernetes resources


# 🏗️ Project Architecture

This project uses a Single Source of Truth configuration.

Core file:
infra/Taskfile.env

Changes propagate to:
1. Local .NET environment variables
2. Docker Compose configuration
3. Helm values.dev.yaml (via envsubst)

## ✅ Key Benefit
- One config → works everywhere
- No environment mismatch
- Consistent behavior across Local, Docker, and Kubernetes