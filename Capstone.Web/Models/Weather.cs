using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {

        public string parkCode { get; set; }
        public int fiveDayForecastValue { get; set; }
        public int low { get; set; }
        public int high { get; set; }
        public int LowCelcius
        {
            get
            {
                return (low - 32) * (5 / 9);
            }
        }
        public int HighCelcius
        {
            get
            {
                return (high - 32) * (5 / 9);
            }
        }
        public string forecast;
        //TODO recommendations 
        
        public List<string> Recommendations { get; set; }
        public string tempUnit { get; set; }
        public int getLow
        {
            get
            {
                if(tempUnit == "c")
                {
                    return LowCelcius;
                }
                else
                {
                    return low;
                }
            }
        }
        public int getHigh
        {
            get
            {
                if (tempUnit == "c")
                {
                    return HighCelcius;
                }
                else
                {
                    return high;
                }
            }
        }


        //will this account for degrees in celsius?  low and high refer to 
        public List<string> AddRecommendations()
        {
            List<string> recommendations = new List<string>();
            if (low < 20)
            {
                recommendations.Add("WARNING: Prepare against exposure to frigid temperatures.");
            }
            if (high - low > 20)
            {
                recommendations.Add("Wear Breathable Layers");
            }
            if (high > 75)
            {
                recommendations.Add("Bring Extra Gallon Of Water");
            }
            switch (forecast)
            {
                case "rain":
                    recommendations.Add("pack rain gear and wear waterproof shoes");
                    break;
                case "snow":
                    recommendations.Add("pack snowshoes");
                    break;
                case "sunny":
                    recommendations.Add("pack sunblock");
                    break;
                case "thunderstorms":
                    recommendations.Add("seek shelter and avoid hiking on exposed ridges");
                    break;
            }
            return recommendations;
        }
    }
}
