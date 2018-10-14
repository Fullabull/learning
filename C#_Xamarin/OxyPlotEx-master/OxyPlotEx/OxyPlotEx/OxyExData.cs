using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace OxyPlotEx
{
    public class OxyExData
    {
        public PlotModel PieModel { get; set; }
        public PlotModel AreaModel { get; set; }
        public PlotModel BarModel { get; set; }
        public PlotModel StackedBarModel { get; set; }

        public OxyExData()
        {
            PieModel = CreatePieChart();
            AreaModel = CreateAreaChart();
            StackedBarModel = CreateBarChart(true, "Stacked Bar Chart");
            BarModel = CreateBarChart(false, "Comparison Bar Chart");
        }
        private PlotModel CreatePieChart()
        {
            var model = new PlotModel { Title = "Pie Chart", };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                AngleSpan = 360,
                StartAngle = 0, 
                FontSize = 10
            };
            
            ps.Slices.Add(new PieSlice("Chrome", 46) { Fill = OxyColor.Parse("#3a17e8") });
            ps.Slices.Add(new PieSlice("Safari", 5){Fill = OxyColor.Parse("#ed0986") });
            ps.Slices.Add(new PieSlice("Edge", 25){Fill = OxyColor.Parse("#09bc27") });
            ps.Slices.Add(new PieSlice("Firefox", 20){Fill = OxyColor.Parse("#edae49") });
            ps.Slices.Add(new PieSlice("Others", 4){Fill = OxyColor.Parse("#2e3dc1") });
            model.Series.Add(ps);
            return model;
        }

        public PlotModel CreateAreaChart()
        {
            var plotModel1 = new PlotModel { Title = "Area Series with crossing lines" };
            var areaSeries1 = new AreaSeries(){Background = OxyColors.Azure};
            areaSeries1.Points.Add(new DataPoint(0, 50));
            areaSeries1.Points.Add(new DataPoint(10, 140));
            areaSeries1.Points.Add(new DataPoint(20, 60));
            areaSeries1.Points2.Add(new DataPoint(0, 60));
            areaSeries1.Points2.Add(new DataPoint(5, 80));
            areaSeries1.Points2.Add(new DataPoint(20, 70));
            plotModel1.Series.Add(areaSeries1);
            return plotModel1;
        }

        private PlotModel CreateBarChart(bool stacked, string title)
        {
            var model = new PlotModel
            {
                Title = title,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            var s1 = new BarSeries { Title = "Cold Drink", IsStacked = stacked,};
            s1.Items.Add(new BarItem { Value = 25 });
            s1.Items.Add(new BarItem { Value = 60 });
            s1.Items.Add(new BarItem { Value = 90 });
            s1.Items.Add(new BarItem { Value = 50 });

            var s2 = new BarSeries { Title = "Tea", IsStacked = stacked, };
            s2.Items.Add(new BarItem { Value = 75 });
            s2.Items.Add(new BarItem { Value = 40 });
            s2.Items.Add(new BarItem { Value = 10 });
            s2.Items.Add(new BarItem { Value = 45 });

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis.Labels.Add("Winter");
            categoryAxis.Labels.Add("Spring");
            categoryAxis.Labels.Add("Summer");
            categoryAxis.Labels.Add("Autumn");
            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            model.Series.Add(s1);
            model.Series.Add(s2);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);
            return model;
        }



    }
}
