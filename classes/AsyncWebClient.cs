using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace TumblRipper2.classes
{
	public class AsyncWebClient
    {
		public AsyncWebClient()
		{
		}

		public Task<byte[]> GetDataAsync(string url)
		{
			return this.GetDataAsync(url, CancellationToken.None);
		}

		public Task<byte[]> GetDataAsync(string url, CancellationToken token)
		{
            // create completion source
            var tcs = new TaskCompletionSource<byte[]>();

            // create a web client for downloading the string
            var wc = new WebClient();

            // Set up variable for referencing anonymous event handler method. We
            // need to first assign null, and then create the method so that we
            // can reference the variable within the method itself.
            DownloadDataCompletedEventHandler downloadCompletedHandler = null;

            // Set up an anonymous method that will handle the DownloadStringCompleted
            // event.
            downloadCompletedHandler = (s, e) =>
            {
                wc.DownloadDataCompleted -= downloadCompletedHandler;
				if (e.Cancelled)
				{
                    tcs.TrySetCanceled();
				}
				else if (e.Error == null)
				{
                    tcs.TrySetResult(e.Result);
				}
				else
				{
                    tcs.TrySetException(e.Error);
				}
                wc.Dispose();
			};

            // Subscribe to the completed event
            wc.DownloadDataCompleted += downloadCompletedHandler;
			try
			{
                wc.DownloadDataAsync(new Uri(url));
				token.Register(new Action(wc.CancelAsync));
			}
			catch (Exception exception)
			{
                tcs.TrySetException(exception);
			}
			return tcs.Task;
		}
	}
}