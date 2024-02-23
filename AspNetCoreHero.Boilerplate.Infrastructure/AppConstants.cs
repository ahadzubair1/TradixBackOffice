namespace AspNetCoreHero.Boilerplate.Application;
internal class AppConstants
{
    public class Procedures
    {
        public const string GetUserNetworkFarNode = "[GetUserNetworkFarNode]";
        public const string GetUserDownlineNetworkTree = "[GetUserDownlineNetworkTree]";
        public const string GetUserUplineNetworkTree = "[GetUserUplineNetworkTree]";
        public const string GetUserDashboardData = "[GetUserDashboardData]";
        //var res = await _mediator.Send(new GetParentUserViaDapperRequest(Guid.Parse(referredBy), position));
    }
}
