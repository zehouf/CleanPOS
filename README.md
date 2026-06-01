# CleanPOS — Point of Sale Application
 
A production-ready POS system built to showcase Clean Architecture
and SOLID principles with ASP.NET Core 10.
 
## Tech Stack
 
 Layer       Technology                        
-----------------------------------------------
 Backend     ASP.NET Core 10, C#               
 ORM         Entity Framework Core 10          
 CQRS        MediatR                           
 Auth        ASP.NET Identity + JWT            
 Database    PostgreSQL                        
 Frontend    Vue.js 3 + Vite + Pinia           
 Tests       xUnit + Moq + FluentAssertions    
 CICD       GitHub Actions                    
 
## Architecture
 
```
Domain - Application - Infrastructure - WebAPI
```
 
## Getting Started
 
```bash
# Clone the repository
git clone httpsgithub.comVOTRE_USERNAMECleanPOS.git
 
# Navigate to backend
cd CleanPOSbackend
 
# Restore dependencies
dotnet restore
 
# Build
dotnet build
```
 
## Project Status
 
- [ ] Domain Layer
- [ ] Application Layer
- [ ] Infrastructure Layer
- [ ] WebAPI Layer
- [ ] Frontend
- [ ] CICD
