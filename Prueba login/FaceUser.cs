namespace Xamarin.Auth.Prueba_login
{
    public class Data
    {
        public bool is_silhouette { get; set; }
        public string url { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }

    public class Cover
    {
        public string id { get; set; }
        public int offset_x { get; set; }
        public int offset_y { get; set; }
        public string source { get; set; }
    }

    public class FaceUser
    {
        public string name { get; set; }
        public Picture picture { get; set; }
        public Cover cover { get; set; }
        public string id { get; set; }
    }

}