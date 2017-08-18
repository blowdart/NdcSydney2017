namespace AuthorizationPolicies
{
    public class WeatherProvider : IWeatherProvider
    {
        Season _season = Season.Winter;

        public Season GetSeason()
        {
            return _season;
        }

        public void SetSeason(Season season)
        {
            _season = season;
        }
    }
}
