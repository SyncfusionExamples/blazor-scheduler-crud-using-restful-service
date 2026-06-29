# How to Integrate Blazor Scheduler with OData V4 using RESTful Services

A Blazor application demonstrating how to perform appointment management (create, read, update, and delete operations) using RESTful web services with the [Blazor Scheduler](https://www.syncfusion.com/blazor-components/blazor-scheduler) component and OData V4 integration.

## Features

- Full CRUD operations on scheduler events using OData V4
- Microsoft SQL Server database integration via Entity Framework Core
- RESTful backend service for appointment management
- Real-time synchronization between frontend and backend
- Support for event timezones, recurrence rules, and event properties

## Prerequisites

* Visual Studio 2022
* .NET 10.0
* SQL Server LocalDB or full SQL Server instance

## Getting Started

To run this application, clone the `blazor-scheduler-crud` repository and navigate to its path. Open the solution in Visual Studio 2019 to run both the backend service and frontend scheduler sample. Make sure both projects are running simultaneously.

## Project Structure

- **Restful_Services**: ASP.NET Core backend with OData V4 controller for CRUD operations
- **SchedulerCRUD**: Blazor frontend with SfSchedule component using SfDataManager

## How to run the project

1. Checkout this project to your local machine
2. Open the solution file using Visual Studio 2022
3. Build the solution to restore the required NuGet packages
4. Press `F5` to start the application

## Documentation & Resources

- [Blazor Introduction](https://blazor.syncfusion.com/documentation/introduction)
- [Syncfusion Blazor Schedule Documentation](https://blazor.syncfusion.com/documentation/schedule/getting-started)
- [OData V4 Integration Guide](https://help.syncfusion.com/)

## Creating DBContext class

The first step is to create a DBContext class called **ScheduleDataContext** to connect to a Microsoft SQL Server database.

```csharp
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restful_Services.Models
{
    public partial class ScheduleDataContext : DbContext
    {
        public ScheduleDataContext()
        {
        }

        public ScheduleDataContext(DbContextOptions<ScheduleDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventData> EventData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\SchedulerCRUD\\Restful_Services\\App_Data\\ScheduleData.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework;Integrated Security=True"); // Here you can provide your path
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventData>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.EndTime).HasColumnType("datetime");
                entity.Property(e => e.RecurrenceID).HasColumnName("RecurrenceID");
                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
```

## Creating OData Controller

An OData Controller has to be created which allows Scheduler to directly consume data from the Entity Framework. The following code example shows how to perform CRUD operations using Entity Framework.

```csharp
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using Microsoft.AspNet.OData;
using Restful_Services.Models;

namespace Restful_Services.Controllers
{
    public class ODataV4Controller : ODataController
    {
        private ScheduleDataContext db = new ScheduleDataContext();

        // GET: odata/ODataV4
        [EnableQuery]
        [AcceptVerbs("GET")]
        public IQueryable<EventData> GetODataV4()
        {
            return db.EventData;
        }

        // GET: odata/ODataV4(5)
        [EnableQuery]
        [AcceptVerbs("GET")]
        public IQueryable<EventData> GetODataV4(string StartDate, string EndDate)
        {
            DateTime start = DateTime.Parse(StartDate);
            DateTime end = DateTime.Parse(EndDate);
            return db.EventData.Where(evt => evt.StartTime >= start && evt.EndTime <= end);
        }

        // POST: odata/ODataV4
        [AcceptVerbs("POST", "OPTIONS")]
        public void Post([FromBody]EventData eventData)
        {
            if (ModelState.IsValid)
            {
                EventData insertData = new EventData();
                insertData.Id = (db.EventData.ToList().Count > 0 ? db.EventData.ToList().Max(p => p.Id) : 1) + 1;
                insertData.Subject = eventData.Subject;
                insertData.StartTime = Convert.ToDateTime(eventData.StartTime);
                insertData.EndTime = Convert.ToDateTime(eventData.EndTime);
                insertData.StartTimezone = eventData.StartTimezone;
                insertData.EndTimezone = eventData.EndTimezone;
                insertData.Location = eventData.Location;
                insertData.Description = eventData.Description;
                insertData.IsAllDay = eventData.IsAllDay;
                insertData.IsBlock = eventData.IsBlock;
                insertData.IsReadOnly = eventData.IsReadOnly;
                insertData.FollowingID = eventData.FollowingID;
                insertData.RecurrenceID = eventData.RecurrenceID;
                insertData.RecurrenceRule = eventData.RecurrenceRule;
                insertData.RecurrenceException = eventData.RecurrenceException;
                db.EventData.Add(insertData);
                db.SaveChanges();
            }
        }

        // PATCH: odata/ODataV4(5)
        [AcceptVerbs("PATCH", "MERGE", "OPTIONS")]
        public void Patch([FromBody]EventData eventData)
        {
            if (ModelState.IsValid)
            {
                EventData updateData = db.EventData.First(i => i.Id == Convert.ToInt32(eventData.Id));
                if (updateData != null)
                {
                    updateData.Subject = eventData.Subject;
                    updateData.StartTime = Convert.ToDateTime(eventData.StartTime);
                    updateData.EndTime = Convert.ToDateTime(eventData.EndTime);
                    updateData.StartTimezone = eventData.StartTimezone;
                    updateData.EndTimezone = eventData.EndTimezone;
                    updateData.Location = eventData.Location;
                    updateData.Description = eventData.Description;
                    updateData.IsAllDay = eventData.IsAllDay;
                    updateData.IsBlock = eventData.IsBlock;
                    updateData.IsReadOnly = eventData.IsReadOnly;
                    updateData.FollowingID = eventData.FollowingID;
                    updateData.RecurrenceID = eventData.RecurrenceID;
                    updateData.RecurrenceRule = eventData.RecurrenceRule;
                    updateData.RecurrenceException = eventData.RecurrenceException;
                    db.SaveChanges();
                }
            }
        }

        // DELETE: odata/ODataV4(5)
        [AcceptVerbs("DELETE", "OPTIONS")]
        public void Delete([FromODataUri]int key)
        {
            if (ModelState.IsValid)
            {
                EventData removeData = db.EventData.First(i => i.Id == key);
                if (removeData != null)
                {
                    db.EventData.Remove(removeData);
                    db.SaveChanges();
                }
            }
        }
    }
}
```

## Configuring Scheduler Component

Configure the Scheduler using `SfDataManager` to interact with the created OData service and consume the data appropriately. To interact with OData, use the `ODataV4Adaptor`.

```csharp
@using Syncfusion.Blazor
@using Syncfusion.Blazor.Schedule
@using Syncfusion.Blazor.Data

<SfSchedule TValue="Restful_Services.Models.EventData" Height="550px" SelectedDate="@(new DateTime(2020, 04, 14))" >
    <ScheduleEventSettings TValue="Restful_Services.Models.EventData" Query="@QueryData" >
        <SfDataManager Url="http://localhost:9876/odata/" Adaptor="Adaptors.ODataV4Adaptor"></SfDataManager>
    </ScheduleEventSettings>
</SfSchedule>
@code {
    public Query QueryData = new Query().From("ODataV4");
}
```
