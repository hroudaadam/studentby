using Microsoft.AspNetCore.Mvc;
using Studentby.Pres.WebApi.Models.Common;

namespace Studentby.Pres.WebApi.Atributes;

internal class ProducesErrorResponseAttribute : ProducesResponseTypeAttribute
{
    public ProducesErrorResponseAttribute(int errorStatus) : base(typeof(ErrorRes), errorStatus)
    {
    }
}
