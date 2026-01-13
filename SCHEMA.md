# Schema Design

Object models, properties, and relationships for the Cart application.

## Models

### Category

The type/classification of a grocery item (dairy, produce, pasta, condiment, etc).

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| Id | int | Yes | Primary key |
| Name | string | Yes | Category name (e.g., "Dairy", "Produce", "Condiments") |

**Relationships:**
- One-to-many with `Grocery` (a category has many groceries)
- Many-to-many with `Aisle` via `CategoryAisleMapping` (a category can appear in multiple aisles per store)

---

### Grocery

The master dictionary of groceries that can be added to shopping lists.

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| Id | int | Yes      | Primary key |
| Name | string | Yes      | Grocery name (e.g., "Milk", "Bread", "Eggs") |
| CategoryId | int | No       | Foreign key to Category |

**Relationships:**
- Many-to-one with `Category` (each grocery belongs to one category)
- One-to-many with `Item` (a grocery can appear on shopping lists as items)
- Groceries can still be created without a category and appear on a list

---

### Item

An entry on the current shopping list. Represents a grocery with quantity and completion status.

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| Id | int | Yes | Primary key |
| GroceryId | int | Yes | Foreign key to Grocery |
| Quantity | int | Yes | Number needed (default: 1) |
| IsCompleted | bool | Yes | Whether item has been picked up (default: false) |

**Relationships:**
- Many-to-one with `Grocery` (each item references one grocery)

**Notes:**
- Items marked as `IsCompleted = true` remain in the list until explicitly purged/cleared
- The same grocery can appear multiple times as separate items if needed

---

### Store

A shopping location with aisle/department configuration.

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| Id | int | Yes | Primary key |
| Name | string | Yes | Store name (e.g., "Kroger", "Whole Foods") |
| Address | string | No | Store address |

**Relationships:**
- One-to-many with `Aisle` (a store has many aisles)
- One-to-many with `CategoryAisleMapping` (a store has category-to-aisle mappings)

---

### Aisle

A location within a store. Not limited to numbered aisles - can include departments like "Deli" or "Produce".

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| Id | int | Yes | Primary key |
| StoreId | int | Yes | Foreign key to Store |
| Name | string | Yes | Aisle/location name (e.g., "Aisle 11", "Deli", "Produce") |
| SortOrder | int | Yes | Display order when organizing shopping list |

**Relationships:**
- Many-to-one with `Store` (each aisle belongs to one store)
- Many-to-many with `Category` via `CategoryAisleMapping`

**Notes:**
- `SortOrder` enables manual ordering of aisles per store (the order you walk through the store)

---

### CategoryAisleMapping

Maps categories to aisles for a specific store. Enables organizing items by store layout.

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| Id | int | Yes | Primary key |
| StoreId | int | Yes | Foreign key to Store |
| CategoryId | int | Yes | Foreign key to Category |
| AisleId | int | Yes | Foreign key to Aisle |

**Relationships:**
- Many-to-one with `Store`
- Many-to-one with `Category`
- Many-to-one with `Aisle`

**Constraints:**
- Unique on (StoreId, CategoryId, AisleId) - prevents duplicate mappings

**Notes:**
- A category can map to multiple aisles in the same store (e.g., "Beverages" in both Aisle 5 and Aisle 12)
- Categories without mappings for a store display as "Unknown" aisle
- The mapping is store-specific (milk might be in Aisle 3 at Kroger but Aisle 7 at Safeway)

---

## Entity Relationship Diagram

```
┌──────────────┐       ┌──────────────┐
│   Category   │       │    Store     │
├──────────────┤       ├──────────────┤
│ Id           │       │ Id           │
│ Name         │       │ Name         │
└──────┬───────┘       │ Address      │
       │               └──────┬───────┘
       │ 1                    │ 1
       │                      │
       │ *                    │ *
┌──────┴───────┐       ┌──────┴───────┐
│   Grocery    │       │    Aisle     │
├──────────────┤       ├──────────────┤
│ Id           │       │ Id           │
│ Name         │       │ StoreId      │
│ CategoryId   │       │ Name         │
└──────┬───────┘       │ SortOrder    │
       │               └──────────────┘
       │ 1
       │
       │ *             ┌────────────────────┐
┌──────┴───────┐       │ CategoryAisleMapping│
│    Item      │       ├────────────────────┤
├──────────────┤       │ Id                 │
│ Id           │       │ StoreId            │
│ GroceryId    │       │ CategoryId         │
│ Quantity     │       │ AisleId            │
│ IsCompleted  │       └────────────────────┘
└──────────────┘
```

## Usage Scenarios

### Adding an Item to the List
1. User searches/selects a `Grocery` (or creates a new one with a `Category`)
2. System creates an `Item` with `GroceryId`, `Quantity`, and `IsCompleted = false`

### Viewing the List by Store
1. User selects a `Store`
2. For each `Item`, system looks up its `Grocery.CategoryId`
3. System finds `CategoryAisleMapping` for that store + category
4. Items are grouped by `Aisle.Name` and sorted by `Aisle.SortOrder`
5. Items with unmapped or empty categories appear in an "Unknown" group

### Completing Items
1. User checks off an `Item`
2. System sets `IsCompleted = true`
3. Item remains visible until user explicitly clears completed items

### Clearing Completed Items
1. User triggers "Clear Completed" action
2. System deletes all `Item` records where `IsCompleted = true`
