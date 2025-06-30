# CSV Data Loading Feature

This application now supports loading data from CSV files in addition to using the built-in sample data.

## CSV Format

The CSV file should have the following format:

```csv
Month,Age,Gender,Country,AmountSpent
2023-01,25,Male,Japan,1500.50
2023-01,30,Female,Germany,2200.75
2023-02,35,Female,Japan,2500.00
```

### Field Descriptions:
- **Month**: String in YYYY-MM format (e.g., "2023-01")
- **Age**: Integer representing the user's age
- **Gender**: String (e.g., "Male", "Female", "Other")
- **Country**: String representing the country name
- **AmountSpent**: Decimal number representing the amount spent

## How to Use

1. Click the "CSV読み込み" (CSV Load) button in the application
2. Select a CSV file from your local filesystem
3. The charts will automatically refresh with the new data
4. The status message will show the number of records loaded

## Error Handling

- Invalid CSV format will show an error message
- Empty files or files with no valid data will display a warning
- Parse errors for individual rows are skipped (only valid rows are loaded)

## Sample Data

A sample CSV file (`sample_data.csv`) is included for testing purposes.