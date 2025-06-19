using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace WpfChartDemo
{
    public class InitViewModel
    {
        // Sample data for game user analysis
        private readonly List<GameUserData> _sampleData = new()
        {
            new GameUserData { Month = "2023-01", Age = 56, Gender = "Other", Country = "Japan", AmountSpent = 5755.21 },
            new GameUserData { Month = "2023-01", Age = 15, Gender = "Male", Country = "Germany", AmountSpent = 2137.56 },
            new GameUserData { Month = "2023-01", Age = 58, Gender = "Other", Country = "France", AmountSpent = 4633.8 },
            new GameUserData { Month = "2023-01", Age = 33, Gender = "Male", Country = "India", AmountSpent = 10719.95 },
            new GameUserData { Month = "2023-02", Age = 28, Gender = "Female", Country = "Japan", AmountSpent = 8755.33 },
            new GameUserData { Month = "2023-02", Age = 42, Gender = "Male", Country = "Germany", AmountSpent = 3445.78 },
            new GameUserData { Month = "2023-02", Age = 35, Gender = "Female", Country = "France", AmountSpent = 6234.55 },
            new GameUserData { Month = "2023-02", Age = 29, Gender = "Male", Country = "India", AmountSpent = 9876.43 },
            new GameUserData { Month = "2023-03", Age = 31, Gender = "Male", Country = "Japan", AmountSpent = 7234.67 },
            new GameUserData { Month = "2023-03", Age = 26, Gender = "Female", Country = "Germany", AmountSpent = 4556.89 },
            new GameUserData { Month = "2023-03", Age = 40, Gender = "Female", Country = "France", AmountSpent = 5678.34 },
            new GameUserData { Month = "2023-03", Age = 38, Gender = "Male", Country = "India", AmountSpent = 8934.21 }
        };

        // Line chart series for monthly sales by gender
        public ISeries[] LineSeries { get; set; }

        // Bar chart series for monthly spending by country
        public ISeries[] BarSeries { get; set; }

        // X-axis configuration for bar chart
        public Axis[] XAxes { get; set; }

        // Y-axis configuration for charts
        public Axis[] YAxes { get; set; }

        // X-axis configuration for line chart
        public Axis[] LineXAxes { get; set; }

        // Title for line chart
        public LabelVisual LineChartTitle { get; set; }

        // Title for bar chart
        public LabelVisual BarChartTitle { get; set; }

        public InitViewModel()
        {
            SetupLineChart();
            SetupBarChart();
        }

        private void SetupLineChart()
        {
            // Group data by month and gender, calculate total sales
            var monthlySalesByGender = _sampleData
                .GroupBy(d => new { d.Month, d.Gender })
                .Select(g => new { Month = g.Key.Month, Gender = g.Key.Gender, TotalSales = g.Sum(x => x.AmountSpent) })
                .ToList();

            var months = _sampleData.Select(d => d.Month).Distinct().OrderBy(m => m).ToList();
            var maleData = new List<double>();
            var femaleData = new List<double>();

            foreach (var month in months)
            {
                var maleTotal = monthlySalesByGender
                    .Where(x => x.Month == month && x.Gender == "Male")
                    .Sum(x => x.TotalSales);
                var femaleTotal = monthlySalesByGender
                    .Where(x => x.Month == month && x.Gender == "Female")
                    .Sum(x => x.TotalSales);

                maleData.Add(maleTotal);
                femaleData.Add(femaleTotal);
            }

            LineSeries = new ISeries[]
            {
                new LineSeries<double>
                {
                    Name = "Male",
                    Values = maleData,
                    Fill = null,
                    GeometrySize = 10,
                    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 3 }
                },
                new LineSeries<double>
                {
                    Name = "Female", 
                    Values = femaleData,
                    Fill = null,
                    GeometrySize = 10,
                    Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 3 }
                }
            };

            LineChartTitle = new LabelVisual
            {
                Text = "Monthly Sales by Gender (JPY)",
                TextSize = 20,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            };

            // X-axis for line chart
            LineXAxes = new Axis[]
            {
                new Axis
                {
                    Labels = months,
                    Name = "Month",
                    NameTextSize = 14,
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                    TicksAtCenter = true,
                    ForceStepToMin = true,
                    MinStep = 1
                }
            };
        }

        private void SetupBarChart()
        {
            // Group data by month and country, calculate total spending
            var monthlySpendingByCountry = _sampleData
                .GroupBy(d => new { d.Month, d.Country })
                .Select(g => new { Month = g.Key.Month, Country = g.Key.Country, TotalSpending = g.Sum(x => x.AmountSpent) })
                .ToList();

            var months = _sampleData.Select(d => d.Month).Distinct().OrderBy(m => m).ToList();
            var countries = _sampleData.Select(d => d.Country).Distinct().ToList();

            var seriesList = new List<ISeries>();

            foreach (var country in countries)
            {
                var countryData = new List<double>();
                foreach (var month in months)
                {
                    var spending = monthlySpendingByCountry
                        .Where(x => x.Month == month && x.Country == country)
                        .Sum(x => x.TotalSpending);
                    countryData.Add(spending);
                }

                seriesList.Add(new ColumnSeries<double>
                {
                    Name = country,
                    Values = countryData
                });
            }

            BarSeries = seriesList.ToArray();

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = months,
                    Name = "Month",
                    NameTextSize = 14,
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                    TicksAtCenter = true,
                    ForceStepToMin = true,
                    MinStep = 1
                }
            };

            BarChartTitle = new LabelVisual
            {
                Text = "Monthly Spending by Country (JPY)",
                TextSize = 20,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            };

            // Y-axis configuration
            YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Amount (JPY)",
                    NameTextSize = 14,
                    LabelsPaint = new SolidColorPaint(new SKColor(90, 90, 90)),
                    NamePaint = new SolidColorPaint(new SKColor(35, 35, 35))
                }
            };
        }

        // Keep the original series for backward compatibility
        public ISeries[] Series { get; set; } = [
            new ColumnSeries<int>(3, 4, 2),
            new ColumnSeries<int>(4, 2, 6),
            new ColumnSeries<double, DiamondGeometry>(4, 3, 4)
        ];
    }
}
