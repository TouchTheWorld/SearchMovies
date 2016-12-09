using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace SearchMoives
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        List<Subjects> list = new List<Subjects>();
      
        private async void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string content = await GetHttpClient("https://api.douban.com/v2/movie/search?q={" +SearchBox.Text +"}");
            JObject jsonobj = JObject.Parse(content);
            string json = jsonobj["subjects"].ToString();
            list = JsonConvert.DeserializeObject<List<Subjects>>(json);
            ResultListView.ItemsSource = list;
        }
        private async Task<string> GetHttpClient(string uri)
        {
            string content = "";
            return await Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                System.Net.Http.HttpResponseMessage response;
                response = httpClient.GetAsync(new Uri(uri)).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    content = response.Content.ReadAsStringAsync().Result;
                return content;
            });
        }
    }
    public class Rootobject
    {
        public int count { get; set; }
        public string start { get; set; }
        public int total { get; set; }
        public string title { get; set; }
        public Subjects[] subjiects{ get; set; }
    }
    public class Subjects
    {
        public string title { get; set; }
        public string year { get; set; }
        public Rating rating { get; set; }
        public string[] genres { get; set; }
        public Casts[] casts { get; set; }
        public int collect_count { get; set; }
        public string original_title { get; set; }
        public Directors[] directors { get; set; }
        public string alt { get; set; }
        public string id { get; set; }

    }
    public class Rating
    {
        public int max { get; set; }
        public string average { get; set; }
        public string stars { get; set; }
        public int min { get; set; }
    }
    public class Casts
    {
        public string alt { get; set; }
        public Avatars avatars { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }
    public class Avatars
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }
    public class Directors
    {
        public string alt { get; set; }
        public Avatars avatars { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }
    public class Images
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }
}
