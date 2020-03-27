namespace XamarinWeatherApp.Models
{
    public class Datum
    {
        public int time { get; set; }
        public double precipIntensity { get; set; }
        public double precipIntensityError { get; set; }
        public double precipProbability { get; set; }
        public string precipType { get; set; }
    }
}