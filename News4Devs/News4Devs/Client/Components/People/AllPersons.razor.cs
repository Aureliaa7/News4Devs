namespace News4Devs.Client.Components.People
{
    public partial class AllPersons : PersonsBase
    {
        protected override string GetUrl()
        {
            return $"{ClientConstants.BaseUrl}/accounts?pageNumber={currentPage}&pageSize={ClientConstants.MaxPageSize}";
        }
    }
}
