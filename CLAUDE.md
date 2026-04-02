# Pin.Products — Blazor Product Form

Blazor Server (.NET 8) app for managing products and categories via an external API.

**CONVENTIONS** — MANDATORY: Read the relevant files below before your first Edit or Write. Do not write code without reading these first:
- `.claude/conventions/CLAUDE_GIT.md`
- `.claude/conventions/CLAUDE_CS.md`
- `.claude/conventions/CLAUDE_DOTNET.md`
- `.claude/conventions/CLAUDE_BLAZOR.md`
- `.claude/conventions/CLAUDE_HTML.md`
- `.claude/conventions/CLAUDE_CSS.md`
- `.claude/conventions/CLAUDE_JAVASCRIPT.md`

**REGULATIONS** — MANDATORY: Read the relevant files below before writing code that touches these domains:
- `.claude/regulations/data-privacy-laws-default.md`
- `.claude/regulations/accessibility-default.md`

## Subproject Guides
Before working in a subproject, read its guide file first:

|    Subproject     |              Guide File              |     Type      |
| :---------------: | :----------------------------------: | :-----------: |
| Pin.Products.Core | `Pin.Products.Core/CLAUDE-CORE.md`   | Class Library |
| Pin.Products.Web  | `Pin.Products.Web/CLAUDE-WEB.md`     | Blazor Server |

## Solution Structure

|      Project       |     Type      |                   Description                    |
| :----------------: | :-----------: | :----------------------------------------------: |
| Pin.Products.Web   | Blazor Server | UI — pages, components, layout                   |
| Pin.Products.Core  | Class Library | API service layer — models, interfaces, services |

## Build & Run

```bash
# Build
dotnet build Pin.Products.Web.sln

# Run the web app
dotnet run --project Pin.Products.Web
```

## Project Layout

```
Pin.Products.Core/
  Services/
    Interfaces/          # ICategoryApiService, IProductApiService
    Models/              # BaseResult, ResultModel<T>, CategoryModel, ProductModel,
                         #   CreateOrUpdateCategoryModel, CreateOrUpdateProductModel
    CategoryApiService.cs
    ProductApiService.cs

Pin.Products.Web/
  Components/
    Pages/               # Categories (.razor + .razor.cs), Products (.razor + .razor.cs),
                         #   Home, Counter, Weather, Error
    UI/                  # AlertComponent, CategoryComponent, CategoryFormComponent,
                         #   CategoryListComponent, ProductComponent, ProductFormComponent,
                         #   ProductListComponent, LoadingComponent
    Layout/              # MainLayout, NavMenu
    App.razor
    Routes.razor
    _Imports.razor
  Enums/                 # AlertStyle
  Program.cs
```
