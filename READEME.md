# Backend (API)

To get the backend up and running:

```sh
dotnet restore         # Restore dependencies
dotnet ef database update   # Apply database migrations
dotnet run --seed      # Run the API with seed data
```

# Frontend

To start the frontend development server:

```sh
npm install       # Install dependencies
npm run dev       # Start development server
```