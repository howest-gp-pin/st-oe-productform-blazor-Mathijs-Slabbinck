# Done — Products Page Implementation

## Step 1: ProductComponent + ProductListComponent

These two components handle **displaying** products. They mirror the existing `CategoryComponent` + `CategoryListComponent` pattern.

### `ProductComponent.razor` — single product card

```razor
<div class="col col-lg-3 border border-1 rounded rounded-2 text-center m-1 p-2">
```
- A Bootstrap column card (`col-lg-3` because products show more info than categories)
- `border`, `rounded`, `text-center`, `m-1 p-2` for styling

**Displays 4 fields:**
- `@Product.Title` in bold — the product name
- `@Product.Description` — product description
- `Price: @Product.Price` — the price
- `Category: @Product.Category?.Name` — the category name (with `?.` null-safe because a product might not have a category loaded)

**Two buttons:**
- **Edit** — calls `OnEdit.InvokeAsync(Product)`, passing the entire `ProductModel` up to the parent so it can populate the edit form
- **Delete** — calls `OnDelete.InvokeAsync(Product.Id)`, passing just the ID since that's all the API needs

**Three `[Parameter]` properties:**
- `Product` — the `ProductModel` to display (passed down from parent)
- `OnDelete` — `EventCallback<int>` — the parent's delete handler
- `OnEdit` — `EventCallback<ProductModel>` — the parent's edit handler

### `ProductListComponent.razor` — list wrapper

**Top row:** A "New" button that calls `OnAdd.InvokeAsync()` — tells the parent to show the form in create mode.

**Second row:** A `@foreach` loop over `Products` that renders a `ProductComponent` for each one:
```razor
<ProductComponent @key="product.Id" Product="product" OnDelete="OnDelete" OnEdit="OnEdit">
```
- `@key="product.Id"` — tells Blazor's diffing algorithm to match components by product ID. Without this, if a product is deleted from the middle of the list, Blazor might reuse the wrong component instances and show stale data.
- Passes the callbacks straight through — this component doesn't handle events itself, it just relays them to the parent page.

**Four `[Parameter]` properties:**
- `Products` — `IEnumerable<ProductModel>` — the full list
- `OnAdd` — `EventCallback` — new button clicked
- `OnDelete` — `EventCallback<int>` — relayed from `ProductComponent`
- `OnEdit` — `EventCallback<ProductModel>` — relayed from `ProductComponent`

---

## Step 2: ProductFormComponent

This handles **creating and editing** products. Mirrors `CategoryFormComponent`.

### The `EditForm`

```razor
<EditForm Model="NewProduct" OnValidSubmit="@(() => OnSave.InvokeAsync(NewProduct))">
```
- `Model="NewProduct"` — binds the form to the `CreateOrUpdateProductModel` instance
- `OnValidSubmit` — only fires when all validation passes. Calls `OnSave` passing the model back to the parent page
- `<DataAnnotationsValidator />` — activates the `[Required]` and `[Range]` attributes on the model (added in step 4)

### Four form fields

Each follows the same pattern:
```razor
<div class="form-group">
    <label>Title:</label>
    <InputText @bind-Value="NewProduct.Title" class="form-control" />
    <ValidationMessage For="@(() => NewProduct.Title)" />
</div>
```
- **label** — tells the user what the field is
- **InputText / InputNumber** — Blazor's built-in form components that support two-way binding (`@bind-Value`) and validation
- **ValidationMessage** — shows the error message from DataAnnotations when validation fails

|    Field    |   Component   |                    Why                     |
| :---------: | :-----------: | :----------------------------------------: |
| Title       | `InputText`   | Text string                                |
| Price       | `InputNumber` | Integer value                              |
| Description | `InputText`   | Text string                                |
| Category ID | `InputNumber` | Integer — the API expects a category ID    |

### Two buttons
- **Save** (`type="submit"`) — triggers form validation, then `OnValidSubmit`
- **Cancel** (`type="button"`) — calls `OnCancel` to hide the form without saving. `type="button"` is important — without it, clicking Cancel would submit the form

### Three `[Parameter]` properties
- `NewProduct` — the model instance (created by the parent page)
- `OnSave` — `EventCallback<CreateOrUpdateProductModel>` — parent handles the API call
- `OnCancel` — `EventCallback` — parent hides the form

---

## Step 3: Products.razor (the page)

This is the **orchestrator** — it manages state and connects the components to the API service. Route: `/products`.

### Dependency injection
```razor
@inject IProductApiService ProductService
```
Gets the `ProductApiService` registered in `Program.cs`. This service makes HTTP calls to the external API.

