# Pin.Products.Core Guide

<!-- AUTO-GENERATED: Do not edit between these markers. Run /c-setup-subprojects-l to refresh. -->

## Project Info
- **Type**: Class Library
- **Target Framework**: net8.0
- **SDK**: Microsoft.NET.Sdk
- **Nullable**: enable
- **Implicit Usings**: enable

## Conventions
Read these before working in this subproject:
- `.claude/conventions/CLAUDE_CS.md`
- `.claude/conventions/CLAUDE_DOTNET.md`
- `.claude/conventions/CLAUDE_GIT.md`

## Folder Structure
```
Services/
  Interfaces/        # Service contracts (ICategoryApiService, IProductApiService)
  Models/            # DTOs and result wrapper (ProductModel, CategoryModel, ResultModel, Create/Update models)
  CategoryApiService.cs
  ProductApiService.cs
```

## Key Files

|               File                |                         Purpose                          |
| :-------------------------------: | :------------------------------------------------------: |
| Pin.Products.Core.csproj          | Project file — class library, net8.0                     |
| Services/ProductApiService.cs     | HTTP client calls to external product API                |
| Services/CategoryApiService.cs    | HTTP client calls to external category API               |
| Services/Models/ResultModel.cs    | Generic result wrapper with `IsSuccess` and `Errors`     |
| Services/Models/ProductModel.cs   | Product DTO with JSON serialization attributes           |
| Services/Models/CategoryModel.cs  | Category DTO with JSON serialization attributes          |

## Dependencies
- No NuGet packages — uses only `System.Net.Http.Json` and `System.Text.Json` (built-in)
- No project references — this is the bottom of the dependency chain

## Patterns
- **Generic result wrapper**: `ResultModel<T>` with `IsSuccess` (computed from `Errors.Any()`) and `Data` property
- **Interface-first services**: `IProductApiService` / `ICategoryApiService` in `Interfaces/`, implementations in parent folder
- **HttpClient injection**: services receive `HttpClient` via constructor, set `BaseAddress` to external API (`https://api.escuelajs.co/api/v1/`)
- **JSON serialization**: models use `[JsonPropertyName]` attributes for API mapping
- **CRUD pattern**: each service exposes `GetAllAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync`

<!-- END AUTO-GENERATED -->

## Notes
<!-- Add project-specific notes, patterns, or conventions below. This section is preserved when running /c-setup-subprojects-l. -->
