using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using XamarinNetworkProj.Model;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinNetworkProj
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "network.db";
        public static FriendAsyncRepository friendsTable;
        public static SQLiteAsyncConnection database;
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
                    if(database == null)
                        database = new SQLiteAsyncConnection(dbPath);
                    friendsTable = new FriendAsyncRepository(database);
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
                    if (database == null)
                        database = new SQLiteAsyncConnection(dbPath);
                    postsTable = new PostAsyncRepository(database);
                }
                return postsTable;
            }
        }

        static private Account AccountConstr(string nickname, string phone, string desc, int id = 0)
        {
            Account a = new Account();
            a.Id = id;
            a.nickname = nickname;
            a.phone = phone;
            a.desc = desc;
            a.likedPosts = new byte[] {  };
            return a;
        }
        public Post PostConstr(int autorId, string content, int likes, DateTime postedOn, int id = 0)
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

            //App.FriendsTable.CreateTable();

            //App.FriendsTable.Clear();
            //App.FriendsTable.InsertItemAsync(AccountConstr("LunarDreamer", "123", "Dance in the moonlight and dream with the stars."));
            //App.FriendsTable.InsertItemAsync(AccountConstr("MysticSoul ", "123", "Embrace the mysteries of the universe within your soul."));
            //App.FriendsTable.InsertItemAsync(AccountConstr("EternalExplorer", "456", "Venture into the depths of the unknown and uncover eternal truths."));
            //App.FriendsTable.InsertItemAsync(AccountConstr("ShadowSeeker", "789", "Embrace the darkness within to find the light that guides your path."));
            //App.FriendsTable.InsertItemAsync(AccountConstr("EternalExplorer", "456", "Venture into the depths of the unknown and uncover eternal truths."));
            //App.FriendsTable.InsertItemAsync(AccountConstr("ShadowSeeker", "789", "Embrace the darkness within to find the light that guides your path."));
            //App.FriendsTable.InsertItemAsync(AccountConstr("SoulfulSeeker", "369", "Seek the depths of your soul to discover the true essence of your being."));
            //App.FriendsTable.InsertItemAsync(AccountConstr("StarryDreamer", "579", "Dream under the starlit sky and let the universe whisper its secrets to your soul."));


            List<Account> a = App.FriendsTable.database.Table<Account>().OrderBy(f => f.Id).ToListAsync().Result;
            int minId = a[0].Id;
            //App.PostsTable.Clear();
            //App.PostsTable.InsertItemAsync(PostConstr(minId, "hi im 1", 2, DateTime.Now));
            //App.PostsTable.InsertItemAsync(PostConstr(minId + 1, "hi im 2", 2, DateTime.Now));
            //App.PostsTable.InsertItemAsync(PostConstr(minId + 2, "hi im 3", 2, DateTime.Now));

            Account user = AccountConstr("LunarDreamer", "123", "Dance in the moonlight and dream with the stars.", minId);

            if (!App.Current.Properties.ContainsKey("user"))
                App.Current.Properties.Add("user", JsonConvert.SerializeObject(user));
            Application.Current.SavePropertiesAsync();

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
