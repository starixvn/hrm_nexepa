namespace NexEPA.Web.Attributes
{
    using System.Net.Http;
    using System.Web.Http.Filters;
    using NexEPA.Core.Utilities;

    public class DeflateCompressionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actContext)
        {
            if (actContext.Response != null)
            {
                HttpContent content = actContext.Response.Content;
                byte[] bytes = content == null ? null : content.ReadAsByteArrayAsync().Result;
                byte[] zlibbedContent = bytes == null ? new byte[0] : CommonHelper.DeflateByte(bytes);

                actContext.Response.Content = new ByteArrayContent(zlibbedContent);
                actContext.Response.Content.Headers.Remove("Content-Type");
                actContext.Response.Content.Headers.Add("Content-encoding", "deflate");
                actContext.Response.Content.Headers.Add("Content-Type", "application/json");
            }

            base.OnActionExecuted(actContext);
        }
    }
}
