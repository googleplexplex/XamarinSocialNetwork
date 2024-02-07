using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using XamarinNetworkProj.Model;
using SQLite;

namespace XamarinNetworkProj
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "network.db";
        public static FriendAsyncRepository friendsTable;
        public static FriendAsyncRepository FriendsTable
        {
            get
            {
                if (friendsTable == null)
                {
                    // путь, по которому будет находиться база данных
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
                    // если база данных не существует (еще не скопирована)
                    if (!File.Exists(dbPath))
                    {
                        // получаем текущую сборку
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        // берем из нее ресурс базы данных и создаем из него поток
                        using (Stream stream = assembly.GetManifestResourceStream($"XamarinNetworkProj.{DATABASE_NAME}"))
                        {
                            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                                fs.Flush();
                            }
                        }
                    }
                    friendsTable = new FriendAsyncRepository(dbPath);
                }
                return friendsTable;
            }
        }
        public static PostAsyncRepository postsTable;
        public static PostAsyncRepository PostsTable
        {
            get
            {
                if (postsTable == null)
                {
                    // путь, по которому будет находиться база данных
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
                    // если база данных не существует (еще не скопирована)
                    if (!File.Exists(dbPath))
                    {
                        // получаем текущую сборку
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        // берем из нее ресурс базы данных и создаем из него поток
                        using (Stream stream = assembly.GetManifestResourceStream($"XamarinNetworkProj.{DATABASE_NAME}"))
                        {
                            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                                fs.Flush();
                            }
                        }
                    }
                    postsTable = new PostAsyncRepository(dbPath);
                }
                return postsTable;
            }
        }

        private Account AccountConstr(string nickname, string phone, string desc, int id = 0)
        {
            Account a = new Account();
            a.Id = id;
            a.nickname = nickname;
            a.phone = phone;
            a.desc = desc;
            return a;
        }
        private Post PostConstr(int autorId, string content, int likes, DateTime postedOn, int id = 0)
        {
            Post a = new Post();
            a.Id = id;
            a.autorId = autorId;
            a.content = content;
            a.likes = likes;
            a.postedOn = postedOn;
            return a;
        }
        public App()
        {
            InitializeComponent();

            App.FriendsTable.Clear();
            App.FriendsTable.InsertItemAsync(AccountConstr("LunarDreamer", "123", "Dance in the moonlight and dream with the stars."));
            App.FriendsTable.InsertItemAsync(AccountConstr("MysticSoul ", "123", "Embrace the mysteries of the universe within your soul."));
            App.FriendsTable.InsertItemAsync(AccountConstr("EternalExplorer", "456", "Venture into the depths of the unknown and uncover eternal truths."));
            App.FriendsTable.InsertItemAsync(AccountConstr("ShadowSeeker", "789", "Embrace the darkness within to find the light that guides your path."));
            App.FriendsTable.InsertItemAsync(AccountConstr("EternalExplorer", "456", "Venture into the depths of the unknown and uncover eternal truths."));
            App.FriendsTable.InsertItemAsync(AccountConstr("ShadowSeeker", "789", "Embrace the darkness within to find the light that guides your path."));
            App.FriendsTable.InsertItemAsync(AccountConstr("SoulfulSeeker", "369", "Seek the depths of your soul to discover the true essence of your being."));
            App.FriendsTable.InsertItemAsync(AccountConstr("StarryDreamer", "579", "Dream under the starlit sky and let the universe whisper its secrets to your soul."));

            App.PostsTable.Clear();
            App.PostsTable.InsertItemAsync(PostConstr(1, "hi im 1", 2, DateTime.Now));
            App.PostsTable.InsertItemAsync(PostConstr(2, "hi im 2", 2, DateTime.Now));
            App.PostsTable.InsertItemAsync(PostConstr(3, "hi im 3", 2, DateTime.Now));

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
