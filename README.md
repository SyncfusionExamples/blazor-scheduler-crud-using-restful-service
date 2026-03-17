# Syncfusion Blazor Scheduler with OData v4 and Entity Framework Core

This [Syncfusion<sup style="font-size:70%">&reg;</sup>  Blazor Scheduler](https://www.syncfusion.com/blazor-components/blazor-scheduler) with an OData v4 service backed by **Entity Framework Core**. The Scheduler fetches events from the [OData](https://www.odata.org/documentation/) API, displays them in a calendar UI, and supports basic CRUD operations that persist to the database.

## Prerequisites

- **.NET SDK 8** or later. Verify with:
    ```bash
    dotnet --version
    ```
- **SQL Server** or **LocalDB**.
- A code editor (Visual Studio / VS Code).

## Setup

### Clone the repository

- Clone or download the repository to your machine:
    ```bash
    git clone <repository-url>
    cd blazor-scheduler-crud-using-restful-service
    ```

## Build and run

1. Restore packages and build the solution:

```bash
dotnet restore
dotnet build
```

2. Run the application from the project root (or open the solution in Visual Studio and run):

```bash
dotnet run
```

3. When the app starts, open the URL shown in the console (for example `https://localhost:5001` or the URL shown by the host).

The Scheduler UI will load and display events provided by the OData controller.

## Output
![Blazor Entity Framework Output](/blazor-scheduler-crud-using-restful-service/wwwroot/blazor-scheduler-EF-Output.png)

## TroubleShooting
### Problem
The Scheduler does not load correctly, or interactive features (drag, drop, resize, edit popup, etc.) do not work.

### Errors
- SfSchedule is not a function
- Scheduler renders but is not interactive

### Solution
Add the Syncfusion script reference inside the <body> of `App.razor`
```razor
<script src="_content/Syncfusion.Blazor.Core/scripts/syncfusion-blazor.min.js"
            type="text/javascript"></script>
```

## See also

- Syncfusion Blazor Scheduler — https://www.syncfusion.com/blazor-components/blazor-scheduler
- Entity Framework Core — https://learn.microsoft.com/ef/core/
- OData v4 documentation — https://www.odata.org/documentation/
- ASP.NET Core documentation — https://learn.microsoft.com/aspnet/core/

---

