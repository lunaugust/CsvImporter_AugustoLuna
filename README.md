# CsvImporter

CsvImporter it's a tool that will help you to process more than a million CSV rows into a database in a matter of minutes.

## Installation

We can run this locally or as a WebJob in Azure.

## Configuration
In appsettings.json we can find this two parameters that we can configure.
We can set Url with the path where we have the CSV file that we need to import.
We can choose BatchSize to set how many rows will be inserted at the same time to improve performance.
```json
{
  "Url": "https://domain.com",
  "BatchSize": "10000"
}
```

## Description
For this tool I've used .Net Core Service Worker with dependency injection, Entity Framework Core and Migrations, EFCore.BulkExtensions for optimal batch database insertions, Serilog, Automapper, CsvHelper, NUnit and Moq for unit testing.

## License
[MIT](https://choosealicense.com/licenses/mit/)