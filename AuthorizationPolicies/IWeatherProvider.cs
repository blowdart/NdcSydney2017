namespace AuthorizationPolicies
{
    public interface IWeatherProvider
    {
        Season GetSeason();

        void SetSeason(Season season);
    }

    public enum Season
    {
        Winter,
        Spring,
        Summer,
        Autumn
    }
}
