using Microsoft.AspNetCore.Mvc;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SmartTradeAdvisor.ChartDrawer.Services;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace SmartTradeAdvisor.ChartDrawer.Controllers;

[ApiController]
[Route("[controller]")]
public class ChartController(IDataService dataService) : ControllerBase
{

    [HttpGet("{index}/{strategy}/{months}")]
    public async Task<IActionResult> GetChart(string index, string strategy, int months)
    {
        try
        {
            var values = await dataService.GetIndexValuesAsync(index, months);
            var transactions = await dataService.GetTransactionsAsync(index, strategy, months);

            var plotModel = new PlotModel { Title = $"{index} - {strategy}" };

            var dateAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd",
                Title = "Date"
            };

            var priceAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Price",
                Key = "PriceAxis",
                StringFormat = "$0.00",
                Minimum = 0
            };

            var walletAxis = new LinearAxis
            {
                Position = AxisPosition.Right,
                Title = "Wallet value",
                Key = "WalletValue",
                StringFormat = "$0.00",
            };

            plotModel.Axes.Add(dateAxis);
            plotModel.Axes.Add(priceAxis);
            plotModel.Axes.Add(walletAxis);

            // Legend
            plotModel.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.BottomCenter,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Horizontal
            });

            // Price line
            var lineSeries = new LineSeries
            {
                YAxisKey = "PriceAxis",
                Title = "Price",
                Color = OxyColors.Orange
            };

            var walletLineSeries = new LineSeries
            {
                YAxisKey = "WalletValue",
                Title = "Wallet value",
                Color = OxyColors.Blue
            };
            foreach (var value in values)
            {
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(value.Date), value.Price));
            }

            plotModel.Series.Add(lineSeries);


            var greenScatterSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Green,
                MarkerSize = 4,
                YAxisKey = "PriceAxis",
                Title = "Buy Transactions"
            };

            var redScatterSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Red,
                MarkerSize = 4,
                YAxisKey = "PriceAxis",
                Title = "Sell Transactions"
            };


            double money = 1000;
            double unitsCount = 0;
            double currentValue = money;
            for (var day = values.First().Date; day.Date <= values.Last().Date; day = day.AddDays(1))
            {
                var value = values.FirstOrDefault(v => v.Date == day);
                var transaction = transactions.FirstOrDefault(v => v.Date == day);
                if (value is not null && transaction is not null)
                {
                    var newMoney = transaction.Seal ? Math.Max(money, unitsCount * value.Price) : 0;
                    var newUnits = !transaction.Seal ? Math.Max(unitsCount, money / value.Price) : 0;
                    money = newMoney;
                    unitsCount = newUnits;
                }
                if (value is not null)
                {
                    currentValue = Math.Max(money, unitsCount * value.Price);
                }
                walletLineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(day), currentValue));

            }
            foreach (var transaction in transactions)
            {
                var price = values.First(v => v.Date == transaction.Date).Price;
                if (transaction.Seal)
                {
                    redScatterSeries.Points.Add(new ScatterPoint(DateTimeAxis.ToDouble(transaction.Date), price));
                }
                else
                {
                    greenScatterSeries.Points.Add(new ScatterPoint(DateTimeAxis.ToDouble(transaction.Date), price));
                }
            }
            plotModel.Series.Add(walletLineSeries);
            plotModel.Series.Add(greenScatterSeries);
            plotModel.Series.Add(redScatterSeries);

            // Annotations
            var lastValue1 = walletLineSeries.Points.Last().Y;
            var lastDate1 = walletLineSeries.Points.Last().X;
            var annotation1 = new TextAnnotation
            {
                Text = $"${lastValue1:F2}",
                TextPosition = new DataPoint(lastDate1, lastValue1),
                Stroke = OxyColors.Transparent,
                TextHorizontalAlignment = HorizontalAlignment.Left,
                TextVerticalAlignment = VerticalAlignment.Bottom
            };
            plotModel.Annotations.Add(annotation1);

            var lastValue2 = lineSeries.Points.Last().Y;
            var lastDate2 = lineSeries.Points.Last().X;
            var annotation2 = new TextAnnotation
            {
                Text = $"${lastValue2:F2}",
                TextPosition = new DataPoint(lastDate2, lastValue2),
                Stroke = OxyColors.Transparent,
                TextHorizontalAlignment = HorizontalAlignment.Left,
                TextVerticalAlignment = VerticalAlignment.Bottom
            };
            plotModel.Annotations.Add(annotation2);

            using (var stream = new MemoryStream())
            {
                var pngExporter = new PngExporter { Width = 1200, Height = 800 };
                pngExporter.Export(plotModel, stream);
                return File(stream.ToArray(), "image/png");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
}
