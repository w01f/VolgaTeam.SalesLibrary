using SalesLibraries.Browser.Controls.BusinessClasses.Helpers;

namespace SalesLibraries.Browser.Controls.Controls.WebPage
{
	public partial class WebKitPage
	{
		public void UpdateYouTubeState()
		{
			_siteContainer.ParentBundle.ButtonExtensionsDownloadYouTube.Visible = YouTubeHelper.IsUrlYouTubeWatch(_webKit.WebView.Url);
			_siteContainer.ParentBundle.barMain.RecalcLayout();
		}

		public void DownloadYouTube()
		{
			_webKit.WebView.EvalScript(
				"(function(w,d,x){x=new(window.XMLHttpRequest||ActiveXObject)('Microsoft.XMLHTTP');x.onreadystatechange=function(){if(x.readyState==4){if(x.status==200)d.body.appendChild(d.createElement('script')).src=JSON.parse(x.responseText).query.results.url;else console.log('ERR',x.status,x.statusText)}};x.open('GET','//query.yahooapis.com/v1/public/yql?q='+encodeURIComponent('select * from json where url=\"http://noflr.deturl.com/l.asp\"')+'&format=json');x.send()})(window,document);");
		}
	}
}
