#### Add migrations

```
cd Quotation.Infrastructure
dotnet ef migrations add {branchName}  --startup-project ../Quotation.API --context QuotationContext  
```

Example: ```dotnet ef migrations add Initial --startup-project ..\Quotation.API\ --context QuotationContext```

#### Update database

```
cd Quotation.Infrastructure
dotnet ef database update --startup-project ../Quotation.API --context QuotationContext  
```

#### Remove last migrations

```
cd Quotation.Infrastructure
dotnet ef migrations remove --startup-project ../Quotation.API --context QuotationContext  -f
```

#### Generate migration script

```
cd Quotation.Infrastructure
dotnet ef migrations script --startup-project ../Quotation.API --context QuotationContext -o .\Scripts\{DateGenerateScriptMigration}_{branchName}.sql
```