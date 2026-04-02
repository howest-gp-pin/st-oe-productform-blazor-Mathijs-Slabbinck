# To Do — Products Page

The Categories side is fully built and serves as the reference pattern. The Products side needs the same treatment.

## Steps

### 1. Create `ProductListComponent.razor`
- New file in `Pin.Products.Web/Components/UI/`
- Mirror `CategoryListComponent.razor` — display products in a list/table with Edit and Delete buttons and an Add button
- Show title, price, description, category name, and image
- Expose `EventCallback` parameters: `OnAdd`, `OnEdit(ProductModel)`, `OnDelete(int)`

### 2. Create `ProductFormComponent.razor`
- New file in `Pin.Products.Web/Components/UI/`
- Mirror `CategoryFormComponent.razor` — form for creating and editing a product
- Fields: Title, Price, Description, CategoryId (dropdown of categories), Images
- Add validation (use `[Required]` etc. on `CreateOrUpdateProductModel` as needed)
- Expose `EventCallback` parameters: `OnSave(CreateOrUpdateProductModel)`, `OnCancel`

### 3. Wire up `Products.razor`
- Call `ProductService.GetAllAsync()` in `OnInitializedAsync` to load products
- Add error/success messaging with `AlertComponent` (same pattern as Categories)
- Show `ProductFormComponent` when adding/editing, `ProductListComponent` otherwise
- Implement handlers: `NewProduct`, `Save`, `Cancel`, `Delete`, `Edit`

### 4. Add validation to `CreateOrUpdateProductModel`
- Add `[Required]` and other `DataAnnotations` attributes (Title, Price, Description, CategoryId)
- Mirror the validation approach used in `CreateOrUpdateCategoryModel`

### 5. Test the full flow
- Build the solution (`dotnet build`)
- Run the app (`dotnet run --project Pin.Products.Web`)
- Verify: list products, create a new product, edit an existing product, delete a product
- Check error handling (disconnect API, submit empty form)
