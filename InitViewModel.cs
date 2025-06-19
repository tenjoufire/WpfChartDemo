using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

namespace WpfChartDemo
{
    public class InitViewModel
    {
        // Age-based Game Users (Bar Chart)
        public ISeries[] AgeUsersSeries { get; set; } = [
            new ColumnSeries<int>
            {
                Name = "男性ユーザー",
                Values = new int[] { 1200, 2100, 1800, 1500, 800, 400, 200 }
            },
            new ColumnSeries<int>
            {
                Name = "女性ユーザー",
                Values = new int[] { 800, 1600, 1400, 1200, 600, 300, 150 }
            }
        ];

        public string[] AgeLabels { get; set; } = ["10代", "20代", "30代", "40代", "50代", "60代", "70代以上"];

        // Monthly Sales (Line Chart)
        public ISeries[] MonthlySalesSeries { get; set; } = [
            new LineSeries<double>
            {
                Name = "売り上げ",
                Values = new double[] { 450, 520, 380, 600, 750, 820, 900, 650, 580, 720, 680, 950 }
            }
        ];

        public string[] MonthLabels { get; set; } = ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"];

        // Country Sales Heat Map (using horizontal bar chart to represent world data)
        public ISeries[] CountrySalesSeries { get; set; } = [
            new RowSeries<double>
            {
                Name = "売り上げ (万円)",
                Values = new double[] { 850, 1200, 1500, 450, 320, 280, 250, 180, 150, 220 }
            }
        ];

        public string[] CountryLabels { get; set; } = ["日本", "米国", "中国", "韓国", "ドイツ", "英国", "フランス", "カナダ", "豪州", "ブラジル"];

        // Legacy property for backward compatibility (not used in updated UI)
        [Obsolete("Use specific chart series instead")]
        public ISeries[] Series { get; set; } = [
            new ColumnSeries<int>(3, 4, 2),
            new ColumnSeries<int>(4, 2, 6),
            new ColumnSeries<double, DiamondGeometry>(4, 3, 4)
        ];
    }
}