### State variables
```csharp
private string? errorMessage;      // red alert text
private string? message;           // blue info text
private CreateOrUpdateProductModel? newProduct;  // null = show list, not null = show form
private IEnumerable<ProductModel>? productModels; // null = loading, not null = loaded
```
The key insight: **`newProduct` controls which view is shown.** When it's `null`, the list shows. When it has a value, the form shows.

### Template logic (top to bottom)

1. **Error alert** — if `errorMessage` is not null, show a red `AlertComponent`
2. **Success alert** — if `message` is not null, show a blue `AlertComponent`
3. **Loading** — if `productModels` is null, show `LoadingComponent` (spinner)
4. **Form or list** — if `newProduct` is not null, show `ProductFormComponent`. Otherwise show `ProductListComponent`

### `OnInitializedAsync` — lifecycle
```csharp
protected override async Task OnInitializedAsync()
{
    await GetProducts();
}
```
Called once when the page loads. Fetches the product list.

### `GetProducts` — load data
```csharp
errorMessage = null;
productModels = null;  // triggers LoadingComponent
var result = await ProductService.GetAllAsync();
```
- Clears errors and sets `productModels` to null (shows the loading spinner)
- Calls the API via `ProductService.GetAllAsync()`
- On success: sets `productModels` to the data (hides spinner, shows list)
- On failure: joins all error messages into `errorMessage` (shows red alert)

### `NewProduct` — create mode
```csharp
private void NewProduct()
{
    errorMessage = null;
    newProduct = new();
}
```
Creates an empty `CreateOrUpdateProductModel`. Since `newProduct` is now not null, the template shows the form instead of the list. All fields are default (Id = 0, strings = null, Price = 0).

### `Save` — create or update
```csharp
if (createOrUpdateProductModel.Id.Equals(0))
    result = await ProductService.CreateAsync(createOrUpdateProductModel);
else
    result = await ProductService.UpdateAsync(createOrUpdateProductModel);
```
- **Id == 0** means it's a new product (never saved to the API) -> `CreateAsync` (POST)
- **Id != 0** means it's an existing product being edited -> `UpdateAsync` (PUT)
- On success: shows "Product created" or "Product updated"
- On failure: shows the API error messages
- Always reloads the list with `await GetProducts()` and hides the form with `newProduct = null`

### `Cancel` — hide form
```csharp
newProduct = null;
```
Sets `newProduct` back to null -> template shows the list again. No API call needed.

### `Delete` — remove product
```csharp
if (await ProductService.DeleteAsync(id))
    message = "Product deleted";
else
    errorMessage = "Product not deleted!";
await GetProducts();
```
Calls the API to delete by ID. Shows success or error message. Reloads the list.

### `Edit` — edit mode
```csharp
newProduct = new();
newProduct.Id = productModel.Id;
newProduct.Title = productModel.Title;
newProduct.Price = productModel.Price;
newProduct.Description = productModel.Description;
newProduct.CategoryId = productModel.Category?.Id ?? 0;
newProduct.Images = productModel.Images;
```
Creates a new `CreateOrUpdateProductModel` and copies all fields from the `ProductModel`. This is necessary because the form model (`CreateOrUpdateProductModel`) has different properties than the display model (`ProductModel`) — for example, the form needs `CategoryId` (an int) while the display model has `Category` (a full `CategoryModel` object). The `?.Id ?? 0` handles the case where a product has no category.

Since `newProduct` is now not null **and** has `Id != 0`, the form shows pre-filled. When saved, the `Save` method sees `Id != 0` and sends a PUT request.

---

## Step 4: Validation on CreateOrUpdateProductModel

Added `System.ComponentModel.DataAnnotations` attributes:

|  Property   |            Attribute             |                        Why                         |
| :---------: | :------------------------------: | :------------------------------------------------: |
| Title       | `[Required]`                     | Can't create a product without a name              |
| Price       | `[Range(1, int.MaxValue)]`       | Price must be at least 1 (0 or negative is invalid) |
| Description | `[Required]`                     | API expects a description                          |
| CategoryId  | `[Range(1, int.MaxValue)]`       | Must select a valid category (0 = none selected)   |
| Images      | *(no validation)*                | Optional — same as Category's Image field          |

These work because `ProductFormComponent` has `<DataAnnotationsValidator />`. When the user clicks Save, Blazor checks all attributes. If any fail, the `<ValidationMessage>` elements show the error text and `OnValidSubmit` does **not** fire — the form is blocked until all fields are valid.

---

## Bonus: AlertComponent rename

Renamed the `Style` parameter to `ComponentStyle` in `AlertComponent.razor` and updated both `Products.razor` and `Categories.razor`. This avoids a conflict with the HTML `style` attribute — Blazor could confuse the two.
