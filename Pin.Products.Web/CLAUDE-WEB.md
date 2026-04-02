# Pin.Products.Web Guide

<!-- AUTO-GENERATED: Do not edit between these markers. Run /c-setup-subprojects-l to refresh. -->

## Project Info
- **Type**: Blazor Server (Interactive Server Render Mode)
- **Target Framework**: net8.0
- **SDK**: Microsoft.NET.Sdk.Web
- **Nullable**: enable
- **Implicit Usings**: enable

## Conventions
Read these before working in this subproject:
- `.claude/conventions/CLAUDE_CS.md`
- `.claude/conventions/CLAUDE_DOTNET.md`
- `.claude/conventions/CLAUDE_BLAZOR.md`
- `.claude/conventions/CLAUDE_HTML.md`
- `.claude/conventions/CLAUDE_CSS.md`
- `.claude/conventions/CLAUDE_GIT.md`

## Folder Structure
```
Components/
  Layout/            # MainLayout, NavMenu (with scoped CSS)
  Pages/             # Routable pages — Categories, Products, Home, Counter, Weather, Error
  UI/                # Reusable components — Alert, Category, CategoryForm, CategoryList, Loading
  App.razor          # Root component
  Routes.razor       # Router configuration
  _Imports.razor     # Global using directives for Razor
Properties/
  launchSettings.json
wwwroot/
  bootstrap/         # Bootstrap CSS (bundled)
  app.css            # Global app styles
  favicon.png
```

## Key Files

|                File                 |                              Purpose                               |
| :---------------------------------: | :----------------------------------------------------------------: |
| Pin.Products.Web.csproj            | Project file — Blazor Server, net8.0, references Pin.Products.Core |
| Program.cs                          | App entry point — DI registration, middleware, render mode config   |
| Components/App.razor                | Root component with HTML head/body structure                       |
| Components/Pages/Products.razor     | Products page — CRUD via IProductApiService                        |
| Components/Pages/Categories.razor   | Categories page — CRUD via ICategoryApiService                     |
| Components/UI/CategoryFormComponent.razor | Category create/edit form component                          |
| Components/UI/CategoryListComponent.razor | Category list display component                              |
| Components/UI/AlertComponent.razor  | Reusable alert/notification component                              |
| Components/UI/LoadingComponent.razor | Loading spinner component                                         |
| appsettings.json                    | App configuration                                                  |

## Dependencies
- **Pin.Products.Core** — project reference for API services and models
- No additional NuGet packages beyond the default ASP.NET Core Blazor stack

## Patterns
- **Interactive Server Render Mode**: `AddInteractiveServerComponents()` + `AddInteractiveServerRenderMode()` in Program.cs
- **DI registration**: `HttpClient` via `AddHttpClient()`, services as `AddScoped<IService, Implementation>()`
- **Component structure**: Pages in `Components/Pages/`, reusable UI in `Components/UI/`
- **Scoped CSS**: `MainLayout.razor.css`, `NavMenu.razor.css` for component-level styles
- **Bootstrap**: bundled in `wwwroot/bootstrap/` (not via CDN)

<!-- END AUTO-GENERATED -->

## Notes
<!-- Add project-specific notes, patterns, or conventions below. This section is preserved when running /c-setup-subprojects-l. -->
