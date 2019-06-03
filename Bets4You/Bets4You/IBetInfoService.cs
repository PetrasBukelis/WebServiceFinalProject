using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Bets4You
{
    [ServiceContract]
    public interface IBetInfoService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AllBets();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "AddBets?betName={betName}&category={category}&coefficient={coefficient}&submitorName={submitorName}")]
        string AddBets(string betName, string category, int coefficient, string submitorName);
    }
}
