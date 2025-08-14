# 🕹️ Anima

Project made to manage insurance proposals and hiring processes.

## Technical Requirements

Hexagonal Architecture
Microservices (Proposal and Contract)
Relational Database
Clean code, DDD, SOLID, TDD
Microservices Communication
.NET 8
Docker

## Technical Requirements

### Proposal Service

Create an insurance proposal
List proposals
Change proposal status (InAnalysis, Approved, Rejected)

### Contract Service

Contract a proposal
Store contract information (ProposalID, ProposalDate)
Communicate with Proposal Service to check proposal status


## Build & Tests
| CI | Status |
| --- | --- | 
| Validação PR| [![Pré-validação de Release/Hotfix](https://github.com/leandrokuranaga/anima/actions/workflows/validate-release-pr.yml/badge.svg)](https://github.com/leandrokuranaga/anima/actions/workflows/validate-release-pr.yml)
