namespace News4Devs.Client.Components.People
{
    public partial class Contacts: PersonsBase
    {
        protected override string GetUrl()
        {
            return $"{ClientConstants.BaseUrl}/users/contacts/{currentUserId}?pageNumber={currentPage}&pageSize={ClientConstants.MaxPageSize}";
        }
    }
}
